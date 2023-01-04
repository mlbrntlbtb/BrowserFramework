using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestRunner.AdvancedScheduler.View;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;


namespace TestRunner.AdvancedScheduler.Presenter
{
    public class ScheduleLogDetailsPresenter
    {
        #region PRIVATE MEMBERS
        private IScheduleLogDetailsView mView;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="view">Required view instance to use this presenter class</param>
        public ScheduleLogDetailsPresenter(IScheduleLogDetailsView view)
        {
            mView = view;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Load schedule logs details
        /// </summary> 
        public void Load()
        {
            string schedulerLogsPath = mView.ScheduleLogsPath;

            if (!File.Exists(schedulerLogsPath))
            {
                return;
            }

            try
            {
                List<DlkScheduleLogsDetailsRecord> scheduleLogs = new DlkScheduleLogsDetailsHandler(schedulerLogsPath).mScheduleLogsRecords.ToList<DlkScheduleLogsDetailsRecord>();
                mView.ScheduleLogsRecord = scheduleLogs.OrderByDescending(s => Convert.ToDateTime(s.mDateTime)).Where(s => s.mTestStatus != "Running").ToList();
            }
            catch
            {
                throw; // re-throw to View
            }
        }

        /// <summary>
        /// Filter schedule logs details
        /// </summary>
        public void FilterDisplay(string displayOption)
        {
            Load();
            if(mView.ScheduleLogsRecord != null)
            {
                List<DlkScheduleLogsDetailsRecord> scheduleLogs = new List<DlkScheduleLogsDetailsRecord>();
                switch (displayOption.ToLower())
                {
                    case "today":
                        foreach (DlkScheduleLogsDetailsRecord sched in mView.ScheduleLogsRecord)
                        {
                            string currentSchedDate = Convert.ToDateTime(sched.mDateTime).ToString("MM/dd/yyyy");
                            string filterDate = DateTime.Now.ToString("MM/dd/yyyy");
                            if (currentSchedDate.Equals(filterDate))
                            {
                                scheduleLogs.Add(sched);
                            }
                        }
                        break;
                    case "within 3 days":
                        foreach (DlkScheduleLogsDetailsRecord sched in mView.ScheduleLogsRecord)
                        {
                            DateTime currentSchedDate = Convert.ToDateTime(Convert.ToDateTime(sched.mDateTime).ToString("MM/dd/yyyy"));
                            DateTime minFilterDate = Convert.ToDateTime(DateTime.Now.AddDays(-3).ToString("MM/dd/yyyy"));
                            DateTime maxFilterDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                            if (currentSchedDate >= minFilterDate && currentSchedDate <= maxFilterDate)
                            {
                                scheduleLogs.Add(sched);
                            }
                        }
                        break;
                    case "this week":
                        foreach (DlkScheduleLogsDetailsRecord sched in mView.ScheduleLogsRecord)
                        {
                            DateTime currentSchedDate = Convert.ToDateTime(Convert.ToDateTime(sched.mDateTime).ToString("MM/dd/yyyy"));
                            DateTime minFilterDate = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy"));
                            DateTime maxFilterDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                            if (currentSchedDate >= minFilterDate && currentSchedDate <= maxFilterDate)
                            {
                                scheduleLogs.Add(sched);
                            }
                        }
                        break;
                    default:
                        break;
                }
                mView.ScheduleLogsRecord = scheduleLogs;
                CountLogsStatus(scheduleLogs);
            }
        }

        /// <summary>
        /// Count schedule logs details by status
        /// </summary>
        public void CountLogsStatus(List<DlkScheduleLogsDetailsRecord> scheduleLogs)
        {
            int passedLogs = 0;
            int failedLogs = 0;
            int errorLogs = 0;
            int warningLogs = 0;
            int cancelledLogs = 0;
            int pendingLogs = 0;
            int disconnectedLogs = 0;

            foreach (DlkScheduleLogsDetailsRecord sched in scheduleLogs)
            {
                string schedStatus = sched.mTestStatus.ToLower();
                switch (schedStatus)
                {
                    case "passed":
                        passedLogs++;
                        break;
                    case "failed":
                        failedLogs++;
                        break;
                    case "error":
                        errorLogs++;
                        break;
                    case "warning":
                        warningLogs++;
                        break;
                    case "cancelled":
                        cancelledLogs++;
                        break;
                    case "pending":
                        pendingLogs++;
                        break;
                    case "disconnected":
                        disconnectedLogs++;
                        break;
                    default:
                        break;
                }
            }

            mView.Passed = passedLogs.ToString();
            mView.Failed = failedLogs.ToString();
            mView.Error = errorLogs.ToString();
            mView.Warning = warningLogs.ToString();
            mView.Cancelled = cancelledLogs.ToString();
            mView.Pending = pendingLogs.ToString();
            mView.Disconnected = disconnectedLogs.ToString();
            mView.Total = scheduleLogs.Count.ToString();
            mView.NoLogsFound = scheduleLogs.Count == 0 ? "True" : "False";
        }

        #endregion
    }
}
