using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.AdvancedScheduler.Model
{
    public class ExecutionHistory : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            // so we can have a local copy(not a reference) of PropertyChangedEventHandler in a multi-threaded environment in case PropertyChanged is null
            // see: http://stackoverflow.com/questions/4489709/why-use-this-construct-propertychangedeventhandler-handler-this-propertychan 
            // and http://stackoverflow.com/questions/672638/use-of-null-check-in-event-handler
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String mId;
        private String mExecutionDate;
        private String mRunningAgent;
        private String mTotalTests;
        private String mPassedTests;
        private String mFailedTests;
        private String mNotRunTests;
        private String mStartTime;
        private String mEndTime;
        private String mDuration;
        private String mPassRate;
        private String mCompletionRate;
        private String mEnvironment;
        private String mBrowser;
        private String mOperatingSystem;
        private String mUserName;
        private String mRunNumber;
        private String mSuiteFullPath;

        public String Id
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
                NotifyPropertyChanged("Id");
            }
        }
        public String ExecutionDate
        {
            get { return mExecutionDate; }
            set 
            {
                mExecutionDate = value;
                NotifyPropertyChanged("ExecutionDate");
            }
        } 
        public String RunningAgent 
        {
            get { return mRunningAgent; }
            set 
            {
                mRunningAgent = value;
                NotifyPropertyChanged("RunningAgent");
            }
        }

        public String TotalTests
        {
            get
            {
                return mTotalTests;
            }
            set
            {
                mTotalTests = value;
                NotifyPropertyChanged("TotalTests");
            }
        }

        public String Environment
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

        public String Browser
        {
            get
            {
                return mBrowser;
            }
            set
            {
                mBrowser = value;
                NotifyPropertyChanged("Browser");
            }
        }

        public String PassedTests
        {
            get
            {
                return mPassedTests;
            }
            set
            {
                mPassedTests = value;
                NotifyPropertyChanged("PassedTests");
            }
        }

        public String FailedTests
        {
            get
            {
                return mFailedTests;
            }
            set
            {
                mFailedTests = value;
                NotifyPropertyChanged("FailedTests");
            }
        }

        public String NotRunTests
        {
            get
            {
                return mNotRunTests;
            }
            set
            {
                mNotRunTests = value;
                NotifyPropertyChanged("NotRunTests");
            }
        }

        public String StartTime
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

        public String EndTime
        {
            get
            {
                return mEndTime;
            }
            set
            {
                mEndTime = value;
                NotifyPropertyChanged("EndTime");
            }
        }
        public String Duration
        {
            get
            {
                return mDuration;
            }
            set
            {
                mDuration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Pass Rate of suite
        /// </summary>
        public string PassRate
        {
            get
            {
                int dPassed, dTotal, dNotRun;

                if (!int.TryParse(mPassedTests, out dPassed) || !int.TryParse(mTotalTests, out dTotal) || !int.TryParse(mNotRunTests, out dNotRun))
                {
                    return string.Empty;
                }
                mPassRate = (dNotRun==dTotal) ? "0%" : string.Format("{0:F0}", (dPassed * 100 / (dTotal - dNotRun))) + "%";
                return mPassRate;
            }
        }

        /// <summary>
        /// Completion Rate of suite
        /// </summary>
        public string CompletionRate
        {
            get
            {
                int dPassed, dFailed, dTotal;

                if (!int.TryParse(mPassedTests, out dPassed) || !int.TryParse(mFailedTests, out dFailed) || !int.TryParse(mTotalTests, out dTotal))
                {
                    return string.Empty;
                }
                mCompletionRate = string.Format("{0:F0}", ((dPassed + dFailed) * 100 / dTotal)) + "%";
                return mCompletionRate;
            }
        }

        /// <summary>
        /// Full file path of test suite
        /// </summary>
        public string SuiteFullPath
        {
            get
            {
                /* TEMP Only: Backward compatibility: some summary.dat do not have 'path' attrib, 'name' attrib was substituted */
                if (!Path.IsPathRooted(mSuiteFullPath)) // If not rooted, 'name' was used as 'path'
                {
                    mSuiteFullPath = CommonLib.DlkHandlers.DlkTestSuiteXmlHandler.GetTestSuitePathFromName(mSuiteFullPath);
                }
                return mSuiteFullPath;
            }
            set
            {
                mSuiteFullPath = value;
            }
        }

        /// <summary>
        /// Full path of results folder
        /// </summary>
        public string ResultsFolderFullPath { get; set; }

        /// <summary>
        /// Operating System of running agent
        /// </summary>
        public string OperatingSystem
        {
            get
            {
                return mOperatingSystem;
            }
            set
            {
                mOperatingSystem = value;
                NotifyPropertyChanged("OperatingSystem");
            }
        }

        /// <summary>
        /// User name used in running agent
        /// </summary>
        public string UserName
        {
            get
            {
                return mUserName;
            }
            set
            {
                mUserName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        /// <summary>
        /// Run (re-run) number of suite run
        /// </summary>
        public string RunNumber
        {
            get
            {
                return mRunNumber;
            }
            set
            {
                mRunNumber = value;
                NotifyPropertyChanged("RunNumber");
            }
        }

        /// <summary>
        /// Owner record of History (Could be Agent or TestLineupRecord)
        /// </summary>
        public object Parent { get; set; }
    }

    /// <summary>
    /// Converter to determine brush of Pass Rate meter/s
    /// </summary>
    public class PassRateFillConverter : IMultiValueConverter
    {
        /// <summary>
        /// Return object based from passed multiple params
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string sRate = values.First() == System.Windows.DependencyProperty.UnsetValue ? "-1" : values.First().ToString().Replace("%", "");
            string sPercentile = values.Last().ToString();
            int rate = int.Parse(sRate);

            if (rate == 100)
            {
                return Brushes.LawnGreen;
            }
            else if (rate == 0)
            {
                return Brushes.Crimson;
            }
            else if (rate == -1)
            {
                return Brushes.White;
            }

            switch(sPercentile)
            {
                case "1":
                    return Brushes.Crimson;
                case "2":
                    if (rate > 10)
                    {
                        return Brushes.IndianRed;
                    }
                    break;
                case "3":
                    if (rate > 20)
                    {
                        return Brushes.DarkSalmon;
                    }
                    break;
                case "4":
                    if (rate > 30)
                    {
                        return Brushes.Orange;
                    }
                    break;
                case "5":
                    if (rate > 40)
                    {
                        return Brushes.Gold;
                    }
                    break;
                case "6":
                    if (rate > 50)
                    {
                        return Brushes.Yellow;
                    }
                    break;
                case "7":
                    if (rate > 60)
                    {
                        return Brushes.GreenYellow;
                    }
                    break;
                case "8":
                    if (rate > 70)
                    {
                        return Brushes.Chartreuse;
                    }
                    break;
                case "9":
                    if (rate > 80)
                    {
                        return Brushes.Lime;
                    }
                    break;
                case "10":
                    if (rate > 90)
                    {
                        return Brushes.LawnGreen;
                    }
                    break;
            }
            return Brushes.White;
        }

        /// <summary>
        /// Return to orig object (unimplemented)
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Converter to get suite File name from Full Path
    /// </summary>
    public class TestSuitePathConverter : IValueConverter
    {
        /// <summary>
        /// Return object based from passed param
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null ? System.IO.Path.GetFileName(value.ToString()) : string.Empty;
        }

        /// <summary>
        /// Return to orig object (unimplemented)
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
