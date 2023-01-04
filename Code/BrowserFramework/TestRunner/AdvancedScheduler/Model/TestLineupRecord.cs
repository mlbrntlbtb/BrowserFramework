using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.IO;
using CommonLib.DlkHandlers;
using System.Xml.Linq;
using System.Windows;
using CommonLib.DlkSystem;
using System.Windows.Media;
using CommonLib.DlkRecords;
using System.Globalization;

namespace TestRunner.AdvancedScheduler.Model
{
    public class TestLineupRecord : INotifyPropertyChanged
    {
        public const string DEFAULT_BROWSER = "-use default-";
        public const string DEFAULT_ENVIRONMENT = "-use default-";

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private bool mIsModified = false;
        private bool mIsBlacklisted = false;
        private Color mRowColor = Color.FromArgb(255, 255, 255, 255);
        private Enumerations.TestStatus mStatus = Enumerations.TestStatus.Ready;
        private string mAssignedAgentName = string.Empty;
        private string mNonDefaultBlacklistedEnvironment = string.Empty;
        private IAgentList mRunningAgent;
        private string mEnvironment;
        private DlkBrowser mBrowser;
        private DateTime mStartTime;
        private Enumerations.RecurrenceType mRecurrence = Enumerations.RecurrenceType.Once;
        private object mSchedule = DateTime.Now;
        private bool mEnabled = true;
        private string mExecuteDependencySuiteRow = string.Empty;
        private string mDependent;
        private bool mExecute = true;
        private string mExecuteDependencyResult;
        private List<ExternalScript> mPostExecScripts = new List<ExternalScript>();
        private List<ExternalScript> mPreExecScripts = new List<ExternalScript>();
        private string mGroupImage = string.Empty;
        private string mExecuteDependencyString = "";
        private bool mIsEnabled = true;
        private ExecutionHistory mLastRunResult;
        private string mPrevAgentName;
        private bool mIsDifferentProduct;
        private string mProduct;
        public string Id { get; set; }
        public TestSuite TestSuiteToRun { get; set; }

        public string PrevAgentName
        {
            get => mPrevAgentName;
            set
            {
                mPrevAgentName = value;
                NotifyPropertyChanged("PrevAgentName");
            }
        }

        public bool IsDifferentProduct
        {
            get 
            {
                mIsDifferentProduct = DlkEnvironment.mCurrentProductName != Product;
                return mIsDifferentProduct;
            }
            set
            {
                mIsDifferentProduct = value;
                NotifyPropertyChanged("IsDifferentProduct");
            }
        }

        public string ProductName
        {
            get 
            {
                return Product;
            }
            set
            {
                Product = value;
                NotifyPropertyChanged("ProductName");
            }
        }

        public bool isModified
        {
            get
            {
                return mIsModified;
            }
            set
            {
                mIsModified = value;
            }
        }

        public bool IsBlacklisted
        {
            get
            {
                return mIsBlacklisted;
            }
            set
            {
                mIsBlacklisted = value;
                NotifyPropertyChanged("IsBlacklisted");
            }
        }

        public Color RowColor
        {
            get { return mRowColor; }
            set 
            { 
                mRowColor = value;
                NotifyPropertyChanged("RowColor");
            }
        }
        public Enumerations.TestStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
                NotifyPropertyChanged("Status");
                NotifyPropertyChanged("StatusText");
            }
        }
        public string StatusText
        {
            get
            {
                string statusText = Enumerations.ConvertToString(mStatus);
                if (mStatus == Enumerations.TestStatus.Running && NumberOfRuns > 1)
                {
                    statusText += string.Format(" ({0}/{1})", CurrentRun, NumberOfRuns);
                }
                return statusText;
            }
        }
        public string AssignedAgentName
        {
            get
            {
                if (Status == Enumerations.TestStatus.Running)
                {
                    if (RunningAgentName == Agent.ANY_AGENT_NAME ||
                        (RunningAgent != null && RunningAgent.AgentType == AgentGroup.AGENT_TYPE_GROUPS))
                    {
                        return mAssignedAgentName;
                    }
                }

                return RunningAgentName;
            }
            set
            {
                //only ANY and agent groups have assigned agents we want to display to users
                if (mAssignedAgentName != value)
                {
                    if (value == Agent.ANY_AGENT_NAME)
                        PrevAgentName = mAssignedAgentName;

                    if (RunningAgentName == Agent.ANY_AGENT_NAME ||
                        (RunningAgent != null && RunningAgent.AgentType == AgentGroup.AGENT_TYPE_GROUPS))
                    {
                        mAssignedAgentName = value;
                        NotifyPropertyChanged("AssignedAgentName");
                    }
                }
            }
        }
        public string RunningAgentName
        {
            get
            {
                if (mRunningAgent != null)
                {
                    return mRunningAgent.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public IAgentList RunningAgent
        {
            get
            {
                return mRunningAgent;
            }
            set
            {
                if (value is Agent)
                    mRunningAgent = value as Agent;                    
                else if (value is AgentGroup)
                    mRunningAgent = value as AgentGroup;

                NotifyPropertyChanged("RunningAgent");
                NotifyPropertyChanged("AssignedAgentName");
            }
        }
        public string Environment
        {
            get
            {
                return mEnvironment;
            }
            set
            {
                mEnvironment = value;
                NotifyPropertyChanged("Environment");
            }
        }

        public string NonDefaultBlacklistedEnvironment
        {
            get
            {
                return mNonDefaultBlacklistedEnvironment;
            }
            set
            {
                mNonDefaultBlacklistedEnvironment = value;
                NotifyPropertyChanged("NonDefaultBlacklistedEnvironment");
            }
        }
        public DlkBrowser Browser
        {
            get
            {
                return mBrowser;
            }
            set
            {
                if (mBrowser != null && value != null &&
                    mBrowser.Name != value.Name)
                {
                    mIsModified = true;
                }
                mBrowser = value;
                NotifyPropertyChanged("Browser");
            }
        }

        public DateTime StartTime
        {
            get
            {
                return mStartTime;
            }
            set
            {
                mStartTime = value;
                NotifyPropertyChanged("StartTime");
            }
        }
        public Enumerations.RecurrenceType Recurrence
        {
            get
            {
                return mRecurrence;
            }
            set
            {
                mRecurrence = value;
                NotifyPropertyChanged("Recurrence");

                DateTime myDate = DateTime.Now;
                DateTime.TryParse(mSchedule.ToString(), out myDate);

                if (mRecurrence == Enumerations.RecurrenceType.Weekly)
                {
                    Schedule = Enumerations.ConvertToString(DateTime.Now.DayOfWeek);
                }
                else
                {
                    if (myDate.Year == 1)
                    {
                        myDate = DateTime.Now;
                    }
                    Schedule = myDate.ToShortDateString();
                }
            }
        }
        public object Schedule
        {
            get
            {
                return mSchedule;
            }
            set
            {
                mSchedule = value;
                NotifyPropertyChanged("Schedule");
            }
        }
        public int NumberOfRuns { get; set; }
        public int CurrentRun { get; set; }
        public bool Enabled
        {
            get
            {
                return mEnabled;
            }
            set
            {
                mEnabled = value;
                NotifyPropertyChanged("Enabled");
            }
        }
        public String Dependent
        {
            get
            {
                return mDependent;
            }
            set
            {
                mDependent = value;
                NotifyPropertyChanged("Dependent");
            }
        }

        public bool Execute
        {
            get
            {
                return mExecute;
            }
            set
            {
                mExecute = value;
                if (Dependent == "True")
                {
                    Enabled = mExecute;
                }
                NotifyPropertyChanged("Execute");
                NotifyPropertyChanged("Enabled");
            }
        }

        public string ExecuteDependencyResult
        {
            get
            {
                return mExecuteDependencyResult;
            }
            set
            {
                mExecuteDependencyResult = value;
                NotifyPropertyChanged("ExecuteDependencyResult");
            }
        }

        public String ExecuteDependencySuiteRow
        {
            get
            {
                return mExecuteDependencySuiteRow;
            }
            set
            {
                mExecuteDependencySuiteRow = value;
                NotifyPropertyChanged("ExecuteDependencySuiteRow");
                ExecuteDependencyString = mExecuteDependencySuiteRow == "preceding" ? "↑" : "1";
            }
        }

        public String ExecuteDependencyString
        {
            get
            {
                
                return mExecuteDependencyString;
            }
            set
            {
                mExecuteDependencyString = value;
                NotifyPropertyChanged("ExecuteDependencyString");
            }
        }

        public List<string> DistributionList { get; set; }
        public bool EmailOnStart { get; set; }
        public List<ExternalScript> PostExecutionScripts
        {
            get { return mPostExecScripts; }
            set
            {
                mPostExecScripts = value;
            }
        }
        public List<ExternalScript> PreExecutionScripts
        {
            get { return mPreExecScripts; }
            set
            {
                mPreExecScripts = value;
            }
        }
        public string Product
        {
            get
            {
                //Backward compatibility
                HandleProdName(ref mProduct);

                string _mProduct = mProduct;
                switch (mProduct)
                {
                    case "Costpoint" when TestSuiteToRun.Path.Contains("Costpoint_701\\Suites"):
                        _mProduct += " - " + "7.0.1";
                        break;
                    case "Costpoint" when TestSuiteToRun.Path.Contains("Costpoint_711\\Suites"):
                        _mProduct += " - " + "7.1.1";
                        break;
                    case "Maconomy iAccess" when TestSuiteToRun.Path.Contains("MaconomyiAccess_13\\Suites"):
                        _mProduct += " - " + "1.3";
                        break;
                    case "Maconomy iAccess" when TestSuiteToRun.Path.Contains("MaconomyiAccess_20\\Suites"):
                        _mProduct += " - " + "2.0";
                        break;
                    case "Maconomy iAccess" when TestSuiteToRun.Path.Contains("MaconomyiAccess_23\\Suites"):
                        _mProduct += " - " + "2.3";
                        break;
                    default:
                        break;
                }
                return _mProduct;
            }
            set
            {
                mProduct = value;
            }
        }
        public string Library { get; set; }
        public string GroupImage
        {
            get
            {
                return mGroupImage;
            }
            set
            {
                mGroupImage = value;
                NotifyPropertyChanged("GroupImage");
                NotifyPropertyChanged("IsEnabledAndNotGroupMember");
                NotifyPropertyChanged("IsEnvironmentEnabled");
            }
        }
        public string GroupID { get; set; }
        public bool IsInGroup
        {
            get
            {
                return !string.IsNullOrEmpty(GroupID);
            }
        }
        public bool IsEnabledAndNotGroupMember
        {
            get
            {
                bool isNotGroupMember = (!IsInGroup) ||
                                        (IsInGroup && GroupID == Id);

                return mIsEnabled && isNotGroupMember;
            }
        }

        public bool IsEnvironmentEnabled
        {
            get
            {
                return !IsDifferentProduct;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return mIsEnabled;
            }
            set
            {
                mIsEnabled = value;
                NotifyPropertyChanged("IsEnabled");
                NotifyPropertyChanged("IsEnabledAndNotGroupMember");
                NotifyPropertyChanged("IsEnvironmentEnabled");
            }
        }
        public ExecutionHistory LastRunResult
        {
            get
            {
                return mLastRunResult;
            }
            set
            {
                mLastRunResult = value;
                NotifyPropertyChanged("LastRunResult");
            }
        }

        public bool RerunFailedTestsOnly { get; set; }
        public bool SendConsolidatedReport { get; set; }

        private static void HandleProdName(ref string prodname)
        {
            if (int.TryParse(prodname, out int pname))
            {
                string _prodname = prodname;
                var foundProdname = DlkTestRunnerSettingsHandler.ApplicationList.FirstOrDefault(a => a.ID == _prodname);
                if (foundProdname == null) prodname = "Undefined";
                prodname = foundProdname.DisplayName;
            }
        }
    }

    public static class DlkAdvancedSchedulerFileHandler
    {
        public static string SchedulesFilePath = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "schedules.xml");
        public static string SchedulesFileGhost = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "schedulesghost.xml");

        public static void SetSchedulesFilePath(string filePath)
        {
            SchedulesFilePath = filePath;
            SchedulesFileGhost = filePath;
        }

        public static List<TestLineupRecord> GetSchedulesLineup()
        {
            List<TestLineupRecord> scheduleList = new List<TestLineupRecord>();
            if (File.Exists(SchedulesFilePath))
            {
                var loginConfigHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords;

                XDocument xSchedule = XDocument.Load(SchedulesFilePath);
                var data = from doc in xSchedule.Descendants("record")
                           where File.Exists(doc.Attribute("testsuite").Value)
                           select new TestLineupRecord
                           {
                               Id = doc.Attribute("id") == null ? Guid.NewGuid().ToString() : doc.Attribute("id").Value.ToString(),
                               TestSuiteToRun = new TestSuite
                               {
                                   Name = Path.GetFileName(doc.Attribute("testsuite").Value),
                                   Path = doc.Attribute("testsuite").Value,
                                   Description = DlkTestSuiteXmlHandler.GetTestSuiteInfo(doc.Attribute("testsuite").Value).Description
                               },
                               Recurrence = (Enumerations.RecurrenceType)Enum.Parse(typeof(Enumerations.RecurrenceType), doc.Attribute("recurrence").Value),
                               Schedule = GetSchedule(doc.Attribute("schedule").Value),
                               RunningAgent = new Agent(doc.Attribute("agent").Value, doc.Attribute("agenttype") != null ? doc.Attribute("agenttype").Value : ""),
                               Environment = doc.Attribute("environment") != null && loginConfigHandler.Any(x => x.mID == doc.Attribute("environment").Value) ? 
                                                doc.Attribute("environment").Value : 
                                                TestLineupRecord.DEFAULT_ENVIRONMENT,
                               Browser = new DlkBrowser(doc.Attribute("browser") != null && (DlkEnvironment.mAvailableBrowsers.Any(x => x.Alias == doc.Attribute("browser").Value) |
                                                DlkRemoteBrowserHandler.mRemoteBrowsers.Any(x => x.Id == doc.Attribute("browser").Value) | DlkMobileHandler.mMobileRec.Any(x => x.MobileId == doc.Attribute("browser").Value)) ?
                                                doc.Attribute("browser").Value :
                                                TestLineupRecord.DEFAULT_BROWSER),
                               StartTime = DateTime.Parse(doc.Attribute("starttime").Value),
                               Status = (Enumerations.TestStatus)Enum.Parse(typeof(Enumerations.TestStatus), doc.Attribute("status").Value),
                               DistributionList = new List<string>(doc.Attribute("email").Value.Split(';')),
                               EmailOnStart = bool.Parse(doc.Attribute("emailonstart").Value),
                               NumberOfRuns = int.Parse(doc.Attribute("numberofruns").Value),
                               Enabled = bool.Parse(doc.Attribute("enabled").Value),
                               Execute = doc.Attribute("execute") != null ? bool.Parse(doc.Attribute("execute").Value) : true,
                               Dependent = doc.Attribute("dependent") != null ? doc.Attribute("dependent").Value : string.Empty,
                               ExecuteDependencyResult = doc.Attribute("executedependencyresult") != null ? doc.Attribute("executedependencyresult").Value : String.Empty,
                               ExecuteDependencySuiteRow = doc.Attribute("executedependencysuiterow") != null ? doc.Attribute("executedependencysuiterow").Value : String.Empty,
                               Library = doc.Attribute("library").Value,
                               Product = doc.Attribute("product").Value,
                               PreExecutionScripts = ConvertXmlScript(doc.Element("preexec")),
                               PostExecutionScripts = ConvertXmlScript(doc.Element("postexec")),
                               GroupID = doc.Attribute("groupid").Value,
                               RerunFailedTestsOnly = doc.Attribute("rerunfailedonly") == null ? false : bool.Parse(doc.Attribute("rerunfailedonly").Value),
                               SendConsolidatedReport = doc.Attribute("consolidatedreport") == null ? false : bool.Parse(doc.Attribute("consolidatedreport").Value),
                               RowColor = doc.Attribute("rowcolor") == null ? Color.FromArgb(255, 255, 255, 255) : ConvertStringToColor(doc.Attribute("rowcolor").Value)                  
                           };

                scheduleList = data.ToList();
            }

            return scheduleList;
        }

        public static Color ConvertStringToColor(string rowColor)
        {
            if (String.IsNullOrEmpty(rowColor))
            {
                //set white as default color
                rowColor = "#FFFFFFFF";
            }

            Color argbVal = Color.FromArgb(255, 255, 255, 255);
            var converter = new BrushConverter();
            Brush br = (Brush)converter.ConvertFromString(rowColor);
            if (br != null)
            {
                byte a = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).A;
                byte g = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).G;
                byte r = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).R;
                byte b = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).B;

                argbVal = Color.FromArgb(a, r, g, b);
            }
            return argbVal;
        }

        public static XDocument GetSchedulerFileDocument(IEnumerable<TestLineupRecord> testLineup)
        {
            var scheds = new List<XElement>();
            foreach (var tlr in testLineup)
            {
                var sched = new XElement("record",
                    new XAttribute("id", tlr.Id),
                    new XAttribute("testsuite", tlr.TestSuiteToRun.Path),
                    new XAttribute("schedule", tlr.Schedule.ToString()),
                    new XAttribute("recurrence", Enumerations.ConvertToString(tlr.Recurrence)),
                    new XAttribute("agent", tlr.RunningAgent.Name),
                    new XAttribute("agenttype", tlr.RunningAgent.AgentType),
                    new XAttribute("environment", tlr.Environment),
                    new XAttribute("browser", tlr.Browser.Name),
                    new XAttribute("starttime", tlr.StartTime.ToShortTimeString()),
                    new XAttribute("status", Enumerations.ConvertToString(Enumerations.TestStatus.Ready)),
                    new XAttribute("email", ConvertEmailListToLongString(tlr.DistributionList)),
                    new XAttribute("emailonstart", tlr.EmailOnStart.ToString()),
                    new XAttribute("numberofruns", tlr.NumberOfRuns.ToString()),
                    new XAttribute("enabled", tlr.Enabled.ToString()),
                    new XAttribute("execute", tlr.Execute.ToString()),
                    new XAttribute("dependent", String.IsNullOrEmpty(tlr.Dependent) ?  "false" : tlr.Dependent.ToString()),
                    new XAttribute("executedependencyresult", tlr.ExecuteDependencyResult != null ? tlr.ExecuteDependencyResult.ToString() : String.Empty),
                    new XAttribute("executedependencysuiterow", tlr.ExecuteDependencySuiteRow != null ? tlr.ExecuteDependencySuiteRow.ToString() : String.Empty),
                    new XAttribute("library", tlr.Library),
                    new XAttribute("product", tlr.Product),
                    new XAttribute("groupid", tlr.GroupID),
                    new XAttribute("rerunfailedonly", tlr.RerunFailedTestsOnly.ToString()),
                    new XAttribute("consolidatedreport", tlr.SendConsolidatedReport.ToString()),
                    new XAttribute("rowcolor", tlr.RowColor),
                    new XElement("preexec", ConvertXmlScript(tlr.PreExecutionScripts)),
                    new XElement("postexec", ConvertXmlScript(tlr.PostExecutionScripts)) );
                scheds.Add(sched);
            }
            XElement root = new XElement("testschedules", scheds);

            return new XDocument(root);
        }

        private static string GetSchedule(string Input)
        {
            string ret = string.Empty;
            DateTime dt;
            DayOfWeek dow;

            if (DateTime.TryParse(Input, out dt))
            {
                ret = dt.ToShortDateString();
            }
            else if (Enum.TryParse(Input, out dow))
            {
                ret = Enumerations.ConvertToString(dow);
            }
            return ret;
        }

        private static string ConvertEmailListToLongString(List<string> Input)
        {
            string ret = string.Empty;
            ret = string.Join(";", Input);
            return ret;
        }

        private static List<ExternalScript> ConvertXmlScript(XElement xmlElement)
        {
            return xmlElement.Elements().Select(x => new ExternalScript
            {
                Path = x.Attribute("path").Value,
                Type = (Enumerations.ExetrnalScriptType)Enum.Parse(typeof(Enumerations.ExetrnalScriptType), x.Attribute("type").Value),
                Order = int.Parse(x.Attribute("order").Value),
                StartIn = x.Attribute("startin").Value,
                Arguments = x.Attribute("args").Value,
                WaitToFinish = bool.Parse(x.Attribute("waitFinish").Value)
            }).ToList();
        }

        private static List<XElement> ConvertXmlScript(List<ExternalScript> externalScript)
        {
            return externalScript.Select(x => new XElement("script",
                    new XAttribute("path", x.Path),
                    new XAttribute("type", Enumerations.ConvertToString(x.Type)),
                    new XAttribute("order", x.Order.ToString()),
                    new XAttribute("startin", x.StartIn),
                    new XAttribute("args", x.Arguments),
                    new XAttribute("waitFinish", x.WaitToFinish)
            )).ToList();
        }
    }

    public class ScheduleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ret = values.First() == null ? string.Empty : values.First().ToString();

            //if (DlkString.IsDayOfWeek(ret))
            //{
            
            //}

            DateTime myDate = DateTime.Now;
            bool parsed = DateTime.TryParse(ret, out myDate);
            if (myDate.Year == 1)
            {
                myDate = DateTime.Now;
            }
            {
                switch ((Enumerations.RecurrenceType)values.Last())
                {
                    case Enumerations.RecurrenceType.Monthly:
                        ret = "Day " + myDate.Day.ToString();
                        break;
                    case Enumerations.RecurrenceType.Weekly:
                        ret = parsed ? myDate.DayOfWeek.ToString() : values.First().ToString();
                        break;
                    case Enumerations.RecurrenceType.Daily:
                    case Enumerations.RecurrenceType.Weekdays:
                        ret = string.Empty;
                        break;
                    default:
                        ret = myDate.ToShortDateString();
                        break;
                }
            }
            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class StartTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string st = value.ToString();

            return DateTime.Parse(st).ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string time = value.ToString();
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 19, 30, 0);
        }
    }

    public class ItalicTextConverter : IValueConverter  //Set font style to italic and color to silver
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontStyle style = FontStyles.Normal;
            if (value.ToString() == "MIXED")
                style = FontStyles.Italic;
            return style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontStyle style = FontStyles.Normal;
            return style;
        }
    }

    public class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "";
            if (value.ToString() == "MIXED")
            color = "Silver";
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class NullImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value.ToString() == string.Empty)
                return DependencyProperty.UnsetValue;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class BlacklistVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ret = false;
            Boolean.TryParse(value.ToString(), out ret);
            return (ret ? Visibility.Visible : Visibility.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class BlacklistTooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string ret = value.ToString();
            return (ret == "" ? "Environment is blacklisted" : "Suite default contains blacklisted environment " + ret);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class DifferentProductTooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return $"Suite belongs to {value.ToString()} product.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class DifferentProductConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isDifferentProduct = System.Convert.ToBoolean(value);

            return isDifferentProduct ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
