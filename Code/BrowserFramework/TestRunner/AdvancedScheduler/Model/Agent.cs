using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Windows.Data;
using CommonLib.DlkHandlers;
using CommonLib.DlkUtility;
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler.Model
{
    public class Agent : INotifyPropertyChanged, IAgentList
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private const string ANY_AGENT_ID = "";
        public const string ANY_AGENT_NAME = "ANY";
        public const string LOCAL_MACHINE = "Local Machine";
        public const string AGENT_TYPE_LOCAL = "Local";
        public const string AGENT_TYPE_NETWORK = "Network";

        public bool IsInGroup { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string AgentType { get; set; }
        public List<TestLineupRecord> QueuedSchedules { get; set; }
        private AgentSystemStatistics mAgentStats;
        private Enumerations.AgentStatus mStatus = Enumerations.AgentStatus.Ready;
        private string mDescription = string.Empty;
        private string mRuntime;
        private bool mDisabled = false;
        private bool mIsReserved = false;
        private string mGetLatestProductFolderName = string.Empty;

        public Agent(string name, string type)
        {
            Id = name;
            Name = name;
            Status = Enumerations.AgentStatus.Ready;
            AgentStats = new AgentSystemStatistics();
            AgentType = type;
            Disabled = false;
        }

        public bool Disabled
        {
            get { return mDisabled; }
            set
            {
                mDisabled = value;
                Status = value ? Enumerations.AgentStatus.Disabled : Enumerations.AgentStatus.Offline;
                NotifyPropertyChanged("Disabled");
            }
        }

        public bool IsReserved
        {
            get { return mIsReserved; }
            set
            {
                mIsReserved = value;

                if (mIsReserved)
                    Status = Enumerations.AgentStatus.Reserved;
            }
        }

        public bool GettingLatest
        {
            get;
            set;
        }

        public string GetLatestFolderName
        {
            get { return mGetLatestProductFolderName; }
            set
            {
                mGetLatestProductFolderName = value;

                if (!string.IsNullOrEmpty(value))
                {
                    Status = Enumerations.AgentStatus.Updating;
                }
            }
        }

        public AgentSystemStatistics AgentStats
        {
            get
            {
                return mAgentStats;
            }
            set
            {
                mAgentStats = value;
                NotifyPropertyChanged("AgentStats");
            }
        }
        
        public Enumerations.AgentStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                NotifyPropertyChanged("Status");
            }
        }

        public string Description
        {
            get
            {
                return mDescription;
            }
            set
            {
                mDescription = value;
                NotifyPropertyChanged("Description");
            }
        }
       
        public string Runtime
        {
            get
            {
                return mRuntime;
            }
            set
            {
                mRuntime = value;
                NotifyPropertyChanged("Runtime");
            }
        }

        public ObservableCollection<ExecutionHistory> ResultsHistory
        {
            get
            {
                ObservableCollection<ExecutionHistory> ret = new ObservableCollection<ExecutionHistory>();
                DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirSuiteResults);
                foreach (FileInfo fi in new List<FileInfo>(di.GetFiles("*.dat", SearchOption.AllDirectories)).FindAll(x => x.CreationTime >= DateTime.Now.AddDays(-30d)))
                {
                    Dictionary<string, string> attribs = DlkTestSuiteResultsFileHandler.GetSummaryAttributeValues(fi.FullName);
                    if (String.Equals(attribs[DlkTestSuiteInfoAttributes.MACHINENAME], this.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        string suitePath = attribs[DlkTestSuiteInfoAttributes.PATH];
                        /* for backward compatibility. Old summary.dat do not have 'path' attrib. Just use name to display */
                        if (string.IsNullOrEmpty(suitePath))
                        {
                            suitePath = attribs[DlkTestSuiteInfoAttributes.NAME] + ".xml";
                        }
                        ret.Add(AgentUtility.GenerateExecutionHistoryFromSuiteInstance(suitePath, fi.FullName, attribs, this));
                    }
                }
                ret.Sort(x => x.OrderByDescending(item => DateTime.ParseExact(item.ExecutionDate, "MM/dd/yyyy", CultureInfo.InvariantCulture))
                    .ThenByDescending(item => DateTime.ParseExact(item.StartTime, "hh:mm:ss.ff tt", CultureInfo.InvariantCulture)));
                return ret;
            }
        }
    }

    public class AgentSystemStatistics
    {
        public string SystemMemory { get; set; }
        public double AvailableDiskSpace { get; set; }
        public string OperatingSystem { get; set; }
        public string PeakMemoryUsage { get; set; }

        string Cpu { get; set; }
        string DiskSpace { get; set; }
        string MemoryUsage { get; set; }
        string CpuUsage { get; set; }
    }

    public static class AgentUtility
    {
        public static string SendCommandToServer(string server, string command)
        {
            try
            {
                UdpClient socket = new UdpClient(server, 2056);
                socket.Client.ReceiveTimeout = 6000;
                byte[] sdata = Encoding.ASCII.GetBytes(command);
                socket.Send(sdata, sdata.Length);

                //receive
                IPEndPoint ep = null;
                byte[] receive = socket.Receive(ref ep);
                return Encoding.ASCII.GetString(receive);
            }
            catch(SocketException)//for offline servers.
            {
                return null;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_SCHEDULER_UNEXPECTED_ERROR + " | Agent Name: " + server + " | " + ex.InnerException + " | ", ex);
                return null;
            }
        }

        /// <summary>
        /// Generates a suite result/ExecutionHistory
        /// </summary>
        /// <param name="mSuiteName">Name of suite</param>
        /// <param name="mSuiteResultsFolder">Full path of all results folder</param>
        /// <param name="instance">Target results folder</param>
        /// <param name="Owner">Owner object of history</param>
        /// <returns>Output ExecutionHistory object</returns>
        public static ExecutionHistory GenerateExecutionHistoryFromSuiteInstance(String SuitePath, String SummaryFileFullPath, Dictionary<string, string> SummaryInfo, object Owner)
        {
            ExecutionHistory historyEntry = null;
            var executionDate = DateTime.ParseExact(Path.GetFileName(Path.GetDirectoryName(SummaryFileFullPath)).Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var duration = TimeSpan.Parse(SummaryInfo[DlkTestSuiteInfoAttributes.ELAPSED]);
            historyEntry = new ExecutionHistory()
            {
                SuiteFullPath = TransformSuitePathToLocal(SuitePath),
                ResultsFolderFullPath = Path.GetDirectoryName(SummaryFileFullPath),
                ExecutionDate = executionDate.Date.ToString("MM/dd/yyyy"),
                Duration = duration.ToString("hh\\:mm\\:ss\\.ff"),
                StartTime = executionDate.Subtract(duration).ToString("hh:mm:ss.ff tt"), // JP: Is this start time reliable?
                EndTime = executionDate.ToString("hh:mm:ss.ff tt"),
                TotalTests = SummaryInfo[DlkTestSuiteInfoAttributes.TOTAL],
                PassedTests = SummaryInfo[DlkTestSuiteInfoAttributes.PASSED],
                FailedTests = SummaryInfo[DlkTestSuiteInfoAttributes.FAILED],
                NotRunTests = SummaryInfo[DlkTestSuiteInfoAttributes.NOTRUN],
                Id = Guid.NewGuid().ToString(), // update id, random guid for now
                RunningAgent = SummaryInfo[DlkTestSuiteInfoAttributes.MACHINENAME],
                OperatingSystem = SummaryInfo[DlkTestSuiteInfoAttributes.OPERATINGSYSTEM],
                UserName = SummaryInfo[DlkTestSuiteInfoAttributes.USERNAME],
                RunNumber = SummaryInfo[DlkTestSuiteInfoAttributes.RUNNUMBER],
                Parent = Owner
            };

            return historyEntry;
        }

        private static string TransformSuitePathToLocal(string SuitePath)
        {
            string ret = SuitePath;
            if (!File.Exists(ret) && Path.IsPathRooted(ret))
            {
                ret = DlkEnvironment.mDirTestSuite + ret.Substring(ret.IndexOf("Suites\\") + 7);
            }
            return ret;
        }
    }

    public interface IAgentList
    {
        string AgentType { get; set; }
        string Name { get; set; }
    }
}
