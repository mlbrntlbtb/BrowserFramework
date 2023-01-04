using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using TestRunner.Common;
using Path = System.IO.Path;
using TestRunner.Designer;
using System.Text.RegularExpressions;
using System.Text;


namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestEditor.xaml
    /// </summary>
    public partial class TestEditor : Window, INotifyPropertyChanged
    {
        #region DECLARATIONS

        private const string STR_TEMP_COMMA_REPLACER = "!)@(#*$&%^";
        private const int SAVE_ACTION_ARG_IDX_CHECKINFLAG = 0;
        private const int SAVE_ACTION_ARG_IDX_FILEPATH = 1;
        private const int SAVE_ACTION_ARG_IDX_COMMENTS = 2;
        private const int SAVE_ACTION_ARG_IDX_SHOWSUCCESSMSG = 3;
        private const int SAVE_ACTION_ARG_IDX_RELOAD = 4;
        private const int SAVE_ACTION_ARG_EXACT_COUNT = 5;
        private const int SAVE_ACTION_RES_IDX_RET = 0;
        private const int SAVE_ACTION_RES_IDX_SHOWSUCCESSMSG = 1;
        private const int SAVE_ACTION_RES_IDX_RELOAD = 2;
        private const int SAVE_ACTION_RES_EXACT_COUNT = 3;
        private const int LAST_UPDATE_ARG_IDX_ISSERVER = 0;
        private const int LAST_UPDATE_ARG_IDX_KEYWORDPATH = 1;
        private const int LAST_UPDATE_ARG_IDX_DATAPATH = 2;
        private const int LAST_UPDATE_ARG_EXACT_COUNT = 3;
        private const int LAST_UPDATE_RES_IDX_KEYWORDDATE = 0;
        private const int LAST_UPDATE_RES_IDX_DATADATE = 1;
        private const int LAST_UPDATE_RES_IDX_KEYWORDUSER = 2;
        private const int LAST_UPDATE_RES_IDX_DATAUSER = 3;
        private const int LAST_UPDATE_RES_EXACT_COUNT = 4;
        private const int LAST_UPDATE_RESFILE_EXACT_LINE_COUNT = 3;
        private const int LAST_UPDATE_RESFILE_TARGET_LINE = 2;
        private const int LAST_UPDATE_RESFILE_USER_IDX = 2;
        private const int LAST_UPDATE_RESFILE_DATE_IDX = 3;
        private const string STATUS_TEXT_WAITING_FOR_SERVER = "Waiting for server response...";
        private const string STATUS_TEXT_SAVING = "Saving...";
        private const string STATUS_TEXT_RUNNING = "Running...";
        private const string STATUS_TEXT_PAUSED = "Paused";
        private const string STATUS_TEXT_CANCELLING = "Cancelling...";
        private const string TEXT_LABEL_PAUSE = "Pause";
        private const string TEXT_LABEL_RESUME = "Resume";
        private const string PATH_ICON_PAUSE = "Resources/recordpause.png";
        private const string PATH_ICON_RESUME = "Resources/recordstart.png";
        private const string FILTER_SELECT_ALL = "(Select all)";
        private const string FILTER_BLANK = "(Blank)";
        private const string TEST_EDITOR_TITLE = "Test Editor : ";
        private const string PASTE_INSERT = "Paste Insert";
        private const string PASTE_OVERWRITE = "Paste Overwrite";
        private const string CLEAR_ALL_ROWS = "Clear All Rows";
        private const string CLEAR_ALL = "Clear All";

        private static String mTemplate = DlkEnvironment.mDirTests + "template.dat";

        private string screenName = "";
        private string keyInput = "";
        private string keywordPreviousValue = "";
        private string controlPreviousType = "";
        private string controlPreviousValue = "";
        private string screenPreviousValue = "";
        private List<string> conversionSettings = new List<string>();
        private string conversionSettingsPreviousValue = "";
        private bool handlePasswordChanged = true;
        private bool isBlankPasswordClear = false;
        private bool isTestLibrary = false;
        private bool mAutoChangedDataCellSelection = false;
        private bool m_DataLoaded = false;
        private bool mResetColumnHeader = false;
        private bool mInitiaLoad = true;
        private bool mPasteOverwrite = true;
        private bool mClearAllRows = true;
        private bool _isChanged;
        private bool isChanged
        {
            get { return _isChanged; }
            set 
            { 
                _isChanged = value;
                if (_isChanged)
                {
                    string title = Title.Replace(TEST_EDITOR_TITLE, "");

                    if (!title.Contains("*"))
                    {
                        Title = TEST_EDITOR_TITLE + "*" + title;
                    }
                }
                else
                    Title = Title.Replace("*", "");
            }
        }

        private bool isPasswordLastControl = false;
        private bool isScreenValid = false;
        private bool isControlTabPressed = false;
        private bool isContextItemSelected = false;
        private bool isSelectedStepContiguous = false;
        private bool mColumnHeaderClicked = false;
        private bool isFilterAutoCheck = false;
        private bool hasGridFilter = false;
        private string mRowHeaderClicked = null;
        private Boolean bChangeParams = true;
        private static bool mWarnDuplicate = true;
        private ContextMenu mCurrentContextMenu = new ContextMenu();
        public string CurrentBrowser = "";
        public string CurrentEnvironment = "";
        private String mLoadedFile = "";
        private String mControlFilter = "";
        private const String SELECT_ALL = "Select All";
        
        public int CurrentInstance = 1;

        private DataGridColumnHeader mCurrentDataColumnHeader = null;
        private Views m_CurrentView;
        private FilterColumn m_NewestFilterColumn = FilterColumn.None;
        private FilterColumn m_TempFilter = FilterColumn.None;
        BackgroundWorker bw;
        private ISuiteEditor _owner;
        public DlkTest mLoadedTest;
        private string mRecentResultPath;


        private List<DlkTestStepRecord> tscrClipboard = new List<DlkTestStepRecord>();
        private List<DlkTestStepRecord> editedRrecords = new List<DlkTestStepRecord>();
        private List<DlkDataRecord> dscrClipboard = new List<DlkDataRecord>();
        private List<ControlItem> _ControlItems;
        private List<String> _Controls;
        private List<DataGridRow> mSelectedDataRows;
        private PasswordMasked mPasswordMasked;

        private int gridFiltersActive = 0;
        private Dictionary<FilterColumn, int> filterOrder = new Dictionary<FilterColumn, int>();

        private List<DlkKeywordHelpBoxRecord> m_Help = new List<DlkKeywordHelpBoxRecord>();
        private ObservableCollection<DlkTestStepRecord> _TestStepRecords;
        private ObservableCollection<DlkKeywordParameterRecord> _mKeywordParameters;
        private ObservableCollection<CheckedListItem<string>> _mCheckedListItems;
        private ObservableCollection<CheckedListItem<string>> _mExecuteFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>();
        private ObservableCollection<CheckedListItem<string>> _mScreenFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>();
        private ObservableCollection<CheckedListItem<string>> _mControlFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>();
        private ObservableCollection<CheckedListItem<string>> _mKeywordFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>();
        private ObservableCollection<CheckedListItem<string>> _mParametersFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>();
        private ObservableCollection<CheckedListItem<string>> _mDelayFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>();
        private List<string> mExecuteItems = new List<string>(new string[] { bool.TrueString, bool.FalseString });

        List<ControlList> _ControlTypeList = new List<ControlList>();
        private static DlkEmailInfo mEmailInfo = null;
        private BackgroundWorker mSaveWorker = new BackgroundWorker();
        private bool mIsTestEditorBusy = false;
        private bool mIsProgressBarVisible = true;
        public event PropertyChangedEventHandler PropertyChanged;
        private string mLastModifiedDateKeyword = string.Empty;
        private string mLastModifiedDateData = string.Empty;
        private string mLastModifiedByUserKeyword = string.Empty;
        private string mLastModifiedByUserData = string.Empty;
        private BackgroundWorker mServerInfoWorker = new BackgroundWorker();
        private string mStatusText = string.Empty;
        private string mSessionId = Guid.NewGuid().ToString();
        private bool mIsRunInProgress = false;
        private string mLastOutput = string.Empty;
        private string mCurrentControlTypeSnapshot = string.Empty;
        private string mCurrentKeywordSnapshot = string.Empty;
        private List<String> mRestrictedKWs = new List<string>();
        private static string[] RestrictedKeywords = new[]
        {
                "AssignPartialValueToVariable",
                "AssignValueToVariable",
                "GetValue"
        };

        private static string[] SpecialFunctionScreens = new[]
        {
                "BrowseFilePopup",
                "Query",
                "CP7Login",
                "FileUploadManager",
                "ShowHideScreenControls",
                "ArrangeTableColumns",
                "ProcessProgress",
                "Dialog",
                "DatePicker",
                "AutoComplete"
        };

        #endregion

        #region PROPERTIES
        /// <summary>
        /// Flag that indicates Test Editor is busy
        /// </summary>
        public bool IsTestEditorBusy
        {
            get
            {
                return mIsTestEditorBusy;
            }
            set
            {
                mIsTestEditorBusy = value;
                NotifyPropertyChanged("IsTestEditorBusy");
                NotifyPropertyChanged("IsTestInfoVisible");
            }
        }

        /// <summary>
        /// Flag that indicates 'template.dat' (New unsaved test) is loaded
        /// </summary>
        public bool IsTemplateLoaded
        {
            get
            {
                return !string.IsNullOrEmpty(mLoadedFile) && System.IO.Path.GetFileName(mLoadedFile) == "template.dat";
            }
            set
            {
                NotifyPropertyChanged("IsTemplateLoaded");
                NotifyPropertyChanged("IsTestInfoVisible");
            }
        }

        /// <summary>
        /// Flag that indicates if Local/Server file info toggle is visible
        /// </summary>
        public bool IsServerInfoToggleVisible
        {
            get
            {
                return DlkSourceControlHandler.SourceControlSupported && DlkSourceControlHandler.SourceControlEnabled;
            }
            set
            {
                NotifyPropertyChanged("IsServerInfoToggleVisible");
            }
        }

        /// <summary>
        /// Flag that indicates if Local/Server file info is visible
        /// </summary>
        public bool IsTestInfoVisible
        {
            get
            {
                return !IsTemplateLoaded && !IsTestEditorBusy;
            }
            set
            {
                NotifyPropertyChanged("IsTestInfoVisible");
            }
        }

        /// <summary>
        /// Flag that indicates whether Test Editor playback progress bar is shown/hidden
        /// </summary>
        public bool IsProgressBarVisible
        {
            get
            {
                return mIsProgressBarVisible;
            }
            set
            {
                mIsProgressBarVisible = value;
                NotifyPropertyChanged("IsProgressBarVisible");
            }
        }

        /// <summary>
        /// Last modified date of Keyword file (.xml)
        /// </summary>
        public string LastModifiedDateKeyword
        {
            get
            {
                return mLastModifiedDateKeyword;
            }
            set
            {
                mLastModifiedDateKeyword = string.IsNullOrEmpty(value) ? "NOT FOUND": value;
                NotifyPropertyChanged("LastModifiedDateKeyword");
            }
        }

        /// <summary>
        /// Last modified date of Data file (.trd)
        /// </summary>
        public string LastModifiedDateData
        {
            get
            {
                return mLastModifiedDateData;
            }
            set
            {
                mLastModifiedDateData = string.IsNullOrEmpty(value) ? "NOT FOUND" : value;
                NotifyPropertyChanged("LastModifiedDateData");
            }
        }

        /// <summary>
        /// Last modified By User in TFS for Keyword file. No value for local
        /// </summary>
        public string LastModifiedByUserKeyword
        {
            get
            {
                return string.IsNullOrEmpty(mLastModifiedByUserKeyword) ? string.Empty : "(" + mLastModifiedByUserKeyword + ")";
            }
            set
            {
                mLastModifiedByUserKeyword = value;
                NotifyPropertyChanged("LastModifiedByUserKeyword");
            }
        }

        /// <summary>
        /// Last modified By User in TFS for Data file. No value for local
        /// </summary>
        public string LastModifiedByUserData
        {
            get
            {
                return string.IsNullOrEmpty(mLastModifiedByUserData) ? string.Empty : "(" + mLastModifiedByUserData + ")";
            }
            set
            {
                mLastModifiedByUserData = value;
                NotifyPropertyChanged("LastModifiedByUserData");
            }
        }

        /// <summary>
        /// Flag that indicates whether a test run is in progress
        /// </summary>
        public bool IsRunInProgress
        {
            get
            {
                return mIsRunInProgress;
            }
            set
            {
                mIsRunInProgress = value;
                NotifyPropertyChanged("IsRunInProgress");
            }
        }

        /// <summary>e
        /// Data displayed in Output view
        /// </summary>
        public string Output
        {
            get
            {
                return mLastOutput;
            }
            set
            {
                mLastOutput = value;
                NotifyPropertyChanged("Output");
            }
        }

        /// <summary>
        /// Status text when Test Editor is busy
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
                NotifyPropertyChanged("StatusText");
            }
        }

        public static DlkEmailInfo EmailInfo
        {
            get
            {
                return mEmailInfo;
            }
            set
            {
                mEmailInfo = value;
            }
        }


        private List<ControlItem> ControlItems
        {
            get
            {
                return GetControls();
            }
        }

        private List<ControlList> ControlTypeList
        {
            get
            {
                if (_ControlTypeList == null)
                {
                    _ControlTypeList = new List<ControlList>(); ;
                    var ctrl = ControlItems;                    
                    foreach (var c in ControlItems)
                    {
                        if (!_ControlTypeList.Exists(x => x.ControlType == c.ControlType))
                            _ControlTypeList.Add(new ControlList(c.ControlType, true));
                    }

                    if (_ControlTypeList.Count > 0)
                    {
                        _ControlTypeList = _ControlTypeList.OrderBy(x => x.ControlType).ToList();
                        _ControlTypeList.Insert(0, new ControlList(SELECT_ALL, true));
                    }
                }
                return _ControlTypeList;
            }
            set { _ControlTypeList = value; }
        }

        private ObservableCollection<DlkTestStepRecord> mTestStepRecords
        {
            get
            {
                if (_TestStepRecords == null)
                {
                    _TestStepRecords = new ObservableCollection<DlkTestStepRecord>();
                }
                return _TestStepRecords;
            }
            set
            {
                _TestStepRecords = value;
            }
        }

        public bool DataTableHasColumns
        {
            get
            {
                return (mLoadedTest != null && mLoadedTest.Data != null && mLoadedTest.Data.Records.Count > 0);
            }
        }

        private List<String> mControls
        {
            get
            {
                if(_Controls == null)
                {
                    if (cmbComponent.SelectedItem != null)
                    {
                        if (cmbComponent.SelectedItem.ToString() == "Function")
                        {
                            _Controls = new List<string>() { "Function" };
                        }
                        else
                        {
                            _Controls = DlkDynamicObjectStoreHandler.GetControlKeys(screenName);
                        }
                    }
                }
                return _Controls;
            }
        }

        private List<String> mKeywords
        {
            get
            {
                List<String> mRes = new List<string>();
                if (cmbComponent.SelectedItem != null)
                {
                    String mControl = "";
                    if (String.IsNullOrEmpty(Convert.ToString(cmbControl.SelectedValue)))
                    {
                        mControl = screenName;
                    }
                    else if (cmbControl.Text == "Function" && cmbComponent.Text == "Function")
                    {
                        mControl = "Function";
                    }
                    else
                    {
                        mControl = DlkDynamicObjectStoreHandler.GetControlType(screenName, cmbControl.Text);
                    }
                    mRes = DlkAssemblyKeywordHandler.GetControlKeywords(mControl);
                }
                return mRes;
            }
        }
        private String mParameters
        {
            get
            {
                String mRes = "";
                if (cmbComponent.SelectedItem != null)
                {
                    String mControl = "";
                    if (cmbControl.Text == null)
                    {
                        mControl = cmbComponent.SelectedItem.ToString();
                    }
                    else if (cmbControl.Text == "Function")
                    {
                        mControl = "Function";
                    }
                    else
                    {
                        mControl = DlkDynamicObjectStoreHandler.GetControlType(cmbComponent.SelectedItem.ToString(), cmbControl.Text);
                    }
                    List<string> prms = DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, cmbKeyword.SelectedValue.ToString());
                    foreach (string param in prms)
                    {
                        mRes += param + "|";
                    }
                }
                return mRes.TrimEnd('|'); ;
            }
        }

        private List<DlkKeywordParameterRecord> mParameterList
        {
            get
            {
                //List<String> mRes = "";
                List<DlkKeywordParameterRecord> prms = new List<DlkKeywordParameterRecord>();
                if (cmbComponent.SelectedItem != null && cmbKeyword.SelectedValue != null)
                {
                    String mControl = "";
                    if (String.IsNullOrEmpty(cmbControl.Text))
                    {
                        mControl = screenName;
                    }
                    else if (cmbControl.Text == "Function" && cmbComponent.Text == "Function")
                    {
                        mControl = "Function";
                    }
                    else
                    {
                        mControl = DlkDynamicObjectStoreHandler.GetControlType(screenName, cmbControl.Text);
                    }
                    for (int idx = 0; idx < DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, cmbKeyword.SelectedValue.ToString()).Count; idx++)
                    {
                        prms.Add(new DlkKeywordParameterRecord(DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, cmbKeyword.SelectedValue.ToString())[idx], "", idx));
                    }
                }
                return prms;
            }
        }

        public ObservableCollection<DlkKeywordParameterRecord> mKeywordParameters
        {
            get
            {
                if (_mKeywordParameters == null)
                {
                    _mKeywordParameters = new ObservableCollection<DlkKeywordParameterRecord>();
                }
                return _mKeywordParameters;
            }
            set
            {
                _mKeywordParameters = value;
            }
        }

        public DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        private Views CurrentView
        {
            get
            {
                return m_CurrentView;
            }
            set
            {
                m_CurrentView = value;
                switch (m_CurrentView)
                {
                    case Views.Keyword:
                        dpKW.Visibility = System.Windows.Visibility.Visible;
                        dpData.Visibility = System.Windows.Visibility.Collapsed;
                        dpOutput.Visibility = System.Windows.Visibility.Collapsed;
                        gridSplitter.Visibility = System.Windows.Visibility.Hidden;
                        rowData.Height = new GridLength(0);
                        rowKeyword.Height = new GridLength(1, GridUnitType.Star);
                        rowSplitter.Height = new GridLength(0);
                        rowOutput.Height = new GridLength(0);
                        break;
                    case Views.Data:
                        dpKW.Visibility = System.Windows.Visibility.Collapsed;
                        dpData.Visibility = System.Windows.Visibility.Visible;
                        dpOutput.Visibility = System.Windows.Visibility.Collapsed;
                        gridSplitter.Visibility = System.Windows.Visibility.Hidden;
                        rowData.Height = new GridLength(1, GridUnitType.Star);
                        rowKeyword.Height = new GridLength(0);
                        rowSplitter.Height = new GridLength(0);
                        rowOutput.Height = new GridLength(0);
                        if (!m_DataLoaded || mResetColumnHeader)
                        {
                            PopulateData();
                            m_DataLoaded = true;
                            mResetColumnHeader = false;
                        }
                        break;
                    case Views.Split:
                        dpKW.Visibility = System.Windows.Visibility.Visible;
                        dpData.Visibility = System.Windows.Visibility.Visible;
                        dpOutput.Visibility = System.Windows.Visibility.Collapsed;
                        gridSplitter.Visibility = System.Windows.Visibility.Visible;
                        rowData.Height = new GridLength(1, GridUnitType.Star);
                        rowKeyword.Height = new GridLength(1, GridUnitType.Star);
                        rowSplitter.Height = GridLength.Auto;
                        rowOutput.Height = new GridLength(0);
                        if (!m_DataLoaded || mResetColumnHeader)
                        {
                            PopulateData();
                            m_DataLoaded = true;
                            mResetColumnHeader = false;
                        }
                        break;
                    case Views.Output:
                        dpKW.Visibility = System.Windows.Visibility.Collapsed;
                        dpData.Visibility = System.Windows.Visibility.Collapsed;
                        dpOutput.Visibility = System.Windows.Visibility.Visible;
                        gridSplitter.Visibility = System.Windows.Visibility.Hidden;
                        rowData.Height = new GridLength(0);
                        rowKeyword.Height = new GridLength(0);
                        rowSplitter.Height = new GridLength(0);
                        rowOutput.Height = new GridLength(1, GridUnitType.Star);
                        break;
                }
            }
        }

        private List<string> ExecuteItems
        {
            get
            {
                return mExecuteItems;
            }
            set
            {
                mExecuteItems = value;
            }
        }

        public ObservableCollection<CheckedListItem<string>> CheckedListItems
        {
            get
            {
                return _mCheckedListItems;
            }
            set
            {
                _mCheckedListItems = value;
                NotifyPropertyChanged("CheckedListItems");
            }
        }

        private string CurrentSelectedControlType
        {
            get
            {
                if (cmbControl.SelectedItem != null)
                {
                    return ((ControlItem)cmbControl.SelectedItem).ControlType;
                }
                else
                {
                    return DlkDynamicObjectStoreHandler.GetControlType(screenName, cmbControl.Text);
                }
            }
        }

        private string CurrentSelectedControlValue
        {
            get
            {
                if (cmbControl.SelectedItem != null)
                {
                    return ((ControlItem)cmbControl.SelectedItem).ControlName;
                }

                return string.Empty;
            }
        }

        #endregion

        #region ENUMS

        private enum Views
        {
            Keyword,
            Data,
            Split,
            Output
        }

        private enum FilterColumn
        {
            Execute,
            Screen,
            Control,
            Keyword,
            Parameters,
            Delay,
            None
        }
        #endregion

        #region CONSTRUCTOR

        public TestEditor(ISuiteEditor Owner, String AssemblyPath, String TestPath = "", bool TestLibrary = false)
        {
            InitializeComponent();
            InitializeContextMenuOptions();
            _owner = Owner;
            //Set initial window height.
            Height = 720;

            // Set start position
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            if (_owner is Window)
            {
                var window = _owner as Window;
                this.Left = window.Left;
                this.Top = window.Top;
            }

            // Load available keywords for product driving test editor
            DlkAssemblyKeywordHandler.Initialize(AssemblyPath);
            //CurrentInstance = Instance;

            cmbExecuteStep.ItemsSource = ExecuteItems;
            filterImg.Visibility = Visibility.Hidden;

            // Load keyword help data
            if (!File.Exists(DlkEnvironment.mHelpConfigFile) & !File.Exists(DlkEnvironment.mCommonHelpConfigFile))
            {
                lblHelp.Visibility = Visibility.Collapsed;
                dockHelp.Visibility = Visibility.Collapsed;
                txtHelp.Visibility = Visibility.Collapsed;
                borderHelp.Visibility = Visibility.Collapsed;
            }
            else
            {
                m_Help = new DlkKeywordHelpBoxHandler().mKeywordHelpBoxRecords.ToList<DlkKeywordHelpBoxRecord>();
            }

            // load the test
            if (DlkEnvironment.IsShowAppNameProduct)
            {
                infoImg.Visibility = Visibility.Visible;
                infoImg.ToolTip = DlkEnvironment.IsShowAppNameEnabled() ? DlkUserMessages.INF_TOOLTIP_SHOW_APP_NAME : DlkUserMessages.INF_TOOLTIP_SHOW_APP_ID;
            }

            if (string.IsNullOrEmpty(TestPath))
            {
                NewTest();
            }
            else
            {
                LoadTest(TestPath);
                UpdateInvalidDataColumns();
            }

            UpdateExportHTMLOptions();

            /* Add event handlers for data view grid item collection changed event */
            CollectionView dataCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(dgDataDriven.Items);
            ((INotifyCollectionChanged)dataCollectionView).CollectionChanged += new NotifyCollectionChangedEventHandler(dataViewCollection_Changed);

            /* Initialize UI properties of parameter field data context menu for this window, control itself is already initialized  */
            mCurrentContextMenu.ItemsPanel = (ItemsPanelTemplate)FindResource("MenuItemPanelTemplate");
            mCurrentContextMenu.Foreground = Brushes.DarkGreen;
            mCurrentContextMenu.FontWeight = FontWeights.Bold;
            mCurrentContextMenu.Width = Double.NaN;

            /* Initialize filter order values */
            filterOrder.Add(FilterColumn.Execute, 0);
            filterOrder.Add(FilterColumn.Screen, 0);
            filterOrder.Add(FilterColumn.Control, 0);
            filterOrder.Add(FilterColumn.Keyword, 0);
            filterOrder.Add(FilterColumn.Parameters, 0);
            filterOrder.Add(FilterColumn.Delay, 0);

            txtTestName.TextChanged += (o, e) => isChanged = true;
            txtTestDescription.TextChanged += (o, e) => isChanged = true;
            txtTestAuthor.TextChanged += (o, e) => isChanged = true;

            isTestLibrary = TestLibrary;

            /* Save worker */
            mSaveWorker.DoWork += mSaveWorker_DoWork;
            mSaveWorker.RunWorkerCompleted += mSaveWorker_RunWorkerCompleted;

            /* Last update info */
            GetLocalTestLastUpdateInfo(mLoadedTest.mTestPath, mLoadedTest.Data != null ?mLoadedTest.Data.Path : string.Empty,
                out mLastModifiedDateKeyword, out mLastModifiedDateData, out mLastModifiedByUserKeyword, out mLastModifiedByUserData);

            mServerInfoWorker.DoWork += mServerInfoWorker_DoWork;
            mServerInfoWorker.RunWorkerCompleted += mServerInfoWorker_RunWorkerCompleted;

            /* Set output datacontext to default */
            Output = DlkOutputManifestHandler.GetLastOutput(mLoadedTest.mTestPath);
            txtOutput.DataContext = this;

            if(string.IsNullOrEmpty(Output))
            {
                btnSaveTestLogs.IsEnabled = false;
                btnEmailTestLogs.IsEnabled = false;
            }

            UpdateRecentResults("");
        }
        #endregion

        #region EVENTS

        private void filterImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            ContextMenu contextMenu = image.ContextMenu;

            if (e.ChangedButton == MouseButton.Left)
            {
                contextMenu.PlacementTarget = image;
                contextMenu.IsOpen = true;
                contextMenu.Visibility = Visibility.Visible;
            }
            else 
            {
                contextMenu.PlacementTarget = null;
                contextMenu.IsOpen = false;
                contextMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void filterControls_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem)
            {
                var mi = (MenuItem)sender;
                if (mi.Header.ToString() == SELECT_ALL)
                {
                    if (mi.IsChecked)
                    {
                        RefreshAllControls(true);
                        cmbControl.DataContext = ControlItems;
                    }
                    else
                    {
                        RefreshAllControls();
                        cmbControl.DataContext = null;
                    }
                    cmbControl.Text = "";
                    mControlFilter = "";
                }
                else
                {
                    if (ControlTypeList != null)
                    {
                        //update selected controltype
                        ControlTypeList.Find(x => x.ControlType == mi.Header.ToString()).Selected = mi.IsChecked;

                        //uncheck/check Select All based on selected controltypes
                        if (ControlTypeList.Exists(x => x.Selected == false && x.ControlType != SELECT_ALL))
                        {
                            ControlTypeList.Find(x => x.ControlType == SELECT_ALL).Selected = false;
                        }
                        else
                        {
                            ControlTypeList.Find(x => x.ControlType == SELECT_ALL).Selected = true;
                        }
                        ctrlFilterMenu.Items.Refresh();
                    }

                    List<string> filteredControlType = GetSelectedControls();
                    cmbControl.DataContext = GetFilteredControlsByType(filteredControlType);
                    cmbControl.Text = "";
                    mControlFilter = "";
                }
            }
        }

        private void btnPasteType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is FrameworkElement addButton)
                {
                    addButton.ContextMenu.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void SearchPasteOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem selectedMenu = sender as MenuItem;

                if (!selectedMenu.IsChecked)
                {
                    selectedMenu.IsChecked = true;
                    return;
                }

                foreach (MenuItem menuItem in cmnuPasteType.Items)
                {
                    if (menuItem.Header != selectedMenu.Header)
                        menuItem.IsChecked = false;
                    else
                    {
                        btnTestStepPaste.ToolTip = menuItem.Header;
                        mPasteOverwrite = menuItem.Header.ToString() == PASTE_OVERWRITE;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void SearchClearOption_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem selectedMenu = sender as MenuItem;

                if (!selectedMenu.IsChecked)
                {
                    selectedMenu.IsChecked = true;
                    return;
                }

                foreach (MenuItem menuItem in cmnuClearType.Items)
                {
                    if (menuItem.Header != selectedMenu.Header)
                        menuItem.IsChecked = false;
                    else
                    {
                        btnClearRow.ToolTip = menuItem.Header;
                        mClearAllRows = menuItem.Header.ToString() == CLEAR_ALL_ROWS;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is FrameworkElement addButton)
                {
                    addButton.ContextMenu.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnPopulateDataView_Click(object sender, RoutedEventArgs e)
        {
            string errMessage;
            try
            {
                List<int> stepsForDataGeneration = GetSelectedStep();
               
                if (ValidateDataGeneration(out errMessage))
                {
                    if (CurrentView != Views.Split)
                    {
                        optSplitView.IsChecked = true;
                        UnselectOtherToggles(new List<RadioButton>() { optDataView, optKWView });
                        tabKW.Header = "Keyword/Data View";
                        CurrentView = Views.Split;
                    }
                    
                    //Ask if to Convert with Modifications
                    ConvertToDataModificationDialog ctdmDialog = new ConvertToDataModificationDialog();
                    ctdmDialog.Owner = this.IsVisible ? this : this.Owner;
                    ctdmDialog.ShowDialog();

                    if (ctdmDialog.ModifyConvertDataChoice == ConvertToDataModificationDialog.ModifyConvertDataOptions.ModifyAndConvert) /* User saves AND checks-in */
                    {
                        //Convert selected parameters only
                        ConvertToDataModificationWindow ctdmWindow = new ConvertToDataModificationWindow(this, DlkEnvironment.mLibrary, mLoadedTest, stepsForDataGeneration);
                        if (ctdmWindow.ShowDialog() == true)
                        {
                            //include param conversion setting
                            PopulateData(null, stepsForDataGeneration, ctdmWindow.ConvertParametersSettingsDict);
                            isChanged = true;
                        }
                    }
                    else if (ctdmDialog.ModifyConvertDataChoice == ConvertToDataModificationDialog.ModifyConvertDataOptions.ConvertOnly) 
                    {
                        //Convert all parameters
                        PopulateData(null, stepsForDataGeneration);
                        isChanged = true;
                    }

                    //update currently shown Parameters
                    DlkTestStepRecord mRec = mTestStepRecords[stepsForDataGeneration.FirstOrDefault() - 1];
                    string[] myParams = mRec.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                    mKeywordParameters.Clear();
                    dgParameters.DataContext = null;
                    //for(int idx = 0; idx < myParams.Count(); idx++)
                    for (int idx = 0; idx < mParameterList.Count(); idx++)
                    {
                        mKeywordParameters.Add(new DlkKeywordParameterRecord(mParameterList[idx].mParameterName, myParams[idx], idx));
                    }
                    dgParameters.ItemsSource = mKeywordParameters;
                    dgParameters.Items.Refresh();

                }
                else
                {
                    ShowError(errMessage);
                }

                UpdateStepButtonStates();
                ToggleConvertToData();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private List<int> GetSelectedStep()
        {
            List<int> selectedStepNumber = new List<int>();

            if (dgTestSteps.SelectedItems.Count > 0)
            {
                foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
                {
                    selectedStepNumber.Add(dsr.mStepNumber);
                }
            }

            return selectedStepNumber;
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ToolBar toolBar = sender as ToolBar;

                var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
                if (mainPanelBorder != null)
                {
                    mainPanelBorder.Margin = new Thickness(0);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgTestSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                isPasswordLastControl = false;
                bChangeParams = false;
                UpdateStepButtonStates();
                UpdateExportHTMLOptions();
                ToggleConvertToData();

                if (dgTestSteps.SelectedIndex >= 0)
                {
                    if (dgTestSteps.SelectedItems.Count > 1)
                    {
                        btnTestStepPaste.IsEnabled = false;
                        SetIsEnablePropertyOnMultiSelect(false);
                        btnTestStepDelete.IsEnabled = !hasGridFilter;
                    }
                    else
                    {
                        if (!CheckClipboardEmpty("TestStepCollection"))
                        {
                            btnTestStepPaste.IsEnabled = !hasGridFilter;
                        }
                        SetIsEnablePropertyOnMultiSelect(true);
                    }

                    DlkTestStepRecord mRec = mTestStepRecords[dgTestSteps.SelectedIndex];
                    lblStep.Content = "Step " + mRec.mStepNumber;
                    String mEx = mRec.mExecute;
                    cmbExecuteStep.Text = mEx;

                    // Clean Slate
                    CleanUpdatePanel();

                    screenPreviousValue = mRec.mScreen;
                    isScreenValid = mRec.mScreen != "" ? true : false;
                    cmbComponent.Text = "";
                    cmbControl.Text = "";
                    PopulateControls();
                    if (DlkDynamicObjectStoreHandler.StillLoading)
                    {
                        cmbComponent.Text = mRec.mScreen;
                        //txtControl.Text = mRec.mControl;
                        cmbControl.Text = mRec.mControl;
                        cmbKeyword.Text = mRec.mKeyword;
                    }
                    else
                    {
                        if (mRec.mScreen != "")
                        {
                            cmbComponent.SelectedItem = mRec.mScreen;
                        }
                        cmbControl.Text = mRec.mControl;
                        cmbControl.SelectedValue = mRec.mControl;
                        RefreshKeywords();
                        cmbKeyword.SelectedValue = mRec.mKeyword;
                    }
                    txtDelay.Text = mRec.mStepDelay.ToString();
                    if (txtDelay.Text == "")
                    {
                        txtDelay.Text = "0";
                    }

                    //Record snapshot
                    mCurrentKeywordSnapshot = mRec.mKeyword;
                    mCurrentControlTypeSnapshot = CurrentSelectedControlType;
                    controlPreviousType = CurrentSelectedControlType;
                    controlPreviousValue = cmbControl.Text;

                    RefreshParameters(mRec);

                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                        new Action(delegate()
                        {
                            ColorParams();
                            ScrollToSelectedItem(dgTestSteps.SelectedIndex, e);
                        }));
                    foreach (DlkKeywordParameterRecord rec in mKeywordParameters)
                    {
                        if (rec.mValue.Contains("D{"))
                        {
                            UpdateDataTabSelection(rec.mValue.TrimStart('D').TrimStart('{').TrimEnd('}'));
                            break;
                        }
                    }
                }
                if (txtHelp.Visibility == Visibility.Visible)
                {
                    FillHelpBoxInfo();
                }
                //#if DEBUG

                //#else
                //           FillHelpBoxInfo();
                //#endif
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ToggleConvertToData()
        {
            if (IsParameterConvertible())
            {
                btnPopulateDataView.IsEnabled = !hasGridFilter;
            }
            else
            {
                btnPopulateDataView.IsEnabled = false;
            }

        }

        private bool IsParameterConvertible()
        {
            bool isConvertible = false;

            foreach (DlkTestStepRecord step in dgTestSteps.SelectedItems)
            {
                if (step.mKeyword == "Click" || step.mKeyword == "Close" || (string.IsNullOrEmpty(step.mControl) && step.mScreen != "Database")
                    || step.mParameters.Count <= 0 || String.IsNullOrEmpty(step.mParameterOrigString) || IsStepPasswordBlank(step)) continue;

                foreach (var param in step.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None).ToList())
                {
                    if (!DlkData.IsDataDrivenParam(param))
                    {
                        isConvertible = true;
                        break;
                    }
                }
                if (isConvertible)
                    break;
            }

            return isConvertible;
        }

        private bool IsStepPasswordBlank(DlkTestStepRecord step)
        {
            return step.mPasswordParameters == null ? false :
                    step.mPasswordParameters.Count == 1 && step.mPasswordParameters.All(x => String.IsNullOrEmpty(x));
        }

        private void UpdateExportHTMLOptions()
        {
            if ((dgTestSteps.Items.Count > 0 || dgDataDriven.Items.Count > 0) || gridFiltersActive > 0)
            {
                ExportHTML.Visibility = Visibility.Visible;
            }
            else
                ExportHTML.Visibility = Visibility.Collapsed;
        }

        private void dgDataDriven_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mSelectedDataRows = new List<DataGridRow>();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgDataDriven_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                e.Row.Header = string.Format("{0:00000}", e.Row.GetIndex() + 1);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgDataDriven_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                UpdateExportHTMLOptions();
                ToggleButtonAndMenusDgData();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgDataDriven_ColumnHeaderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var columnHeader = sender as DataGridColumnHeader;
                if (columnHeader != null)
                {
                    DataGridColumn col = null;

                    foreach (DataGridColumn dgc in dgDataDriven.Columns)
                    {
                        if (dgc.Header.ToString() == columnHeader.Content.ToString())
                        {
                            col = dgc;
                            break;
                        }
                    }

                    if (col != null)
                    {
                        mAutoChangedDataCellSelection = true;
                        mCurrentDataColumnHeader = columnHeader;
                        btnDeleteRow.IsEnabled = false;
                        menuitemDeleteRow.IsEnabled = btnDeleteRow.IsEnabled;
                        btnDeleteData.IsEnabled = true;
                        btnRenameData.IsEnabled = true;
                        btnInsertRow.IsEnabled = false;
                        menuitemInsertRow.IsEnabled = btnInsertRow.IsEnabled;
                        btnUpData.IsEnabled = false;
                        menuitemUpData.IsEnabled = btnUpData.IsEnabled;
                        btnDownData.IsEnabled = false;
                        menuitemDownData.IsEnabled = btnDownData.IsEnabled;
                        btnLeftData.IsEnabled = true && col.DisplayIndex > 0;
                        btnRightData.IsEnabled = true && col.DisplayIndex < dgDataDriven.Columns.Count - 1;
                        btnCopyRow.IsEnabled = false;
                        btnPasteRow.IsEnabled = false;
                        menuitemCopyRow.IsEnabled = false;
                        menuitemPasteRow.IsEnabled = false;
                        dgDataDriven.SelectionUnit = DataGridSelectionUnit.Cell;
                        dgDataDriven.SelectionMode = DataGridSelectionMode.Extended;
                        dgDataDriven.SelectedCells.Clear();
                        foreach (var item in dgDataDriven.Items)
                        {
                            dgDataDriven.SelectedCells.Add(new DataGridCellInfo(item, col));
                        }
                        mAutoChangedDataCellSelection = false;
                        mColumnHeaderClicked = true;
                        mResetColumnHeader = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgDataDriven_RowHeaderClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var rowHeader = sender as DataGridRowHeader;
                if (rowHeader != null)
                {
                    mRowHeaderClicked = rowHeader.Content.ToString();
                    int rowIndex = GetRowIndexFromDataGrid(dgDataDriven, mRowHeaderClicked);
                    DataGridRow row = (DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromIndex(rowIndex);

                    btnDeleteRow.IsEnabled = true;
                    btnInsertRow.IsEnabled = true;
                    dgDataDriven.SelectionMode = DataGridSelectionMode.Extended;

                    if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) || mColumnHeaderClicked)
                    {
                        mSelectedDataRows.Clear();
                        dgDataDriven.SelectedCells.Clear();
                    }
                    else if (dgDataDriven.Columns.Count > 1 && dgDataDriven.SelectedCells.Count == 1)
                    {
                        mSelectedDataRows.Clear();
                        dgDataDriven.SelectedCells.Clear();
                    }

                    if (mSelectedDataRows.Contains(row))
                    {
                        mSelectedDataRows.Clear();
                        dgDataDriven.SelectedCells.Clear();
                        mSelectedDataRows.Add(row);
                    }
                    else
                    {
                        mSelectedDataRows.Add(row);
                    }
                    //select each cell of the row
                    foreach (DataGridColumn dgc in dgDataDriven.Columns)
                    {
                        DataGridCellInfo cellToSelect = new DataGridCellInfo(row.Item, dgc);
                        dgDataDriven.SelectedCells.Add(cellToSelect);
                    }
                    mRowHeaderClicked = null;
                    mColumnHeaderClicked = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgDataDriven_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                string header = e.Column.Header.ToString();

                // Replace all underscores with two underscores, to prevent AccessKey handling
                e.Column.Header = header.Replace("_", "__");
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnAddData.IsEnabled) // for Context Menu Item - won't function if button is disabled
                {
                    AddDataColumn frm = new AddDataColumn(mLoadedTest.Data);
                    frm.Owner = this;
                    if ((bool)frm.ShowDialog())
                    {
                        PopulateData();
                        UpdateStepButtonStates();
                        isChanged = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnDeleteData.IsEnabled) // for Context Menu Item - won't function if button is disabled
                {
                    var targetColumnHeader = GetColumnName(mCurrentDataColumnHeader.Content.ToString());
                    if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_COLUMN + targetColumnHeader + "' column?", "Delete Column") == MessageBoxResult.Yes)
                    {
                        mLoadedTest.Data.DeleteColumn(targetColumnHeader);

                        PopulateData();
                        isChanged = true;
                        btnRenameData.IsEnabled = false;
                        btnDeleteData.IsEnabled = false;
                        btnLeftData.IsEnabled = false;
                        btnRightData.IsEnabled = false;
                        UpdateStepButtonStates();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnLeftData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnLeftData.IsEnabled) // for Context Menu Item - won't function if button is disabled
                {
                    int targetColIndex = mLoadedTest.Data.Records.FindIndex(rec => rec.Name == GetColumnName(mCurrentDataColumnHeader.Content.ToString()));
                    mLoadedTest.Data.MoveColumn(targetColIndex, left: true);

                    PopulateData();
                    dgDataDriven_ColumnHeaderClick(mCurrentDataColumnHeader, null);
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRightData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnRightData.IsEnabled) // for Context Menu Item - won't function if button is disabled
                {
                    int targetColIndex = mLoadedTest.Data.Records.FindIndex(rec => rec.Name == GetColumnName(mCurrentDataColumnHeader.Content.ToString()));
                    mLoadedTest.Data.MoveColumn(targetColIndex, left: false);

                    PopulateData();
                    dgDataDriven_ColumnHeaderClick(mCurrentDataColumnHeader, null);
                    isChanged = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnUpData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetDataRowColPosition(out int rowIndex, out int colIndex);

                if (rowIndex > -1 && colIndex > -1)
                {
                    dgDataDriven.CommitEdit();
                    mLoadedTest.Data.MoveRow(rowIndex, up: true);
                    PopulateData();
                    UpdateDataTabSelection(colIndex, rowIndex - 1);
                    isChanged = true;
                }
                else
                {
                    btnDownData.IsEnabled = true;
                    menuitemDownData.IsEnabled = btnDownData.IsEnabled;
                    btnUpData.IsEnabled = false;
                    menuitemUpData.IsEnabled = btnUpData.IsEnabled;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDownData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetDataRowColPosition(out int rowIndex, out int colIndex);

                if (rowIndex > -1 && colIndex > -1)
                {
                    dgDataDriven.CommitEdit();
                    mLoadedTest.Data.MoveRow(rowIndex, up: false);
                    PopulateData();
                    UpdateDataTabSelection(colIndex, rowIndex + 1);
                    isChanged = true;
                }
                else
                {
                    btnDownData.IsEnabled = false;
                    menuitemDownData.IsEnabled = btnDownData.IsEnabled;
                    btnUpData.IsEnabled = true;
                    menuitemUpData.IsEnabled = btnUpData.IsEnabled;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Gets the row and column index of the last selected cell in Data driven grid
        /// </summary>
        /// <param name="rowIndex">target cell row index</param>
        /// <param name="colIndex">target cell column index</param>
        private void GetDataRowColPosition(out int rowIndex, out int colIndex)
        {
            if (dgDataDriven.SelectedCells.Count > 0)
            {
                var targetCell = dgDataDriven.SelectedCells.Last();
                DataGridRow rowToMove = (DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(targetCell.Item);
                rowIndex = rowToMove.GetIndex();
                colIndex = targetCell.Column.DisplayIndex;
            }
            else
            {
                rowIndex = -1;
                colIndex = -1;
            }
        }

        private void cmbKeyword_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                bChangeParams = true;
                keywordPreviousValue = !cmbKeyword.Items.Contains(cmbKeyword.Text) ? cmbKeyword.Text : cmbKeyword.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddStep();
                //PopulateTestSteps(CurrentTestInstance);
                PopulateTestSteps();
                dgTestSteps.SelectedIndex = mTestStepRecords.Count - 1;
                Keyboard.Focus(cmbComponent);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTestSteps.SelectedIndex == -1)
                {
                    return;
                }
                //clear previous content//
                tscrClipboard.Clear();
                List<DlkTestStepRecord> gridSteps = dgTestSteps.SelectedItems.Cast<DlkTestStepRecord>().ToList();
                if (gridSteps.All(x => String.IsNullOrEmpty(x.mScreen)))
                {
                    return;
                }
                //add steps
                foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
                {
                    tscrClipboard.Add(dsr);
                }
                tscrClipboard = tscrClipboard.OrderBy(x => x.mStepNumber).ToList();

                //set content to clipboard//
                Clipboard.SetData("TestStepCollection", tscrClipboard);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepPaste_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTestSteps.SelectedIndex == -1)
                {
                    return;
                }
                bool isDestinationStepBlank = !mPasteOverwrite;

                //put clipboard content into collection//
                tscrClipboard = (List<DlkTestStepRecord>)Clipboard.GetData("TestStepCollection");
                int currStep = dgTestSteps.SelectedIndex;
                if (!mPasteOverwrite && !String.IsNullOrEmpty(mLoadedTest.mTestSteps[currStep].mScreen))
                {
                    isDestinationStepBlank = false;
                    currStep++;
                }
                else if (mPasteOverwrite)
                {
                    for (int i = currStep; i < currStep + tscrClipboard.Count; i++)
                    {
                        if (mLoadedTest.mTestSteps.Count - 1 < i)
                            break;
                        else if (!String.IsNullOrEmpty(mLoadedTest.mTestSteps[i].mScreen))
                        {
                            if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_PASTE_OVERWRITE) == MessageBoxResult.No)
                                return;
                            else
                                break;
                        }                        
                    }
                }

                List<int> pastedItemIndex = new List<int>();
                foreach (DlkTestStepRecord src in tscrClipboard)
                {
                    DlkTestStepRecord newStep = new DlkTestStepRecord();
                    newStep.mExecute = src.mExecute;
                    newStep.mScreen = src.mScreen;
                    newStep.mControl = src.mControl;
                    newStep.mKeyword = src.mKeyword;
                    newStep.mParameters = new List<string>(src.mParameters);

                    if (src.mPasswordParameters != null)
                    {
                        newStep.mPasswordParameters = newStep.mPasswordParameters ?? new List<string>(src.mPasswordParameters);
                    }

                    newStep.mStepDelay = src.mStepDelay;
                    newStep.mStepStatus = src.mStepStatus;
                    newStep.mStepLogMessages = new List<DlkLoggerRecord>(src.mStepLogMessages);
                    newStep.mStepStart = src.mStepStart;
                    newStep.mStepEnd = src.mStepEnd;
                    newStep.mStepElapsedTime = src.mStepElapsedTime;

                    //insert step
                    if (isDestinationStepBlank)
                    {
                        isDestinationStepBlank = false;
                        mLoadedTest.mTestSteps.Remove(mLoadedTest.mTestSteps[currStep]);
                    }                  
                    
                    if(!mPasteOverwrite || currStep > mLoadedTest.mTestSteps.Count - 1)
                        mLoadedTest.mTestSteps.Insert(currStep, newStep);
                    else
                        mLoadedTest.mTestSteps[currStep] = newStep;

                    // renumber all succeeding steps
                    for (int idx = newStep.mStepNumber; idx < mLoadedTest.mTestSteps.Count; idx++)
                    {
                        mLoadedTest.mTestSteps[idx].mStepNumber = idx + 1;
                    }

                    pastedItemIndex.Add(newStep.mStepNumber - 1);
                    currStep++;
                }

                PopulateTestSteps();
                UpdateRowSelection(pastedItemIndex);
                isChanged = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string question = "";
                if (dgTestSteps.SelectedItems.Count == 1)
                {
                    question = DlkUserMessages.ASK_DELETE_STEP + (dgTestSteps.SelectedIndex + 1).ToString() + "?";
                }
                else if (dgTestSteps.SelectedItems.Count > 1)
                {
                    List<int> indexList = new List<int>();
                    for (int num = 0; num < dgTestSteps.SelectedItems.Count; num++)
                    {
                        indexList.Add(dgTestSteps.Items.IndexOf(dgTestSteps.SelectedItems[num]));
                    }
                    bool isConsecutive = !indexList.Select((i, j) => i - j).Distinct().Skip(1).Any();
                    if (isConsecutive) 
                    {
                        question = DlkUserMessages.ASK_DELETE_STEPS + (dgTestSteps.Items.IndexOf(dgTestSteps.SelectedItems[0]) + 1).ToString() + " - " + (dgTestSteps.Items.IndexOf(dgTestSteps.SelectedItems[dgTestSteps.SelectedItems.Count - 1]) + 1).ToString() + "?";
                    }
                    else 
                    {
                        question = DlkUserMessages.ASK_DELETE_SELECTED_STEPS;
                    }
                }
                if (DlkUserMessages.ShowQuestionYesNo(question, "Delete Test Step/s?") == MessageBoxResult.Yes)
                {
                    var currSelectedIndex = DeleteStep();
                    PopulateTestSteps();

                    //add a new row if all test steps were deleted
                    if (mLoadedTest.mTestSteps.Count == 0)
                    {
                        DlkUserMessages.ShowInfo(DlkUserMessages.INF_EMPTY_TEST);
                        AddStep();
                        currSelectedIndex = 0;
                        PopulateTestSteps();
                    }

                    dgTestSteps.Items.Refresh();
                    dgTestSteps.SelectedIndex = currSelectedIndex;                    
                    SelectPasswordControl();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTestSteps.SelectedItems.Count > 1)
                {
                    if (ValidateUpdate())
                    {
                        if (Boolean.TryParse(cmbExecuteStep.Text, out bool res))
                        {
                            cmbExecuteStep.Text = SetExecuteStepCasing(cmbExecuteStep.Text);
                        }
                        UpdateStepsExecution();
                        if (gridFiltersActive > 0)
                        {
                            ReapplyFilter();
                        }
                        else
                        {
                            List<DlkTestStepRecord> lstSteps = new List<DlkTestStepRecord>();
                            foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
                            {
                                lstSteps.Add(dsr);
                            }
                            LoadPasswordSteps();
                            PopulateTestSteps();
                            foreach (var item in lstSteps)
                            {
                                dgTestSteps.SelectedItems.Add(item);
                            }
                        }
                        dgTestSteps_SelectionChanged(null, null);
                    }
                }
                else
                {
                    int iCurrentRec = dgTestSteps.SelectedIndex;
                    if (iCurrentRec >= 0)
                    {
                        if (ValidateUpdate())
                        {
                            if (Boolean.TryParse(cmbExecuteStep.Text, out bool res))
                            {
                                cmbExecuteStep.Text = SetExecuteStepCasing(cmbExecuteStep.Text);
                            }
                            dgDataDriven.Focus(); // remove focus from dgParameters to commit edit - for shortcut key
                            dgParameters.Focus();
                            dgParameters.CommitEdit();
                            dgParameters.CommitEdit(); // need to commit twice to save - for shortcut key
                            dgParameters.Items.Refresh();
                            UpdateStep();
                            //PopulateTestSteps(CurrentTestInstance);
                            if (gridFiltersActive > 0)
                            {
                                ReapplyFilter();
                            }
                            else
                            {
                                LoadPasswordSteps();
                                PopulateTestSteps();
                            }
                            dgTestSteps.SelectedIndex = iCurrentRec;
                        }
                    }
                }
                mResetColumnHeader = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int currSelectedIndex = dgTestSteps.SelectedIndex;
                if (dgTestSteps.Items.Count == 0 || currSelectedIndex <= 0)
                {
                    // do nothing
                }
                else
                {
                    List<DlkTestStepRecord> lstSteps = new List<DlkTestStepRecord>();
                    foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
                    {
                        
                        lstSteps.Add(dsr);
                    }
                    MoveUpStep(lstSteps);                    
                    PopulateTestSteps();
                    foreach (var item in lstSteps)
                    {
                        dgTestSteps.SelectedItems.Add(item);
                    }                    
                    //MoveUpStep();
                    ////PopulateTestSteps(CurrentTestInstance);
                    //PopulateTestSteps();
                    //dgTestSteps.SelectedIndex = currSelectedIndex - 1;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int currSelectedIndex = dgTestSteps.SelectedIndex;
                if (dgTestSteps.Items.Count == 0 || currSelectedIndex == dgTestSteps.Items.Count - 1
                    || currSelectedIndex == -1)
                {
                    // do nothing
                }
                else
                {
                    List<DlkTestStepRecord> lstSteps = new List<DlkTestStepRecord>();                    
                    foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
                    {
                        lstSteps.Add(dsr);
                    }                    
                    MoveDownStep(lstSteps);                    
                    //PopulateTestSteps(CurrentTestInstance);
                    PopulateTestSteps();
                    //dgTestSteps.SelectedIndex = currSelectedIndex + 1;
                    foreach (var item in lstSteps)
                    {
                        dgTestSteps.SelectedItems.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSaveTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.IO.Path.GetFileName(mLoadedTest.mTestPath) == "template.dat")
                {
                    SaveTestAs();
                }
                else
                {
                    //mLoadedTest.SaveTest(mLoadedFile);
                    SaveTest("Save");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnNewTest_Click(object sender, RoutedEventArgs e)
        {
            //LoadTest(mTemplate);
            try
            {
                if (isChanged)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_SAVE_SCRIPT_BEFORE_CREATING_NEW, "Save Changes?");
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            SaveTestAs();
                            break;
                        case MessageBoxResult.No:
                            NewTest();
                            break;
                        default:
                            break;
                    }
                }              
                else
                {
                    NewTest();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSaveTestAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveTestAs("Save As");
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnNewRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mLoadedTest.Data.NewRow();
                PopulateData();
                isChanged = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mSelectedDataRows.RemoveAll(item => item == null);
                if (dgDataDriven.SelectedCells.Count > 0)
                {
                    dgDataDriven.CommitEdit();
                    if (mSelectedDataRows.Count < 1)
                        mSelectedDataRows.Add((DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(dgDataDriven.SelectedCells[0].Item));

                    bool confirmDeletion = false;
                    if (mSelectedDataRows.Count == 1)
                        confirmDeletion = DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_ROW + mSelectedDataRows[0].Header.ToString() + "' ?", "Delete Row") == MessageBoxResult.Yes ? true : false;
                    else
                        confirmDeletion = DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_DELETE_ROWS, "Delete Multiple Rows") == MessageBoxResult.Yes ? true : false;

                    if (confirmDeletion)
                    {
                        int rowIndexToBeRemoved;
                        foreach (DataGridRow dataRow in mSelectedDataRows.ToList())
                        {
                            rowIndexToBeRemoved = GetRowIndexFromDataGrid(dgDataDriven, dataRow.Header.ToString());
                            mLoadedTest.Data.FlagRowDelete(rowIndexToBeRemoved);
                        }
                        mLoadedTest.Data.DeleteRowValues();
                        PopulateData();     
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (mClearAllRows)
                {
                    if (!mLoadedTest.Data.Records.Any(val => val.Values.Count > 0))
                    {
                        DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ROWS_TO_CLEAR);
                        return;
                    }
                    else if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_CLEAR_DATAVIEW_ROWS) == MessageBoxResult.Yes)
                    {
                        mLoadedTest.Data.ClearAllRecordValues();               
                        isChanged = true;
                    }
                    else
                        return;
                }
                else
                {
                    if (DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_CLEAR_DATAVIEW_ALL) == MessageBoxResult.Yes)
                    {
                        mLoadedTest.Data.Records.Clear();
                        isChanged = true;                        
                    }
                    else
                        return;
                }
                PopulateData();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnInsertData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Header = String.Empty;
                GetDataRowColPosition(out int rowIndex, out int columnIndex);
                
                if (mLoadedTest.Data.Records.Count > 0)
                {
                    if (btnDeleteRow.IsEnabled) // if user inserts while cell is highlighted
                    {
                        if (columnIndex == -1 && dgDataDriven.CurrentColumn != null)
                        {
                            Header = dgDataDriven.CurrentColumn.Header.ToString();
                        }
                        else
                        {
                            Header = mLoadedTest.Data.Records[columnIndex].Name;
                        }
                    }
                    else if (btnDeleteData.IsEnabled) // if user inserts while column is highlighted
                    {
                        Header = mCurrentDataColumnHeader.Content.ToString();
                    }

                    if (columnIndex == -1)
                    {                        
                        columnIndex = mLoadedTest.Data.Records.FindIndex(rec => rec.Name == Header);
                    }
                }
                AddDataColumn frm = new AddDataColumn(mLoadedTest.Data, "", columnIndex) { Owner = this };
                if ((bool)frm.ShowDialog())
                {
                    PopulateData();
                    isChanged = true;
                    if (dgDataDriven.Items.Count > 0)
                    {
                        UpdateDataTabSelection(columnIndex + 1, rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        private void btnCopyRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnCopyRow.IsEnabled) //for Context Menu check if button is enabled
                {
                    //clear clipboard content
                    Clipboard.Clear();

                    //clear previous content
                    if (dscrClipboard != null)
                    {
                        dscrClipboard.Clear();
                    }
                    else
                    {
                        dscrClipboard = new List<DlkDataRecord>();
                    }
                    
                    if (dgDataDriven.Items.Count > 0)
                    {
                        //cell copy
                        List<DataGridRow> itemsForCopy = new List<DataGridRow>();
                        if (mSelectedDataRows == null || mSelectedDataRows.Count == 0)
                        {
                            //single cell selection
                            itemsForCopy.Add((DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(dgDataDriven.SelectedCells.Last().Item));
                        }
                        else
                        {
                            //entire row selection
                            itemsForCopy = mSelectedDataRows;
                        }

                        foreach (DataGridRow rowData in itemsForCopy)
                        {
                            foreach (DataGridColumn col in dgDataDriven.Columns.ToList())
                            {
                                DataGridCell cellContent = GetCell(rowData, col.DisplayIndex);
                                if (cellContent.Content == null) continue;
                                var cellValue = ((TextBlock)cellContent.Content);

                                if (dscrClipboard.Exists(x => x.Name == col.Header.ToString()))
                                {
                                    //update clipboard
                                    DlkDataRecord dataRec = dscrClipboard.Find(x => x.Name == col.Header.ToString());
                                    dataRec.Values.Add(cellValue.Text);
                                }
                                else
                                {
                                    //new item in clipboard
                                    DlkDataRecord dataRec = new DlkDataRecord();
                                    dataRec.Name = col.Header.ToString();
                                    dataRec.Values.Add(cellValue.Text);
                                    dscrClipboard.Add(dataRec);
                                }
                            }
                        }
                    }
                    //set content to clipboard
                    Clipboard.SetData("DataDrivenCollection", dscrClipboard);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnPasteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnPasteRow.IsEnabled) //for Context Menu check if button is enabled
                {
                    DataGridRow rowData = (DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(dgDataDriven.CurrentItem);
                    int targetRowIndex = rowData.GetIndex();
                    int targetColIndex = dgDataDriven.CurrentColumn.DisplayIndex;

                    //put clipboard content into collection
                    dscrClipboard = (List<DlkDataRecord>)Clipboard.GetData("DataDrivenCollection");

                    if (dscrClipboard != null && dscrClipboard.Count > 0)
                    {
                        foreach (DlkDataRecord src in dscrClipboard)
                        {
                            int index = mLoadedTest.Data.Records.FindIndex(x => x.Name == GetColumnName(src.Name));

                            if (index > -1) //Column Name was found
                            {
                                int count = targetRowIndex == (mLoadedTest.Data.Records[index].Values.Count - 1) ? 1 : dscrClipboard[0].Values.Count;

                                if (targetRowIndex <= (mLoadedTest.Data.Records[index].Values.Count - 1))
                                {
                                    //overwrite row if the targetRow has contents
                                    mLoadedTest.Data.Records[index].Values.RemoveRange(targetRowIndex, count);
                                }
                                mLoadedTest.Data.Records[index].Values.InsertRange(targetRowIndex, src.Values);
                            }
                        }

                        isChanged = true;
                        PopulateData();
                        UpdateDataTabSelection(targetColIndex, targetRowIndex);
                    }
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnInsertRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetDataRowColPosition(out int rowIndex, out int colIndex);
                dgDataDriven.CommitEdit();
                foreach (DlkDataRecord rec in mLoadedTest.Data.Records)
                {
                    rec.Values.Insert(rowIndex, string.Empty);
                }
                PopulateData();
                UpdateDataTabSelection(colIndex, Math.Min(rowIndex, dgDataDriven.Items.Count - 1));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRenameData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnRenameData.IsEnabled) // for Context Menu Item - won't function if button is disabled
                {
                    string targetColumnHeader = GetColumnName(mCurrentDataColumnHeader.Content.ToString());

                    AddDataColumn dlg = new AddDataColumn(mLoadedTest.Data, targetColumnHeader);
                    dlg.Title = "Rename '" + targetColumnHeader + "'";
                    dlg.Owner = this;

                    if ((bool)dlg.ShowDialog())
                    {
                        string newHeader = mLoadedTest.Data.Records[mCurrentDataColumnHeader.DisplayIndex].Name;
                        PopulateData();

                        foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
                        {
                            var paramName = step.mParameterOrigString.Replace("D{" + targetColumnHeader + "}", "D{" + newHeader + "}");
                            step.mParameters[0] = paramName;

                            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct && step.mPasswordParameters != null)
                            {
                                for (int i = 0; i < step.mPasswordParameters.Count(); i++)
                                {
                                    if (step.mPasswordParameters[i] == "D{" + targetColumnHeader + "}")
                                        step.mPasswordParameters[i] = "D{" + newHeader + "}";
                                }
                            }
                        }

                        dgTestSteps.Items.Refresh();
                        dgTestSteps_SelectionChanged(null, null);
                        isChanged = true;
                        btnRenameData.IsEnabled = false;
                        btnDeleteData.IsEnabled = false;
                        btnLeftData.IsEnabled = false;
                        btnRightData.IsEnabled = false;
                        isContextItemSelected = true;
                        RefreshParameters(mTestStepRecords[dgTestSteps.SelectedIndex]);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnTestStepInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgTestSteps.SelectedIndex == -1)
                {
                    return;
                }
                int currIndex = dgTestSteps.SelectedIndex;
                InsertStep(mTestStepRecords[currIndex]);                
                //PopulateTestSteps(CurrentTestInstance);
                PopulateTestSteps();
                dgTestSteps.SelectedIndex = currIndex;
                Keyboard.Focus(cmbComponent);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // If the loaded test is from a results file, reload the test as fresh script then save in background
                if (mLoadedTest.mTestSetupLogMessages.Count > 0)
                {
                    mLoadedTest = new DlkTest(mLoadedFile, true);
                    SaveInBackground(false, mLoadedFile, false);
                }

                dgTestSteps.SelectedIndex = 0;
                dgTestSteps_SelectionChanged(null, null);
                PopulateUpdatePanelWithDataValues();

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                // Require save?
                if (System.IO.Path.GetFileName(mLoadedTest.mTestPath) == "template.dat")
                {
                    SaveTestAs();
                    if (System.IO.Path.GetFileName(mLoadedTest.mTestPath) == "template.dat")
                    {
                        return;
                    }

                }
                //else
                //{
                //    //mLoadedTest.SaveTest(mLoadedFile);
                //    SaveTest();
                //}

                //DlkTest test = new DlkTest(mLoadedTest.mTestPath);
                //int savedCount = test.mInstanceCount;
                //if (int.Parse(txtCurrentInstance.Text) > savedCount)
                //{
                //    MessageBox.Show("Current test instance not found in file. \n\nPlease save your test before running.",
                //        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}

                //Check if Test Capture is open before executing the script
                if (_owner.TestCaptureForm != null && _owner.TestCaptureForm.IsVisible)
                {
                    string testCaptureStatus = _owner.TestCaptureForm.ViewStatus.ToString();
                    if (testCaptureStatus != "Default")
                    {
                        ShowError(DlkUserMessages.ERR_TEST_CAPTURE_ONGOING_RECORDING);
                        return;
                    }
                }

                //If there are changes, ask whether to save or not. If not, run anyway using the unchanged file.
                if (isChanged)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoWarning(DlkUserMessages.ASK_UNSAVED_TEST_RUN, "Save Changes?");
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            SaveTest();
                            break;
                        case MessageBoxResult.No:
                            break;
                        default:
                            break;
                    }
                }

                if (!_owner.EnvironmentIDs.Any())
                {
                    DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_ENVIRONMENT_SETTINGS + Environment.NewLine + Environment.NewLine + DlkUserMessages.WRN_ADD_NEW_ENVIRONMENT);
                    return;
                }

                TestExecutionParametersDialog dlg = new TestExecutionParametersDialog(
                            _owner.AllBrowsers, _owner.EnvironmentIDs.ToList(),
                            (mLoadedTest.Data.Records.Count == 0 || mLoadedTest.Data.Records.First().Values.Count == 0) ? 1
                            : mLoadedTest.Data.Records.First().Values.Count);

                dlg.Owner = this;
                if ((bool)dlg.ShowDialog() == true)
                {
                    if (!DlkEnvironment.IsURLBlacklist(CurrentEnvironment))
                    {
                        optOutput_Click(optOutput, null); /* simulate user click to display Output view */
                        ExecuteTest();
                        UpdateRecentResults("");
                        mResetColumnHeader = true;
                    }
                    else
                    {
                        DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.ERR_URL_BLACKLIST, CurrentEnvironment));   
                    }
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void optKWView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                optKWView.IsChecked = true;
                UnselectOtherToggles(new List<RadioButton>() { optDataView, optSplitView, optOutput });
                tabKW.Header = "Keyword View";
                CurrentView = Views.Keyword;
                RefreshKeywordSelection();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void optSplitView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                optSplitView.IsChecked = true;
                UnselectOtherToggles(new List<RadioButton>() { optDataView, optKWView, optOutput });
                tabKW.Header = "Keyword/Data View";
                CurrentView = Views.Split;
                RefreshKeywordSelection();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void optDataView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                optDataView.IsChecked = true;
                UnselectOtherToggles(new List<RadioButton>() { optKWView, optSplitView, optOutput });
                tabKW.Header = "Data View";
                CurrentView = Views.Data;
                RefreshKeywordSelection();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Output radio button click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void optOutput_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                optOutput.IsChecked = true;
                UnselectOtherToggles(new List<RadioButton>() { optDataView, optKWView, optSplitView });
                tabKW.Header = "Output View";
                CurrentView = Views.Output;
                RefreshKeywordSelection();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        private void cmbControl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (cmbControl.SelectedItem == null)
                {
                    mControlFilter = cmbControl.Text;
                    _ControlItems = null;
                    cmbControl.Items.Refresh();                    
                    List<string> filteredControlType = GetSelectedControls();
                    if (filteredControlType.Count() == ControlTypeList.Count() - 1)
                    {                        
                        cmbControl.DataContext = ControlItems;                        
                    }
                    else
                    {                       
                        cmbControl.DataContext = GetFilteredControlsByType(filteredControlType);                       
                    }
                }
                else if (e.Key.Equals(Key.Back))
                {
                    cmbControl.Text = "";
                }                
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cmbComponent_LostFocus(object sender, RoutedEventArgs e)
        {
            if (screenPreviousValue == cmbComponent.Text)
            {
                return;
            }
            screenPreviousValue = cmbComponent.Text;
            isScreenValid = true;
            if (cmbComponent.Text == "")
            {
                isScreenValid = false;
            }
            else if (DlkEnvironment.IsShowAppNameProduct && DlkEnvironment.IsShowAppNameEnabled())
            {
                if (!DlkDynamicObjectStoreHandler.Alias.OrderBy(_screen => _screen).ToList().Contains(cmbComponent.Text))
                {
                    isScreenValid = false;
                }
            }
            else
            {
                if (!DlkDynamicObjectStoreHandler.Screens.Contains(cmbComponent.Text))
                {
                    isScreenValid = false;
                }
            }
                cmbComponent_SelectionChanged(null, null);
        }

        private void cmbComponent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!isScreenValid)
                {
                    screenName = "";
                }
                else if (cmbComponent.SelectedItem != null)
                {
                    screenName = cmbComponent.SelectedItem.ToString();
                    if (DlkEnvironment.IsShowAppNameProduct && DlkEnvironment.IsShowAppNameEnabled())
                        screenName = DlkDynamicObjectStoreHandler.Screens[DlkDynamicObjectStoreHandler.Alias.IndexOf(screenName)];               
                }
                mControlFilter = "";
                _ControlItems = null;
                _ControlTypeList = null;
                _Controls = null;
                RefreshKeywords();
                keywordPreviousValue = "";
                cmbControl.Text = "";
                controlPreviousType = "";
                controlPreviousValue = "";
                filterImg.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cmbControl_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbControl.SelectedValue == null && mControls.Contains(cmbControl.Text))
                    cmbControl.SelectedValue = cmbControl.Text;

                if (CurrentSelectedControlType != controlPreviousType && CurrentSelectedControlType != "")
                    {
                        RefreshKeywords();
                        keywordPreviousValue = "";
                    }
                else if (controlPreviousValue != cmbControl.Text)
                {
                    if (mControls.Contains(cmbControl.Text))
                    {
                        if (isPasswordLastControl)
                        {
                            mPasswordMasked = null;
                            isPasswordLastControl = false;
                            RefreshKeywords(!cmbKeyword.Items.Contains(cmbKeyword.Text));
                            RefreshParameters();
                        }
                        else
                        {
                            mPasswordMasked = null;
                            List<DlkPasswordControl> passwordControls = DlkPasswordMaskedRecord.GetPasswordControls();
                            string selectedKeyword = cmbKeyword.SelectedValue == null ? cmbKeyword.Text : cmbKeyword.SelectedValue.ToString();
                            var result = passwordControls.SingleOrDefault(_parameter => _parameter.Screen == screenName
                                                        && _parameter.Control == cmbControl.Text
                                                        && _parameter.Keyword == selectedKeyword)?.Parameters;
                            if (result != null)
                            {
                                // control must be masked - change to masked control
                                mPasswordMasked = new PasswordMasked(screenName, cmbControl.Text, cmbKeyword.SelectedValue.ToString(), mParameterList);
                                if (!mPasswordMasked.AllowMasks)
                                    mPasswordMasked = null;
                                RefreshParameters();
                                isPasswordLastControl = true;
                            }
                            RefreshKeywords(false);
                        }
                        
                    }

                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cmbControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbControl.SelectedItem != null)
                {
                    cmbControl.Text = ((ControlItem)cmbControl.SelectedItem).ControlName;
                    if (controlPreviousValue != CurrentSelectedControlValue)
                    {
                        if (CurrentSelectedControlType != controlPreviousType)
                        {
                            RefreshKeywords();
                            keywordPreviousValue = "";
                        }
                        // change Parameters control if switching from or to masked control
                        if (!isPasswordLastControl)
                        {
                            mPasswordMasked = null;
                            List<DlkPasswordControl> passwordControls = DlkPasswordMaskedRecord.GetPasswordControls();
                            string selectedKeyword = cmbKeyword.SelectedValue == null ? cmbKeyword.Text : cmbKeyword.SelectedValue.ToString();
                            var result = passwordControls.SingleOrDefault(_parameter => _parameter.Screen == screenName
                                                        && _parameter.Control == CurrentSelectedControlValue
                                                        && _parameter.Keyword == selectedKeyword)?.Parameters;
                            if (result != null)
                            {
                                // control must be masked - change to masked control
                                mPasswordMasked = new PasswordMasked(screenName, cmbControl.Text, cmbKeyword.SelectedValue.ToString(), mParameterList);
                                if (!mPasswordMasked.AllowMasks)
                                    mPasswordMasked = null;
                                RefreshParameters();
                                isPasswordLastControl = true;
                            }
                            RefreshKeywords(false);
                        }
                        else
                        {
                            mPasswordMasked = null;
                            isPasswordLastControl = false;
                            if (CurrentSelectedControlType != controlPreviousType)
                            {
                                // remove parameters and masked control since control type is different
                                mKeywordParameters.Clear();
                                dgParameters.DataContext = null;
                            }
                            else
                            {
                                RefreshKeywords(!cmbKeyword.Items.Contains(cmbKeyword.Text));
                                RefreshParameters();
                            }
                        }
                        controlPreviousType = CurrentSelectedControlType;
                        controlPreviousValue = CurrentSelectedControlValue;
                    }
                }
                else if(SpecialFunctionScreens.Contains(screenName))
                {
                    RefreshKeywords();
                    controlPreviousValue = "";
                    controlPreviousType = "";
                }   
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cmbKeyword_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (bChangeParams)
                {
                    //Added logic to retain keyword parameters if the keyword selected is the same with its control type and last control is not password enabled
                    bool retainKeywordParameters = false;
                    if (cmbKeyword.SelectedValue != null &&
                        mCurrentControlTypeSnapshot.Equals(CurrentSelectedControlType) &&
                        mCurrentKeywordSnapshot.Equals(cmbKeyword.SelectedValue) && !isPasswordLastControl
                        && (keywordPreviousValue == cmbKeyword.SelectedValue.ToString() || keywordPreviousValue == ""))
                    {
                        retainKeywordParameters = true;
                    }

                    if (cmbKeyword.SelectedValue != null && 
                        !retainKeywordParameters)
                    {
                        if (e != null || (e == null && cmbKeyword.Items.Contains(cmbKeyword.Text) && keywordPreviousValue != cmbKeyword.SelectedValue.ToString()))
                        {
                            RefreshParameters();
                            if (txtHelp.Visibility == Visibility.Visible)
                            {
                                FillHelpBoxInfo();
                            }
                        }             
                    }

                    if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct)
                    {
                        if (cmbKeyword.SelectedValue != null)
                        {                            
                            mPasswordMasked = new PasswordMasked(screenName, cmbControl.Text, cmbKeyword.SelectedValue.ToString(), mParameterList);
                            if (!mPasswordMasked.AllowMasks)
                                mPasswordMasked = null;
                            else
                            {
                                isPasswordLastControl = true;
                                if (retainKeywordParameters)
                                    RefreshParameters();
                            }                                
                        }
                        else
                        {
                            SelectPasswordControl();
                        }
                    }
                }
                else
                {
                    SelectPasswordControl();
                    bChangeParams = true;
                }
                keyInput = "";
                keywordPreviousValue = !cmbKeyword.Items.Contains(cmbKeyword.Text) ? cmbKeyword.Text : cmbKeyword.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cmbKeyword_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbKeyword.Text != keywordPreviousValue)
                    cmbKeyword_SelectionChanged(sender, null);
                if (mParameterList?.Count() > 0 && isControlTabPressed)
                {
                    isControlTabPressed = false;
                    dgParameters.CurrentCell = new DataGridCellInfo(dgParameters.Items[0], dgParameters.Columns[0]);
                    (Keyboard.FocusedElement as UIElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cmbKeyword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & (ModifierKeys.Shift)) == (ModifierKeys.Shift))
            {
                // skip shift tab for reverse tabbing to work as intended
            }
            else if (e.Key == Key.Tab)
            {
                isControlTabPressed = true;
            }
        }

        private void cmbControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && (Keyboard.Modifiers & (ModifierKeys.Shift)) == (ModifierKeys.Shift))
            {
                // skip shift tab for reverse tabbing to work as intended
            }
            else if (e.Key == Key.Tab)
            {
                bChangeParams = true;
                keywordPreviousValue = !cmbKeyword.Items.Contains(cmbKeyword.Text) ? cmbKeyword.Text : cmbKeyword.SelectedValue.ToString();
            }
        }

        /// <summary>
        /// Masks Entered password (applicable for costpoint)
        /// </summary>
        /// <param name="sender">Contains which control has fired the event</param>
        /// <param name="e">Event arguments(contains data context of DlkTestStepRecord)</param>
        private void PasswordChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!DlkPasswordMaskedRecord.IsPasswordMaskedProduct || cmbKeyword.SelectedValue == null)
                    return;

                TextBox source = (TextBox)e.OriginalSource;
                string paramName = ((DlkKeywordParameterRecord)source.DataContext).mParameterName;
                if (mPasswordMasked == null || !handlePasswordChanged || !mPasswordMasked.ContainsParameter(paramName))
                    return;

                TextChange changes = e.Changes.First();
                string sourceInput = source.Text.Substring(changes.Offset, changes.AddedLength);

                if (keyInput == "" && sourceInput != DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE) //context menu
                {
                    if (DlkData.IsDataDrivenParam(source.Text) && isContextItemSelected)
                        keyInput = sourceInput;
                }
                else if (keyInput == " ") //reject space
                {
                    keyInput = "";
                }

                handlePasswordChanged = false;
                TextBox txtPassword = (TextBox)sender;
                mPasswordMasked.PasswordChanged(changes, keyInput, ref isBlankPasswordClear);

                //show placeholder after update and edit cell
                isBlankPasswordClear = keyInput == "" && sourceInput == DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE ? false : isBlankPasswordClear;

                txtPassword.Text = mPasswordMasked.DisplayPassword(isBlankPasswordClear);
                txtPassword.SelectionStart = mPasswordMasked.SelectionStart;
                keyInput = "";
                handlePasswordChanged = true;
                isBlankPasswordClear = false;
                isContextItemSelected = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Get user key input
        /// </summary>
        /// <param name="sender">Contains which control has fired the event</param>
        /// <param name="e">event arguments(contains data context of DlkTestStepRecord)</param>
        private void PreviewPasswordInput(object sender, TextCompositionEventArgs e)
        {
            string paramName = ((DlkKeywordParameterRecord)((TextBox)e.OriginalSource).DataContext).mParameterName;
            if (mPasswordMasked != null && mPasswordMasked.ContainsParameter(paramName) && !(e.Text == "\t" && e.Text.Length == 1))
                keyInput = e.Text;
            else
                keyInput = "";
        }

        private void cmbControl_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbControl.IsDropDownOpen = true;
                cmbControl.StaysOpenOnEdit = true;                
                PopulateControls();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // run test
                if (File.Exists(mLoadedTest.mTestPath))
                {
                    string relativePath = mLoadedTest.mTestPath.Replace(DlkEnvironment.mDirTests, "").Trim('\\');
                    DlkTestRunnerApi.ExecuteTest(mLoadedTest.mTestPath, false, CurrentEnvironment, relativePath, CurrentInstance,
                        CurrentBrowser, mSessionId);
                    
                    /* Update Output */
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                        new Action(delegate()
                        {
                            txtOutput.DataContext = DlkLogger.GetSessionObject(mSessionId);
                        }));

                    while (DlkTestRunnerApi.mExecutionStatus == "running")
                    {
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                /* Display results if existing */
                IsTestEditorBusy = false;
                if (Directory.Exists(DlkTestRunnerApi.mResultsPath))
                {
                    DirectoryInfo mDir = new DirectoryInfo(System.IO.Path.Combine(DlkEnvironment.mDirTestResults,
                        DlkTestRunnerApi.mResultsPath));
                    FileInfo file = mDir.GetFiles("*.xml").First();
                    DlkTest test = new DlkTest(file.FullName);
                    UpdateRecentResults(file.FullName);

                    /* Log results to Output */
                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("END OF TEST"), mSessionId);
                    DlkLogger.LogToOutputDisplay("\r\nTerminating session...\r\n", mSessionId);
                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("TEST RESULTS"), mSessionId);
                    if (test.mTestStatus.ToLower() == "failed")
                    {
                        DlkLogger.LogToOutputDisplay("Result\t\t\t:\tFAILED", mSessionId);
                        DlkLogger.LogToOutputDisplay(string.Format("Failed at step\t\t:\t{0}", test.mTestFailedAtStep), mSessionId);
                    }
                    else
                    {
                        DlkLogger.LogToOutputDisplay("Result\t\t\t:\tPASSED", mSessionId);
                    }
                    DlkLogger.LogToOutputDisplay(string.Format("Passed/Total steps\t:\t{0}/{1}",
                        test.mTestSteps.Count(x => x.mStepStatus.ToLower() == "passed"), test.mTestSteps.Count), mSessionId);

                    DlkLogger.LogToOutputDisplay(string.Format("Start Time\t\t:\t{0}", test.mTestStart), mSessionId);
                    DlkLogger.LogToOutputDisplay(string.Format("End Time\t\t:\t{0}", test.mTestEnd), mSessionId);

                    DlkLogger.LogToOutputDisplay(string.Format("Elapsed\t\t\t:\t{0}", test.mTestElapsed), mSessionId);
                    DlkLogger.LogToOutputDisplay(DlkLogger.ConvertToHeader("END OF RESULTS"), mSessionId);

                    ExecuteScriptDialog esd = new ExecuteScriptDialog(test);
                    esd.Owner = this;
                    esd.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            finally
            {
                /* Retain output text before set of datacontext to default */
                string lastOutput = txtOutput.Text;
                /* Save to manifest file */
                DlkOutputManifestHandler.UpdateOutputManifest(mLoadedTest.mTestPath, lastOutput);
                txtOutput.DataContext = this;
                Output = lastOutput;
                /* remove session object */
                DlkLogger.RemoveSessionLogs(mSessionId);
                /* Set Cancelation flag to default */
                DlkTestRunnerApi.mCancellationPending = false;
                IsRunInProgress = false;

                if(!string.IsNullOrEmpty(txtOutput.Text))
                {
                    btnSaveTestLogs.IsEnabled = true;
                    btnEmailTestLogs.IsEnabled = true;
                }
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

        /// <summary>
        /// Refresh keyword parameters grid
        /// </summary>
        private void RefreshParameters(DlkTestStepRecord rec = null)
        {
            mKeywordParameters = new ObservableCollection<DlkKeywordParameterRecord>(mParameterList);

            if (rec != null)
            {
                string[] myParams = rec.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                mKeywordParameters.Clear();
                dgParameters.DataContext = null;

                for (int idx = 0; idx < mParameterList.Count; idx++)
                {
                    mKeywordParameters.Add(new DlkKeywordParameterRecord(mParameterList[idx].mParameterName, myParams[idx], idx));
                }
            }

            dgParameters.ItemsSource = mKeywordParameters;
            dgParameters.Items.Refresh();
        }

        /// <summary>
        /// Refresh keyword bindings when changed occured in data driven grid
        /// </summary>
        private void RefreshKeywordSelection()
        {
            var _mParameter = new ObservableCollection<DlkKeywordParameterRecord>();

            foreach (var item in mKeywordParameters)
            {
                _mParameter.Add(new DlkKeywordParameterRecord(item.mParameterName,item.mValue,item.mIndex,item.mParamConversionSetting));
            }
            mKeywordParameters = new ObservableCollection<DlkKeywordParameterRecord>(_mParameter);
            dgParameters.ItemsSource = mKeywordParameters;
            dgParameters.Items.Refresh();
        }

        private void SaveTestAsHtml(object sender, RoutedEventArgs e)
        {
            //export and save test as html
            ExportSaveTestAsHtml();
            var htmlFile = mLoadedTest.mTestPath.Replace(".xml", ".html");
            if (File.Exists(htmlFile))
            {
                MessageBox.Show(DlkUserMessages.INF_TEST_HTML_EXPORT, "Export Test as HTML", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                OpenTestHTMLInBrowser(htmlFile);
            }
            else
            {
                MessageBox.Show(DlkUserMessages.ERR_TEST_HTML_EXPORT, "Export Test as HTML", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EmailTestHtml(object sender, RoutedEventArgs e)
        {
            //export and save test as HTML
            ExportSaveTestAsHtml();
            var htmlFile = mLoadedTest.mTestPath.Replace(".xml", ".html");
            if (File.Exists(htmlFile))
            {
                //options for email and open in browser
                OpenInBrowserOptionDialog openInBrowserOptionDialog = new OpenInBrowserOptionDialog();
                openInBrowserOptionDialog.Owner = this;
                openInBrowserOptionDialog.ShowDialog();

                string subject = "[" + DlkTestRunnerSettingsHandler.ApplicationUnderTest.Name + "] Test: " + mLoadedTest.mTestName;
                string body = DlkTestContentsHtmlHandler.CreateSummaryBody(mLoadedTest.mTestPath);
                if (gridFiltersActive > 0)
                {
                    List<DlkTestStepRecord> gridSteps = new List<DlkTestStepRecord>();
                    foreach (object selectedItem in dgTestSteps.Items)
                    {
                        int iCurrentRec = ((DlkTestStepRecord)selectedItem).mStepNumber - 1;
                        DlkTestStepRecord currStep = mLoadedTest.mTestSteps[iCurrentRec];
                        gridSteps.Add(currStep);
                    }
                    body = DlkTestContentsHtmlHandler.CreateSummaryBody(mLoadedTest.mTestPath, gridSteps);
                }

                switch (openInBrowserOptionDialog.EmailOpenBrowserChoice)
                {
                    case OpenInBrowserOptionDialog.EmailOpenBrowserOptions.EmailAndOpenBrowser:
                        OpenTestHTMLInBrowser(htmlFile);
                        SendToEmail(subject, body, htmlFile);
                        break;
                    case OpenInBrowserOptionDialog.EmailOpenBrowserOptions.EmailOnly:
                        SendToEmail(subject, body, htmlFile);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show(DlkUserMessages.ERR_TEST_HTML_EXPORT, "Export Test as HTML", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ctlScreenLoading_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (!(bool)e.NewValue)
                {
                    cmbComponent.IsEnabled = true;
                    //txtControl.IsEnabled = true;
                    cmbControl.IsEnabled = true;
                    cmbKeyword.IsEnabled = true;

                    // SelectedIndex returns -1 at this point. Causes exception. Commented out.

                    //DlkTestStepRecord mRec = mTestStepRecords[dgTestSteps.SelectedIndex];
                    //cmbComponent.SelectedItem = mRec.mScreen;
                    ////txtControl.Text = mRec.mControl;
                    //cmbControl.SelectedValue = mRec.mControl;
                    ////cmbKeyword.SelectedItem = mRec.mKeyword;
                    //cmbKeyword.SelectedValue = mRec.mKeyword;
                }
                else
                {
                    cmbComponent.IsEnabled = false;
                    //txtControl.IsEnabled = false;
                    cmbControl.IsEnabled = false;
                    cmbKeyword.IsEnabled = false;
                }
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
                if (isTestLibrary)
                this.DialogResult = true;
                bool cancelClosing = false;

                if (isChanged)
                {
                    MessageBoxResult res = DlkUserMessages.ShowQuestionYesNoCancelWarning(DlkUserMessages.ASK_UNSAVED_CHANGES, "Save Changes?");
                    switch (res)
                    {
                        case MessageBoxResult.Yes:
                            if (System.IO.Path.GetFileName(mLoadedTest.mTestPath) == "template.dat")
                            {
                                SaveTestAs();
                            }
                            else
                            {
                                SaveTest();
                            }
                            e.Cancel = false;
                            break;
                        case MessageBoxResult.No:
                            e.Cancel = false;
                            break;
                        case MessageBoxResult.Cancel:
                            e.Cancel = true;
                            cancelClosing = true;
                            break;
                        default:
                            break;
                    }
                }

                if (!cancelClosing)
                {
                    /*Checks and cancels ongoing test run before closing test editor*/
                    bool isCancelledWhileRunning = WindowClose_RunInProgress();
                    if (isCancelledWhileRunning)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Closes any test run in progress if confirmed
        /// </summary>
        private bool WindowClose_RunInProgress()
        {
            bool isCancelled = false;

            if (IsRunInProgress)
            {
                MessageBoxResult cancel = DlkUserMessages.ShowQuestionOkCancel(DlkUserMessages.ASK_CANCEL_TEST_RUN_CLOSE_TEST_EDITOR, "Close");
                if (cancel.Equals(MessageBoxResult.OK))
                {
                    IsRunInProgress = false;
                    StatusText = STATUS_TEXT_CANCELLING;
                    DlkTestRunnerApi.StopCurrentExecution();
                    isCancelled = true;
                }
                else
                {
                    isCancelled = false;
                }
            }
            else
            {
                isCancelled = true;
                DlkTestRunnerApi.mCancellationPending = false;
            }
            return isCancelled;
        }

        private void EditCell(object sender, EventArgs e)
        {
            try
            {
                /* Select all text on focus */
                TextBox tb = sender as TextBox;
                tb.Dispatcher.BeginInvoke(
                            new Action(delegate
                            {
                                if (tb.Text.Equals(DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE))
                                {
                                    if (IsCurrentPasswordBlank())
                                    {
                                        isBlankPasswordClear = true;
                                        tb.Clear();
                                    }
                                }
                                else
                                {
                                    tb.SelectAll();
                                }
                            }), System.Windows.Threading.DispatcherPriority.Input);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Checks if the current password is blank or has content. 
        /// </summary>
        /// <returns></returns>
        private bool IsCurrentPasswordBlank()
        {
            int iCurrentRec = ((DlkTestStepRecord)dgTestSteps.SelectedItem).mStepNumber - 1;
            DlkTestStepRecord currStep = mLoadedTest.mTestSteps[iCurrentRec];
            string passwordParam = "";
            currStep.mPasswordParameters.ForEach(pw => passwordParam += pw);
            
            return String.IsNullOrWhiteSpace(passwordParam);
        }

        private void btnShowControl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (cmbComponent.Text == "")
                //{
                //    ShowError("Please select a component and control to find.");
                //    return;
                //}
                //if (cmbControl.Text == "")
                //{
                //    ShowError("Please select a control to find.");
                //    return;
                //}
                //DlkEnvironment.CleanFrameworkWorkingHighlightDirResults();
                //DlkHighlightHandler.Highlight(cmbComponent.Text, cmbControl.Text);
                //if (!DlkHighlightHandler.mControlFound)
                //{
                //    ShowError("Control not found.");
                //}
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        /// <summary>
        /// Event handler for parameter context menu item clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        private void InsertDataContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu prnt = ((ContextMenu)((MenuItem)sender).Parent);
                dgParameters.BeginEdit();
                isContextItemSelected = true;
                mKeywordParameters[int.Parse(prnt.Tag.ToString())].mValue = ((MenuItem)sender).Header.ToString().Replace("__","_");
                dgParameters.CommitEdit();
                RefreshKeywordSelection();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Click handler for parameter context menu
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        private void btnInsertDataValue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mCurrentContextMenu != null)
                {
                    mCurrentContextMenu.IsOpen = true;
                    mCurrentContextMenu.Tag = ((Button)sender).Tag;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        /// <summary>
        /// Handler for any changes made to Data View grid item collection
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        private void dataViewCollection_Changed(object sender, EventArgs e)
        {
            try
            {
                PopulateUpdatePanelWithDataValues();
                RefreshKeywordSelection();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for actions after any cell of data-driven grid enters Edit mode
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event args</param>
        private void dgDataDriven_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            btnCopyRow.IsEnabled = false;
            btnPasteRow.IsEnabled = false;
        }

        /// <summary>
        /// Handler for editing cells in Data View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgDataDriven_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.EditAction == DataGridEditAction.Commit)
                {
                    if (e.Column is DataGridColumn column)
                    {
                        var item = e.EditingElement as TextBox;
                        
                        foreach (DlkDataRecord rec in mLoadedTest.Data.Records)
                        {
                            if (column.Header.ToString().Replace("__", "_").Equals(rec.Name))
                            {
                                rec.Values[e.Row.GetIndex()] = item.Text;
                                isChanged = true;
                                break;
                            }
                        }
                    }
                }
                SetCopyPasteRowButtonStates();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for clicking Links
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Links_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu ctx = new ContextMenu();
                ctx.Style = ((Style)FindResource("contextMenuStyle1"));
                ctx.Items.Clear();

                /* Do not display anything if no links attached */
                if (mLoadedTest.mLinks.Any())
                {
                    MenuItem grpLinks = new MenuItem();
                    grpLinks.Header = "Go to..."; // Link group header
                    grpLinks.IsEnabled = false;
                    grpLinks.FontWeight = FontWeights.Bold;
                    grpLinks.Foreground = new SolidColorBrush(Colors.Blue);
                    grpLinks.Margin = new Thickness(-20, 0, 0, 0);
                    ctx.Items.Add(grpLinks);

                    foreach (DlkTestLinkRecord rec in mLoadedTest.mLinks)
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
                MenuItem manageLinks = SetMenuItem("Manage Test Links...", ManageLinks_Clicked); // Management menu item
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

        /// <summary>
        /// Handler for clicking Tags
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Tags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu ctx = new ContextMenu();
                ctx.Style = ((Style)FindResource("contextMenuStyle1"));
                ctx.Items.Clear();

                if(mLoadedTest.mTags.Any())
                {
                    var sortedTags = mLoadedTest.mTags.OrderBy(x => x.Name).ToList();
                    //filter tags attached to the test which were removed/deleted from the central tags file.
                    sortedTags = DlkTag.LoadAllTags().FindAll(x => sortedTags.Any(y => y.Id == x.Id));

                    /* Do not display anything if no tags attached */
                    //if (mLoadedTest.mTags.Any())
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
        /// Any Link click handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LinkMenu_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = (((sender as MenuItem).Tag) as DlkTestLinkRecord).LinkPath;
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
                ManageTestLinks dlg = new ManageTestLinks(mLoadedTest);
                dlg.Owner = this;
                /* Check if user went thru with save, this is NULL if aborted */
                if (dlg.ShowDialog() == true && dlg.IsSaveAndCheckIn != null)
                {
                    SaveInBackground((bool)dlg.IsSaveAndCheckIn, mLoadedTest.mTestPath, false);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Add/Edit Tags menu clicked handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void ManageTags_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DlkTag> tagList = DlkTag.LoadAllTags();
                ManageTags dlg = new ManageTags(tagList, mLoadedTest.mTags, true, mLoadedTest);
                dlg.Owner = this;
                /* Check if user went thru with save, this is NULL if aborted */
                if (dlg.ShowDialog() == true && dlg.IsSaveAndCheckIn != null)
                {
                    SaveInBackground((bool)dlg.IsSaveAndCheckIn, mLoadedTest.mTestPath, false);
                    _owner.RefreshInMemoryTestTree(mLoadedTest.mTestPath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Save worker work completed handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mSaveWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                IsTestEditorBusy = false;
                isChanged = false;

                object[] resArr = (object[])e.Result;

                if (resArr.Count() != SAVE_ACTION_RES_EXACT_COUNT)
                {
                    return;
                }

                bool bFuncRes = (bool)resArr[SAVE_ACTION_RES_IDX_RET];
                if (bFuncRes)
                {
                    bool bReload = (bool)resArr[SAVE_ACTION_RES_IDX_RELOAD];
                    if (bReload) /* Reload test after save --> for Save As calls */
                    {
                        LoadTest(mLoadedTest.mTestPath);
                    }

                    GetLastUpdateInfoInBackground((bool)chkFileInfoToggle.IsChecked, mLoadedTest.mTestPath, mLoadedTest.Data.Path);

                    bool bShowMsg = (bool)resArr[SAVE_ACTION_RES_IDX_SHOWSUCCESSMSG];
                    if (bShowMsg) /* Show success message after save, some calls have silent option (Manage Links) */
                    {
                        DlkUserMessages.ShowInfo(DlkUserMessages.INF_SAVE_SUCCESSFUL + mLoadedFile, "Save");
                    }
                    if (!isTestLibrary)
                    {
                        _owner.RefreshInMemoryTestTree(mLoadedTest.mTestPath);
                        _owner.RefreshTestQueue(mLoadedTest.mTestPath);
                    }
                    IsTemplateLoaded = false; /* Since save was performed, assert that template is not loaded */
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Save worker do work handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">event arguments</param>
        private void mSaveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] args = (object[])e.Argument;

                if (args.Count() != SAVE_ACTION_ARG_EXACT_COUNT)
                {
                    e.Result = new object[] { false, false };
                    return;
                }

                bool bIsSaveAndCheckin = (bool)args[SAVE_ACTION_ARG_IDX_CHECKINFLAG];
                string sPath = args[SAVE_ACTION_ARG_IDX_FILEPATH].ToString();
                string sComments = args[SAVE_ACTION_ARG_IDX_COMMENTS].ToString();
                bool bShowSuccessMsg = (bool)args[SAVE_ACTION_ARG_IDX_SHOWSUCCESSMSG];
                bool bReload = (bool)args[SAVE_ACTION_ARG_IDX_RELOAD];

                e.Result = new object[] { bIsSaveAndCheckin ? SaveAndCheckIn(sPath, sComments) : SaveLocal(sPath), bShowSuccessMsg, bReload };
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Work completed handler for Last Update background worker
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mServerInfoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                object[] resArr = (object[])e.Result;

                if (resArr.Count() != LAST_UPDATE_RES_EXACT_COUNT)
                {
                    return;
                }

                LastModifiedDateKeyword = resArr[LAST_UPDATE_RES_IDX_KEYWORDDATE].ToString();
                LastModifiedDateData = resArr[LAST_UPDATE_RES_IDX_DATADATE].ToString();
                LastModifiedByUserKeyword = resArr[LAST_UPDATE_RES_IDX_KEYWORDUSER].ToString();
                LastModifiedByUserData = resArr[LAST_UPDATE_RES_IDX_DATAUSER].ToString();

                IsTestEditorBusy = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Do work handler for Last Update background worker
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mServerInfoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] args = (object[])e.Argument;

                if (args.Count() != LAST_UPDATE_ARG_EXACT_COUNT)
                {
                    e.Result = new object[] { string.Empty, string.Empty, string.Empty, string.Empty };
                    return;
                }

                bool bIsServer = (bool)args[LAST_UPDATE_ARG_IDX_ISSERVER];
                string sKeywordPath = args[LAST_UPDATE_ARG_IDX_KEYWORDPATH].ToString();
                string sDataPath = args[LAST_UPDATE_ARG_IDX_DATAPATH].ToString();
                string sOutKeywordDate;
                string sOutDataDate;
                string sOutKeywordUser;
                string sOutDataUser;

                if (bIsServer) /* TFS */
                {
                    GetServerTestLastUpdateInfo(sKeywordPath, sDataPath, out sOutKeywordDate, out sOutDataDate,
                        out sOutKeywordUser, out sOutDataUser);
                }
                else /* Local */
                {
                    GetLocalTestLastUpdateInfo(sKeywordPath, sDataPath, out sOutKeywordDate, out sOutDataDate,
                        out sOutKeywordUser, out sOutDataUser);
                }

                e.Result = new object[] { sOutKeywordDate, sOutDataDate, sOutKeywordUser, sOutDataUser };
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Test Editor window activated event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                NotifyPropertyChanged("IsServerInfoToggleVisible");
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Local/Server last update file info toggle click event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chkFileInfoToggle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton tb = sender as ToggleButton;
                GetLastUpdateInfoInBackground((bool)tb.IsChecked, mLoadedTest.mTestPath, mLoadedTest.Data.Path);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for txtOutput textchanged event
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void txtOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                tb.CaretIndex = tb.Text.Length;
                tb.ScrollToEnd();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Pause Test click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnPauseTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTestRunnerApi.mExecutionPaused = !DlkTestRunnerApi.mExecutionPaused;
                /* Get Test Playback state to update UI */
                if (DlkTestRunnerApi.mExecutionPaused)
                {
                    StatusText = STATUS_TEXT_PAUSED;
                    IsProgressBarVisible = false;
                }
                else
                {
                    StatusText = STATUS_TEXT_RUNNING;
                    IsProgressBarVisible = true;
                }
                UpdatePauseResumeIcon(DlkTestRunnerApi.mExecutionPaused);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Cancel Test click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancelTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsRunInProgress = false; //disable Pause after clicking Cancel to avoid issues
                DlkTestRunnerApi.mExecutionPaused = false;
                UpdatePauseResumeIcon(DlkTestRunnerApi.mExecutionPaused);
                IsProgressBarVisible = true;
                StatusText = STATUS_TEXT_CANCELLING;
                DlkTestRunnerApi.StopCurrentExecution();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Save Test logs click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnSaveTestLogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string savePath = SaveOutputLogs();
                if (!string.IsNullOrEmpty(savePath))
                {
                    DlkUserMessages.ShowInfo("Save successful!\n\n" + savePath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Email Test logs click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnEmailTestLogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* write temp file */
                string attachment = SaveOutputLogs(true);
                string subject = "[" + DlkTestRunnerSettingsHandler.ApplicationUnderTest.Name + "] Ad-hoc Run Test Logs: " + mLoadedTest.mTestName;
                string body = GetOutputSummary(Output);

                /* compose and attach */
                SendToEmail(subject, body, attachment);

                /* delete temp file */
                if (File.Exists(attachment))
                {
                    File.Delete(attachment);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Sets filter options and popup content to Execute column
        /// </summary>
        private void btnExecuteFilter_Click(object sender, RoutedEventArgs e)
        {
            SetFilterType(FilterColumn.Execute);
        }

        /// <summary>
        /// Sets filter options and popup content to Screen column
        /// </summary>
        private void btnScreenFilter_Click(object sender, RoutedEventArgs e)
        {
            SetFilterType(FilterColumn.Screen);
        }

        /// <summary>
        /// Sets filter options and popup content to Control column
        /// </summary>
        private void btnControlFilter_Click(object sender, RoutedEventArgs e)
        {
            SetFilterType(FilterColumn.Control);
        }

        /// <summary>
        /// Sets filter options and popup content to Keyword column
        /// </summary>
        private void btnKeywordFilter_Click(object sender, RoutedEventArgs e)
        {
            SetFilterType(FilterColumn.Keyword);
        }

        /// <summary>
        /// Sets filter options and popup content to Parameters column
        /// </summary>
        private void btnParametersFilter_Click(object sender, RoutedEventArgs e)
        {
            SetFilterType(FilterColumn.Parameters);
        }

        /// <summary>
        /// Sets filter options and popup content to Delay column
        /// </summary>
        private void btnDelayFilter_Click(object sender, RoutedEventArgs e)
        {
            SetFilterType(FilterColumn.Delay);
        }

        /// <summary>
        /// Toggles Filter for columns on/off
        /// </summary>
        private void btnGridFilter_Click(object sender, RoutedEventArgs e)
        {
            if (btnGridFilter.IsChecked != null)
            {
                hasGridFilter = (bool)btnGridFilter.IsChecked;
                UpdateStepButtonStates();
                btnTestStepPaste.IsEnabled = hasGridFilter ? false : !CheckClipboardEmpty("TestStepCollection");
                ToggleConvertToData();
                if (!hasGridFilter)
                {
                    RemoveAllFilters();
                }
            }
        }

        /// <summary>
        /// Applies filter selections to current grid
        /// </summary>
        private void btnFilterOK_Click(object sender, RoutedEventArgs e)
        {
            editedRrecords.Clear();
            if (CheckedListItems.Skip(1).All(x => x.IsChecked)) // remove filter for column if newest
            {
                /* do nothing if no prior filter, or if not newest filter*/
                if (filterOrder[m_TempFilter] > 0 && filterOrder[m_TempFilter] == gridFiltersActive)
                {
                    m_NewestFilterColumn = m_TempFilter;
                    filterOrder[m_NewestFilterColumn] = 0;
                    gridFiltersActive--;
                    if (gridFiltersActive > 0)
                    {
                        m_NewestFilterColumn = filterOrder.FirstOrDefault(x => x.Value == gridFiltersActive).Key;
                        ApplyFilterToGrid();
                    }
                    else
                    {
                        PopulateTestSteps();
                        m_NewestFilterColumn = FilterColumn.None;
                    }
                }
                popupFilter.IsOpen = false;
                return;
            }
            else if (CheckedListItems.Skip(1).Any(x => x.IsChecked))
            {
                switch (m_TempFilter)
                {
                    case FilterColumn.Execute:
                        filterOrder[FilterColumn.Execute] = m_NewestFilterColumn != m_TempFilter ? gridFiltersActive + 1 : filterOrder[FilterColumn.Execute];
                        _mExecuteFilterCheckBoxes = CheckedListItems;
                        break;
                    case FilterColumn.Screen:
                        filterOrder[FilterColumn.Screen] = m_NewestFilterColumn != m_TempFilter ? gridFiltersActive + 1 : filterOrder[FilterColumn.Screen];
                        _mScreenFilterCheckBoxes = CheckedListItems;
                        break;
                    case FilterColumn.Control:
                        filterOrder[FilterColumn.Control] = m_NewestFilterColumn != m_TempFilter ? gridFiltersActive + 1 : filterOrder[FilterColumn.Control];
                        _mControlFilterCheckBoxes = CheckedListItems;
                        break;
                    case FilterColumn.Keyword:
                        filterOrder[FilterColumn.Keyword] = m_NewestFilterColumn != m_TempFilter ? gridFiltersActive + 1 : filterOrder[FilterColumn.Keyword];
                        _mKeywordFilterCheckBoxes = CheckedListItems;
                        break;
                    case FilterColumn.Parameters:
                        filterOrder[FilterColumn.Parameters] = m_NewestFilterColumn != m_TempFilter ? gridFiltersActive + 1 : filterOrder[FilterColumn.Parameters];
                        _mParametersFilterCheckBoxes = CheckedListItems;
                        break;
                    case FilterColumn.Delay:
                        filterOrder[FilterColumn.Delay] = m_NewestFilterColumn != m_TempFilter ? gridFiltersActive + 1 : filterOrder[FilterColumn.Delay];
                        _mDelayFilterCheckBoxes = CheckedListItems;
                        break;
                }

                if (m_NewestFilterColumn != m_TempFilter)
                {
                    gridFiltersActive++;
                    m_NewestFilterColumn = m_TempFilter;
                }
                ApplyFilterToGrid();
                popupFilter.IsOpen = false;
            }
        }

        /// <summary>
        /// Closes popup
        /// </summary>
        private void btnFilterCancel_Click(object sender, RoutedEventArgs e)
        {
            popupFilter.IsOpen = false;
        }

        /// <summary>
        /// Checks whether clicked checkbox is (Select all)
        /// </summary>
        private void checkBoxListItem_Click(object sender, RoutedEventArgs e)
        {
            if (!isFilterAutoCheck)
            {
                isFilterAutoCheck = true;
                CheckBox checkedItem = (CheckBox)sender;
                if (checkedItem.Tag.ToString() == FILTER_SELECT_ALL)
                {
                    foreach (CheckedListItem<string> item in CheckedListItems)
                    {
                        item.IsChecked = CheckedListItems.First().IsChecked;
                    }
                }
                else
                {
                    /* change state of Select All based on other items */
                    CheckedListItems.First().IsChecked = CheckedListItems.Skip(1).All(x => x.IsChecked);
                }
                isFilterAutoCheck = false;

                btnFilterOK.IsEnabled = CheckedListItems.Any(item => item.IsChecked);
            }
        }

        //private void txtTotalInstance_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    txtTotalInstanceData.Text = txtTotalInstance.Text;
        //    UpdateNavButtonStates();
        //    UpdateInstanceButtonStates();
        //}


        //private void tabMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (tabData.IsSelected)
        //    {
        //        if (e.Source is TabControl)
        //        {
        //            PopulateDataDrivenTab();
        //        }
        //    }
        //    else
        //    {
        //        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
        //            new Action(delegate()
        //            {
        //                dgTestSteps.ScrollIntoView(dgTestSteps.SelectedItem);
        //            }));
        //    }
        //}


        //private void btnNextTest_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = dgTestSteps.SelectedIndex;
        //        txtCurrentInstance.Text = (CurrentTestInstance + 1).ToString();
        //        //txtCurrentInstanceData.Text = txtCurrentInstance.Text;
        //        PopulateTestSteps(CurrentTestInstance);
        //        //UpdateNavButtonStates();
        //        dgTestSteps.SelectedIndex = selectedIndex;

        //        if (tabData.IsSelected)
        //        {
        //            dgDataDriven.CurrentCell = new DataGridCellInfo(dgDataDriven.Items[int.Parse(txtCurrentInstanceData.Text) - 1], dgDataDriven.Columns[dgTestSteps.SelectedIndex]);
        //            if (!dgDataDriven.SelectedCells.Contains(dgDataDriven.CurrentCell))
        //            {
        //                dgDataDriven.SelectedCells.Add(dgDataDriven.CurrentCell);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        // do nothing
        //    }
        //}

        //private void UpdateNavButtonStates()
        //{
        //    btnPreviousTest.IsEnabled = CurrentTestInstance != 1;
        //    btnPreviousTestData.IsEnabled = btnPreviousTest.IsEnabled;
        //    btnNextTest.IsEnabled = CurrentTestInstance < TotalTestInstance;
        //    btnNextTestData.IsEnabled = btnNextTest.IsEnabled;
        //}

        //private void UpdateTestInstanceDisplay()
        //{
        //    txtTotalInstance.Text = mLoadedTest.mInstanceCount.ToString();
        //}

        //private void btnPreviousTest_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = dgTestSteps.SelectedIndex;
        //        txtCurrentInstance.Text = (CurrentTestInstance - 1).ToString();
        //        //txtCurrentInstanceData.Text = txtCurrentInstance.Text;
        //        PopulateTestSteps(CurrentTestInstance);
        //        //UpdateNavButtonStates();
        //        dgTestSteps.SelectedIndex = selectedIndex;

        //        if (tabData.IsSelected)
        //        {
        //            dgDataDriven.CurrentCell = new DataGridCellInfo(dgDataDriven.Items[int.Parse(txtCurrentInstanceData.Text) - 1], dgDataDriven.Columns[dgTestSteps.SelectedIndex]);
        //            if (!dgDataDriven.SelectedCells.Contains(dgDataDriven.CurrentCell))
        //            {
        //                dgDataDriven.SelectedCells.Add(dgDataDriven.CurrentCell);
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private void btnAddTest_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        int selectedIndex = dgTestSteps.SelectedIndex;
        //        AddTestInstance();
        //        txtTotalInstance.Text = (TotalTestInstance + 1).ToString();
        //        txtCurrentInstance.Text = txtTotalInstance.Text;
        //        PopulateTestSteps(CurrentTestInstance);
        //        //UpdateNavButtonStates();
        //        dgTestSteps.SelectedIndex = selectedIndex;
        //        if (tabData.IsSelected)
        //        {
        //            PopulateDataDrivenTab();
        //        }
        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private void btnDeleteTest_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (TotalTestInstance == 1)
        //        {
        //            return;
        //        }
        //        if (MessageBox.Show("Are you sure you want to delete current test instance?", "Delete Test Instance", MessageBoxButton.YesNo,
        //            MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
        //        {
        //            return;
        //        }
        //        int selectedIndex = dgTestSteps.SelectedIndex;
        //        DeleteTestInstance();
        //        txtTotalInstance.Text = (TotalTestInstance - 1).ToString();
        //        if (CurrentTestInstance > TotalTestInstance)
        //        {
        //            txtCurrentInstance.Text = TotalTestInstance.ToString();
        //        }
        //        PopulateTestSteps(CurrentTestInstance);
        //        //UpdateNavButtonStates();
        //        dgTestSteps.SelectedIndex = selectedIndex;
        //        if (tabData.IsSelected)
        //        {
        //            PopulateDataDrivenTab();
        //        }
        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private void txtCurrentInstance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    try
        //    {
        //        e.Handled = txtCurrentInstance.Text.Length >= 3 || !DlkString.IsNumeric(e.Text);
        //        //|| int.Parse(txtCurrentInstance + int.Parse(e.Text) > TotalTestInstance;
        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private void txtCurrentInstance_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        int currIndex = dgTestSteps.SelectedIndex;
        //        if (tabData.IsSelected)
        //        {
        //            //currIndex = tabData.
        //        }
        //        if (string.IsNullOrEmpty(txtCurrentInstance.Text))
        //        {
        //            txtCurrentInstance.Text = "1";
        //        }
        //        else if (CurrentTestInstance == 0)
        //        {
        //            txtCurrentInstance.Text = "1";
        //        }
        //        else if (CurrentTestInstance > TotalTestInstance)
        //        {
        //            txtCurrentInstance.Text = TotalTestInstance.ToString();
        //        }
        //        txtCurrentInstanceData.Text = txtCurrentInstance.Text;
        //        UpdateNavButtonStates();
        //        PopulateTestSteps(CurrentTestInstance);
        //        dgTestSteps.SelectedIndex = currIndex;
        //        if (!tabData.IsSelected)
        //        {
        //            dgDataDriven.CurrentCell = new DataGridCellInfo(dgDataDriven.Items[int.Parse(txtCurrentInstanceData.Text) - 1], dgDataDriven.Columns[dgTestSteps.SelectedIndex]);
        //            if (!dgDataDriven.SelectedCells.Contains(dgDataDriven.CurrentCell))
        //            {
        //                dgDataDriven.SelectedCells.Add(dgDataDriven.CurrentCell);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private void btnInsertTest_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {

        //        int selectedIndex = dgTestSteps.SelectedIndex;
        //        InsertTestInstance();
        //        txtTotalInstance.Text = (TotalTestInstance + 1).ToString();
        //        PopulateTestSteps(CurrentTestInstance);
        //        //UpdateNavButtonStates();
        //        dgTestSteps.SelectedIndex = selectedIndex;
        //        if (tabData.IsSelected)
        //        {
        //            PopulateDataDrivenTab();
        //        }
        //    }
        //    catch
        //    {
        //        // Do nothing
        //    }
        //}

        //private void UpdateInstanceButtonStates()
        //{
        //    btnDeleteTest.IsEnabled = int.Parse(txtTotalInstance.Text) > 1;
        //    btnDeleteTestData.IsEnabled = btnDeleteTest.IsEnabled;
        //}


        //private void txtControl_KeyUp(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Key.Equals(Key.Down))
        //        {
        //            e.Handled = true;
        //            //lbControls.Focus();
        //        }
        //        else
        //        {
        //            mControlFilter = txtControl.Text;
        //            RefreshListbox();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
        //    }
        //}


        /// <summary>
        /// Handler for checked Continue on Error
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkContinueOnError_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isChanged = true;
                mLoadedTest.mContinueOnError = (bool)chkContinueOnError.IsChecked;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Execute Step checkbox state changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxExecuteStep_CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            SetTestStepUpdateButtonState();
        }

        /// <summary>
        /// Handler for Step Delay checkbox state changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBoxStepDelay_CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            SetTestStepUpdateButtonState();
        }

        /// <summary>
        /// Handler for Last Results button click event
        /// </summary>
        private void btnLastResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTest test = new DlkTest(mRecentResultPath);
                ExecuteScriptDialog esd = new ExecuteScriptDialog(test);
                esd.Owner = this;
                esd.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Initialize Paste Modes and Data Driven Clear All modes
        /// </summary>
        private void InitializeContextMenuOptions()
        {
            //load paste options
            cmnuPasteType.Items.Clear();
            foreach (string pasteType in new string[] { PASTE_OVERWRITE, PASTE_INSERT })
            {
                MenuItem searchPasteOption = new MenuItem
                {
                    Header = pasteType,
                    IsCheckable = true,
                    IsChecked = pasteType == PASTE_OVERWRITE
                };
                searchPasteOption.Click += SearchPasteOption_Click;
                cmnuPasteType.Items.Add(searchPasteOption);
            }

            //load clear data driven rows options
            cmnuClearType.Items.Clear();
            foreach (string clearType in new string[] { CLEAR_ALL_ROWS, CLEAR_ALL })
            {
                MenuItem searchClearOption = new MenuItem
                {
                    Header = clearType,
                    IsCheckable = true,
                    IsChecked = clearType == CLEAR_ALL_ROWS
                };
                searchClearOption.Click += SearchClearOption_Click;
                cmnuClearType.Items.Add(searchClearOption);
            }
        }

        /// <summary>
        /// Opens test HTML in default web browser
        /// </summary>
        /// <param name="htmlFile"></param>
        private void OpenTestHTMLInBrowser(string htmlFile)
        {
            try
            {
                //open file on default browser
                Process.Start(htmlFile);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        /// <summary>
        /// Save test and export as HTML
        /// </summary>
        private void ExportSaveTestAsHtml()
        {
            try
            {
                if (isChanged)
                {
                    //force save test to get all updates/changes     
                    if (Path.GetFileName(mLoadedTest.mTestPath) == "template.dat")
                    {
                        SaveTestAs();
                    }
                    else
                    {
                        SaveTest();
                    }
                }
                if (gridFiltersActive > 0)
                {
                    List<DlkTestStepRecord> gridSteps = new List<DlkTestStepRecord>();
                    foreach (object selectedItem in dgTestSteps.Items)
                    {
                        int iCurrentRec = ((DlkTestStepRecord)selectedItem).mStepNumber - 1;
                        DlkTestStepRecord currStep = mLoadedTest.mTestSteps[iCurrentRec];
                        gridSteps.Add(currStep);
                    }
                    DlkTestContentsHtmlHandler.CreateHTMLReportBody(mLoadedTest.mTestPath, gridSteps);
                    return;
                }
                DlkTestContentsHtmlHandler.CreateHTMLReportBody(mLoadedTest.mTestPath);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Get user selected control types as basis for filtering Control ComboBox
        /// </summary>
        /// <returns>Returns all selected control type for filtering basis</returns>
        private List<string> GetSelectedControls()
        {
            List<string> filteredControlType = new List<string>();
            if (ControlTypeList != null)
            {
                List<ControlList> selectedControls = ControlTypeList.FindAll(ctrl => ctrl.Selected == true && ctrl.ControlType != SELECT_ALL).ToList();
                foreach (var c in selectedControls)
                {
                    filteredControlType.Add(c.ControlType);
                }
            }
            return filteredControlType;
        }

        /// <summary>
        /// Updates all control type menu items 
        /// </summary>
        /// <param name="selected">Specifies if all controltype values are selected or not</param>
        private void RefreshAllControls(bool selected = false)
        {
            if (ControlTypeList != null)
            {
                ControlTypeList = ControlTypeList.Select(c => { c.Selected = selected; return c; }).ToList();
                ctrlFilterMenu.Items.Refresh();
                filterImg.IsEnabled = ControlTypeList.Count() > 0;
            }
        }

        /// <summary>
        /// Updates control filter and control combobox
        /// </summary>
        private void PopulateControls()
        {
            if (cmbComponent.SelectedValue != null && cmbComponent.SelectedValue.ToString() != "Function" && cmbComponent.SelectedValue.ToString() != "Database" && dgTestSteps.SelectedItems.Count == 1)
            {
                List<string> filteredControlType = GetSelectedControls();
                if (filteredControlType.Count() == ControlTypeList.Count() - 1)
                {
                    cmbControl.DataContext = ControlItems;
                }
                else
                {
                    cmbControl.DataContext = GetFilteredControlsByType(filteredControlType);
                }

                ctrlFilterMenu.DataContext = ControlTypeList;
                filterImg.Visibility = ControlTypeList.Count() > 0 ? Visibility.Visible : Visibility.Hidden;
            }
            else
            {
                filterImg.Visibility = Visibility.Hidden;
                cmbControl.DataContext = cmbComponent.SelectedValue != null && cmbComponent.SelectedValue.ToString() == "Function" ? ControlItems : null;
                ctrlFilterMenu.DataContext = null;
            }
        }

        /// <summary>
        /// Scrolls into view selected item from test step grid
        /// </summary>
        /// <param name="selectedIndex">index of selected item in grid</param>
        private void ScrollToSelectedItem(int selectedIndex, SelectionChangedEventArgs e)
        {
            try
            {
                int testStepsCount = dgTestSteps.Items.Count;//moved out of the dispatcher for debugging purposes
                int lastRowIndex = testStepsCount - 1;//set to the last step by default

                /* 
                  on SelectionChanged event, "e" will contain the newly added items to the selection. from these newly added items, the first added item 
                  (the last selected) will be the row to be scrolled to.
                */
                if (dgTestSteps.SelectedItems.Count > 0)
                {
                    if (e != null)
                    {
                        lastRowIndex = e.AddedItems.Count > 0 ? (dgTestSteps.Items.IndexOf(e.AddedItems[0])) : selectedIndex;
                    }
                }
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
                    (System.Windows.Threading.DispatcherOperationCallback)delegate
                    {
                        if (testStepsCount == 0)
                            return null;

                        if (selectedIndex < 0)
                        {
                            dgTestSteps.ScrollIntoView(dgTestSteps.Items[0]); // scroll to first
                        }

                        else
                        {
                            /*
                             if there are selected items, scroll to the last selected item.
                              otherwise check if there are no selected items then
                              check if the selectedindex is within the bounds/range of the number of test steps
                              if within the bounds, scroll to the selectedindex
                              else scroll to the last item.
                             */
                            dgTestSteps.ScrollIntoView(dgTestSteps.SelectedItems.Count > 0 ? dgTestSteps.Items[lastRowIndex] : (selectedIndex < testStepsCount ? dgTestSteps.Items[selectedIndex] : dgTestSteps.Items[testStepsCount - 1]));
                        }
                        return null;
                    }, null);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private string GetColumnName(string columnName)
        {
            return columnName.Replace("__", "_");
        }

        public List<DlkKeywordHelpBoxRecord> HelpData
        {
            get
            {
                return m_Help;
            }
        }

        private List<ControlItem> GetControls()
        {
            if (_ControlItems == null || _ControlItems.Count == 0)
            {
                _ControlItems = new List<ControlItem>();
                List<ControlItem> filteredContainer = new List<ControlItem>();
                _ControlItems = GetFilteredControls(mControlFilter, string.IsNullOrEmpty(mControlFilter) ? null : filteredContainer);
                _ControlItems = string.IsNullOrEmpty(mControlFilter) ? _ControlItems : filteredContainer;
            }
            return _ControlItems;
        }

        public List<ControlItem> GetFilteredControls(String Filter = "", List<ControlItem> FilteredItemContainer = null)
        {
            List<ControlItem> controls = new List<ControlItem>();
            if (mControls.Count() > 0)
            {
                if (mControls.ElementAt(0).Equals("Function"))
                {
                    controls.Add(new ControlItem("Function","Function"));
                }
                else
                {
                    var ctrls = DlkDynamicObjectStoreHandler.GetControlRecords(screenName);
                    if (FilteredItemContainer != null)
                    {
                        foreach (var ctrl in ctrls)
                        {
                            if (ctrl.mKey.ToLower().Contains(Filter.ToLower()))
                            {
                                FilteredItemContainer.Add(new ControlItem(ctrl.mKey, ctrl.mControlType));
                            }
                        }
                    }
                    else
                    {
                        foreach (var ctrl in ctrls)
                        {
                            controls.Add(new ControlItem(ctrl.mKey, ctrl.mControlType));
                        }
                    }                    
                }
            }
            if (!string.IsNullOrEmpty(Filter) && FilteredItemContainer != null)
            {
                controls = FilteredItemContainer;
            }
            return controls;
        }

        /// <summary>
        /// Gets all available controls for the screen based on selected control types
        /// </summary>
        /// <param name="Filters">List of all user selected control type as filters</param>
        /// <returns>List of controls based on control type filter</returns>
        public List<ControlItem> GetFilteredControlsByType(List<String> Filters = null)
        {
            List<ControlItem> filteredItemContainer = new List<ControlItem>();
            List<ControlItem> controls = new List<ControlItem>();
            if (mControls.Count() > 0)
            {
                if (mControls.ElementAt(0).Equals("Function"))
                {
                    controls.Add(new ControlItem("Function", "Function"));
                }
                else
                {
                    var ctrls = DlkDynamicObjectStoreHandler.GetControlRecords(screenName);
                    if (Filters != null || Filters.Count() > 0)
                    {
                        foreach (var ctrl in ctrls)
                        {
                            if (Filters.Contains(ctrl.mControlType))
                            {
                                filteredItemContainer.Add(new ControlItem(ctrl.mKey, ctrl.mControlType));
                            }
                        }
                    }
                    else
                    {
                        foreach (var ctrl in ctrls)
                        {
                            controls.Add(new ControlItem(ctrl.mKey, ctrl.mControlType));
                        }
                    }
                }
            }
            return filteredItemContainer;
        }

        private bool CheckClipboardEmpty(string collection)
        {
            if (Clipboard.ContainsData(collection))
            {
                return false;
            }
            return true;
        }

        private void SaveTestAs(string DialogName = "Save")
        {
            UpdateData();
            mLoadedTest.mTestName = txtTestName.Text;
            mLoadedTest.mTestDescription = txtTestDescription.Text.Trim();
            mLoadedTest.mTestAuthor = txtTestAuthor.Text;
            string mFilteredPath = _owner.TestRoot;
            SaveTestDialog dlg = new SaveTestDialog(mLoadedTest, mFilteredPath, DialogName);
            dlg.Owner = this;
            string sFilePath;
            bool? bSaveAndCheckin;
            dlg.ShowDialog(out bSaveAndCheckin, out sFilePath);

            if (bSaveAndCheckin != null)
            {
                UpdateRecentResults("");
                SaveInBackground((bool)bSaveAndCheckin, sFilePath, true, true);
            }

            //reload test description after trimming and saving
            if (txtTestDescription.Text != mLoadedTest.mTestDescription)
            {
                txtTestDescription.Text = mLoadedTest.mTestDescription;
            }
        }

        private bool IsValidFileName(string Input)
        {
            bool ret = true;
            if (Input.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                ret = false;
            }
            return ret;
        }

        private void ClearStepPanel()
        {
            cmbExecuteStep.Text = string.Empty;
            cmbComponent.Text = string.Empty;
            cmbControl.Text = string.Empty;
            cmbKeyword.Text = string.Empty;
            //txtParameters.Text = string.Empty;
            txtDelay.Text = "0";
        }

        private int GetRowIndexFromDataGrid(DataGrid dg, string rowHeader)
        {
            DataGridRow row = null;
            for (int i = 0; i < dg.Items.Count; i++)
            {
                row = (DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromIndex(i);
                if (row.Header.ToString() == rowHeader.ToString())
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// checks if the clipboard is empty
        /// </summary>
        /// <returns></returns>
        //private DlkTest CurrentTest
        //{
        //    get
        //    {
        //        return mTests[CurrentTestInstance - 1];
        //    }
        //}

        //private int CurrentTestInstance
        //{
        //    get
        //    {
        //        return int.Parse(txtCurrentInstance.Text);
        //    }
        //}

        //private int TotalTestInstance
        //{
        //    get
        //    {
        //        return int.Parse(txtTotalInstance.Text);
        //    }
        //}


        /// <summary>
        /// Used to disable the form (but leaves the grid enabled so it can be scrolled, etc).
        /// Used most often when viewing a DDT
        /// </summary>
        //private void MakeFormReadOnly()
        //{
        //    panelStepButtons.IsEnabled = false;
        //    //panelmMainRightTop.IsEnabled = false;
        //    panelmMainRightBottom.IsEnabled = false;
        //    panelMainTestButtons.IsEnabled = false;
        //    PanelTestDescription.IsEnabled = false;
        //    //PanelTestName.IsEnabled = false;
        //    PanelUserPassword.IsEnabled = false;
        //    //PanelTestOptions.IsEnabled = false;
        //}

        private void LoadTest(String TestPath)
        {
            // load the test
            mLoadedFile = TestPath;


            if (mLoadedFile == "")
            {
                mLoadedFile = mTemplate;
                lnkLink.IsEnabled = false;
            }
            if (!File.Exists(mLoadedFile))
            {
                ShowError(DlkUserMessages.ERR_FILE_NOT_FOUND + mLoadedFile);
                return;
            }
            mLoadedTest = new DlkTest(mLoadedFile);

            // LoadTests
            Title = TEST_EDITOR_TITLE + mLoadedFile;

            // populate the test steps
            LoadPasswordSteps();
            PopulateTestSteps(CurrentInstance);

            // populate the gen info
            txtTestName.Text = mLoadedTest.mTestName;
            txtTestDescription.Text = mLoadedTest.mTestDescription;
            txtTestAuthor.Text = mLoadedTest.mTestAuthor;
            chkContinueOnError.IsChecked = mLoadedTest.mContinueOnError;

            // This handles duplicate controls
            if (mWarnDuplicate && DlkDynamicObjectStoreHandler.mDuplicateControls.Count > 0)
            {
                string duplicatecontrols = "";

                foreach (string item in DlkDynamicObjectStoreHandler.mDuplicateControls)
                {
                    duplicatecontrols += item.Split(';').First() + ": " + item.Split(';').Last() + "\n";
                }
                DlkUserMessages.ShowInfo(DlkUserMessages.INF_DUPLICATE_CONTROLS + duplicatecontrols);
                mWarnDuplicate = false;
            }

            ctlScreenLoading.DataContext = DlkDynamicObjectStoreHandler;
            ctlControlLoading.DataContext = DlkDynamicObjectStoreHandler;
            ctlKeywordLoading.DataContext = DlkDynamicObjectStoreHandler;

            // populate the components
            //cmbComponent.DataContext = mComponents;

            cmbComponent.DataContext = DlkEnvironment.IsShowAppNameProduct && DlkEnvironment.IsShowAppNameEnabled() ? DlkDynamicObjectStoreHandler.Alias.OrderBy(_screen => _screen).ToList() : DlkDynamicObjectStoreHandler.Screens;

            // select the first row in the test steps
            if (mLoadedTest.mTestSteps.Count > 0)
            {
                dgTestSteps.SelectedIndex = 0;
            }

            // update view
            CurrentView = mLoadedTest.mInstanceCount > 1 ? Views.Split : Views.Keyword;
            if (mLoadedTest.mInstanceCount > 1 || (mLoadedTest.Data != null && mLoadedTest.Data.Records.Count > 0))
            {
                optSplitView_Click(null, null);
            }
            else
            {
                optKWView_Click(null, null);
            }
            //optKWView.IsChecked = !optSplitView.IsChecked;

            //UpdateNavButtonStates();

        }

        /// <summary>
        /// Removes all data from the grid
        /// </summary>
        private void ClearTestStepsGrid()
        {
            _TestStepRecords = new ObservableCollection<DlkTestStepRecord>();
            dgTestSteps.DataContext = mTestStepRecords;
        }

        /// <summary>
        /// populates the test steps from the file
        /// </summary>
        private void PopulateTestSteps(int instance = 1)
        {
            ClearTestStepsGrid();

            foreach (DlkTestStepRecord tsr in mLoadedTest.mTestSteps)
            {
                tsr.mCurrentInstance = instance;
                mTestStepRecords.Add(tsr);

            }

            //if (mTests.Count > 0)
            //{
            //    txtCurrentInstance.Text = Instance.ToString();
            //    foreach (DlkTestStepRecord tsr in mTests[Instance - 1].mTestSteps)
            //    {
            //        tsr.mCurrentInstance = Instance;
            //        mTestStepRecords.Add(tsr);
            //    }
            //}
        }

        /// <summary>
        /// gets all row of a specific data grid
        /// </summary>
        public List<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource;
            List<DataGridRow> rows = new List<DataGridRow>();
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    rows.Add((DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(item));

                }
            }

            return rows;
        }


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
        /// Populates the Execute field and Parameter grid in Update Panel with data values if any
        /// </summary>
        private void PopulateUpdatePanelWithDataValues()
        {
            ExecuteItems = new List<string>(new string[] { bool.TrueString, bool.FalseString });
            if (DataTableHasColumns)
            {
                mCurrentContextMenu.Items.Clear();
                ExecuteItems.Add(string.Empty); /* line separator */
                foreach (DlkDataRecord dr in mLoadedTest.Data.Records)
                {
                    ExecuteItems.Add("D{" + dr.Name + "}");
                    mCurrentContextMenu.Items.Add(SetMenuItem("D{" + dr.Name.Replace("_","__") + "}", InsertDataContextMenuClicked));
                }
            }
            dgParameters.Items.Refresh();
            cmbExecuteStep.ItemsSource = ExecuteItems;
            cmbExecuteStep.Items.Refresh();
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

        private void SetIsEnablePropertyOnMultiSelect(bool isEnabled)
        {
            cmbComponent.IsEnabled = isEnabled;
            cmbControl.IsEnabled = isEnabled;
            cmbKeyword.IsEnabled = isEnabled;
            dgParameters.IsEnabled = isEnabled;

            btnTestStepNew.IsEnabled = isEnabled && !hasGridFilter;
            btnTestStepInsert.IsEnabled = isEnabled && !hasGridFilter;
            btnTestStepDelete.IsEnabled = isEnabled && !hasGridFilter;

            if (!isEnabled)
            {
                lblExecuteStep.FontWeight = FontWeights.Bold;
                lblStepDelay.FontWeight = FontWeights.Bold;
                chkBoxExecuteStep.Visibility = Visibility.Visible;
                chkBoxStepDelay.Visibility = Visibility.Visible;
                chkBoxExecuteStep.IsChecked = true;
                chkBoxStepDelay.IsChecked = false;
            }
            else
            {
                lblExecuteStep.FontWeight = FontWeights.Normal;
                lblStepDelay.FontWeight = FontWeights.Normal;
                chkBoxExecuteStep.Visibility = Visibility.Collapsed;
                chkBoxStepDelay.Visibility = Visibility.Collapsed;
                chkBoxExecuteStep.IsChecked = false;
                chkBoxStepDelay.IsChecked = false;
            }

        }

        private void UpdateDataTabSelection(string ColumnName)
        {
            int idx = 0;
            for (idx = 0; idx < dgDataDriven.Columns.Count; idx++)
            {
                if (ColumnName == dgDataDriven.Columns[idx].Header.ToString().Replace("__", "_"))
                {
                    break;
                }
            }

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate()
                    {
                        try
                        {
                            dgDataDriven.Focus();
                            dgDataDriven.SelectedCells.Clear();
                            dgDataDriven.CurrentCell = new DataGridCellInfo(dgDataDriven.Items[0], dgDataDriven.Columns[idx]);
                            dgDataDriven.SelectedCells.Add(dgDataDriven.CurrentCell);
                            dgDataDriven.SelectedItem = dgDataDriven.CurrentCell;
                            dgDataDriven.UpdateLayout();
                            dgDataDriven.CurrentColumn = dgDataDriven.Columns[0];
                            dgDataDriven.CurrentColumn = dgDataDriven.Columns[idx];
                            dgDataDriven.ScrollIntoView(dgDataDriven.CurrentCell, dgDataDriven.CurrentColumn);
                            dgTestSteps.Focus();
                        }
                        catch (Exception ex)
                        {
                            DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);

                        }
                    }));
        }

        private void UpdateRowSelection(List<int> RowIndex)
        {
            if (RowIndex.Count <= 0)
            {
                return;
            }

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                   new Action(delegate()
                   {
                       try
                       {

                           foreach (int index in RowIndex)
                           {
                               var item = dgTestSteps.Items[index];
                               dgTestSteps.CurrentItem = item;
                               DataGridRow visualItem =
                                   (DataGridRow)
                                       dgTestSteps.ItemContainerGenerator.ContainerFromItem(dgTestSteps.CurrentItem);
                               visualItem.IsSelected = true;
                               dgTestSteps.SelectedItems.Add(visualItem);

                           }
                       }
                       catch (Exception ex)
                       {
                           DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);

                       }

                   }));
        }

        private void UpdateDataTabSelection(int ColumnIndex, int RowIndex)
        {
            if (ColumnIndex < 0 || RowIndex < 0)
            {
                return;
            }

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate()
                    {
                        try
                        {
                            dgDataDriven.Focus();
                            dgDataDriven.SelectedCells.Clear();
                            dgDataDriven.CurrentCell = new DataGridCellInfo(dgDataDriven.Items[RowIndex], dgDataDriven.Columns[ColumnIndex]);
                            dgDataDriven.SelectedCells.Add(dgDataDriven.CurrentCell);
                            dgDataDriven.SelectedItem = dgDataDriven.CurrentCell;
                            dgDataDriven.UpdateLayout();
                            dgDataDriven.CurrentColumn = dgDataDriven.Columns[0];
                            dgDataDriven.CurrentColumn = dgDataDriven.Columns[ColumnIndex];
                            dgDataDriven.ScrollIntoView(dgDataDriven.CurrentCell, dgDataDriven.CurrentColumn);
                        }
                        catch (Exception ex)
                        {
                            DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);

                        }
                    }));
        }

        private void UpdateStep()
        {
            isChanged = true;
            int iCurrentRec = ((DlkTestStepRecord)dgTestSteps.SelectedItem).mStepNumber - 1;
            DlkTestStepRecord currStep = mLoadedTest.mTestSteps[iCurrentRec];
            currStep.mExecute = cmbExecuteStep.Text;
            currStep.mScreen = cmbComponent.Text;
            currStep.mControl = cmbControl.Text;
            //currStep.mControl = cmbControl.Text;
            currStep.mKeyword = cmbKeyword.Text;
            //if ((bool)chkUpdateAll.IsChecked)
            {
                for (int idx = 0; idx < currStep.mParameters.Count; idx++)
                {
                    currStep.mParameters[idx] =  GetParametersFromGrid();
                }
            }

            //used to update password masked parameters
            if (mPasswordMasked != null && mPasswordMasked.PasswordParameters != null)
            {
                currStep.mPasswordParameters = new List<string>();
                for (int i = 0; i < mPasswordMasked.PasswordParameters.Count(); i++)
                {
                    DlkPasswordParameter _pMasked = mPasswordMasked.Step.Parameters.SingleOrDefault(s => s.Index == i);
                    if (_pMasked != null)
                        currStep.mPasswordParameters.Add(_pMasked.Password);
                    else
                        currStep.mPasswordParameters.Add("");
                }
            }
            else
            {
                currStep.mPasswordParameters = null;
            }
            //else
            //{
            //    currStep.mParameters[CurrentTestInstance - 1] = GetParametersFromGrid();
            //}
            currStep.mStepDelay = int.Parse(txtDelay.Text);
            UpdateFilterContent(currStep);
        }

        private void UpdateStepsExecution()
        {
            isChanged = true;
            int iCurrentRec = 0;
            foreach (object selectedItem in dgTestSteps.SelectedItems)
            {
                iCurrentRec = ((DlkTestStepRecord)selectedItem).mStepNumber - 1;
                DlkTestStepRecord currStep = mLoadedTest.mTestSteps[iCurrentRec];                
                currStep.mExecute = chkBoxExecuteStep.IsChecked == true ? cmbExecuteStep.Text : currStep.mExecute;
                currStep.mStepDelay = chkBoxStepDelay.IsChecked == true ? int.Parse(txtDelay.Text) : currStep.mStepDelay;
            }
            DlkTestStepRecord step = mLoadedTest.mTestSteps[iCurrentRec];
            UpdateFilterContent(step);
        }

        private void SaveTest(string DialogName = "")
        {
            UpdateData();
            SaveTestDialog dlg = new SaveTestDialog(mLoadedTest, "", DialogName);
            dlg.Owner = this;
            mLoadedTest.mTestName = txtTestName.Text;
            mLoadedTest.mTestDescription = txtTestDescription.Text.Trim();
            mLoadedTest.mTestAuthor = txtTestAuthor.Text;

            bool? bSaveAndCheckin;
            string filePath;


            dlg.SaveTest(out bSaveAndCheckin, out filePath);
            if (bSaveAndCheckin != null)
            {
                SaveInBackground((bool)bSaveAndCheckin, filePath);
            }

            //reload test description after trimming and saving
            if (txtTestDescription.Text != mLoadedTest.mTestDescription)
            {
                txtTestDescription.Text = mLoadedTest.mTestDescription;
            }
        }

        /// <summary>
        /// Save test via Save worker
        /// </summary>
        /// <param name="bSaveAndCheckin">Flag to checkin after save</param>
        /// <param name="filePath">Save path </param>
        /// <param name="DisplaySuccessMessage">Flag to display success message after save</param>
        /// <param name="Reload"><Flag to reload after save/param>
        private void SaveInBackground(bool bSaveAndCheckin, string filePath, bool DisplaySuccessMessage = true, bool Reload = false)
        {
            string sComments = string.Empty;
            StatusText = STATUS_TEXT_SAVING;
            if (bSaveAndCheckin == true)
            {
                CheckInCommentsDialog commentDlg = new CheckInCommentsDialog();
                if (commentDlg.ShowDialog() == true)
                {
                    sComments = commentDlg.UserComments;
                }
            }
            IsTestEditorBusy = true;
            mSaveWorker.RunWorkerAsync(new object[] { bSaveAndCheckin, filePath, sComments, DisplaySuccessMessage, Reload });
        }

        /// <summary>
        /// Display last update info (local or server) of Keyword file and Data file asynchronously
        /// </summary>
        /// <param name="IsServerValue">Flag to get server value</param>
        /// <param name="keywordPath">Abs Path of Keyword file (.xml)</param>
        /// <param name="dataPath">Abs Path of Data file (.trd)</param>
        private void GetLastUpdateInfoInBackground(bool IsServerValue, string keywordPath, string dataPath)
        {
            StatusText = STATUS_TEXT_WAITING_FOR_SERVER;
            IsTestEditorBusy = true;
            mServerInfoWorker.RunWorkerAsync(new object[] { IsServerValue, keywordPath, dataPath });
        }

        /// <summary>
        /// Save test to local location
        /// </summary>
        /// <param name="sFilePath">Save path</param>
        /// <returns>TRUE if successfully saved; FALSE otherwise</returns>
        private bool SaveLocal(string sFilePath)
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

                        // Check for dataFile
                        string dataFile = new DlkTest(sFilePath).Data.Path;
                        if (File.Exists(dataFile))
                        {
                            FileInfo fi2 = new FileInfo(dataFile);
                            fi2.IsReadOnly = false;
                        }
                    }
                    mLoadedTest.mTestPath = sFilePath;
                    mLoadedTest.WriteTest(sFilePath, true);
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
        /// Save test and check-in to TFS after
        /// </summary>
        /// <param name="sFilePath">Save path</param>
        /// <param name="CheckInComments">TFS Check-in comments</param>
        /// <returns>TRUE if successfully saved; FALSE otherwise</returns>
        private bool SaveAndCheckIn(string sFilePath, string CheckInComments)
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
                    mLoadedTest.mTestPath = sFilePath;
                    mLoadedTest.WriteTest(sFilePath, true);
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
        /// Add file to source control
        /// </summary>
        /// <param name="parameters">Method arguments</param>
        private void AddToSource(object[] parameters)
        {
            DlkSourceControlHandler.Add(parameters.First().ToString(), parameters.Last().ToString());
            string dataPath = new DlkTest(parameters.First().ToString()).Data.Path;
            if (System.IO.File.Exists(dataPath))
            {
                DlkSourceControlHandler.Add(dataPath, parameters.Last().ToString());
            }
        }

        /// <summary>
        /// Check-in to source control
        /// </summary>
        /// <param name="parameters">Method arguments</param>
        private void CheckInToSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckIn(parameters.First().ToString(), parameters.Last().ToString());
            string dataPath = new DlkTest(parameters.First().ToString()).Data.Path;
            if (System.IO.File.Exists(dataPath))
            {
                DlkSourceControlHandler.CheckIn(dataPath, parameters.Last().ToString());
            }
        }

        /// <summary>
        /// Checkout from source control
        /// </summary>
        /// <param name="parameters">Method arguments</param>
        private void CheckOutFromSource(object[] parameters)
        {
            DlkSourceControlHandler.CheckOut(parameters.First().ToString(), parameters.Last().ToString());
            // Check if data file exists
            string dataPath = new DlkTest(parameters.First().ToString()).Data.Path;
            if (System.IO.File.Exists(dataPath))
            {
                DlkSourceControlHandler.CheckOut(dataPath, parameters.Last().ToString());
            }
        }

        private void ExecuteTest()
        {
            /*Set to false to ensure new browser is started on ad hoc runs*/
            DlkEnvironment.mKeepBrowserOpen = false;
            DlkTestExecute.IsBrowserOpen = false;

            DlkTestRunnerApi.CurrentRunningStep = 0;
            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            IsRunInProgress = true;
            StatusText = STATUS_TEXT_RUNNING;
            IsTestEditorBusy = true;
            bw.RunWorkerAsync();
        }

        private void UpdateTestInMemory()
        {
            //String sTestName = txtTestName.Text.Trim();
            //String sTestDesc = txtTestDescription.Text.Trim();
            //String sTestUser = txtUser.Text.Trim();
            //String sTestPassword = txtPassword.Text.Trim();
            //Boolean bBackupRestoreGlobalDat = Convert.ToBoolean(chkBackupRestoreGlobalDat.IsChecked);
            //Boolean bBackupRestoreProjDat = Convert.ToBoolean(chkBackupRestoreProjDat.IsChecked);
            //Boolean bDeleteConfig = Convert.ToBoolean(chkDeleteConfig.IsChecked); ;
            //TestInfoRecord mInfoUpdate = new TestInfoRecord(sTestName, sTestDesc, 
            //    0, sTestUser, sTestPassword, bBackupRestoreGlobalDat, bBackupRestoreProjDat, 
            //    bDeleteConfig, mLoadedTest.TestInfo.restorefromzip);
            //mLoadedTest.UpdateTest(mInfoUpdate, mTestStepRecords.ToList());
        }

        private String SaveFileVal()
        {
            //String sTestName = txtTestName.Text.Trim();
            //if (sTestName == "")
            //{
            //    return "Please enter a test name";
            //}
            //String sTestDesc = txtTestDescription.Text.Trim();
            //if (sTestDesc == "")
            //{
            //    return "Please enter a test description";
            //}
            //String sTestUser = txtUser.Text.Trim();
            //if (sTestUser == "")
            //{
            //    return "Please enter a user";
            //}
            return "";
        }

        private void UpdateStepButtonStates()
        {
            bool hasFirstStep = false;
            bool hasLastStep = false;
            int idx = dgTestSteps.SelectedIndex;

            btnTestStepDelete.IsEnabled = idx != -1 && _TestStepRecords.Count > 1 && !hasGridFilter;
            btnTestStepUp.IsEnabled = _TestStepRecords.Count > 1 && idx != 0 && !hasGridFilter;
            btnTestStepDown.IsEnabled = _TestStepRecords.Count > 1 && idx != _TestStepRecords.Count - 1 && !hasGridFilter;

            if (dgTestSteps.SelectedItems.Count > 1)
            {
                List<int> StepNumbers = new List<int>();
                foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
                {
                    if (dsr.mStepNumber == 1)
                    {
                        hasFirstStep = true;
                    }
                    if (dsr.mStepNumber == _TestStepRecords.Count)
                    {
                        hasLastStep = true;
                    }
                    StepNumbers.Add(dsr.mStepNumber);
                }
                btnTestStepUp.IsEnabled = !hasFirstStep && !hasGridFilter;
                btnTestStepDown.IsEnabled = !hasLastStep && !hasGridFilter;
                /*Check if contiguous */
                StepNumbers.Sort();
                isSelectedStepContiguous = !StepNumbers.Select((i, j) => i - j).Distinct().Skip(1).Any();
                if (!isSelectedStepContiguous)
                {
                    btnTestStepDown.IsEnabled = false;
                    btnTestStepUp.IsEnabled = false;
                }
            }
            btnTestStepNew.IsEnabled = !hasGridFilter;
            btnTestStepInsert.IsEnabled = !hasGridFilter;
            btnTestStepCopy.IsEnabled = !hasGridFilter;
            btnTestStepUpdate.IsEnabled = true;
        }

        private bool ValidateUpdate()
        {
            //TODO: set an if for multiselect
            bool ret = false;
            bool bExecuteError = true;
            bool bComponeError = true;
            bool bKeywordError = true;
            bool bControlError = true;
            bool bStepDelayError = true;
            bool bWarn = false;
            //bool bParametersError = true;

            SolidColorBrush errorColor = new SolidColorBrush(Colors.Red);
            string msg = "";
            List<string> warnList = new List<string>();

            CleanUpdatePanel();
            // mark all field failing validation

            // validate
            // regex for data driven value
            bExecuteError = !(cmbExecuteStep.Text.ToLower() == "true" || cmbExecuteStep.Text.ToLower() == "false" || 
                new Regex(@"^D{([a-zA-Z0-9_]+( [a-zA-Z0-9_]+)+|[a-zA-Z0-9_]+)}$").IsMatch(cmbExecuteStep.Text));

            //bComponeError = !mComponents.Contains(cmbComponent.Text);

            if (dgTestSteps.SelectedItems.Count > 1)
            {
                bComponeError = false;
                bKeywordError = false;
                bControlError = false;
            }
            else
            {
                bComponeError = !DlkDynamicObjectStoreHandler.Screens.Contains(screenName);
                bControlError = (!bComponeError && !string.IsNullOrEmpty(cmbControl.Text)) && !mControls.Contains(cmbControl.Text);
                bControlError = (!bComponeError && !string.IsNullOrEmpty(cmbControl.Text)) && !mControls.Contains(cmbControl.Text);
                if (cmbControl.Text == "Password")
                {
                    bKeywordError = !mRestrictedKWs.Contains(cmbKeyword.Text);
                }
                else
                {
                    bKeywordError = !mKeywords.Contains(cmbKeyword.Text);
                }
            }
            bStepDelayError = ((chkBoxStepDelay.IsChecked == true) && (!DlkString.IsNumeric(txtDelay.Text) || String.IsNullOrEmpty(txtDelay.Text)));
            // Mark in UI
            if (bExecuteError)
            {
                lblExecuteStep.Foreground = errorColor;
                bWarn = true;
                warnList.Add("Execute");
                //cmbExecuteStep.Text = "";
            } 

            if (bComponeError)
            {
                lblScreen.Foreground = errorColor;
                bWarn = true;
                warnList.Add("Screen");

                //cmbComponent.Text = "";
            }

            if (bControlError)
            {
                lblControl.Foreground = errorColor;
                bWarn = true;
                warnList.Add("Control");

                //cmbControl.Text = "";
            }

            if (bStepDelayError)
            {
                lblStepDelay.Foreground = errorColor;
                bWarn = true;
                warnList.Add("Step Delay");
            }

            if (bKeywordError)
            {
                lblKeyword.Foreground = errorColor;
                bWarn = true;
                warnList.Add("Keyword");

                //cmbKeyword.Text = "";
            }
            else
            {
                //bParametersError = !ValidateParameters();

                //if (bParametersError)
                //{
                //    lblParameters.Foreground = errorColor;
                //}
            }

            if (bWarn)
            {
                foreach (string li in warnList)
                {
                    msg += li + " | ";
                }
                msg = msg.Remove(msg.Length - 2);
                DlkUserMessages.ShowWarning(DlkUserMessages.INF_FIELDS_INCORRECT_VALUES + Environment.NewLine + msg + Environment.NewLine + Environment.NewLine + DlkUserMessages.INF_FIELDS_REPLACE_INCORRECT_VALUES, "Update Failed");
            }
            ret = !bExecuteError && !bComponeError && !bControlError && !bKeywordError && !bStepDelayError;

            return ret;
        }

        private bool ValidateParameters()
        {
            string mControl = "";
            bool ret = true;

            // assert that keyword selected is valid
            if (cmbControl.Text == null)
            {
                mControl = cmbComponent.SelectedItem.ToString();
            }
            else if (cmbControl.Text == "Function")
            {
                mControl = "Function";
            }
            else
            {
                mControl = DlkDynamicObjectStoreHandler.GetControlType(cmbComponent.SelectedItem.ToString(), cmbControl.Text);
            }
            List<string> prms = DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, cmbKeyword.SelectedValue.ToString());

            string displayedParams = GetParametersFromGrid();

            int actual = string.IsNullOrEmpty(displayedParams) ? 0 : 1;
            actual = actual > 0 ? actual + displayedParams.Count(x => x == '|') : 0;
            int reference = prms.Count;

            if (actual == 0 && reference == 1) // special case
            {
                // do nothing
            }
            else if (actual != reference)
            {
                ret = false;
            }
            return ret;
        }

        private string GetParametersFromGrid()
        {
            string ret = string.Empty;
            bool hasParam = false;
            string delimiter = DlkTestStepRecord.globalParamDelimiter;
            foreach (DlkKeywordParameterRecord dr in dgParameters.Items)
            {
                ret += dr.mValue + delimiter;
                hasParam = true;
            }
            if (hasParam)
            {
                int count = CountStringOccurrences(ret, delimiter);
                if (count != dgParameters.Items.Count - 1 && ret.EndsWith(delimiter))
                {
                    ret = ret.Substring(0, ret.Length - delimiter.Length);
                }
            }
            return ret;
        }

         /// <summary>
        /// Loop through all instances of the string 'text'.
        /// </summary>
        /// <param name="text">Whole string</param>
        /// <param name="pattern">value to find in a string</param>
        /// <returns>Count of the pattern occurence in the given text</returns>
        public int CountStringOccurrences(string text, string pattern)
        {
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        private void CleanUpdatePanel()
        {
            SolidColorBrush defaultColor = new SolidColorBrush(Colors.Black);
            lblExecuteStep.Foreground = defaultColor;
            lblScreen.Foreground = defaultColor;
            lblControl.Foreground = defaultColor;
            lblKeyword.Foreground = defaultColor;
            lblParameters.Foreground = defaultColor;
            lblStepDelay.Foreground = defaultColor;
            // remove all validation indicators
        }

        /// <summary>
        /// Creates parameter name when converting to data using control name and instance
        /// </summary>
        /// <param name="stepControlName">control name of the step</param>
        /// <returns>Parameter name with instance</returns>
        private string GetParamName(string stepControlName)
        {
            int instance = 0;
            string parameterName = stepControlName;

            while(mLoadedTest.Data.Records.FindAll(x => x.Name == parameterName).Count >= 1)
            {
               instance++;
               parameterName = stepControlName + "_" + instance;
            }

            return parameterName;
        }

        private void PopulateData(DataTable dtSource = null, List<int> stepsForDataGeneration = null, Dictionary<int, string> paramConversionSetting = null)
        {
            if (!(CurrentView == Views.Data || CurrentView == Views.Split))
                return;

            dgDataDriven.CommitEdit(DataGridEditingUnit.Cell, true);
            dgDataDriven.CommitEdit(DataGridEditingUnit.Row, true);

            if (stepsForDataGeneration != null)
            {
                try
                {
                    foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
                    {
                        string currentConversionSetting = "";
                        if (stepsForDataGeneration.Contains(step.mStepNumber))
                        {
                            if (step.mKeyword == "Click") continue;
                            if (step.mKeyword == "Close") continue;
                            if (string.IsNullOrEmpty(step.mControl) && step.mScreen != "Database") continue;
                            if (step.mParameters.Count <= 0) continue;
                            if (step.mParameterOrigString == "") continue; //mParameterString - combination of all parameters separated by delimiter
                            if (IsStepPasswordBlank(step)) continue; // skip if the parameter is only a blank password

                            bool ConvertAllParam = false;
                            var prmSetting = new List<string>();
                            if (paramConversionSetting != null)
                            {
                                //get paramConversionSetting for this step
                                string stepDataConversionSetting = paramConversionSetting[step.mStepNumber];
                                currentConversionSetting = stepDataConversionSetting;
                                conversionSettings.Add(currentConversionSetting);
                                prmSetting = stepDataConversionSetting.Split('|').ToList();
                                ConvertAllParam = false;
                                conversionSettingsPreviousValue = stepDataConversionSetting;
                                if (prmSetting.Count < step.mParameters[0].Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None).Length)
                                {
                                    throw new Exception("Not all parameters have conversion setting.");
                                }
                            }
                            else
                            {
                                conversionSettings.Add(currentConversionSetting);
                                ConvertAllParam = true;
                            }

                            string reusableParamName = GetReusableParamName(step.mStepNumber, currentConversionSetting);

                            if (reusableParamName == string.Empty)
                            {
                                //convert data using a new param name
                                ConvertParameterToData(step, ConvertAllParam, prmSetting);
                            }
                            else
                            {
                                //reuse existing param name whenever applicable
                                ConvertParameterToData(step, ConvertAllParam, prmSetting, reusableParamName);                                
                            }
                            isContextItemSelected = true;
                            dgTestSteps.Items.Refresh();
                        }
                        else
                        {
                            conversionSettings.Add(currentConversionSetting);
                        }
                    }
                    conversionSettings.Clear();
                }
                catch (Exception ex)
                {
                    DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                }
            }

            if (dtSource == null && mLoadedTest.Data.Records.Count > 0)
            {
                dtSource = new DataTable();

                foreach (var record in mLoadedTest.Data.Records)
                {
                    dtSource.Columns.Add(record.Name);
                }

                int rowCount = mLoadedTest.Data.Records.First().Values.Count;
                int colCount = mLoadedTest.Data.Records.Count;
                if (rowCount > 0)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        List<string> values = new List<string>();
                        for (int col = 0; col < colCount; col++)
                        {
                            values.Add(mLoadedTest.Data.Records[col].Values[i]);
                        }
                        dtSource.Rows.Add(values.ToArray());
                    }
                }
            }
            else if (dtSource == null)
                dtSource = new DataTable();

            dgDataDriven.ItemsSource = null;
            dgDataDriven.ItemsSource = dtSource.AsDataView();

            //UI states
            btnNewRow.IsEnabled = mLoadedTest.Data.Records.Count > 0;
            btnClearRow.IsEnabled = btnNewRow.IsEnabled;
            menuitemNewRow.IsEnabled = btnNewRow.IsEnabled;
        }

        /// <summary>
        /// Convert a step's parameter to data driven
        /// </summary>
        /// <param name="step">Test step</param>
        /// <param name="ConvertAllParam">True if chosen option is convert all, false if chosen option is modify before converting</param>
        /// <param name="prmSetting">Customized conversion setting created by user</param>
        /// <param name="reusableParamName">Existing parameter name from other steps of the same parameter that can be reused</param>
        private void ConvertParameterToData(DlkTestStepRecord step, bool ConvertAllParam, List<string> prmSetting, string reusableParamName = "")
        {
            string delimiter = DlkTestStepRecord.globalParamDelimiter;
            string convertedParamName = "";
            int recNeededForthisStep = step.mParameters[0].Split(new[] { delimiter }, StringSplitOptions.None).Length;
            var reusableParamNameList = reusableParamName.Split(new[] { delimiter }, StringSplitOptions.None).ToList();

            for (int i = 0; i < recNeededForthisStep; i++)
            {
                if (DlkData.IsDataDrivenParam(step.mParameters[0].Split(new[] { delimiter }, StringSplitOptions.None)[i]))
                    //if (step.mParameters[0].Split(new[] { delimiter }, StringSplitOptions.None)[i].Contains("D{"))
                {
                    //already data driven - use parameter value if already data driven
                    convertedParamName += step.mParameters[0].Split(new[] { delimiter }, StringSplitOptions.None)[i] + delimiter;
                }
                else if (ConvertAllParam || Convert.ToBoolean(prmSetting[i]))
                {
                    //convert parameter to data if convert all or setting is true
                    //use reusableParamName when available else generate new param name

                    if (reusableParamName == "" || !DlkData.IsDataDrivenParam(reusableParamNameList[i]))
                    {
                        string paramName = "";
                        paramName = GetParamName(step.mScreen == "Database" ? "Database" : step.mControl);

                        DlkDataRecord rec = new DlkDataRecord(paramName);
                        mLoadedTest.Data.Records.Add(rec);
                        string[] _stepParameters = step.mParameters[0].Split(new[] { delimiter }, StringSplitOptions.None);

                        string getParameter(int index)
                        {
                            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct && step.mPasswordParameters != null)
                                return step.mPasswordParameters[index] != "" ? step.mPasswordParameters[index] :
                                    _stepParameters[index].Equals(DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE) ? step.mPasswordParameters[index] : _stepParameters[index];
                            else
                                return _stepParameters[index];
                        };

                        if (mLoadedTest.Data.Records.First().Values.Count == 0)
                        {
                            for (int idx = 0; idx < mLoadedTest.Data.Records.Count; idx++)
                            {
                                mLoadedTest.Data.Records[idx].Values.Add(string.Empty);
                                if (idx == mLoadedTest.Data.Records.Count - 1)
                                {
                                    mLoadedTest.Data.Records[idx].Values[0] = getParameter(i);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < mLoadedTest.Data.Records.First().Values.Count; j++)
                            {
                                rec.Values.Add(getParameter(i));
                            }
                        }
                        convertedParamName += "D{" + rec.Name + "}" + delimiter;
                    }
                    else
                    {
                        convertedParamName += reusableParamNameList[i] + delimiter;
                    }
                }
                else
                {
                    //use parameter value if setting is false
                    convertedParamName += step.mParameters[0].Split(new[] { delimiter }, StringSplitOptions.None)[i] + delimiter;
                }

            }
            for (int i = 0; i < step.mParameters.Count; i++)
            {
                step.mParameters[i] = convertedParamName.Substring(0, convertedParamName.Length - 4);
            }

            ConvertPasswordToData(step);
        }

        /// <summary>
        /// Store data driven parameter to DlkTestStepRecord mPasswordParameters
        /// </summary>
        /// <param name="step"></param>
        private void ConvertPasswordToData(DlkTestStepRecord step)
        {
            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct)
            {
                mPasswordMasked = new PasswordMasked(step.mScreen, step.mControl, step.mKeyword, step);

                if (mPasswordMasked.AllowMasks)
                {

                    string[] splitParams = step.mParameters[0].Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);

                    for (int i = 0; i < splitParams.Length; i++)
                    {
                        var pMasked = mPasswordMasked.Step.Parameters.FirstOrDefault(f => f.Index == i);
                        if (pMasked != null && DlkData.IsDataDrivenParam(splitParams[i]))
                        {
                            step.mPasswordParameters[i] = splitParams[i];
                        }
                    }
                }
                else
                    mPasswordMasked = null;
            }
        }

        /// <summary>
        /// Get data driven parameter name where values are the same as the ones being converted
        /// </summary>
        /// <param name="currentStepNumber">Step number of step being converted</param>
        /// <returns>Returns existing paramaeter name that can be reused by the current step</returns>
        private string GetReusableParamName(int currentStepNumber, string currentConversionSettings)
        {
            string reusableParameterName = string.Empty;

            try
            {
                //get data driven values if existing
                if (mLoadedTest.Data.Records.Count > 0)
                {
                    List<DlkTestStepRecord> loadedTestSteps = mLoadedTest.mTestSteps;
                    DlkTestStepRecord currentStep = loadedTestSteps.Find(x => x.mStepNumber == currentStepNumber);
                    string currentStepParameter = null;


                    if (currentStep.mPasswordParameters != null)
                    {
                        string[] origParameter = currentStep.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                        for (int i = 0; i < origParameter.Length; i++)
                        {
                            if (currentStep.mPasswordParameters[i] != "" || origParameter[i] == DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE)
                            {
                                origParameter[i] = currentStep.mPasswordParameters[i];
                            }
                        }
                        currentStepParameter = string.Join(DlkTestStepRecord.globalParamDelimiter, origParameter);
                    }
                    else
                        currentStepParameter = currentStep.mParameterOrigString;

                    //get all test step with data driven, same screen name, same parameter numbers and same control name
                    List<DlkTestStepRecord> sameControlTestStep = loadedTestSteps.FindAll(x => x.mParameterOrigString.Contains("D{") && x.mControl == currentStep.mControl && x.mScreen == currentStep.mScreen && x.mParameterString.Split('|').Length == currentStep.mParameterString.Split('|').Length);

                    //generate original value of already converted steps
                    Dictionary<int, string> stepWithOrigParam = new Dictionary<int, string>();
                    stepWithOrigParam = FindOriginalParameterOfConvertedSteps(sameControlTestStep);

                    //construct parameter string based on existing steps
                    List<string> convertSettings = currentConversionSettings != String.Empty ? currentConversionSettings.Split('|').ToList() : new List<string>();
                    string[] currentStepParams = currentStepParameter.Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                    string paramString = "";
                    int paramIndex = 0;
                    foreach (string stepParam in currentStepParams)
                    {
                        string settingToAdd = currentStepParams[paramIndex];
                        if (!DlkData.IsDataDrivenParam(settingToAdd))
                        {
                            if (!convertSettings.Any() || convertSettings[paramIndex] == "True")
                            {
                                foreach (DlkTestStepRecord record in sameControlTestStep)
                                {
                                    string parameter = record.mParameterString.Split('|')[paramIndex];
                                    string originalParameter = stepWithOrigParam[record.mStepNumber].Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None)[paramIndex];
                                    if (DlkData.IsDataDrivenParam(parameter) && originalParameter == currentStepParams[paramIndex])
                                    {
                                        settingToAdd = parameter;
                                        break;
                                    }
                                }
                            }
                        }
                        paramString += settingToAdd + DlkTestStepRecord.globalParamDelimiter;
                        paramIndex++;
                    }
                    paramString = paramString.Remove(paramString.LastIndexOf(DlkTestStepRecord.globalParamDelimiter), DlkTestStepRecord.globalParamDelimiter.Length);
                    reusableParameterName = paramString;
                }

                return reusableParameterName;
            }
            catch
            {
                //do nothing
            }
            return reusableParameterName;
        }

        /// <summary>
        /// Retrieves the equivalent values of respective parameters from data driven view
        /// </summary>
        /// <param name="testStep">List of dataDriven test steps requesting for original parameter Values</param>
        /// <returns>Returns a dictionary of step number and parameter's oriiginal value</returns>
        private Dictionary<int, string> FindOriginalParameterOfConvertedSteps(List<DlkTestStepRecord> testStep)
        {
            Dictionary<int, string> stepsWithOrigParameter = new Dictionary<int, string>();
            try
            {
                string delimiter = DlkTestStepRecord.globalParamDelimiter;
                foreach (var step in testStep)
                {
                    var origValue = string.Empty;

                    foreach (string param in step.mParameterOrigString.Split(new[] { delimiter }, StringSplitOptions.None))
                    {
                        string variableName = string.Empty;
                        var value = param;
                        if (param.Contains("D{"))
                        {
                            variableName = param.Replace("D{", "").Replace("}", "");
                            value = mLoadedTest.Data.Records.Find(x => x.Name == variableName).Values.First();
                        }
                        if (value != null)
                        {
                            origValue += value + DlkTestStepRecord.globalParamDelimiter;
                        }
                    }
                                                   
                    if(origValue.EndsWith(delimiter))
                    {
                        origValue = origValue.Substring(0, origValue.Length - 4);
                    }
                    stepsWithOrigParameter.Add(step.mStepNumber, origValue);
                }

                //ID: StepNumber
                //Values - DataDrivenVariableValues (based on first row of data driven)
                return stepsWithOrigParameter;
            }
            catch
            {
                //do nothing
            }
            return stepsWithOrigParameter;
        }

        /// <summary>
        /// Select password enabled keyword
        /// </summary>
        private void SelectPasswordControl()
        {
            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct && dgTestSteps.SelectedIndex > -1)
            {
                var step = mLoadedTest.mTestSteps[dgTestSteps.SelectedIndex];
                if (step != null && step.mScreen != "" && step.mPasswordParameters != null)
                {
                    isPasswordLastControl = true;
                    mPasswordMasked = new PasswordMasked(step.mScreen, step.mControl, step.mKeyword, step);
                }
                else
                { 
                    mPasswordMasked = null;
                }
            }
        }

        /// <summary>
        /// Update data (instances) information to be committed on save
        /// </summary>
        private void UpdateData()
        {
            /* Populate DataView based on Data file and/or backward compatibility logic */
            PopulateData();

            /* If need to add parameter nodes in test file, do so */
            if (mLoadedTest.Data.Records.Count > 0 && mLoadedTest.Data.Records.First().Values.Count > 0)
            {
                foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
                {
                    if (step.mParameters.Count > 0)
                    {
                        string paramToCopy = step.mParameters.First();
                        step.mParameters.Clear();
                        for (int idx = 0; idx < mLoadedTest.Data.Records.First().Values.Count; idx++)
                        {
                            step.mParameters.Add(paramToCopy);
                        }
                    }
                }
            }
            else /* If need to subtract, do so. This will ensure 1 instance test if no data information obtained */
            {
                foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
                {
                    if (step.mParameters.Count > 0)
                    {
                        while (step.mParameters.Count != 1)
                        {
                            step.mParameters.RemoveAt(step.mParameters.Count - 1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Toggles button and menus for data-driven test
        /// </summary>
        private void ToggleButtonAndMenusDgData()
        {
            if (mAutoChangedDataCellSelection)
            {
                btnLeftData.IsEnabled = GetColumnIndex() > 0;
                btnRightData.IsEnabled = GetColumnIndex() < dgDataDriven.Columns.Count - 1;
                return;
            }
            mAutoChangedDataCellSelection = false;
            if (!String.IsNullOrEmpty(mRowHeaderClicked))
            {
                //whole row
                btnUpData.IsEnabled = GetRowIndexFromDataGrid(dgDataDriven, mRowHeaderClicked) > 0;
                btnDownData.IsEnabled = GetRowIndexFromDataGrid(dgDataDriven, mRowHeaderClicked) < dgDataDriven.Items.Count - 1;
            }
            else
            {
                //single cell
                dgDataDriven.SelectionMode = DataGridSelectionMode.Single;
                dgDataDriven.SelectionUnit = DataGridSelectionUnit.Cell;
                mSelectedDataRows.Clear();
                btnUpData.IsEnabled = dgDataDriven.Items.IndexOf(dgDataDriven.CurrentItem) > 0 && dgDataDriven.SelectedCells.Count > 0;
                btnDownData.IsEnabled = dgDataDriven.Items.IndexOf(dgDataDriven.CurrentItem) < dgDataDriven.Items.Count - 1 && dgDataDriven.SelectedCells.Count > 0;
            }

            SetCopyPasteRowButtonStates();
            int itemsCount = dgDataDriven.Items.Count;
            btnDeleteRow.IsEnabled = itemsCount > 0 && dgDataDriven.SelectedCells.Count > 0;
            menuitemDeleteRow.IsEnabled = btnDeleteRow.IsEnabled;
            btnInsertRow.IsEnabled = itemsCount > 0 && dgDataDriven.SelectedCells.Count > 0;
            menuitemInsertRow.IsEnabled = btnInsertRow.IsEnabled;
            menuitemUpData.IsEnabled = btnUpData.IsEnabled;
            menuitemDownData.IsEnabled = btnDownData.IsEnabled;
            btnDeleteData.IsEnabled = false;
            btnRenameData.IsEnabled = false;
            btnLeftData.IsEnabled = false;
            btnRightData.IsEnabled = false;
        }

        //private void UpdateKeywordTab()
        //{
        //    txtCurrentInstance.Text = ((DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(dgDataDriven.CurrentCell.Item)).Header.ToString();
        //    bool updateAgain = false;
        //    updateAgain = dgTestSteps.SelectedIndex == dgDataDriven.CurrentCell.Column.DisplayIndex;
        //    dgTestSteps.SelectedIndex = dgDataDriven.CurrentCell.Column.DisplayIndex;
        //    // just double update
        //    if (updateAgain)
        //    {
        //        dgTestSteps_SelectionChanged(null, null);
        //    }
        //}

        //For Parameters Data Grid
        private void UpdateParameters(string mParameterValues)
        {
            //Boolean bNewStep = true;
            //List<DlkKeywordParameterRecord> parms = DlkKeywordHandler.GetControlKeywordParameterRecords()
            //if(String.IsNullOrEmpty(mParameters))
            //{

            //    bNewStep = false;
            //}
            //if(bNewStep)
            //{

            //}
        }

        //private void ClearForm()
        //{
        //    mTestStepRecords.Clear();
        //    txtCurrentInstance.Text = "1";
        //    txtTotalInstance.Text = "1";
        //    ClearStepPanel();

        //    //cmbExecuteStep.ClearValue();

        //}

        static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                T correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }

        private void UnselectOtherToggles(List<RadioButton> UnselectedToggles)
        {
            foreach (RadioButton tb in UnselectedToggles)
            {
                tb.IsChecked = false;
            }
        }


        public List<T> GetVisualChildCollection<T>(object parent) where T : Visual
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            try
            {
                //default values
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.InitialDirectory = Path.GetDirectoryName(this.Title.Replace(TEST_EDITOR_TITLE, ""));
                sfd.FileName = IsValidFileName(txtTestName.Text + "_data") ? txtTestName.Text + "_data.csv" : Path.GetFileName(this.Title).Replace(".xml", "_data.csv");
                sfd.Filter = "CSV Files (*.csv)|*.csv| Microsoft Exel Worksheet (*xlsx)|*.xlsx";
                sfd.AddExtension = true;
                sfd.DefaultExt = ".csv";

                if (DlkUserMessages.ShowQuestionYesNo(DlkUserMessages.ASK_UNSAVED_FILE_EXPORT, "Save Test?") == MessageBoxResult.Yes)
                {
                    if (System.IO.Path.GetFileName(mLoadedTest.mTestPath) == "template.dat")
                    {
                        SaveTestAs();
                    }
                    else
                    {
                        SaveTest();
                    }
                }
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fullPath = string.Empty;

                    switch (Path.GetExtension(sfd.FileName))
                    {
                        case ".csv":
                            fullPath = ExportToCSV(sfd.FileName);
                            break;
                        case ".xlsx":
                            fullPath = ExportToExcel(sfd.FileName);
                            break;
                        default:
                            DlkUserMessages.ShowError(DlkUserMessages.ERR_CSV_EXCEL_WRONG_EXTENSION, "Export Data");
                            break;
                    }
                    DlkUserMessages.ShowInfo(DlkUserMessages.INF_EXPORT_COMPLETED + fullPath, "Export Data");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private string ExportToCSV(string filePath)
        {
            /* Consider using LINQ for faster performance */
            string fileExtension = System.IO.Path.GetExtension(filePath);

            if (fileExtension != ".csv") // for files with no .csv extension
            {
                filePath += ".csv";
            }
            StreamWriter sw = new StreamWriter(filePath);
            string header = "";
            foreach (DlkDataRecord rec in mLoadedTest.Data.Records)
            {
                string encloser = rec.Name.Contains(",") || rec.Name.Contains("\"") ? "\"" : string.Empty; // enclose in double-quotes if header has commas/double-quotes
                header += encloser + rec.Name.Replace("\"", "\"\"") + encloser + ","; // duplicates double-quotes to escape string
            }
            sw.WriteLine(header.Trim(','));

            for (int i = 0; i < mLoadedTest.Data.Records.First().Values.Count; i++)
            {
                string newLine = "";
                for (int j = 0; j < mLoadedTest.Data.Records.Count; j++)
                {
                    /* enclose in double-quotes if field has commas/double-quotes */
                    string encloser = mLoadedTest.Data.Records[j].Values[i].Contains(",") || mLoadedTest.Data.Records[j].Values[i].Contains("\"") || mLoadedTest.Data.Records[j].Values[i].Contains("\n") ? "\"" : string.Empty;
                    newLine += encloser + mLoadedTest.Data.Records[j].Values[i].Replace("\"", "\"\"") + encloser; // duplicates double-quotes to escape string
                    if (j < mLoadedTest.Data.Records.Count - 1) //no comma if field is last - check needed to avoid excess columns
                        newLine += ",";
                }
                sw.WriteLine(newLine); // don't trim commas to accept empty values
            }
            sw.Close();
            return filePath;
        }

        //private string ExportToXML(string filePath)
        //{
        //    DataTable dt = new DataTable();
        //    dt = ConvertDataToDataTable();

        //    FileStream fs = new FileStream(filePath, FileMode.Create);
        //    XmlTextWriter xmlWriter = new XmlTextWriter(fs, Encoding.Unicode);

        //    //formatting
        //    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='utf-8'");
        //    xmlWriter.WriteProcessingInstruction("mso-application", "progid='Excel.Sheet'");

        //    dt.WriteXml(xmlWriter);

        //    return filePath;
        //}

        private string ExportToExcel(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension != ".xlsx") // for files with no .xlsx extension
            {
                filePath += ".xlsx";
            }

            DataTable dt = (dgDataDriven.ItemsSource as DataView).ToTable();
            return DlkExcelHelper.ExportToExcel(dt, filePath);
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.InitialDirectory = System.IO.Path.GetDirectoryName(this.Title.Replace(TEST_EDITOR_TITLE, ""));
                ofd.Filter = "CSV Files (*.csv)|*.csv| Microsoft Exel Worksheet (*xlsx)|*.xlsx";
                ofd.AddExtension = true;
                ofd.DefaultExt = ".csv";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string errorMessage;
                    switch (Path.GetExtension(ofd.FileName))
                    {
                        case ".csv":
                            errorMessage = ImportFromCSV(ofd.FileName);
                            break;
                        case ".xlsx":
                            errorMessage = ImportFromExcel(ofd.FileName);
                            break;
                        default:
                            errorMessage = DlkUserMessages.ERR_CSV_EXCEL_WRONG_FORMAT;
                            break;
                    }

                    if (errorMessage == String.Empty)
                        DlkUserMessages.ShowInfo(DlkUserMessages.INF_IMPORT_COMPLETED + ofd.FileName + DlkUserMessages.INF_SAVE_TO_PRESERVE, "Import Data");
                    else if (errorMessage == null) // returns null only if there are unescaped double-quotes
                    {
                        // Do nothing - message already displayed
                    }
                    else // for other errors
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_IMPORT_FILE_INVALID + errorMessage, "Import Data");
                }
            }
            catch (Exception ex)
            {
                // if import exception is unhandled
                DlkLogger.LogToFile(DlkUserMessages.ERR_IMPORT_INVALID, ex);
                DlkUserMessages.ShowError(DlkUserMessages.ERR_IMPORT_INVALID, "Import Data");
            }
        }

        private string ImportFromExcel(string filePath)
        {
            string errorMessage = String.Empty;
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension != ".xlsx") // additional error check
            {
                errorMessage = DlkUserMessages.ERR_CSV_EXCEL_WRONG_FORMAT;
            }
            else if (CurrentView == Views.Data || CurrentView == Views.Split)
            {
                DataTable dt = new DataTable();
                if (DlkExcelHelper.CheckIfExcelIsEmpty(filePath)) // early check if empty
                {
                    errorMessage = DlkUserMessages.ERR_EXCEL_EMPTY;
                }
                try
                {
                    dt = DlkExcelHelper.ImportFromExcel(filePath);
                }
                catch (DuplicateNameException) // if there are duplicate headers
                {
                    errorMessage = DlkUserMessages.ERR_EXCEL_DUPLICATE_HEADERS;
                }
                catch (ArgumentException) // if values exceed headers
                {
                    errorMessage = DlkUserMessages.ERR_EXCEL_HEADERS_INVALID;
                }
                catch (IOException) // if values exceed headers
                {
                    errorMessage = DlkUserMessages.ERR_EXCEL_FILE_IN_USE;
                }

                if (dt == null) // for 'unescaped double-quotes' error
                {
                    errorMessage = null;
                }

                if (errorMessage == "")
                {
                    ImportData(dt, out errorMessage);
                }
            }

            return errorMessage;
        }

        private string ImportFromCSV(string filePath)
        {
            string errorMessage = String.Empty;
            string fileExtension = Path.GetExtension(filePath);

            if (fileExtension != ".csv") // additional error check
            {
                errorMessage = DlkUserMessages.ERR_CSV_EXCEL_WRONG_FORMAT;
            }
            else if (CurrentView == Views.Data || CurrentView == Views.Split)
            {
                DataTable dt = new DataTable();
                List<string> lines = new List<string>(File.ReadLines(filePath));
                if (lines.Count() == 0 || lines.All(x => String.IsNullOrWhiteSpace(x))) // early check if empty
                {
                    errorMessage = DlkUserMessages.ERR_CSV_EMPTY;
                }
                try
                {
                    dt = DlkCSVHelper.CSVParse(filePath);
                }
                catch (DuplicateNameException) // if there are duplicate headers
                {
                    errorMessage = DlkUserMessages.ERR_CSV_DUPLICATE_HEADERS;
                }
                catch (ArgumentException) // if values exceed headers
                {
                    errorMessage = DlkUserMessages.ERR_CSV_HEADERS_INVALID;
                }

                if (errorMessage == "")
                {
                    ImportData(dt, out errorMessage);
                }
            }
            return errorMessage;
        }

        /// <summary>
        /// Import excel or csv file to TE data driven
        /// </summary>
        /// <param name="dt">Datatable that contains the parsed excel or csv file</param>
        /// <param name="errorMessage">Import error message</param>
        private void ImportData(DataTable dt, out string errorMessage)
        {
            if (ValidateDataColumnHeaders(dt, out List<DlkDataRecord> resDataRecords, out errorMessage))
            {
                if (errorMessage == null || (errorMessage != null && DlkUserMessages.ShowQuestionYesNoWarning(errorMessage) == MessageBoxResult.Yes))
                {
                    mLoadedTest.Data.Records = new List<DlkDataRecord>(resDataRecords);
                    errorMessage = "";
                }
                else
                {
                    errorMessage = null;
                }
            }

            if (errorMessage == "")
            {
                foreach (DataRow row in dt.Rows)
                {
                    for (int j = 0; j < row.ItemArray.Count(); j++)
                    {
                        mLoadedTest.Data.Records[j].Values.Add(row[j].ToString());
                    }
                }
                /* Populate grid */
                PopulateData(dt);

                /* Update UI states */
                btnNewRow.IsEnabled = dt.Columns.Count > 0;
                btnClearRow.IsEnabled = btnNewRow.IsEnabled;
                menuitemNewRow.IsEnabled = btnNewRow.IsEnabled;
            }
        }

        /// <summary>
        /// Validates datatable column headers
        /// </summary>
        /// <param name="dt">Imported data</param>
        /// <param name="resDataRecords">New List of data records</param>
        /// <param name="validationErrorMessage">Validation error message</param>
        /// <returns>True=Passed;False=Failed</returns>
        private bool ValidateDataColumnHeaders(DataTable dt,out List<DlkDataRecord> resDataRecords, out string validationErrorMessage)
        {
            resDataRecords = new List<DlkDataRecord>();
            validationErrorMessage = null;

            if (dt == null)
                return false;
            
            /* Regex definition: Allow underscore or space between AlphaNumeric words */
            Regex dataRegex = new Regex("^[a-zA-Z0-9]+(( +[a-zA-Z0-9]+|_[a-zA-Z0-9]+))*$", RegexOptions.Compiled);

            foreach (DataColumn col in dt.Columns)
            {
                string colName = col.ColumnName.Trim(' ','_');

                if (colName.Contains(" "))
                {
                    validationErrorMessage = DlkUserMessages.ASK_REPLACE_COLUMN_HEADERS;
                    colName = colName.Replace(" ", "");
                }

                if (!dataRegex.IsMatch(colName))
                {
                    validationErrorMessage = DlkUserMessages.ERR_DDT_SPECIAL_CHARACTERS;
                    return false;
                }

                /* Generate unique column name*/
                if (resDataRecords.SingleOrDefault(rec => rec.Name == colName) != null)
                {
                    do
                    {
                        string[] tempColNameArr = colName.Split('_');
                        if (tempColNameArr.Length > 1 && int.TryParse(tempColNameArr.Last(), out int counter))
                        {
                            tempColNameArr[tempColNameArr.Length - 1] = (counter + 1).ToString();
                            colName = string.Join("_", tempColNameArr);
                        }
                        else
                        {
                            colName += "_1";
                        }
                    } while (resDataRecords.SingleOrDefault(rec => rec.Name == colName) != null);
                }
                resDataRecords.Add(new DlkDataRecord(colName));
            }
            /* Replace datatable columnheaders */
            if (validationErrorMessage != null)
            {
                for (int i = dt.Columns.Count - 1; i >= 0; i--)
                {
                    dt.Columns[i].ColumnName = resDataRecords[i].Name;
                }
            }
            return true;
        }

        private void RefreshKeywords(bool clearKeyword = true)
        {
            try
            {
                if (Convert.ToString(cmbControl.SelectedValue) == "Password")
                {
                    if (mRestrictedKWs.Count == 0) //To populate list for Password control
                    {
                        mRestrictedKWs = RemoveRestricted();
                    }
                    cmbKeyword.DataContext = mRestrictedKWs;
                    if (clearKeyword)
                    {
                        cmbKeyword.Text = "";
                    }
                }
                else
                {
                    cmbKeyword.DataContext = mKeywords;
                    if (clearKeyword)
                    {
                        cmbKeyword.Text = "";
                    }
                    //txtParameters.Text = "";
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        private void LockScreenForEdit(bool IsLocked)
        {
            //txtTestName.IsEnabled = IsLocked;
            btnNewTest.IsEnabled = !IsLocked;
            btnSaveTest.IsEnabled = !IsLocked;
            btnSaveTestAs.IsEnabled = !IsLocked;
            //txtTestDescription.IsEnabled = IsLocked;
            btnRun.IsEnabled = !IsLocked;
            //btnTestStepNew.IsEnabled = IsLocked;
            //btnTestStepInsert.IsEnabled = IsLocked;
            //btnTestStepCopy.IsEnabled = IsLocked;
            //btnTestStepDelete.IsEnabled = IsLocked;
            //btnTestStepUp.IsEnabled = IsLocked;
            //btnTestStepDown.IsEnabled = IsLocked;

            //btnAddData.IsEnabled = IsLocked;
            //btnRenameData.IsEnabled = IsLocked;
            //btnDeleteData.IsEnabled = IsLocked;
            //btnNewRow.IsEnabled = IsLocked;
            //btnDeleteRow.IsEnabled = IsLocked;
        }

        private DataGridCell GetCell(DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    dgDataDriven.ScrollIntoView(row, dgDataDriven.Columns[column]);
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

        private void CopyDataCommand(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                DataGridCellInfo dgci = dgDataDriven.SelectedCells.First();
                DataGridRow dgr = (DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(dgci.Item);
                DataGridCell cell = GetCell(dgr, dgDataDriven.CurrentColumn.DisplayIndex);                
                Clipboard.SetText(((TextBox)cell.Content).Text);
            }
            catch
            {
                //
            }

        }

        private void PasteDataCommand(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                DataGridCellInfo dgci = dgDataDriven.SelectedCells.First();
                DataGridRow dgr = (DataGridRow)dgDataDriven.ItemContainerGenerator.ContainerFromItem(dgci.Item);
                int rowIndex = dgr.GetIndex();
                DataGridCell cell = GetCell(dgr, dgDataDriven.CurrentColumn.DisplayIndex);
                int colIndex = cell.Column.DisplayIndex;
                mLoadedTest.Data.Records[rowIndex].Values[colIndex] = Clipboard.GetText();
            }
            catch
            {
                //
            }
        }

        private int GetColumnIndex() // Added in SelectedcellsChanged for move left/right - index was -1 without this function
        {
            int ColumnIndex = 0;
            foreach (DataGridColumn dgc in dgDataDriven.Columns)
            {
                if (dgc.Header.ToString() == mCurrentDataColumnHeader.Content.ToString())
                {
                    ColumnIndex = dgc.DisplayIndex;
                    break;
                }
            }
            return ColumnIndex;
        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void ColorParams()
        {
            string executeVal = "";
            bool bInitPass = true;
            int ctr = 0;
            const int INT_EXECUTE_COL = 2;
            const int INT_DATAGRID_COL_HEADERS = 7;

            try
            {
                if (mTestStepRecords.Count > 0)
                {
                    foreach (TextBlock block in FindVisualChildren<TextBlock>(dgTestSteps)) // look for txtParams child control
                    {
                        ctr++;
                        // This will skip the initial 7 blocks as these are the datagrid column headers.
                        if (bInitPass)
                        {
                            if (ctr == INT_DATAGRID_COL_HEADERS)
                            {
                                ctr = 0;
                                bInitPass = false;
                            }
                            continue;
                        }
                        // To retrieve the value of the second column - Execute column
                        if (ctr == INT_EXECUTE_COL)
                        {
                            executeVal = block.Text;
                            if (DlkData.IsDataDrivenParam(executeVal))
                            {
                                block.Inlines.Clear();
                                block.Inlines.Add(new Run(executeVal) { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
                            }
                        }
                        else if (ctr == INT_DATAGRID_COL_HEADERS)
                        {
                            ctr = 0; // To reset the counter
                        }

                        if (block.Name == "txtParams")
                        {
                            if (executeVal.ToLower() != "false")
                            {
                                string[] paramFields = block.Text.Split('|');
                                block.Inlines.Clear();
                                int fieldCount = 1; // counter for number of parameters
                                foreach (string param in paramFields)
                                {
                                    if (DlkData.IsDataDrivenParam(param)) // data-driven fields
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
                                    }
                                    else if (DlkData.IsOutPutVariableParam(param)) // output fields
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.Blue, FontWeight = FontWeights.Bold });
                                    }
                                    else if (DlkData.IsGlobalVarParam(param)) // global variable fields
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.DarkViolet, FontWeight = FontWeights.Bold });
                                    }
                                    else
                                    {
                                        block.Inlines.Add(new Run(param));
                                    }
                                    if (fieldCount < paramFields.Length)
                                    {
                                        block.Inlines.Add(new Run("|") { Foreground = Brushes.Black });
                                    }
                                    fieldCount++;
                                }
                            }
                            else // For rows that contain D{, G{ and O{ but have been set to False (execute)
                            {
                                string paramVal = block.Text;
                                block.Inlines.Clear();
                                block.Inlines.Add(new Run(paramVal) { Foreground = Brushes.Gray, FontWeight = FontWeights.Normal, FontStyle = FontStyles.Italic });
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


        private void FillHelpBoxInfo(bool mReChecked = false)
        {
            txtHelp.Inlines.Clear();
            if (m_Help.Count > 0 && cmbKeyword.SelectedValue.ToString() != String.Empty)
            {
                string helpKey = cmbKeyword.SelectedValue.ToString();
                List<DlkKeywordHelpBoxRecord> keywordHelp = m_Help.FindAll(f => f.mKeyword == helpKey);
                DlkKeywordHelpBoxRecord mKeywordHelp;

                if (keywordHelp.Count == 1 && keywordHelp.SingleOrDefault(s => s.mControlType == "" || s.mControlType == CurrentSelectedControlType) != null)
                    mKeywordHelp = keywordHelp.Single();
                else
                    mKeywordHelp = keywordHelp.FirstOrDefault(f => f.mControlType == CurrentSelectedControlType || (f.mControlType == "Function" && CurrentSelectedControlType == "" && screenName == "Function"));

                if (mKeywordHelp != null)
                {
                    txtHelp.Inlines.Add(new Run(mKeywordHelp.mKeyword + Environment.NewLine) { FontWeight = FontWeights.Bold });
                    txtHelp.Inlines.Add(new Run(mKeywordHelp.mInformation + Environment.NewLine + Environment.NewLine));
                    txtHelp.Inlines.Add(new Run("Parameters:" + Environment.NewLine) { FontWeight = FontWeights.Bold });
                    txtHelp.Inlines.Add(new Run(mKeywordHelp.mParameters + Environment.NewLine + Environment.NewLine));
                    txtHelp.Inlines.Add(new Run("Used in:" + Environment.NewLine) { FontWeight = FontWeights.Bold });
                    txtHelp.Inlines.Add(new Run(mKeywordHelp.mControls + Environment.NewLine + Environment.NewLine + Environment.NewLine) { FontStyle = FontStyles.Italic });
                }
                else
                {
                    if (File.Exists(DlkEnvironment.mHelpConfigFile) & !mReChecked)
                    {
                        m_Help.Clear();
                        m_Help = new DlkKeywordHelpBoxHandler(true).mKeywordHelpBoxRecords.ToList<DlkKeywordHelpBoxRecord>();
                        FillHelpBoxInfo(true);
                    }
                    else
                    {
                        txtHelp.Inlines.Clear();
                        txtHelp.Inlines.Add(new Run("No information found for the keyword"));
                    }
                }
            }
            else
            {
                txtHelp.Inlines.Add(new Run("Information for keywords will be displayed here"));
            }

            m_Help.Clear();
            m_Help = new DlkKeywordHelpBoxHandler().mKeywordHelpBoxRecords.ToList<DlkKeywordHelpBoxRecord>();
        }

        //private void txtControl_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        lbControls.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception ex)
        //    {
        //        DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
        //    }
        //}

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
        /// Property notifier for all propertychanged listeners
        /// </summary>
        /// <param name="propertyName">Target property</param>
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Save output logs to file
        /// </summary>
        /// <param name="IsTemporary">Optional parameter to indicate if saving is temporary</param>
        /// <returns>File path of saved file</returns>
        private string SaveOutputLogs(bool IsTemporary = false)
        {
            if (!IsTemporary)
            {
                System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
                sfd.InitialDirectory = DlkEnvironment.mDirUserData;
                sfd.FileName = "log_" + DlkString.GetDateAsText(DateTime.Now, "file") + ".txt";
                sfd.Filter = "Text Files (*.txt)|*.txt";
                sfd.AddExtension = true;
                sfd.DefaultExt = ".txt";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, Output);
                    return sfd.FileName;
                }
            }
            else
            {
                string path = Path.Combine(DlkEnvironment.mDirUserData, "log_" 
                    + DlkString.GetDateAsText(DateTime.Now, "file") + ".txt");

                File.WriteAllText(path, Output);
                return path;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the Test Results Summary part from Output log
        /// </summary>
        /// <param name="FullOutputLogs">Full content of Output log</param>
        /// <returns>Test Results summary string</returns>
        private string GetOutputSummary(string FullOutputLogs)
        {
            string ret = string.Empty;
            string resultsHeader = DlkLogger.ConvertToHeader("TEST RESULTS");
            if (FullOutputLogs.Contains(resultsHeader))
            {
                ret = FullOutputLogs.Substring(FullOutputLogs.IndexOf(resultsHeader));
            }
            return ret;
        }

        /// <summary>
        /// Updates Pause button icon, tooltip and label
        /// </summary>
        /// <param name="IsPaused">Paused/resumed state</param>
        private void UpdatePauseResumeIcon(bool IsPaused)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://siteoforigin:,,,/" + (IsPaused ? PATH_ICON_RESUME : PATH_ICON_PAUSE), UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            executionImage.Source = bitmapImage;
            btnPauseTest.ToolTip = (IsPaused ? TEXT_LABEL_RESUME : TEXT_LABEL_PAUSE);
            executionLabel.Text = (IsPaused ? TEXT_LABEL_RESUME : TEXT_LABEL_PAUSE);
        }

        /// <summary>
        /// Populates current grid filter
        /// </summary>
        /// <param name="filterType">Filter type based on column</param>
        private void PopulateFilter(FilterColumn filterType)
        {
            List<string> elementList = new List<string>();
            CheckedListItems = new ObservableCollection<CheckedListItem<string>>();
            CheckedListItems.Add(new CheckedListItem<string>(FILTER_SELECT_ALL, true, FILTER_SELECT_ALL));
            List<DlkTestStepRecord> gridSteps = dgTestSteps.Items.Cast<DlkTestStepRecord>().ToList();
            switch (filterType)
            {
                case FilterColumn.Execute:
                    elementList = SetListContent(FilterColumn.Execute, _mExecuteFilterCheckBoxes, gridSteps);
                    break;
                case FilterColumn.Screen:
                    elementList = SetListContent(FilterColumn.Screen, _mScreenFilterCheckBoxes, gridSteps);
                    break;
                case FilterColumn.Control:
                    elementList = SetListContent(FilterColumn.Control, _mControlFilterCheckBoxes, gridSteps);
                    break;
                case FilterColumn.Keyword:
                    elementList = SetListContent(FilterColumn.Keyword, _mKeywordFilterCheckBoxes, gridSteps);
                    break;
                case FilterColumn.Parameters:
                    elementList = SetListContent(FilterColumn.Parameters, _mParametersFilterCheckBoxes, gridSteps);
                    break;
                case FilterColumn.Delay:
                    elementList = SetListContent(FilterColumn.Delay, _mDelayFilterCheckBoxes, gridSteps);
                    break;
            }
            if (elementList.Any())
            {
                elementList.Sort();
                if (elementList.Any(x => String.IsNullOrWhiteSpace(x)))
                {
                    elementList.RemoveAll(x => String.IsNullOrWhiteSpace(x));
                    elementList.Add(FILTER_BLANK);
                }
                foreach (string elementName in elementList)
                {
                    CheckedListItems.Add(new CheckedListItem<string>(elementName));
                }
            }
            DataContext = this;
        }

        /// <summary>
        /// Applies filter options
        /// </summary>
        /// <param name="editedRecords">Edited steps which must remain in grid after filter</param>
        private void ApplyFilterToGrid(List<DlkTestStepRecord> editedRecords = null)
        {
            List<string> elementsToFilter = new List<string>();
            List<DlkTestStepRecord> gridSteps = mLoadedTest.mTestSteps;
            List<DlkTestStepRecord> filteredSteps = new List<DlkTestStepRecord>();
            for (int filterIndex = 1; filterIndex <= gridFiltersActive; filterIndex++)
            {
                m_NewestFilterColumn = filterOrder.FirstOrDefault(x => x.Value == filterIndex).Key;
                List<DlkTestStepRecord> blankSteps = new List<DlkTestStepRecord>();
                switch (m_NewestFilterColumn)
                {
                    case FilterColumn.Execute:
                        CheckedListItems = _mExecuteFilterCheckBoxes;
                        elementsToFilter = CheckedListItems.Where(x => x.IsChecked).Select(y => y.Item).ToList();
                        gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mExecute, StringComparer.CurrentCultureIgnoreCase)).ToList();
                        break;
                    case FilterColumn.Screen:
                        CheckedListItems = _mScreenFilterCheckBoxes;
                        elementsToFilter = CheckedListItems.Where(x => x.IsChecked).Select(y => y.Item).ToList();
                        gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mScreen, StringComparer.CurrentCultureIgnoreCase)).ToList();
                        break;
                    case FilterColumn.Control:
                        CheckedListItems = _mControlFilterCheckBoxes;
                        if (CheckedListItems.Any(x => x.Item == FILTER_BLANK && x.IsChecked))
                        {
                            blankSteps = gridSteps.Where(x => String.IsNullOrWhiteSpace(x.mControl)).ToList();
                        }
                        elementsToFilter = CheckedListItems.Where(x => x.IsChecked).Select(y => y.Item).ToList();
                        gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mControl, StringComparer.CurrentCultureIgnoreCase)).ToList();
                        gridSteps = blankSteps.Any() ? gridSteps.Concat(blankSteps).OrderBy(x => x.mStepNumber).ToList() : gridSteps;
                        break;
                    case FilterColumn.Keyword:
                        CheckedListItems = _mKeywordFilterCheckBoxes;
                        elementsToFilter = CheckedListItems.Where(x => x.IsChecked).Select(y => y.Item).ToList();
                        gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mKeyword, StringComparer.CurrentCultureIgnoreCase)).ToList();
                        break;
                    case FilterColumn.Parameters:
                        CheckedListItems = _mParametersFilterCheckBoxes;
                        if (CheckedListItems.Any(x => string.Equals(x.Item, FILTER_BLANK, StringComparison.CurrentCultureIgnoreCase) && x.IsChecked))
                        {
                            List<CheckedListItem<string>> listBlankItems = CheckedListItems.Where(x => string.Equals(x.Item, FILTER_BLANK, StringComparison.CurrentCultureIgnoreCase)).ToList();
                            if (listBlankItems.All(x => x.IsChecked)) // both are checked
                            {
                                blankSteps = gridSteps.Where(x => String.IsNullOrWhiteSpace(x.mParameterString)).ToList();
                            }
                            else if (listBlankItems.Last().IsChecked) // only last is checked - show only empty/whitespace values
                            {
                                blankSteps = gridSteps.Where(x => String.IsNullOrWhiteSpace(x.mParameterString)).ToList();
                                elementsToFilter = CheckedListItems.Where(x => x.IsChecked && x.Item != FILTER_BLANK).Select(y => y.Item).ToList();
                                gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mParameterString)).ToList();
                                gridSteps = blankSteps.Any() ? gridSteps.Concat(blankSteps).OrderBy(x => x.mStepNumber).ToList() : gridSteps;
                                break;
                            }
                        }
                        elementsToFilter = CheckedListItems.Where(x => x.IsChecked).Select(y => y.Item).ToList();
                        gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mParameterString, StringComparer.CurrentCultureIgnoreCase)).ToList();
                        gridSteps = blankSteps.Any() ? gridSteps.Concat(blankSteps).OrderBy(x => x.mStepNumber).ToList() : gridSteps;
                        break;
                    case FilterColumn.Delay:
                        CheckedListItems = _mDelayFilterCheckBoxes;
                        elementsToFilter = CheckedListItems.Where(x => x.IsChecked).Select(y => y.Item).ToList();
                        gridSteps = gridSteps.Where(x => elementsToFilter.Contains(x.mStepDelay.ToString(), StringComparer.CurrentCultureIgnoreCase)).ToList();
                        break;
                }
            }
            if (editedRecords != null)
            {
                foreach (DlkTestStepRecord record in editedRecords)
                {
                    if (!gridSteps.Any(x => x.mStepNumber == record.mStepNumber))
                    {
                        gridSteps.Add(record);
                    }
                }
                gridSteps = gridSteps.OrderBy(x => x.mStepNumber).ToList();
            }
            ClearTestStepsGrid();
            if (gridSteps.Any())
            {
                foreach (DlkTestStepRecord tsr in gridSteps)
                {
                    mTestStepRecords.Add(tsr);
                }
            }
            else
            {
                RemoveAllFilters();
            }
            dgTestSteps.Items.Refresh();
        }

        /// <summary>
        /// Updates active filters' content
        /// </summary>
        /// <param name="step">Edited step to add to existing filters</param>
        private void UpdateFilterContent(DlkTestStepRecord step)
        {
            if (gridFiltersActive > 0)
            {
                List<DlkTestStepRecord> gridSteps = dgTestSteps.Items.Cast<DlkTestStepRecord>().ToList();
                FilterColumn columnToModify = filterOrder.First(x => x.Value == gridFiltersActive).Key;
                switch (columnToModify)
                {
                    case FilterColumn.Execute:
                        if (!_mExecuteFilterCheckBoxes.Any(x => string.Equals(x.Item, step.mExecute, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _mExecuteFilterCheckBoxes = AddFilterItem(step.mExecute, _mExecuteFilterCheckBoxes);
                        }
                        break;
                    case FilterColumn.Screen:
                        if (!_mScreenFilterCheckBoxes.Any(x => string.Equals(x.Item, step.mScreen, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _mScreenFilterCheckBoxes = AddFilterItem(step.mScreen, _mScreenFilterCheckBoxes);
                        }
                        break;
                    case FilterColumn.Control:
                        string stepControl = step.mControl;
                        if (String.IsNullOrWhiteSpace(stepControl))
                        {
                            stepControl = FILTER_BLANK;
                        }
                        if (!_mControlFilterCheckBoxes.Any(x => string.Equals(x.Item, stepControl, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _mControlFilterCheckBoxes = AddFilterItem(step.mControl, _mControlFilterCheckBoxes);
                        }
                        break;
                    case FilterColumn.Keyword:
                        if (!_mKeywordFilterCheckBoxes.Any(x => string.Equals(x.Item, step.mKeyword, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _mKeywordFilterCheckBoxes = AddFilterItem(step.mKeyword, _mKeywordFilterCheckBoxes);
                        }
                        break;
                    case FilterColumn.Parameters:
                        bool? isReallyBlank = null;
                        if (step.mParameterString == FILTER_BLANK)
                        {
                            isReallyBlank = false;
                        }
                        else if (String.IsNullOrWhiteSpace(step.mParameterString))
                        {
                            isReallyBlank = true;
                        }
                        if (isReallyBlank != null)
                        {
                            List<CheckedListItem<string>> listBlankItems = CheckedListItems.Where(x => string.Equals(x.Item, FILTER_BLANK, StringComparison.CurrentCultureIgnoreCase)).ToList();
                            if (listBlankItems.Count == 1)
                            {
                                CheckedListItem<string> itemToAdd = new CheckedListItem<string>(FILTER_BLANK, false);
                                CheckedListItem<string> selectAllItem = new CheckedListItem<string>(FILTER_SELECT_ALL, false);
                                if (mLoadedTest.mTestSteps.Any(x => string.Equals(x.mParameterString, FILTER_BLANK, StringComparison.CurrentCultureIgnoreCase)) && isReallyBlank == true)
                                {
                                    _mParametersFilterCheckBoxes.Add(itemToAdd);
                                    _mParametersFilterCheckBoxes.Insert(0, selectAllItem);
                                }
                                else if (mLoadedTest.mTestSteps.Any(x => String.IsNullOrWhiteSpace(x.mParameterString)) && isReallyBlank == false)
                                {
                                    CheckedListItem<string> realBlankOption = _mParametersFilterCheckBoxes.Last();
                                    _mParametersFilterCheckBoxes.RemoveAt(_mParametersFilterCheckBoxes.Count - 1);
                                    _mParametersFilterCheckBoxes.Add(itemToAdd);
                                    _mParametersFilterCheckBoxes = new ObservableCollection<CheckedListItem<string>>(_mParametersFilterCheckBoxes.Skip(1).OrderBy(x => x.Item));
                                    _mParametersFilterCheckBoxes.Insert(0, selectAllItem);
                                    _mParametersFilterCheckBoxes.Add(realBlankOption);
                                }
                            }
                            break;
                        }
                        else if (string.Equals(step.mParameterString, FILTER_SELECT_ALL, StringComparison.CurrentCultureIgnoreCase) && CheckedListItems.Where(x => string.Equals(x.Item, FILTER_SELECT_ALL, StringComparison.CurrentCultureIgnoreCase)).Count() == 1)
                        {
                            _mParametersFilterCheckBoxes = AddFilterItem(FILTER_SELECT_ALL, _mParametersFilterCheckBoxes);
                            break;
                        }
                        if (!_mParametersFilterCheckBoxes.Any(x => string.Equals(x.Item, step.mParameterString, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _mParametersFilterCheckBoxes = AddFilterItem(step.mParameterString, _mParametersFilterCheckBoxes);
                        }
                        if (_mParametersFilterCheckBoxes.Where(x => string.Equals(x.Item, FILTER_BLANK, StringComparison.CurrentCultureIgnoreCase)).Count() > 1)
                        {
                            CheckedListItem<string> realBlankOption = _mParametersFilterCheckBoxes.Where(x => x.Item == FILTER_BLANK).Last();
                            _mParametersFilterCheckBoxes.Remove(realBlankOption);
                            _mParametersFilterCheckBoxes.Add(realBlankOption);
                        }
                        break;
                    case FilterColumn.Delay:
                        string delayString = step.mStepDelay.ToString();
                        if (!_mDelayFilterCheckBoxes.Any(x => string.Equals(x.Item, delayString, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _mDelayFilterCheckBoxes = AddFilterItem(delayString, _mDelayFilterCheckBoxes);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Reapplies filters after updating step/s
        /// </summary>
        private void ReapplyFilter()
        {
            foreach (object selectedItem in dgTestSteps.SelectedItems)
            {
                int stepNumber = ((DlkTestStepRecord)selectedItem).mStepNumber - 1;
                DlkTestStepRecord currStep = mLoadedTest.mTestSteps[stepNumber];
                editedRrecords.Add(currStep);
            }
            PopulateTestSteps();
            ApplyFilterToGrid(editedRrecords);
        }

        /// <summary>
        /// Populate filter list based on clicked column button
        /// </summary>
        /// <param name="filterType">Filter type based on column</param>
        private void SetFilterType(FilterColumn filterType)
        {
            m_TempFilter = filterType;
            txtColumnName.Text = m_TempFilter.ToString();
            popupFilter.IsOpen = true;
            PopulateFilter(m_TempFilter);
            btnFilterOK.IsEnabled = CheckedListItems.Any(item => item.IsChecked);
        }

        /// <summary>
        /// Returns a list of unique strings based on column type
        /// </summary>
        /// <param name="filterType">Filter type based on column</param>
        /// <param name="checkBoxContent">Previous content of filter</param>
        /// <param name="stepsToFilter">Steps in grid to filter</param>
        /// <returns>List of names to be selected as filter/s</returns>
        private List<string> SetListContent(FilterColumn filterType, ObservableCollection<CheckedListItem<string>> checkBoxContent, List<DlkTestStepRecord> stepsToFilter)
        {
            List<string> checkBoxList = new List<string>();
            if (filterOrder[filterType] > 0 && m_NewestFilterColumn == filterType)
            {
                CheckedListItems = checkBoxContent;
            }
            else
            {
                switch (filterType)
                {
                    case FilterColumn.Execute:
                        checkBoxList = stepsToFilter.Select(x => x.mExecute).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
                        break;
                    case FilterColumn.Screen:
                        checkBoxList = stepsToFilter.Select(x => x.mScreen).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
                        break;
                    case FilterColumn.Control:
                        checkBoxList = stepsToFilter.Select(x => x.mControl).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
                        break;
                    case FilterColumn.Keyword:
                        checkBoxList = stepsToFilter.Select(x => x.mKeyword).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
                        break;
                    case FilterColumn.Parameters:
                        checkBoxList = stepsToFilter.Select(x => x.mParameterString).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
                        break;
                    case FilterColumn.Delay:
                        checkBoxList = stepsToFilter.Select(x => x.mStepDelay.ToString()).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
                        break;
                }
                
            }
            return checkBoxList;
        }

        /// <summary>
        /// Adds filter item based on filter type
        /// </summary>
        /// <param name="listItemName">Name of list item in popup</param>
        /// <param name="listItems">List of items to be modified</param>
        private ObservableCollection<CheckedListItem<string>> AddFilterItem(string listItemName, ObservableCollection<CheckedListItem<string>> listItems)
        {
            CheckedListItem<string> itemToAdd = new CheckedListItem<string>(listItemName, false);
            CheckedListItem<string> selectAllItem = new CheckedListItem<string>(FILTER_SELECT_ALL, false);
            listItems.Add(itemToAdd);
            listItems = new ObservableCollection<CheckedListItem<string>>(listItems.Skip(1).OrderBy(x => x.Item));
            listItems.Insert(0, selectAllItem);
            return listItems;
        }

        /// <summary>
        /// Remove all filters and filter data
        /// </summary>
        private void RemoveAllFilters()
        {
            filterOrder[m_NewestFilterColumn] = 0;
            foreach (FilterColumn key in filterOrder.Keys.ToList())
            {
                filterOrder[key] = 0;
            }
            gridFiltersActive = 0;
            PopulateTestSteps();
            m_NewestFilterColumn = FilterColumn.None;
        }


        /// <summary>
        /// This automatically updates invalid column names specifically those with trailing spaces in the data column names.
        /// This is to prevent TR from crashing and changes will be persisted upon saving of the test.
        /// </summary>
        private void UpdateInvalidDataColumns()
        {
            for (int i = 0; i < mLoadedTest.Data.Records.Count; i++)
            {
                if (mLoadedTest.Data.Records[i].Name.EndsWith(" "))
                {
                    mLoadedTest.Data.Records[i].Name = mLoadedTest.Data.Records[i].Name.Trim();
                }
            }
        }

        /// <summary>
        /// Removes the restricted keywords for the Password control (Login & Database screen)
        /// </summary>
        /// <returns>list of available keywords</returns>
        private List<string> RemoveRestricted()
        {
            mRestrictedKWs = mKeywords;
            foreach (string kw in RestrictedKeywords)
            {
                if (mRestrictedKWs.Contains(kw))
                {
                    mRestrictedKWs.Remove(kw);
                }
            }
            return mRestrictedKWs;
        }

        /// <summary>
        /// Sets the value of true or false to correct casing for Execute
        /// </summary>
        /// <returns>"True"/"False"</returns>
        private String SetExecuteStepCasing(String input)
        {
            string output = "";

            if (input.ToLower().Equals("true"))
            {
                return "True";
            }
            if (input.ToLower().Equals("false"))
            {
                return "False";
            }

            return output;
        }

        /// <summary>
        /// Enables/disables Copy/Paste Row states depending on selected cells and clipboard content
        /// </summary>
        private void SetCopyPasteRowButtonStates()
        {
            try
            {
                if (dgDataDriven.SelectedCells.Count > 0)
                {
                    if (!CheckClipboardEmpty("DataDrivenCollection"))
                    {
                        btnPasteRow.IsEnabled = true;
                        menuitemPasteRow.IsEnabled = true;
                    }
                    btnCopyRow.IsEnabled = true;
                    menuitemCopyRow.IsEnabled = true;

                }
                else
                {
                    btnPasteRow.IsEnabled = false;
                    btnCopyRow.IsEnabled = false;
                    menuitemCopyRow.IsEnabled = false;
                    menuitemPasteRow.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }

        private void SetTestStepUpdateButtonState()
        {
            if (chkBoxExecuteStep.IsChecked == false & chkBoxStepDelay.IsChecked == false)
                btnTestStepUpdate.IsEnabled = false;
            else
                btnTestStepUpdate.IsEnabled = true;
        }

        /// <summary>
        /// Updates the recent results path and state of Last Results button
        /// </summary>
        private void UpdateRecentResults(string testresultpath)
        {
            mRecentResultPath = testresultpath;
            btnLastResults.IsEnabled = !String.IsNullOrEmpty(mRecentResultPath) ? true : false;            
        }
        #endregion
        
        #region Test Methods
        private void NewTest()
        {
            // clear everything
            CurrentInstance = 1;
            LoadTest(System.IO.Path.Combine(DlkEnvironment.mDirTests, "template.dat"));
            dgDataDriven.ItemsSource = null;
            dgDataDriven.Items.Refresh();
            string fileName = GetDefaultTestName();
            Title = TEST_EDITOR_TITLE + "*" + System.IO.Path.Combine(_owner.TestRoot, fileName);
            txtTestName.Text = System.IO.Path.GetFileNameWithoutExtension(fileName);
            txtTestName.SelectAll();
            IsTemplateLoaded = true;
            
            /* Set output logs data context to default */
            txtOutput.DataContext = this;
            Output = string.Empty;
            UpdateRecentResults("");
        }

        private string GetDefaultTestName()
        {
            int windowCount = 0;
            string currentName = "NewTest";

            bool teWindowExists(string scriptPath) => File.Exists(scriptPath.Replace("*","")) || DlkEditorWindowHandler.IsEditorScriptOpened(scriptPath, false);

            while (teWindowExists(Path.Combine("*" + DlkEnvironment.mDirTests, $"{currentName}{(windowCount == 0 ? "" : windowCount.ToString())}") + ".xml"))
                windowCount++;

            return $"{currentName}{(windowCount == 0 ? "" : windowCount.ToString())}.xml";
        }

        /// <summary>
        /// Get local last update of info of Keyword file (.xml) and Data file (.trd)
        /// </summary>
        /// <param name="keywordPath">Absolute path of Keyword file (.xml)</param>
        /// <param name="dataPath">Absolute path of Data file (.trd)</param>
        /// <param name="keywordFileDate">Output container for result Keyword file last modified date</param>
        /// <param name="dataFileDate">Output container for result Data file last modified date</param>
        /// <param name="keywordFileUser">Output container for result Keyword file last modified by user</param>
        /// <param name="dataFileUser">Output container for result Data file last modified by user</param>
        private void GetLocalTestLastUpdateInfo(string keywordPath, string dataPath, 
            out string keywordFileDate, out string dataFileDate, out string keywordFileUser, out string dataFileUser)
        {
            keywordFileDate = string.Empty;
            dataFileDate = string.Empty;
            keywordFileUser = string.Empty;
            dataFileUser = string.Empty;

            if (IsTemplateLoaded) /* Do nothing if template.dat is loaded */
            {
                return;
            }

            if (!string.IsNullOrEmpty(keywordPath)) /* Keyword */
            {
                FileInfo kFi = new FileInfo(keywordPath);
                keywordFileDate = kFi.LastWriteTime.ToShortDateString();
            }

            if (!string.IsNullOrEmpty(dataPath)) /* Data */
            {
                FileInfo dFi = new FileInfo(dataPath);
                dataFileDate = dFi.LastWriteTime.ToShortDateString();
            }
        }

        /// <summary>
        /// Get server (TFS) last update of info of Keyword file (.xml) and Data file (.trd)
        /// </summary>
        /// <param name="keywordPath">Absolute path of Keyword file (.xml)</param>
        /// <param name="dataPath">Absolute path of Data file (.trd)</param>
        /// <param name="keywordFileDate">Output container for result Keyword file last modified date</param>
        /// <param name="dataFileDate">Output container for result Data file last modified date</param>
        /// <param name="keywordFileUser">Output container for result Keyword file last modified by user</param>
        /// <param name="dataFileUser">Output container for result Data file last modified by user</param>
        private void GetServerTestLastUpdateInfo(string keywordPath, string dataPath, out string keywordFileDate, out string dataFileDate, out string keywordFileUser, out string dataFileUser)
        {
            keywordFileDate =  string.Empty;
            dataFileDate = string.Empty;
            keywordFileUser = string.Empty;
            dataFileUser = string.Empty;

            string resultFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                DlkSourceControlHandler.FILE_SRC_CONTROL_LOG);

            /* keyword */
            DlkSourceControlHandler.History(keywordPath, "/stopafter:1");
            
            if (File.Exists(resultFile))
            {
                string[] lns = File.ReadAllLines(resultFile);
                if (lns.Count() == LAST_UPDATE_RESFILE_EXACT_LINE_COUNT)
                {
                    keywordFileUser = lns[LAST_UPDATE_RESFILE_TARGET_LINE].Split(new[] { "  " }, /* delimeter: double space */
                        StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[LAST_UPDATE_RESFILE_USER_IDX];
                    if (keywordFileUser.Contains("..."))
                    {
                        string temp = keywordFileUser;
                        keywordFileUser = keywordFileUser.Substring(0, keywordFileUser.IndexOf("..."));
                        keywordFileDate = temp.Substring(temp.IndexOf("...")).Replace("...", string.Empty).Trim();
                    }
                    else
                    {
                        keywordFileDate = lns[LAST_UPDATE_RESFILE_TARGET_LINE].Split(new[] { "  " }, /* delimeter: double space */
                            StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[LAST_UPDATE_RESFILE_DATE_IDX];
                    }
                }
            }

            /* data */
            DlkSourceControlHandler.History(dataPath, "/stopafter:1");

            if (File.Exists(resultFile))
            {
                string[] lns = File.ReadAllLines(resultFile);
                if (lns.Count() == LAST_UPDATE_RESFILE_EXACT_LINE_COUNT)
                {
                    dataFileUser = lns[LAST_UPDATE_RESFILE_TARGET_LINE].Split(new[] { "  " }, /* delimeter: double space */
                        StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[LAST_UPDATE_RESFILE_USER_IDX];
                    if (dataFileUser.Contains("..."))
                    {
                        string temp = dataFileUser;
                        dataFileUser = dataFileUser.Substring(0, dataFileUser.IndexOf("..."));
                        dataFileDate = temp.Substring(temp.IndexOf("...")).Replace("...", string.Empty).Trim();
                    }
                    else
                    {
                        dataFileDate = lns[LAST_UPDATE_RESFILE_TARGET_LINE].Split(new[] { "  " }, /* delimeter: double space */
                            StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList()[LAST_UPDATE_RESFILE_DATE_IDX];
                    }
                }
            }
        }
#endregion

#region Step Methods
        private void AddStep(DlkTestStepRecord src = null)
        {
            DlkTestStepRecord newStep = new DlkTestStepRecord();
            if (src == null) // add new
            {
                newStep.mStepNumber = mTestStepRecords.Count + 1;
                newStep.mExecute = bool.TrueString;
                newStep.mScreen = "";
                newStep.mControl = "";
                newStep.mKeyword = "";
                newStep.mParameters = new List<string>();
                newStep.mStepDelay = 0;
                newStep.mStepStatus = "Not run";
                newStep.mStepLogMessages = new List<DlkLoggerRecord>();
                newStep.mStepStart = new DateTime();
                newStep.mStepEnd = new DateTime();
                newStep.mStepElapsedTime = "";
                newStep.mParameters = new List<string>();
                for (int idx = 0; idx < mLoadedTest.mInstanceCount; idx++)
                {
                    newStep.mParameters.Add("");
                }
                mLoadedTest.mTestSteps.Add(newStep);
            }
            else // copy existing
            {
                //newStep.mStepNumber = mTestStepRecords.Count + 1;
                newStep.mStepNumber = src.mStepNumber + 1;
                newStep.mExecute = src.mExecute;
                newStep.mScreen = src.mScreen;
                newStep.mControl = src.mControl;
                newStep.mKeyword = src.mKeyword;
                newStep.mParameters = new List<string>(src.mParameters);
                newStep.mStepDelay = src.mStepDelay;
                newStep.mStepStatus = src.mStepStatus;
                newStep.mStepLogMessages = new List<DlkLoggerRecord>(src.mStepLogMessages);
                newStep.mStepStart = src.mStepStart;
                newStep.mStepEnd = src.mStepEnd;
                newStep.mStepElapsedTime = src.mStepElapsedTime;
                mLoadedTest.mTestSteps.Insert(newStep.mStepNumber - 1, newStep);
                // renumber all succeeding steps
                for (int idx = newStep.mStepNumber; idx < mLoadedTest.mTestSteps.Count; idx++)
                {
                    mLoadedTest.mTestSteps[idx].mStepNumber++;
                }
            }
            isChanged = true;
        }

        private void InsertStep(DlkTestStepRecord src = null)
        {
            isChanged = true;
            DlkTestStepRecord newStep = new DlkTestStepRecord();
            //newStep.mStepNumber = mTestStepRecords.Count + 1;
            newStep.mStepNumber = src.mStepNumber;
            newStep.mExecute = bool.TrueString;
            newStep.mScreen = "";
            newStep.mControl = "";
            newStep.mKeyword = "";
            newStep.mParameters = new List<string>();
            for (int idx = 0; idx < mLoadedTest.mInstanceCount; idx++)
            {
                newStep.mParameters.Add("");
            }
            newStep.mStepDelay = 0;
            newStep.mStepStatus = "Not run";
            newStep.mStepLogMessages = new List<DlkLoggerRecord>();
            newStep.mStepStart = new DateTime();
            newStep.mStepEnd = new DateTime();
            newStep.mStepElapsedTime = "";
            mLoadedTest.mTestSteps.Insert(newStep.mStepNumber - 1, newStep);
            // renumber all succeeding steps
            for (int idx = newStep.mStepNumber; idx < mLoadedTest.mTestSteps.Count; idx++)
            {
                mLoadedTest.mTestSteps[idx].mStepNumber++;
            }
        }

        /// <summary>
        /// Load steps and masked password enabled control parameters
        /// </summary>
        private void LoadPasswordSteps()
        {            
            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct)
            {
                mPasswordMasked = new PasswordMasked(mLoadedTest.mTestSteps);

                if (!mPasswordMasked.AllowMasks)
                    mPasswordMasked = null;
            }
        }

        private int DeleteStep()
        {
            isChanged = true;

            //delete selected steps & get next/previous step for selection
            int lastRemovedStep = 0;
            foreach (DlkTestStepRecord dsr in dgTestSteps.SelectedItems)
            {
                lastRemovedStep = dsr.mStepNumber;
                mLoadedTest.mTestSteps.Remove(dsr);                
            }
            var currSelectedItem = mLoadedTest.mTestSteps.Find(x => x.mStepNumber == lastRemovedStep + 1) ??
                                  mLoadedTest.mTestSteps.Find(x => x.mStepNumber == lastRemovedStep - 1);

            // renumber all succeeding steps
            int currSelectedIndex = mLoadedTest.mTestSteps.Count > 0 ? mLoadedTest.mTestSteps.Count - 1 : 0;
            for (int idx = 0; idx < mLoadedTest.mTestSteps.Count; idx++)
            {
                if (currSelectedItem != null && currSelectedItem.mStepNumber == mLoadedTest.mTestSteps[idx].mStepNumber)
                {
                    currSelectedIndex = idx;
                }
                mLoadedTest.mTestSteps[idx].mStepNumber = idx + 1;
            }
            return currSelectedIndex;
        }

        private void MoveUpStep(List<DlkTestStepRecord> Steps)
        {
            if (dgTestSteps.Items.Count == 0 || dgTestSteps.SelectedIndex <= 0)
            {
                // do nothing
            }
            else
            {
                Steps = Steps.OrderBy(x => x.mStepNumber).ToList();
                foreach (DlkTestStepRecord dsr in Steps)
                {
                    // update tests
                    int selectedIndex = dsr.mStepNumber - 1;

                    // update test for saving
                    mLoadedTest.mTestSteps[selectedIndex].mStepNumber--;
                    mLoadedTest.mTestSteps[selectedIndex - 1].mStepNumber++;
                    mLoadedTest.mTestSteps = new List<DlkTestStepRecord>(mLoadedTest.mTestSteps.OrderBy(o => o.mStepNumber).ToList());
                }
                isChanged = true;
            }
        }

        private void MoveDownStep(List<DlkTestStepRecord> Steps)
        {
            if (dgTestSteps.Items.Count == 0 || dgTestSteps.SelectedIndex == dgTestSteps.Items.Count - 1
                || dgTestSteps.SelectedIndex == -1)
            {
                // do nothing
            }
            else
            {                
                Steps = Steps.OrderByDescending(x => x.mStepNumber).ToList();
                foreach (DlkTestStepRecord dsr in Steps)
                {
                    // update tests
                    int selectedIndex = dsr.mStepNumber - 1;

                    //update test for saving
                    mLoadedTest.mTestSteps[selectedIndex].mStepNumber++;
                    mLoadedTest.mTestSteps[selectedIndex + 1].mStepNumber--;
                    mLoadedTest.mTestSteps = (List<DlkTestStepRecord>)(mLoadedTest.mTestSteps.OrderBy(o => o.mStepNumber).ToList());
                }
                isChanged = true;                
            }
        }

        private bool ValidateDataGeneration(out string message)
        {
            message = string.Empty;

            // there should be at least 1 step with a valid parameter
            if (!mLoadedTest.mTestSteps.FindAll(x => x.mParameters[0] != "").Any())
            {
                message = "No valid parameters for data driven test generation.";
                return false;
            }
            
            return true;
        }        
        #endregion

        #region Test Instance methods

        //private void AddTestInstance()
        //{
        //    foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
        //    {
        //        step.mParameters.Add(step.mParameters[CurrentTestInstance - 1]);
        //    }
        //    mLoadedTest.mInstanceCount++;
        //}

        //private void DeleteTestInstance()
        //{
        //    foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
        //    {
        //        step.mParameters.RemoveAt(CurrentTestInstance - 1);
        //    }
        //    mLoadedTest.mInstanceCount--;
        //}

        //private void InsertTestInstance()
        //{
        //    foreach (DlkTestStepRecord step in mLoadedTest.mTestSteps)
        //    {
        //        step.mParameters.Insert(CurrentTestInstance - 1, step.mParameters[CurrentTestInstance - 1]);
        //    }
        //    mLoadedTest.mInstanceCount++;
        //}
        #endregion

        #region //handler events for keyboard navigation // not working as expected
        //private void lbControls_KeyUp(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Key.Equals(Key.Enter))
        //        {
        //            cmbKeyword.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
        //    }
        //}

        //private void lbControls_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Key.Equals(Key.Up))
        //        {
        //            if (lbControls.SelectedIndex != 0)
        //            {
        //                lbControls.SelectedIndex--;
        //            }
        //        }
        //        if (e.Key.Equals(Key.Up))
        //        {
        //            if (ControlItems.Count() != lbControls.SelectedIndex)
        //            {
        //                lbControls.SelectedIndex++;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
        //    }
        //}
        #endregion

        #region//old textbox+listbox control
        //private void lbControls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        txtControl.Text = ((ControlItem)lbControls.SelectedItem).ControlName;
        //        lbControls.Visibility = Visibility.Collapsed;
        //        RefreshKeywords();
        //    }
        //    catch (Exception ex)
        //    {
        //        DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
        //    }
        //}

        //private void RefreshListbox()
        //{
        //    _ControlItems = null;
        //    lbControls.DataContext = ControlItems;
        //}

        //private void txtControl_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    lbControls.Visibility = Visibility.Collapsed;
        //    try
        //    {
        //        cmbKeyword.DataContext = mKeywords;
        //        cmbKeyword.Text = "";
        //        //txtParameters.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
        //    }
        //}

        #endregion

        #region EMAIL
        /// <summary>
        /// Display default mail client Compose Window with input parameters
        /// </summary>
        /// <param name="subject">Subject of email</param>
        /// <param name="body">Body of email</param>
        /// <param name="attachment">Optional mail attachment</param>
        private void SendToEmail(string subject, string body, string attachment = "")
        {
            try
            {
                EmailInfo = CreateEmailInfo();

                DlkMAPI mapi = new DlkMAPI();
                foreach (var recepient in EmailInfo.mRecepient)
                {
                    mapi.AddRecipientTo(recepient);
                }
                if (!string.IsNullOrEmpty(attachment))
                {
                    mapi.AddAttachment(attachment);
                }
                mapi.SendMailPopup(subject, body);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private static DlkEmailInfo CreateEmailInfo()
        {
            var emailInfo = new DlkEmailInfo {mRecepient = new List<string>()};
            //get SMTP Configuration
            GetSmtpConfig(emailInfo);

            //get sender address
            bool useCustomEmail;
            bool.TryParse(DlkConfigHandler.GetConfigValue("usecustomsenderadd"), out useCustomEmail);
            emailInfo.mSender = DlkConfigHandler.GetConfigValue(useCustomEmail ? "customsenderadd" : "defaultsenderadd");

            //include default emails if enabled
            bool useDefaultEmail;
            bool.TryParse(DlkConfigHandler.GetConfigValue("usedefaultemail"), out useDefaultEmail);
            if (useDefaultEmail)
            {
                var defaultEmails = DlkConfigHandler.GetConfigValue("defaultemail");
                if (!string.IsNullOrWhiteSpace(defaultEmails))
                    emailInfo.mRecepient.AddRange(new List<string>(defaultEmails.Split(';')));
            }

            return emailInfo;
        }

        private static void GetSmtpConfig(DlkEmailInfo info)
        {
            info.mSMTPHost = DlkConfigHandler.GetConfigValue("smtphost");
            int defaultPort = 25;
            Int32.TryParse(DlkConfigHandler.GetConfigValue("smtpport"), out defaultPort);
            info.mSMTPPort = defaultPort;
            info.mSMTPUser = DlkConfigHandler.GetConfigValue("smtpuser");
            info.mSMTPPass = DlkConfigHandler.GetConfigValue("smtppass");
        }
        #endregion
    }

    public class ControlItem
    {
        public ControlItem(String name, String type)
        {
            ControlName = name;
            ControlType = type;
        }
        public String ControlName { get; set; }
        public String ControlType { get; set; }
    }

    public class ControlList
    {
        public ControlList(string controltype, bool selected)
        {
            ControlType = controltype;
            Selected = selected;
        }
        public String ControlType { get; set; }
        public Boolean Selected { get; set; }
    }

    /// <summary>
    /// Populates filter popup, consists of checkbox text and state
    /// </summary>
    public class CheckedListItem<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isChecked;
        private string itemName;
        private T item;

        public CheckedListItem(T item, bool isChecked = true, string itemName = "")
        {
            this.item = item;
            this.isChecked = isChecked;
            this.itemName = itemName;
        }

        public T Item
        {
            get { return item; }
            set
            {
                item = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Item"));
            }
        }

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
            }
        }

        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsSelectAll"));
            }
        }
    }

    /// <summary>
    /// Handler for password masked parameters
    /// </summary>
    internal class PasswordMasked
    {
        /// <summary>
        /// Constructor for new record
        /// </summary>
        /// <param name="screen">current selected screen</param>
        /// <param name="control">current selected control</param>
        /// <param name="keyword">current selected keyword</param>
        /// <param name="mParameterList">keyword parameters value</param>
        public PasswordMasked(string screen, string control, string keyword, List<DlkKeywordParameterRecord> mParameterList)
        {
            List<DlkPasswordControl> passwordControls = DlkPasswordMaskedRecord.GetPasswordControls();

            var result = passwordControls.SingleOrDefault(_parameter => _parameter.Screen == screen
                                        && _parameter.Control == control
                                        && _parameter.Keyword == keyword)?.Parameters;

            if (result != null)
            {
                var parameters = new List<DlkPasswordParameter>();
                PasswordParameters = new List<string>();

                foreach (var item in mParameterList)
                {                    
                    parameters.Add(new DlkPasswordParameter(item.mParameterName, item.mIndex, "",result.SingleOrDefault(s => s.Name == item.mParameterName) != null));
                    PasswordParameters.Add("");
                }

                Step = new DlkPasswordControl(screen, control, keyword, parameters);
                AllowMasks = true;
            }
            else
            {
                AllowMasks = false;
            }
        }

        /// <summary>
        /// Edit password masked keyword parameters
        /// </summary>
        /// <param name="screen">current selected screen</param>
        /// <param name="control">current selected control</param>
        /// <param name="keyword">current selected keyword</param>
        /// <param name="mParameters">keyword parameters value</param>
        public PasswordMasked(string screen, string control, string keyword, DlkTestStepRecord step)
        {
            _screen = screen;
            _control = control;
            _keyword = keyword;
            List<DlkPasswordControl> passwordControls = DlkPasswordMaskedRecord.GetPasswordControls();

            var result = passwordControls.SingleOrDefault(_parameter => _parameter.Screen == screen
                                        && _parameter.Control == control
                                        && _parameter.Keyword == keyword)?.Parameters;

            DlkPasswordParameter _pParameter = null;
            List<DlkPasswordParameter> parameters = new List<DlkPasswordParameter>();
            PasswordParameters = new List<string>();

            string[] arrParams = step.mParameters[0].Split(new string[] { DlkTestStepRecord.globalParamDelimiter },StringSplitOptions.None);

            if (step.mPasswordParameters != null)
            {
                for (int i = 0; i < step.mPasswordParameters.Count(); i++)
                {
                    if (result != null)
                        _pParameter = result.SingleOrDefault(s => s.Index == i);

                    if (_pParameter != null)
                    {
                        parameters.Add(new DlkPasswordParameter(_pParameter.Name, i, DlkData.IsDataDrivenParam(step.mPasswordParameters[_pParameter.Index]) ? arrParams[_pParameter.Index] : step.mPasswordParameters[_pParameter.Index], true));//mParameters[_pParameter.Index]
                        Step = new DlkPasswordControl(_screen, _control, _keyword, parameters);
                    }
                    PasswordParameters.Add("");
                }
            }

            AllowMasks = result != null;
        }

        /// <summary>
        /// Load password steps
        /// </summary>
        /// <param name="steps">Test script steps</param>
        public PasswordMasked(List<DlkTestStepRecord> steps)
        {
            List<DlkPasswordParameter> parameters = new List<DlkPasswordParameter>();
            
            foreach (DlkTestStepRecord step in steps)
            {
                if (step.mPasswordParameters != null)
                {
                    DlkPasswordControl control = DlkPasswordMaskedRecord.GetMatchedControl(step);
                    string[] arrParameters = null;

                    if (PasswordParameters == null)
                    {
                        PasswordParameters = new List<string>();
                        AllowMasks = true;
                    }

                    for (int index = 0; index < step.mParameters.Count(); index++)
                    {
                        if (index == 0) // no need to complete iteration process since it has same data in parameters
                        {
                            arrParameters = step.mParameters[0].Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);

                            for (int i = 0; i < arrParameters.Count(); i++)
                            {
                                if (DlkPasswordMaskedRecord.IsMaskedParameter(step, i) && !DlkData.IsDataDrivenParam(step.mPasswordParameters[i]) &&
                                    !DlkData.IsOutPutVariableParam(step.mPasswordParameters[i]) && !DlkData.IsGlobalVarParam(step.mPasswordParameters[i]))
                                {
                                    var _control = control.Parameters.SingleOrDefault(s => s.Index == i);
                                    string maskedText = "";
                                    parameters.Add(new DlkPasswordParameter(_control.Name, _control.Index, step.mPasswordParameters[i], true));
                                    PasswordParameters.Add(step.mPasswordParameters[i]);

                                    step.mPasswordParameters[i].ToArray().ToList().ForEach(f => maskedText += DlkPasswordMaskedRecord.PasswordChar);
                                    arrParameters[i] = !String.IsNullOrWhiteSpace(maskedText) ? maskedText : DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE;
                                }
                                else
                                    PasswordParameters.Add("");
                            }
                        }

                        Step = new DlkPasswordControl(step.mScreen, step.mControl, step.mKeyword, parameters);
                        step.mParameters[index] = string.Join(DlkTestStepRecord.globalParamDelimiter, arrParameters);
                    }
                }
            }
        }

        #region Public Methods

        /// <summary>
        /// Set display for password textbox
        /// </summary>
        /// <returns>Data Driven Text, Output Var Text, Global Var Text or Masked Text</returns>
        /// <param name="IsBlankPasswordCleared"> specify if blank password should be shown as clear or masked </param>
        public string DisplayPassword(bool IsBlankPasswordCleared)
        {
            string result = string.Empty;
            if (DlkData.IsDataDrivenParam(PasswordText) || DlkData.IsOutPutVariableParam(PasswordText) || DlkData.IsGlobalVarParam(PasswordText))
            {
                result = PasswordText;
            }
            else
            {
                var _dummyPassword = new StringBuilder();
                Enumerable.Range(1, PasswordText.Length).ToList().ForEach(f => _dummyPassword.Append(DlkPasswordMaskedRecord.PasswordChar));
                result = !String.IsNullOrWhiteSpace(PasswordText) ? _dummyPassword.ToString() : //if password text not null, use dummy password
                        IsBlankPasswordCleared ?_dummyPassword.ToString() : //if password cleared is true, use dummy password
                        DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE; //else, use default blank masked value
            }
            return result;
        }

        /// <summary>
        /// Password textbox textchanged handler
        /// </summary>
        /// <param name="changed">changed property of textbox</param>
        /// <param name="keyInput">keyboard key input</param>
        /// <param name="IsBlankPasswordClear">specify if blank password should be shown as clear or masked</param>
        public void PasswordChanged(TextChange changed, string keyInput, ref bool IsBlankPasswordClear)
        {

            //logic for contextmenu click
            if (keyInput.Length > 1)
                _inputPassword = new StringBuilder();
            else
                _inputPassword = new StringBuilder(Step.Parameters.SingleOrDefault(s => s.Name == _currentParameter).Password);

            if (keyInput == "" && changed.AddedLength > 0 && _inputPassword.Length > 0)
                return;

            if (!(changed.AddedLength > 0 && keyInput == "" && changed.RemovedLength == 0))
            {
                if (changed.AddedLength > 0 && changed.RemovedLength == 0)
                {
                    if (changed.Offset > 0)
                    {
                        _inputPassword.Insert(changed.Offset, keyInput);
                    }
                    else
                    {
                        _inputPassword.Insert(0, keyInput);
                    }
                }
                else
                {
                    if (changed.AddedLength > 0)
                    {
                        _inputPassword.Remove(changed.Offset, changed.RemovedLength);
                        _inputPassword.Insert(changed.Offset, keyInput);
                    }
                    else
                    {
                        //perform this action only if password is not empty
                        if (!String.IsNullOrWhiteSpace(_inputPassword.ToString()))
                        {
                            _inputPassword.Remove(changed.Offset, changed.RemovedLength);
                            //treat as blank password if changes will clear the password content
                            if (String.IsNullOrWhiteSpace(_inputPassword.ToString())) IsBlankPasswordClear = true;
                        }
                    }
                }
            }

            if (keyInput == PasswordText && keyInput.Length > 0)
                SelectionStart = keyInput.Length;
            else
                SelectionStart = changed.Offset + (changed.RemovedLength == 0 || changed.AddedLength > 0 ? 1 : 0);

            var step = Step.Parameters.SingleOrDefault(s => s.Name == _currentParameter);
            step.Password = PasswordText;
            IsBlankPasswordClear = PasswordText == "";
            PasswordParameters[step.Index] = PasswordText;
        }

        /// <summary>
        /// Checks keyword parameter if set to masked control
        /// </summary>
        /// <param name="parameterName">keyword parameter name</param>
        /// <returns>true (if parameter exists) or false (parameter does not exists)</returns>
        public bool ContainsParameter(string parameterName)
        {
            var selected = Step.Parameters.SingleOrDefault(s => s.Name == parameterName && s.AllowMasked);

            if (selected != null)
            {
                _currentParameter = parameterName;
                return true;
            }
            return false;
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Holds masked enabled keyword parameter 
        /// </summary>
        public DlkPasswordControl Step { get; set; }

        /// <summary>
        /// Selection start for masked enabled parameter
        /// </summary>
        public int SelectionStart { get; private set; }

        /// <summary>
        /// Identifier to allow masked for selected parameter
        /// </summary>
        public bool AllowMasks { get; set; }

        /// <summary>
        /// Holds parameters value for current step
        /// </summary>
        public List<string> PasswordParameters { get; set; }
        #endregion

        #region Private Properties
        private string PasswordText => _inputPassword.ToString();

        private StringBuilder _inputPassword { get; set; }

        private string _currentParameter { get; set; }

        private string _screen { get; set; }

        private string _control { get; set; }

        private string _keyword { get; set; }
        #endregion
    }
}
