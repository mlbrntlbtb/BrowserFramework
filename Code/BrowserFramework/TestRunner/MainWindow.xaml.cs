//#define ADVANCED_SCHEDULER

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;
using TestRunner.Common;
using TestRunner.Recorder;
using TestRunner.AdvancedScheduler;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;
using System.Xml.Linq;
using System.Net.Mail;
using System.Windows.Threading;
using TestRunner.Designer;
using TestRunner.Controls;
using Recorder.Presenter;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged, ISuiteEditor
    {
        #region PUBLIC MEMBERS
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PRIVATE MEMBERS
        private const int PROCESS_ACTION_ARG_IDX_ACTION = 0;
        private const int PROCESS_ACTION_ARG_IDX_PATH = 1;
        private const int PROCESS_ACTION_ARG_IDX_COMMENTS = 2;
        private const int PROCESS_ACTION_ARG_IDX_SOURCE = 3;
        private const int PROCESS_ACTION_ARG_EXACT_COUNT = 4;
        private const int PROCESS_ACTION_RES_IDX_ACTION = 0;
        private const int PROCESS_ACTION_RES_IDX_RET = 1;
        private const int PROCESS_ACTION_RES_IDX_SAVEPATH = 2;
        private const int PROCESS_ACTION_RES_EXACT_COUNT = 3;
        private const string STATUS_TEXT_SAVING = "Saving...";
        private const string STATUS_TEXT_DELETING = "Deleting...";
        private const string STATUS_TEXT_MOVING_SOURCE_CONTROL = "Moving source control files...";
        private const string STATUS_TEXT_READY = "Ready";
        private const string STATUS_TEXT_CHECKIN_CHANGES_TO_CONFIG = "Checking-in changes to configuration files";
        private const string STATUS_TEXT_RUNNING_TESTS_IN_QUEUE = "Running tests in queue...";
        private const string STATUS_TEXT_RELOADING = "Reloading Test Runner...";
        private const string STATUS_TEXT_CHANGING_APPLICATION = "Changing target application...";
        private const string STATUS_TEXT_CHANGING_TESTSUITEDIR = "Changing test/suite directories...";
        private const string MENU_TEXT_TESTSONLY = "Search for tests only";
        private const string MENU_TEXT_TAGSONLY = "Search for tags only";
        private const string MENU_TEXT_AUTHORONLY = "Search for authors only";
        private const string MENU_TEXT_TESTSANDTAGS = "Include tests and tags in search";

        private System.Windows.Forms.Timer mTxtSearchTimer;
        private string mLoadedSuite = string.Empty;
        private delegate void InvokerDelegate();
        private DlkTestSuiteInfoRecord mTestSuiteInfo;
        private String mTestRoot;
        private ObservableCollection<DlkExecutionQueueRecord> mExecutionQueueRecords;
        private List<String> mStatusList;
        private List<KwDirItem> mKeywordDirectories;
        private List<KwDirItem> mExpandedDirectories;
        private String mTestFilter = "";
        private String mSuiteToExecute = "";
        private KwDirItem.SearchType mSearchType;
        private Boolean mCommandLineMode = false;
        private BackgroundWorker mHistoryBW = new BackgroundWorker();
        private String mCurrentlyDisplayedLog;
        private DlkExecutionQueueRecord mCurrentSelectedTestInQueue;
        private List<DlkExecutionQueueRecord> deqrClipboard = new List<DlkExecutionQueueRecord>();
        private BackgroundWorker mExecutionBW;
        private bool mTestExecutionCompleted = false;
        private PopulateReportDBDialog mPopulateReportDialog;
        private TestExecutionDialog mTestExecutionDialog;
        private DlkTestHistory mCurrentTestHistory;
        private DataGrid mlogGrid;
        private BackgroundWorker mSyncBW;
        private string mPathToSync;
        private BackgroundWorker mCheckInBW;
        private BackgroundWorker mCopyTestFilesBW;
        private BackgroundWorker mCopySuiteFilesBW;
        private Dictionary<TreeViewItem, KwDirItem> mTreeItemSelectionList = new Dictionary<TreeViewItem, KwDirItem>();
        private bool mIsCheckingIn = false;
        private bool mExpanded;
        private bool mIsQueueUpdated = false;
        private bool mIsInsert = false;
        private bool mIsCtxQueueOpen = false;
        private bool mIsCtxSchedulerOpen = false;
        private bool mIsResultOpen = false;
        private bool mIsTestQueueRunning = false;
        private bool mAllowQueueRefreshForKeepOpen = false;
        private bool mAskDependencyOnce = false;
        private bool mAskDependencyCancelled = false;
        private bool mRemoveDependency = false;
        private bool _isChanged;
        private TextBlock mDisplayNameTextBlock = new TextBlock();
        private List<string> mExecuteValueList = null;
        private ObservableCollection<string> mWebBrowsers;
        private ObservableCollection<string> mRemoteBrowsers;
        private ObservableCollection<string> mMobileBrowsers;        
        private ListCollectionView mAllBrowsers;
        private DlkTestResultsDbTalker mResultsDbTalker { get; set; }
        private string mImportLocation;
        private string mSchedulerFileName = "scheduler.exe";
        private string mTestRunnerTitle = string.Empty;
        private string mTestCaptureStatus = string.Empty;
        TestCapture mFrmTestCapture;
        OSRecorder mFrmOSRecorder;
        private bool mShouldCollapseLogRow = true;
        private DataGridRow mCurrentLogRow = null;
        private bool mIsProcessInProgress = false;
        private bool mIsSourceControlDragDropSuccessful = false;
        private string mStatusText = string.Empty;
        private BackgroundWorker mProcessWorker = null;
        private BackgroundWorker mRefreshBW;
        private int testSuiteInstanceCount;
        private int testSuiteInstancePrevVal;
        private string testSuiteEnvironmentPrevVal;
        private string testSuiteExecutePrevVal;
        private string testSuiteBrowserPrevVal;
        private string testSuiteDescriptionPrevVal;
        private string testSuiteOwnerPrevVal;
        private DlkProductConfigHandler mProdConfigHandler;
        private Modal mTestConnectModalDlg = null;
        private enum ProcessWorkerAction /* Add here if additional action in the future */
        {
            SaveSuiteLocal,
            SaveSuiteLocalSilent,
            SaveAndCheckInSuite,
            SaveAndCheckInSuiteSilent,
            AddTestFolderLocal,
            AddAndCheckInTestFolder,
            MoveAndCheckInTest,
            DeleteTestItemLocal,
            DeleteAndCheckInTestItem,
            DeleteMultipleTestItemsLocal,
            DeleteAndCheckInMultipleTestItems
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// List conatining all EnvironmentIDs on config file
        /// </summary>
        public ObservableCollection<string> EnvironmentIDs
        {
            get
            {
                DlkLoginConfigHandler loginHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile);
                ObservableCollection<string> ret = new ObservableCollection<string>();
                foreach (DlkLoginConfigRecord rec in loginHandler.mLoginConfigRecords)
                {
                    ret.Add(rec.mID);
                }
                return new ObservableCollection<string>(ret.OrderBy(str => str).ToList<string>());
            }
        }

        /// <summary>
        /// List containing all Browsers read from the config file, classified into groups determined by BrowserType property
        /// </summary>
        public ListCollectionView AllBrowsers
        {
            get
            {
                ObservableCollection<DlkBrowser> browserBuffer = new ObservableCollection<DlkBrowser>();
                mAllBrowsers = new ListCollectionView(browserBuffer);

                foreach (string browserName in mWebBrowsers)
                {
                    browserBuffer.Add(new DlkBrowser("Web", browserName));
                }
                foreach (string browserName in mRemoteBrowsers)
                {
                    browserBuffer.Add(new DlkBrowser("Remote", browserName));
                }
                foreach (string browserName in mMobileBrowsers)
                {
                    browserBuffer.Add(new DlkBrowser("Mobile", browserName));
                }

                mAllBrowsers.GroupDescriptions.Add(new PropertyGroupDescription("BrowserType"));

                return mAllBrowsers;
            }
        }

        /// <summary>
        /// List containing all Browsers on config file
        /// </summary>
        public ObservableCollection<string> KeepOpenStates
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "Keep browser open after execution", "Close browser after execution [default]"});
                return ret;
            }
        }

        /// <summary>
        /// List containing all Browsers on config file
        /// </summary>
        public ObservableCollection<string> DisplayNames
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "File Name [default]", "Test Name", "Full Path"});
                return ret;
            }
        }

        /// <summary>
        /// The currently loaded suite. This is expressed in absolute path
        /// </summary>
        public String LoadedSuite
        {
            get
            {
                return mLoadedSuite;
            }
            set
            {
                mLoadedSuite = value;
                OnPropertyChanged("LoadedSuite");
                OnPropertyChanged("IsSuiteLoaded");
            }
        }

        /// <summary>
        /// The results folder currently loaded. This is expressed in absolute path
        /// </summary>
        public string LoadedResultFolder
        {
            get;
            set;
        }

        /// <summary>
        /// Root directory of all tests
        /// </summary>
        public String TestRoot
        {
            get
            {
                if (mTestRoot == null)
                {
                    mTestRoot = DlkEnvironment.mDirTests;
                }
                return mTestRoot;
            }
            set
            {
                mTestRoot = value;
                txtKwRootFolder.Text = value;
                txtKwRootFolder.ToolTip = value;
            }
        }

        /// <summary>
        /// Destination of files or folders to be imported
        /// </summary>
        public String ImportLocation
        {
            get
            {
                if (mImportLocation == null)
                {
                    mImportLocation = DlkEnvironment.mDirTests;
                }
                return mImportLocation;
            }
            set
            {
                mImportLocation = value;
            }
        }

        /// <summary>
        /// Data source of execution queue
        /// </summary>
        public ObservableCollection<DlkExecutionQueueRecord> ExecutionQueueRecords
        {
            get
            {
                if (mExecutionQueueRecords == null)
                {
                    mExecutionQueueRecords = new ObservableCollection<DlkExecutionQueueRecord>();
                }
                return mExecutionQueueRecords;
            }
        }

        /// <summary>
        /// List of test execution status
        /// </summary>
        public List<String> StatusList
        {
            get
            {
                if (mStatusList == null)
                {
                    mStatusList = new List<string>();
                    mStatusList.Add("Not Run");
                    mStatusList.Add("Passed");
                    mStatusList.Add("Failed");
                    mStatusList.Add("Blocked");
                }
                return mStatusList;
            }
        }

        /// <summary>
        /// Item source of Execute column
        /// </summary>
        public List<string> ExecuteValueList
        {
            get
            {
                if (mExecuteValueList == null)
                {
                    mExecuteValueList = new List<string>();
                    mExecuteValueList.Add("True");
                    mExecuteValueList.Add("False");
                    mExecuteValueList.Add("Set condition...");
                }
                return mExecuteValueList;
            }
        }

        /// <summary>
        /// Data source of Test Explorer treeview
        /// </summary>
        private List<KwDirItem> KeywordDirectories
        {
            get
            {
                if (mKeywordDirectories == null)
                {
                    mKeywordDirectories = new List<KwDirItem>();
                    mKeywordDirectories = new DlkKeywordTestsLoader().GetKeywordDirectories(TestRoot,mExpandedDirectories);
                }
                return mKeywordDirectories;
            }
            set
            {
                mKeywordDirectories = value;
            }
        }

        /// <summary>
        /// Object to load object store files
        /// </summary>
        public DlkDynamicObjectStoreHandler ObjectStoreHandler
        {
            get 
            { 
                return DlkDynamicObjectStoreHandler.Instance; 
            }
        }
        
        /// <summary>
        /// Flag to cache if TFS mapping is present
        /// </summary>
        public bool IsMapped
        {
            get;
            set;
        }

        /// <summary>
        /// Flag to cache if test settings are expanded or collapsed
        /// </summary>
        public bool IsTestTreeExpanded
        {
            get
            {
                return mExpanded;
            }
            set
            {
                try
                {
                    DlkConfigHandler.UpdateConfigValue("testtreeexpanded", value.ToString());
                }
                catch
                {
                    // do nothing
                }
                mExpanded = value;
                OnPropertyChanged("IsTestTreeExpanded");
            }
        }

        /// <summary>
        /// The current instance of the Test Capture form
        /// </summary>
        public TestCapture TestCaptureForm
        {
            get
            {
                return mFrmTestCapture;
            }
            set
            {
                mFrmTestCapture = value;
            }
        }

        public OSRecorder OSRecorderForm
        {
            get
            {
                return mFrmOSRecorder;
            }
            set
            {
                mFrmOSRecorder = value;
            }
        }

        /// <summary>
        /// Flag for adding asterisk to suite title
        /// </summary>
        public bool isChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                if (_isChanged)
                {
                    string title = Title.Replace(mTestRunnerTitle + " : ", "");

                    if (!title.Contains("*") && title != mTestRunnerTitle)
                    {
                        Title = mTestRunnerTitle + " : *" + title;
                    }
                }
                else
                    Title = Title.Replace("*", "");
            }
        }

        /// <summary>
        /// Readonly flag to check if CTRL key is pressed
        /// </summary>
        private bool IsCtrlPressed
        {
            get
            {
                return System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl)
                    || System.Windows.Input.Keyboard.IsKeyDown(Key.RightCtrl);
            }
        }

        /// <summary>
        /// List of items to expand
        /// </summary>
        private List<KwDirItem> ExpandedList(List<KwDirItem> listToExpand)
        {
            List<KwDirItem> listExpanded = new List<KwDirItem>();
            foreach (KwDirItem item in listToExpand)
            {
                if (item.IsExpanded)
                {
                    if (item is TestRunner.Common.KwFolder)
                    {
                        KwFolder folder = (KwFolder)item;
                        listExpanded.Add(folder);
                        if (folder.DirItems.Count > 0)
                        {
                            listExpanded.AddRange(ExpandedList(folder.DirItems));
                        }
                    }
                    else if (item is TestRunner.Common.KwFile)
                    {
                        listExpanded.Add((KwFile)item);
                    }
                }

            }
            return listExpanded;
        }

        /// <summary>
        /// Flag that indicates whether a process is in progress in the background
        /// </summary>
        public bool IsBackgroundProcessInProgress
        {
            get
            {
                return mIsProcessInProgress;
            }
            set
            {
                mIsProcessInProgress = value;
                OnPropertyChanged("IsBackgroundProcessInProgress");
            }
        }

        /// <summary>
        /// Flag that indicates whether a suite is loaded
        /// </summary>
        public bool IsSuiteLoaded
        {
            get
            {
                return !string.IsNullOrEmpty(LoadedSuite);
            }
        }

        /// <summary>
        /// Status text when Main Window is busy
        /// </summary>
        public string StatusText
        {
            get
            {
                return mStatusText;
            }
            set
            {
                mStatusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initializes form and its members
        /// </summary>
        public void Initialize()
        {
            /* UI default values */
//#if DEBUG
//            Title = "Deltek Test Runner [" + DlkEnvironment.mProduct + "]";
//#else
//            About abt = new About();
//            Title = "Deltek Test Runner [" + abt.TargetApplicationNameAndVersion + "]";
//#endif

            mTestRunnerTitle = "Deltek Test Runner [" + DlkTestRunnerSettingsHandler.ApplicationUnderTest.DisplayName + "]";
            Title = mTestRunnerTitle;

            string[] searchType = { MENU_TEXT_TESTSONLY, MENU_TEXT_AUTHORONLY, MENU_TEXT_TAGSONLY, MENU_TEXT_TESTSANDTAGS };
            cmnuSearchTypes.Items.Clear();
            for (int countType = 0; countType < searchType.Length; countType++)
            {
                MenuItem searchItem = new MenuItem();
                searchItem.Header = searchType[countType];
                searchItem.IsCheckable = true;
                if (countType == 0)
                {
                    searchItem.IsChecked = true;
                }
                searchItem.Click += SearchTypeMenuItem_Click;
                cmnuSearchTypes.Items.Add(searchItem);
            }

            /* Set initial value */
            ClearTestsInQueue();

            mTestSuiteInfo = new DlkTestSuiteInfoRecord();
            TestRoot = DlkEnvironment.mDirTests;
            //Setting test tree to collapse view by default to minimize usage of memory upon load (even when changing target product)
            btnTestDirSettings.IsChecked = false;
            IsTestTreeExpanded = false;
            UpdateButtonStates();

            /* Initialize Data Bindings */
            this.DataContext = this;
            tvKeywordDirectory.DataContext = KeywordDirectories;
            TestQueue.DataContext = ExecutionQueueRecords;
            //TestEnvironmentColumn.ItemsSource = EnvironmentIDs;
            OnPropertyChanged("EnvironmentIDs");

            /* Sub initializers */
            InitializeBrowserLists();
            InitializeContextMenus();
            InitializeSourceControl();
            InitializeHistoryLoader();
            InitializeProcessWorker();
            InitializeFileCopyWorker();
            InitializeControls();
            DlkEnvironment.InitializeBlacklistedURLs();

            mLoadedResultsFileManifest.Clear();
        }

        /// <summary>
        /// Get the origal value of instance before editing
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TextBox_InstanceGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtInstance = (TextBox)sender;
            testSuiteInstancePrevVal = Convert.ToInt32(txtInstance.Text);
            DlkExecutionQueueRecord selectedItem = (DlkExecutionQueueRecord)TestQueue.SelectedItem;
            DlkTest test = new DlkTest(selectedItem.fullpath);
            testSuiteInstanceCount = test.mInstanceCount;
        }

        /// <summary>
        /// Validation of instance value when edited on the table
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TextBox_InstanceLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtInstance = (TextBox)sender;

            try
            {
                if (string.IsNullOrEmpty(txtInstance.Text))
                {
                    throw new Exception(DlkUserMessages.ERR_VALUE_NOT_EMPTY);
                }
                int instanceVal;
                if (!int.TryParse(txtInstance.Text, out instanceVal)
                    || instanceVal < 1 || instanceVal > testSuiteInstanceCount)
                {
                    throw new Exception(DlkUserMessages.ERR_INVALID_INSTANCE_VALUE);
                }
                if (txtInstance.Text != testSuiteInstancePrevVal.ToString())
                {
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                txtInstance.Text = testSuiteInstancePrevVal.ToString();
                DlkUserMessages.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Get the original value of environment before editing
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ComboBox_EnvironmentGotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox cbBoxEnv = (ComboBox)sender;
                testSuiteEnvironmentPrevVal = cbBoxEnv.Text;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Validation of environment value when edited on the table
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ComboBox_EnvironmentLostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox cbBoxEnv = (ComboBox)sender;
            try
            {
                if (string.IsNullOrEmpty(cbBoxEnv.Text))
                {
                    throw new Exception(DlkUserMessages.ERR_BLANK_ENVIRONMENT_CHANGE_DEFAULT);
                }
                else
                {
                    bool eFound = false;
                    foreach (var env in EnvironmentIDs)
                    {
                        if (env.Equals(cbBoxEnv.Text))
                        {
                            eFound = true;
                            testSuiteEnvironmentPrevVal = null;
                            if (mAllowQueueRefreshForKeepOpen)
                            {
                                ChangeKeepOpenBrowsers();
                                mAllowQueueRefreshForKeepOpen = false;
                            }
                            break;
                        }
                    }
                    if (!eFound)
                    {
                        throw new Exception(DlkUserMessages.ERR_INVALID_ENVIRONMENT_VALUE);
                    }

                }
            }
            catch (Exception ex)
            {
                cbBoxEnv.Text = !String.IsNullOrEmpty(testSuiteEnvironmentPrevVal) ? testSuiteEnvironmentPrevVal : cbBoxEnv.Text;
                testSuiteEnvironmentPrevVal = null;
                DlkUserMessages.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Get the original value of browser before editing
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ComboBox_BrowserGotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox cbBoxBrowser = (ComboBox)sender;
                testSuiteBrowserPrevVal = cbBoxBrowser.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Get the original value of execute before editing
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ComboBox_ExecuteGotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox cbBoxExec = (ComboBox)sender;
                testSuiteExecutePrevVal = cbBoxExec.Text;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Validation of execute value when edited on the table
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ComboBox_ExecuteLostFocus(object sender, RoutedEventArgs e)
        {
            ComboBox cbBoxExec = (ComboBox)sender;
            try
            {
                if (string.IsNullOrEmpty(cbBoxExec.Text))
                {
                    throw new Exception(DlkUserMessages.ERR_BLANK_EXECUTE_CHANGE_DEFAULT);
                }
                else
                {
                    bool eFound = false;
                    foreach (var exec in ExecuteValueList)
                    {
                        if (exec.ToLower().Equals(cbBoxExec.Text.ToLower()))
                        {
                            eFound = true;
                            testSuiteExecutePrevVal = null;
                            break;
                        }
                    }
                    if (!eFound)
                    {
                        throw new Exception(DlkUserMessages.ERR_INVALID_EXECUTE_VALUE);
                    }
                }
            }
            catch (Exception ex)
            {
                cbBoxExec.Text = !String.IsNullOrEmpty(testSuiteExecutePrevVal) ? testSuiteExecutePrevVal : cbBoxExec.Text;
                testSuiteExecutePrevVal = null;
                DlkUserMessages.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name"></param>
        private void OnPropertyChanged(string Name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(Name));
            }
        }

        /// <summary>
        /// Initializes history loading, a background operation
        /// </summary>
        private void InitializeHistoryLoader()
        {
            mHistoryBW.DoWork += mHistoryBW_DoWork;
            mHistoryBW.RunWorkerCompleted += mHistoryBW_RunWorkerCompleted;
        }

        /// <summary>
        /// Initializes context menus in Execution Queue grid
        /// </summary>
        private void InitializeContextMenus()
        {
            ContextMenu env = ((ContextMenu)FindResource("ctxEnvironment"));
            ContextMenu browser = ((ContextMenu)FindResource("ctxBrowser"));
            ContextMenu isOpen = ((ContextMenu)FindResource("ctxIsOpen"));
            ContextMenu displayName = ((ContextMenu)FindResource("ctxDisplayName"));
            ContextMenu execute = ((ContextMenu)FindResource("ctxExecute"));
            ContextMenu queue = ((ContextMenu)FindResource("ctxQueueType"));
            ContextMenu schedulerType = ((ContextMenu)FindResource("ctxSchedulerType"));
            ContextMenu resultOptions = ((ContextMenu)FindResource("ctxResultOptionType"));

            // needed to loop instead of setting datacontext since I need MenuItem as type of item
            // I cannot dynamically assign handler of click event if item type is any other object
            browser.Items.Clear();
            env.Items.Clear();
            isOpen.Items.Clear();
            displayName.Items.Clear();
            execute.Items.Clear();
            queue.Items.Clear();
            schedulerType.Items.Clear();
            resultOptions.Items.Clear();

            foreach (string itm in EnvironmentIDs)
            {
                env.Items.Add(SetMenuItem(itm.Replace("_", "__"), EnvironmentContextMenuClicked));
            }

            if (mWebBrowsers.Count > 0)
            {
                browser.Items.Add(SetGroupMenu("Web"));
                foreach (string itm in mWebBrowsers)
                {
                    browser.Items.Add(SetMenuItem(itm.Replace("_", "__"), BrowserContextMenuClicked));
                }
            }

            if (mRemoteBrowsers.Count > 0)
            {
                browser.Items.Add(SetGroupMenu("Remote"));
                foreach (string itm in mRemoteBrowsers)
                {
                    browser.Items.Add(SetMenuItem(itm.Replace("_", "__"), BrowserContextMenuClicked));
                }
            }

            if (mMobileBrowsers.Count > 0)
            {
                browser.Items.Add(SetGroupMenu("Mobile"));
                foreach (string itm in mMobileBrowsers)
                {
                    browser.Items.Add(SetMenuItem(itm.Replace("_", "__"), BrowserContextMenuClicked));
                }
            }

            foreach (string itm in KeepOpenStates)
            {
                isOpen.Items.Add(SetMenuItem(itm, IsOpenContextMenuClicked));
            }

            foreach (string itm in DisplayNames)
            {
                MenuItem mnu = SetMenuItem(itm, DisplayNameContextMenuClicked);
                displayName.Items.Add(mnu);
                mnu.IsChecked = mnu.Header.ToString().Contains("default");
            }

            for (int i=0; i < 2; i++)
            {
                execute.Items.Add(SetMenuItem(ExecuteValueList[i], ExecuteContextMenuClicked));
            }
            
            // Add new menu item in Execute context menu
            execute.Items.Add(new Separator());
            execute.Items.Add(SetMenuItem("Apply conditional setting", LoadCreateExecutionConditionDialog));

            //Add new menu items for Queue context menu
            queue.Items.Add(SetMenuItem("Queue", QueueTypeMenuClicked));
            queue.Items.Add(SetMenuItem("Insert", QueueTypeMenuClicked));

            //Add new menu items for Scheduler context menu
            schedulerType.Items.Add(SetMenuItem("Scheduler", SchedulerTypeMenuClicked));
            schedulerType.Items.Add(SetMenuItem("New Scheduler", SchedulerTypeMenuClicked));

            //Add new menu item fro results option
            resultOptions.Items.Add(SetMenuItem("Load Results", ResultOptionMenuClicked));
            resultOptions.Items.Add(SetMenuItem("Clear Results", ResultOptionMenuClicked));
            resultOptions.Items.Add(SetMenuItem("Export Results", ResultOptionMenuClicked));

            //assign closed event for Queue context menu
            ((ContextMenu)FindResource("ctxQueueType")).Closed += new RoutedEventHandler(QueueContextMenuClosed);

            //assign closed event for Scheduler context menu
            ((ContextMenu)FindResource("ctxSchedulerType")).Closed += new RoutedEventHandler(SchedulerContextMenuClosed);

            //assign closed event for Result context menu
            ((ContextMenu)FindResource("ctxResultOptionType")).Closed += new RoutedEventHandler(ResultOptionMenuClosed);
        }

        /// <summary>
        /// Initializes context menus in Execution Queue grid
        /// </summary>
        public void InitializeBrowserLists()
        {
            mWebBrowsers = new ObservableCollection<string>();
            mRemoteBrowsers = new ObservableCollection<string>();
            mMobileBrowsers = new ObservableCollection<string>();

            foreach (DlkBrowser browser in DlkEnvironment.mAvailableBrowsers)
            {
                mWebBrowsers.Add(browser.Alias);
            }
#if DEBUG
            foreach (DlkRemoteBrowserRecord rec in DlkRemoteBrowserHandler.mRemoteBrowsers)
            {
                mRemoteBrowsers.Add(rec.Id);
            }
            foreach (DlkMobileRecord mob in DlkMobileHandler.mMobileRec)
            {
                mMobileBrowsers.Add(mob.MobileId);
            }
#endif
        }

        /// <summary>
        /// Initialize Process background worker
        /// </summary>
        private void InitializeProcessWorker()
        {
            if (mProcessWorker == null)
            {
                mProcessWorker = new BackgroundWorker();
                mProcessWorker.DoWork += mProcessWorker_DoWork;
                mProcessWorker.RunWorkerCompleted += mProcessWorker_RunWorkerCompleted;
            }
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
        /// Returns a MenuItem with the correct properties
        /// Purpose is for proper and uniform format of context menu headers
        /// </summary>
        /// <param name="menuHeader"></param>
        private MenuItem SetGroupMenu(string menuHeader)
        {
            MenuItem groupMenu = new MenuItem();
            groupMenu.Header = menuHeader;
            groupMenu.IsEnabled = false;
            groupMenu.FontWeight = FontWeights.Bold;
            groupMenu.Margin = new Thickness(-15, 0, 0, 0);

            return groupMenu;
        }


        /// <summary>
        /// Displays log results of selected test
        /// </summary>
        /// <param name="iRow"></param>
        private void DisplayLogResults(int iRow)
        {
            mCurrentlyDisplayedLog = "";
            dgLogTestSteps.DataContext = null;
            if (iRow < 0)
            {
                return;
            }

            mCurrentlyDisplayedLog = GetLogName(iRow);
            if (File.Exists(mCurrentlyDisplayedLog))
            {
                DlkTest currentTest = new DlkTest(mCurrentlyDisplayedLog);
                // update parameter count
                foreach (DlkTestStepRecord step in currentTest.mTestSteps)
                {
                    step.mCurrentInstance = currentTest.mTestInstanceExecuted;
                }

                DlkTestStepRecord setup = new DlkTestStepRecord
                {
                    //mStepNumber = 0,
                    mExecute = "",
                    mScreen = "Test Setup",
                    mControl = "",
                    mKeyword = "",
                    mStepDelay = 0,
                    mStepStatus = "",
                    mStepLogMessages = currentTest.mTestSetupLogMessages,
                    mStepStart = new DateTime(),
                    mStepEnd = new DateTime(),
                    mStepElapsedTime = "",
                    mParameters = new List<string>()
                };

                setup.mStepStatus = setup.mStepLogMessages.FindAll(x => x.mMessageType.ToLower().Contains("exception")).Count > 0 ? "failed" : "passed";

                for (int idx = 0; idx < currentTest.mInstanceCount; idx++)
                {
                    setup.mParameters.Add("");
                }
                currentTest.mTestSteps.Insert(0, setup);

                if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct)
                {
                    MaskedSuiteTestLog(currentTest.mTestSteps);
                }

                if (DlkEnvironment.IsShowAppNameProduct)
                {
                    ConvertTestStepScreens(currentTest.mTestSteps);
                }

                dgLogTestSteps.DataContext = currentTest.mTestSteps;
                dgLogTestSteps.Items.Refresh();
            }
            else
            {
                mCurrentlyDisplayedLog = "";
            }
        }

        /// <summary>
        /// Convert loaded test steps screen if show app name is enabled
        /// </summary>
        /// <param name="steps">list of steps</param>
        private void ConvertTestStepScreens(List<DlkTestStepRecord> steps)
        {
            bool convertToAlias = DlkEnvironment.IsShowAppNameEnabled();
            foreach (DlkTestStepRecord step in steps)
            {
                DlkDynamicObjectStoreHandler osInstance = DlkDynamicObjectStoreHandler.Instance;
                if (step.mScreen == "Test Setup")
                    continue;

                int index = convertToAlias ? osInstance.Screens.IndexOf(step.mScreen) : //Get appid index if convertToAlias is true
                                         osInstance.Alias.IndexOf(step.mScreen);    //Get alias index if convertToAlias is false
                if (index != -1)
                    step.mScreen = convertToAlias ? osInstance.Alias[index] :       //change mScreen to alias
                                                    osInstance.Screens[index];      //change mscreen to appid
            }
        }

        /// <summary>
        /// Masked parameters display in MainWindow log
        /// </summary>
        /// <param name="steps">list of steps</param>
        private void MaskedSuiteTestLog(List<DlkTestStepRecord> steps)
        {
            foreach (DlkTestStepRecord step in steps)
            {
                if (step.mPasswordParameters != null)
                {
                    for (int index = 0; index < step.mParameters.Count(); index++)
                    {
                        string[] arrParameters = step.mParameters[index].Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                        for (int i = 0; i < arrParameters.Count(); i++)
                        {
                            if (DlkPasswordMaskedRecord.IsMaskedParameter(step, i) && !DlkData.IsDataDrivenParam(step.mPasswordParameters[i]))
                            {
                                string maskedText = "";
                                foreach (var item in arrParameters[i])
                                    maskedText += DlkPasswordMaskedRecord.PasswordChar;

                                arrParameters[i] = !String.IsNullOrWhiteSpace(maskedText) ? maskedText: DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE;
                            }
                        }
                        step.mParameters[index] = string.Join(DlkTestStepRecord.globalParamDelimiter, arrParameters);
                    }
                }
            }
        }

        private void ExecuteInCommandLineMode()
        {
            LoadSuite(DlkEnvironment.mDirTestSuite + mSuiteToExecute);
            btnRunTestsInQueue_Click(null, null);
        }

        /// <summary>
        /// Load saved suite
        /// </summary>
        /// <param name="SuitePath">Path to target suite to load</param>
        /// <param name="IsSilent">Flag whether to display load success message</param>
        private void LoadSuite(String SuitePath, bool IsSilent = false)
        {
            /* Clear test queue */
            ClearTestsInQueue();

            /* Load test suite info */
            LoadedSuite = SuitePath;
            DlkTestSuiteInfoRecord suiteInfo = DlkTestSuiteXmlHandler.GetTestSuiteInfo(LoadedSuite);
            mTestSuiteInfo.Update(suiteInfo.Browser, suiteInfo.EnvID, suiteInfo.Language, suiteInfo.Tags, suiteInfo.Email, suiteInfo.Description, suiteInfo.mLinks, suiteInfo.Owner, suiteInfo.GlobalVar);
            txtDescription.Text = suiteInfo.Description;
            txtOwner.Text = suiteInfo.Owner;

            /* Load test suite tests */
            List<DlkExecutionQueueRecord> mRecs = DlkTestSuiteXmlHandler.Load(LoadedSuite);
            for (int recordIndex = 0; recordIndex < mRecs.Count; recordIndex++)
            {
                var suiteExecutionQueueRecord = mRecs[recordIndex];
                // reset the row counter of all member test scripts in the suite
                suiteExecutionQueueRecord.testrow = (recordIndex + 1).ToString();
                DlkTest testCopy = new DlkTest(suiteExecutionQueueRecord.fullpath);
                suiteExecutionQueueRecord.instanceRange = string.Format("Value range: 1 - {0}", testCopy.mInstanceCount);
                ExecutionQueueRecords.Add(suiteExecutionQueueRecord);
            }
            /* Prompt to remove missing scripts in UI */
            if (!mCommandLineMode && !IsSilent)
            {
                if (DlkTestSuiteXmlHandler.IsScriptMissing)
                {
                    String mfiles = "";
                    foreach (DlkExecutionQueueRecord eqr in DlkTestSuiteXmlHandler.mMissingRecs)
                    {
                        mfiles += (System.IO.Path.Combine(DlkEnvironment.mDirTests, eqr.folder.Trim('\\'), eqr.file) + " [ Instance: " + eqr.instance + " ]" + "\n");
                        UpdateTestDependencyAfterDataDeletion(mRecs, eqr);
                    }

                    MessageBox.Show("Loaded Suite: " + LoadedSuite + "\n\n" + DlkUserMessages.INF_TEST_SUITE_MISSING_SCRIPTS + mfiles, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (ExecutionQueueRecords.Count == 0)
                    { 
                        MessageBoxResult result = MessageBox.Show(DlkUserMessages.ASK_TEST_SUITE_MISSING_SCRIPTS, "Missing Test Scripts", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                        if (result.Equals(MessageBoxResult.Cancel))
                        {
                            btnClearTestsInQueue_Click(null, null);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Loaded Suite: " + LoadedSuite, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            /* Load results */
            string resultFolder = "";
            string absoluteSuitePath = RemoveSuitePathWhiteSpace(LoadedSuite);
            List<DlkExecutionQueueRecord> mRecsLatestResult = DlkTestSuiteResultsFileHandler.GetLatestSuiteResults(absoluteSuitePath, out resultFolder);
            LoadedResultFolder = resultFolder;
            UpdateExecutionQueueRecords(mRecsLatestResult);

            /* Select first record */
            if (ExecutionQueueRecords.Count > 0)
            {
                TestQueue.SelectedIndex = 0;
            }

            // Update Other UI elements
            Title = mTestRunnerTitle + " : " + LoadedSuite;
            txtExecutionDate.Text = GetLatestResultsFolderDate();
            txtSuitePath.ToolTip = LoadedSuite;
            ToolTipService.SetIsEnabled(txtSuitePath, !String.IsNullOrEmpty(LoadedSuite));
        }

        /// <summary>
        /// Removes white spaces of suite path to avoid invalid path name
        /// </summary>
        /// <param name="SuitePath">Current suite path to remove white spaces</param>
        private string RemoveSuitePathWhiteSpace(string SuitePath)
        {
            string xml_Extension = ".xml";
            string currentSuitePath = SuitePath.Replace(xml_Extension, "");
            string newSuitePath = currentSuitePath.Trim();

            /*Check if current suite path contains trailing white spaces*/
            if (!currentSuitePath.Equals(newSuitePath))
            {
                return newSuitePath + xml_Extension;
            }
            else
            {
                return SuitePath;
            }
        }

        private string GetLatestResultsFolderDate()
        {
            string execDate = string.Empty;
            if (!string.IsNullOrEmpty(LoadedResultFolder))
            {
                DirectoryInfo di = new DirectoryInfo(LoadedResultFolder);
                execDate = di.Name.Insert(4, "-");
                execDate = execDate.Insert(7, "-");
                execDate = execDate.Insert(10, " ");
                execDate = execDate.Insert(13, ":");
                execDate = execDate.Insert(16, ":");
            }
            return execDate;
        }

        private void ClearTestQueueResults()
        {
            for (int i = 0; i < ExecutionQueueRecords.Count; i++)
            {
                DlkExecutionQueueRecord mRec = ExecutionQueueRecords[i];

                DlkTest test = new DlkTest(mRec.fullpath);
                test.mTestInstanceExecuted = Convert.ToInt32(mRec.instance);
                DlkData.SubstituteExecuteDataVariables(test);
                mRec.executedsteps = "0/" + test.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count;

                mRec.teststatus = "Not Run";
                mRec.duration = "";
                mRec.executiondate = "";
                ExecutionQueueRecords.Insert(i, mRec);
                ExecutionQueueRecords.RemoveAt(i + 1);
                TestQueue.SelectedIndex = i;
            }
            txtExecutionDate.Text = "";
            LoadedResultFolder = "";

            //Clear contents regardless of saved suite or not
            dgLogTestSteps.DataContext = null;
            dgTestHistory.DataContext = null;
            mCurrentlyDisplayedLog = "";
            imgError.Visibility = Visibility.Hidden;

            mLoadedResultsFileManifest.Clear();
            //UpdateTestExecutionDetails();
        }

        private Dictionary<string, string> mLoadedResultsFileManifest = new Dictionary<string, string>();

        private void UpdateExecutionQueueRecords(List<DlkExecutionQueueRecord> res)
        {
            mLoadedResultsFileManifest.Clear();

            for (int idx = 0; idx < ExecutionQueueRecords.Count; idx++)
            {
                // match test and result based on ID
                if (res.FindAll(x => x.identifier == ExecutionQueueRecords[idx].identifier).Count > 0)
                {
                    DlkExecutionQueueRecord hit = res.Find(x => x.identifier == ExecutionQueueRecords[idx].identifier);
                    ExecutionQueueRecords[idx].executedsteps = hit.executedsteps;
                    ExecutionQueueRecords[idx].teststatus = hit.teststatus;
                    ExecutionQueueRecords[idx].duration = hit.duration;

                    /* update results manifest */
                    mLoadedResultsFileManifest.Add(ExecutionQueueRecords[idx].identifier, hit.file);
                }
            }
            TestQueue.Items.Refresh();
        }

        //private void UpdateCurrentExecutionQueueRecords(List<DlkExecutionQueueRecord> res)
        //{

        //    // update status and duration
        //    for (int idx = 0; idx < res.Count; idx++)
        //    {
        //        if (ExecutionQueueRecords[idx].name == res[idx].name &&
        //            ExecutionQueueRecords[idx].instance == res[idx].instance)
        //        {
        //            ExecutionQueueRecords[idx].teststatus = res[idx].teststatus;
        //            ExecutionQueueRecords[idx].duration = res[idx].duration;
        //            ExecutionQueueRecords[idx].description = res[idx].description;
        //        }
        //    }
        //    TestQueue.Items.Refresh();
        //}

        private void UpdateTestExecutionDetails()
        {
            DisplayLogResults(TestQueue.SelectedIndex);
            DisplayScreenshot();
            LoadTestHistory();
        }

        private void UpdateButtonStates()
        {
            mIsTestQueueRunning = IsTestQueueRunning();
            btnMoveTestUpInQueue.IsEnabled = ExecutionQueueRecords.Count > 1 && TestQueue.SelectedIndex != 0 && !mIsTestQueueRunning;
            btnMoveTestDownInQueue.IsEnabled = ExecutionQueueRecords.Count > 1 && TestQueue.SelectedIndex != ExecutionQueueRecords.Count - 1 && !mIsTestQueueRunning;
            btnEditTestInQueue.IsEnabled = ExecutionQueueRecords.Count > 0 && !mIsTestQueueRunning;
            btnRemoveTestInQueue.IsEnabled = ExecutionQueueRecords.Count > 0 && !mIsTestQueueRunning;
            btnClearTestsInQueue.IsEnabled = ExecutionQueueRecords.Count > 0 && !mIsTestQueueRunning;
            btnRunTestsInQueue.IsEnabled = ExecutionQueueRecords.Count > 0 && !mIsTestQueueRunning;
            btnOpenTestSuiteToQueue.IsEnabled = EnvironmentIDs.Any() && !mIsTestQueueRunning;
            ContextMenuService.SetIsEnabled(TestQueue, false);
            UpdateCopyPasteTestButtonStates();
            UpdateTestQueueingState();
            btnResultsOptions.IsEnabled = !string.IsNullOrEmpty(mCurrentlyDisplayedLog) && !mIsQueueUpdated;
        }

        private void UpdateButtonStatesMultiSelect()
        {
            if(TestQueue.SelectedItems.Count > 1)
            {
                //Validation for move up and down buttons
                bool hasFirstStep = false;
                bool hasLastStep = false;

                var selectedItems = TestQueue.SelectedItems;
                List<int> selectedItemsIndex = new List<int>();

                //Check if selected items contains first or last step
                List<int> stepIndex = new List<int>();
                foreach (DlkExecutionQueueRecord deq in selectedItems)
                {
                    int currentStep = Convert.ToInt32(deq.testrow);
                    if (currentStep == 1)
                    {
                        hasFirstStep = true;
                    }
                    if(currentStep == ExecutionQueueRecords.Count)
                    {
                        hasLastStep = true;
                    }
                    stepIndex.Add(currentStep);
                }

                //Check if selection is contigous
                stepIndex.Sort();
                bool isSelectedStepContiguous = !stepIndex.Select((i, j) => i - j).Distinct().Skip(1).Any();

                btnMoveTestUpInQueue.IsEnabled = !hasFirstStep & isSelectedStepContiguous;
                btnMoveTestDownInQueue.IsEnabled = !hasLastStep & isSelectedStepContiguous;

                ContextMenuService.SetIsEnabled(TestQueue, true);
                btnEditTestInQueue.IsEnabled = false;
                UpdateCopyPasteTestButtonStates();
            }
            else
            {
                UpdateButtonStates();
            }
        }

        private void UpdateTestQueueingState()
        {
            btnKwQueue.IsEnabled = mTreeItemSelectionList.Count > 0 && !mIsTestQueueRunning;
            TestQueue.IsEnabled = !mIsTestQueueRunning;
        }

        private bool IsTestQueueRunning()
        {
            return mExecutionBW != null && mExecutionBW.IsBusy; //returns state if tests queued are running
        }

        private void AddToExecutionQueue(object ItemToAdd)
        {
            int iRow = (mIsInsert && ExecutionQueueRecords.Count > 0) ? TestQueue.SelectedIndex : ExecutionQueueRecords.Count + 1;
            string defaultBrowser = DlkEnvironment.GetDefaultBrowserNameOrIndex(returnName: true);
            defaultBrowser = defaultBrowser == "" ? ((DlkBrowser)AllBrowsers.GetItemAt(0)).Name : defaultBrowser;

            if (ItemToAdd.GetType() == typeof(KwFile))
            {
                KwFile kwScript = (KwFile)ItemToAdd;
                DlkTest test = new DlkTest(kwScript.Path);

                if (mIsInsert && ExecutionQueueRecords.Count > 0)  //Do not perform insert if Test Queue is empty. Perform normal queue operation instead
                {
                    // during insert, start with the highest instance to retain order
                    for (int idx = test.mInstanceCount; idx >= 1; idx--)
                    {
                        DlkTest testCopy = new DlkTest(kwScript.Path);
                        testCopy.mTestInstanceExecuted = idx;
                        DlkData.SubstituteExecuteDataVariables(testCopy);

                        DlkExecutionQueueRecord record = new DlkExecutionQueueRecord(
                            Guid.NewGuid().ToString(),
                            iRow.ToString(),
                            "0/" + testCopy.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count,
                            "Not Run",
                             Path.GetDirectoryName(test.mTestPath).Replace(Path.GetDirectoryName(DlkEnvironment.mDirTests), ""),
                             test.mTestName,
                             test.mTestDescription.Trim(),
                             Path.GetFileName(test.mTestPath),
                             idx.ToString(),
                             EnvironmentIDs.First(),
                             defaultBrowser,
                             "false",
                             "",
                             "",
                             "True",
                             "false",
                             "",
                             "",
                             string.Format("Value range: 1 - {0}", test.mInstanceCount),
                             test.mTestSteps.Count.ToString());

                        AddExecutionQueueRecord(iRow, record, true);                        
                    }
                }
                else
                {
                    for (int idx = 1; idx <= test.mInstanceCount; idx++)
                    {
                        DlkTest testCopy = new DlkTest(kwScript.Path);
                        testCopy.mTestInstanceExecuted = idx;
                        DlkData.SubstituteExecuteDataVariables(testCopy);

                        DlkExecutionQueueRecord record = new DlkExecutionQueueRecord(
                            Guid.NewGuid().ToString(),
                            iRow.ToString(),
                            "0/" + testCopy.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count,
                            "Not Run",
                             Path.GetDirectoryName(test.mTestPath).Replace(Path.GetDirectoryName(DlkEnvironment.mDirTests), ""),
                             test.mTestName,
                             test.mTestDescription.Trim(),
                             Path.GetFileName(test.mTestPath),
                             idx.ToString(),
                             EnvironmentIDs.First(),
                             defaultBrowser,
                             "false",
                             "",
                             "",
                             "True",
                             "false",
                             "",
                             "",
                             string.Format("Value range: 1 - {0}", test.mInstanceCount),
                             test.mTestSteps.Count.ToString());

                        AddExecutionQueueRecord(iRow, record, false);

                        iRow++;
                    }
                }                
                if (LoadedSuite != "")
                {
                    isChanged = true;
                }
            }
            else if (ItemToAdd.GetType() == typeof(KwFolder))
            {
                QueueFolder(ItemToAdd, true, defaultBrowser);
            }
            else if (ItemToAdd.GetType() == typeof(KwInstance))
            {
                KwInstance kwScript = (KwInstance)ItemToAdd;
                string path = kwScript.Path.Split('|').First();
                string instance = kwScript.Path.Split('|').Last();
                DlkTest test = new DlkTest(path);
                testSuiteInstanceCount = test.mInstanceCount;
                test.mTestInstanceExecuted = Convert.ToInt32(instance);
                DlkData.SubstituteExecuteDataVariables(test);

                DlkExecutionQueueRecord record = new DlkExecutionQueueRecord(
                    Guid.NewGuid().ToString(), 
                    iRow.ToString(),
                    "0/" + test.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count, 
                    "Not Run",
                    Path.GetDirectoryName(test.mTestPath).Replace(Path.GetDirectoryName(DlkEnvironment.mDirTests), ""),
                    test.mTestName, 
                    test.mTestDescription.Trim(), 
                    Path.GetFileName(test.mTestPath), 
                    instance,
                    EnvironmentIDs.First(),
                    defaultBrowser, 
                    "false", 
                    "", 
                    "", 
                    "True", 
                    "false", 
                    "", 
                    "",
                    string.Format("Value range: 1 - {0}", test.mInstanceCount),
                    test.mTestSteps.Count.ToString());

                AddExecutionQueueRecord(iRow, record, mIsInsert && ExecutionQueueRecords.Count > 0);

                if (LoadedSuite != "")
                {
                    isChanged = true;
                }
            }
        }

        void QueueFolder(object ItemToAdd, bool AskForQueue, string defaultBrowser) // added bool to prevent prompt from repeat displays, type int to retain numbering
        {
            KwFolder folder = (KwFolder)ItemToAdd;
            MessageBoxResult result = MessageBoxResult.No;
            Boolean bInsert = mIsInsert && ExecutionQueueRecords.Count > 0;
            if (folder.DirItems.Count <= 0)
            {
                MessageBox.Show(DlkUserMessages.ERR_NO_TEST_IN_QUEUE_FOLDER, bInsert ? "Insert Folder" : "Queue Folder", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (AskForQueue == true)
            {
                result = MessageBox.Show(string.Format((bInsert ? DlkUserMessages.ASK_FOLDER_INSERT : DlkUserMessages.ASK_FOLDER_QUEUE), folder.Name), bInsert ? "Insert" : "Queue", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No);
            }
            if (result == MessageBoxResult.Yes || AskForQueue == false)
            {                
                List<KwDirItem> DirItems = folder.DirItems.ToList(); //Create copy to avoid rearranging original folder items during insert
                if (bInsert)  //Insert the items from the bottom of the list to retain the order
                {
                    DirItems.Reverse();
                }
                foreach (KwDirItem kwItem in DirItems)
                {
                    if (kwItem is KwFolder) // Repeat function for current subfolder
                    {
                        QueueFolder(kwItem, false, defaultBrowser);
                    }
                    else if (kwItem is KwFile) // Queue all files in target folder
                    {
                        int iRow = bInsert ? TestQueue.SelectedIndex : ExecutionQueueRecords.Count + 1;
                        DlkTest test = new DlkTest(((KwFile)kwItem).Path);
                        if (bInsert)  //Do not perform insert if Test Queue is empty. Perform normal queue operation instead
                        {
                            //during insert, start with the highest instance to retain order                                                       
                            for (int idx = test.mInstanceCount; idx >= 1; idx--)
                            {
                                DlkTest testCopy = new DlkTest(((KwFile)kwItem).Path);
                                testCopy.mTestInstanceExecuted = idx;
                                DlkData.SubstituteExecuteDataVariables(testCopy);

                                DlkExecutionQueueRecord record = new DlkExecutionQueueRecord(
                                    Guid.NewGuid().ToString(),
                                    iRow.ToString(),
                                    "0/" + testCopy.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count,
                                    "Not Run",
                                    Path.GetDirectoryName(test.mTestPath).Replace(Path.GetDirectoryName(DlkEnvironment.mDirTests), ""),
                                    test.mTestName,
                                    test.mTestDescription.Trim(),
                                    Path.GetFileName(test.mTestPath),
                                    idx.ToString(),
                                    EnvironmentIDs.First(),
                                    defaultBrowser,
                                    "false",
                                    "",
                                    "",
                                    "True",
                                    "false",
                                    "",
                                    "",
                                    string.Format("Value range: 1 - {0}", test.mInstanceCount),
                                    test.mTestSteps.Count().ToString());

                                AddExecutionQueueRecord(iRow, record, true);
                            }
                        }
                        else
                        {
                            for (int idx = 1; idx <= test.mInstanceCount; idx++)
                            {
                                DlkTest testCopy = new DlkTest(((KwFile)kwItem).Path);
                                testCopy.mTestInstanceExecuted = idx;
                                DlkData.SubstituteExecuteDataVariables(testCopy);

                                try
                                {
                                    DlkExecutionQueueRecord record = new DlkExecutionQueueRecord(
                                        Guid.NewGuid().ToString(),
                                        iRow.ToString(),
                                        "0/" + testCopy.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count,
                                        "Not Run",
                                        Path.GetDirectoryName(test.mTestPath).Replace(Path.GetDirectoryName(DlkEnvironment.mDirTests), ""),
                                        test.mTestName,
                                        test.mTestDescription.Trim(),
                                        Path.GetFileName(test.mTestPath),
                                        idx.ToString(),
                                        EnvironmentIDs.First(),
                                        defaultBrowser,
                                        "false",
                                        "",
                                        "",
                                        "True",
                                        "false",
                                        "",
                                        "",
                                        string.Format("Value range: 1 - {0}", test.mInstanceCount),
                                        test.mTestSteps.Count().ToString());

                                    AddExecutionQueueRecord(iRow, record, false);
                                    iRow++;
                                }
                                catch
                                {
                                    DlkLogger.LogToFile($"{Path.GetFileName(test.mTestPath)} is malformed or does not exist");
                                }
                            }
                        }                        
                    }
                }

                if (LoadedSuite != "")
                {
                    isChanged = true;
                }
            }
        }


        /// <summary>
        /// Adds or Inserts the execution queue record into the test queue
        /// </summary>
        private void AddExecutionQueueRecord(int Row, DlkExecutionQueueRecord Record, Boolean IsInsert)
        {
            if (IsInsert)
            {
                if (ResolveDependencyImpact(false))
                {
                    // get the currently selected record
                    DlkExecutionQueueRecord mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[Row];

                    // insert new record
                    ExecutionQueueRecords.Insert(Row, Record);

                    // renumber the tests
                    for (int i = Row; i < ExecutionQueueRecords.Count; i++)
                    {
                        mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[i];
                        mRowRec.testrow = (i + 1).ToString();
                        ExecutionQueueRecords.RemoveAt(i);
                        ExecutionQueueRecords.Insert(i, mRowRec);
                    }

                    //select the newly inserted record
                    TestQueue.SelectedIndex = Row;
                    if(Row > 0) { TestQueue.ScrollIntoView(ExecutionQueueRecords[Row - 1]); }                        
                }
            }
            else
            {
                ExecutionQueueRecords.Add(Record);

                //select the newly queued record
                TestQueue.SelectedIndex = ExecutionQueueRecords.Count - 1;
                TestQueue.ScrollIntoView(ExecutionQueueRecords[ExecutionQueueRecords.Count - 1]);
            }
        }

        /// <summary>
        /// Displays the passed in error; uses "Error" as the caption. OK button and Error image are used.
        /// </summary>
        /// <param name="ErrorMsg"></param>
        private void ShowError(String ErrorMsg)
        {
            ShowError(ErrorMsg, "Error");
        }

        /// <summary>
        /// Displays the passed in error with the supplied caption. OK button and Error image are used.
        /// </summary>
        /// <param name="ErrorMsg"></param>
        private void ShowError(String ErrorMsg, String ErrorCaption)
        {
            MessageBox.Show(ErrorMsg, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ClearTestsInQueue()
        {
            //remove tests from queue
            while (true)
            {
                if (ExecutionQueueRecords.Count > 0)
                {
                    ExecutionQueueRecords.RemoveAt(0);
                    continue;
                }
                break;
            }

            mLoadedResultsFileManifest.Clear();
            tabLog.IsSelected = true;

            //clear suite info panel
            txtDescription.Text = string.Empty;
            txtSuitePath.Text = string.Empty;
            txtSuitePath.ToolTip = string.Empty;
            txtExecutionDate.Text = string.Empty;
            txtOwner.Text = string.Empty;
            ToolTipService.SetIsEnabled(txtSuitePath, false);
        }
        
        /// <summary>
        /// Find and display the screen capture
        /// </summary>
        private void DisplayScreenshot()
        {
            String mErrorPath = GetErrorScreenShot();
            if (mErrorPath == "")
            {
                imgError.Source = new BitmapImage();
                imgError.Visibility = Visibility.Hidden;
                btnViewErrorScreenshot.Visibility = Visibility.Hidden;
            }
            else
            {
                imgError.Source = new BitmapImage(new Uri(mErrorPath));
                imgError.Visibility = Visibility.Visible;
                btnViewErrorScreenshot.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Gets the screenshot path or returns ""
        /// </summary>
        /// <returns></returns>
        private String GetErrorScreenShot()
        {
            String ret = "";

            try
            {
                if (TestQueue.SelectedIndex < 0)
                {
                    return "";
                }
                String mLog = GetLogName(TestQueue.SelectedIndex);

                if (File.Exists(mLog))
                {
                    DlkTest currentTest = new DlkTest(mLog);
                    int targetIndex = currentTest.mTestFailedAtStep - 1;
                    if (targetIndex >= 0)
                    {
                        foreach (DlkLoggerRecord log in currentTest.mTestSteps[targetIndex].mStepLogMessages)
                        {
                            if (log.mMessageType == "EXCEPTIONIMG")
                            {
                                string myLog = log.mMessageDetails.Substring(log.mMessageDetails.IndexOf(' ') + 1);
                                ret = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(mLog), System.IO.Path.GetFileName(myLog));
                                break;
                            }
                        }
                    }
                    else // check if it is SETUP
                    {
                        foreach (DlkLoggerRecord log in currentTest.mTestSetupLogMessages)
                        {
                            if (log.mMessageType == "EXCEPTIONIMG")
                            {
                                string myLog = log.mMessageDetails.Substring(log.mMessageDetails.IndexOf(' ') + 1);
                                ret = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(mLog), System.IO.Path.GetFileName(myLog));
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_NO_IMAGE_TO_DISPLAY, e);
            }

            return ret;

        }

        private void LoadTestHistory()
        {
            dgTestHistory.DataContext = null;

            // if no suite is loaded.... then we have no history to view
            if ((txtSuitePath.Text == "") || (txtSuitePath.Text == null))
            {
                return;
            }
            if (TestQueue.SelectedIndex < 0)
            {
                return;
            }

            mCurrentTestHistory = new DlkTestHistory(txtSuitePath.Text, (DlkExecutionQueueRecord)TestQueue.SelectedItem);
            mHistoryBW.RunWorkerAsync();
        }
        private void EnableTestTreeHelperControls(bool IsEnabled)
        {
            btnTestDirSettings.IsEnabled = IsEnabled;
            btnSyncSelected.IsEnabled = IsEnabled;
            //btnSyncDir.IsEnabled = IsEnabled;
            btnSearchTest.IsEnabled = IsEnabled;
            btnFilter.IsEnabled = IsEnabled;
            txtSearch.IsEnabled = IsEnabled;
        }
        private List<DataGridCell> GetLogCellsFromRow(DataGridRow dgr)
        {
            List<DataGridCell> lst = new List<DataGridCell>();
            if (dgr != null)
            {
                this.GetVisualChildren<DataGridCell>(dgr, lst);
            }
            return lst;
        }
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

        private DataGridCell GetCell(DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    TestQueue.ScrollIntoView(row, TestQueue.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        

        private void InitializeFileCopyWorker()
        {
            mCopyTestFilesBW = new BackgroundWorker();
            mCopyTestFilesBW.DoWork += mCopyTestFilesBW_DoWork;
            mCopyTestFilesBW.RunWorkerCompleted += mCopyTestFilesBW_RunWorkerCompleted;

            mCopySuiteFilesBW = new BackgroundWorker();
            mCopySuiteFilesBW.DoWork += mCopySuiteFilesBW_DoWork;
            mCopySuiteFilesBW.RunWorkerCompleted += mCopySuiteFilesBW_RunWorkerCompleted;
        }

        private void InitializeSourceControl()
        {
            mSyncBW = new BackgroundWorker();
            mSyncBW.DoWork += mSyncBW_DoWork;
            mSyncBW.RunWorkerCompleted += mSyncBW_RunWorkerCompleted;

            mCheckInBW = new BackgroundWorker();
            mCheckInBW.DoWork += mCheckInBW_DoWork;
            mCheckInBW.RunWorkerCompleted += mCheckInBW_RunWorkerCompleted;

            // initial visibility of sync buttons
            //btnSyncDir.Visibility = DlkSourceControlHandler.SourceControlEnabled ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            //btnSyncSelected.Visibility = btnSyncDir.Visibility;
        }
        private void SyncStarted()
        {
            EnableTestTreeHelperControls(false);
            UpdateStatusBar("Syncing " + mPathToSync);
        }
        private void SyncCompleted()
        {
            EnableTestTreeHelperControls(true);
            RefreshTestTree();
            UpdateStatusBar();
        }
        private void UpdateStatusBar(string Status = "")
        {
            if (string.IsNullOrEmpty(Status))
            {
                StatusText = STATUS_TEXT_READY;

            }
            else
            {
                StatusText = Status;
            }
        }
        private void CheckInConfigFilesStarted()
        {
            btnSyncSelected.IsEnabled = false;
            //btnSyncDir.IsEnabled = false;
            UpdateStatusBar(STATUS_TEXT_CHECKIN_CHANGES_TO_CONFIG);
            mIsCheckingIn = true;
        }

        private void CheckInConfigFilesDone()
        {
            btnSyncSelected.IsEnabled = true;
            //btnSyncDir.IsEnabled = true;
            UpdateStatusBar();
            mIsCheckingIn = false;
        }
        private bool AddToSelectionList(TreeViewItem ItemToAdd, KwDirItem Test)
        {
            bool ret = false;
            if (!mTreeItemSelectionList.Keys.Contains(ItemToAdd))
            {
                mTreeItemSelectionList.Add(ItemToAdd, Test);
                /* style item to simulate selected item look */
                ItemToAdd.Background = Brushes.DodgerBlue;
                ItemToAdd.Foreground = Brushes.White;
                ret = true;
            }
            else /* remove if already there */
            {
                if (mTreeItemSelectionList.Count == 1)
                {
                    ret = true;
                }
                else
                {
                    mTreeItemSelectionList.Remove(ItemToAdd);
                    /* style item to simulate selected item look */
                    ItemToAdd.Background = Brushes.White;
                    ItemToAdd.Foreground = Brushes.Black;
                }
            }
            btnKwTestEditor.IsEnabled = mTreeItemSelectionList.Count == 1 && (mTreeItemSelectionList.First().Value.GetType() == typeof(KwFile)
                || mTreeItemSelectionList.First().Value.GetType() == typeof(KwInstance));

            UpdateTestQueueingState();
            btnSyncSelected.IsEnabled = mTreeItemSelectionList.Count == 1;
            return ret;
        }
        private void ClearSelectionList()
        {
            foreach (KeyValuePair<TreeViewItem, KwDirItem> kvp in mTreeItemSelectionList)
            {
                /* revert styling of every item in list */
                kvp.Key.Background = Brushes.White;
                kvp.Key.Foreground = Brushes.Black;
            }
            mTreeItemSelectionList.Clear();
        }

        /// <summary>
        /// Resolves dependency conditions affected by queue re-order
        /// </summary>
        /// <param name="IsUp">True if invoked by 'Up', False if  by 'Down'</param>
        /// <returns></returns>
        private bool ResolveDependencyImpact(bool IsUp, bool IsMultiselect = false, int selectedIndex = 0)
        {
            bool ret = true;

            DlkExecutionQueueRecord currEqr = null;

            // Return false if dependency was previously cancelled
            if (mAskDependencyCancelled)
            {
                return ret = false;
            }

            if (IsMultiselect)
            {
                // Multiple selection item
                currEqr = TestQueue.Items[selectedIndex] as DlkExecutionQueueRecord;
            }
            else
            {
                //Single selection item
                currEqr = TestQueue.SelectedItem as DlkExecutionQueueRecord;
            }

            int destinationRowNum = int.Parse(currEqr.testrow) + (IsUp ? -1 : 1);

            if (IsUp) /* Up re-order might invalidate current test execution condition */
            {
                if (bool.Parse(currEqr.dependent))
                {
                    if (int.Parse(currEqr.executedependencytestrow) == destinationRowNum)
                    {
                        if (!mAskDependencyOnce)
                        {
                            ret = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_REMOVE_CURRENT_TEST_DEPENDENCY)
                            == MessageBoxResult.Yes;

                            if (ret)
                            {
                                mRemoveDependency = true; // Set to true if confirmed yes
                            }
                            else
                            {
                                mAskDependencyCancelled = true; // Set to true if warning dialog was cancelled
                            }

                            mAskDependencyOnce = true; // Set to true if warning dialog has appeared
                        }

                        if (mRemoveDependency)
                        {
                            currEqr.dependent = "False";
                            currEqr.executedependency = null;
                            currEqr.executedependencyresult = string.Empty;
                            currEqr.executedependencytestrow = string.Empty;
                        }
                    }
                }
            }
            else /* Down re-order might invalidate execution condition of other tests down the queue */
            {
                foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords.ToList().FindAll(x => x.dependent.ToLower() == "true"
                    && int.Parse(x.executedependencytestrow) == int.Parse(currEqr.testrow)))
                {
                    if (int.Parse(eqr.testrow) == destinationRowNum)
                    {
                        if (!mAskDependencyOnce)
                        {
                            ret = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_REMOVE_TEST_DEPENDENCY + "\n\nTest #" + eqr.testrow
                           + " - " + eqr.file + "\n\nThe condition will be removed. Proceed?") == MessageBoxResult.Yes;

                            if (ret)
                            {
                                mRemoveDependency = true; // Set to true if confirmed yes
                            }
                            else
                            {
                                mAskDependencyCancelled = true; // Set to true if warning dialog was cancelled
                            }

                            mAskDependencyOnce = true; // Set to true if warning dialog has appeared
                        }

                        if (mRemoveDependency)
                        {
                            eqr.dependent = "False";
                            eqr.executedependency = null;
                            eqr.executedependencyresult = string.Empty;
                            eqr.executedependencytestrow = string.Empty;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// IsUpdatedTestPartOfSuite
        /// </summary>
        /// <param name="testPath">the full path of test currently open in Test Editor</param>
        /// <returns>Returns True if updated test in Test Editor is part of loaded suite in the Test Queue grid</returns>
        private bool IsUpdatedTestPartOfSuite(string testPath)
        {
            bool ret = false;

            List<DlkExecutionQueueRecord> _eqr = new List<DlkExecutionQueueRecord>();
            foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
            {
                if (testPath == eqr.fullpath)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Save suite to local location
        /// </summary>
        /// <param name="sFilePath">Save path</param>
        /// <returns>TRUE if successfully saved; FALSE otherwise</returns>
        private bool SaveLocalSuite(string sFilePath)
        {
            bool ret = false;
            try
            {
                if (!string.IsNullOrEmpty(sFilePath))
                {
                    if (File.Exists(sFilePath))
                    {
                        FileInfo fi = new FileInfo(sFilePath);
                        fi.IsReadOnly = false;
                    }
                    LoadedSuite = sFilePath;
                    DlkTestSuiteXmlHandler.Save(sFilePath, mTestSuiteInfo, ExecutionQueueRecords.ToList());
                    ret = true;
                }
            }
            catch
            {
                /* Do nothing */
            }
            return ret;
        }

        /// <summary>
        /// Add folder locally 
        /// </summary>
        /// <param name="sFolderPath">Path of folder to add</param>
        /// <returns>TRUE if folder is created; FALSE if not</returns>
        private bool AddLocalFolder(string sFolderPath)
        {
            bool ret = false;
            try
            {
                if (!string.IsNullOrEmpty(sFolderPath))
                {
                    Directory.CreateDirectory(sFolderPath);
                    ret = true;
                }
            }
            catch
            {
                /* Do nothing */
            }
            return ret;
        }

        /// <summary>
        /// Save suite and check-in to TFS after
        /// </summary>
        /// <param name="sFilePath">Save path</param>
        /// <param name="CheckInComments">TFS Check-in comments</param>
        /// <returns>TRUE if successfully saved; FALSE otherwise</returns>
        private bool SaveAndCheckInSuite(string sFilePath, string CheckInComments)
        {
            bool ret = false;
            try
            {
                if (!string.IsNullOrEmpty(sFilePath) && !string.IsNullOrEmpty(CheckInComments))
                {
                    /* Check out THEN write */
                    if (File.Exists(sFilePath))
                    {
                        CheckOutFromSource(new object[] { sFilePath, string.Empty }); /* No effect if already checked-out or not under sc yet */
                    }

                    LoadedSuite = sFilePath;
                    DlkTestSuiteXmlHandler.Save(sFilePath, mTestSuiteInfo, ExecutionQueueRecords.ToList());
                    AddToSource(new object[] { sFilePath, string.Empty }); /* No effect if already under source control */

                    /* Check-in */
                    CheckInToSource(new object[] { sFilePath, "/comment:" + "\"" + CheckInComments + "\"" });
                    ret = true;
                }
            }
            catch
            {
                /* Do nothing */
            }
            return ret;
        }

        /// <summary>
        /// Add folder and check in to server
        /// </summary>
        /// <param name="sFolderPath">Folder path to add and check-0n</param>
        /// <param name="CheckInComments">Check-in comments</param>
        /// <returns>TRUE if successfully added; FALSE otherwise</returns>
        private bool AddAndCheckInFolder(string sFolderPath, string CheckInComments)
        {
            bool ret = false;
            try
            {
                if (!string.IsNullOrEmpty(sFolderPath) && !string.IsNullOrEmpty(CheckInComments))
                {
                    Directory.CreateDirectory(sFolderPath);
                    AddToSource(new object[] { sFolderPath, string.Empty }); /* No effect if already under source control */

                    /* Check-in */
                    CheckInToSource(new object[] { sFolderPath, "/comment:" + "\"" + CheckInComments + "\"" });
                    ret = true;
                }
            }
            catch
            {
                /* Do nothing */
            }
            return ret;
        }

        /// <summary>
        /// Delete old files, then move a set of files to another location in Source Control
        /// </summary>
        /// <param name="sFilePath">Source file path</param>
        /// <param name="sDestination">Destination path</param>
        /// <returns>TRUE if successfully added; FALSE otherwise</returns>
        private bool MoveAndCheckInItem(string sFilePath, string sDestination)
        {
            bool ret = false;
            try
            {
                if (!string.IsNullOrEmpty(sFilePath) && !string.IsNullOrEmpty(sDestination))
                {
                    foreach (string filePath in sFilePath.Split(';'))
                    {
                        string destinationPath = sDestination + Path.GetFileName(filePath);
                        string fileTrdPath = filePath.Replace(".xml", ".trd");
                        string destinationTrdPath = destinationPath.Replace(".xml", ".trd");
                        DlkSourceControlHandler.Delete(destinationPath, "");
                        DlkSourceControlHandler.CheckIn(destinationPath, "/comment:TestRunner-Drag&Drop - Deleted file before moving");
                        DlkSourceControlHandler.Delete(destinationTrdPath, "");
                        DlkSourceControlHandler.CheckIn(destinationTrdPath, "/comment:TestRunner-Drag&Drop - Deleted file before moving");
                        DlkSourceControlHandler.Delete(filePath, "");
                        DlkSourceControlHandler.CheckIn(filePath, "/comment:TestRunner-Drag&Drop - Deleted file before moving");
                        DlkSourceControlHandler.Delete(fileTrdPath, "");
                        DlkSourceControlHandler.CheckIn(fileTrdPath, "/comment:TestRunner-Drag&Drop - Deleted file before moving");
                        AddToSource(new object[] { destinationPath, string.Empty }); /* No effect if already under source control */
                        AddToSource(new object[] { destinationTrdPath, string.Empty }); /* No effect if already under source control */
                        /* Check-in */
                        CheckInToSource(new object[] { destinationPath, "/comment:TestRunner-Drag&Drop - Moved file to this location" });
                        CheckInToSource(new object[] { destinationTrdPath, "/comment:TestRunner-Drag&Drop - Moved file to this location" });
                    }
                    mIsSourceControlDragDropSuccessful = true;
                    ret = true;
                }
            }
            catch
            {
                /* Do nothing */
            }
            return ret;
        }

        /// <summary>
        /// Add file to source control
        /// </summary>
        /// <param name="parameters">Method arguments</param>
        private void AddToSource(object[] parameters)
        {
            DlkSourceControlHandler.Add(parameters.First().ToString(), parameters.Last().ToString());
        }

        /// <summary>
        /// Check-in to source control
        /// </summary>
        /// <param name="parameters">Method arguments</param>
        private void CheckInToSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckIn(parameters.First().ToString(), parameters.Last().ToString());
        }

        /// <summary>
        /// Checkout from source control
        /// </summary>
        /// <param name="parameters">Method arguments</param>
        private void CheckOutFromSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckOut(parameters.First().ToString(), parameters.Last().ToString());
        }


        /// <summary>
        /// Run process via Backgroundworker
        /// </summary>
        /// <param name="actionToPerform">Process to run</param>
        /// <param name="path">Relevant file Path</param>
        private void RunProcessInBackground(ProcessWorkerAction actionToPerform, string path, string source="")
        {
            string sComments = string.Empty;
            StatusText = STATUS_TEXT_SAVING;
            if (actionToPerform == ProcessWorkerAction.SaveAndCheckInSuite
                || actionToPerform == ProcessWorkerAction.SaveAndCheckInSuiteSilent
                || actionToPerform == ProcessWorkerAction.AddAndCheckInTestFolder)
            {
                CheckInCommentsDialog commentDlg = new CheckInCommentsDialog();
                if (commentDlg.ShowDialog() == true)
                {
                    sComments = commentDlg.UserComments;
                }
            }
            IsBackgroundProcessInProgress = true;

            /* set status text */
            string statusTxt = string.Empty;
            switch (actionToPerform)
            {
                case ProcessWorkerAction.SaveSuiteLocal:
                case ProcessWorkerAction.SaveSuiteLocalSilent:
                case ProcessWorkerAction.SaveAndCheckInSuite:
                case ProcessWorkerAction.SaveAndCheckInSuiteSilent:
                case ProcessWorkerAction.AddTestFolderLocal:
                case ProcessWorkerAction.AddAndCheckInTestFolder:
                    statusTxt = STATUS_TEXT_SAVING;
                    break;
                case ProcessWorkerAction.DeleteTestItemLocal:
                case ProcessWorkerAction.DeleteAndCheckInTestItem:
                case ProcessWorkerAction.DeleteMultipleTestItemsLocal:
                case ProcessWorkerAction.DeleteAndCheckInMultipleTestItems:
                    statusTxt = STATUS_TEXT_DELETING;
                    break;
                case ProcessWorkerAction.MoveAndCheckInTest:
                    statusTxt = STATUS_TEXT_MOVING_SOURCE_CONTROL;
                    break;
                default:
                    break;
            }

            UpdateStatusBar(statusTxt);
            mProcessWorker.RunWorkerAsync(new object[] { actionToPerform, path, sComments, source });
        }

        /// <summary>
        /// Set ismodified property of test queue items
        /// </summary>
        /// <param name="isModified">Property value to set</param>
        private void SetTestQueueIsModified(bool isModified)
        {
            //reset queue items ismodified property
            foreach (var item in TestQueue.Items.Cast<DlkExecutionQueueRecord>())
            {
                item.isModified = isModified;
            }

            mIsQueueUpdated = isModified;
        }

        /// <summary>
        /// Move files in Tree View
        /// </summary>
        /// <param name="file">File to move</param>
        /// <param name="modifySourceControl">Flag whether to move files within source control</param>
        /// <param name="destination">Target folder - root path if null</param>
        private string MoveFileInTreeView(KwFile file, bool modifySourceControl, KwFolder destination = null)
        {
            string ret = "";
            string destinationPath = destination == null ? mTestRoot : destination.Path + @"\";
            string newFilePath = destinationPath + file.Name;
            if (newFilePath == file.Path) // same source and destination
            {
                return ret;
            }
            string sourceTrdPath = file.Path.Replace(file.Name, Path.GetFileNameWithoutExtension(newFilePath)) + ".trd";
            string destinationTrdPath = destinationPath + (Path.GetFileNameWithoutExtension(newFilePath)) + ".trd";
            if (File.Exists(newFilePath))
            {
                if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_OVERWRITE_TEST_TREE) == MessageBoxResult.No)
                {
                    return ret;
                }
                File.Delete(newFilePath);
                if (File.Exists(destinationTrdPath))
                {
                    File.Delete(destinationTrdPath);
                }
            }
            File.Move(file.Path, newFilePath);
            if (File.Exists(sourceTrdPath))
            {
                File.Move(sourceTrdPath, destinationTrdPath);
            }
            if (modifySourceControl)
            {
                return file.Path;
            }
            return ret;
        }

        /// <summary>
        /// Get nearest TreeViewItem from interacted element
        /// </summary>
        /// <param name="element">UI Element interacted with</param>
        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }

        /// <summary>
        /// Checks if directory is the root Tests/Suites directory or if it contains or is under the root Tests/Suites directory
        /// </summary>
        /// <param name="importFolderDirectory">Directory to be checked</param>
        /// <param name="error">Error encountered</param>
        private bool CheckFolderForRootTestsOrSuites(string importFolderDirectory, out String error)
        {
            error = "";
            bool contains = false;
            string rootTest = DlkEnvironment.mDirTests.Remove(DlkEnvironment.mDirTests.Length - 1);
            string rootSuite = DlkEnvironment.mDirTestSuite.Remove(DlkEnvironment.mDirTestSuite.Length - 1);

            // check if folder is under root Tests/Suites folder or if directory is the root
            if (importFolderDirectory.Contains(rootTest))
            {
                error = "RootTest";
                return true;
            }
            else if (importFolderDirectory.Contains(rootSuite))
            {
                error = "RootSuite";
                return true;
            }
            else
            {
                List<string> directories = Directory.GetDirectories(importFolderDirectory, "*", SearchOption.AllDirectories).ToList();

                foreach (string dir in directories)
                {
                    if (dir.Equals(rootTest))
                    {
                        error = "RootSuiteAndTest";
                        return true;
                    }
                    if (dir.Equals(rootSuite))
                    {
                        error = "RootSuiteAndTest";
                        return true;
                    }
                }
            }

            return contains;
        }

        /// <summary>
        /// Checks if directory has more than 5 levels deep of nested folders
        /// </summary>
        /// <param name="importFolderDirectory">Directory to be checked</param>
        private bool CheckFolderForMultipleNestedFolders(string importFolderDirectory)
        {
            bool contains = false;
            string parentDirectory = importFolderDirectory.Substring(0, importFolderDirectory.LastIndexOf("\\"));
            List<string> directories = Directory.GetDirectories(importFolderDirectory, "*", SearchOption.AllDirectories).ToList();
            
            foreach (string dir in directories)
            {
                string directory = dir.Substring(parentDirectory.Length + 1);
                int count = directory.Count(x => (x == '\\'));
                if (count > 5)
                {
                    return true;
                }
            }

            return contains;
        }

        /// <summary>
        /// Handles the visibility of the vertical scroll bar of the Test Queue Grid
        /// </summary>
        private void DisplayVerticalScrollBar()
        {
            int minItemCount = 1;
            if (TestQueue.Items.Count > minItemCount)
            {
                TestQueue.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                TestQueue.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }

        /// <summary>
        /// Clear suite results
        /// </summary>
        private void ClearResults()
        {
            try
            {
                tabLog.IsSelected = true;
                ClearTestQueueResults();
                TestQueue.SelectedIndex = 0;
                TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Load suite results
        /// </summary>
        private void LoadResults()
        {
            try
            {
                if (txtSuitePath.Text == "")
                {
                    ShowError("Please open a test suite.");
                    return;
                }
                //tabLog.IsSelected = true;

                LoadResultsDialog lrd = new LoadResultsDialog(System.IO.Path.GetFileName(txtSuitePath.Text));
                lrd.Owner = this;
                if (lrd.ShowDialog() == true)
                {
                    //    DlkEnvironment.CleanFrameworkWorkingResults();
                    //ClearTestsInQueue();
                    List<DlkExecutionQueueRecord> mRecs = DlkTestSuiteResultsFileHandler.GetResults(LoadedSuite, lrd.mSelectedExecutionDate);
                    LoadedResultFolder = lrd.mSelectedLogFolder;

                    // update status and duration
                    UpdateExecutionQueueRecords(mRecs);

                    if (ExecutionQueueRecords.Count > 0)
                    {
                        TestQueue.SelectedIndex = 0;
                    }

                    // Update UI
                    txtExecutionDate.Text = lrd.mSelectedExecutionDate;
                    UpdateTestExecutionDetails();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion

        #region EVENT HANDLERS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Initialize();

                if (mCommandLineMode)
                {
                    ExecuteInCommandLineMode();
                }

                if (DlkTestRunnerSettingsHandler.IsFirstTimeLaunch == true)
                {
                    LoadNewFeaturesDialog();
                    DlkTestRunnerSettingsHandler.IsFirstTimeLaunch = false;
                    DlkTestRunnerSettingsHandler.Save();
                }
                Activate();
                BringIntoView();
                Focus();
                ShowInTaskbar = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event that initialize the checking of items in TestQueue grid
        /// </summary>
        /// <param name="sender"> Object sender</param>
        /// <param name="e">Event arguments</param>
        private void TestQueue_Loaded(object sender, RoutedEventArgs e)
        {
            ExecutionQueueRecords.CollectionChanged += ExecutionQueueRecords_CollectionChanged;
        }

        /// <summary>
        /// Event that trigger when TestQueue grid changes items
        /// </summary>
        /// <param name="sender">Object sender</param>
        /// <param name="e">Event arguments</param>
        private void ExecutionQueueRecords_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ExecutionQueueRecords.Count() > 0)
            {
                txtDescription.IsEnabled = true;
                txtOwner.IsEnabled = true;
            }
            else
            {
                txtDescription.IsEnabled = false;
                txtOwner.IsEnabled = false;
            }
        }

        private void btnKwTestNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkEditorWindowHandler.IsActiveEditorsMaxed()) { return; }

                TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, "");
                te.Show();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnKwTestEditor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                object selectedItem = tvKeywordDirectory.SelectedItem;

                while (ObjectStoreHandler.StillLoading)
                {
                    //do nothing
                }

                if (selectedItem == null)
                {
                    if (DlkEditorWindowHandler.IsActiveEditorsMaxed()) { return; }
                    TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, "");
                    te.Show();
                }
                else if (selectedItem.GetType() == typeof(KwFile))
                {
                    KwFile kwScript = (KwFile)selectedItem;
                    if (!DlkEditorWindowHandler.IsEditorScriptOpened(kwScript.Path))
                    {
                        if (DlkEditorWindowHandler.IsActiveEditorsMaxed()) { return; }
                        TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, kwScript.Path);
                        te.Show();
                    }
                }
                else if (selectedItem.GetType() == typeof(KwInstance))
                {
                    KwInstance kwScript = (KwInstance)selectedItem;
                    string path = kwScript.Path.Split('|').First();
                    int instance = int.Parse(kwScript.Path.Split('|').Last());

                    if (!DlkEditorWindowHandler.IsEditorScriptOpened(path))
                    {
                        if (DlkEditorWindowHandler.IsActiveEditorsMaxed()) { return; }
                        TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, path);
                        te.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }        

        private void TestQueue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (mTestExecutionCompleted || mCurrentSelectedTestInQueue != (DlkExecutionQueueRecord)((DataGrid)sender).SelectedItem)
                {
                    DlkExecuteConverter.SetUpStep = true;
                    mCurrentSelectedTestInQueue = (DlkExecutionQueueRecord)((DataGrid)sender).SelectedItem;
                    UpdateButtonStates();
                    UpdateTestExecutionDetails();
                }
                UpdateButtonStatesMultiSelect();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Sets vertical scroll bar to hidden to avoid unresponsive behaviour when Test Queue has reached its minimum height size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestQueue_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DisplayVerticalScrollBar();
        }

        /// <summary>
        /// Opens row editor edit menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestQueue_EditMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = TestQueue.SelectedItems.Cast<DlkExecutionQueueRecord>().ToList();
                TestQueueRowEditorDialog rowEditorDialog = new TestQueueRowEditorDialog(this, selectedItems, EnvironmentIDs.ToList(), AllBrowsers);
                rowEditorDialog.Owner = this;
                rowEditorDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveTestUpInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // validate that we can move the record up
                if (ExecutionQueueRecords.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_ORDER);
                    return;
                }
                if (TestQueue.SelectedItems.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_IN_QUEUE);
                    return;
                }
                if (TestQueue.SelectedItem is DlkExecutionQueueRecord)
                {
                }
                else
                {
                    ShowError(DlkUserMessages.ERR_NO_SELECTED_TEST_IN_QUEUE);
                    return;
                }

                // Validate and retrieve multiple selected item
                var selectedItems = TestQueue.SelectedItems;
                List<int> selectedItemsIndex = new List<int>();
                bool isMultiselect = selectedItems.Count > 1 ? true : false;
                mAskDependencyOnce = false; // For multiple selection to ask dependency validation once
                mAskDependencyCancelled = false;
                mRemoveDependency = false;

                // Retrieve current index of selected items
                foreach (var selectedItem in selectedItems)
                {
                    selectedItemsIndex.Add(TestQueue.Items.IndexOf(selectedItem));
                }
                selectedItemsIndex.Sort(); // Re-arrange indexes in ascending order

                // Move up each selected item
                foreach (var selectedIndex in selectedItemsIndex)
                {
                    int iIndex = selectedIndex;
                    if (iIndex == 0)
                    {
                        //ShowError("The selected test is already at the top.");
                        return;
                    }

                    /* Check for dependency implications. will return true no dependency impact or user willing to remove affected dependencies*/
                    if (ResolveDependencyImpact(true, isMultiselect, iIndex))
                    {
                        // get the record and change the row #
                        DlkExecutionQueueRecord mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[iIndex];
                        mRowRec.testrow = (iIndex).ToString();

                        // move it
                        ExecutionQueueRecords.RemoveAt(iIndex);
                        ExecutionQueueRecords.Insert(iIndex - 1, mRowRec);

                        // renumber the tests
                        for (int i = iIndex; i < ExecutionQueueRecords.Count; i++)
                        {
                            mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[i];
                            mRowRec.testrow = (i + 1).ToString();
                            ExecutionQueueRecords.RemoveAt(i);
                            ExecutionQueueRecords.Insert(i, mRowRec);
                        }

                        // select the moved row
                        TestQueue.SelectedIndex = iIndex - 1;
                        TestQueue.ScrollIntoView(ExecutionQueueRecords[iIndex - 1]);

                        if (LoadedSuite != "")
                        {
                            isChanged = true;
                            mIsQueueUpdated = true;
                        }
                        ChangeKeepOpenBrowsers();
                    }
                }

                // Select multiple items after update in test queue
                if(selectedItemsIndex.Count > 1 & !mAskDependencyCancelled)
                {
                    foreach(var selectedIndex in selectedItemsIndex)
                    {
                        TestQueue.SelectedItems.Add(TestQueue.Items[selectedIndex - 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveTestDownInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // validate that we can move the record up
                if (ExecutionQueueRecords.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_ORDER);
                    return;
                }
                if (TestQueue.SelectedItems.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_IN_QUEUE);
                    return;
                }
                if (TestQueue.SelectedItem is DlkExecutionQueueRecord)
                {
                }
                else
                {
                    ShowError(DlkUserMessages.ERR_NO_SELECTED_TEST_IN_QUEUE);
                    return;
                }

                // Validate and retrieve multiple selected item
                var selectedItems = TestQueue.SelectedItems;
                List<int> selectedItemsIndex = new List<int>();
                bool isMultiselect = selectedItems.Count > 1 ? true : false;
                mAskDependencyOnce = false; // For multiple selection to ask dependency validation once
                mAskDependencyCancelled = false;
                mRemoveDependency = false;

                // Retrieve current index of selected items
                foreach (var selectedItem in selectedItems)
                {
                    selectedItemsIndex.Add(TestQueue.Items.IndexOf(selectedItem));
                }
                selectedItemsIndex.Sort();
                selectedItemsIndex.Reverse(); // Re-arrange indexes in descending order

                // Move down each selected item
                foreach (var selectedIndex in selectedItemsIndex)
                {
                    int iIndex = selectedIndex;
                    int iLastTest = ExecutionQueueRecords.Count - 1;
                    if (iIndex == iLastTest)
                    {
                        //ShowError("The selected test is already at the bottom.");
                        return;
                    }

                    /* Check for dependency implications. will return true no dependency impact or user willing to remove affected dependencies*/
                    if (ResolveDependencyImpact(false, isMultiselect, iIndex))
                    {
                        // get the record and change the row #
                        DlkExecutionQueueRecord mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[iIndex];

                        // move it
                        ExecutionQueueRecords.RemoveAt(iIndex);
                        ExecutionQueueRecords.Insert(iIndex + 1, mRowRec);

                        // renumber the tests
                        for (int i = iIndex; i < ExecutionQueueRecords.Count; i++)
                        {
                            mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[i];
                            mRowRec.testrow = (i + 1).ToString();
                            ExecutionQueueRecords.RemoveAt(i);
                            ExecutionQueueRecords.Insert(i, mRowRec);
                        }

                        // select the moved row
                        TestQueue.SelectedIndex = iIndex + 1;
                        TestQueue.ScrollIntoView(ExecutionQueueRecords[iIndex + 1]);

                        if (LoadedSuite != "")
                        {
                            isChanged = true;
                            mIsQueueUpdated = true;
                        }
                        ChangeKeepOpenBrowsers();
                    }
                }

                // Select multiple items after update in test queue
                if (selectedItemsIndex.Count > 1 & !mAskDependencyCancelled)
                {
                    foreach (var selectedIndex in selectedItemsIndex)
                    {
                        TestQueue.SelectedItems.Add(TestQueue.Items[selectedIndex + 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRemoveTestInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // validate that we can remove the record
                if (ExecutionQueueRecords.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_REMOVE);
                    return;
                }
                if (TestQueue.SelectedItems.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_IN_QUEUE);
                    return;
                }
                if (TestQueue.SelectedItem is DlkExecutionQueueRecord)
                {
                }
                else
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_IN_QUEUE);
                    return;
                }

                // Validate and retrieve multiple selected item
                var selectedItems = TestQueue.SelectedItems;
                List<int> selectedItemsIndex = new List<int>();
                bool isMultiselect = selectedItems.Count > 1 ? true : false;
                mAskDependencyOnce = false; // For multiple selection to ask dependency validation once

                // Retrieve current index of selected items
                foreach (var selectedItem in selectedItems)
                {
                    selectedItemsIndex.Add(TestQueue.Items.IndexOf(selectedItem));
                }
                selectedItemsIndex.Sort();
                selectedItemsIndex.Reverse(); // Re-arrange indexes in descending order

                // Remove selected items
                foreach (var selectedIndex in selectedItemsIndex)
                {
                    int iIndex = selectedIndex;
                    foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                    {
                        if (eqr.executedependencytestrow == (iIndex + 1).ToString())
                        {
                            eqr.dependent = "False";
                            eqr.executedependencytestrow = string.Empty;
                            eqr.executedependencyresult = string.Empty;
                        }
                    }

                    ExecutionQueueRecords.RemoveAt(iIndex);
                    // renumber the tests
                    for (int i = iIndex; i < ExecutionQueueRecords.Count; i++)
                    {
                        DlkExecutionQueueRecord mRowRec = (DlkExecutionQueueRecord)ExecutionQueueRecords[i];
                        mRowRec.testrow = (i + 1).ToString();
                        ExecutionQueueRecords.RemoveAt(i);
                        ExecutionQueueRecords.Insert(i, mRowRec);
                    }

                    // select the record that is now in that spot
                    if (iIndex < ExecutionQueueRecords.Count)
                    {
                        TestQueue.SelectedIndex = iIndex;
                        TestQueue.ScrollIntoView(ExecutionQueueRecords[iIndex]);
                    }
                    else
                    {
                        // select the record that is now 1 lt that spot
                        if (iIndex - 1 < ExecutionQueueRecords.Count)
                        {
                            TestQueue.SelectedIndex = iIndex - 1;
                            if (TestQueue.SelectedIndex >= 0)
                            {
                                TestQueue.ScrollIntoView(ExecutionQueueRecords[iIndex - 1]);
                            }
                        }
                        else
                        {
                            if (ExecutionQueueRecords.Count > 0)
                            {
                                TestQueue.SelectedIndex = 0;
                                TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);
                            }
                        }
                    }
                    ChangeKeepOpenBrowsers();
                    if (LoadedSuite != "")
                    {
                        isChanged = true;
                        mIsQueueUpdated = true;
                    }
                    if (ExecutionQueueRecords.Count < 1)
                    {
                        mTestSuiteInfo.GlobalVar = "";
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearTestsInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearTestsInQueue();
                Title = mTestRunnerTitle;
                mTestSuiteInfo.GlobalVar = "";
                LoadedSuite = "";
                UpdateButtonStates();
                mIsQueueUpdated = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCopyTestInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TestQueue.SelectedItems.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_IN_QUEUE);
                    return;
                }
                if (btnCopyTestInQueue.IsEnabled)
                {
                    deqrClipboard = new List<DlkExecutionQueueRecord>();
                    foreach (Object queueItem in TestQueue.SelectedItems)
                    {
                        DlkExecutionQueueRecord queueRecord = (DlkExecutionQueueRecord)queueItem;
                        // create temporary copy - needed for environment, browser values to persist
                        deqrClipboard.Add(CreateQueueRecordFromExisting(queueRecord, queueRecord.testrow));
                    }
                    deqrClipboard = deqrClipboard.OrderBy(x => x.testrow.Length).ThenBy(x => x.testrow).ToList();
                    UpdateCopyPasteTestButtonStates();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnPasteTestToQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnPasteTestToQueue.IsEnabled)
                {
                    int selectedIndex = TestQueue.SelectedIndex;
                    bool isLastSelectedTestOrQueueEmpty = (ExecutionQueueRecords.Count == 0 || selectedIndex == ExecutionQueueRecords.Count-1 || (ExecutionQueueRecords.Count == 1 && selectedIndex == 0));
                    int iRow = isLastSelectedTestOrQueueEmpty ? ExecutionQueueRecords.Count+1 : selectedIndex+1;
                    if (deqrClipboard != null && deqrClipboard.Count > 0)
                    {
                        isChanged = true;
                        foreach (DlkExecutionQueueRecord src in deqrClipboard)
                        {
                            AddExecutionQueueRecord(iRow, CreateQueueRecordFromExisting(src, iRow.ToString()), !isLastSelectedTestOrQueueEmpty);
                            iRow++;
                        }
                        ChangeKeepOpenBrowsers();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnOpenTestSuiteToQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTestSuiteInfoRecord tst = new DlkTestSuiteInfoRecord();
                SaveSuiteDialogSC sv = new SaveSuiteDialogSC("", tst, null, true);

                sv.Owner = this;
                if ((bool)sv.ShowDialog() == true)
                {
                    LoadSuite(LoadedSuite);
                    mIsQueueUpdated = false;
                    ChangeKeepOpenBrowsers();
                    //reset queue items ismodified property
                    foreach (var item in TestQueue.Items.Cast<DlkExecutionQueueRecord>())
                    {
                        item.isModified = false;
                    }
                }

                TestQueue.SelectedIndex = 0;
                TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);
                UpdateButtonStates();
                DisplayVerticalScrollBar();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSaveTestsToSuiteFromQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ExecutionQueueRecords.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_SAVE);
                    return;
                }

                //Checking for valid environment and blacklist URL
                for (int i = 0; i < TestQueue.Items.Count; i++)
                {
                    DlkExecutionQueueRecord rec = (DlkExecutionQueueRecord)TestQueue.Items[i];

                    if (!EnvironmentIDs.Contains(rec.environment))
                    {
                        DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_INVALID_ENVIRONMENT_VALUE_SAVE, rec.environment));
                        return;
                    }
                    if (DlkEnvironment.IsURLBlacklist(rec.environment))
                    {
                        DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_URL_BLACKLIST, rec.environment));
                        return;
                    }
                }

                // clean out status and duration before saving
                List<DlkExecutionQueueRecord> mRecs = ExecutionQueueRecords.ToList();
                for (int i = 0; i < mRecs.Count; i++)
                {
                    mRecs[i].teststatus = "Not Run";
                    mRecs[i].duration = "";
                }

                if (TestQueue.Items.Cast<DlkExecutionQueueRecord>().All(x => x.execute.ToLower() == "false"))
                {
                    if (DlkUserMessages.ShowQuestionOkCancelWarning(DlkUserMessages.WRN_SUITE_SAVE_ALL_FALSE) == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                }

                SaveSuiteDialogSC sv = new SaveSuiteDialogSC(LoadedSuite, mTestSuiteInfo, mRecs, false, txtDescription.Text, txtOwner.Text);
                sv.Owner = this;

                bool? bSaveAndCheckIn;
                string sFilePath;
                sv.ShowDialog(out bSaveAndCheckIn, out sFilePath);
                if (bSaveAndCheckIn != null) /* Null if user aborted */
                {
                    RunProcessInBackground(bSaveAndCheckIn == true ? ProcessWorkerAction.SaveAndCheckInSuite 
                        : ProcessWorkerAction.SaveSuiteLocal, sFilePath);
                }

                TestQueue.SelectedIndex = 0;
                TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRunTestsInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Validation */
                if (ExecutionQueueRecords.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_RUN);
                    return;
                }

                /* Check Test Capture session status */
                if (TestCaptureForm != null && TestCaptureForm.IsVisible)
                {
                    mTestCaptureStatus = TestCaptureForm.ViewStatus.ToString();
                    if (mTestCaptureStatus != "Default")
                    {
                        ShowError(DlkUserMessages.ERR_TEST_CAPTURE_ONGOING_RECORDING);
                        return;
                    }
                }

                //Checking for valid environment and blacklist URL
                for (int i = 0; i < TestQueue.Items.Count; i++)
                {
                    DlkExecutionQueueRecord rec = (DlkExecutionQueueRecord)TestQueue.Items[i];

                    if (!EnvironmentIDs.Contains(rec.environment))
                    {
                        DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_INVALID_ENVIRONMENT_VALUE_RUN, rec.environment));
                        return;
                    }
                    if (DlkEnvironment.IsURLBlacklist(rec.environment))
                    {
                        DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_URL_BLACKLIST, rec.environment));
                        return;
                    }
                }

                /* Pre-run UI updates */
                tabLog.IsSelected = true;
                ClearTestQueueResults();
                TestQueue.SelectedIndex = 0;
                TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);

                //check if queue items' properties are modified on a loaded suite
                if ((LoadedSuite != null) && (LoadedSuite != "") &&
                    TestQueue.Items.Cast<DlkExecutionQueueRecord>().Any(x => x.isModified))
                {
                    mIsQueueUpdated = true;
                }

                if (mIsQueueUpdated)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_UNSAVED_SUITE_RUN, "Save Changes?");
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            SaveSuiteDialogSC sv = new SaveSuiteDialogSC(LoadedSuite, mTestSuiteInfo, ExecutionQueueRecords.ToList(), false, txtDescription.Text, txtOwner.Text);
                            sv.Owner = this;
                            bool? bSaveAndCheckIn;
                            string filePath;
                            sv.ShowDialog(out bSaveAndCheckIn, out filePath);
                            if (bSaveAndCheckIn != null)
                            {
                                RunProcessInBackground(bSaveAndCheckIn == true ? ProcessWorkerAction.SaveAndCheckInSuite
                                    : ProcessWorkerAction.SaveSuiteLocal, filePath);
                                while (IsBackgroundProcessInProgress)
                                {
                                    System.Windows.Forms.Application.DoEvents(); /* Just wait without blocking UI */
                                }
                            }
                            break;
                        case MessageBoxResult.No:
                            break;
                        default:
                            break;
                    }
                }

                if (TestQueue.Items.Cast<DlkExecutionQueueRecord>().All(x => x.execute.ToLower() == "false"))
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_SUITE_RUN_ALL_FALSE);
                    return;
                }

                /* Background execution thread preparation */
                DlkTestRunnerApi.mTestsRan = 0; // reset it here. in some machines, the thread that resets this is slower than UI thread
                DlkTestRunnerApi.CurrentRunningTest = ExecutionQueueRecords.First(x => String.Compare(x.execute.ToLower(), "true") == 0).testrow;
                mExecutionBW = new BackgroundWorker();
                mExecutionBW.DoWork += new DoWorkEventHandler(mExecutionBW_DoWork);
                mExecutionBW.ProgressChanged += new ProgressChangedEventHandler(mExecutionBW_ProgressChanged);
                mExecutionBW.WorkerReportsProgress = true;
                mExecutionBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mExecutionBW_RunWorkerCompleted);

                /* Execution dialog */
                mTestExecutionDialog = new TestExecutionDialog(LoadedSuite, true, mExecutionQueueRecords.Count);
                mTestExecutionDialog.Show();

                /* Run tests */
                mExecutionBW.RunWorkerAsync();
                UpdateButtonStates();
                /* Test on-going UI updates */
                if (string.IsNullOrEmpty(mLoadedSuite))
                {
                    UpdateStatusBar(STATUS_TEXT_RUNNING_TESTS_IN_QUEUE);
                }
                else
                {
                    UpdateStatusBar("Running " + mLoadedSuite);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        

        private void tabControlTestResultDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
            //if (tabErrorScreenshot.IsSelected)
            //{
            //    DisplayScreenshot();
            //}
            //else if (tabTestHistory.IsSelected)
            //{
            //    TestHistory();
            //}
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgLogTestSteps_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {

                //if (e.Row.Item is TestStepRecord)
                e.Row.Background = new SolidColorBrush(Colors.White);
                if (e.Row.Item is DlkTestStepRecord)
                {

                }
                else
                {
                    return;
                }


                //SolidColorBrush mStatusBrush = new SolidColorBrush(Colors.LemonChiffon);
                SolidColorBrush mStatusBrush = new SolidColorBrush(Colors.LightGreen);
                SolidColorBrush mRedBrush = new SolidColorBrush(Colors.Tomato);
                SolidColorBrush mSilverBrush = new SolidColorBrush(Colors.Silver);
                SolidColorBrush mSalmonBrush = new SolidColorBrush(Colors.Salmon);

                //TestStepRecord mRowRec = (TestStepRecord)e.Row.DataContext;
                DlkTestStepRecord mRowRec = (DlkTestStepRecord)e.Row.DataContext;
                //foreach (LoggerRecord lRec in mRowRec.logdetails)

                if (mRowRec.mStepNumber == 0)
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
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

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

        private void btnViewErrorScreenshot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String mErrorPath = GetErrorScreenShot();
                if ((mErrorPath == "") || (!File.Exists(mErrorPath)))
                {
                    ShowError(DlkUserMessages.ERR_NO_IMAGE_TO_DISPLAY);
                    return;
                }
                else
                {
                    Process process = new Process();
                    ProcessStartInfo si = new ProcessStartInfo("OpenMspaint.bat");
                    si.Arguments = "\"" + mErrorPath + "\"";
                    si.WindowStyle = ProcessWindowStyle.Hidden;
                    si.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    process.StartInfo = si;
                    process.Start();
                    //DlkEnvironment.OpenMspaint(mErrorPath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnReports_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            //ReportsGenerator mReportGeneratorDialog = new ReportsGenerator();
            //mReportGeneratorDialog.ShowDialog();


            //reportBW = new BackgroundWorker();
            //reportBW.DoWork += new DoWorkEventHandler(reportBW_DoWork);
            //reportBW.ProgressChanged += new ProgressChangedEventHandler(reportBW_ProgressChanged);
            //reportBW.WorkerReportsProgress = true;
            //reportBW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(reportBW_RunWorkerCompleted);

            //reportBW.RunWorkerAsync();
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /*Dashboard is hidden for now*/
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (Convert.ToBoolean(DlkConfigHandler.GetConfigValue("usedatabase")))
            {
                String mUrl = String.Format("http://{0}/TestRunnerDashboard/default.aspx?Product={1}", DlkConfigHandler.GetConfigValue("dbserver"), DlkEnvironment.mProductFolder);
                DlkEnvironment.RunProcess(mUrl, "", "", true, -1);
            }
            else
            {
                GenerateDashboardWindow genDash = new GenerateDashboardWindow();
                genDash.Owner = this;
                genDash.ShowDialog();
                DlkEnvironment.RunProcess(DlkEnvironment.mDirDataFrameworkDashboardRepositoryPublished + "Summary.xml", "", "", true, -1);
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnScheduler_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIsCtxSchedulerOpen) // context menu still open, handle
                {
                    e.Handled = true;
                    return;
                }

                switch (tbKwScheduler.Text)
                {
                    case "Scheduler":
                        
                        if (DlkProcess.IsProcessRunning("scheduler", false))
                        {
                            DlkUserMessages.ShowError("Another instance of the scheduler is currently running.");
                            return;
                        }

                        if (!SchedulerMultiHostv2.IsOpen)
                        {
                            SchedulerMultiHostv2 mSched = new SchedulerMultiHostv2();
                            mSched.Owner = this;
                            mSched.Show();
                        }
                        else
                        {
                            DlkUserMessages.ShowError("Another instance of the scheduler is currently running.");
                            return;
                        }
                        break;

                    case "New Scheduler":

                        if (!SchedulerMultiHostv2.IsOpen)
                        {
                            DlkProcess.RunProcess(mSchedulerFileName, DlkConfigHandler.GetConfigValue("currentapp"), AppDomain.CurrentDomain.BaseDirectory, false, 0);
                        }

                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnKwQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Boolean bInsert = mIsInsert && ExecutionQueueRecords.Count > 0;
                if (mIsCtxQueueOpen) // context menu still open, handle
                { 
                    e.Handled = true;
                    return;
                }

                if (tvKeywordDirectory.SelectedItem == null)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_QUEUE);
                    return;
                }

                if (EnvironmentIDs.Count() == 0)
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, bInsert ? "Insert failed" : "Queue failed");
                    return;
                }

                if (mTreeItemSelectionList.Count > 1)
                {
                    foreach (KeyValuePair<TreeViewItem, KwDirItem> kvp in mTreeItemSelectionList)
                    {
                        AddToExecutionQueue(kvp.Value);
                    }
                    ClearSelectionList();
                    ChangeKeepOpenBrowsers();
                }
                else
                {
                    AddToExecutionQueue(tvKeywordDirectory.SelectedItem);
                    ChangeKeepOpenBrowsers();
                }

                if ((LoadedSuite != null) && (LoadedSuite != ""))
                {
                    mIsQueueUpdated = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            finally
            {
                DisplayVerticalScrollBar();
            }
        }
        
        private void ShowAbout(object sender, RoutedEventArgs e)
        {
            try
            {
                About abt = new About();
                abt.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            try
            {
                SettingsForm m_Settings = new SettingsForm(this);

                // if login config/remote browser files are changed, we need to checkin
                if ((bool)m_Settings.ShowDialog())
                {
                    if (DlkSourceControlHandler.SourceControlEnabled)
                    {
                        if (!mIsCheckingIn)
                        {
                            CheckInConfigFilesStarted();
                            mCheckInBW.RunWorkerAsync();
                        }
                    }
                }

                if(DlkGlobalVariables.mIsSuiteDirChanged || DlkGlobalVariables.mIsTestDirChanged)
                {
                    mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\ProdConfig.xml"));
                    string suitepath = mProdConfigHandler.GetConfigValue("suitepath");
                    string testpath = mProdConfigHandler.GetConfigValue("testpath");

                    DlkUserMessages.ShowInfo(string.Format("Test and/or Suite directories have been changed as follows:\n\n" +
                        "Test Directory - {0} \n\n" +
                        "Suite Directory - {1} \n\n" +
                        "Kindly ensure that you import your tests and suites to the new directories to guarantee successful loading and execution of suites.",
                        testpath,
                        suitepath));
                }

                if (DlkEnvironment.IsShowAppNameProduct)
                {
                    if (LoadedResultFolder != null)
                    {
                        DisplayLogResults(TestQueue.SelectedIndex);
                    }
                }

                if (DlkGlobalVariables.mIsApplicationChanged)
                {
                    UpdateStatusBar(STATUS_TEXT_CHANGING_APPLICATION);
                }

                if (DlkTestRunnerSettingsHandler.NeedsRefresh)
                {
                    LoadingDialog ld = new LoadingDialog(this);
                    ld.ShowDialog();
                    ToggleButtonsVisibility(null, null);
                    DlkTestRunnerSettingsHandler.NeedsRefresh = false;
                    UpdateStatusBar();
                    //Initialize();
                }

                //TestEnvironmentColumn.ItemsSource = EnvironmentIDs;
                OnPropertyChanged("EnvironmentIDs");
                InitializeContextMenus();

                if (!EnvironmentIDs.Any())
                    ClearTestsInQueue();

                UpdateButtonStates();

                // sync folders UI
                //btnSyncDir.Visibility = DlkSourceControlHandler.SourceControlEnabled ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                //btnSyncSelected.Visibility = btnSyncDir.Visibility;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Launches the Diagnostic tool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDiagnostic(object sender, RoutedEventArgs e)
        {
            DiagnosticTest dTest = new DiagnosticTest() { Owner = this };
            dTest.ShowDialog();
        }

        /// <summary>
        /// Launches the Test Runner Help file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowHelpFile(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "DeltekTestRunnerUserGuide.chm");
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            try
            {
            this.Close();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestCapture_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!AllBrowsers.Cast<DlkBrowser>().ToList().Any(x => x.Name == "Firefox" || x.Name == "Chrome"))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_CAPTURE_REQUIRES_FF_OR_CHROME);
                    return;
                }

                if (IsFormOpen<Window>("Test Capture"))
                {
                    if (TestCaptureForm.WindowState == WindowState.Minimized)
                    {
                        TestCaptureForm.WindowState = WindowState.Normal;
                    }
                    TestCaptureForm.Focus();
                    return;
                }

                TestCaptureForm = new TestCapture(this);
                TestCaptureForm.Show();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Check if a selected window if already open
        /// </summary>
        /// <typeparam name="T">Control type</typeparam>
        /// <param name="name">Window name</param>
        /// <returns></returns>
        public bool IsFormOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
                ? Application.Current.Windows.OfType<T>().Any()
                : Application.Current.Windows.OfType<T>().Any(w => w.Title.Equals(name));
        }

        /// <summary>
        /// Display Environment context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowEnvironmentContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
            if (TestQueue.Items.Count > 0)
            {
                TestQueue.CancelEdit();
                ((ContextMenu)FindResource("ctxEnvironment")).IsOpen = true;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display Environment context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowIsOpenContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
            if (TestQueue.Items.Count > 0)
            {
                TestQueue.CancelEdit();
                ((ContextMenu)FindResource("ctxIsOpen")).IsOpen = true;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display browser context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowBrowserContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
            if (TestQueue.Items.Count > 0)
            {
                TestQueue.CancelEdit();
                ((ContextMenu)FindResource("ctxBrowser")).IsOpen = true;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display browser context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDisplayNameContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
            if (TestQueue.Items.Count > 0)
            {
                TestQueue.CancelEdit();
                ((ContextMenu)FindResource("ctxDisplayName")).IsOpen = true;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display Execute context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowExecuteContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
            if (TestQueue.Items.Count > 0)
            {
                TestQueue.CancelEdit();
                ((ContextMenu)FindResource("ctxExecute")).IsOpen = true;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display Queue context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowQueueContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ContextMenu)FindResource("ctxQueueType")).IsOpen = true;
                mIsCtxQueueOpen = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for queue context menu item closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueueContextMenuClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                mIsCtxQueueOpen = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for environment context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnvironmentContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TestQueue.CancelEdit();
                bool environmentChanged = false;
                foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                {
                    string selectedEnvironment = ((MenuItem)sender).Header.ToString();
                    selectedEnvironment = selectedEnvironment.Replace("__", "_");
                    if (!environmentChanged)
                    {
                        environmentChanged = eqr.environment != selectedEnvironment;
                    }
                    eqr.environment = selectedEnvironment;
                }
                TestQueue.Items.Refresh();
                if (environmentChanged)
                {
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display result option menu
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        /// <param name="e"></param>
        private void ShowSuiteResultOptionMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ContextMenu)FindResource("ctxResultOptionType")).IsOpen = true;
                mIsResultOpen = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for scheduler context menu item closed
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        /// <param name="e"></param>
        private void ResultOptionMenuClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                mIsResultOpen = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display Scheduler Context Menu
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        /// <param name="e"></param>
        private void ShowSchedulerContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ContextMenu)FindResource("ctxSchedulerType")).IsOpen = true;
                mIsCtxSchedulerOpen = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for scheduler context menu item closed
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        /// <param name="e"></param>
        private void SchedulerContextMenuClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                mIsCtxSchedulerOpen = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for Execute context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bContinue = true;

                /* Ask user if deleting ALL dependency conditions is OK */
                if (ExecutionQueueRecords.ToList().FindAll(x => x.dependent.ToLower() == "true").Count > 0 
                    && DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_DELETE_ALL_CURRENT_EXEC_CONDITIONS) == MessageBoxResult.No)
                {
                    bContinue = false;
                }

                /* If user OKs deletion of all existing dependency conditions, proceed */
                if (bContinue)
                {
                    TestQueue.CancelEdit();
                    bool executeChanged = false;
                    foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                    {
                        string selectedExecute = ((MenuItem)sender).Header.ToString();
                        if (!executeChanged)
                        {
                            executeChanged = eqr.dependent != "False" || eqr.execute != selectedExecute || eqr.executedependency != null || eqr.executedependencyresult != string.Empty;
                        }
                        eqr.execute = selectedExecute;
                        eqr.dependent = "False";
                        eqr.executedependency = null;
                        eqr.executedependencyresult = string.Empty;
                    }
                    TestQueue.Items.Refresh();
                    if (executeChanged)
                    {
                        isChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for browser context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TestQueue.CancelEdit();
                bool browserChanged = false;
                foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                {
                    string selectedBrowser = ((MenuItem)sender).Header.ToString();
                    selectedBrowser = selectedBrowser.Replace("__", "_");
                    if (!browserChanged)
                    {
                        browserChanged = eqr.Browser.Name != selectedBrowser || browserChanged;
                    }
                    eqr.Browser = new DlkBrowser(selectedBrowser);
                }
                TestQueue.Items.Refresh();
                if (browserChanged)
                {
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for keep open context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IsOpenContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TestQueue.CancelEdit();
                bool keepOpenChanged = false;
                bool isOpen = ((MenuItem)sender).Header.ToString().ToLower().Contains("open");
                int recordCount = 1;
                foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                {
                    
                    string keepOpenValue = isOpen ? "true" : "false";
                    if (!keepOpenChanged)
                    {
                        keepOpenChanged = recordCount == ExecutionQueueRecords.Count ? (eqr.keepopen != "false" || keepOpenChanged) : (eqr.keepopen != keepOpenValue || keepOpenChanged);
                    }
                    eqr.keepopen = keepOpenValue;
                    recordCount++;
                }
                ExecutionQueueRecords.Last().keepopen = "false";
                TestQueue.Items.Refresh();
                if (keepOpenChanged)
                {
                    isChanged = true;
                }
                if (isOpen)
                {
                    DlkUserMessages.ShowInfo(DlkUserMessages.INF_LAST_TEST_IN_SUITE_WILL_CLOSE_BROWSER, "Keep Open");
                }
                ChangeKeepOpenBrowsers();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for display name context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayNameContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                TestQueue.CancelEdit();
                foreach (MenuItem itm in ((ContextMenu)FindResource("ctxDisplayName")).Items)
                {
                    itm.IsChecked = false;
                }
                string displayName = ((MenuItem)sender).Header.ToString().ToLower();
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
                MenuItem mnu = sender as MenuItem;
                mnu.IsChecked = true;

                //reset binding for displaying test display name
                TestDisplayName.Binding = bnd;
                Style toolTipStyle = new Style();
                toolTipStyle.Setters.Add(new Setter(ToolTipService.ToolTipProperty, bnd));
                TestDisplayName.ElementStyle = toolTipStyle;

                TestQueue.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Disable/enable controls on initial load
        /// </summary>
        private void InitializeControls()
        {
            UpdateButtonStates();
            ToolTipService.SetIsEnabled(txtSuitePath, !String.IsNullOrEmpty(LoadedSuite));
        }

        /// <summary>
        /// Event handler for queue context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueueTypeMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string QueueType = ((MenuItem)sender).Header.ToString();
                switch (QueueType)
                {
                    case "Queue":
                        tbKwQueue.Text = "Queue";
                        mIsInsert = false;
                        break;
                    case "Insert":
                        tbKwQueue.Text = "Insert";
                        mIsInsert = true;
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for result option menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResultOptionMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string ResultOptions = ((MenuItem)sender).Header.ToString();
                switch (ResultOptions)
                {
                    case "Load Results":
                        tbSuiteresults.Text = "Load Results";
                        btnResultsOptions.ToolTip = "Loading results";
                        break;
                    case "Clear Results":
                        tbSuiteresults.Text = "Clear Results";
                        btnResultsOptions.ToolTip = "Clearing results";
                        break;
                    case "Export Results":
                        tbSuiteresults.Text = "Export Results";
                        btnResultsOptions.ToolTip = "Exporting results";
                        break;
                    default:
                        btnResultsOptions.ToolTip = "Show more options for results";
                        return;
                }
                btnResultsOptions.ToolTip = ResultOptions;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for scheduler context menu item clicked
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        /// <param name="e"></param>
        private void SchedulerTypeMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string SchedulerType = ((MenuItem)sender).Header.ToString();
                switch (SchedulerType)
                {
                    case "Scheduler":
                        tbKwScheduler.Text = "Scheduler";
                        break;
                    case "New Scheduler":
                        tbKwScheduler.Text = "New Scheduler";
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var comboBox = sender as ComboBox;
                var selectedItem = TestQueue.SelectedItem;

                /* Set execution condition (dependency) */
                if (comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() == "Set condition...")
                {
                    TestDependencyDialog dp = new TestDependencyDialog((DlkExecutionQueueRecord)selectedItem, ExecutionQueueRecords.ToList(), this);
                    dp.Owner = this;
                    if ((bool)dp.ShowDialog())
                    {
                        if (dp.HasChanges)
                        {
                            isChanged = true;
                        }
                    }
                    TestQueue.CancelEdit();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvKeywordDirectory_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
            //tvKeywordDirectory.SelectedItem = null;
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        //int selectionManuallyChanged = 0;
        private void tvKeywordDirectory_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                //if (selectionManuallyChanged > 0)
                //{
                //    if (selectionManuallyChanged == 2)
                //    {
                //        selectionManuallyChanged = 0;
                //    }
                //    selectionManuallyChanged++;
                //    return;
                //}
                //object selectedItem = tvKeywordDirectory.SelectedItem;
                //btnKwTestEditor.IsEnabled = selectedItem != null &&
                //    (selectedItem.GetType() == typeof(KwFile) ||
                //    selectedItem.GetType() == typeof(KwInstance));

                //btnKwQueue.IsEnabled = selectedItem != null;

                //if (selectedItem.GetType() == typeof(KwFolder))
                //{
                //    btnKwQueue.IsEnabled = ((KwFolder)selectedItem).DirItems.Count > 0;
                //}
                //KwDirItem test = (KwDirItem)((TreeView)e.OriginalSource).SelectedItem;
                //if (IsCtrlPressed)
                //{
                //    if (!AddToSelectionList(m_CurrentSelectedTreeItem as TreeViewItem, test) && m_TreeItemSelectionList.Count > 0)
                //    {
                //        //selectionManuallyChanged++;
                //        //m_TreeItemSelectionList.First().Key.IsSelected = true;
                //    }
                //}
                //else
                //{
                //    ClearSelectionList();
                //    AddToSelectionList(m_CurrentSelectedTreeItem as TreeViewItem, test);
                //}
                if (!((KwDirItem)tvKeywordDirectory.SelectedItem).Path.Contains("|"))
                {
                    btnDeleteTest.IsEnabled = true;
                }
                else
                {
                    btnDeleteTest.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEditTestInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // validate that we can remove the record
                if (TestQueue.SelectedItems.Count < 1)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_IN_QUEUE);
                    return;
                }

                int idx = TestQueue.SelectedIndex;
                if (idx == -1)
                {
                    return;
                }

                DlkExecutionQueueRecord rec = ExecutionQueueRecords[idx];
                //TestEditor te = new TestEditor(DlkEnvironment.mLibrary, System.IO.Path.Combine(DlkEnvironment.mDirTests, 
                //    rec.folder.Trim('\\'), rec.file), int.Parse(rec.instance));
                if(!DlkEditorWindowHandler.IsEditorScriptOpened(rec.fullpath))
                {
                    if (DlkEditorWindowHandler.IsActiveEditorsMaxed()) { return; }
                    TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, System.IO.Path.Combine(DlkEnvironment.mDirTests,
                   rec.folder.Trim('\\'), rec.file));
                    //te.Owner = this;
                    te.Show();
                    //RefreshTestTree();
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Any Link click handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LinkMenu_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = (((sender as MenuItem).Tag) as DlkSuiteLinkRecord).LinkPath;
                /* if not valid local/network path, append HTTP as default protocol if not valid absolute URI */
                if (!Path.IsPathRooted(path) && !path.StartsWith("http"))
                {
                    path = "http://" + path;
                }
                Process.Start(new ProcessStartInfo(path));
            }
            catch
            {
                // Ignore invalid link
            }
        }

        /// <summary>
        /// Manage Links menu clicked handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void ManageLinks_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ManageSuiteLinks dlg = new ManageSuiteLinks(LoadedSuite, mTestSuiteInfo, ExecutionQueueRecords.ToList());
                dlg.Owner = this;
                /* Check if user went thru with save, this is NULL if aborted */
                if (dlg.ShowDialog() == true && dlg.IsSaveAndCheckIn != null)
                {
                    RunProcessInBackground((bool)dlg.IsSaveAndCheckIn == true ? ProcessWorkerAction.SaveAndCheckInSuiteSilent
                        : ProcessWorkerAction.SaveSuiteLocalSilent, LoadedSuite);
                    isChanged = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Manage Tags menu clicked handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void ManageTags_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DlkTag> tagList = DlkTag.LoadAllTags();
                ManageSuiteTags dlg = new ManageSuiteTags(tagList, mTestSuiteInfo.Tags, LoadedSuite, mTestSuiteInfo, ExecutionQueueRecords.ToList(), true);
                dlg.Owner = this;
                /* Check if user went thru with save, this is NULL if aborted */
                if (dlg.ShowDialog() == true && dlg.IsSaveAndCheckIn != null)
                {
                    RunProcessInBackground((bool)dlg.IsSaveAndCheckIn == true ? ProcessWorkerAction.SaveAndCheckInSuiteSilent
                        : ProcessWorkerAction.SaveSuiteLocalSilent, LoadedSuite);
                    isChanged = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LoadLogDetailsGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                string errorLogLevel = DlkEnvironment.mErrorLogLevel;
                mlogGrid = (DataGrid)sender;
                
                foreach (object dgr in mlogGrid.Items)
                {
                    DataGridRow _dgr = (DataGridRow)(mlogGrid.ItemContainerGenerator.ContainerFromItem(dgr));

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
                                ((TextBlock)(cell.Content)).Text = "Image: " + System.IO.Path.Combine(LoadedResultFolder,
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
                    Process process = new Process();
                    ProcessStartInfo si = new ProcessStartInfo("OpenMspaint.bat");
                    si.Arguments = "\"" + pathToFile + "\"";
                    si.WindowStyle = ProcessWindowStyle.Hidden;
                    si.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    process.StartInfo = si;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            FilterTestDirectoryForm frm = new FilterTestDirectoryForm(TestRoot);
            frm.Owner = this;
            if ((bool)frm.ShowDialog())
            {
                TestRoot = frm.SelectedTestPath;
                RefreshTestTree();
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddTestDirectoryForm frm = new AddTestDirectoryForm(TestRoot);
                frm.Owner = this;
                bool? bSaveAndCheckIn;
                string sSavedFolderPath;
                frm.ShowDialog(out bSaveAndCheckIn, out sSavedFolderPath);
                if (bSaveAndCheckIn != null)
                {
                    RunProcessInBackground(bSaveAndCheckIn == true ? ProcessWorkerAction.AddAndCheckInTestFolder
                        : ProcessWorkerAction.AddTestFolderLocal, sSavedFolderPath);
                }
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
                txtSearch.Background = Brushes.LemonChiffon;
                grdSearchGrid.Visibility = System.Windows.Visibility.Collapsed;
                BrushConverter bc = new BrushConverter();
                btnSearchTest.BorderBrush = (Brush)bc.ConvertFrom("#FF828790");
                mTestFilter = "";
                //use the in-memory list containing the test directory instead of reloading the entire Tests folder.
                tvKeywordDirectory.DataContext = SearchForTests._testsFolderDirectory;
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            if (mTreeItemSelectionList.Values.Count == 1) 
            {
                DeleteSingleItem();
            }
            else if (mTreeItemSelectionList.Values.Count > 1)
            {
                DeleteMultipleItems();
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int typeLettersInSuccessionDelay = 2000;
                //timer where filtering occurs only ~2 seconds after the user pressed a key.
                if (mTxtSearchTimer != null)
                {
                    if (mTxtSearchTimer.Interval < typeLettersInSuccessionDelay)
                    {
                        //increment interval every time the user is typing.
                        mTxtSearchTimer.Interval += typeLettersInSuccessionDelay;
                    }
                }
                else
                {
                    mTxtSearchTimer = new System.Windows.Forms.Timer();
                    mTxtSearchTimer.Tick += new EventHandler(typing_Finished);
                    mTxtSearchTimer.Interval = typeLettersInSuccessionDelay;
                    mTxtSearchTimer.Start();
                }
            }
            catch
            {
                /* Do nothing. Normally, code reaches this catch block when the user typed while the filtering is still ongoing */
            }
        }

        /// <summary>
        /// Event handler where the timer has elapsed (meaning that it assumes that the user is finished typing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typing_Finished(object sender, System.EventArgs e)
        {
            try
            {
                mTestFilter = txtSearch.Text;

                if (string.IsNullOrEmpty(mTestFilter))
                {
                    tvKeywordDirectory.DataContext = SearchForTests._testsFolderDirectory;
                    txtSearch.Background = Brushes.LemonChiffon;
                    return;
                }
                txtSearch.Background = (mSearchType == KwDirItem.SearchType.Author || DlkString.IsSearchValid(mTestFilter)) ? Brushes.LemonChiffon : Brushes.LightPink;

                List<KwDirItem> treeData = SearchForTests.FilterTests(mSearchType, mTestFilter);
                if (treeData.Count > 0)
                {
                    tvKeywordDirectory.DataContext = treeData;
                }
                else
                {
                    tvKeywordDirectory.DataContext = null;
                }
                //null check
                if (mTxtSearchTimer != null)
                {
                    mTxtSearchTimer.Stop();
                    mTxtSearchTimer.Dispose();
                    mTxtSearchTimer = null;   
                }
            }
            catch
            {
               /* Do nothing */
            }
        }
        private void btnKwTestImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ImportTestOptionDialog importDlg = new ImportTestOptionDialog("test");
                importDlg.Owner = this;
                importDlg.ShowDialog();

                ImportDestination importDest = new ImportDestination(TestRoot);
                importDest.Owner = this;

                if (importDlg.ImportOption == ImportTestOptionDialog.ImportTestOptions.Folder)
                {
                    var oFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
                    oFolderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    oFolderDialog.SelectedPath = DlkEnvironment.mDirTests;
                    oFolderDialog.Description = "Import Test Folder";

                    if (oFolderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (CheckFolderForRootTestsOrSuites(oFolderDialog.SelectedPath, out String error))
                        {
                            switch (error)
                            {
                                case "RootTest":
                                    DlkUserMessages.ShowError(DlkUserMessages.ERR_IMPORT_ROOTTEST);
                                    break;
                                case "RootSuite":
                                    DlkUserMessages.ShowError(DlkUserMessages.ERR_IMPORT_ROOTSUITE);
                                    break;
                                case "RootSuiteAndTest":
                                    DlkUserMessages.ShowError("The folder you wish to import contains the Tests/Suites directory of " + DlkEnvironment.mProductFolder
                                    + " inside. Please choose a different folder."); ;
                                    break;
                            }
                        }
                        else
                        {
                            if (CheckFolderForMultipleNestedFolders(oFolderDialog.SelectedPath))
                            {
                                MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_IMPORT_NESTEDFOLDERS, "Import Folder?");
                                if (res == MessageBoxResult.No)
                                {
                                    return;
                                }
                            }

                            if ((bool)importDest.ShowDialog())
                            {
                                ImportLocation = importDest.ImportDestinationPath;
                                string targetPath = ImportLocation + Path.GetFileName(oFolderDialog.SelectedPath);

                                // To copy a folder to a new location: Check if the directory or folder already exists.
                                if (!System.IO.Directory.Exists(targetPath))
                                {
                                    CopyDirectory(oFolderDialog.SelectedPath, targetPath);
                                }
                                else
                                {
                                    // Prompt for the user to overwrite the destination file if it already exists.
                                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_FOLDER_EXISTS_IMPORT_FAILED, "Import Folder?");
                                    switch (res)
                                    {
                                        case MessageBoxResult.Yes:
                                            CopyDirectory(oFolderDialog.SelectedPath, targetPath);
                                            break;
                                        case MessageBoxResult.No:
                                            oFolderDialog.ShowDialog();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                RefreshTestTree();
                            }
                        }
                    }
                }
                else if (importDlg.ImportOption == ImportTestOptionDialog.ImportTestOptions.File)
                {
                    var ofDialog = new System.Windows.Forms.OpenFileDialog();
                    ofDialog.InitialDirectory = DlkEnvironment.mDirTests;
                    ofDialog.Filter = "XML files (*.xml)|*.xml";
                    ofDialog.Title = "Import Test";
                    ofDialog.Multiselect = true;

                    if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if ((bool)importDest.ShowDialog())
                        {
                            ImportLocation = importDest.ImportDestinationPath;
                            bool hasImported = true;
                            foreach (string importFile in ofDialog.FileNames)
                            {
                                int icount = 1;
                                bool bcont = true;
                                string destFile = System.IO.Path.Combine(ImportLocation, System.IO.Path.GetFileName(importFile));
                                //check if filename already exists
                                if (File.Exists(destFile))
                                {
                                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(string.Format(DlkUserMessages.ASK_FILE_EXISTS_IMPORT_FAILED, System.IO.Path.GetFileName(importFile)), "Save Changes?");
                                    switch (res)
                                    {
                                        case MessageBoxResult.Yes:
                                            while (File.Exists(destFile))
                                            {
                                                string tempFileName = string.Format("{0}({1})", System.IO.Path.GetFileNameWithoutExtension(importFile), icount++);
                                                destFile = System.IO.Path.Combine(ImportLocation, tempFileName + ".xml");
                                            }
                                            break;
                                        case MessageBoxResult.No:
                                            hasImported = false;
                                            destFile = null;
                                            bcont = false;
                                            break;
                                        default:
                                            break;
                                    }
                                }

                                if (bcont)
                                {
                                    //set the trd file
                                    string trdFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(importFile), System.IO.Path.GetFileNameWithoutExtension(importFile) + ".trd");
                                    string trdDestFile = destFile.Replace(".xml", ".trd");

                                    try
                                    {
                                        if (DlkTest.IsValidTest(importFile))
                                        {
                                            File.Copy(importFile, destFile, false);
                                            if (File.Exists(trdFile))
                                            {
                                                File.Copy(trdFile, trdDestFile, false);
                                            }
                                        }
                                        else
                                        {
                                            DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                                            return;
                                        }
                                    }
                                    catch
                                    {
                                        DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                                        return;
                                    }
                                }
                            }
                            if (hasImported)
                            {
                                RefreshTestTree();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        private void btnSyncDir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            mPathToSync = TestRoot;
            SyncStarted();
            mSyncBW.RunWorkerAsync();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSyncSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mPathToSync = TestRoot;
                SyncStarted();
                mSyncBW.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestDirSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTestDirSettings.IsChecked = !(IsTestTreeExpanded);
                IsTestTreeExpanded = !(IsTestTreeExpanded);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lnkManageLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu ctx = new ContextMenu();
                ctx.Style = ((Style)FindResource("contextMenuStyle1"));
                ctx.Items.Clear();

                /* Do not display anything if no links attached */
                if (mTestSuiteInfo.mLinks.Any())
                {
                    MenuItem grpLinks = new MenuItem();
                    grpLinks.Header = "Go to..."; // Link group header
                    grpLinks.IsEnabled = false;
                    grpLinks.FontWeight = FontWeights.Bold;
                    grpLinks.Foreground = new SolidColorBrush(Colors.Blue);
                    grpLinks.Margin = new Thickness(-20, 0, 0, 0);
                    ctx.Items.Add(grpLinks);

                    foreach (DlkSuiteLinkRecord rec in mTestSuiteInfo.mLinks)
                    {
                        MenuItem itm = SetMenuItem(rec, "DisplayName", LinkMenu_Clicked);
                        itm.ToolTip = rec.LinkPath;
                        if (itm != null)
                        {
                            ctx.Items.Add(itm);
                        }
                    }
                    Separator sep = new Separator(); // horizontal line
                    sep.Margin = new Thickness(-27, 0, 0, 0);
                    ctx.Items.Add(sep);
                }
                MenuItem manageLinks = SetMenuItem("Manage Suite Links...", ManageLinks_Clicked); // Management menu item
                manageLinks.FontWeight = FontWeights.Bold;
                manageLinks.Margin = new Thickness(-20, 0, 0, 0);
                ctx.Items.Add(manageLinks);
                ctx.IsOpen = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void lnkManageTags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu ctx = new ContextMenu();
                ctx.Style = ((Style)FindResource("contextMenuStyle1"));
                ctx.Items.Clear();

                /* Do not display anything if no tags attached */
                if (mTestSuiteInfo.Tags.Any())
                {
                    var sortedTags = mTestSuiteInfo.Tags.OrderBy(x => x.Name).ToList();
                    //filter tags attached to the test which were removed/deleted from the central tags file.
                    sortedTags = DlkTag.LoadAllTags().FindAll(x => sortedTags.Any(y => y.Id == x.Id));

                    if (sortedTags.Any())
                    {
                        MenuItem grpTags = new MenuItem();
                        grpTags.Header = "Tags"; // Tag group header
                        grpTags.IsEnabled = false;
                        grpTags.FontWeight = FontWeights.Bold;
                        grpTags.Foreground = new SolidColorBrush(Colors.Black);
                        grpTags.Margin = new Thickness(-20, 0, 0, 0);
                        ctx.Items.Add(grpTags);

                        foreach (DlkTag rec in sortedTags)
                        {
                            MenuItem itm = new MenuItem();
                            itm.Header = rec.Name;
                            itm.IsEnabled = false;
                            itm.Foreground = new SolidColorBrush(Colors.Black);

                            if (!String.IsNullOrWhiteSpace(rec.Description))
                            {
                                itm.ToolTip = rec.Description;
                                ToolTipService.SetShowOnDisabled(itm, true);
                            }

                            ctx.Items.Add(itm);
                        }
                        Separator sep = new Separator(); // horizontal line
                        sep.Margin = new Thickness(-27, 0, 0, 0);
                        ctx.Items.Add(sep);
                    }
                }
                MenuItem addEditTags = SetMenuItem("Manage Tags...", ManageTags_Clicked); // Add/Edit Tags menu item
                addEditTags.FontWeight = FontWeights.Bold;
                addEditTags.Margin = new Thickness(-20, 0, 0, 0);
                ctx.Items.Add(addEditTags);
                ctx.IsOpen = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handles Global Variable link click event. Also includes global variable suite saving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkManageGlobalVar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GlobalVariableFileSelectionDialog m_GlobalVarSelection = new GlobalVariableFileSelectionDialog(this, mTestSuiteInfo.GlobalVar);

                if ((bool)m_GlobalVarSelection.ShowDialog())
                {
                    if (mTestSuiteInfo.GlobalVar != m_GlobalVarSelection.SelectedGlobalVarFile)
                    {
                        SaveSuiteDialogSC dlg = new SaveSuiteDialogSC(LoadedSuite, mTestSuiteInfo, ExecutionQueueRecords.ToList(), false, mTestSuiteInfo.Description, mTestSuiteInfo.Owner);
                        bool? bSaveAndCheckIn;
                        string filePath;
                        dlg.Owner = this;
                        dlg.SaveSuite(out bSaveAndCheckIn, out filePath);

                        if (bSaveAndCheckIn is bool)
                        {
                            mTestSuiteInfo.GlobalVar = m_GlobalVarSelection.SelectedGlobalVarFile;
                            RunProcessInBackground((bool)bSaveAndCheckIn == true ? ProcessWorkerAction.SaveAndCheckInSuiteSilent
                        : ProcessWorkerAction.SaveSuiteLocalSilent, LoadedSuite);
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_GLOBAL_VAR_SELECT_SUCCESSFUL_PREFIX + mTestSuiteInfo.GlobalVar + DlkUserMessages.INF_GLOBAL_VAR_SELECT_SUCCESSFUL_SUFFIX);
                            isChanged = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvKeywordDirectoryItem_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                TreeViewItem tvi = sender as TreeViewItem;
                if (tvi != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    DragDrop.DoDragDrop(tvi, new DataObject("kwdiritem", tvKeywordDirectory.SelectedItem), DragDropEffects.Copy);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvKeywordDirectoryItem_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                // Verify that this is a valid drop and then store the drop target
                TreeViewItem item = GetNearestContainer
                (e.OriginalSource as UIElement);
                if (!(item.Header is KwFolder))
                {
                    e.Effects = DragDropEffects.None;
                }
                e.Handled = true;
            }
            catch (Exception)
            {

            }
        }

        private void tvKeywordDirectoryItem_Drop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                TreeViewItem TargetItem = GetNearestContainer
                    (e.OriginalSource as UIElement);
                if (TargetItem != null)
                {
                    if (mTreeItemSelectionList.Count > 0 && TargetItem.Header is KwFolder)
                    {
                        if (mTreeItemSelectionList.Count == 1 && (!(mTreeItemSelectionList.First().Value is KwFile)))
                        {
                            return;
                        }
                        bool modifySourceControl = false;
                        KwFolder destination = TargetItem.Header as KwFolder;
                        if (DlkSourceControlHandler.SourceControlSupported && DlkSourceControlHandler.SourceControlEnabled)
                        {
                                if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_MODIFY_SOURCE_CONTROL) == MessageBoxResult.Yes)
                                {
                                    modifySourceControl = true;
                                }
                        }
                        string sourceControlFiles = "";
                        foreach (KeyValuePair<TreeViewItem, KwDirItem> kvp in mTreeItemSelectionList)
                        {
                            if (kvp.Value is KwFile)
                            {
                                KwFile file = kvp.Value as KwFile;
                                sourceControlFiles += MoveFileInTreeView(file, modifySourceControl, destination) + ";";
                            }
                        }
                        RefreshTestTree();
                        sourceControlFiles = sourceControlFiles.Trim(';');
                        if (modifySourceControl && sourceControlFiles != String.Empty)
                        {
                            RunProcessInBackground(ProcessWorkerAction.MoveAndCheckInTest, sourceControlFiles, destination.Path + "\\");
                        }
                        ClearSelectionList();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvKeywordDirectory_Drop(object sender, DragEventArgs e)
        {
            try
            {
                    if (mTreeItemSelectionList.Count > 0)
                    {
                        if (mTreeItemSelectionList.Count == 1 && (!(mTreeItemSelectionList.First().Value is KwFile)))
                        {
                            return;
                        }
                        bool modifySourceControl = false;
                        if (DlkSourceControlHandler.SourceControlSupported && DlkSourceControlHandler.SourceControlEnabled)
                        {
                                if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_MODIFY_SOURCE_CONTROL) == MessageBoxResult.Yes)
                                {
                                    modifySourceControl = true;
                                }
                        }
                        string sourceControlFiles = "";
                        foreach (KeyValuePair<TreeViewItem, KwDirItem> kvp in mTreeItemSelectionList)
                        {
                            if (kvp.Value is KwFile)
                            {
                                KwFile file = kvp.Value as KwFile;
                                sourceControlFiles += MoveFileInTreeView(file, modifySourceControl) + ";";
                            }
                        }
                        RefreshTestTree();
                        sourceControlFiles = sourceControlFiles.Trim(';');
                        if (modifySourceControl && sourceControlFiles != String.Empty)
                        {
                            RunProcessInBackground(ProcessWorkerAction.MoveAndCheckInTest, sourceControlFiles, mTestRoot);
                        }
                        ClearSelectionList();
                    }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgTestLineup_Drop(object sender, DragEventArgs e)
        {

            try
            {
                Boolean bInsert = mIsInsert && ExecutionQueueRecords.Count > 0;
                if (tvKeywordDirectory.SelectedItem == null)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_QUEUE);
                    return;
                }

                if (EnvironmentIDs.Count() == 0)
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, bInsert ? "Insert failed" : "Queue failed");
                    return;
                }

                if (mTreeItemSelectionList.Count > 1)
                {
                    foreach (KeyValuePair<TreeViewItem, KwDirItem> kvp in mTreeItemSelectionList)
                    {
                        AddToExecutionQueue(kvp.Value);
                    }
                    ClearSelectionList();
                    ChangeKeepOpenBrowsers();
                }
                else
                {
                    AddToExecutionQueue(tvKeywordDirectory.SelectedItem);
                    ChangeKeepOpenBrowsers();
                }

                if ((LoadedSuite != null) && (LoadedSuite != ""))
                {
                    mIsQueueUpdated = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void GetSelectedItem(object sender, RoutedEventArgs e)
        {
            try
            {
            TreeViewItem tvi = e.OriginalSource as TreeViewItem;
            KwDirItem test = (KwDirItem)(tvKeywordDirectory).SelectedItem;
            if (IsCtrlPressed)
            {
                if (!AddToSelectionList(tvi, test))
                {
                    if (mTreeItemSelectionList.Count > 0)
                    {
                        tvi.IsSelected = false;
                    }
                }
            }
            else
            {
                ClearSelectionList();
                AddToSelectionList(tvi, test);
            }
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Copy all folders and files
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="destDirName">Destination directory</param>
        /// <param name="copySubDirs">Recursive</param>
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (!file.Name.Equals("template.dat") && !file.Name.Equals("test_connect.dat"))
                {
                    string extn = Path.GetExtension(file.Name);
                    DlkLogger.LogToFile(extn);
                    if (extn.Equals(".xml") || extn.Equals(".csv") || extn.Equals(".trd"))
                    {
                        string temppath = Path.Combine(destDirName, file.Name);
                        file.CopyTo(temppath, true);
                    }
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        /// <summary>
        /// Sends the recent error logs to MakatiAutomation
        /// </summary>
        private void ReportError(object sender, RoutedEventArgs e)
        {
            try
            {
                //Determine the logs to be sent
                List<string> mLogFiles = new List<string>();

                if (Directory.Exists(DlkLogger.m_LogFolderPath))
                {
                    mLogFiles = new List<string>(Directory.GetFiles(DlkLogger.m_LogFolderPath, "*.log"));
                }

                if (mLogFiles.Count > 0)
                {
                    //Create folder for error log
                    string newLogFolder = System.IO.Path.Combine(DlkLogger.m_LogFolderPath, DateTime.Now.ToString("yyyyMMddHHmmss"));
                    Directory.CreateDirectory(newLogFolder);
                    StreamWriter swFile = new StreamWriter(System.IO.Path.Combine(newLogFolder, "errorlog.txt"), true);
                    DateTime logTimestamp = new DateTime();

                    FileInfo[] logFiles = Directory.GetFiles(DlkLogger.m_LogFolderPath, "*.log").Select(x => new FileInfo(x)).ToArray();
                    if (mLogFiles.Count > 5)
                    {
                        logFiles = null;
                        logFiles = Directory.GetFiles(DlkLogger.m_LogFolderPath, "*.log").Select(x => new FileInfo(x)).OrderByDescending(x => x.LastWriteTime).Take(5).ToArray();
                    }
                    Directory.SetCurrentDirectory(DlkLogger.m_LogFolderPath);

                    //Write each errorlog into string
                    foreach (FileInfo fi in logFiles)
                    {
                        StringBuilder sb = new StringBuilder();
                        logTimestamp = DateTime.ParseExact(fi.Name.Remove(14, 4), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        sb.AppendLine("Error log encountered on: " + logTimestamp.ToString("MM dd yyyy HH:mm:ss"));

                        using (StreamReader sr = new StreamReader(fi.Name))
                        {
                            String line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                sb.AppendLine(line);
                            }
                        }

                        sb.AppendLine("===================================" + Environment.NewLine);
                        swFile.Write(sb.ToString());
                    }

                    //close streamwriter
                    swFile.Close();
                   
                    //Open message prompt before creating email //

                    MessageBoxResult res = DlkUserMessages.ShowInfo(DlkUserMessages.INF_FEEDBACK_MESSAGE, @"Feedback & Error Reporting");
                    //string msg = "Error log found!\n" + Environment.NewLine + "You may include this file in your email as an attachment.";
                    //MessageBoxResult res = MessageBox.Show(msg, @"Feedback & Error Reporting", MessageBoxButton.OK, MessageBoxImage.Information);
                    switch (res)
                    {
                        case MessageBoxResult.OK:
                            string recepients = "DeltekTestRunner@deltek.com";
                            string htmlBody = "Thank you for using the Deltek Test Runner!%0A%0AWe welcome your feedback and suggestions, so feel free to type them in below.%0A%0AYou may also include the log file found in this directory: " + "<" + @"file://" + newLogFolder.Trim() + ">" + " as an e-mail attachment if you like to report an error.";
                            string command = string.Format("mailto:{0}?subject={1}&body={2}", recepients, "Error Report", htmlBody);
                            System.Diagnostics.Process.Start(command);
                            break;
                        case MessageBoxResult.No:
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    DlkUserMessages.ShowInfo(DlkUserMessages.INF_FEEDBACK_MESSAGE, @"Feedback & Error Reporting");
                    string recepients = "DeltekTestRunner@deltek.com";
                    string htmlBody = "Thank you for using the Deltek Test Runner!%0A%0AWe welcome your feedback and suggestions, so feel free to type them in below.";
                    string command = string.Format("mailto:{0}?subject={1}&body={2}", recepients, "Error Report", htmlBody);
                    System.Diagnostics.Process.Start(command);
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// alternate way of sending email error report by creating an eml file before sending //not used
        /// /// </summary>
        private void ReportErrorAlt(object sender, RoutedEventArgs e)
        {
            try
            {
                 //Determine the logs to be sent
                string logDirectory = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "logs");
                string[] mLogFiles = Directory.GetFiles(logDirectory, "*.log");
                DateTime logTimestamp = new DateTime();
                List<DlkErrorRecord> elRec = new List<DlkErrorRecord>();

                //Create folder for error log
                string newLogFolder = System.IO.Path.Combine(logDirectory, DateTime.Now.ToString("yyyyMMddHHmmss"));
                Directory.CreateDirectory(newLogFolder);
                StreamWriter swFile = new StreamWriter(System.IO.Path.Combine(newLogFolder, "errorlog.txt"),true);

                if (mLogFiles.Length > 0)
                {
                    FileInfo[] logFiles = Directory.GetFiles(logDirectory, "*.log").Select(x => new FileInfo(x)).ToArray();
                    if (mLogFiles.Length > 5)
                    {
                        logFiles = null;
                        logFiles = Directory.GetFiles(logDirectory, "*.log").Select(x => new FileInfo(x)).OrderByDescending(x => x.LastWriteTime).Take(5).ToArray();
                    }
                    Directory.SetCurrentDirectory(logDirectory);

                    //Write each errorlog into string
                    foreach (FileInfo fi in logFiles)
                    {
                        StringBuilder sb = new StringBuilder();
                        logTimestamp = DateTime.ParseExact(fi.Name.Remove(14, 4), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        sb.AppendLine("Error log encountered on: " + logTimestamp.ToString("MM dd yyyy HH:mm:ss"));

                        using (StreamReader sr = new StreamReader(fi.Name))
                        {
                            String line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                sb.AppendLine(line);
                            }
                        }

                        // To create an errorlog per instance for report creation
                        DlkErrorRecord er = new DlkErrorRecord();
                        logTimestamp = DateTime.ParseExact(fi.Name.Remove(14, 4), "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        er.Instance = logTimestamp.ToString("MM dd yyyy HH:mm:ss");
                        er.Logs = sb.ToString();
                        elRec.Add(er);
                    }

                    //check if eml file exists
                    FileInfo[] mailFiles = Directory.GetFiles(logDirectory, "*.eml").Select(x => new FileInfo(x)).ToArray();

                    if(mailFiles.Length > 0){
                        foreach (FileInfo fi in mailFiles)
                        {
                            File.Delete(fi.FullName);
                        }
                    }

                     //To create an html report 
                    DlkErrorReportsHandler.CreateErrorReport(System.IO.Path.Combine(logDirectory, "errorreport.xml"), elRec);
                    Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    string htmlBody = DlkErrorReportsHtmlHandler.CreateHTMLReportBody(System.IO.Path.Combine(logDirectory, "errorreport.xml"));
                    XDocument html = XDocument.Load(htmlBody);


                    //Create mail message
                    MailMessage message = new MailMessage();
                    message.To.Add(new MailAddress("DeltekTestRunner@deltek.com"));
                    message.From = new MailAddress("you@youraddress.com");
                    message.Headers.Add("X-Unsent", "1");
                    message.Subject = "Error Report";
                    message.IsBodyHtml = true;
                    //message.Body = sb.ToString();
                    message.Body = "See attached error logs." + Environment.NewLine + html.ToString();


                    //Send eml to log directory.
                    SmtpClient client = new SmtpClient();
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = logDirectory;
                    client.Send(message);

                    //get the eml file and send
                    FileInfo mailFile = Directory.GetFiles(logDirectory, "*.eml").Select(x => new FileInfo(x)).OrderByDescending(x => x.LastWriteTime).First();
                    System.Diagnostics.Process.Start(mailFile.FullName);
                }
                else
                {
                    DlkUserMessages.ShowInfo(DlkUserMessages.INF_NO_ERROR_LOGS, "Send Error Report");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        private void KeepOpenCheckBoxClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                isChanged = true;
                ChangeKeepOpenBrowsers();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ShowNewFeatures(object sender, RoutedEventArgs e)
        {
            try
            {
                NewFeaturesDialog newFeatures = new NewFeaturesDialog(this);
                newFeatures.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display the What's New Dialog when a new version of Test Runner is launched for the first time.
        /// </summary>
        /// <param name="config"></param>
        private void LoadNewFeaturesDialog()
        {
            NewFeaturesDialog newDialog = new NewFeaturesDialog(this);
            newDialog.ShowDialog();
        }

        /// <summary>
        /// Enables/disables Copy/Paste test states depending on selected cells and clipboard content
        /// </summary>
        private void UpdateCopyPasteTestButtonStates()
        {
            try
            {
                btnCopyTestInQueue.IsEnabled = ExecutionQueueRecords.Count > 0 && !mIsTestQueueRunning;
                btnPasteTestToQueue.IsEnabled = deqrClipboard.Count > 0 && (TestQueue.Items.Count == 0 || (TestQueue.Items.Count > 0 && TestQueue.SelectedIndex >= 0 && TestQueue.SelectedItems.Count == 1)) && !mIsTestQueueRunning;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        /// <summary>
        /// Constructs a new execution queue record from an existing record.
        /// New record gains a new ID and skips unneeded data from old record like logs, dependencies, etc.
        /// </summary>
        /// <param name="oldRecord">Old queue record to retrieve existing data from</param>
        /// <param name="rowNumber">TestRow of the queue record - defaults to 0 if replaced later</param>
        /// <returns>Created DlkExecutionQueueRecord</returns>
        private DlkExecutionQueueRecord CreateQueueRecordFromExisting(DlkExecutionQueueRecord oldRecord, string rowNumber)
        {
            DlkExecutionQueueRecord newRecord = new DlkExecutionQueueRecord(
                            Guid.NewGuid().ToString(),
                            rowNumber.ToString(),
                            "0/" + oldRecord.executedsteps.Split('/').Last(),
                            "Not Run",
                            oldRecord.folder,
                            oldRecord.name,
                            oldRecord.description,
                            oldRecord.file,
                            oldRecord.instance,
                            oldRecord.environment,
                            oldRecord.Browser.Name,
                            oldRecord.keepopen,
                            "",
                            "",
                            oldRecord.execute,
                            "false",
                            "",
                            "",
                            oldRecord.instanceRange,
                            oldRecord.totalsteps);
            return newRecord;
        }

        private void ToggleButtonsVisibility(object sender, RoutedEventArgs e)
        {
            try
            {               
                if (AppClassFactory.GetInspector() != null)
                {
                    btnTestCapture.Visibility = Visibility.Visible;
                }
                else
                {
                    btnTestCapture.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtDisplayNameHeader_Loaded(object sender, RoutedEventArgs e)
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Clipboard.Clear();
                foreach(Window win in Application.Current.Windows)
                {
                    if (win.GetType().ToString() == "TestRunner.TestEditor")
                    {
                        e.Cancel = DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_APPLICATION_QUIT_TEST_EDITOR) == MessageBoxResult.No;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Returns a MenuItem with the correct properties
        /// Purpose is for proper and uniform format of context menu items
        /// </summary>
        /// <param name="Item">Object containing property to be used as menuitem header</param>
        /// <param name="HeaderPath">Property name to be used as header</param>
        /// <param name="eventHandlerName">Click handler for menu item</param>
        /// <returns>Created MenuItem</returns>
        private MenuItem SetMenuItem(object Item, string HeaderPath, RoutedEventHandler eventHandlerName)
        {
            try
            {
                MenuItem mnu = new MenuItem();
                PropertyInfo prop = Item.GetType().GetProperty(HeaderPath);
                object propValue = prop.GetValue(Item, null);
                mnu.Header = propValue.ToString();
                mnu.Tag = Item;
                mnu.Click += new RoutedEventHandler(eventHandlerName);
                return mnu;
            }
            catch
            {
                return null;
            }
        }

        private void btnSetSearchType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenContextMenu(sender);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Gets clicked context menu item text
        /// </summary>
        /// <param name="sender">Clicked context menu item</param>
        private string ProcessMenuItem(object sender)
        {
            if (sender is MenuItem)
            {
                IEnumerable<MenuItem> menuItems = null;
                var mi = (MenuItem)sender;
                if (mi.Parent is ContextMenu)
                    menuItems = ((ContextMenu)mi.Parent).Items.OfType<MenuItem>();
                if (mi.Parent is MenuItem)
                    menuItems = ((MenuItem)mi.Parent).Items.OfType<MenuItem>();
                if (menuItems != null)
                    foreach (var item in menuItems)
                    {
                        if (item.IsCheckable && item == mi)
                        {
                            item.IsChecked = true;
                        }
                        else if (item.IsCheckable && item != mi)
                        {
                            item.IsChecked = false;
                        }
                    }
                return mi.Header.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Opens context menu of object
        /// </summary>
        /// <param name="buttonSender">Parent object of context menu</param>
        private void OpenContextMenu(object buttonSender)
        {
            var addButton = buttonSender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void SearchTypeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string SearchName = ProcessMenuItem(sender);
                switch (SearchName)
                {
                    case MENU_TEXT_TESTSONLY:
                        mSearchType = KwDirItem.SearchType.TestsSuitesOnly;
                        break;
                    case MENU_TEXT_AUTHORONLY:
                        mSearchType = KwDirItem.SearchType.Author;
                        break;
                    case MENU_TEXT_TAGSONLY:
                        mSearchType = KwDirItem.SearchType.TagsOnly;
                        break;
                    case MENU_TEXT_TESTSANDTAGS:
                        mSearchType = KwDirItem.SearchType.TestsSuitesAndTags;
                        break;
                    default:
                        return;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnResultsOptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIsResultOpen) // context menu still open, handle
                {
                    e.Handled = true;
                    return;
                }

                switch (tbSuiteresults.Text)
                {
                    case "Load Results":
                        LoadResults();
                        break;
                    case "Clear Results":
                        ClearResults();
                        break;
                    case "Export Results":
                        ExportResultsDialog rs = new ExportResultsDialog(this, mDisplayNameTextBlock.Text);
                        rs.Owner = this;
                        rs.ShowDialog();
                        break;
                    default:
                        ShowSuiteResultOptionMenu(null,null);
                        break;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ComboBox_EnvironmentTextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox cbBoxEnv = (ComboBox)sender;
            try
            {
                if (!string.IsNullOrEmpty(cbBoxEnv.Text) && testSuiteEnvironmentPrevVal != null)
                {
                    if (EnvironmentIDs.Any(x => x == cbBoxEnv.Text) && cbBoxEnv.Text != testSuiteEnvironmentPrevVal)
                    {
                        isChanged = true;
                        mAllowQueueRefreshForKeepOpen = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ComboBox_EnvironmentDropDownClosed(object sender, EventArgs e)
        {
            ComboBox cbBoxEnv = (ComboBox)sender;
            try
            {
                if (!string.IsNullOrEmpty(cbBoxEnv.Text) && testSuiteEnvironmentPrevVal != null)
                {
                    if (EnvironmentIDs.Any(x => x == cbBoxEnv.Text) && cbBoxEnv.Text != testSuiteEnvironmentPrevVal)
                    {
                        isChanged = true;
                        ChangeKeepOpenBrowsers();
                    }
                }
            }
            catch (Exception ex)
            {
                cbBoxEnv.Text = !String.IsNullOrEmpty(testSuiteEnvironmentPrevVal) ? testSuiteEnvironmentPrevVal : cbBoxEnv.Text;
                testSuiteEnvironmentPrevVal = null;
                DlkUserMessages.ShowError(ex.Message);
            }
        }

        private void ComboBox_BrowserLostFocus(object sender, EventArgs e)
        {
            try
            {
                if (testSuiteBrowserPrevVal != null)
                {
                    ComboBox cbBoxBrowser = (ComboBox)sender;
                    isChanged = testSuiteBrowserPrevVal != cbBoxBrowser.SelectedValue.ToString();
                    testSuiteBrowserPrevVal = null;
                }
                
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ComboBox_BrowserSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (testSuiteBrowserPrevVal != null)
                {
                    ComboBox cbBoxBrowser = (ComboBox)sender;
                    isChanged = testSuiteBrowserPrevVal != cbBoxBrowser.SelectedValue.ToString();
                    testSuiteBrowserPrevVal = null;
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ComboBoxBrowser_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                ChangeKeepOpenBrowsers();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ComboBox_ExecuteTextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox cbBoxExecute = (ComboBox)sender;
            try
            {
                if (!string.IsNullOrEmpty(cbBoxExecute.Text) && testSuiteExecutePrevVal != null)
                {
                    if (cbBoxExecute.Text != testSuiteExecutePrevVal && (cbBoxExecute.Text.ToLower() == "true" || cbBoxExecute.Text.ToLower() == "false"))
                    {
                        isChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void TextBox_DescriptionGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBoxDescription = (TextBox)sender;
            try
            {
                testSuiteDescriptionPrevVal = txtBoxDescription.Text;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void TextBox_OwnerGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox txtBoxOwner = (TextBox)sender;
            try
            {
                testSuiteOwnerPrevVal = txtBoxOwner.Text;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void TextBox_DescriptionTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBoxDescription = (TextBox)sender;
            try
            {
                if (txtBoxDescription.Text != testSuiteDescriptionPrevVal)
                {
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void TextBox_OwnerTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBoxOwner = (TextBox)sender;
            try
            {
                if (txtBoxOwner.Text != testSuiteOwnerPrevVal)
                {
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region BACKGROUND METHODS
        /* History */
        private void mHistoryBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgTestHistory.DataContext = mCurrentTestHistory.mTestHistory;
                dgTestHistory.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void mHistoryBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                mCurrentTestHistory.LoadHistory();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /* Execution */
        private void mExecutionBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
            // call the Archiver if running a suite
            List<DlkExecutionQueueRecord> results = null;

            if (!DlkTestRunnerApi.mAbortionPending)
            //if (!TestExecutionDialog.bCanceledAll)
            {
                string folder;
                if ((LoadedSuite != null) && (LoadedSuite != "") && (!mIsQueueUpdated))
                {
                    //DlkTestSuiteResultsFileHandler.SaveSuiteResults(System.IO.Path.GetFileName(LoadedSuite), DlkTestRunnerApi.mResultsFile);
                    results = DlkTestSuiteResultsFileHandler.GetLatestSuiteResults(LoadedSuite, out folder);
                }
                else
                {
                    results = DlkTestSuiteResultsFileHandler.GetLatestResults(out folder);
                }

                /* Cache the resultd folder cache */
                LoadedResultFolder = folder;

                /* Update the test queue results grid */
                UpdateExecutionQueueRecords(results);
                UpdateTestExecutionDetails();
                /* Set Test exececution completion flag before changing selection index */
                mTestExecutionCompleted = true;
                TestQueue.SelectedIndex = 0;
                TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);

                if (mCommandLineMode)
                {
                    Close();
                }
                txtExecutionDate.Text = GetLatestResultsFolderDate();
            }

            /* Close execution dialog if visible */
            if (mTestExecutionDialog != null && mTestExecutionDialog.IsVisible)
            {
                mTestExecutionDialog.Close();
            }

            /* Reset all cancellation flags */
            DlkTestRunnerApi.mAbortionPending = false;
            DlkTestRunnerApi.mCancellationPending = false;

            /* Reset execution completion flag */
            mTestExecutionCompleted = false;
            UpdateButtonStates();
            UpdateStatusBar();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mExecutionBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

            /* Reset cancellation flag to default */
            DlkTestRunnerApi.mCancellationPending = false;

            if (mTestExecutionDialog == null)
            {
                if (e.ProgressPercentage < 100)
                {
                    mTestExecutionDialog = new TestExecutionDialog(txtSuitePath.Text, true, mExecutionQueueRecords.Count);
                    mTestExecutionDialog.Show();
                }
            }
            else
            {
                mTestExecutionDialog.Close();
                if (e.ProgressPercentage < 100)
                {
                    mTestExecutionDialog = new TestExecutionDialog(txtSuitePath.Text, true, mExecutionQueueRecords.Count);
                    mTestExecutionDialog.Show();
                }
            }

            // call the Archiver if running a suite
            List<DlkExecutionQueueRecord> results = null;
            string folder;
            results = DlkTestSuiteResultsFileHandler.GetLatestResults(out folder);

            /* Cache the resultd folder cache */
            LoadedResultFolder = folder;
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
            /* Update the test queue results grid */
                UpdateExecutionQueueRecords(results);
                UpdateTestExecutionDetails();
            }));
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void mExecutionBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
            /* Check if suite is saved in file or in memory only */
            if ((LoadedSuite != null) && (LoadedSuite != "") && (!mIsQueueUpdated))
            {
                DlkTestRunnerApi.ExecuteTest(LoadedSuite);
            }
            else
            {
                DlkTestRunnerApi.mGlobalVarFileName = mIsQueueUpdated ? mTestSuiteInfo.GlobalVar : String.Empty;
                DlkTestRunnerApi.ExecuteTest(ExecutionQueueRecords.ToList());
            }

            int testsRan = 0;
            decimal progUnit = ExecutionQueueRecords.Count > 0 ? Decimal.Floor(100 / ExecutionQueueRecords.Count) : 0;
            decimal progress = 0;

            while (DlkTestRunnerApi.mExecutionStatus == "running")
            {
                Thread.Sleep(500);
                if (DlkTestRunnerApi.mTestsRan != testsRan)
                {
                    mExecutionBW.ReportProgress(Decimal.ToInt32(progress));
                    progress += progUnit;
                    testsRan = DlkTestRunnerApi.mTestsRan;
                }
            }

             mExecutionBW.ReportProgress(100);
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /* Report */
        private void mReportBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
            //List<DlkTestSuiteManifestRecord> suiteResults = DlkTestSuiteResultsFileHandler.ResultManifestRecords;
            //double i = 0;
            //double iTotal = suiteResults.Count;
            //DlkReportGenerator.ClearReportDB();
            //foreach (DlkTestSuiteManifestRecord manifestRec in suiteResults)
            //{
            //    DlkReportGenerator.AddResultManifestRecToDB(manifestRec);
            //    DlkReportGenerator.AddResultsToDB(manifestRec.suitepath, manifestRec.resultsdirectory);
            //    i++;
            //    double progress = (i / iTotal) * 100;
            //    reportBW.ReportProgress(Convert.ToInt32(progress));

            //}
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void mReportBW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
            if (mPopulateReportDialog == null)
            {
                if (e.ProgressPercentage < 100)
                {
                    mPopulateReportDialog = new PopulateReportDBDialog("[" + e.ProgressPercentage.ToString() + "%]");
                    mPopulateReportDialog.ShowDialog();
                }
            }
            else
            {
                if (e.ProgressPercentage < 100)
                {
                    mPopulateReportDialog.UpdateProgress("[" + e.ProgressPercentage.ToString() + "%]");

                }
                else
                {
                    mPopulateReportDialog.Close();
                }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void mReportBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
            //DlkEnvironment.KillProcessByName("MSACCESS.EXE");
            //DlkEnvironment.RunProcess("msaccess.exe", DlkEnvironment.DirReport + "Report.accdb", DlkEnvironment.DirWorking, false, 0);
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /* Sync files */
        private void mSyncBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (DlkConfigHandler.GetConfigValue("sourcecontrolenabled").ToLower() == "true")
                {
                    DlkSourceControlHandler.GetFiles(mPathToSync, "/recursive /all");
                }
            }
            catch
            {
                Dispatcher.BeginInvoke(new InvokerDelegate(SyncCompleted), null);
            }
        }
        private void mSyncBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
            SyncCompleted();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /* Refresh test tree */
        private void mRefreshBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                UpdateStatusBar();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void mRefreshBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateStatusBar("Refreshing test tree...");
                RefreshTestTree();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /* Delete files/folders */

        /// <summary>
        /// Delete folder from local and server
        /// </summary>
        /// <param name="path">Folder path to delete</param>
        private void DeleteSourceControlDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return;
                }
                DlkSourceControlHandler.Delete(path, "");
                DlkSourceControlHandler.CheckIn(path, "/comment:TestRunner");
                if (Directory.Exists(path))
                {
                    DeleteLocalDirectory(path);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete folder from local
        /// </summary>
        /// <param name="path">Folder path to delete</param>
        private void DeleteLocalDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return;
                }
                string[] readOnlyFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                /* remove lock of files inside */
                foreach (string file in readOnlyFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.IsReadOnly = false;
                }
                Directory.Delete(path, true);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete test from local and server
        /// </summary>
        /// <param name="path">Path of test to delete</param>
        private void DeleteSourceControlTest(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                DlkSourceControlHandler.Delete(path, "");
                DlkSourceControlHandler.CheckIn(path, "/comment:TestRunner");
                string sTrd = path.Replace("xml", "trd");
                if (File.Exists(sTrd))
                {
                    DlkSourceControlHandler.Delete(sTrd, "");
                    DlkSourceControlHandler.CheckIn(sTrd, "/comment:TestRunner");
                }
                if (File.Exists(path))
                {
                    DeleteLocalTest(path);
                }
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Delete test from local
        /// </summary>
        /// <param name="path">Path of test to delete</param>
        private void DeleteLocalTest(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                FileInfo fileInfo = new FileInfo(path);
                fileInfo.IsReadOnly = false;
                File.Delete(path);

                string sTrd = path.Replace("xml", "trd");
                if (File.Exists(sTrd))
                {
                    fileInfo = new FileInfo(sTrd);
                    fileInfo.IsReadOnly = false;
                    File.Delete(sTrd);
                }
            }
            catch
            {
                throw;
            }
            
        }

        /// <summary>
        /// Delete single item in Test Explorer and checkin
        /// </summary>
        /// <param name="pathToDelete">Path of item selected</param>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private bool DeleteItemAndCheckIn(string pathToDelete)
        {
            bool ret = true;
            try
            {
                bool bIsFile = File.Exists(pathToDelete);
                bool bIsFolder = Directory.Exists(pathToDelete);
                if (!bIsFile && !bIsFolder) /* Nothing to delete */
                {
                    return false;
                }

                if (bIsFolder)
                {
                    DeleteSourceControlDirectory(pathToDelete);
                }
                else
                {
                    DeleteSourceControlTest(pathToDelete);
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Delete multiple items in Test Explorer and checkin
        /// </summary>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private bool DeleteMultipleItemsAndCheckIn()
        {
            bool ret = true;
            try
            {
                foreach (KwDirItem entry in mTreeItemSelectionList.Values)
                {
                    FileAttributes pathAttr = File.GetAttributes(entry.Path); // for checking if file or folder
                    if ((pathAttr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        DeleteSourceControlDirectory(entry.Path);
                    }
                    else
                    {
                        DeleteSourceControlTest(entry.Path);
                    }
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Delete single item in Test Explorer
        /// </summary>
        /// <param name="pathToDelete">Path of item selected</param>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private bool DeleteItemLocal(string pathToDelete)
        {
            bool ret = true;
            try
            {
                bool bIsFile = File.Exists(pathToDelete);
                bool bIsFolder = Directory.Exists(pathToDelete);
                if (!bIsFile && !bIsFolder) /* Nothing to delete */
                {
                    return false;
                }
                if (bIsFolder)
                {
                    DeleteLocalDirectory(pathToDelete);
                }
                else
                {
                    DeleteLocalTest(pathToDelete);                    
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Delete multiple items in Test Explorer
        /// </summary>
        /// <returns>TRUE if successful; FALSE otherwise</returns>
        private bool DeleteMultipleItemsLocal()
        {
            bool ret = true;
            try
            {
                foreach (KwDirItem entry in mTreeItemSelectionList.Values)
                {
                    FileAttributes pathAttr = File.GetAttributes(entry.Path); // for checking if file or folder
                    if ((pathAttr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        DeleteLocalDirectory(entry.Path);
                    }
                    else
                    {
                        DeleteLocalTest(entry.Path);
                    }
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Delete single item from Test Explorer
        /// </summary>
        private void DeleteSingleItem()
        {
            try
            {
                if (tvKeywordDirectory.SelectedItem != null)
                {
                    string pathToDelete = ((KwDirItem)tvKeywordDirectory.SelectedItem).Path;
                    bool bIsFile = File.Exists(pathToDelete);
                    bool bIsFolder = Directory.Exists(pathToDelete);
                    if (bIsFile || bIsFolder)
                    {
                        if (DlkSourceControlHandler.SourceControlEnabled)
                        {
                            CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_DELETE_SOURCE_CONTROL);
                            ciscDialog.rbSaveAndCheckin.Content = "Yes, delete both local and source control file/s.";
                            ciscDialog.rbSaveOnly.Content = "No, delete local file/s only.";
                            ciscDialog.Owner = IsVisible ? this : Owner;
                            ciscDialog.ShowDialog();
                            if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveAndCheckin)
                            {
                                RunProcessInBackground(ProcessWorkerAction.DeleteAndCheckInTestItem, pathToDelete);
                            }
                            else if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveOnly)
                            {
                                RunProcessInBackground(ProcessWorkerAction.DeleteTestItemLocal, pathToDelete);
                            }
                        }
                        else
                        {
                            string msg = bIsFile ? DlkUserMessages.ASK_TEST_DELETE + pathToDelete + "?" : DlkUserMessages.ASK_FOLDER_DELETE + pathToDelete + DlkUserMessages.ASK_FOLDER_ALLFILES;
                            if (HasReadOnlyFile(pathToDelete)) /* Change message if there 'readonly' conideration */
                            {
                                msg = bIsFile ? DlkUserMessages.ASK_TEST_DELETE + pathToDelete + "?\n\n" + DlkUserMessages.ASK_FILE_READ_ONLY 
                                    : DlkUserMessages.ASK_FOLDER_DELETE + pathToDelete + DlkUserMessages.ASK_FOLDER_ALLFILES + "\n\n" + DlkUserMessages.ASK_FOLDER_READ_ONLY;
                            }

                            if (DlkUserMessages.ShowQuestionYesNo(msg, "Delete") == MessageBoxResult.Yes)
                            {
                                RunProcessInBackground(ProcessWorkerAction.DeleteTestItemLocal, pathToDelete);
                            }
                        }
                    }
                    else
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_NONEXISTENT_PATH + pathToDelete, "File/directory doesn't exist");
                    }
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Delete multiple items from Test Explorer
        /// </summary>
        private void DeleteMultipleItems()
        {
            try
            {
                if (DlkSourceControlHandler.SourceControlEnabled)
                {
                    CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_DELETE_SOURCE_CONTROL);
                    ciscDialog.rbSaveAndCheckin.Content = "Yes, delete both local and source control files.";
                    ciscDialog.rbSaveOnly.Content = "No, delete local files only.";
                    ciscDialog.Owner = IsVisible ? this : Owner;
                    ciscDialog.ShowDialog();

                    if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveAndCheckin)
                    {
                        RunProcessInBackground(ProcessWorkerAction.DeleteAndCheckInMultipleTestItems, string.Empty);
                    }
                    else if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveOnly)
                    {
                        RunProcessInBackground(ProcessWorkerAction.DeleteMultipleTestItemsLocal, string.Empty);
                    }
                }
                else
                {
                    if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_MASS_DELETE_READ_ONLY, "Delete") == MessageBoxResult.Yes)
                    {
                        RunProcessInBackground(ProcessWorkerAction.DeleteMultipleTestItemsLocal, string.Empty);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Check whether file is readonly or directory contains any readonly file
        /// </summary>
        /// <param name="path">Path of either file or directory</param>
        /// <returns>TRUE if has readonly; FALSE otherwise</returns>
        private bool HasReadOnlyFile(string path)
        {
            bool ret = false;
            if (Directory.Exists(path))
            {
                string[] readOnlyFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                ret = readOnlyFiles.Select(x => new FileInfo(x)).Any(y => y.IsReadOnly == true);
            }
            else if (File.Exists(path))
            {
                ret = new FileInfo(path).IsReadOnly;
            }
            return ret;
        }

        /// <summary>
        /// This method is used in Import when the user selects to copy a Folder/Directory.
        /// </summary>
        /// <param name="sourcePath">Full path of the folder/directory to copy</param>
        /// <param name="targetPath">Full path of the destination directory</param>
        private void CopyDirectory(string sourcePath, string targetPath)
        {          
              if (!Directory.Exists(targetPath))
              {
                Directory.CreateDirectory(targetPath);
              }

              string[] files = Directory.GetFiles(sourcePath, "*.xml");
              foreach (var file in files)
              {
                string trdFile = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".trd");
                string destFile = Path.Combine(targetPath, Path.GetFileNameWithoutExtension(file) + ".xml");
                string trdDestFile = Path.Combine(targetPath, Path.GetFileNameWithoutExtension(file) + ".trd");

                try
                {
                    if (DlkTest.IsValidTest(file))
                    {
                        File.Copy(file, destFile, true);
                        if (File.Exists(trdFile))
                        {
                            File.Copy(trdFile, trdDestFile, true);
                        }
                    }
                    else
                    {
                        // Skip invalid tests
                        continue; 
                    }
                }
                catch
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_TEST_XML_INVALID);
                    return;
                }
              }

              string[] directories = Directory.GetDirectories(sourcePath);
              foreach (var directory in directories)
              {
                string name = Path.GetFileName(directory);
                string dest = Path.Combine(targetPath, name);
                CopyDirectory (directory, dest);
              }
        }

        /* Check-in files */
        private void mCheckInBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FileInfo fi = new FileInfo(DlkEnvironment.mLoginConfigFile);
                fi.IsReadOnly = true;
                DlkSourceControlHandler.CheckOut(DlkEnvironment.mLoginConfigFile, "");
                DlkSourceControlHandler.CheckIn(DlkEnvironment.mLoginConfigFile, "/comment:TestRunner");
                fi = new FileInfo(DlkEnvironment.mRemoteBrowsersFile);
                fi.IsReadOnly = true;
                DlkSourceControlHandler.CheckOut(DlkEnvironment.mRemoteBrowsersFile, "");
                DlkSourceControlHandler.CheckIn(DlkEnvironment.mRemoteBrowsersFile, "/comment:TestRunner");
                fi = new FileInfo(DlkEnvironment.mTagsFilePath);
                fi.IsReadOnly = true;
                DlkSourceControlHandler.CheckOut(DlkEnvironment.mTagsFilePath, "");
                DlkSourceControlHandler.CheckIn(DlkEnvironment.mTagsFilePath, "/comment:TestRunner");

                if(DlkEnvironment.mIsMobileSettingChange)
                {
                    fi = new FileInfo(DlkEnvironment.mMobileConfigFile);
                    fi.IsReadOnly = true;
                    DlkSourceControlHandler.CheckOut(DlkEnvironment.mMobileConfigFile, "");
                    DlkSourceControlHandler.CheckIn(DlkEnvironment.mMobileConfigFile, "/comment:TestRunner");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        private void mCheckInBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
            CheckInConfigFilesDone();
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void LoadCreateExecutionConditionDialog(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateExecutionConditionDialog execConditionDlg = new CreateExecutionConditionDialog(ExecutionQueueRecords.ToList(), this);
                execConditionDlg.Owner = this;

                if ((bool)execConditionDlg.ShowDialog())
                {
                    if (execConditionDlg.HasChanges)
                    {
                        isChanged = true;
                    }
                    TestQueue.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Checks the test dependencies and removes any dependency on a missng record.
        /// </summary>
        /// <param name="eqrList">list of execution queue records</param>
        /// <param name="eqr">the execution queue record that is missing</param>
        private void UpdateTestDependencyAfterDataDeletion(List<DlkExecutionQueueRecord> eqrList, DlkExecutionQueueRecord eqr)
        {
            try
            {
                for (int recordIndex = 0; recordIndex < eqrList.Count; recordIndex++)
                {
                    var testDependencyRow = eqrList[recordIndex].executedependencytestrow;

                    // check if row value is equal to missing record
                    if (testDependencyRow == eqr.testrow)
                    {
                        eqrList[recordIndex].executedependency = null;
                        eqrList[recordIndex].dependent = "False";
                        eqrList[recordIndex].executedependencyresult = string.Empty;
                        eqrList[recordIndex].executedependencytestrow = string.Empty;
                    }
                    TestQueue.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Process worker work completed handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mProcessWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                IsBackgroundProcessInProgress = false;
                UpdateStatusBar();

                object[] resArr = (object[])e.Result;

                if (resArr.Count() != PROCESS_ACTION_RES_EXACT_COUNT)
                {
                    return;
                }

                bool bFuncRes = (bool)resArr[PROCESS_ACTION_RES_IDX_RET];
                if(bFuncRes)
                {
                    ProcessWorkerAction action = (ProcessWorkerAction)resArr[PROCESS_ACTION_RES_IDX_ACTION];
                    string sPath = resArr[PROCESS_ACTION_RES_IDX_SAVEPATH].ToString();

                    switch (action)
                    {
                        case ProcessWorkerAction.SaveSuiteLocal:
                        case ProcessWorkerAction.SaveAndCheckInSuite:
                            LoadSuite(sPath, true);
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_SAVE_SUCCESSFUL + sPath);
                            ChangeKeepOpenBrowsers();
                            SetTestQueueIsModified(false);
                            break;
                        case ProcessWorkerAction.SaveSuiteLocalSilent:
                        case ProcessWorkerAction.SaveAndCheckInSuiteSilent:
                            SetTestQueueIsModified(false);
                            break;
                        case ProcessWorkerAction.AddTestFolderLocal:
                        case ProcessWorkerAction.AddAndCheckInTestFolder:
                            RefreshTestTree();
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_SAVE_SUCCESSFUL + sPath);
                            break;
                        case ProcessWorkerAction.DeleteTestItemLocal:
                        case ProcessWorkerAction.DeleteMultipleTestItemsLocal:
                        case ProcessWorkerAction.DeleteAndCheckInTestItem:
                        case ProcessWorkerAction.DeleteAndCheckInMultipleTestItems:
                            RefreshTestTree();
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_DELETE_SUCCESSFUL_SELECTED);
                            break;
                        case ProcessWorkerAction.MoveAndCheckInTest:
                            if (mIsSourceControlDragDropSuccessful)
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_DRAG_DROP_SOURCE_CONTROL_SUCCESSFUL);
                            mIsSourceControlDragDropSuccessful = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Process worker Do Work handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mProcessWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] args = (object[])e.Argument;

                if (args.Count() != PROCESS_ACTION_ARG_EXACT_COUNT)
                {
                    e.Result = new object[] { false, false };
                    return;
                }

                ProcessWorkerAction action = (ProcessWorkerAction)args[PROCESS_ACTION_ARG_IDX_ACTION];
                string sPath = args[PROCESS_ACTION_ARG_IDX_PATH].ToString();
                string sComments = args[PROCESS_ACTION_ARG_IDX_COMMENTS].ToString();
                string sSource = args[PROCESS_ACTION_ARG_IDX_SOURCE].ToString();

                switch (action)
                {
                    case ProcessWorkerAction.SaveSuiteLocal:
                        e.Result = new object[] { ProcessWorkerAction.SaveSuiteLocal, SaveLocalSuite(sPath), sPath };
                        break;
                    case ProcessWorkerAction.SaveSuiteLocalSilent:
                        e.Result = new object[] { ProcessWorkerAction.SaveSuiteLocalSilent, SaveLocalSuite(sPath), sPath };
                        break;
                    case ProcessWorkerAction.SaveAndCheckInSuite:
                        e.Result = new object[] { ProcessWorkerAction.SaveAndCheckInSuite, SaveAndCheckInSuite(sPath, sComments), sPath };
                        break;
                    case ProcessWorkerAction.SaveAndCheckInSuiteSilent:
                        e.Result = new object[] { ProcessWorkerAction.SaveAndCheckInSuiteSilent, SaveAndCheckInSuite(sPath, sComments), sPath };
                        break;
                    case ProcessWorkerAction.AddTestFolderLocal:
                        e.Result = new object[] { ProcessWorkerAction.AddTestFolderLocal, AddLocalFolder(sPath), sPath };
                        break;
                    case ProcessWorkerAction.AddAndCheckInTestFolder:
                        e.Result = new object[] { ProcessWorkerAction.AddAndCheckInTestFolder, AddAndCheckInFolder(sPath, sComments), sPath };
                        break;
                    case ProcessWorkerAction.MoveAndCheckInTest:
                        e.Result = new object[] { ProcessWorkerAction.MoveAndCheckInTest, MoveAndCheckInItem(sPath, sSource), sPath };
                        break;
                    case ProcessWorkerAction.DeleteTestItemLocal:
                        e.Result = new object[] { ProcessWorkerAction.DeleteTestItemLocal, DeleteItemLocal(sPath), sPath };
                        break;
                    case ProcessWorkerAction.DeleteAndCheckInTestItem:
                        e.Result = new object[] { ProcessWorkerAction.DeleteTestItemLocal, DeleteItemAndCheckIn(sPath), sPath };
                        break;
                    case ProcessWorkerAction.DeleteMultipleTestItemsLocal:
                        e.Result = new object[] { ProcessWorkerAction.DeleteMultipleTestItemsLocal, DeleteMultipleItemsLocal(), sPath };
                        break;
                    case ProcessWorkerAction.DeleteAndCheckInMultipleTestItems:
                        e.Result = new object[] { ProcessWorkerAction.DeleteAndCheckInMultipleTestItems, DeleteMultipleItemsAndCheckIn(), sPath };
                        break;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        void mCopySuiteFilesBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mTestConnectModalDlg != null && mTestConnectModalDlg.IsVisible)
            {
                mTestConnectModalDlg.Close();
            }
        }

        void mCopySuiteFilesBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\ProdConfig.xml"));
                string destPath = mProdConfigHandler.GetConfigValue("suitepath");

                if (DlkGlobalVariables.mOldSuiteDirPath != null)
                    DirectoryCopy(DlkGlobalVariables.mOldSuiteDirPath, destPath, true);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        void mCopyTestFilesBW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mTestConnectModalDlg != null && mTestConnectModalDlg.IsVisible)
            {
                mTestConnectModalDlg.Close();
            }
        }

        void mCopyTestFilesBW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\ProdConfig.xml"));
                string destPath = mProdConfigHandler.GetConfigValue("testpath");

                if (DlkGlobalVariables.mOldTestDirPath != null)
                    DirectoryCopy(DlkGlobalVariables.mOldTestDirPath, destPath, true);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mKeywordDirectories = SplashScreen.TestsToLoad;
//#if DEBUG
//            btnTestCapture.Visibility = Visibility.Collapsed;
//#else
//                btnOSRecorder.Visibility = Visibility.Collapsed;
//                //btnOSRecorder.IsEnabled = false;
//                //btnOSRecorder.Opacity = 0;
//                //btnTestCapture.IsEnabled = false;
//                //btnTestCapture.Opacity = 0;
//#endif
            ToggleButtonsVisibility(null, null);

            string mMode = DlkArgs.GetArg("-mode");
            if (mMode.ToLower() == "objectrecorder")
            {
                OSRecorder winOSRecorder = new OSRecorder();
                winOSRecorder.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Refreshes data of Test Explorer treeview
        /// </summary>
        public void RefreshTestTree()
        {
            /*remove existing values to store a fresh copy after calling the Get accessor of KeywordDirectories 
              because calling it it will re-add the values.
              This just prevents duplication after deletion or importing of tests.*/
            SearchForTests._testsFolderDirectory.Clear();
            SearchForTests._globalCollectionOfTests.Clear();

            mExpandedDirectories = ExpandedList(mKeywordDirectories);
            mKeywordDirectories = null;
            tvKeywordDirectory.DataContext = KeywordDirectories;
        }

        /// <summary>
        /// Refreshes to use in memory Test Explorer
        /// </summary>
        public void RefreshInMemoryTestTree(string testPath)
        {
            new DlkKeywordTestsLoader().UpdateSpecificMemoryDirectory(TestRoot, testPath, ExpandedList(mKeywordDirectories));
            tvKeywordDirectory.DataContext = SearchForTests._testsFolderDirectory;
        }

        public void RefreshTestQueue(string editedTestPath)
        {
            bool bPartOfSuite = false;
            try
            {
                bPartOfSuite = IsUpdatedTestPartOfSuite(editedTestPath);

                if (LoadedSuite != "" && (!mIsQueueUpdated))
                {
                    if (bPartOfSuite)
                    {
                        LoadSuite(LoadedSuite);
                    }
                }
                else
                {

                    List<DlkExecutionQueueRecord> _eqr = new List<DlkExecutionQueueRecord>();
                    bool AfterTestQueueRun = ExecutionQueueRecords.Any(x => x.teststatus != "Not Run");
                    
                    foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                    {
                        DlkTest test = new DlkTest(Path.Combine(DlkEnvironment.mDirTests.Trim('\\') + eqr.folder, eqr.file));
                        test.mTestInstanceExecuted = Convert.ToInt32(eqr.instance);
                        DlkData.SubstituteExecuteDataVariables(test);
                        if (!AfterTestQueueRun)
                        {
                            eqr.executedsteps = "0/" + test.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count;
                        }
                        eqr.name = test.mTestName;
                        eqr.description = test.mTestDescription.Trim();
                        _eqr.Add(eqr);
                    }
                    /* replace executionqueuerecords */
                    mExecutionQueueRecords.Clear();
                    foreach (DlkExecutionQueueRecord e in _eqr)
                    {
                        mExecutionQueueRecords.Add(e);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Refreshes data of Test Explorer treeview in background
        /// </summary>
        public void InitializeBackgroundRefresh()
        {
            mRefreshBW = new BackgroundWorker();
            mRefreshBW.DoWork += mRefreshBW_DoWork;
            mRefreshBW.RunWorkerCompleted += mRefreshBW_RunWorkerCompleted;
            mRefreshBW.RunWorkerAsync();
        }

        /// <summary>
        /// Gets log results based on ExecutionQueue index 
        /// </summary>
        public String GetLogName(int iRow)
        {
            String mLog = "";

            if (ExecutionQueueRecords.Count > 0)
            {
                string fileResultPath = System.IO.Path.GetFileNameWithoutExtension(ExecutionQueueRecords[iRow].file) + "_" + ExecutionQueueRecords[iRow].instance;
                mLog = System.IO.Path.Combine(LoadedResultFolder, fileResultPath + "_" + ExecutionQueueRecords[iRow].identifier + ".xml");

                if (!File.Exists(mLog) && iRow < ExecutionQueueRecords.Count && mLoadedResultsFileManifest.ContainsKey(ExecutionQueueRecords[iRow].identifier))
                {
                    mLog = System.IO.Path.Combine(LoadedResultFolder, mLoadedResultsFileManifest[ExecutionQueueRecords[iRow].identifier]);
                    if (!File.Exists(mLog))
                    {
                        mLog = string.Empty;
                    }
                }
            }
            return mLog;
        }

        /// <summary>
        /// Changes Browser and Environment states and values based on Keep Open state
        /// </summary>
        public void ChangeKeepOpenBrowsers()
        {
            int rowCount = 0;
            bool previousKeepOpen = false;
            foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
            {
                if (eqr.keepopen.ToLower() == "true" && TestQueue.Items.Count > rowCount + 1)
                {
                    TestQueue.CommitEdit();
                    mExecutionQueueRecords[rowCount + 1].Browser = mExecutionQueueRecords[rowCount].Browser;
                    mExecutionQueueRecords[rowCount + 1].environment = mExecutionQueueRecords[rowCount].environment;
                }
                eqr.keepopenfieldsenabled = !previousKeepOpen;
                rowCount++;
                previousKeepOpen = Convert.ToBoolean(eqr.keepopen);
            }
            TestQueue.CommitEdit();
            TestQueue.Items.Refresh();
        }

        #endregion

    }
    }
