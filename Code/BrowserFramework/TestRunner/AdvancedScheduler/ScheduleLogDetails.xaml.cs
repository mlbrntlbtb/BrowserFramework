using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.AdvancedScheduler.Presenter;
using TestRunner.AdvancedScheduler.View;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System.ComponentModel;
using TestRunner.Common;
using CommonLib.DlkSystem;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for ScheduleLogDetails.xaml
    /// </summary>
    public partial class ScheduleLogDetails : Window, IScheduleLogDetailsView, INotifyPropertyChanged
    {
        #region PRIVATE MEMBERS
        private string mScheduleLogsPath;
        private List<DlkScheduleLogsDetailsRecord> mScheduleLogsDetailsRecord;
        private string mPassed;
        private string mFailed;
        private string mError;
        private string mCancelled;
        private string mWarning;
        private string mPending;
        private string mDisconnected;
        private string mTotal;
        private string mNoLogsFound;
        ScheduleLogDetailsPresenter mPresenter;
        private List<string> mDisplayLogItems;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="scheduleLogsPath">Path of target suite</param>
        public ScheduleLogDetails(string scheduleLogsPath)
        {
            InitializeComponent();
            ScheduleLogsPath = scheduleLogsPath;
            mPresenter = new ScheduleLogDetailsPresenter(this);
        }
        #endregion

        #region PUBLIC MEMBERS
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Path of the schedule logs details
        /// </summary>
        public string ScheduleLogsPath
        {
            get
            {
                return mScheduleLogsPath;
            }
            set
            {
                mScheduleLogsPath = value;
                OnPropertyChanged("ScheduleLogsPath");
            }
        }

        /// <summary>
        /// List of schedule logs
        /// </summary>
        public List<DlkScheduleLogsDetailsRecord> ScheduleLogsRecord
        {
            get
            {
                return mScheduleLogsDetailsRecord;
            }
            set
            {
                mScheduleLogsDetailsRecord = value;
                LogsQueue.ItemsSource = mScheduleLogsDetailsRecord;
                LogsQueue.Items.Refresh();
            }
        }

        /// <summary>
        /// Count of logs with passed status
        /// </summary>
        public string Passed
        {
            get
            {
                return mPassed;
            }
            set
            {
                mPassed = value;
                OnPropertyChanged("Passed");
            }
        }

        /// <summary>
        /// Count of logs with failed status
        /// </summary>
        public string Failed
        {
            get
            {
                return mFailed;
            }
            set
            {
                mFailed = value;
                OnPropertyChanged("Failed");
            }
        }

        /// <summary>
        /// Count of logs with error status
        /// </summary>
        public string Error
        {
            get
            {
                return mError;
            }
            set
            {
                mError = value;
                OnPropertyChanged("Error");
            }
        }

        /// <summary>
        /// Count of logs with warning status
        /// </summary>
        public string Warning
        {
            get
            {
                return mWarning;
            }
            set
            {
                mWarning = value;
                OnPropertyChanged("Warning");
            }
        }

        /// <summary>
        /// Count of logs with pending status
        /// </summary>
        public string Pending
        {
            get
            {
                return mPending;
            }
            set
            {
                mPending = value;
                OnPropertyChanged("Pending");
            }
        }

        /// <summary>
        /// Count of logs with cancelled status
        /// </summary>
        public string Cancelled
        {
            get
            {
                return mCancelled;
            }
            set
            {
                mCancelled = value;
                OnPropertyChanged("Cancelled");
            }
        }

        /// <summary>
        /// Count of logs with failed status
        /// </summary>
        public string Disconnected
        {
            get
            {
                return mDisconnected;
            }
            set
            {
                mDisconnected = value;
                OnPropertyChanged("Disconnected");
            }
        }

        /// <summary>
        /// Total count of displayed logs
        /// </summary>
        public string Total
        {
            get
            {
                return mTotal;
            }
            set
            {
                mTotal = value;
                OnPropertyChanged("Total");
            }
        }

        /// <summary>
        /// Boolean for no logs found
        /// </summary>
        public string NoLogsFound
        {
            get
            {
                return mNoLogsFound;
            }
            set
            {
                mNoLogsFound = value;
                OnPropertyChanged("NoLogsFound");
            }
        }

        /// <summary>
        /// List of display log options
        /// </summary>
        public List<string> DisplayLogItems
        {
            get
            {
                return mDisplayLogItems;
            }
            set
            {
                mDisplayLogItems = value;
            }
        }
        #endregion

        #region PRIVATE FUNCTIONS

        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name">Name of property</param>
        private void OnPropertyChanged(string Name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(Name));
            }
        }

        /// <summary>
        /// Populates display log filter items
        /// </summary>
        private void PopulateDisplayLogItems()
        {
            DisplayLogItems = new List<string>(new string[] { "Today", "Within 3 days", "This week" });
            cmbFilter.ItemsSource = DisplayLogItems;
            cmbFilter.Items.Refresh();
        }

        /// <summary>
        /// Populates datagrid based on selected display filter
        /// </summary>
        private void PopulateDisplayLog(string displayOption)
        {
            mPresenter.FilterDisplay(displayOption);
            LogsQueue.Items.Refresh();
        }
        #endregion

        #region EVENT HANDLERS

        /// <summary>
        /// Window loaded event handler
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PopulateDisplayLogItems();
                cmbFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Filter dropdown selection changed handler
        /// </summary>
        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PopulateDisplayLog(cmbFilter.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
