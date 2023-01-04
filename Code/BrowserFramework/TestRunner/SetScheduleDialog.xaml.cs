using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddScheduleDialog.xaml
    /// </summary>
    public partial class SetScheduleDialog : Window
    {
        //private ObservableCollection<DlkScheduleRecord> _SchedRecs;
        private List<DlkScheduleRecord> _SchedRecs;
        private List<string> mSuite;
        private bool mIsEdit;
        private String mWeekday;
        private String mTSuite;
        private int mOrder = 0;
        private bool mRunStatus = true;
        private string mExistingSched;
        public List<DlkScheduleRecord> mSchedRecs;
        private String mTestFilter = "";
        public DlkScheduleRecord mRec;

        private DayOfWeek weekday {
            get
            {
                return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), tbWeekday.Text);
            }
        }

        private List<KwDirItem> _lstSuiteDirectories;
        public List<KwDirItem> lstSuiteDirectories
        {
            get
            {
                if (_lstSuiteDirectories == null)
                {
                    _lstSuiteDirectories = new List<KwDirItem>();
                    List<KwDirItem> filteredContainer = new List<KwDirItem>();
                    _lstSuiteDirectories = new DlkTestSuiteLoader().GetSuiteDirectories2(TestRoot, mTestFilter,
                        string.IsNullOrEmpty(mTestFilter) ? null : filteredContainer);
                    _lstSuiteDirectories = string.IsNullOrEmpty(mTestFilter) ? _lstSuiteDirectories : filteredContainer;
                }
                return _lstSuiteDirectories;
                
            }
        }

        //private List<KwDirItem> _lstKeywordDirectories;
        //public List<KwDirItem> lstKeywordDirectories
        //{
        //    get
        //    {
        //        if (_lstKeywordDirectories == null)
        //        {
        //            _lstKeywordDirectories = new List<KwDirItem>();
        //            List<KwDirItem> filteredContainer = new List<KwDirItem>();
        //            _lstKeywordDirectories = new DlkKeywordTestsLoader().GetKeywordDirectories(TestRoot, mTestFilter,
        //                string.IsNullOrEmpty(mTestFilter) ? null : filteredContainer);
        //            _lstKeywordDirectories = string.IsNullOrEmpty(mTestFilter) ? _lstKeywordDirectories : filteredContainer;

        //        }
        //        return _lstKeywordDirectories;
        //    }
        //}

        private String _TestRoot;
        public String TestRoot
        {
            get
            {
                if (_TestRoot == null)
                {
                    _TestRoot = DlkEnvironment.mDirTestSuite;
                }
                return _TestRoot;
            }
            set
            {
                _TestRoot = value;
            }
        }

        //public SetScheduleDialog(List<DlkScheduleRecord> SchedRecs, String tWeekday, String ScheduleFilePath)
        //{
        //    InitializeComponent();
        //    this._SchedRecs = SchedRecs;
        //    _tWeekday = tWeekday;
        //    LoadData();

        //    isEdit = false;
        //}

        public SetScheduleDialog(List<DlkScheduleRecord> SchedRecs, String tWeekday, String tSuite, String existingSched,int order, bool tisEdit, bool tRunStatus = true)
        {
            InitializeComponent();
            this._SchedRecs = SchedRecs;
            mWeekday = tWeekday;
            mTSuite = tSuite;
            mOrder = order;
            mRunStatus = tRunStatus;
            mExistingSched = (DateTime.Parse(existingSched)).ToString("HH:mm");
            mIsEdit = tisEdit;
            LoadData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
            if (mRunStatus)
            {
                rdoTrue.IsChecked = true;
            }
            else
            {
                rdoFalse.IsChecked = true;
            }
            if (mIsEdit && mOrder == 1)
            {
                tbWeekday.Text = mWeekday;
                txtSuite.Text = mTSuite;
                tvSuiteDirectory.IsEnabled = false;
                if (DlkString.IsValidTime(mExistingSched))
                {
                    SetSchedToggle(false);
                    DateTime dt = DlkString.GetTimeIn12Hour(mExistingSched);
                    scheduleTime.Hours = int.Parse(dt.ToString("hh"));
                    scheduleTime.Minutes = dt.Minute;
                    scheduleTime.AMPM = dt.ToString("tt");
                }else{
                    SetSchedToggle(true);
                    txtQueue.Text = System.IO.Path.GetFileName(mExistingSched).ToString();
                }
                RunSuite.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbWeekday.Text = mWeekday;
                txtQueue.Text = mTSuite;
                txtSuite.Text = "";

                if (mOrder == 1)
                {
                    SetSchedToggle(false);
                    RunSuite.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (mIsEdit)
                        txtSuite.Text = _SchedRecs[_SchedRecs.IndexOf(_SchedRecs.Find(x => (x.Order == mOrder & x.Day == weekday)))].TestSuiteDisplay;
                    SetSchedToggle(true);
                    RunSuite.Visibility = Visibility.Visible;
                }
            }
            tvSuiteDirectory.DataContext = lstSuiteDirectories;
            RefreshData();
            //RefreshQueueData();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LoadData()
        {
            mSuite= new List<string>();
            mSuite.Clear();
            foreach(DlkScheduleRecord dsr in _SchedRecs){
                mSuite.Add(dsr.TestSuiteDisplay);
            }
            //mQueue = new List<string>();
            //mQueue.Clear();
            //foreach (DlkScheduleRecord dsr in _SchedRecs)
            //{
            //    mQueue.Add(dsr.msuitedisplay);
            //}
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (SetSchedule())
            {
                this.mSchedRecs = _SchedRecs;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_SET_SCHEDULE_FAILED, "Set schedule failed");
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            this.DialogResult = false;
            this.Close();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool SetSchedule()
        {
            DateTime timeSched = new DateTime();

            try
            {
                string search = txtSuite.Text;

                if (mOrder == 1)
                {
                    timeSched = scheduleTime.GetTimeAsDateTime();
                }
                
                /* if edited, find target and commit values in cache */
                if (mIsEdit)
                {
                    if (mOrder == 1)
                    {
                        _SchedRecs[_SchedRecs.IndexOf(_SchedRecs.Find(x => (x.TestSuiteDisplay == search & x.Day == weekday)))].Time = timeSched;
                    }
                    else
                    {
                        int testIndex = _SchedRecs.IndexOf(_SchedRecs.Find(x => (x.Order == mOrder & x.Day == weekday)));
                        if ((BFFile)tvSuiteDirectory.SelectedItem != null) // to avoid exception if only RunStatus is to be edited
                        {
                            _SchedRecs[testIndex].TestSuiteRelativePath = ((BFFile)tvSuiteDirectory.SelectedItem).Path.Substring(((BFFile)tvSuiteDirectory.SelectedItem).Path.IndexOf("BrowserFramework\\Products\\") + 26);
                        }
                        _SchedRecs[testIndex].RunStatus = (bool)rdoTrue.IsChecked ? true : false;
                    }
                }

                else /* Add new schedule */
                {
                    //add new record otherwise - no more checks for same suite names
                    List<DlkExternalScript> lsEscr = new List<DlkExternalScript>();
                    DlkScheduleRecord mNewRec = new DlkScheduleRecord(weekday, timeSched, mOrder, "", DlkEnvironment.mLibrary,
                        DlkTestRunnerSettingsHandler.ApplicationUnderTest.Name, (bool)rdoTrue.IsChecked ? true : false, false, ((BFFile)tvSuiteDirectory.SelectedItem).Path, lsEscr);
                    mRec = mNewRec;
                    _SchedRecs.Add(mNewRec);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void SetSchedToggle(bool timeExists)
        {
            if (timeExists)
            {
                QueueSched.Visibility = Visibility.Visible;
                TimeSched.Visibility = Visibility.Collapsed;
            }
            else
            {
                TimeSched.Visibility = Visibility.Visible;
                QueueSched.Visibility = Visibility.Collapsed;
            }
        }

        private void txtTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
            foreach (char c in e.Text)
            {
                    if (!Char.IsDigit(c))
                {
                    if (c.Equals(':'))
                    {
                        break;
                    }
                    e.Handled = true;
                    break;
                }
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        protected string DisplayAs24HrTime(object timeValue)
        {
            if (!Convert.IsDBNull(timeValue))
            {
                try
                {
                    String ts = timeValue.ToString();
                    DateTime dt = Convert.ToDateTime(ts);
                    return dt.ToString("H:mm");
                }
                catch
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.ERR_INCORRECT_TIME_FORMAT);
                    return "";
                }
                
            }
            else
            {
                return "00:00";
            }
        }

//temp remove
        //private void txtTime_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        txtTime.Text = DisplayAs24HrTime(txtTime.Text);
        //    }
        //    catch
        //    {
        //        txtTime.Text = null;
        //    }
        //}

        private void RefreshData()
        {
            LoadData();

            if (!scheduleTime.IsVisible)
            {
                if (!String.IsNullOrEmpty(txtQueue.Text))
                {
                    mSuite.RemoveAt(mSuite.IndexOf(txtQueue.Text));
                }
            }
        }
        private bool CheckIfTimeFirst()
        {
            if (!_SchedRecs.Any(x => (x.Day == weekday & x.Order == 1)))
            {
                return true;
            }
            return false;
        }

        private bool CheckIfTimeExists()
        {
            if (!_SchedRecs.Any(x => (x.Day == weekday & x.Order == 1)))
            {
                return true;
            }
            return false;
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
            mTestFilter = txtSearch.Text;
            RefreshTestTree();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSearchTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (grdSearchGrid.Visibility == System.Windows.Visibility.Collapsed)
            {
                grdSearchGrid.Visibility = System.Windows.Visibility.Visible;
                btnSearchTest.BorderBrush = Brushes.Navy;
                txtSearch.Focus();
            }
            else
            {
                txtSearch.Clear();
                grdSearchGrid.Visibility = System.Windows.Visibility.Collapsed;
                BrushConverter bc = new BrushConverter();
                btnSearchTest.BorderBrush = (Brush)bc.ConvertFrom("#FF828790");
                mTestFilter = "";
                RefreshTestTree();
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        
        private void RefreshTestTree()
        {
            _lstSuiteDirectories = null;
           tvSuiteDirectory.DataContext = lstSuiteDirectories;
        }

        private void tvSuiteDirectory_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                object selectedItem = tvSuiteDirectory.SelectedItem;
                txtSuite.Text = ((BFFile)tvSuiteDirectory.SelectedItem).Name;
            }
            catch
            {
                // do nothing
            }
        }
    }
}
