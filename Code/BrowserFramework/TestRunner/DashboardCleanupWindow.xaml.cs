using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for DashboardCleanupWindow.xaml
    /// </summary>
    public partial class DashboardCleanupWindow : Window, INotifyPropertyChanged
    {
        private const string FILTER_1DAY = "Dashboard entries older than 1 day";
        private const string FILTER_3DAYS = "Dashboard entries older than 3 days";
        private const string FILTER_7DAYS = "Dashboard entries older than 7 days";
        private const string FILTER_15DAYS = "Dashboard entries older than 15 days";
        private const string FILTER_30DAYS = "Dashboard entries older than 30 days";
        private const string FILTER_90DAYS = "Dashboard entries older than 3 months";
        private const string FILTER_180DAYS = "Dashboard entries older than 6 months";
        private const string FILTER_360DAYS = "Dashboard entries older than 1 year";
        private const string FILTER_EVERYTHING = "All Dashboard entries (This will clear the repository)";


        private string[] mFilterTypes = { FILTER_1DAY, FILTER_3DAYS, FILTER_7DAYS, FILTER_15DAYS, FILTER_30DAYS, 
                                            string.Empty, FILTER_90DAYS, FILTER_180DAYS, FILTER_360DAYS, string.Empty, FILTER_EVERYTHING};

        private string mCurrRecCount = "HUH";
        private string mTotRecCount = "HUH";

        public event PropertyChangedEventHandler PropertyChanged;
        public DashboardCleanupWindow()
        {
            InitializeComponent();
            Initialize();
        }

        public List<ManifestResultsRecordForCleanup> mResults { get; set; }

        public string CurrentRecordCount
        {
            get
            {
                return mCurrRecCount;
            }
            set
            {
                mCurrRecCount = value;
                OnPropertyChanged("CurrentRecordCount");
            }
        }

        public string TotalRecordCount
        {
            get
            {
                return mTotRecCount;
            }
            set
            {
                mTotRecCount = value;
                OnPropertyChanged("TotalRecordCount");

            }
        }

        private void Initialize()
        {
            /* Get all manifest records */
            LoadResults();

            /* Load Defaults */
            LoadDefaults();
        }

        private void LoadDefaults()
        {
            /* Load data bindings */
            cmbFilter.ItemsSource = mFilterTypes;
            dgResults.ItemsSource = mResults;
            txtCurrCount.DataContext = this;
            txtTotCount.DataContext = this;

            cmbFilter.SelectedItem = FILTER_360DAYS;
        }

        private void OnPropertyChanged(string Name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(Name));
            }
        }

        private void LoadResults()
        {
            mResults = new List<ManifestResultsRecordForCleanup>();

            foreach (DlkTestSuiteResultsManifestRecord rec in DlkTestSuiteResultsManifestHandler.GetSuiteResultsManifestRecords())
            {
                mResults.Add(new ManifestResultsRecordForCleanup(rec));
            }

            mResults = mResults.OrderByDescending(x => Convert.ToDateTime(x.executiondate)).ToList();
        }

        private void Filter()
        {
            string filterString = cmbFilter.SelectedItem == null ? FILTER_360DAYS : cmbFilter.SelectedItem.ToString();
            Predicate<ManifestResultsRecordForCleanup> filterType = null;

            switch (filterString)
            {
                case FILTER_1DAY:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(1, 0, 0, 0);
                    break;
                case FILTER_3DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(3, 0, 0, 0);
                    break;
                case FILTER_7DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(7, 0, 0, 0);
                    break;
                case FILTER_15DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(15, 0, 0, 0);
                    break;
                case FILTER_30DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(30, 0, 0, 0);
                    break;
                case FILTER_90DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(90, 0, 0, 0);
                    break;
                case FILTER_180DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(180, 0, 0, 0);
                    break;
                case FILTER_360DAYS:
                    filterType = x => DateTime.Today.Date - Convert.ToDateTime(x.executiondate).Date > new TimeSpan(360, 0, 0, 0);
                    break;
                case FILTER_EVERYTHING:
                default:
                    filterType = x => true;
                    break;
            }

            SelectUnselectAll(true);
            foreach (ManifestResultsRecordForCleanup rec in mResults)
            {
                rec.isvisible = false;
            }

            foreach (ManifestResultsRecordForCleanup rec in mResults.FindAll(filterType))
            {
                rec.isvisible = true;
            }
        }

        private void SelectUnselectAll(bool IsCheck)
        {
            foreach (ManifestResultsRecordForCleanup rec in mResults)
            {
                rec.ischecked = IsCheck;
            }
        }

        private void Notify()
        {
            dgResults.Items.Refresh();
            TotalRecordCount = mResults == null ? "0" : mResults.Count.ToString();
            CurrentRecordCount = mResults == null ? "0" : mResults.FindAll(x => x.ischecked && x.isvisible).Count.ToString(); ;
        }

        private void Save()
        {
            //if (DlkUserMessages.ShowQuestionOkCancelWarning()
            foreach (DlkTestSuiteResultsManifestRecord rec in mResults.FindAll(x => x.ischecked && x.isvisible))
            {
                DlkTestSuiteResultsManifestHandler.DeleteResultsManifestRecord(rec);
            }
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Filter();
                Notify();
            }
            catch
            {

            }
        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectUnselectAll((bool)chkSelectAll.IsChecked);
                Notify();
            }
            catch
            {

            }
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectUnselectAll((bool)chkSelectAll.IsChecked);
                Notify();
            }
            catch
            {

            }
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                Notify();
            }
            catch
            {

            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int itemsToClearCount = mResults.FindAll(x => x.isvisible && x.ischecked).Count;
                if (itemsToClearCount == 0)
                {
                    DlkUserMessages.ShowInfo("No entries to clean-up.");
                    return;
                }
                if (DlkUserMessages.ShowQuestionYesNoWarning("Are you sure you want to remove " 
                    + itemsToClearCount +" of " + mResults.Count + " total records?") == MessageBoxResult.Yes)
                {
                    Save();
                    DlkUserMessages.ShowInfo("Clean-up completed.");
                    LoadResults();
                    LoadDefaults();
                    Notify();
                }
            }
            catch
            {

            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch
            {

            }
        }
    }

    public class ManifestResultsRecordForCleanup : DlkTestSuiteResultsManifestRecord
    {
        public bool ischecked { get; set; }
        public bool isvisible { get; set; }
        public new string resultsdirectory { get; set; }
        public new string suite { get; set; }
        public new string suitepath { get; set; }
        public new string executiondate { get; set; }
        public bool cleanup
        {
            get
            {
                return ischecked && isvisible;
            }
        }
        public ManifestResultsRecordForCleanup(DlkTestSuiteResultsManifestRecord MyBase, bool IncludeInCleanup=true, bool Visible=true)
            : base(MyBase.suite, MyBase.suitepath, MyBase.executiondate, MyBase.passed, MyBase.failed, MyBase.notrun, MyBase.resultsdirectory)
        {
            ischecked = IncludeInCleanup;
            isvisible = Visible;
            resultsdirectory = MyBase.resultsdirectory;
            suite = MyBase.suite;
            suitepath = MyBase.suitepath;
            executiondate = MyBase.executiondate;
        }
    }
}
