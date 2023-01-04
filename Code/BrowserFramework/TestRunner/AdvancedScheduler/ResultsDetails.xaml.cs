using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using TestRunner.Common;
using TestRunner.AdvancedScheduler.Presenter;
using TestRunner.AdvancedScheduler.View;
using CommonLib.DlkHandlers;
using CommonLib.DlkUtility;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for ResultsDetails.xaml
    /// </summary>
    public partial class ResultsDetails : Window, IResultsDetailsView, INotifyPropertyChanged
    {
        #region PRIVATE MEMBERS
        private string mSuitePath;
        private string mExecutionDate;
        private string mDescription;
        private string mMachineName;
        private string mOperatingSystem;
        private string mDuration;
        private string mPassed;
        private string mFailed;
        private string mNotRun;
        private string mTotal;
        private string mPassRate;
        private string mCompletionRate;
        private string mUserName;
        private bool mCurrentShowAppNameSetting;
        private BitmapImage mErrorScreenShot;
        private DlkExecutionQueueRecord mSelectedTest;
        private List<DlkExecutionQueueRecord> mExecutionQueueRecords;
        private List<DlkTestStepRecord> mLogs;
        private ResultsDetailsPresenter mPresenter;
        private TextBlock mDisplayNameTextBlock;
        private string mDistributionList;
        private List<string> mDisplayNames = new List<string>(new string[] { "File Name [default]", "Test Name", "Full Path" });
        private bool mShouldCollapseLogRow = true;
        private DataGridRow mCurrentLogRow = null;
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="suitePath">Path of target suite</param>
        /// <param name="suiteResultsPath">Path of target suite results</param>
        public ResultsDetails(string suitePath, string suiteResultsPath, string DistributionList="")
        {
            InitializeComponent();
            SuitePath = suitePath;
            SuiteResultsPath = suiteResultsPath;
            mDistributionList = DistributionList;
            mPresenter = new ResultsDetailsPresenter(this);
        }
        #endregion

        #region PUBLIC MEMBERS
        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// List of tests in target suite
        /// </summary>
        public List<DlkExecutionQueueRecord> ExecutionQueueRecords
        {
            get
            {
                return mExecutionQueueRecords;
            }
            set
            {
                mExecutionQueueRecords = value;
                TestQueue.ItemsSource = mExecutionQueueRecords;
                TestQueue.Items.Refresh();
            }
        }

        /// <summary>
        /// Number of passed test in target suite
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
        /// Number of failed tests in target suite
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
        /// Total number of tests ib target suite
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
        /// Number of Not Run test in target suite
        /// </summary>
        public string NotRun
        {
            get
            {
                return mNotRun;
            }
            set
            {
                mNotRun = value;
                OnPropertyChanged("NotRun");

            }
        }

        /// <summary>
        /// Success rate in percentage of target suite
        /// </summary>
        public string PassRate
        {
            get
            {
                return mPassRate;
            }
            set
            {
                mPassRate = value;
                OnPropertyChanged("PassRate");

            }
        }

        /// <summary>
        /// Completion rate in percentage of target suite
        /// </summary>
        public string CompletionRate
        {
            get
            {
                return mCompletionRate;
            }
            set
            {
                mCompletionRate = value;
                OnPropertyChanged("CompletionRate");
            }
        }

        /// <summary>
        /// Current error screenshot
        /// </summary>
        public BitmapImage ErrorScreenShot
        {
            get
            {
                return mErrorScreenShot;
            }
            set
            {
                mErrorScreenShot = value;
                OnPropertyChanged("ErrorScreenShot");
            }
        }

        /// <summary>
        /// Path of target suite
        /// </summary>
        public string SuitePath
        {
            get
            {
                return mSuitePath;
            }
            set
            {
                mSuitePath = value;
                OnPropertyChanged("SuitePath");
            }
        }

        /// <summary>
        /// Path if target suite results
        /// </summary>
        public string SuiteResultsPath { get; set; }

        /// <summary>
        /// Traget suite result execution date
        /// </summary>
        public string ExecutionDate
        {
            get
            {
                return mExecutionDate;
            }
            set
            {
                mExecutionDate = value;
                OnPropertyChanged("ExecutionDate");
            }
        }

        /// <summary>
        /// Target suite description
        /// </summary>
        public string Description
        {
            get
            {
                return mDescription;
            }
            set
            {
                mDescription = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>
        /// User who executed target suite
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
                OnPropertyChanged("UserName");
            }
        }

        public bool IsShowAppNameSettingChanged
        {
            get 
            {
                if (!DlkEnvironment.IsShowAppNameProduct)
                    return false;

                bool newShowAppNameSetting = DlkEnvironment.IsShowAppNameEnabled();
                if (newShowAppNameSetting != mCurrentShowAppNameSetting)
                {
                    mCurrentShowAppNameSetting = newShowAppNameSetting;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Machine where target suite was executed
        /// </summary>
        public string MachineName
        {
            get
            {
                return mMachineName;
            }
            set
            {
                mMachineName = value;
                OnPropertyChanged("MachineName");
            }
        }

        /// <summary>
        /// OS against which target suite was executed
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
                OnPropertyChanged("OperatingSystem");
            }
        }

        /// <summary>
        /// Duration of target suite execution
        /// </summary>
        public string Duration
        {
            get
            {
                return mDuration;
            }
            set
            {
                mDuration = value;
                OnPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Currently selected test
        /// </summary>
        public DlkExecutionQueueRecord SelectedTest
        {
            get
            {
                return mSelectedTest;
            }
            set
            {
                mSelectedTest = value;
                mPresenter.LoadLogs();
            }
        }

        /// <summary>
        /// Current test logs
        /// </summary>
        public List<DlkTestStepRecord> Logs
        {
            get
            {
                return mLogs;
            }
            set
            {
                mLogs = value;
                dgLogTestSteps.ItemsSource = mLogs;
                dgLogTestSteps.Items.Refresh();
            }
        }
        #endregion

        #region PRIVATE FUNCTIONS
        /// <summary>
        /// Load suite
        /// </summary>
        private void LoadSuite()
        {
            mPresenter.Load();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ErrorMessage">Error text to display</param>
        public void DisplayError(string ErrorMessage)
        {
            DlkUserMessages.ShowError(ErrorMessage);
        }

        /// <summary>
        /// Get all cells conatined inside target datagrid row
        /// </summary>
        /// <param name="dgr">Target datagrid row</param>
        /// <returns>Datagrid cells in target row</returns>
        private List<DataGridCell> GetLogCellsFromRow(DataGridRow dgr)
        {
            List<DataGridCell> lst = new List<DataGridCell>();
            this.GetVisualChildren<DataGridCell>(dgr, lst);
            return lst;
        }

        /// <summary>
        /// Get all visual childen of certain type of input parent control
        /// </summary>
        /// <typeparam name="T">Child type</typeparam>
        /// <param name="parent">Parent control</param>
        /// <param name="children">Children control</param>
        /// <returns></returns>
        private T GetVisualChildren<T>(Visual parent, List<T> children) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = this.GetVisualChildren<T>(v, children);
                }
                else if (child != null)
                {
                    children.Add(child);
                }
            }
            return child;
        }

        /// <summary>
        /// Returns a MenuItem with the correct properties
        /// Purpose is for proper and uniform format of context menu items
        /// </summary>
        /// <param name="menuHeader"></param>
        private MenuItem SetMenuItem(string menuHeader, RoutedEventHandler eventHandlerName)
        {
            MenuItem mnu = new MenuItem();
            mnu.Header = menuHeader;
            mnu.Click += new RoutedEventHandler(eventHandlerName);
            return mnu;
        }

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
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// Window loaded event handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadSuite();
                mCurrentShowAppNameSetting = DlkEnvironment.IsShowAppNameEnabled();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Windows activate event handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event argument</param>
        private void Window_Activated(object sender, EventArgs e)
        {
            if (IsShowAppNameSettingChanged)
            {
                TestQueue_SelectionChanged(this, null);
            }
        }

        /// <summary>
        /// Event handler for Test Queue selection chanaged
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void TestQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (TestQueue != null && TestQueue.SelectedItem != null)
                {
                    while (DlkDynamicObjectStoreHandler.Instance.Screens.Count == 0 || DlkDynamicObjectStoreHandler.Instance.StillLoading)
                    {
                        System.Threading.Thread.Sleep(500);
                    }

                    SelectedTest = TestQueue.SelectedItem as DlkExecutionQueueRecord;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler of load event of log grid
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LoadLogDetailsGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                string errorLogLevel = DlkEnvironment.mErrorLogLevel;
                DataGrid logGrid = sender as DataGrid;

                foreach (object dgr in logGrid.Items)
                {
                    DataGridRow _dgr = (DataGridRow)(logGrid.ItemContainerGenerator.ContainerFromItem(dgr));

                    foreach (DataGridCell cell in GetLogCellsFromRow(_dgr))
                    {
                        /* Hide rows based on settings */
                        DataGridColumn dgc = cell.Column;
                        string myCellContent = ((TextBlock)(cell.Content)).Text;
                        if (dgc.Header.Equals("Type"))
                        {
                            switch (errorLogLevel)
                            {
                                case "default":
                                    _dgr.Visibility = Visibility.Visible;
                                    break;
                                case "errorinfo":
                                    if (myCellContent.Contains("EXCEPTIONIMG") || myCellContent.Contains("INFO") || myCellContent.Contains("EXCEPTIONMSG"))
                                    {
                                        _dgr.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        _dgr.Visibility = Visibility.Collapsed;
                                    }
                                    break;
                                case "erroronly":
                                    if (myCellContent.Contains("EXCEPTIONIMG") || myCellContent.Contains("EXCEPTIONMSG"))
                                    {
                                        _dgr.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        _dgr.Visibility = Visibility.Collapsed;
                                    }
                                    break;
                                default:
                                    _dgr.Visibility = Visibility.Visible;
                                    break;
                            }
                        }

                        /* Make error image clickable*/
                        if (myCellContent.Contains("Image: ") || myCellContent.Contains("outputfile: ") || DlkString.IsFile(myCellContent))
                        {
                            /* Hotfix for displaying link of image from remote agents */
                            if (myCellContent.Contains("Image: "))
                            {
                                ((TextBlock)(cell.Content)).Text = "Image: " + System.IO.Path.Combine(SuiteResultsPath, 
                                    System.IO.Path.GetFileName(myCellContent));
                            }
                            /* end of hotfix */
                            cell.MouseLeftButtonUp -= LogDetailsClick;
                            cell.MouseLeftButtonUp += LogDetailsClick;
                            cell.MouseEnter += LogDetailsMouseOver;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for click event of link in logs
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LogDetailsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string pathToFile = "";

                var cellContent = ((TextBlock)(((DataGridCell)sender).Content)).Text;
                if (DlkString.IsFile(cellContent))
                {
                    pathToFile = cellContent;
                }
                else if (cellContent.Contains("Image:"))
                {
                    pathToFile = cellContent.Replace("Image: ", "");
                }
                else if (cellContent.Contains("outputfile:"))
                {
                    pathToFile = cellContent.Substring(cellContent.LastIndexOf("outputfile:") + 12);
                }

                if (pathToFile.Contains(".xml") || pathToFile.Contains(".html"))
                {
                    System.Diagnostics.Process.Start(pathToFile);
                }
                else
                {
                    mPresenter.DisplayLinkedImage(pathToFile);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for mouseover event of links in logs
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LogDetailsMouseOver(object sender, RoutedEventArgs e)
        {
            try
            {
                ((DataGridCell)sender).Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler of loading row event of log grid
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void dgLogTestSteps_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
                if (e.Row.Item is DlkTestStepRecord)
                {
                    SolidColorBrush mStatusBrush = new SolidColorBrush(Colors.LightGreen);
                    SolidColorBrush mRedBrush = new SolidColorBrush(Colors.Tomato);
                    SolidColorBrush mSilverBrush = new SolidColorBrush(Colors.Silver);
                    SolidColorBrush mSalmonBrush = new SolidColorBrush(Colors.Salmon);

                    DlkTestStepRecord mRowRec = (DlkTestStepRecord)e.Row.DataContext;

                    if (mRowRec.mStepNumber == 0) /* SETUP */
                    {
                        e.Row.Background = mSilverBrush;
                    }
                    foreach (DlkLoggerRecord lRec in mRowRec.mStepLogMessages)
                    {
                        if (lRec.mMessageType.ToLower().Contains("exception"))
                        {
                            e.Row.Background = mRowRec.mStepNumber == 0 ? mSalmonBrush : mRedBrush;
                            return;
                        }
                    }
                    if (mRowRec.mStepStatus.ToLower() != "not run" & mRowRec.mStepNumber > 0)
                    {
                        e.Row.Background = mStatusBrush;
                    }
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for click event of ViewErrorScreenshot button
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnViewErrorScreenshot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mPresenter.DisplayScreenshot();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Hanlder for click event of btnDisplayName
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnDisplayName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button headerBtn = sender as Button;
                if (sender != null && TestQueue.Items.Count > 0)
                {
                    TestQueue.CancelEdit();
                    if (headerBtn.ContextMenu == null)
                    {
                        headerBtn.ContextMenu = new ContextMenu();
                        /* Add items */
                        foreach(string itm in mDisplayNames)
                        {
                            MenuItem mnu = SetMenuItem(itm, DisplayNameContextMenuClicked);
                            headerBtn.ContextMenu.Items.Add(mnu);
                            mnu.IsChecked = mnu.Header.ToString().Contains("default");
                        }
                    }
                    headerBtn.ContextMenu.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event hanlder for environment context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayNameContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TestQueue.CancelEdit();
                MenuItem mnu = sender as MenuItem;

                /* Uncheck all */
                foreach (MenuItem itm in (mnu.Parent as ContextMenu).Items)
                {
                    itm.IsChecked = false;
                }

                /* set text via binding */
                string displayName = mnu.Header.ToString().ToLower();
                var bnd = new Binding("file");
                if (displayName.Contains("test"))
                {
                    bnd = new Binding("name");
                    mDisplayNameTextBlock.Text = "Test Name";
                }
                else if (displayName.Contains("path"))
                {
                    bnd = new Binding("fullpath");
                    mDisplayNameTextBlock.Text = "Full Path";
                }
                else
                {
                    mDisplayNameTextBlock.Text = "File Name";
                }
                TestDisplayName.Binding = bnd;

                /* Check */
                mnu.IsChecked = true;
                TestQueue.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Loaded event of btnDisplayName
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguemnts</param>
        private void btnDisplayName_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mDisplayNameTextBlock = sender as TextBlock;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Email send click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmailResults emailResultsFrm = new EmailResults(SuitePath, SuiteResultsPath, mDistributionList);
                emailResultsFrm.Owner = this;
                emailResultsFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Log Clicks
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LogRow_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                /* Expand if there is a collapsed row OR user switched rows */
                if (dgLogTestSteps.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.Collapsed || sender != mCurrentLogRow)
                {
                    dgLogTestSteps.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                }
                else if (mShouldCollapseLogRow) /* Do not collapse if log row details is clicked */
                {
                    dgLogTestSteps.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
                }
                mShouldCollapseLogRow = true;
                mCurrentLogRow = sender as DataGridRow; // cache clicked row
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for log details click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">event arguments</param>
        private void LogRowDetails_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                mShouldCollapseLogRow = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion
    }
}
