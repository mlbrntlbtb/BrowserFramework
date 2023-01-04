using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Reflection;
using CommonLib.DlkUtility;
using TestRunner.AdvancedScheduler.View;
using TestRunner.AdvancedScheduler.Model;
using System.Collections.Concurrent;
using System.Globalization;
using CommonLib.DlkSystem;
using TestRunner.Common;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;

namespace TestRunner.AdvancedScheduler.Presenter
{
    public class ScheduleRunnerPresenter
    {
        private IScheduleRunnerView mView = null;
        private List<TestLineupRecord> SchedulesForExecution;
        private List<TestLineupRecord> allSchedule;
        private List<TestLineupRecord> PendingRunNowSchedules = new List<TestLineupRecord>();
        private LocalMachineManifestItem localMachineProcessManifest = null;
        private ConcurrentDictionary<string, AgentManifestItem> agentProcessManifest = new ConcurrentDictionary<string, AgentManifestItem>();
        List<string> lowPrioAgents = new List<string>();
        private Object agentSchedulerLock = new Object();
        private Object localSchedulerLock = new Object();
        private Object loadScheduleLock = new Object();
        private Object schedulePresenterLock = new Object();

        public ScheduleRunnerPresenter(IScheduleRunnerView view)
        {
            this.mView = view;
            SchedulesForExecution = new List<TestLineupRecord>();
            allSchedule = new List<TestLineupRecord>();
        }

        /// <summary>
        /// Queue up test once we reach the scheduled time 
        /// </summary>
        public void LoadSchedulesForExecution(int environmentCount)
        {
            lock (loadScheduleLock)
            {
                while (!mView.AllowExecute)
                {
                    Thread.Sleep(500);
                }
                LoadSchedules();

                /* get all for today */
                // monthly 
                List<TestLineupRecord> lstMonthly = allSchedule.FindAll(x => x.Recurrence == Enumerations.RecurrenceType.Monthly && ConvertDateToDay(x.Schedule) == DateTime.Today.Day.ToString());
                List<TestLineupRecord> lstWeekly = allSchedule.FindAll(x => x.Recurrence == Enumerations.RecurrenceType.Weekly && x.Schedule.ToString() == Enumerations.ConvertToString(DateTime.Today.DayOfWeek));
                List<TestLineupRecord> lstOnce = allSchedule.FindAll(x => x.Recurrence == Enumerations.RecurrenceType.Once && x.Schedule.ToString() == DateTime.Today.ToShortDateString());

                /* daily */
                List<TestLineupRecord> all = allSchedule.FindAll(x => x.Recurrence == Enumerations.RecurrenceType.Daily);
                all.AddRange(lstMonthly);
                all.AddRange(lstWeekly);
                all.AddRange(lstOnce);

                /* Weekdays */
                if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
                {
                    List<TestLineupRecord> lstWeekdays = allSchedule.FindAll(x => x.Recurrence == Enumerations.RecurrenceType.Weekdays);
                    all.AddRange(lstWeekdays);
                }

                //add scheduled tests that are not added yet to our queue and are Enabled
                var scheduledRecord = all.FindAll(x => x.StartTime.Hour == DateTime.Now.Hour && x.StartTime.Minute == DateTime.Now.Minute && (x.Enabled || x.Dependent=="True"));

                if (scheduledRecord.Count > 0 && environmentCount == 1)
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, "Scheduled run failed");
                    return;
                }
                
                foreach (var record in scheduledRecord)
                {
                    if (!SchedulesForExecution.Any(x => x.Id == record.Id))
                    {
                        record.Status = Enumerations.TestStatus.Pending;
                        mView.UpdateScheduleRecord(record);
                        //do not add group members, because for grouped items we only want to add group headers
                        if (!record.IsInGroup || record.GroupID == record.Id)
                        {
                            SchedulesForExecution.Add(record);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check status, update models and Execute tests in queue for local machine
        /// </summary>
        public void ExecuteQueueItemsOnLocalMachine()
        {
            //locking to avoid conflict with main ui thread
            lock (schedulePresenterLock)
            {
                //UPDATE COMPLETED STATUS
                if (localMachineProcessManifest != null && !localMachineProcessManifest.Process.IsAlive)
                {
                    var currentSched = localMachineProcessManifest.Schedule;
                    currentSched.Status = GetLastResultStatus(currentSched, Environment.MachineName);
                    mView.UpdateScheduleRecord(currentSched);

                    //cancel
                    if (localMachineProcessManifest.IsCancelling)
                    {
                        currentSched.Status = Enumerations.TestStatus.Cancelled;
                        if (PendingRunNowSchedules.Any(x => x.Id == currentSched.Id))
                        {
                            PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == currentSched.Id));
                            if (currentSched.IsInGroup)
                            {
                                PendingRunNowSchedules.RemoveAll(x => x.GroupID == currentSched.GroupID);
                            }
                        }
                        mView.UpdateScheduleRecord(currentSched);
                        localMachineProcessManifest = null;
                        return;
                    }

                    //set manifest to empty
                    localMachineProcessManifest = null;

                    if (currentSched.Status == Enumerations.TestStatus.Failed  && currentSched.CurrentRun < currentSched.NumberOfRuns)
                    {
                        //check if we need rerun
                        TryRunProcessOnLocalMachine(currentSched, runNumber:currentSched.CurrentRun + 1);
                    }
                    else
                    {
                        //check if there were reruns, send consolidated report
                        if (currentSched.SendConsolidatedReport && currentSched.CurrentRun > 1)
                        {
                            SendConsolidateRerunReport(currentSched, Environment.MachineName, currentSched.CurrentRun);
                        }

                        if (PendingRunNowSchedules.Any(x => x.Id == currentSched.Id))
                        {
                            PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == currentSched.Id));
                        }

                        //handle grouping scenario
                        var nextItemInGroup = GetNextItemInGroup(currentSched);
                        if (nextItemInGroup != null)
                        {
                            RunGroupSuiteLocally(nextItemInGroup, currentSched);
                        }
                    }
                }

                //START QUEUE PROCESS
                //get queued schedules for local machine
                var schedule = SchedulesForExecution.FirstOrDefault(x => x.RunningAgent.Name == "Local Machine");
                if (PendingRunNowSchedules.Count > 0) // check for pending Run Now schedules
                {
                    schedule = PendingRunNowSchedules.FirstOrDefault(x => x.RunningAgentName == "Local Machine");
                }

                if(SchedulesForExecution.Count > 0)
                {
                    DlkTestRunnerApi.mTestScheduled = true;
                }

                //if a schedule is queued and local machine is not busy - run the test
                TryRunProcessOnLocalMachine(schedule);
            }
        }

        /// <summary>
        /// Execute tests in queue for agents
        /// </summary>
        public void ExecuteQueueItemsOnAgents()
        {
            //locking to avoid conflict with main ui thread
            lock (schedulePresenterLock)
            {
                //UPDATE TEST STATUS for vm runs
                var agentManifestKeys = agentProcessManifest.Keys;
                foreach (var key in agentManifestKeys)
                {
                    UpdateAgentTestStatus(key);
                }

                //get schedules that are queued for ANY vm -- //create filter for items not already queued in manifest
                var schedules = SchedulesForExecution.Where(x => x.RunningAgent.Name != Agent.LOCAL_MACHINE && !agentProcessManifest.Keys.Any(y => y == x.Id));

                //get low prio agents (agents specifically requested by schedules)
                lowPrioAgents = new List<string>();
                var selectedAgents = agentProcessManifest.Where(x => x.Value.Status == Enumerations.AgentExecutionStatus.Rerun || x.Value.Status == Enumerations.AgentExecutionStatus.NextInGroup)
                    .Select(x => allSchedule.First(y => y.Id == x.Key).RunningAgent.Name)
                    .Where(x => x != Agent.ANY_AGENT_NAME);
                lowPrioAgents.AddRange(selectedAgents);
                selectedAgents = schedules.Select(x => x.RunningAgent.Name).Where(x => x != Agent.ANY_AGENT_NAME);
                lowPrioAgents.AddRange(selectedAgents);

                //START QUEUE PROCESS - utilize manifest to track status of suite
                QueueReruns();
                QueueNextInGroups();
                QueueTestSchedules(schedules);
            }
        }

        /// <summary>
        /// Check if there are any test currently running
        /// </summary>
        /// <returns></returns>
        public bool IsTestRunning()
        {
            if (localMachineProcessManifest == null && agentProcessManifest.Keys.Count() == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if Suite should be executed
        /// </summary>
        /// <param name="Suite">Target suite</param>
        /// <param name="AllSuites">All suites in queue</param>
        /// <param name="LastResults">Previous results list</param>
        /// <returns>True if needs to execute, false otherwise</returns>
        private bool IsForExecution(TestLineupRecord Suite, TestLineupRecord LastRunSuite = null)
        {
            bool ret;
            bool bDependent;
            bool bExecute = Suite.Execute;


            /* Execute value depends on result of a preceding suite in queue */
            if (bool.TryParse(Suite.Dependent, out bDependent) && bDependent)
            {
                ret = bExecute;
                if (Suite.ExecuteDependencySuiteRow.ToLower() == "preceding") // check is based on preceding suite
                {
                    ret = LastRunSuite.StatusText.ToLower() == Suite.ExecuteDependencyResult.ToLower() ? bExecute : !bExecute;
                }
                else if (Suite.ExecuteDependencySuiteRow.ToLower() == "first") // check is based on first suite in group
                {
                    ret = GetLastResultStatus(GetFirstItemInGroup(LastRunSuite)).ToString().ToLower() == Suite.ExecuteDependencyResult.ToLower() ? bExecute : !bExecute;
                }
            }
            else /* not dependent, determine execute value outright */
            {
                ret = bExecute;
            }
            return ret;
        }

        /// <summary>
        /// Load schedules from scheduler xml file
        /// </summary>
        private void LoadSchedules()
        {
            allSchedule.Clear();
            allSchedule = DlkAdvancedSchedulerFileHandler.GetSchedulesLineup();
        }

        /// <summary>
        /// Create process to run on local machine. Update the manifest and status of objects/lists
        /// </summary>
        /// <param name="schedule"></param>
        private bool TryRunProcessOnLocalMachine(TestLineupRecord schedule, string scheduledStart = "", int runNumber = 1)
        {
            lock (localSchedulerLock)
            {
                if (schedule != null && localMachineProcessManifest == null)
                {
                    //Setup manifest file
                    var processThread = new Thread(() => { LocalMachine_DoWork(schedule, runNumber, scheduledStart); });
                    localMachineProcessManifest = new LocalMachineManifestItem(schedule, processThread);
                    //Run the process
                    processThread.Start();
                    //Update test status
                    schedule.CurrentRun = runNumber;
                    schedule.Status = Enumerations.TestStatus.Running;
                    mView.UpdateScheduleRecord(schedule);
                    //remove item from execution list
                    SchedulesForExecution.Remove(schedule);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// thread work running the schedule on local machine
        /// </summary>
        private void LocalMachine_DoWork(TestLineupRecord schedule, int runNumber = 1, string scheduledStart = "")
        {
            try
            {
                string preScripts = PrepareExternalScript(schedule.PreExecutionScripts);
                string postScripts = PrepareExternalScript(schedule.PostExecutionScripts);
                string startScheduleDateTime = "";

                if (String.IsNullOrEmpty(scheduledStart))
                {
                    DateTime today = DateTime.Now;  //date is always today  / execution date
                    startScheduleDateTime =
                        new DateTime(today.Year, today.Month, today.Day, schedule.StartTime.Hour, schedule.StartTime.Minute,
                            schedule.StartTime.Second).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    //for run now functionality - use exact date/time where run now was triggered
                    startScheduleDateTime = scheduledStart;
                }

                var arguments = new DlkTestRunnerCmdLibExecutionArgs() 
                { 
                    FilePath = schedule.TestSuiteToRun.Path,
                    Library = Path.GetFileName(schedule.Library),
                    Product = schedule.Product,
                    RunNumber = runNumber,
                    EmailDistro = string.Join(";", schedule.DistributionList),
                    SendPreEmail = schedule.EmailOnStart,
                    PreScript = preScripts,
                    PostScript = postScripts,
                    RerunFailedTestsOnly = schedule.RerunFailedTestsOnly,
                    NumberOfRuns = schedule.NumberOfRuns,
                    ScheduledStart = startScheduleDateTime
                };

                SetupArgumentOverrideProperties(arguments, schedule.Browser.Name, schedule.Environment);

                string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string mRootPath = Directory.GetParent(binDir).FullName;
                while (new DirectoryInfo(mRootPath).GetDirectories()
                    .Where(x => x.FullName.Contains("Products")).Count() == 0)
                {
                    mRootPath = Directory.GetParent(mRootPath).FullName;
                }

                /* Initialize config file */
                DlkTestRunnerSettingsHandler.Initialize(mRootPath);
                DlkTestRunnerSettingsHandler.ApplicationUnderTest = DlkTestRunnerSettingsHandler.ApplicationList.Find(
                    x => x.ProductFolder == DlkTestRunnerCmdLib.GetProductFolder(schedule.TestSuiteToRun.Path));

                //run test
                DlkTestRunnerCmdLib.Run(arguments);

                //reset flags
                DlkTestRunnerCmdLib.ResetFlags();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void SetupArgumentOverrideProperties(DlkTestRunnerCmdLibExecutionArgs arguments, string browser, string environment)
        {
            if (browser != TestLineupRecord.DEFAULT_BROWSER)
            {
                arguments.BrowserOverride = browser;
            }
            if (environment != TestLineupRecord.DEFAULT_ENVIRONMENT)
            {
                var configHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile);
                var loginConfig = configHandler.mLoginConfigRecords.FirstOrDefault(x => x.mID == environment);
                if (loginConfig != null)
                {
                    arguments.LoginConfigOverride = new DlkLoginConfigRecord("*" + loginConfig.mID, loginConfig.mUrl, loginConfig.mUser, loginConfig.mPassword, loginConfig.mDatabase,
                                                                                loginConfig.mPin, string.Empty, loginConfig.mMetaData);
                }
            }
        }

        /// <summary>
        /// Stop the thread execution on local machine
        /// </summary>
        /// <param name="schedule"></param>
        private void StopProcessOnLocalMachine(TestLineupRecord schedule)
        {
            DlkTestRunnerCmdLib.Stop();
        }

        /// <summary>
        /// try reserve an agent and execute the schedule
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool TryReserveAgent(Agent agent, TestLineupRecord schedule, string scheduledStart = "", int runNumber = 1)
        {
            string startScheduleDateTime = "";

            if (String.IsNullOrEmpty(scheduledStart))
            {
                DateTime today = DateTime.Now;  //date is always today  / execution date
                startScheduleDateTime =
                    new DateTime(today.Year, today.Month, today.Day, schedule.StartTime.Hour, schedule.StartTime.Minute,
                        schedule.StartTime.Second).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                //for run now functionality - use exact date/time where run now was triggered
                startScheduleDateTime = scheduledStart;
            }

            bool success = false;

            lock (agentSchedulerLock)
            {
                if (agent.Status == Enumerations.AgentStatus.Ready)
                {
                    agent.IsReserved = true;
                    agentProcessManifest.TryAdd(schedule.Id, new AgentManifestItem()
                    {
                        Agent = agent,
                        Status = Enumerations.AgentExecutionStatus.Waiting,
                        RunNumber = runNumber,
                        IsCancelling = false,
                        Cancelled = false
                    });
                    string preScripts = PrepareExternalScript(schedule.PreExecutionScripts);
                    string postScripts = PrepareExternalScript(schedule.PostExecutionScripts);

                    //email distro - include default emails
                    var emailDistro = schedule.DistributionList.Select(x => x).ToList();
                    bool useDefaultEmail;
                    Boolean.TryParse(DlkConfigHandler.GetConfigValue("usedefaultemail"), out useDefaultEmail);
                    if (useDefaultEmail)
                    {
                        string defaultEmails = DlkConfigHandler.GetConfigValue("defaultemail");
                        if (!String.IsNullOrWhiteSpace(defaultEmails))
                            emailDistro.AddRange(new List<string>(defaultEmails.Split(';')));
                    }

                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        var argumentObj = new DlkTestRunnerCmdLibExecutionArgs()
                        {
                            ScheduleId = schedule.Id,
                            MachineName = Environment.MachineName,
                            FilePath = schedule.TestSuiteToRun.Path,
                            Library = Path.GetFileName(schedule.Library),
                            Product = schedule.Product,
                            RunNumber = runNumber,
                            EmailDistro = string.Join(";", emailDistro),
                            SendPreEmail = schedule.EmailOnStart,
                            PreScript = preScripts,
                            PostScript = postScripts,
                            RerunFailedTestsOnly = schedule.RerunFailedTestsOnly,
                            NumberOfRuns = schedule.NumberOfRuns,
                            ScheduledStart = startScheduleDateTime
                        };

                        SetupArgumentOverrideProperties(argumentObj, schedule.Browser.Name, schedule.Environment);

                        //this could block the thread if vm is offline, hence the thread and manifest
                        string reply = AgentUtility.SendCommandToServer(agent.Name, argumentObj.CreateSchedulerExecutionString());
                        agentProcessManifest[schedule.Id].Status = reply == null || reply.ToLower().StartsWith("error") ?
                            Enumerations.AgentExecutionStatus.Error : Enumerations.AgentExecutionStatus.Started;

                        agent.IsReserved = false;
                        //System.Diagnostics.Debug.WriteLine("MANIFEST STATUS: " + agentProcessManifest[schedule.Id].Status + " of: " + schedule.Id);
                    });
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Stop the thread execution on an agent machine
        /// </summary>
        /// <param name="schedule"></param>
        private void StopProcessOnAgent(string agentName)
        {
            AgentUtility.SendCommandToServer(agentName, "stop");
        }

        /// <summary>
        /// Remove agent from view
        /// </summary>
        /// <param name="agents"></param>
        public bool RemoveAgent(Agent agent)
        {
            bool success = false;

            lock (schedulePresenterLock)
            {
                if (agent.Status != Enumerations.AgentStatus.Busy || agent.Status != Enumerations.AgentStatus.Reserved)
                {
                    if (!agentProcessManifest.Any(x=>x.Value.Agent.Name == agent.Name))
                    {
                        mView.RemoveAgent(agent);
                        return true;
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Set agents disable value
        /// </summary>
        /// <param name="agents"></param>
        public void SetAgentsDisabled(IEnumerable<Agent> agents, bool disabled)
        {
            lock (schedulePresenterLock)
            {
                foreach (var agent in agents)
                {
                    if (agent.Status != Enumerations.AgentStatus.Busy || agent.Status != Enumerations.AgentStatus.Reserved)
                    {
                        if (!agentProcessManifest.Any(x => x.Value.Agent.Name == agent.Name))
                        {
                            agent.Disabled = disabled;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get latest agents
        /// </summary>
        /// <param name="agents"></param>
        /// <param name="productFolderName"></param>
        public void GetLatestAgents(IEnumerable<Agent> agents, string productFolderName)
        {
            lock (schedulePresenterLock)
            {
                foreach (var agent in agents)
                {
                    if (agent.Status != Enumerations.AgentStatus.Busy || agent.Status != Enumerations.AgentStatus.Reserved)
                    {
                        if (!agentProcessManifest.Any(x => x.Value.Agent.Name == agent.Name))
                        {
                            agent.GetLatestFolderName = productFolderName;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Runs any schedule now whether in a local machine or any available agent.
        /// </summary>
        /// <param name="schedule">selected TestLineupRecord object</param>
        /// <returns>whether execution was successful or not</returns>
        public bool RunNow(TestLineupRecord schedule, string scheduledStart) 
        {
            bool success = false;

            //Load latest schedule
            LoadSchedules();

            if(schedule.IsInGroup)
            {
                //grouped items should run the whole group
                schedule = allSchedule.FirstOrDefault(x=>x.Id == schedule.GroupID);
            }
            else
            {
                //get schedule reference from presenter
                schedule = allSchedule.FirstOrDefault(x => x.Id == schedule.Id);
            }

            //locking to avoid conflict with execution thread
            lock (schedulePresenterLock)
            {
                if (schedule != null)
                {
                    if (schedule.RunningAgent.Name == Agent.LOCAL_MACHINE && schedule.RunningAgent.AgentType == Agent.AGENT_TYPE_LOCAL)
                    {
                        success = TryRunProcessOnLocalMachine(schedule, scheduledStart);
                    }
                    else
                    {
                        if (schedule.RunningAgent.AgentType == AgentGroup.AGENT_TYPE_GROUPS)
                        {
                            var agent = GetNextAvailableAgentInGroup(schedule.RunningAgent.Name);
                            if (agent != null)
                            {
                                success = TryReserveAgent(agent, schedule, scheduledStart);
                            }
                        }
                        else if (schedule.RunningAgent.Name == Agent.ANY_AGENT_NAME)
                        {
                            while (mView.GetAvailableAgents().Count() > 0 && !success)
                            {
                                var agent = GetNextAvailableAgent();
                                //try reserving agent, if it fails, skip and go to next agent
                                success = TryReserveAgent(agent, schedule, scheduledStart);
                            }
                        }
                        else
                        {
                            var agent = GetAvailableAgent(schedule.RunningAgent.Name);
                            if (agent != null)
                            {
                                success = TryReserveAgent(agent, schedule, scheduledStart);
                            }
                        }

                        //update schdule status
                        if (success)
                        {
                            schedule.Status = Enumerations.TestStatus.Pending;
                            mView.UpdateScheduleRecord(schedule);
                        }
                    }
                }
            }

            //set group items' status to pending
            if (success && schedule.IsInGroup)
            {
                foreach(var sched in allSchedule.Where(x=>x.GroupID == schedule.GroupID))
                {
                    if(sched.Id != schedule.GroupID)
                    {
                        sched.Status = Enumerations.TestStatus.Pending;
                        mView.UpdateScheduleRecord(sched);
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// Stores schedules set to Run Now to a separate list to be run when able
        /// </summary>
        /// <param name="schedule">selected TestLineupRecord object</param>
        public void PendSchedulesForRunNow(TestLineupRecord schedule)
        {
            //Load latest schedule
            LoadSchedules();
            if (schedule.IsInGroup)
            {
                schedule = allSchedule.FirstOrDefault(x => x.Id == schedule.GroupID);
                foreach (var sched in allSchedule.Where(x => x.GroupID == schedule.GroupID))
                {
                    sched.Status = Enumerations.TestStatus.Pending;
                    mView.UpdateScheduleRecord(sched);
                    PendingRunNowSchedules.Add(sched);
                }
            }
            else
            {
                //get schedule reference from presenter
                schedule = allSchedule.FirstOrDefault(x => x.Id == schedule.Id);
                schedule.Status = Enumerations.TestStatus.Pending;
                PendingRunNowSchedules.Add(schedule);
                mView.UpdateScheduleRecord(schedule);
            }
        }

        /// <summary>
        /// Cancel a test currently running
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public void CancelTest(TestLineupRecord schedule)
        {
            //Load latest schedule
            LoadSchedules();

            //locking to avoid conflict with execution thread
            lock (schedulePresenterLock)
            {
                var currSched = allSchedule.FirstOrDefault(x => x.Id == schedule.Id);

                if (currSched != null)
                {
                    if (currSched.IsInGroup)
                    {
                        //GROUPED items should stop test currently running and cancel other pending items
                        foreach(var groupItem in allSchedule.Where(x=> x.GroupID == currSched.GroupID))
                        {
                            CancelRunningSchedule(groupItem);
                        }
                    }
                    else
                    {
                        CancelRunningSchedule(currSched);
                    }
                }
            }
        }

        /// <summary>
        /// Method to switch pending schedule position
        /// </summary>
        /// <returns></returns>
        public bool TrySwitchPendingSchedules(string id1, string id2)
        {
            lock (schedulePresenterLock)
            {
                if (SchedulesForExecution.Any(x => x.Id == id1) && SchedulesForExecution.Any(x => x.Id == id2))
                {
                    var index1 = SchedulesForExecution.IndexOf(SchedulesForExecution.First(x=>x.Id == id1));
                    var index2 = SchedulesForExecution.IndexOf(SchedulesForExecution.First(x=>x.Id == id2));
                    //swap the items
                    var tmp = SchedulesForExecution[index1];
                    SchedulesForExecution[index1] = SchedulesForExecution[index2];
                    SchedulesForExecution[index2] = tmp;

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Replace an id in pending schedules with a new one
        /// </summary>
        /// <param name="idToRemove"></param>
        /// <param name="idToChangeWith"></param>
        /// <returns></returns>
        public bool TryReplacePendingSchedules(string idToRemove, string idToChangeWith)
        {
            lock (schedulePresenterLock)
            {
                if (SchedulesForExecution.Any(x => x.Id == idToRemove) && !SchedulesForExecution.Any(x => x.Id == idToChangeWith) &&
                    allSchedule.Any(x => x.Id == idToChangeWith))
                {
                    var indexToRemove = SchedulesForExecution.IndexOf(SchedulesForExecution.First(x => x.Id == idToRemove));
                    //change the item
                    SchedulesForExecution[indexToRemove] = allSchedule.First(x => x.Id == idToChangeWith);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// stop/remove schedule in pending/running from queue then set status to cancelled
        /// </summary>
        /// <param name="schedule"></param>
        private void CancelRunningSchedule(TestLineupRecord schedule)
        {
            var schedulerStatus = mView.GetScheduleStatus(schedule);

            if (schedulerStatus == Enumerations.TestStatus.Pending)
            {
                //remove from queue if in queue, grouped items are not in queue but are pending
                if (SchedulesForExecution.Any(x => x.Id == schedule.Id)) 
                {
                    SchedulesForExecution.Remove(SchedulesForExecution.First(x => x.Id == schedule.Id));
                }

                //check if item is already in manifest - agent machine may have a bit of delay in updating status
                //if not in manifest, we can set it to cancelled
                if (!TryRemoveFromManifest(schedule))
                {
                    schedule.Status = Enumerations.TestStatus.Cancelled;
                    mView.UpdateScheduleRecord(schedule);
                }
            }
            else if (schedulerStatus == Enumerations.TestStatus.Running)
            {
                TryRemoveFromManifest(schedule);
            }
            if (PendingRunNowSchedules.Any(x => x.Id == schedule.Id)) // remove if pended by Run Now
            {
                PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == schedule.Id));
                if (schedule.IsInGroup)
                {
                    PendingRunNowSchedules.RemoveAll(x => x.GroupID == schedule.GroupID);
                }
            }
        }

        /// <summary>
        /// Check manifest if schedule is there, if yes, request for cancellation
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private bool TryRemoveFromManifest(TestLineupRecord schedule)
        {
            if (schedule.RunningAgent.Name == Agent.LOCAL_MACHINE)
            {
                if (localMachineProcessManifest != null)
                {
                    StopProcessOnLocalMachine(schedule);
                    schedule.Status = Enumerations.TestStatus.Cancelling;
                    mView.UpdateScheduleRecord(schedule);

                    //set cancelling flag
                    localMachineProcessManifest.IsCancelling = true;

                    return false;
                }
            }
            else
            {
                if (agentProcessManifest.ContainsKey(schedule.Id))
                {
                    var manifest = agentProcessManifest[schedule.Id];
                    //stop on agent machine
                    StopProcessOnAgent(manifest.Agent.Name);
                    //remove from manifest
                    manifest.IsCancelling = true;
                    //update status
                    schedule.Status = Enumerations.TestStatus.Cancelling;
                    mView.UpdateScheduleRecord(schedule);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Update the UI and manifest files based on agent status
        /// </summary>
        /// <param name="scheduleKey"></param>
        private void UpdateAgentTestStatus(string scheduleKey)
        {
            AgentManifestItem outManifest;
            var manifest = agentProcessManifest[scheduleKey];
            if (manifest.IsCancelling)
            {
                if (manifest.Cancelled)
                {
                    agentProcessManifest.TryRemove(scheduleKey, out outManifest);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        string reply = AgentUtility.SendCommandToServer(manifest.Agent.Name, "status");
                        if (reply != null && reply.ToLower().StartsWith("ready"))
                        {
                            manifest.Cancelled = true;
                            //update status
                            var record = allSchedule.FirstOrDefault(x => x.Id == scheduleKey);
                            record.Status = Enumerations.TestStatus.Cancelled;
                            record.AssignedAgentName = string.Empty;
                            mView.UpdateScheduleRecord(record);
                        }
                    });
                }
            }
            else if (manifest.Status == Enumerations.AgentExecutionStatus.Error)
            {
                //remove from execution queue if it has not yet been removed. because test has failed
                if (SchedulesForExecution.Any(x => x.Id == scheduleKey))
                    SchedulesForExecution.Remove(SchedulesForExecution.First(x => x.Id == scheduleKey));

                var record = allSchedule.FirstOrDefault(x => x.Id == scheduleKey);
                HandleError(manifest, record, scheduleKey);
            }
            else if (manifest.Agent.Status == Enumerations.AgentStatus.Warning)
            {
                var record = allSchedule.FirstOrDefault(x => x.Id == scheduleKey);
                HandleWarning(manifest, record, scheduleKey);
            }
            else if (manifest.Status == Enumerations.AgentExecutionStatus.ToRemove)
            {
                agentProcessManifest.TryRemove(scheduleKey, out outManifest);
            }
            else if (manifest.Status == Enumerations.AgentExecutionStatus.Disconnected)
            {
                var record = allSchedule.FirstOrDefault(x => x.Id == scheduleKey);
                HandleError(manifest, record, scheduleKey, Enumerations.TestStatus.Disconnected);
            }
            else if (manifest.Status == Enumerations.AgentExecutionStatus.Started)
            {
                //remove from execution queue if it has not yet been removed. because test has started - it will either end up complete or error now
                if (SchedulesForExecution.Any(x => x.Id == scheduleKey))
                    SchedulesForExecution.Remove(SchedulesForExecution.First(x => x.Id == scheduleKey));

                //if agent become ready once it has started, it mean it has completed
                var record = allSchedule.FirstOrDefault(x => x.Id == scheduleKey);
                if (manifest.Agent.Status == Enumerations.AgentStatus.Ready)
                {
                    ThreadPool.QueueUserWorkItem(delegate
                    {
                        try
                        {
                            //we want to make sure status is really ready, because there is a possible race condition here
                            string reply = AgentUtility.SendCommandToServer(manifest.Agent.Name, "status");
                            if (reply != null && reply.ToLower().StartsWith("ready"))
                            {
                                Enumerations.TestStatus resStatus = GetLastResultStatus(record, manifest.Agent.Name);
                                TestLineupRecord nextItem = GetNextItemInGroup(record);
                                if (resStatus == Enumerations.TestStatus.Failed && manifest.RunNumber < record.NumberOfRuns)
                                {
                                    record.Status = Enumerations.TestStatus.Pending;
                                    agentProcessManifest[scheduleKey].Status = Enumerations.AgentExecutionStatus.Rerun;
                                    //System.Diagnostics.Debug.WriteLine("---end queue rerun " + agentKey);
                                }
                                else if (nextItem != null)
                                {
                                    record.Status = resStatus;
                                    if (PendingRunNowSchedules.Any(x => x.Id == scheduleKey))
                                    {
                                        PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == scheduleKey));
                                    }
                                    RunGroupSuiteInAgents(nextItem, record, scheduleKey);
                                }
                                else
                                {
                                    record.Status = resStatus;
                                    record.AssignedAgentName = string.Empty;
                                    if (PendingRunNowSchedules.Any(x => x.Id == scheduleKey))
                                    {
                                        PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == scheduleKey));
                                    }
                                    manifest.Status = Enumerations.AgentExecutionStatus.ToRemove;
                                    //System.Diagnostics.Debug.WriteLine("---end finished " + agentKey);

                                    //send consolidated rerun report
                                    if (record.SendConsolidatedReport && manifest.RunNumber > 1)
                                    {
                                        SendConsolidateRerunReport(record, manifest.Agent.Name, manifest.RunNumber);
                                    }
                                }
                                mView.UpdateScheduleRecord(record);
                            }
                        }
                        catch (Exception ex)
                        {
                            DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                        }
                    });
                }
                else if (manifest.Agent.Status == Enumerations.AgentStatus.Error)
                {
                    HandleError(manifest, record, scheduleKey);
                }
                else if (manifest.Agent.Status == Enumerations.AgentStatus.Offline)
                {
                    //reconnecting every 30 seconds 
                    if (manifest.Retries % 15 == 0)
                    {
                        ThreadPool.QueueUserWorkItem(delegate
                        {
                            try
                            {
                                //we want to make sure status is really offline, because udp can fail
                                string reply = AgentUtility.SendCommandToServer(manifest.Agent.Name, "status");
                                if (reply == null)
                                {
                                    if (manifest.Retries >= 120)
                                    {
                                        //disconnect the test if the 10th retry is still unable to reconnect
                                        manifest.Status = Enumerations.AgentExecutionStatus.Disconnected;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                            }
                        });
                    }

                    manifest.Retries++;
                }
                else if (manifest.Agent.Status == Enumerations.AgentStatus.Warning)
                {
                    record.Status = Enumerations.TestStatus.Warning;
                    mView.UpdateScheduleRecord(record);
                }
                else
                {
                    manifest.Retries = 0;
                    record.CurrentRun = manifest.RunNumber;
                    record.Status = Enumerations.TestStatus.Running;
                    record.AssignedAgentName = manifest.Agent.Name;
                    mView.UpdateScheduleRecord(record);
                }
            }
        }

        /// <summary>
        /// Queue rerun items
        /// </summary>
        /// <param name="lowPrioAgents"></param>
        private void QueueReruns()
        {
            //look for rerun in manifest, it means it is ready for run, do this first ahead of test queue
            foreach (var manifest in agentProcessManifest.Where(x => x.Value.Status == Enumerations.AgentExecutionStatus.Rerun))
            {
                //break loop if there are no more available agents
                if (mView.GetAvailableAgents().Count() == 0)
                {
                    break;
                }

                if (manifest.Key != null && allSchedule.Any(x => x.Id == manifest.Key))
                {
                    var schedule = allSchedule.First(x => x.Id == manifest.Key);
                    var currentRun = manifest.Value.RunNumber;
                    AgentManifestItem outManifest;

                    //System.Diagnostics.Debug.WriteLine("execute RERUN " + manifest.Key + " agent: " + agent.Name);
                    agentProcessManifest.TryRemove(manifest.Key, out outManifest);
                    TryReserveAgent(outManifest.Agent, schedule, runNumber:currentRun + 1);
                }
            }
        }

        /// <summary>
        /// Queue next in group items
        /// </summary>
        /// <param name="lowPrioAgents"></param>
        private void QueueNextInGroups()
        {
            //look for next item in group, run it ahead of test queue
            foreach (var manifest in agentProcessManifest.Where(x => x.Value.Status == Enumerations.AgentExecutionStatus.NextInGroup))
            {
                //break loop if there are no more available agents
                if (mView.GetAvailableAgents().Count() == 0)
                {
                    break;
                }

                if (manifest.Key != null && allSchedule.Any(x => x.Id == manifest.Key))
                {
                    var schedule = allSchedule.First(x => x.Id == manifest.Key);
                    var nextInGroup = GetNextItemInGroup(schedule);
                    AgentManifestItem outManifest;

                    Agent agent;
                    if (schedule.RunningAgent.AgentType == AgentGroup.AGENT_TYPE_GROUPS)
                        agent = GetNextAvailableAgentInGroup(schedule.RunningAgent.Name, true);
                    else if (schedule.RunningAgent.Name == Agent.ANY_AGENT_NAME)
                        agent = GetNextAvailableAgent(true);
                    else
                        agent = GetAvailableAgent(schedule.RunningAgent.Name, true);

                    if (agent != null)
                    {
                        //System.Diagnostics.Debug.WriteLine("execute RERUN " + manifest.Key + " agent: " + agent.Name);
                        agentProcessManifest.TryRemove(manifest.Key, out outManifest);
                        TryReserveAgent(agent, nextInGroup);
                    }
                }
            }
        }

        /// <summary>
        /// Queue schedules ready for run
        /// </summary>
        /// <param name="schedules"></param>
        /// <param name="lowPrioAgents"></param>
        private void QueueTestSchedules(IEnumerable<TestLineupRecord> schedules)
        {
            if (PendingRunNowSchedules.Where(x => x.RunningAgent.Name != Agent.LOCAL_MACHINE).ToList().Count > 0)
            {
                List<TestLineupRecord> recordsWithPendingRunNow = PendingRunNowSchedules.Where(x => x.RunningAgent.Name != Agent.LOCAL_MACHINE && !agentProcessManifest.Keys.Any(y => y == x.Id)).ToList();
                schedules = recordsWithPendingRunNow;
            }
            if (schedules != null && schedules.Count() > 0)
            {
                //look for next item in group, run it ahead of test queue
                foreach (var schedule in schedules.OrderBy(x => x.RunningAgent.Name == Agent.ANY_AGENT_NAME).ThenBy(x => x.RunningAgent.Name))
                {
                    //break loop if there are no more available agents
                    if (mView.GetAvailableAgents().Count() == 0)
                    {
                        break;
                    }

                    if (schedule != null)
                    {
                        Agent agent;
                        if (schedule.RunningAgent.AgentType == AgentGroup.AGENT_TYPE_GROUPS)
                            agent = GetNextAvailableAgentInGroup(schedule.RunningAgent.Name);
                        else if (schedule.RunningAgent.Name == Agent.ANY_AGENT_NAME)
                            agent = GetNextAvailableAgent();
                        else
                            agent = GetAvailableAgent(schedule.RunningAgent.Name);

                        if (agent != null)
                        {
                            //System.Diagnostics.Debug.WriteLine("execute RUN " + schedule.Id + " agent: " + agent.Name);
                            TryReserveAgent(agent, schedule);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// get next available agent, sort the lowprioagents at bottom of list so ANY will not hog these agents if possible
        /// </summary>
        /// <param name="includeAgentInManifest"></param>
        /// <returns></returns>
        private Agent GetNextAvailableAgent(bool includeAgentInManifest = false)
        {
            var agentsList = mView.GetAvailableAgents();
            foreach (var agent in agentsList.OrderBy(x => lowPrioAgents.Any(y => y == x.Name)))
            {
                if (includeAgentInManifest || !agentProcessManifest.Values.Any(x => x.Agent.Name == agent.Name))
                {
                    return agent;
                }
            }

            return null;
        }

        /// <summary>
        /// Get the next available agent by name, return null if not available
        /// </summary>
        /// <param name="name"></param>
        /// <param name="includeAgentInManifest"></param>
        /// <returns></returns>
        private Agent GetAvailableAgent(string name, bool includeAgentInManifest = false)
        {
            if (includeAgentInManifest || !agentProcessManifest.Values.Any(x => x.Agent.Name == name))
            {
                return mView.GetAvailableAgents().FirstOrDefault(x => x.Name == name);
            }

            return null;
        }

        /// <summary>
        /// get next available agent in group, sort the lowprioagents at bottom of list so ANY will not hog these agents if possible
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="includeAgentInManifest"></param>
        /// <returns></returns>
        private Agent GetNextAvailableAgentInGroup(string groupName, bool includeAgentInManifest = false)
        {
            var agentsList = mView.GetAvailableAgentsInGroup(groupName);
            foreach (var agent in agentsList.OrderBy(x => lowPrioAgents.Any(y => y == x.Name)))
            {
                if (includeAgentInManifest || !agentProcessManifest.Values.Any(x => x.Agent.Name == agent.Name))
                {
                    return agent;
                }
            }

            return null;
        }

        /// <summary>
        /// Update the manifest and record on agent failure
        /// </summary>
        private void HandleError(AgentManifestItem manifest, TestLineupRecord record, string scheduleKey, Enumerations.TestStatus errorStatus = Enumerations.TestStatus.Error)
        {
            //remove failed run from manifest for failed communication with agent. But if still has rerun or next in group, do not remove yet
            if (manifest.RunNumber < record.NumberOfRuns)
            {
                record.Status = Enumerations.TestStatus.Pending;
                agentProcessManifest[scheduleKey].Status = Enumerations.AgentExecutionStatus.Rerun;
            }
            else if (GetNextItemInGroup(record) != null)
            {
                TestLineupRecord nextItem = GetNextItemInGroup(record);
                record.Status = errorStatus;
                if (PendingRunNowSchedules.Any(x => x.Id == record.Id))
                {
                    PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == record.Id));
                }
                RunGroupSuiteInAgents(nextItem, record, scheduleKey);
            }
            else
            {
                AgentManifestItem outManifest;
                record.Status = errorStatus;
                if (PendingRunNowSchedules.Any(x => x.Id == record.Id))
                {
                    PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == record.Id));
                }
                record.AssignedAgentName = string.Empty;
                agentProcessManifest.TryRemove(scheduleKey, out outManifest);
            }
            mView.UpdateScheduleRecord(record);
            DlkLogger.LogToFile("Failed trying to communicate with agent.");
        }

        /// <summary>
        /// Update the manifest and record on agent warning
        /// </summary>
        /// <param name="manifest"></param>
        /// <param name="record"></param>
        /// <param name="scheduleKey"></param>
        private void HandleWarning(AgentManifestItem manifest, TestLineupRecord record, string scheduleKey)
        {
            if (manifest.RunNumber < record.NumberOfRuns)
            {
                record.Status = Enumerations.TestStatus.Pending;
                agentProcessManifest[scheduleKey].Status = Enumerations.AgentExecutionStatus.Rerun;
            }
            else
            {
                AgentManifestItem outManifest;
                record.Status = Enumerations.TestStatus.Warning;
                if (PendingRunNowSchedules.Any(x => x.Id == record.Id))
                {
                    PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == record.Id));
                }
                record.AssignedAgentName = string.Empty;
                agentProcessManifest.TryRemove(scheduleKey, out outManifest);
            }

            try
            {
                mView.UpdateScheduleRecord(record);
                var agentStat = AgentUtility.SendCommandToServer(record.AssignedAgentName, "status");
                var message = agentStat.Split('|').ElementAt(0).Split(':').ElementAt(1).Trim();
                DlkLogger.LogToFile(message);
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile("Agent returns a Warning.");
            }
        }

        /// <summary>
        /// Get next grouped item, return null if there are no items left or not in group
        /// </summary>
        private TestLineupRecord GetNextItemInGroup(TestLineupRecord record)
        {
            if (record.IsInGroup)
            {
                var scheduleItem = allSchedule.FirstOrDefault(x=>x.Id == record.Id);
                if (scheduleItem != null)
                {
                    var currentIndex = allSchedule.IndexOf(scheduleItem);
                    if (allSchedule.Count > currentIndex + 1)
                    {
                        var nextItem = allSchedule[currentIndex + 1];
                        if (nextItem.GroupID == scheduleItem.GroupID)
                        {
                            return nextItem;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get first grouped item
        /// </summary>
        private TestLineupRecord GetFirstItemInGroup(TestLineupRecord record)
        {
            if (record.IsInGroup)
            {
                var scheduleItem = allSchedule.FirstOrDefault(x => x.Id == record.Id);
                if (scheduleItem != null)
                {
                    var firstItem = allSchedule.FirstOrDefault(x => x.GroupID == record.GroupID);
                    return firstItem;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the last suite result of a specific suite and agent
        /// </summary>
        /// <param name="testLineupRecord">testlineuprecord result we want to get</param>
        /// <param name="agentName">null if we want to get latest result regardless of agent. if agent name is passed in parameter, get latest result for that machine.</param>
        /// <returns></returns>
        private Enumerations.TestStatus GetLastResultStatus(TestLineupRecord testLineupRecord, string agentName = null)
        {
            var suiteNameWithoutExtension = Path.GetFileNameWithoutExtension(testLineupRecord.TestSuiteToRun.Name);
            var productFolderName = DlkTestRunnerCmdLib.GetProductFolder(testLineupRecord.TestSuiteToRun.Path);
            var suiteResultsDirectory = testLineupRecord.TestSuiteToRun.Path.Substring(0, testLineupRecord.TestSuiteToRun.Path.IndexOf(productFolderName) + productFolderName.Length) + "\\Framework\\SuiteResults\\";
            var mSuiteResultsFolder = suiteResultsDirectory + suiteNameWithoutExtension;
            if (Directory.Exists(mSuiteResultsFolder))
            {
                try
                {
                    var directories = new DirectoryInfo(mSuiteResultsFolder).GetDirectories();
                    if (directories != null && directories.Count() > 0)
                    {
                        var orderedDirectories = directories.OrderByDescending(x => x.CreationTime);
                        foreach (var directory in orderedDirectories)
                        {
                            var summaryPath = mSuiteResultsFolder + @"\" + directory + @"\summary.dat";
                            if (!File.Exists(summaryPath))
                            {
                                throw new Exception("Summary DAT file does not exists. Results cannot be processed.");
                            }
                            var summaryFile = XDocument.Load(summaryPath);
                            var summary = summaryFile.Element("summary");
                            var summaryAgent = summary.Attribute("machinename").Value;
                            var failed = summary.Attribute("failed").Value;

                            if (agentName == null || agentName.ToLower() == summaryAgent.ToLower())
                            {
                                int numFails = 1;
                                if (int.TryParse(failed, out numFails))
                                {
                                    if (numFails == 0)
                                    {
                                        return Enumerations.TestStatus.Passed;
                                    }
                                }
                                return Enumerations.TestStatus.Failed;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DlkLogger.LogToFile("Unexpected error encountered.", ex);
                    return Enumerations.TestStatus.Error;
                }
            }

            return Enumerations.TestStatus.Failed;
        }

        /// <summary>
        /// Prepare the external scripts for sending 
        /// </summary>
        /// <param name="scripts"></param>
        /// <returns></returns>
        private string PrepareExternalScript(List<ExternalScript> scripts)
        {
            StringBuilder combinedScripts = new StringBuilder();
            foreach (ExternalScript script in scripts)
            {
                combinedScripts.Append(string.Format("{0}<{1}<{2}<{3}::", script.Path, script.Arguments, script.StartIn, script.WaitToFinish));
            }
            if (combinedScripts.Length > 0)
                return combinedScripts.Remove(combinedScripts.Length - 2, 2).ToString();
            return string.Empty;
        }

        /// <summary>
        /// Convert schedule object to get the day string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string ConvertDateToDay(object date)
        {
            string day = string.Empty;

            DateTime dateTime;
            if(DateTime.TryParse(date.ToString(), out dateTime))
            {
                day = dateTime.Day.ToString();
            }

            return day;
        }

        /// <summary>
        /// Send consolidated rerun report email
        /// </summary>
        private void SendConsolidateRerunReport(TestLineupRecord testLineupRecord, string agentName, int runNumber)
        {
            try
            {
                var di = new DirectoryInfo(Path.Combine(DlkEnvironment.mDirSuiteResults, Path.GetFileNameWithoutExtension(testLineupRecord.TestSuiteToRun.Name)));
                var recentDatFiles = (di.GetFiles("*.dat", SearchOption.AllDirectories))
                            .Where(x => x.CreationTime >= DateTime.Now.AddDays(-2d))
                            .OrderByDescending(x => x.CreationTime)
                            .Select(x => new Tuple<string, Dictionary<string, string>>(x.FullName, DlkTestSuiteResultsFileHandler.GetSummaryAttributeValues(x.FullName)));
                var reportSummaries = new List<string>();
                //get the summary.dat paths of all reports to be consolidated
                for (int i = runNumber; i > 0; i--)
                {
                    var selectedSummary = recentDatFiles.FirstOrDefault(x =>
                        x.Item2[DlkTestSuiteInfoAttributes.MACHINENAME].Equals(agentName, StringComparison.OrdinalIgnoreCase) &&
                        x.Item2[DlkTestSuiteInfoAttributes.RUNNUMBER].Equals(i.ToString()));
                    reportSummaries.Add(selectedSummary.Item1);
                }

                var emailInfo = CreateEmailInfo(testLineupRecord);
                string emailTool = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AutoMail.exe");
                string recepients = DlkEmailInfo.GetEmailRecepients(emailInfo.mRecepient);
                string htmlBody = string.Empty;
                string arguments = string.Empty;
                htmlBody = DlkTestSuiteResultsHtmlHandler.CreateHTMLConsolidatedEmailBody(reportSummaries);
                arguments = "/e " + emailInfo.mSMTPHost +
                    " /p " + emailInfo.mSMTPPort +
                    " /u " + emailInfo.mSMTPUser +
                    " /w " + emailInfo.mSMTPPass +
                    " /s " + emailInfo.mSubject +
                    " /o " + emailInfo.mSender +
                    " /t " + recepients +
                    " /h \"" + htmlBody + "\"";

                DlkProcess.RunProcess(emailTool, arguments, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, 0);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Setup email info
        /// </summary>
        private DlkEmailInfo CreateEmailInfo(TestLineupRecord testLineupRecord)
        {
            var emailInfo = new DlkEmailInfo();
            var subjectSuffix = @"' Consolidated Rerun Report Notification" + "\"";
            emailInfo.mSubject = "\"[" + testLineupRecord.Product + @"] Test Suite: '" + Path.GetFileName(testLineupRecord.TestSuiteToRun.Path) + subjectSuffix;
            emailInfo.mRecepient = new List<string>();
            if (testLineupRecord.DistributionList != null && testLineupRecord.DistributionList.Count > 0)
            {
                emailInfo.mRecepient.AddRange(testLineupRecord.DistributionList);
            }
            //get SMTP Configuration
            GetSMTPConfig(emailInfo);

            //get sender address
            bool useCustomEmail = false;
            Boolean.TryParse(DlkConfigHandler.GetConfigValue("usecustomsenderadd"), out useCustomEmail);
            if (useCustomEmail)
                emailInfo.mSender = DlkConfigHandler.GetConfigValue("customsenderadd");
            else
                emailInfo.mSender = DlkConfigHandler.GetConfigValue("defaultsenderadd");

            //include default emails if enabled
            bool useDefaultEmail;
            Boolean.TryParse(DlkConfigHandler.GetConfigValue("usedefaultemail"), out useDefaultEmail);
            if (useDefaultEmail)
            {
                string defaultEmails = DlkConfigHandler.GetConfigValue("defaultemail");
                if (!String.IsNullOrWhiteSpace(defaultEmails))
                    emailInfo.mRecepient.AddRange(new List<string>(defaultEmails.Split(';')));
            }

            return emailInfo;
        }

        /// <summary>
        /// Get smtp configurations
        /// </summary>
        private void GetSMTPConfig(DlkEmailInfo info)
        {
            info.mSMTPHost = DlkConfigHandler.GetConfigValue("smtphost");
            int defaultPort = 25;
            Int32.TryParse(DlkConfigHandler.GetConfigValue("smtpport"), out defaultPort);
            info.mSMTPPort = defaultPort;
            info.mSMTPUser = DlkConfigHandler.GetConfigValue("smtpuser");
            info.mSMTPPass = DlkConfigHandler.GetConfigValue("smtppass");
        }

        /// <summary>
        /// Run suites in a group locally
        /// </summary>
        /// <param name="record">Suite to be executed</param>
        /// <param name="prevRecord">Previously executed suite</param>
        private void RunGroupSuiteLocally(TestLineupRecord record, TestLineupRecord prevRecord)
        {
            TestLineupRecord mPrevRecord = prevRecord;
            if (IsForExecution(record, mPrevRecord))
            {
                TryRunProcessOnLocalMachine(record);
            }
            else
            {
                record.Status = Enumerations.TestStatus.Skipped;
                mView.UpdateScheduleRecord(record);
                mPrevRecord = record;
                record = GetNextItemInGroup(record);
                if (record != null)
                {
                    RunGroupSuiteLocally(record, mPrevRecord);
                }
            }
        }

        /// <summary>
        /// Run suites in a group through agents
        /// </summary>
        /// <param name="record">Suite to be executed</param>
        /// <param name="prevRecord">Previously executed suite</param>
        private void RunGroupSuiteInAgents(TestLineupRecord record, TestLineupRecord prevRecord, string scheduleKey)
        {
            TestLineupRecord mPrevRecord = prevRecord;
            if (IsForExecution(record, mPrevRecord))
            {
                record.Status = Enumerations.TestStatus.Pending;
                agentProcessManifest[scheduleKey].Status = Enumerations.AgentExecutionStatus.NextInGroup;
            }
            else
            {
                record.Status = Enumerations.TestStatus.Skipped;
                if (PendingRunNowSchedules.Any(x => x.Id == record.Id))
                {
                    PendingRunNowSchedules.Remove(PendingRunNowSchedules.First(x => x.Id == record.Id));
                }
                mView.UpdateScheduleRecord(record);
                mPrevRecord = record;
                record = GetNextItemInGroup(record);
                if (record != null)
                {
                    RunGroupSuiteInAgents(record, mPrevRecord, scheduleKey);
                }
                else
                {
                    agentProcessManifest.TryRemove(scheduleKey, out AgentManifestItem outManifest);
                }
            }
        }
    }

    public class AgentManifestItem
    {
        public Agent Agent;
        public Enumerations.AgentExecutionStatus Status;
        public int RunNumber;
        public int Retries;
        public bool IsCancelling;
        public bool Cancelled;
    }

    public class LocalMachineManifestItem
    {
        public TestLineupRecord Schedule;
        public Thread Process;
        public bool IsCancelling = false;

        public LocalMachineManifestItem(TestLineupRecord schedule, Thread process)
        {
            Schedule = schedule;
            Process = process;
        }
    }
}
