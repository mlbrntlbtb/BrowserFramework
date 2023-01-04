using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;

namespace CommonLib.DlkSystem
{
    public static class DlkTestRunnerApi
    {
        #region PRIVATE MEMBERS
        private const string PATH_TEMP_EXEC_LOG_FILE = @"C:\trc\current.log";

        private static ThreadStart mThreadJob;
        private static Thread mTestRunThread;
        private static String mTestPath = "";
        private static DlkTestSuiteInfoRecord mSuiteInfo = null;
        private static bool mIsSuite = true;
        private static string mBrowser = "Firefox";
        private static int mInstance = 1;
        private static string mEnvironment = "default";
        private static string mRelativePath = "";
        private static String mConsoleOutput = "";
        private static List<DlkExecutionQueueRecord> mTestRecords = null;
        private static BackgroundWorker bwExecute;
        private static DlkEmailInfo mEmailInfo = null;
        private static DlkEmailInfo mNotificationInfo = null;
        private static string mRunningTest = "";
        private static DlkTestResultsDbTalker mTestResultsDbTalker;
        private static int mRunningStep = 0;
        private static int mRunNumber = 1;
        private static int mNumberOfRuns = 1;
        private static string mScheduledStart = string.Empty;
        private static bool mRunOnlyFailedTests = false;
        private static List<string> mTestAlreadyPassedOnRerun = new List<string>();
        private static string mSessionID;
        private static bool mIsAbortionPending = false;
        #endregion

        #region PUBLIC MEMBERS
        public static String mExecutionStatus = "idle";
        public static int mTestsRan = 0;
        public static bool mCancellationPending = false;
        public static String mResultsPath = "";
        public static String mConsoleTestScript = "";
        public static bool mExecutionPaused = false;
        public static String mGlobalVarFileName = String.Empty;
        public static bool mIsTestScheduled = false;

        public static String ConsoleOutput
        {
            get
            {
                return mConsoleOutput;
            }
        }

        public static DlkEmailInfo EmailInfo
        {
            get
            {
                return mEmailInfo;
            }
            set
            {
                mEmailInfo = value;
            }
        }

        public static DlkEmailInfo NotificationInfo
        {
            get
            {
                return mNotificationInfo;
            }
            set
            {
                mNotificationInfo = value;
            }
        }

        public static string CurrentRunningTest 
        {
            get
            {
                return mRunningTest;
            }
            set 
            {
                mRunningTest = value;
            }
        }

        public static int CurrentRunningStep
        {
            get
            {
                return mRunningStep;
            }
            set
            {
                mRunningStep = value;
            }
        }

        public static bool mAbortionPending
        {
            get
            {
                return mIsAbortionPending;
            }
            set
            {
                mIsAbortionPending = value;
                /* mCancellationPending automatically mirrors this value */
                mCancellationPending = value;
            }
        }

        public static bool mAbortScheduledRun
        {
            get
            {
                return mIsAbortionPending;
            }
            set
            {
                mIsAbortionPending = value;
                /* mCancellationPending automatically mirrors this value */
                mCancellationPending = value;
            }
        }

        public static bool mTestScheduled
        {
            get
            {
                return mIsTestScheduled;
            }
            set
            {
                mIsTestScheduled = value;
            }
        }
        #endregion

        #region PUBLIC METHODS
        public static void ExecuteTest(List<DlkExecutionQueueRecord> Records, bool IsSuite = true, string Environment = "", string RelativePath = "",
            int Instance = 1, string Browser = "", string SessionId = "")
        {
            mTestPath = "";
            mTestRecords = Records;
            mIsSuite = IsSuite;
            mEnvironment = Environment;
            mRelativePath = RelativePath;
            mInstance = Instance;
            mBrowser = Browser;
            mExecutionStatus = "running";
            mConsoleOutput = "";
            mSessionID = string.IsNullOrEmpty(SessionId) ? Guid.NewGuid().ToString() : SessionId;
            DlkTestExecute.IsPlayBack = false;
            mThreadJob = new ThreadStart(ExecuteTestApi);
            mTestRunThread = new Thread(mThreadJob);
            mTestRunThread.Start();
            //ExecuteTestApi();
        }

        public static void ExecuteTest(String TestFilePath, bool IsSuite=true, string Environment="", string RelativePath="",
            int Instance = 1, string Browser = "", string SessionId = "")
        {
            mTestPath = TestFilePath;
            mIsSuite = IsSuite;
            mEnvironment = Environment;
            mRelativePath = RelativePath;
            mInstance = Instance;
            mBrowser = Browser;
            mExecutionStatus = "running";
            mConsoleOutput = "";
            mSessionID = string.IsNullOrEmpty(SessionId) ? Guid.NewGuid().ToString() : SessionId;
            DlkTestExecute.IsPlayBack = false;
            mGlobalVarFileName = IsSuite && File.Exists(TestFilePath) ? DlkTestSuiteXmlHandler.GetTestSuiteInfo(TestFilePath).GlobalVar : String.Empty;
            mThreadJob = new ThreadStart(ExecuteTestApi);
            mTestRunThread = new Thread(mThreadJob);
            mTestRunThread.Start();
            //ExecuteTestApi();
        }

        public static void ExecuteScheduledTest(String TestFilePath, string ScheduledStart = "", int RunNumber = 1, int NumberOfRuns = 1, bool RunOnlyFailedTests = false,  bool IsSuite = true, string Environment = "",
            string RelativePath = "", int Instance = 1, string Browser = "", string SessionId = "")
        {
            mTestPath = TestFilePath;
            mRunNumber = RunNumber;
            mNumberOfRuns = NumberOfRuns;
            mScheduledStart = ScheduledStart;
            mRunOnlyFailedTests = RunOnlyFailedTests;
            mIsSuite = IsSuite;
            mEnvironment = Environment;
            mRelativePath = RelativePath;
            mInstance = Instance;
            mBrowser = Browser;
            mExecutionStatus = "running";
            mConsoleOutput = "";
            mSessionID = string.IsNullOrEmpty(SessionId) ? Guid.NewGuid().ToString() : SessionId;
            mGlobalVarFileName = File.Exists(TestFilePath) ? DlkTestSuiteXmlHandler.GetTestSuiteInfo(TestFilePath).GlobalVar : String.Empty;
            DlkTestExecute.IsPlayBack = false;
            StartExecution(null, null);
        }

        public static void ExecuteTestPlayback(String TestFilePath, bool IsSuite = false, string Environment = "", string RelativePath = "",
            int Instance = 1, string Browser = "", string SessionId = "")
        {
            mTestPath = TestFilePath;
            mIsSuite = IsSuite;
            mEnvironment = Environment;
            mRelativePath = RelativePath;
            mInstance = Instance;
            mBrowser = Browser;
            mExecutionStatus = "running";
            mConsoleOutput = "";
            mSessionID = string.IsNullOrEmpty(SessionId) ? Guid.NewGuid().ToString() : SessionId;
            mGlobalVarFileName = String.Empty;
            DlkTestExecute.IsPlayBack = true;
            StartExecution(null, new DoWorkEventArgs(true));
        }

        public static void StopCurrentExecution()
        {
            mCancellationPending = true;
        }

        public static void StopExecution()
        {
            mAbortionPending = true;
        }

        public static void SetupOverrideLoginConfig(string id, string url, string username, string password, string database, string pin, string metadata)
        {
            DlkTestExecute.OverrideLoginConfigRecord = new DlkLoginConfigRecord(id, url, username, password, database, pin, string.Empty, metadata);
        }

        public static void SetupOverrideBrowser(string browser)
        {
            DlkTestExecute.OverrideBrowser = browser;
        }

        public static void ResetConfigOverrides()
        {
            DlkTestExecute.OverrideLoginConfigRecord = null;
            DlkTestExecute.OverrideBrowser = null;
        }

        public static void SetupEmailNotification(DlkScheduleRecord Schedule, bool considerDefaultEmail=true)
        {
            EmailInfo = CreateEmailInfo(Schedule, false, considerDefaultEmail);
        }

        public static void SetupPreEmailNotification(DlkScheduleRecord Schedule, bool considerDefaultEmail=true)
        {
            NotificationInfo = CreateEmailInfo(Schedule, true, considerDefaultEmail);
        }

        public static void RunPreScripts(DlkScheduleRecord Record, string rootPath)
        {
            /* Temporary only, need extract this from file */
            if (DlkSourceControlHandler.SourceControlSupported && DlkSourceControlHandler.SourceControlEnabled)
            {
                GetLatestFiles(Record.ProductFolder, rootPath);
                List<DlkExternalScript> mPreScripts = Record.ExternalScripts.Where(x => x.Type == DlkExternalScriptType.PreExecutionScript).OrderBy(x => x.Order).ToList();
                foreach (DlkExternalScript script in mPreScripts)
                {
                    RunProgram(script);
                }
            }
        }

        public static void RunPostScripts(DlkScheduleRecord Record, string rootPath)
        {
            /* Do not remove yet */
            CreateSharedFolder(Record.Product, rootPath);
            List<DlkExternalScript> mPostScripts = Record.ExternalScripts.Where(x => x.Type == DlkExternalScriptType.PostExecutionScript).OrderBy(x => x.Order).ToList();
            foreach (DlkExternalScript script in mPostScripts)
            {
                RunProgram(script);
            }
        }

        public static void RunPostScriptsWithoutShare(DlkScheduleRecord Record, string rootPath)
        {
            List<DlkExternalScript> mPostScripts = Record.ExternalScripts.Where(x => x.Type == DlkExternalScriptType.PostExecutionScript).OrderBy(x => x.Order).ToList();
            foreach (DlkExternalScript script in mPostScripts)
            {
                RunProgram(script);
            }
        }
        #endregion

        #region PRIVATE METHODS
        private static void ExecuteTestApi()
        {
            ////set executing test suite
            //DlkExecuteInfoFileHandler.UpdateTestSuiteFile(mTestPath);

            ////clean old results
            //DlkEnvironment.CleanFrameworkWorkingResults();

            ////setup configuration
            //DlkConfigFileHandler.Initialize();
            //mConsoleTestScript = DlkConfigFileHandler.DebugTest;

            ////setup environment id in global var
            //DlkGlobalHandler.Initialize();


            //get execution configuration from test suite
            if (mTestPath != "")
            {
                mSuiteInfo = DlkTestSuiteXmlHandler.GetTestSuiteInfo(mTestPath);
                //DlkConfigFileHandler.TestBrowser = suiteInfo.Browser;
                //DlkConfigFileHandler.Language = suiteInfo.Language;
                //DlkConfigFileHandler.Save();

                //DlkGlobalHandler.SetGlobalVar("EnvID", suiteInfo.EnvID);
            }

            bwExecute = new BackgroundWorker();
            bwExecute.WorkerReportsProgress = true;
            bwExecute.DoWork += new DoWorkEventHandler(StartExecution);
            bwExecute.ProgressChanged += new ProgressChangedEventHandler(bw_ExecuteProgressChanged);
            bwExecute.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ExecutionCompleted);
            bwExecute.WorkerSupportsCancellation = true;

            bwExecute.RunWorkerAsync();


        }

        private static void StartExecution(object sender, DoWorkEventArgs e)
        {
            DlkEnvironment.CustomInfo = new Dictionary<string, string[]>();
            DlkEnvironment.mDirTestResultsCurrent = null;

            SendEmailNotification();

            if (mIsSuite)
            {
                List<DlkExecutionQueueRecord> input = string.IsNullOrEmpty(mTestPath) ? mTestRecords : DlkTestSuiteXmlHandler.Load(mTestPath);
                List<DlkExecutionQueueRecord> lastResults = new List<DlkExecutionQueueRecord>();
                mTestsRan = 0;

                /* need to check last result if (a) user wants re-run for failed tests only AND (b) this is a re-run */
                if (mRunOnlyFailedTests && mRunNumber > 1 )
                {
                    try
                    {
                        string lastResultsFolder = string.Empty;
                        lastResults = DlkTestSuiteResultsFileHandler.GetLatestSuiteResults(mTestPath, out lastResultsFolder);
                    }
                    catch
                    {
                        /* Do nothing. Ignore if error parsing last results, just execute everything */
                    }
                }
                else
                {
                    /* flush re-run cache */
                    mTestAlreadyPassedOnRerun = new List<string>();
                }

                foreach (DlkExecutionQueueRecord record in input)
                {
                    DlkEnvironment.mCurrentTestIdentifier = record.identifier;
                    CurrentRunningTest = record.testrow;
                    // need to check if file exists
                    string file = Path.Combine(record.folder.Trim('\\'), record.file);

                    /* Check if current tst should be executed */
                    if (!IsExecute(record, input, lastResults))
                    {
                        DlkEnvironment.mCurrentTestIdentifier = "";
                        if (string.IsNullOrEmpty(mTestPath) || File.Exists(Path.Combine(DlkEnvironment.mDirTests, file)))
                        {
                            if (string.IsNullOrEmpty(mResultsPath))
                            {
                                mResultsPath = DlkEnvironment.mDirTestResultsCurrent;
                            }
                            record.teststatus = "not run";
                        }
                        continue;
                    }
                    if (string.IsNullOrEmpty(mTestPath) || File.Exists(Path.Combine(DlkEnvironment.mDirTests, file)))
                    {
                        if (Directory.Exists(Path.GetDirectoryName(PATH_TEMP_EXEC_LOG_FILE)))
                        {
                            WriteCurrentExecutionTempLog(PATH_TEMP_EXEC_LOG_FILE, mTestPath, Path.Combine(DlkEnvironment.mDirTests, file), record.instance, input.Count.ToString(), (input.IndexOf(record) + 1).ToString());
                        }
                        mResultsPath = (string)DlkAssemblyHandler.Invoke(DlkEnvironment.mLibrary, DlkAssemblyHandler.TestMethods.ExecuteTest,
                            false,DlkEnvironment.mProductFolder, record.environment, file, record.instance, record.Browser.Name, record.keepopen);
                        record.teststatus = DlkTestSuiteResultsFileHandler.GetTestResultUsingId(mResultsPath, record.identifier);
                    }
                    DlkEnvironment.mCurrentTestIdentifier = "";
                    if (mAbortionPending || mAbortScheduledRun)
                    {
                        mTestsRan++;
                        break;
                    }
                    mTestsRan++;
                }

                //Save suite results
                if (!string.IsNullOrEmpty(mTestPath) & !mAbortScheduledRun)
                {
                    // transform mResultsFile from testresults dir to the suiteresults dir
                    mResultsPath = DlkTestSuiteResultsFileHandler.SaveSuiteResults(mTestPath, mResultsPath);
                    DlkTestSuiteResultsFileHandler.GetTestSuiteDescription(mTestPath);
                    string environmentOverrideName = DlkTestExecute.OverrideLoginConfigRecord != null ? DlkTestExecute.OverrideLoginConfigRecord.mID : string.Empty;
                    DlkTestSuiteResultsFileHandler.CreateSuiteResultsSummary(mTestPath, mResultsPath, input, mRunNumber, mNumberOfRuns, DlkTestExecute.OverrideBrowser, environmentOverrideName, mScheduledStart);
                    try
                    {
                        if (DlkConfigHandler.mDashboardEnabled)
                        {
                            DlkTestSuiteResultsFileHandler.AddSuiteResultsManifestEntry(mTestPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        DlkLogger.LogToFile("Error accessing suite results manifest file.", ex);
                    }
                    if (Directory.Exists(Path.GetDirectoryName(PATH_TEMP_EXEC_LOG_FILE)))
                    {
                        DeleteCurrentExecutionTempLog(PATH_TEMP_EXEC_LOG_FILE);
                    }
                }
            }
            else // DEBUG mode
            {
                /* Start new session logger. Limit to Ad-hoc runs for now */
                DlkLogger.StartNewSessionLogs(mSessionID);

                CurrentRunningStep = 0;
                DlkTestExecute.IsBrowserOpen = e != null && e.Argument != null ? (bool)e.Argument : false;
                DlkEnvironment.mCurrentTestIdentifier = Guid.NewGuid().ToString();
                mResultsPath = (string)DlkAssemblyHandler.Invoke(DlkEnvironment.mLibrary, DlkAssemblyHandler.TestMethods.ExecuteTest, true,
                    DlkEnvironment.mProductFolder, mEnvironment, mRelativePath, mInstance.ToString(), mBrowser, DlkEnvironment.mKeepBrowserOpen.ToString());
                DlkEnvironment.mCurrentTestIdentifier = "";
            }
            if (sender != null && e != null)
            {
                bwExecute.ReportProgress(100);
            }
            else
            {                
                ExecutionCompleted(null, null);
            }
        }

        /// <summary>
        /// Check if Test should be executed
        /// </summary>
        /// <param name="Test">Target test</param>
        /// <param name="AllTests">All tests in queue</param>
        /// <param name="LastResults">Previous results list</param>
        /// <returns>True if needs to execute, fslse otherwise</returns>
        private static bool IsExecute(DlkExecutionQueueRecord Test, List<DlkExecutionQueueRecord> AllTests, List<DlkExecutionQueueRecord> LastResults)
        {
            bool ret;
            bool bDependent;
            bool bExecute;
            bool.TryParse(Test.execute, out bExecute);

            if (mRunOnlyFailedTests // user only wants to run failed tests
                && mTestAlreadyPassedOnRerun.Contains(Test.identifier) // on rerun 'passed' cache
                || // OR
                (LastResults != null // previous results list is not null
                && LastResults.Any()  // previous results list is not empty
                && LastResults.FindAll(x => x.identifier == Test.identifier).Any() // previous results list contains target test result
                && LastResults.Find(x => x.identifier == Test.identifier).teststatus.ToLower() == "passed")) // target test passed on previous run
            {
                if (!mTestAlreadyPassedOnRerun.Contains(Test.identifier))
                {
                    mTestAlreadyPassedOnRerun.Add(Test.identifier);
                }
                return false;
            }

            /* Execute value depends on result of a preceding test in queue */
            if (bool.TryParse(Test.dependent, out bDependent) && bDependent)
            {
                if (AllTests.FindAll(x => x.testrow == Test.executedependencytestrow).Count() > 0)
                {
                    ret = AllTests.FindAll(x => x.testrow == Test.executedependencytestrow).First().teststatus.ToLower()
                        == Test.executedependencyresult.ToLower() ? bExecute : !bExecute;
                }
                else
                {
                    ret = bExecute;
                }
            }
            else /* not dependent, determine execute value outright */
            {
                ret = bExecute;
            }
            return ret;
        }

        private static void executeScript_OutputDataRecieved(object sender, DataReceivedEventArgs e)
        {
            bwExecute.ReportProgress(0, e.Data);
        }

        private static void bw_ExecuteProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != 100)
            {
                mConsoleOutput = mConsoleOutput + (String)e.UserState + "\n";
            }

        }

        private static void ExecutionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //xml log formatting
            if (mTestPath != "" && EmailInfo != null)
            {
                try
                {
                    // email here
                    string emailTool = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AutoMail.exe");
                    
                    if(!File.Exists(emailTool))
                    {
                        DlkLogger.LogToFile(string.Format("[Email Notification] File '{0}' is missing.", emailTool));
                        return;
                    }
                    
                    string recepients = DlkEmailInfo.GetEmailRecepients(EmailInfo.mRecepient);
                    bool isCancelled = mAbortionPending;
                    string htmlBody = DlkTestSuiteResultsHtmlHandler.CreateHTMLReportBody(Path.Combine(mResultsPath, "summary.dat"), isCancelled);
                    
                    string subject = EmailInfo.mSubject;
                    //insert the run number
                    if (subject.Contains(" Results Notification"))
                    {
                        subject = subject.Insert(subject.IndexOf(" Results Notification"), string.Format(" ({0}/{1})", mRunNumber, mNumberOfRuns));
                    }
                    //insert cancelled if iscancelled
                    if (isCancelled && subject.Contains("\""))
                    {
                        subject = subject.Insert(subject.LastIndexOf("\""), " (Cancelled)");
                    }

                    string arguments = "/e " + EmailInfo.mSMTPHost +
                        " /p " + EmailInfo.mSMTPPort +
                        " /u " + EmailInfo.mSMTPUser +
                        " /w " + EmailInfo.mSMTPPass +
                        " /s " + subject +
                        " /o " + EmailInfo.mSender +
                        " /t " + recepients +
                        " /h \"" + htmlBody + "\"";
                    DlkProcess.RunProcess(emailTool, arguments, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, 0);
                    EmailInfo = null;
                }
                catch(Exception ex)
                {
                    DlkLogger.LogToFile("[Email Notification] Encountered error while sending suite results", ex);
                }
            }

            //if (!String.IsNullOrEmpty(mTestPath)) //save suite results only to Dashboard. mTestPath is empty string if not suite
            //{
            //    Boolean bShowError = false;                 
            //    if (DlkConfigHandler.mResultsDbRecordResults == true & (DlkEnvironment.mProductFolder.ToLower().Contains("costpoint") || DlkEnvironment.mProductFolder.ToLower().Equals("timeandexpense")))
            //    {
            //        /* Prepare database recording */
            //        mTestResultsDbTalker = new DlkTestResultsDbTalker();
            //        List<DlkExecutionQueueRecord> input = string.IsNullOrEmpty(mTestPath) ? mTestRecords : DlkTestSuiteXmlHandler.Load(mTestPath);                    
            //        DlkTestResultsDbTalker.InitializeDatabaseForResults(Path.GetFileName(mTestPath), input.Count, DlkEnvironment.mProductFolder);
            //        if (DlkTestResultsDbTalker.mRunId < 0)
            //        {
            //            DlkLogger.LogToFile("Test Results Database Communication Error");
            //        }
                    
            //         /* Update the test results db */
            //        if (DlkTestResultsDbTalker.mRunId >= 0)
            //        {
            //            UpdateDashboard();

            //            /* Finalize the database results */
            //            Stopwatch mDbUpdateFinished = new Stopwatch();
            //            mDbUpdateFinished.Start();
            //            int mMax = 5 * 60 * 1000; // wait 5 minutes for results to store
            //            while (DlkTestResultsDbTalker.mTestResultsRemainingToReport > 0)
            //            {
            //                if (mDbUpdateFinished.ElapsedMilliseconds > mMax)
            //                {
            //                    bShowError = true;
            //                    DlkTestResultsDbTalker.HaltTestResultStorage();
            //                }
            //                Thread.Sleep(2000);
            //            }
            //            mDbUpdateFinished.Stop();
            //            DlkTestResultsDbTalker.FinalizeDbResults();
            //        }

            //        if (bShowError)
            //        {
            //            DlkLogger.LogToFile("All test results could not be stored in the Test Result Database.");
            //        }
            //    }
            //}
            
            mExecutionStatus = "idle";
        }

        private static DlkEmailInfo CreateEmailInfo(DlkScheduleRecord Schedule, bool isNotificationEmail, bool considerDefaultEmail)
        {
            var emailInfo = new DlkEmailInfo();
            var subjectSuffix = isNotificationEmail ? @"' has started execution in " + Environment.MachineName + "\"" : @"' Results Notification" + "\"";
            emailInfo.mSubject = "\"[" + Schedule.Product + @"] Test Suite: '" + Path.GetFileName(Schedule.TestSuiteRelativePath) + subjectSuffix;
            emailInfo.mRecepient = new List<string>();
            if (!string.IsNullOrEmpty(Schedule.Email))
            {
                emailInfo.mRecepient.AddRange(new List<string>(Schedule.Email.Split(';')));
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
            if (considerDefaultEmail && useDefaultEmail)
            {
                string defaultEmails = DlkConfigHandler.GetConfigValue("defaultemail");
                if (!String.IsNullOrWhiteSpace(defaultEmails))
                    emailInfo.mRecepient.AddRange(new List<string>(defaultEmails.Split(';')));
            }

            return emailInfo;
        }

        private static void GetSMTPConfig(DlkEmailInfo info)
        {
            info.mSMTPHost = DlkConfigHandler.GetConfigValue("smtphost");
            int defaultPort = 25;
            Int32.TryParse(DlkConfigHandler.GetConfigValue("smtpport"), out defaultPort);
            info.mSMTPPort = defaultPort;
            info.mSMTPUser = DlkConfigHandler.GetConfigValue("smtpuser");
            info.mSMTPPass = DlkConfigHandler.GetConfigValue("smtppass");
        }

        private static void SendEmailNotification()
        {
            if (mTestPath != "" && NotificationInfo != null)
            {
                string emailTool = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AutoMail.exe");
                string recepients = DlkEmailInfo.GetEmailRecepients(NotificationInfo.mRecepient);
                string htmlBody = string.Empty;
                string arguments = string.Empty;

                htmlBody = DlkTestSuiteContentsHtmlHandler.CreateHTMLEmailNotificationBody(mTestPath);
                arguments = "/e " + NotificationInfo.mSMTPHost +
                   " /p " + NotificationInfo.mSMTPPort +
                   " /u " + NotificationInfo.mSMTPUser +
                   " /w " + NotificationInfo.mSMTPPass +
                   " /s " + NotificationInfo.mSubject +
                   " /o " + NotificationInfo.mSender +
                   " /t " + recepients +
                   " /h \"" + htmlBody + "\"";
               
                DlkProcess.RunProcess(emailTool, arguments, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, 0);
                NotificationInfo = null;
            }
        }

        private static void UpdateDashboard()
        {
            //Get the test results from the results path
            FileInfo[] resultFiles = new DirectoryInfo(mResultsPath).GetFiles("*.xml");
            foreach (FileInfo file in resultFiles)
            {
                string errormsg = string.Empty;
                string errtype = string.Empty;
                mTestResultsDbTalker = new DlkTestResultsDbTalker();
                DlkTest mTest = new DlkTest(file.FullName);
                String mShortLogFile = @"\" + file.FullName.Replace(DlkEnvironment.mDirSuiteResults, "");
                if (mTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONMSG").Count > 0)
                {
                    errormsg = mTest.mTestSetupLogMessages.FindAll(
                        x => x.mMessageType == "EXCEPTIONMSG").First().mMessageDetails;
                }
                else
                {
                    if (mTest.mTestFailedAtStep > 0)
                    {
                        if (mTest.mTestSteps[mTest.mTestFailedAtStep - 1].mStepLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONMSG").Count > 0)
                        {
                            errormsg = mTest.mTestSteps[mTest.mTestFailedAtStep - 1].mStepLogMessages.FindAll(
                            x => x.mMessageType == "EXCEPTIONMSG").First().mMessageDetails;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(errormsg))
                {
                    if (errormsg.ToLower().Contains("verify"))
                        errtype = "TestStepAssertion";
                    else
                        errtype = "TestStepAction";
                }


                //update results
                mTestResultsDbTalker.UpdateDatabaseResults(
                    file.Name,
                    mTest.mTestPath,
                    Convert.ToInt32(mTest.mTestInstanceExecuted),
                    mTest.mTestStatus,
                    mTest.mTestStart,
                    mTest.mTestEnd,
                    mShortLogFile,
                    mTest.mTestFailedAtStep,
                    errtype,
                    errormsg
                    );
            }
        }

        private static void WriteCurrentExecutionTempLog(string filePath, string suitePath, string currentTestPath, string currentInstance, string testCount, string testOrder)
        {
            if (!string.IsNullOrEmpty(filePath) && Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                XElement test = new XElement("test", currentTestPath);
                XElement suite = new XElement("suite", suitePath);
                XElement testcount = new XElement("testcount", testCount);
                XElement testorder = new XElement("testorder", testOrder);
                XElement inst = new XElement("instance", currentInstance);
                XElement root = new XElement("current", suite, testcount, test, inst, testorder);
                XDocument doc = new XDocument(root);
                doc.Save(filePath);
            }
        }

        private static void DeleteCurrentExecutionTempLog(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private static void GetLatestFiles(string productFolderName, string rootPath)
        {
            try
            {
                Console.Write("Getting latest " + productFolderName + " files...");
                DlkProcess.RunProcess(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SourceControl.bat"),
                    "get /recursive /overwrite " + Path.Combine(rootPath, "Products", productFolderName),
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    false,
                    300);
                Console.WriteLine("DONE");
            }
            catch
            {

            }
        }

        private static void CreateSharedFolder(string productName, string rootPath)
        {
            try
            {
                Console.Write("Creating shared folder for " + productName + " ...");
                DlkProcess.RunProcess(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SharedFolder.bat"),
                    rootPath + "\\Products",
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    false,
                    300);
                Console.WriteLine("DONE");
            }
            catch
            {

            }
        }

        private static void RunProgram(DlkExternalScript script)
        {
            try
            {
                string startDir = script.StartIn;
                if (String.IsNullOrWhiteSpace(startDir))
                {
                    startDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }
                Console.Write("Executing " + script.Name + " ...");
                DlkProcess.RunProcess(script.Path, script.Arguments, startDir, false, script.WaitToFinish);
                Console.WriteLine("DONE");
            }
            catch
            {

            }
        }
        #endregion
    }
}
