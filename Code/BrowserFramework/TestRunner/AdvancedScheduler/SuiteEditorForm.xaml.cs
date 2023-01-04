using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using TestRunner.AdvancedScheduler.Classes;
using TestRunner.Common;
using TestRunner.Designer;
using TestRunner.Recorder;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for SuiteEditorForm.xaml
    /// </summary>
    public partial class SuiteEditorForm : Window, ISuiteEditor, INotifyPropertyChanged
    {
        #region DECLARATIONS
        private const int PROCESS_ACTION_ARG_IDX_ACTION = 0;
        private const int PROCESS_ACTION_ARG_IDX_PATH = 1;
        private const int PROCESS_ACTION_ARG_IDX_COMMENTS = 2;
        private const int PROCESS_ACTION_ARG_EXACT_COUNT = 3;
        private const int PROCESS_ACTION_RES_IDX_ACTION = 0;
        private const int PROCESS_ACTION_RES_IDX_RET = 1;
        private const int PROCESS_ACTION_RES_IDX_SAVEPATH = 2;
        private const int PROCESS_ACTION_RES_EXACT_COUNT = 3;
        private const string STATUS_TEXT_WAITING_FOR_SERVER = "Waiting for server response...";
        private const string STATUS_TEXT_SAVING = "Saving...";
        private const string STATUS_TEXT_DELETING = "Deleting...";
        private const string STATUS_TEXT_READY = "Ready";
        private List<DlkExecutionQueueRecord> deqrClipboard = new List<DlkExecutionQueueRecord>();

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public SuiteEditorForm()
        {
            InitializeComponent();

            /* Initialize Data Bindings */
            this.DataContext = this;
            tvKeywordDirectory.DataContext = KeywordDirectories;
            TestQueue.DataContext = ExecutionQueueRecords;
        }

        #region PRIVATE MEMBERS
        private const string MENU_TEXT_TESTSONLY = "Search for tests only";
        private const string MENU_TEXT_TAGSONLY = "Search for tags only";
        private const string MENU_TEXT_AUTHORONLY = "Search for authors only";
        private const string MENU_TEXT_TESTSANDTAGS = "Include tests and tags in search";
        private string mTestRoot;
        private string mPathToSync;
        private string mTestFilter = string.Empty;
        private KwDirItem.SearchType mSearchType;
        private string mLoadedSuite = string.Empty;
        private string mImportLocation;
        private bool mAllowQueueRefreshForKeepOpen = false;
        private bool mExpanded;
        private bool mIsQueueUpdated = false;
        private bool mTestExecutionCompleted = false;
        private bool mIsCtxQueueOpen = false;
        private bool mIsInsert = false;
        private bool _isChanged;
        private bool mAskDependencyOnce = false;
        private bool mAskDependencyCancelled = false;
        private bool mRemoveDependency = false;
        private bool mIgnoreEnvironmentErrors = false;
        private System.Windows.Forms.Timer mTxtSearchTimer;
        private delegate void InvokerDelegate();
        private TextBlock mDisplayNameTextBlock = new TextBlock();
        private DlkTestSuiteInfoRecord mTestSuiteInfo;
        private DlkExecutionQueueRecord mCurrentSelectedTestInQueue;
        private ObservableCollection<DlkExecutionQueueRecord> mExecutionQueueRecords;
        private List<string> mExecuteValueList = null;
        private List<KwDirItem> mKeywordDirectories;
        private List<KwDirItem> mExpandedDirectories;
        private Dictionary<TreeViewItem, KwDirItem> mTreeItemSelectionList = new Dictionary<TreeViewItem, KwDirItem>();
        private ObservableCollection<string> mWebBrowsers;
        private ObservableCollection<string> mRemoteBrowsers;
        private ObservableCollection<string> mMobileBrowsers;
        private ListCollectionView mAllBrowsers;
        private BackgroundWorker mSyncBW;
        private BackgroundWorker mCheckInBW;
        private bool mIsProcessInProgress = false;
        private string mStatusText = string.Empty;
        private string mSuiteEditorTitle = string.Empty;
        private BackgroundWorker mProcessWorker = null;
        private int testSuiteInstanceCount;
        private int testSuiteInstancePrevVal;
        private string testSuiteBrowserPrevVal;
        private string testSuiteDescriptionPrevVal;
        private string testSuiteOwnerPrevVal;
        private string testSuiteEnvironmentPrevVal = null;
        private string testSuiteExecutePrevVal = null;

        private enum ProcessWorkerAction /* Add here if additional action in the future */
        {
            SaveSuiteLocal,
            SaveSuiteLocalSilent,
            SaveAndCheckInSuite,
            SaveAndCheckInSuiteSilent,
            AddTestFolderLocal,
            AddAndCheckInTestFolder,
            DeleteTestItemLocal,
            DeleteAndCheckInTestItem,
            DeleteMultipleTestItemsLocal,
            DeleteAndCheckInMultipleTestItems
        }
        #endregion


        #region PROPERTIES
        /// <summary>
        /// Root directory of all tests
        /// </summary>
        public string TestRoot
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
        /// Data source of Test Explorer treeview
        /// </summary>
        public List<KwDirItem> KeywordDirectories
        {
            get
            {
                if (mKeywordDirectories == null)
                {
                    mKeywordDirectories = new List<KwDirItem>();
                    mKeywordDirectories = KeywordLoaderSingleton.Instance.GetKeywordDirectories(TestRoot, mExpandedDirectories);
                }
                return mKeywordDirectories;
            }
            set
            {
                mKeywordDirectories = value;
            }
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
        /// List containing all Browsers on config file
        /// </summary>
        public ObservableCollection<string> KeepOpenStates
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "Keep browser open after execution", "Close browser after execution [default]" });
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
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "File Name [default]", "Test Name", "Full Path" });
                return ret;
            }
        }

        /// <summary>
        /// The current instance of the Test Capture form
        /// </summary>
        public TestCapture TestCaptureForm
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Status text when Suite Editor is busy
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
        #endregion


        #region PRIVATE METHODS
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

        private void Initialize()
        {
            mSuiteEditorTitle = "Suite Editor [" + DlkTestRunnerSettingsHandler.ApplicationUnderTest.DisplayName + "]";
            Title = mSuiteEditorTitle;

            string[] searchType = { MENU_TEXT_TESTSONLY, MENU_TEXT_AUTHORONLY, MENU_TEXT_TAGSONLY, MENU_TEXT_TESTSANDTAGS };
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

            mTestSuiteInfo = new DlkTestSuiteInfoRecord();
            TestRoot = string.IsNullOrEmpty(KeywordLoaderSingleton.Instance.GetTestDirPath()) ? 
                DlkEnvironment.mDirTests : 
                KeywordLoaderSingleton.Instance.GetTestDirPath();
            IsTestTreeExpanded = Boolean.TryParse(DlkConfigHandler.GetConfigUnencryptedValue("testtreeexpanded"), out mExpanded)
                ? mExpanded : false;
            btnTestDirSettings.IsChecked = IsTestTreeExpanded;
            UpdateButtonStates();

            OnPropertyChanged("EnvironmentIDs");

            /* Sub initializers */
            InitializeBrowserLists();
            InitializeContextMenus();
#if DEBUG
            InitializeSourceControl();
#endif
            InitializeProcessWorker();

            //load suite
            LoadSuite(LoadedSuite);
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
            if (DlkTestSuiteXmlHandler.IsScriptMissing)
            {
                String mfiles = "";
                foreach (DlkExecutionQueueRecord eqr in DlkTestSuiteXmlHandler.mMissingRecs)
                {
                    mfiles += (System.IO.Path.Combine(DlkEnvironment.mDirTests, eqr.folder.Trim('\\'), eqr.file) + " [ Instance: " + eqr.instance + " ]" + "\n");
                    UpdateTestDependencyAfterDataDeletion(mRecs, eqr);
                }
                MessageBox.Show("Loaded Suite: " + LoadedSuite + "\n\n" + DlkUserMessages.INF_TEST_SUITE_MISSING_SCRIPTS + mfiles, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //else
            //{
            //    MessageBox.Show("Loaded Suite: " + LoadedSuite, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            //}

            /* Select first record */
            if (ExecutionQueueRecords.Count > 0)
            {
                TestQueue.SelectedIndex = 0;
            }

            // Update Other UI elements
            Title = mSuiteEditorTitle + " : " + LoadedSuite;
            ChangeKeepOpenBrowsers();
            //reset modified flag
            foreach (var item in TestQueue.Items.Cast<DlkExecutionQueueRecord>())
            {
                item.isModified = false;
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
        /// Initializes context menus in Execution Queue grid
        /// </summary>
        private void InitializeBrowserLists()
        {
            mWebBrowsers = new ObservableCollection<string>();
            mRemoteBrowsers = new ObservableCollection<string>();
            mMobileBrowsers = new ObservableCollection<string>();

            foreach (DlkBrowser browser in DlkEnvironment.mAvailableBrowsers)
            {
                mWebBrowsers.Add(browser.Alias);
            }
            if (DlkTestRunnerSettingsHandler.ApplicationUnderTest.Type.ToLower().Equals("internal"))
            {
                foreach (DlkRemoteBrowserRecord rec in DlkRemoteBrowserHandler.mRemoteBrowsers)
                {
                    mRemoteBrowsers.Add(rec.Id);
                }
                foreach (DlkMobileRecord mob in DlkMobileHandler.mMobileRec)
                {
                    mMobileBrowsers.Add(mob.MobileId);
                }
            }
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

            // needed to loop instead of setting datacontext since I need MenuItem as type of item
            // I cannot dynamically assign handler of click event if item type is any other object
            browser.Items.Clear();
            env.Items.Clear();
            isOpen.Items.Clear();
            displayName.Items.Clear();
            execute.Items.Clear();
            queue.Items.Clear();

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

            for (int i = 0; i < 2; i++)
            {
                execute.Items.Add(SetMenuItem(ExecuteValueList[i], ExecuteContextMenuClicked));
            }

            // Add new menu item in Execute context menu
            execute.Items.Add(new Separator());
            execute.Items.Add(SetMenuItem("Apply conditional setting", LoadCreateExecutionConditionDialog));

            //Add new menu items for Queue context menu
            queue.Items.Add(SetMenuItem("Queue", QueueTypeMenuClicked));
            queue.Items.Add(SetMenuItem("Insert", QueueTypeMenuClicked));

            //assign closed event for Queue context menu
            ((ContextMenu)FindResource("ctxQueueType")).Closed += new RoutedEventHandler(QueueContextMenuClosed);
        }

        private void InitializeSourceControl()
        {
            mSyncBW = new BackgroundWorker();
            mSyncBW.DoWork += mSyncBW_DoWork;
            mSyncBW.RunWorkerCompleted += mSyncBW_RunWorkerCompleted;

            mCheckInBW = new BackgroundWorker();
            mCheckInBW.DoWork += mCheckInBW_DoWork;
            mCheckInBW.RunWorkerCompleted += mCheckInBW_RunWorkerCompleted;
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

        private void SyncCompleted()
        {
            EnableTestTreeHelperControls(true);
            RefreshTestTree();
            UpdateStatusBar();
        }

        private void CheckInConfigFilesDone()
        {
            btnSyncSelected.IsEnabled = true;
            UpdateStatusBar();
            //mIsCheckingIn = false;
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
        /// Refreshes data of Test Explorer treeview
        /// </summary>
        private void RefreshTestTree()
        {
            /*remove existing values to store a fresh copy after calling the Get accessor of KeywordDirectories 
              because calling it it will re-add the values.
              This just prevents duplication after deletion or importing of tests.*/
            SearchForTests._testsFolderDirectory.Clear();
            SearchForTests._globalCollectionOfTests.Clear();

            mExpandedDirectories = ExpandedList(mKeywordDirectories);
            mKeywordDirectories = null;
            KeywordLoaderSingleton.Instance.SetKeywordDirectories(null);
            tvKeywordDirectory.DataContext = KeywordDirectories;
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
        /// Changes all valid items' expanded/collapsed state
        /// </summary>
        private void ChangeTreeViewItemsState()
        {
            ItemContainerGenerator gen = this.tvKeywordDirectory.ItemContainerGenerator;

            foreach (var itm in tvKeywordDirectory.Items)
            {
                TreeViewItem item = gen.ContainerFromItem(itm) as TreeViewItem;
                if (item != null)
                {
                    item.IsExpanded = IsTestTreeExpanded;

                    //if (item.IsExpanded)
                    //{
                    //    item.ExpandSubtree();
                    //}
                }
            }
        }

        private void SyncStarted()
        {
            EnableTestTreeHelperControls(false);
            UpdateStatusBar("Syncing " + mPathToSync);
        }

        private void EnableTestTreeHelperControls(bool IsEnabled)
        {
            btnTestDirSettings.IsEnabled = IsEnabled;
            btnSyncSelected.IsEnabled = IsEnabled;
            btnSearchTest.IsEnabled = IsEnabled;
            btnFilter.IsEnabled = IsEnabled;
            txtSearch.IsEnabled = IsEnabled;
        }

        private void UpdateStatusBar(string Status = "")
        {
            StatusText = string.IsNullOrEmpty(Status) ? STATUS_TEXT_READY : Status;
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

        private void DeleteSourceControlDirectory(string path)
        {
            DlkSourceControlHandler.Delete(path, "");
            DlkSourceControlHandler.CheckIn(path, "/comment:TestRunner");
            if (Directory.Exists(path))
            {
                DeleteReadOnlyDirectory(path);
            }
        }

        private void DeleteReadOnlyDirectory(string path)
        {
            try
            {
                string[] readOnlyFiles = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in readOnlyFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    fileInfo.IsReadOnly = false;
                }
                Directory.Delete(path, true);
            }
            catch (DirectoryNotFoundException ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void DeleteLocalDirectory(string path)
        {
            Directory.Delete(path, true);
        }

        private void DeleteSourceControlTest(string path)
        {
            DlkSourceControlHandler.Delete(path, "");
            DlkSourceControlHandler.CheckIn(path, "/comment:TestRunner");
            DlkSourceControlHandler.Delete(path.Replace("xml", "trd"), "");
            DlkSourceControlHandler.CheckIn(path.Replace("xml", "trd"), "/comment:TestRunner");
            if (File.Exists(path))
            {
                DeleteLocalTest(path);
            }
        }

        private void DeleteLocalTest(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            fileInfo.IsReadOnly = false;
            File.Delete(path);
            try
            {
                fileInfo = new FileInfo(path.Replace("xml", "trd"));
                fileInfo.IsReadOnly = false;
                File.Delete(path.Replace("xml", "trd"));
            }
            catch (FileNotFoundException ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

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
                            if (HasReadOnlyFile(pathToDelete)) /* Change message if there is 'readonly' conideration */
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

        private void DeleteMultipleItems()
        {
            try
            {
                //FileAttributes pathAttr;
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

            btnKwQueue.IsEnabled = mTreeItemSelectionList.Count > 0;
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
                    string title = Title.Replace(mSuiteEditorTitle + " : ", "");

                    if (!title.Contains("*") && title != mSuiteEditorTitle)
                    {
                        Title = mSuiteEditorTitle + " : *" + title;
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
                CopyDirectory(directory, dest);
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

        private void AddToExecutionQueue(object ItemToAdd)
        {
            int iRow = (mIsInsert && ExecutionQueueRecords.Count > 0) ? TestQueue.SelectedIndex : ExecutionQueueRecords.Count + 1;
            //String sRow = (ExecutionQueueRecords.Count + 1).ToString();
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
                             ((DlkBrowser)AllBrowsers.GetItemAt(0)).Name,
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
                             ((DlkBrowser)AllBrowsers.GetItemAt(0)).Name,
                             "false",
                             "",
                             "",
                             "True",
                             "false",
                             "",
                             "",
                             string.Format("Value range: 1 - {0}", test.mInstanceCount),
                             test.mTestSteps.Count().ToString());
                        ExecutionQueueRecords.Add(record);
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
                QueueFolder(ItemToAdd, true);
            }
            else if (ItemToAdd.GetType() == typeof(KwInstance))
            {
                KwInstance kwScript = (KwInstance)ItemToAdd;
                string path = kwScript.Path.Split('|').First();
                string instance = kwScript.Path.Split('|').Last();
                DlkTest test = new DlkTest(path);
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
                    ((DlkBrowser)AllBrowsers.GetItemAt(0)).Name,
                    "false",
                    "",
                    "",
                    "True",
                    "false",
                    "",
                    "",
                    string.Format("Value range: 1 - {0}", test.mInstanceCount),
                    test.mTestSteps.Count().ToString());

                AddExecutionQueueRecord(iRow, record, mIsInsert && ExecutionQueueRecords.Count > 0);

                if (LoadedSuite != "")
                {
                    isChanged = true;
                }
            }
        }

        private void QueueFolder(object ItemToAdd, bool AskForQueue) // added bool to prevent prompt from repeat displays, type int to retain numbering
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
                        QueueFolder(kwItem, false);
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
                                    ((DlkBrowser)AllBrowsers.GetItemAt(0)).Name,
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

                                DlkExecutionQueueRecord record = new DlkExecutionQueueRecord(
                                    Guid.NewGuid().ToString(),
                                    (ExecutionQueueRecords.Count + 1).ToString(),
                                    "0/" + testCopy.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count,
                                    "Not Run",
                                    Path.GetDirectoryName(test.mTestPath).Replace(Path.GetDirectoryName(DlkEnvironment.mDirTests), ""),
                                    test.mTestName,
                                    test.mTestDescription.Trim(),
                                    Path.GetFileName(test.mTestPath),
                                    idx.ToString(),
                                    EnvironmentIDs.First(),
                                    ((DlkBrowser)AllBrowsers.GetItemAt(0)).Name,
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
                        if(!mAskDependencyOnce)
                        {
                            ret = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_REMOVE_CURRENT_TEST_DEPENDENCY)
                            == MessageBoxResult.Yes;

                            if (ret)
                            {
                                mRemoveDependency = true;
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
                                mRemoveDependency = true;
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

        private void UpdateButtonStates()
        {
            btnMoveTestUpInQueue.IsEnabled = ExecutionQueueRecords.Count > 1 && TestQueue.SelectedIndex != 0;
            btnMoveTestDownInQueue.IsEnabled = ExecutionQueueRecords.Count > 1 && TestQueue.SelectedIndex != ExecutionQueueRecords.Count - 1;
            btnEditTestInQueue.IsEnabled = ExecutionQueueRecords.Count > 0;
            btnRemoveTestInQueue.IsEnabled = ExecutionQueueRecords.Count > 0;
            ContextMenuService.SetIsEnabled(TestQueue, false);
            UpdateCopyPasteTestButtonStates();
        }

        private void UpdateButtonStatesMultiSelect()
        {
            if (TestQueue.SelectedItems.Count > 1)
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
                    if (currentStep == ExecutionQueueRecords.Count)
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

        private bool SaveSuite()
        {
            if (ExecutionQueueRecords.Count < 1)
            {
                ShowError(DlkUserMessages.ERR_NO_TEST_TO_SAVE);
                return false;
            }

            //Checking for valid environment and blacklist URL
            for (int i = 0; i < TestQueue.Items.Count; i++)
            {
                DlkExecutionQueueRecord rec = (DlkExecutionQueueRecord)TestQueue.Items[i];

                if (!EnvironmentIDs.Contains(rec.environment))
                {
                    DlkUserMessages.ShowError(string.Format(DlkUserMessages.ERR_INVALID_ENVIRONMENT_VALUE_SAVE, rec.environment));
                    return false;
                }
                if (DlkEnvironment.IsURLBlacklist(rec.environment))
                {
                    DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.ERR_URL_BLACKLIST, rec.environment));
                    return false;
                }
            }

            //save file
            if (TrySaveSuite())
            {
                mIsQueueUpdated = false;

                //reset queue items ismodified property
                foreach (var item in TestQueue.Items.Cast<DlkExecutionQueueRecord>())
                {
                    item.isModified = false;
                }

                // Update Other UI elements
                Title = "Suite Editor [" + DlkEnvironment.mProductFolder + "]" + " : " + LoadedSuite;
            }

            TestQueue.SelectedIndex = 0;
            TestQueue.ScrollIntoView(ExecutionQueueRecords[0]);

            return true;
        }

        private bool TrySaveSuite()
        {
            bool checkIn = false;
            if (!DlkSourceControlHandler.SourceControlEnabled)
            {
                if (IsFileExistsAndReadOnly(LoadedSuite) &&
                    DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_OVERWRITE_READ_ONLY) == MessageBoxResult.No)
                {
                    return false;
                }
            }
            else
            {
                CheckinSourceControlDialog ciscDialog = new CheckinSourceControlDialog(DlkUserMessages.ASK_CHECK_IN_FILE_AFTER_SAVING);
                if (this.IsVisible) /* The dialog is visible, this should be the parent form */
                {
                    ciscDialog.Owner = this;
                }
                else /* Main Window is the parent form, meaning 'Save' was clicked for an existing file */
                {
                    ciscDialog.Owner = this.Owner;
                }
                ciscDialog.ShowDialog();

                if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.AbortSave)
                {
                    return false;
                }
                else if (ciscDialog.SaveCheckinChoice == CheckinSourceControlDialog.SaveCheckinOptions.SaveAndCheckin)
                {
                    checkIn = true;
                }
                else
                {
                    FileInfo fi = new FileInfo(LoadedSuite);
                    fi.IsReadOnly = false;
                }
            }
  
            mTestSuiteInfo.Description = txtDescription.Text;
            mTestSuiteInfo.Owner = txtOwner.Text;
            RunProcessInBackground(checkIn == true ? ProcessWorkerAction.SaveAndCheckInSuite : 
                ProcessWorkerAction.SaveSuiteLocal, LoadedSuite);

            return true;
        }

        private void CheckOutFromSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckOut(parameters.First().ToString(), parameters.Last().ToString());
        }

        private void AddToSource(object[] parameters)
        {
            DlkSourceControlHandler.Add(parameters.First().ToString(), parameters.Last().ToString());
        }

        private void CheckInToSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckIn(parameters.First().ToString(), parameters.Last().ToString());
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
        /// Set isModified property of test queue items
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
        /// Checks if input file exists AND is readonly
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <returns>TRUE if file exists and readonly</returns>
        /// FALSE if file does not exist OR not readonly
        private bool IsFileExistsAndReadOnly(string path)
        {
            return File.Exists(path) && new FileInfo(path).IsReadOnly;
        }

        /// <summary>
        /// Run process via Backgroundworker
        /// </summary>
        /// <param name="actionToPerform">Process to run</param>
        /// <param name="path">Relevant file Path</param>
        private void RunProcessInBackground(ProcessWorkerAction actionToPerform, string path)
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
                default:
                    break;
            }

            UpdateStatusBar(statusTxt);
            mProcessWorker.RunWorkerAsync(new object[] { actionToPerform, path, sComments });
        }

        /// <summary>
        /// Enables/disables Copy/Paste test states depending on selected cells and clipboard content
        /// </summary>
        private void UpdateCopyPasteTestButtonStates()
        {
            try
            {
                btnCopyTestInQueue.IsEnabled = ExecutionQueueRecords.Count > 0;
                btnPasteTestToQueue.IsEnabled = deqrClipboard.Count > 0 && (TestQueue.Items.Count == 0 || (TestQueue.Items.Count > 0 && TestQueue.SelectedIndex >= 0 && TestQueue.SelectedItems.Count == 1));
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

        /// <summary>
        /// Adds or Inserts the execution queue record into the test queue
        /// </summary>
        /// <param name="Row">Row number of the test to add or insert to the queue</param>
        /// <param name="Record">Queue record to add or insert to the queue</param>
        /// <param name="isInsert">Determines whether the record is added or inserted to the queue</param>
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
                    if (Row > 0) { TestQueue.ScrollIntoView(ExecutionQueueRecords[Row - 1]); }
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
        /// Refresh the Environment IDs after an env update is made in the Settings window. Clears out invalid env IDs in the grid.
        /// </summary>
        private void RefreshEnvironments()
        {
            try
            {
                OnPropertyChanged("EnvironmentIDs");
                InitializeContextMenus();
                foreach (DlkExecutionQueueRecord eqr in ExecutionQueueRecords)
                {
                    if (!EnvironmentIDs.Contains(eqr.environment))
                        eqr.environment = string.Empty;
                }
            }
            catch
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR);
            }
        }
        #endregion


        #region PUBLIC METHODS
        /// <summary>
        /// Refreshes to use in memory Test Explorer
        /// </summary>
        public void RefreshInMemoryTestTree(string testPath)
        {
            new DlkKeywordTestsLoader().UpdateSpecificMemoryDirectory(TestRoot, testPath, ExpandedList(mKeywordDirectories));
            tvKeywordDirectory.DataContext = SearchForTests._testsFolderDirectory;
            KeywordLoaderSingleton.Instance.SetKeywordDirectories(SearchForTests._testsFolderDirectory);
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
                        eqr.description = test.mTestDescription;
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


        #region EVENT HANDLERS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Initialize();
                Activate();
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
                //check if queue items' properties are modified on a loaded suite
                if ((LoadedSuite != null) && (LoadedSuite != "") &&
                    TestQueue.Items.Cast<DlkExecutionQueueRecord>().Any(x => x.isModified))
                {
                    mIsQueueUpdated = true;
                }
                //check if description or owner info is updated
                if (!mTestSuiteInfo.Description.Trim().Equals(txtDescription.Text.Trim()) ||
                    !mTestSuiteInfo.Owner.Trim().Equals(txtOwner.Text.Trim()))
                {
                    mIsQueueUpdated = true;
                }

                if (mIsQueueUpdated)
                {
                    e.Cancel = DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_APPLICATION_QUIT_SUITE_EDITOR) == MessageBoxResult.No;
                }

                Clipboard.Clear();

                int testEditorWindowCount = 1;
                foreach (Window win in Application.Current.Windows)
                {
                    if (win.GetType().ToString() == "TestRunner.TestEditor")
                    {
                        //on first instance - check if user really want to quit suite editor
                        if (testEditorWindowCount == 1 &&
                            DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_SCHEDULER_QUIT_TEST_EDITOR) == MessageBoxResult.No)
                        {
                            e.Cancel = true;
                            break;
                        }

                        win.Close();
                        testEditorWindowCount++;
                    }
                }
                mIgnoreEnvironmentErrors = !e.Cancel;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Form_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSuite();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Form_Close(object sender, RoutedEventArgs e)
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

        private void btnTestDirSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnTestDirSettings.IsChecked = !(IsTestTreeExpanded);
                IsTestTreeExpanded = !(IsTestTreeExpanded);
                ChangeTreeViewItemsState();
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
                    mTestFilter = string.Empty;
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

        private void tvKeywordDirectory_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
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
                            bool hasImported = false;
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
                                            hasImported = true;
                                            while (File.Exists(destFile))
                                            {
                                                string tempFileName = string.Format("{0}({1})", System.IO.Path.GetFileNameWithoutExtension(importFile), icount++);
                                                destFile = System.IO.Path.Combine(ImportLocation, tempFileName + ".xml");
                                            }
                                            break;
                                        case MessageBoxResult.No:
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

        private void btnKwTestNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkEditorWindowHandler.IsActiveEditorsMaxed(true)) { return; }

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
                    if (DlkEditorWindowHandler.IsActiveEditorsMaxed(true)) { return; }
                    TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, "");
                    te.Show();
                }
                else if (selectedItem.GetType() == typeof(KwFile))
                {
                    KwFile kwScript = (KwFile)selectedItem;
                    if (!DlkEditorWindowHandler.IsEditorScriptOpened(kwScript.Path))
                    {
                        if (DlkEditorWindowHandler.IsActiveEditorsMaxed(true)) { return; }
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
                        if (DlkEditorWindowHandler.IsActiveEditorsMaxed(true)) { return; }
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

        private void btnKwQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Boolean bInsert = mIsInsert && ExecutionQueueRecords.Count > 0;
                if (!EnvironmentIDs.Any())
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, bInsert ? "Insert failed" : "Queue failed");
                    return;
                }

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
                if (!bInsert)
                {
                    TestQueue.SelectedIndex = ExecutionQueueRecords.Count - 1;
                    TestQueue.ScrollIntoView(ExecutionQueueRecords[ExecutionQueueRecords.Count - 1]);
                }

                lnkManageLink.IsEnabled = true;
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

        private void lnkManageTagLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu ctx = new ContextMenu();
                ctx.Style = ((Style)FindResource("contextMenuStyle1"));
                ctx.Items.Clear();

                /* Do not display anything if no tags attached */
                if (mTestSuiteInfo.Tags.Any())
                {
                    MenuItem grpTags = new MenuItem();
                    grpTags.Header = "Tags"; // Tag group header
                    grpTags.IsEnabled = false;
                    grpTags.FontWeight = FontWeights.Bold;
                    grpTags.Foreground = new SolidColorBrush(Colors.Black);
                    grpTags.Margin = new Thickness(-20, 0, 0, 0);
                    ctx.Items.Add(grpTags);

                    var sortedTags = mTestSuiteInfo.Tags.OrderBy(x => x.Name).ToList();
                    //filter tags attached to the test which were removed/deleted from the central tags file.
                    sortedTags = DlkTag.LoadAllTags().FindAll(x => sortedTags.Any(y => y.Id == x.Id));
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
                if (selectedItemsIndex.Count > 1 & !mAskDependencyCancelled)
                {
                    foreach (var selectedIndex in selectedItemsIndex)
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

        private void btnEditTestInQueue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                if (!DlkEditorWindowHandler.IsEditorScriptOpened(rec.fullpath))
                {
                    if (DlkEditorWindowHandler.IsActiveEditorsMaxed(true)) { return; }
                    TestEditor te = new TestEditor(this, DlkEnvironment.mLibrary, System.IO.Path.Combine(DlkEnvironment.mDirTests, rec.folder.Trim('\\'), rec.file));
                    //te.Owner = this;
                    te.Show();
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

                    if (TestQueue.Items == null || TestQueue.Items.Count < 1)
                    {
                        lnkManageLink.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveSuite();
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
                }
                UpdateButtonStatesMultiSelect();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void TestQueue_EditMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItems = TestQueue.SelectedItems.Cast<DlkExecutionQueueRecord>().ToList();
                SuiteEditorTestRowEditorDialog rowEditorDialog = new SuiteEditorTestRowEditorDialog(this, selectedItems, EnvironmentIDs.ToList(), AllBrowsers);
                rowEditorDialog.Owner = this;
                rowEditorDialog.ShowDialog();
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
                if (!EnvironmentIDs.Any())
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT, bInsert ? "Insert failed" : "Queue failed");
                    return;
                }

                if (tvKeywordDirectory.SelectedItem == null)
                {
                    ShowError(DlkUserMessages.ERR_NO_TEST_TO_QUEUE);
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
                if (!bInsert)
                {
                    TestQueue.SelectedIndex = ExecutionQueueRecords.Count - 1;
                    TestQueue.ScrollIntoView(ExecutionQueueRecords[ExecutionQueueRecords.Count - 1]);
                }

                lnkManageLink.IsEnabled = true;
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
                    RefreshEnvironments();
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

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
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
                    bool isLastSelectedTestOrQueueEmpty = (ExecutionQueueRecords.Count == 0 || selectedIndex == ExecutionQueueRecords.Count - 1 || (ExecutionQueueRecords.Count == 1 && selectedIndex == 0));
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
                RefreshEnvironments();
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
                if (mIgnoreEnvironmentErrors)
                {
                    return;
                }
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
                        if (!cbBoxEnv.IsKeyboardFocusWithin)
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

        private void ComboBox_EnvironmentTextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox cbBoxEnv = (ComboBox)sender;
            try
            {
                if (!string.IsNullOrEmpty(cbBoxEnv.Text) && testSuiteEnvironmentPrevVal != null)
                {
                    if (EnvironmentIDs.Any(x => x == cbBoxEnv.Text) && cbBoxEnv.Text != testSuiteEnvironmentPrevVal)
                    {
                        mAllowQueueRefreshForKeepOpen = true;
                        isChanged = true;
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
                        ChangeKeepOpenBrowsers();
                        isChanged = true;
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
                if (bFuncRes)
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
        #endregion

        
    }
}
