using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using CommonLib.DlkUtility;
using TestRunner.Common;
using TestRunner.Controls;
using TestRunner.Designer.View;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using TestRunner.AdvancedScheduler;
using System.ComponentModel;

namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for SuitesForm.xaml
    /// </summary>
    public partial class LibraryMainForm : Window, IMainView, ITestSuitesView, ITestsView, IFinderView, INotifyPropertyChanged
    {
        #region CONSTANTS
        public const int TAB_IDX_HOME = 0;
        public const int TAB_IDX_SUITES = 1;
        public const int TAB_IDX_TESTS = 2;
        public const int TAB_IDX_FINDER = 3;
        public const int TAB_IDX_ADMIN = 4;
        private const string STR_LOADING_SCREEN_TEXT = "Loading Test Library...";
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region PRIVATE MEMBERS
        private Filter mFilter = new Filter();
        private List<KwDirItem> mSuites;
        private List<KwDirItem> mFilteredSuites;
        private List<KwDirItem> mTests;
        private List<KwDirItem> mFilteredTests;
        private List<TLSuite> mAllSuites;
        private List<DlkObjectStoreFileRecord> mObjectStoreFiles;
        private MainPresenter mMainPresenter;
        private TLSuite mTargetSuite;
        private DlkExecutionQueueRecord mTargetSuiteTests;
        private List<DlkTestStepRecord> mSelectedTestSteps;
        private TestSuitesPresenter mTestSuitesPresenter;
        private List<TLSuite> mContainingSuites = new List<TLSuite>();
        private DlkTest mTargetTest;
        private List<DlkExecutionQueueRecord> mSelectedSuiteTests;
        private List<DlkTestStepRecord> mSelectedSuiteTestSteps;
        private TestPresenter mTestPresenter;
        private FinderPresenter mFinderPresenter;
        private List<string> mScreens;
        private List<string> mControls;
        private List<string> mKeywords;
        private List<Match> mFinderMatches;
        private string mCurrentControl;
        private string mCurrentKeyword;
        private string mCurrentParameter;
        private string mCurrentScreen;
        private List<DlkTestStepRecord> mFinderTest;
        private bool mIncludeParameters;
        private bool mIncludeParametersCanceled = false;
        private System.Windows.Forms.Timer mTxtSearchTimer;
        //private BFFolder[] mTestSuiteRoot;
        private ObservableCollection<DlkKeywordParameterRecord> _mKeywordParameters;
        private ContextMenu mCurrentContextMenu = new ContextMenu();
        private string mSaveDialogFileName = String.Empty;
        private bool mSaveDialogLaunched = false;
        private List<ControlItem> _ControlItems;
        private const string FIND_RESULTS = "Find Results";
        private string mDirectoryFilter = string.Empty;
        public static string mCurrentSelectedTestInSuite = string.Empty;
        public static string mCurrentSelectedParentSuiteInTest = string.Empty;
        private Dictionary<TreeViewItem, KwDirItem> mTreeItemSelectionList = new Dictionary<TreeViewItem, KwDirItem>();
        private bool mIsLastMouseClickInTestTreeRight;
        DlkBackgroundWorkerWithAbort mBgw = new DlkBackgroundWorkerWithAbort();
        private Modal mLoadingScreen = new Modal(null, STR_LOADING_SCREEN_TEXT);
        private List<DlkTargetApplication> mAllProducts = new List<DlkTargetApplication>();
        private DlkTargetApplication mTarget = new DlkTargetApplication();
        private static string DIR_FILE = DlkEnvironment.mDirFramework + "Library\\Directory\\Directory.xml";
        private bool mShowQueryResults;
        private List<DlkTest> mQueryResultsTest = new List<DlkTest>();
        private DlkTest mSelectedQueryResultTest = null;
        private Dictionary<string, List<DlkTest>> mAllQueryResultTest = new Dictionary<string, List<DlkTest>>();
        private string mQueryResultsTitle;
        private string mSelectedQueryResultTestStepCount;
        private string mCurrentQueryPath = string.Empty;
        private Enumerations.QueryType mCurrentQueryType = Enumerations.QueryType.Test;
        private Dictionary<int, Enumerations.QueryRowColor> mQueryColorMap = new Dictionary<int, Enumerations.QueryRowColor>();
        private List<QueryRow> mQueryRows = new List<QueryRow>();
        private List<DlkTag> mTagsToAdd = new List<DlkTag>();
        private List<DlkTag> mTags = new List<DlkTag>();
        private List<ControlItem> ControlItems
        {
            get
            {
                return GetControls();
            }
        }
        private List<QueryCol> mQueryCols = new List<QueryCol>();
        private Query mCurrentQuery;
        private List<string> mQueryTypes = new List<string>();
        private List<string> mQueryList = new List<string>();
        #endregion

        #region PUBLIC FUNCTIONS
        /// <summary>
        /// Class constructor
        /// </summary>
        public LibraryMainForm()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// Update status of UI
        /// </summary>
        /// <param name="Status"></param>
        public void UpdateViewStatus(object Status)
        {
            if (Status is String)
            {
                string update = Status.ToString();

                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        txtStatus.Text = update;
                    }));
            }
            else if (Status is Enumerations.TestViewStatus)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        var advStatus = (Enumerations.TestViewStatus)Status;
                        switch (advStatus)
                        {
                            case Enumerations.TestViewStatus.NewSuiteAdded:
                                break;
                            case Enumerations.TestViewStatus.SelectedSuiteEdited:
                                /* Re-select */
                                SelectSuiteFromTree(tvwAllSuites);
                                break;
                            default:
                                break;
                        }
                    }));
            }
            else if (Status is Enumerations.FinderViewStatus)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        var advStatus = (Enumerations.FinderViewStatus)Status;
                        switch (advStatus)
                        {
                            case Enumerations.FinderViewStatus.NewTestAdded:
                                break;
                            case Enumerations.FinderViewStatus.SelectedTestEdited:
                                if (tabMain.SelectedIndex == TAB_IDX_SUITES)
                                {
                                    SelectSuiteFromTree(tvwAllSuites); /* Re-select */
                                }
                                else if (tabMain.SelectedIndex == TAB_IDX_TESTS)
                                {
                                    SelectTestFromTree(tvwAllTests); /* Re-select */
                                }
                                dgTestsInSuite.Items.Refresh();
                                break;
                            default:
                                break;
                        }
                    }));
            }
        }

        /// <summary>
        /// All products available for selection
        /// </summary>
        public List<DlkTargetApplication> AvailableProducts
        {
            get
            {
                return mAllProducts;
            }
            set
            {
                mAllProducts = value;
            }
        }

        /// <summary>
        /// User selection from list
        /// </summary>
        public DlkTargetApplication SelectedProduct
        {
            get
            {
                return mTarget;
            }
            set
            {
                mTarget = value;
            }
        }

        /// <summary>
        /// Currently selected cell in Query report pane in Row|Column (*) format
        /// </summary>
        public string QueryResultsTitle
        {
            get
            {
                return mQueryResultsTitle;
            }
            set
            {
                mQueryResultsTitle = value;
                OnPropertyChanged("QueryResultsTitle");
            }
        }

        /// <summary>
        /// Number of steps of selected test in query result pane in (*) format
        /// </summary>
        public string SelectedQueryResultTestStepCount
        {
            get
            {
                return mSelectedQueryResultTestStepCount;
            }
            set
            {
                mSelectedQueryResultTestStepCount = value;
                OnPropertyChanged("SelectedQueryResultTestStepCount");
            }
        }

        public string CurrentQueryPath
        {
            get
            {
                return mCurrentQueryPath;
            }
            set
            {
                mCurrentQueryPath = value;
            }
        }

        public Enumerations.QueryType CurrentQueryType
        {
            get
            {
                return mCurrentQueryType;
            }
            set
            {
                mCurrentQueryType = value;
                grdSuiteQueryResults.Visibility = value == Enumerations.QueryType.Suite ? Visibility.Visible : Visibility.Collapsed;
                grdTestQueryResults.Visibility = value == Enumerations.QueryType.Suite ? Visibility.Collapsed : Visibility.Visible;


                /* TEMP: while Suite Query results pane not yet ready */
                if (value == Enumerations.QueryType.Suite)
                {
                    chkShowQueryResults.IsChecked = false;
                    ShowQueryResult = false;
                    chkShowQueryResults.IsEnabled = false;
                }
                else // Test
                {
                    chkShowQueryResults.IsEnabled = true;
                }
                /* end of TEMP */
            }
        }

        public DataView CurrentQueryDataSource
        {
            get
            {
                return dgQuery.ItemsSource as DataView;
            }
            set
            {
                dgQuery.ItemsSource = value;
                dgQuery.Items.Refresh();
            }
        }

        public Dictionary<int, Enumerations.QueryRowColor> QueryColorMap
        {
            get
            {
                return mQueryColorMap;
            }
            set
            {
                mQueryColorMap = value;
                for (int i = 0; i < mQueryColorMap.Count; i++)
                {
                    DataGridRow firstRow = dgQuery.ItemContainerGenerator.ContainerFromItem(dgQuery.Items[i]) as DataGridRow; // wrong
                    DataGridCell firstColumnInFirstRow = dgQuery.Columns[0].GetCellContent(firstRow).Parent as DataGridCell;

                }
            }
        }

        public List<QueryRow> QueryRows
        {
            get
            {
                return mQueryRows;
            }
            set
            {
                mQueryRows = value;
                dgQuery.ItemsSource = mQueryRows.ToDataTable(new string[] { "Name", "TotalValue", "Color" }).AsDataView();
                dgQuery.Items.Refresh();
                //dgQuery.Columns[2].Visibility = System.Windows.Visibility.Hidden;
                //dgQuery.Columns[2].Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public List<DlkTest> AllTests
        {
            get
            {
                return mFinderPresenter.LoadedTests;
            }
        }

        public List<TLSuite> AllSuites
        {
            get
            {
                return mTestPresenter.AllSuites;
            }
        }

        public List<DlkTag> TagsToAdd
        {
            get
            {
                return mTagsToAdd;
            }
            set
            {
                mTagsToAdd = value;
            }
        }

        public List<DlkTag> Tags
        {
            get
            {
                return mTags;
            }
            set
            {
                mTags = value;
            }
        }

        public List<QueryCol> QueryCols
        {
            get
            {
                return mQueryCols;
            }
            set
            {
                mQueryCols = value;
            }
        }

        public Query CurrentQuery
        {
            get
            {
                return mCurrentQuery;
            }
            set
            {
                cboCurrentQuery.SelectedItem = value.Name;
                mCurrentQuery = value;
            }
        }

        public List<string> QueryTypes
        {
            get
            {
                if (mQueryTypes == null || !mQueryTypes.Any())
                {
                    mQueryTypes = new List<string>() { Enumerations.ConvertToString(Enumerations.QueryType.Test),
                        Enumerations.ConvertToString(Enumerations.QueryType.Suite)};
                }
                return mQueryTypes;
            }
            set
            {
                mQueryTypes = value;
            }
        }

        public List<string> QueryList
        {
            get
            {
                return mQueryList;
            }
            set
            {
                mQueryList = value;
                cboCurrentQuery.ItemsSource = value;
                if (value.Any())
                {
                    cboCurrentQuery.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region PRIVATE FUNCTIONS

        /// <summary>
        /// Initialize critical resources and UI states
        /// </summary>
        private void Initialize()
        {
            /* BW to load resources */
            mBgw.DoWork += MBgw_DoWork;
            mBgw.RunWorkerCompleted += MBgw_RunWorkerCompleted;
            mBgw.RunWorkerAsync();

            /* Dislay loading screen */
            mLoadingScreen.ShowDialog();

            /* Set data bindings */
            tvwAllSuites.DataContext = Suites;
            tvwAllTests.DataContext = Tests;
            cboScreens.ItemsSource = Screens;
            cboQueryTypes.ItemsSource = QueryTypes;
            cboQueryTypes.SelectedIndex = 0;

            /* Set visibility of tabs, UI cosmetics */
            foreach (TabItem ti in tabMain.Items)
            {
                ti.Visibility = System.Windows.Visibility.Collapsed;
            }

            /* temporary --> to select Suites tab, should be Home */
            rdoSuites.IsChecked = true;
            RefreshTagList();
            AdminButtonStates(false);

            /* Admin - Preferences Tab */
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DlkTestRunnerSettingsHandler.Initialize(Directory.GetParent(binDir).FullName);
            mAllProducts = DlkTestRunnerSettingsHandler.ApplicationList;
            cboProduct.ItemsSource = AvailableProducts;
            SetProduct();
            txtSuiteDirectory.Text = mDirectoryFilter;

            FilterChange();
        }

        /// <summary>
        /// Load all resources from files
        /// </summary>
        private void LoadResources()
        {
            /* Load resources */
            mFilteredSuites = new List<KwDirItem>();
            mFilteredTests = new List<KwDirItem>();
            mAllSuites = new List<TLSuite>();
            mFinderTest = new List<DlkTestStepRecord>();
            mMainPresenter = new MainPresenter(this);
            mMainPresenter.LoadTags();
            if (SelectedProduct.ProductFolder == null)
            {
                mDirectoryFilter = mMainPresenter.RetrieveDirectoryFilter(DIR_FILE);
            }
            else
            {
                mDirectoryFilter = mMainPresenter.RetrieveDirectoryFilter(DlkEnvironment.mDirProductsRoot + SelectedProduct.ProductFolder +
                            "\\Framework\\Library\\Directory\\Directory.xml");
            }
            mMainPresenter.LoadObjectStore();
            mMainPresenter.LoadSuites(mDirectoryFilter);
            mMainPresenter.LoadTests();
            mFinderPresenter = new FinderPresenter(this);
            mFinderPresenter.LoadScreens(ObjectStoreFiles);
            mFinderPresenter.LoadAllTests();

            mTestSuitesPresenter = new TestSuitesPresenter(this);
            mTestPresenter = new TestPresenter(this);
            mTestPresenter.LoadAllSuiteTests(mDirectoryFilter);

            mMainPresenter.LoadAllQueries();
            //mMainPresenter.LoadQuery();
        }

        /// <summary>
        /// Select current tab with index
        /// </summary>
        /// <param name="index"></param>
        private void SelectTab(int index)
        {
            tabMain.SelectedIndex = index;
        }

        /// <summary>
        /// Display test steps in destinagrid based from selection in sourceGrid
        /// </summary>
        /// <param name="sourceGrid"></param>
        /// <param name="destinationGrid"></param>
        /// <param name="showData"></param>
        private void ShowSelectedTestSteps(DataGrid sourceGrid, DataGrid destinationGrid, bool showData)
        {
            if (mTestSuitesPresenter != null && sourceGrid.SelectedItem != null && sourceGrid.SelectedItem.GetType() == typeof(DlkExecutionQueueRecord))
            {
                DlkExecutionQueueRecord targetTest = sourceGrid.SelectedItem as DlkExecutionQueueRecord;
                mTestSuitesPresenter.LoadSelectedTestSteps(targetTest, showData);
                destinationGrid.ItemsSource = SelectedTestSteps;
                // Display name & description of selected test in grid
                txtSelectedTestName.Text = targetTest.file;
                string author = string.Empty;
                string desc = targetTest.description;
                try
                {
                    var tst = new DlkTest(targetTest.fullpath);
                    author = tst.mTestAuthor;
                    desc = tst.mTestDescription;
                }
                catch
                {
                    /* Ignore. This is only for purposes of retrieving author */
                }
                txtSelectedTestAuthor.Text = author;
                txtSelectedTestDescription.Text = desc;
            }
            else if (dgTestSteps != null)
            {
                destinationGrid.ItemsSource = null;
            }
            else
            {
                return;
            }
            /* refresh UI */
            destinationGrid.Items.Refresh();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                new Action(delegate()
                {
                    ColorParams(destinationGrid);
                }));
        }

        /// <summary>
        /// Display test steps in destinagrid based from sourceTest
        /// </summary>
        /// <param name="sourceTest"></param>
        /// <param name="destinationGrid"></param>
        /// <param name="showData"></param>
        private void ShowSelectedTestSteps(DlkTest sourceTest, DataGrid destinationGrid, bool showData)
        {
            if (mTestSuitesPresenter != null)
            {
                mTestSuitesPresenter.LoadSelectedTestSteps(sourceTest, showData);
                destinationGrid.ItemsSource = SelectedTestSteps;
                /* refresh UI */
                destinationGrid.Items.Refresh();
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        ColorParams(destinationGrid);
                    }));
            }
        }

        /// <summary>
        /// Helper function to return visual children of input control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
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

        /// <summary>
        /// Apply foreground color changes to gridcell contents if uses data-driven convention (D{})
        /// </summary>
        /// <param name="targetGrid"></param>
        private void ColorParams(DataGrid targetGrid)
        {
            try
            {
                if (SelectedTestSteps != null && SelectedTestSteps.Count > 0)
                {
                    List<TextBlock> lst = FindVisualChildren<TextBlock>(targetGrid).ToList();
                    foreach (TextBlock block in FindVisualChildren<TextBlock>(targetGrid)) // look for txtParams child control
                    {
                        if (block.Name == "txtParams")
                        {
                            string[] paramFields = block.Text.Split('|');
                            block.Inlines.Clear();
                            int fieldCount = 1; // counter for number of parameters
                            foreach (string param in paramFields)
                            {
                                if (param.Substring(0, param.Length > 1 ? 2 : 0) == "D{") // data-driven fields
                                {
                                    block.Inlines.Add(new Run(param) { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
                                }
                                else if (param.Substring(0, param.Length > 1 ? 2 : 0) == "O{") // output fields
                                {
                                    block.Inlines.Add(new Run(param) { Foreground = Brushes.Blue, FontWeight = FontWeights.Bold });
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
                    }

                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Get the values from the parameter grid to be display on the table
        /// </summary>
        /// <returns></returns>
        private string GetParametersFromGrid()
        {
            string delimiter = DlkTestStepRecord.globalParamDelimiter;
            string ret = string.Empty;
            bool hasParam = false;
            foreach (DlkKeywordParameterRecord dr in dgParameters.Items)
            {
                ret += string.Format("{0}{1}", dr.mValue, (dr.mContains == true) ? "^" : "") + delimiter;
                hasParam = true;
            }
            if (hasParam)
            {
                int count = CountStringOccurrences(ret, delimiter);
                if (count != dgParameters.Items.Count - 1 && ret.EndsWith(delimiter))
                {
                    ret = ret.Substring(0, ret.Length - 4);
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

        private void RefreshTagList()
        {
            lbxTags.Items.Clear();
            foreach (DlkTag tag in Tags)
            {
                lbxTags.Items.Add(tag.Name);
            }
        }

        private List<DlkTag> BuildConflicts(string conflictIDs)
        {
            List<DlkTag> tagList = new List<DlkTag>();
            if (!(String.IsNullOrEmpty(conflictIDs)))
            {
                List<string> IDs = conflictIDs.Trim(';').Split(';').ToList();

                foreach (string ID in IDs)
                {
                    tagList.Add(Tags.Find(x => x.Id == ID));
                }
            }
            return tagList;
        }

        private void AdminButtonStates(bool state)
        {
            btnAddEditConflict.IsEnabled = state;
            btnEditTag.IsEnabled = state;
        }

        private static DataGridRow GetRow(DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                grid.UpdateLayout();
                if (grid.Items.CurrentItem != null)
                {
                    grid.ScrollIntoView(grid.Items.CurrentItem);
                }
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        private static DataGridCell GetCell(DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        private static T GetVisualChild<T>(Visual parent) where T : Visual
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

        private void RefreshRowColors()
        {
            foreach (QueryRow row in QueryRows)
            {
                DataGridRow dgRow = GetRow(dgQuery, row.Index);
                DataGridCell cell = GetCell(dgQuery, dgRow, 0);

                if (cell != null)
                {
                    switch (row.Color)
                    {
                        case Enumerations.QueryRowColor.Gray:
                            cell.Background = Brushes.WhiteSmoke;
                            break;
                        case Enumerations.QueryRowColor.Orange:
                            cell.Background = Brushes.Gold;
                            break;
                        case Enumerations.QueryRowColor.Yellow:
                            cell.Background = Brushes.LemonChiffon;
                            break;
                    }
                }
            }
        }

        private bool LaunchSaveDialog()
        {
            // Displays a SaveFileDialog so the can select where to save the exported file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx;";
            saveFileDialog.Title = "Save File To Export";
            saveFileDialog.FileName = "TestLibraryReport";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)saveFileDialog.ShowDialog())
            {
                mSaveDialogFileName = saveFileDialog.FileName.ToString();
                mSaveDialogLaunched = true;
            }
            return mSaveDialogLaunched;
        }

        private List<ControlItem> GetControls()
        {
            if (_ControlItems == null)
            {
                _ControlItems = new List<ControlItem>();
                foreach (String ctrl in mControls)
                {
                    String mControl = "";
                    if (ctrl == "Function")
                    {
                        mControl = "Function";
                    }
                    else
                    {
                        mControl = DlkDynamicObjectStoreHandler.GetControlType(cboScreens.SelectedItem.ToString(), ctrl);
                    }
                    _ControlItems.Add(new ControlItem(ctrl, mControl));
                }
            }
            return _ControlItems;
        }

        /// <summary>
        /// Gets KwDirItem of Test to select in Test tab treeview
        /// </summary>
        /// <param name="path">Path of target test</param>
        /// <param name="listOfItems">Pool where to search for test</param>
        /// <returns>KwDirItem of target test</returns>
        private KwDirItem GetTestTreeviewItemToSelect(string path, List<KwDirItem> listOfItems)
        {
            KwDirItem ret = null;
            try
            {
                foreach (KwDirItem itm in listOfItems)
                {
                    if (itm is KwFolder)
                    {
                        ret = GetTestTreeviewItemToSelect(path, ((KwFolder)itm).DirItems);
                        if (ret != null)
                        {
                            break;
                        }
                    }
                    else if (itm is KwFile)
                    {
                        if (itm.Path == path)
                        {
                            ret = itm;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            return ret;
        }

        /// <summary>
        /// Gets KwDirItem of Suite to select in Suite tab treeview
        /// </summary>
        /// <param name="path">Path of target suite</param>
        /// <param name="listOfItems">Pool where to search for suite</param>
        /// <returns>KwDirItem of target suite</returns>
        private KwDirItem GetSuiteTreeviewItemToSelect(string path, List<KwDirItem> listOfItems)
        {
            KwDirItem ret = null;
            try
            {
                foreach (KwDirItem itm in listOfItems)
                {
                    if (itm is BFFolder)
                    {
                        ret = GetSuiteTreeviewItemToSelect(path, ((BFFolder)itm).DirItems);
                        if (ret != null)
                        {
                            break;
                        }
                    }
                    else if (itm is BFFile)
                    {
                        if (itm.Path == path)
                        {
                            ret = itm;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            return ret;
        }

        /// <summary>
        /// Select Test in Test tab treeview
        /// </summary>
        /// <param name="testPath">Path of test to select</param>
        /// <param name="suitePath">Path of current suite</param>
        private void SelectTest(string testPath, string suitePath)
        {
            /* Should not be invoked by Test tab */
            if (tabMain.SelectedIndex != TAB_IDX_TESTS)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        KwDirItem target = GetTestTreeviewItemToSelect(testPath, Tests.ToList());
                        if (target != null)
                        {
                            mCurrentSelectedTestInSuite = target.Path; // Cache path of current selected test in suite
                            CollapseTestTreeViewItems(Tests.ToList()); // Collapse everything, let datatrigger expand target
                            target.IsSelected = true;
                        }
                    }));
            }
        }

        /// <summary>
        /// Select Suite in Suite tab treeview
        /// </summary>
        /// <param name="suitePath">Path of suite to select</param>
        /// <param name="testPath">Path of currrent test</param>
        private void SelectSuite(string suitePath, string testPath)
        {
            /* Should not be invoked by Suite tab */
            if (tabMain.SelectedIndex != TAB_IDX_SUITES)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        KwDirItem target = GetSuiteTreeviewItemToSelect(suitePath, Suites.ToList());
                        if (target != null)
                        {
                            mCurrentSelectedParentSuiteInTest = target.Path; // Cache path of current selected parent suite
                            CollapseSuiteTreeViewItems(Suites.ToList()); // Collapse everything, let datatrigger expand target
                            target.IsSelected = true;
                        }
                    }));
            }
        }

        /// <summary>
        /// Checks if CTRL key is pressed
        /// </summary>
        /// <returns>True if pressed, False otherwise</returns>
        private bool IsControlPressed()
        {
            return System.Windows.Input.Keyboard.IsKeyDown(Key.LeftCtrl)
                || System.Windows.Input.Keyboard.IsKeyDown(Key.RightCtrl);
        }

        /// <summary>
        /// Add treeviewitem to selection cache
        /// </summary>
        /// <param name="ItemToAdd">Tree view item to add</param>
        /// <param name="Test">Object of item</param>
        /// <returns>True if new item was added, False otherwise</returns>
        private bool AddToSelectionList(TreeViewItem ItemToAdd, KwDirItem Test)
        {
            bool ret = false;
            if (!mTreeItemSelectionList.Keys.Contains(ItemToAdd))
            {
                mTreeItemSelectionList.Add(ItemToAdd, Test);
                /* style item to simulate selected item look */
                ItemToAdd.Background = SystemColors.HighlightBrush;
                ItemToAdd.Foreground = SystemColors.HighlightTextBrush;
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
            return ret;
        }

        /// <summary>
        /// Clears Test treeview selection cache
        /// </summary>
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
        /// Sets the Product combo box with the selected product
        /// </summary>
        private void SetProduct()
        {
            string selectedProduct = string.Empty;
            string productFolder = SelectedProduct.ProductFolder == null ? DlkEnvironment.mProductFolder : SelectedProduct.ProductFolder;

            foreach (DlkTargetApplication app in AvailableProducts)
            {
                if (app.ProductFolder == productFolder)
                {
                    cboProduct.Text = app.DisplayName;
                    break;
                }
            }
        }

        /// <summary>
        ///  Sets the Product combo box with the selected product
        /// </summary>
        /// <param name="displayName">Display name of the product</param>
        /// <returns>DlkTargetApplication object</returns>
        private DlkTargetApplication SetTargetProduct(string displayName)
        {
            mTarget = null;

            foreach (DlkTargetApplication app in AvailableProducts)
            {
                if (app.DisplayName == displayName)
                {
                    mTarget = app;
                    break;
                }
            }
            return mTarget;
        }

        /// <summary>
        /// Open target test in Test Editor
        /// </summary>
        /// <param name="TestPath">Path of test to open</param>
        /// <returns>Dialog result in bool</returns>
        private bool EditTest(string TestPath)
        {
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mRootPath = Directory.GetParent(binDir).FullName;
            bool ret;

            while (new DirectoryInfo(mRootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }
            MainWindow dummyWindow = new MainWindow();
            DlkTestRunnerSettingsHandler.Initialize(mRootPath);
            TestEditor editorWindow = new TestEditor(dummyWindow, DlkEnvironment.mLibrary, TestPath, true);
            editorWindow.btnRun.Visibility = Visibility.Collapsed;
            editorWindow.ShowDialog();
            ret = (bool)editorWindow.DialogResult;
            if (ret)
            {
                mFinderPresenter.UpdateLoadedTests(new FileInfo[] { new FileInfo(TestPath) });
                dummyWindow.Close();
            }
            return ret;
        }

        /// <summary>
        /// Select suite from input Treeview control
        /// </summary>
        /// <param name="targetTree">Target treeview control</param>
        private void SelectSuiteFromTree(TreeView targetTree)
        {
            try
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        var selectedItem = targetTree.SelectedItem;
                        if (mTestSuitesPresenter != null && selectedItem != null && selectedItem.GetType() == typeof(BFFile))
                        {
                            BFFile file = targetTree.SelectedItem as BFFile;
                            mTestSuitesPresenter.LoadTargetSuite(file.Path, AllSuites);

                            txtSuiteName.Text = TargetSuite.name;
                            txtSuiteDescription.Text = TargetSuite.SuiteInfo.Description;
                            dgTestsInSuite.ItemsSource = TargetSuite.Tests;

                            if (TargetSuite.Tests.Any())
                            {
                                int index = 0;

                                /* Update description */
                                //foreach (DlkExecutionQueueRecord tst in TargetSuite.Tests)
                                //{
                                //    var targetTestinCache = AllTests.FindAll(x => x.mTestPath == tst.fullpath).FirstOrDefault();
                                //    if (targetTestinCache != null)
                                //        tst.description = targetTestinCache.mTestDescription;
                                //}
                                /* end update desc */

                                if (TargetSuite.Tests.Any(x => x.fullpath == mCurrentSelectedTestInSuite))
                                {
                                    index = dgTestsInSuite.Items.IndexOf(TargetSuite.Tests.FirstOrDefault(
                                        x => x.fullpath == mCurrentSelectedTestInSuite));
                                }
                                if (dgTestsInSuite.SelectedIndex != index)
                                {
                                    dgTestsInSuite.SelectedIndex = index;
                                }
                                else
                                {
                                    dgTestsInSuite_SelectionChanged(dgTestsInSuite, null);
                                }
                                dgTestsInSuite.ScrollIntoView(dgTestsInSuite.SelectedItem);
                            }

                            tsHeader.Text = string.Format("Tests in Suite ({0})", TargetSuite.Tests.Count());
                        }
                        else
                        {
                            txtSuiteName.Clear();
                            txtSuiteDescription.Clear();
                            txtSelectedTestName.Clear();
                            txtSelectedTestAuthor.Clear();
                            txtSelectedTestDescription.Clear();
                            dgTestsInSuite.ItemsSource = null;
                            tsHeader.Text = "Tests in Suite (0)";
                        }

                        /* refresh UI */
                        dgTestsInSuite.Items.Refresh();
                    }));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Select test from input Treeview control
        /// </summary>
        /// <param name="targetTree">Target treeview control</param>
        private void SelectTestFromTree(TreeView targetTree)
        {
            try
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                    new Action(delegate ()
                    {
                        if (mTestPresenter != null && targetTree.SelectedItem != null && targetTree.SelectedItem.GetType() == typeof(KwFile))
                        {
                            KwFile file = targetTree.SelectedItem as KwFile;
                            mTestPresenter.LoadTargetTest(file.Path, AllTests);

                            txtTestName.Text = TargetTest.mTestName;
                            txtTestDescription.Text = TargetTest.mTestDescription;
                            txtTestAuthor.Text = TargetTest.mTestAuthor;

                            if (ContainingSuites.Any())
                            {
                                int index = 0;
                                if (ContainingSuites.Any(x => x.path == mCurrentSelectedParentSuiteInTest))
                                {
                                    index = dgContainingSuites.Items.IndexOf(ContainingSuites.FirstOrDefault(
                                        x => x.path == mCurrentSelectedParentSuiteInTest));
                                }
                                if (dgContainingSuites.SelectedIndex != index)
                                {
                                    dgContainingSuites.SelectedIndex = index;

                                }
                                else
                                {
                                    dgContainingSuites_SelectionChanged(dgContainingSuites, null);
                                }
                                dgContainingSuites.ScrollIntoView(dgContainingSuites.SelectedItem);
                            }
                            else
                            {
                                ShowSelectedTestSteps(TargetTest, dgTestStepsTest, (bool)chkShowDataTest.IsChecked);
                            }
                        }
                        else
                        {
                            txtTestName.Clear();
                            txtTestDescription.Clear();
                            txtTestAuthor.Clear();
                            dgContainingSuites.ItemsSource = null;
                            dgContainingSuites.Items.Refresh();
                        }
            }));
        }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void FilterChange()
        {
            //Description
            mFilter.Description = tbxDescription.Text;
            mFilter.DescExactMatch = cbDescExactmatch.IsChecked == true;
            //Tags
            mFilter.Tags = tbxTags.Text;
            if (rTagsAnd.IsChecked == true)
            {
                mFilter.TagsAnd = true;
                mFilter.TagsOr = false;
            }
            else if (rTagsOr.IsChecked == true)
            {
                mFilter.TagsAnd = false;
                mFilter.TagsOr = true;
            }
            mFilter.TagsExactMatch = cbTagsExactmatch.IsChecked == true;
            //Links
            mFilter.Links = tbxLinks.Text;
            if (rLinksAnd.IsChecked == true)
            {
                mFilter.LinksAnd = true;
                mFilter.LinksOr = false;
            }
            else if (rLinksOr.IsChecked == true)
            {
                mFilter.LinksAnd = false;
                mFilter.LinksOr = true;
            }
            mFilter.LinksExactMatch = cbLinksExactmatch.IsChecked == true;

            Filter = mFilter;
        }

        private void ColorCell(DataGridCell cell, Enumerations.QueryRowColor color)
        {
            switch (color)
            {
                case Enumerations.QueryRowColor.Orange:
                    cell.Background = new SolidColorBrush(Colors.Orange);
                    break;
                case Enumerations.QueryRowColor.Yellow:
                    cell.Background = new SolidColorBrush(Colors.LemonChiffon);
                    break;
                case Enumerations.QueryRowColor.Gray:
                default:
                    cell.Background = new SolidColorBrush(Colors.Silver);
                    break;
            }
        }
        #endregion

        #region EVENT HANDLERS

        private void Filter_Change(object sender, RoutedEventArgs e)
        {
            try
            {
                FilterChange();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
        private void rdoAll_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton rb = sender as RadioButton;
                switch (rb.Content.ToString())
                {
                    case "Query":
                        SelectTab(TAB_IDX_HOME);
                        RefreshRowColors();
                        UpdateViewStatus("Ready");
                        break;
                    case "Suites":
                        SelectTab(TAB_IDX_SUITES);
                        UpdateViewStatus("Ready");
                        break;
                    case "Tests":
                        SelectTab(TAB_IDX_TESTS);
                        mTestPresenter.UpdateLoadProgress();
                        break;
                    case "Finder":
                        SelectTab(TAB_IDX_FINDER);
                        mFinderPresenter.UpdateLoadProgress();
                        break;
                    case "Admin":
                        SelectTab(TAB_IDX_ADMIN);
                        UpdateViewStatus("Ready");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvwAllSuites_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectSuiteFromTree(sender as TreeView);
        }

        private void dgTestsInSuite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ShowSelectedTestSteps(dgTestsInSuite, dgTestSteps, (bool)chkShowData.IsChecked);
                DlkExecutionQueueRecord targetTest = dgTestsInSuite.SelectedItem as DlkExecutionQueueRecord;
                if (targetTest != null)
                {
                    KwDirItem currentSelectedSuite = tvwAllSuites.SelectedItem as KwDirItem;
                    string suitePath = currentSelectedSuite == null ? string.Empty : currentSelectedSuite.Path;

                    /* Select target test on Test tab */
                    SelectTest(targetTest.fullpath, suitePath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Collapse TreeviewItems from input KwDirItem list recursively
        /// </summary>
        /// <param name="items">KwDirItem list</param>
        private void CollapseTestTreeViewItems(List<KwDirItem> items)
        {
            foreach (KwDirItem itm in items)
            {
                itm.Path = itm.Path; /* No value change, just call setter to notify corresponding datatrigger */
                if (itm is KwFolder)
                {
                    CollapseTestTreeViewItems((itm as KwFolder).DirItems);
                }
            }
        }

        /// <summary>
        /// Collapse TreeviewItems from input KwDirItem list recursively
        /// </summary>
        /// <param name="items">KwDirItem list</param>
        private void CollapseSuiteTreeViewItems(List<KwDirItem> items)
        {
            foreach (KwDirItem itm in items)
            {
                itm.Path = itm.Path; /* No value change, just call setter to notify corresponding datatrigger */
                if (itm is BFFolder)
                {
                    CollapseSuiteTreeViewItems((itm as BFFolder).DirItems);
                }
            }
        }

        /// <summary>
        /// Handler for Selected event of TreeViewItem; Scrolls selected into view
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void ScrollSelectedTreeViewItem(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            if (item != null)
            {
                item.BringIntoView();
                e.Handled = true;
            }
        }

        private void chkShowData_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowSelectedTestSteps(dgTestsInSuite, dgTestSteps, (bool)chkShowData.IsChecked);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkShowData_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowSelectedTestSteps(dgTestsInSuite, dgTestSteps, (bool)chkShowData.IsChecked);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvwAllTests_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectTestFromTree(sender as TreeView);
        }

        private void dgContainingSuites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = sender as DataGrid;

                if (mTestPresenter != null && dg.SelectedItem != null)
                {
                    TLSuite suite = dg.SelectedItem as TLSuite;
                    mTestPresenter.LoadSelectedContainingSuite(suite.path, AllSuites);

                    //if (SelectedSuiteTests.Any())
                    {
                        var target = SelectedSuiteTests.FirstOrDefault(x => x.fullpath == ((KwDirItem)tvwAllTests.SelectedItem).Path);
                        if (target != null)
                        {
                            dgTestsInContainingSuite.SelectedItem = target;
                            dgTestsInContainingSuite.ScrollIntoView(target);
                        }
                        //if (SelectedSuiteTests.FindAll(x => x.fullpath == ((KwDirItem)tvwAllTests.SelectedItem).Path).Any())
                        //{
                        //    dgTestsInContainingSuite.SelectedItem = SelectedSuiteTests.FindAll(x => x.fullpath == ((KwDirItem)tvwAllTests.SelectedItem).Path).First();
                        //    dgTestsInContainingSuite.ScrollIntoView(dgTestsInContainingSuite.SelectedItem);
                        //}
                    }

                    TLSuite targetSuite = dgContainingSuites.SelectedItem as TLSuite;
                    if (targetSuite != null)
                    {
                        KwDirItem currentSelectedTest = tvwAllTests.SelectedItem as KwDirItem;
                        string testPath = currentSelectedTest == null ? string.Empty : currentSelectedTest.Path;

                        /* Select target suite on Suite tab */
                        SelectSuite(targetSuite.path, testPath);
                    }
                }
                else
                {
                    dgTestsInContainingSuite.ItemsSource = null;
                    dgTestsInContainingSuite.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgTestsInContainingSuite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ShowSelectedTestSteps(dgTestsInContainingSuite, dgTestStepsTest, (bool)chkShowDataTest.IsChecked);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkShowDataTest_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ContainingSuites.Any())
                {
                    ShowSelectedTestSteps(dgTestsInContainingSuite, dgTestStepsTest, (bool)chkShowDataTest.IsChecked);
                }
                else
                {
                    ShowSelectedTestSteps(TargetTest, dgTestStepsTest, (bool)chkShowDataTest.IsChecked);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkShowDataTest_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ContainingSuites.Any())
                {
                    ShowSelectedTestSteps(dgTestsInContainingSuite, dgTestStepsTest, (bool)chkShowDataTest.IsChecked);
                }
                else
                {
                    ShowSelectedTestSteps(TargetTest, dgTestStepsTest, (bool)chkShowDataTest.IsChecked);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboScreens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cboControls.Text = string.Empty;
                txtParameters.Clear();
                dgParameters.ItemsSource = null;
                dgParameters.Items.Refresh();
                _ControlItems = null;
                if (cboScreens.SelectedItem != null)
                {
                    mFinderPresenter.LoadControls(cboScreens.SelectedItem.ToString());
                    mFinderPresenter.LoadKeywords(cboScreens.SelectedItem.ToString(), string.Empty);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboControls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cboKeywords.Text = string.Empty;
                txtParameters.Clear();
                dgParameters.ItemsSource = null;
                dgParameters.Items.Refresh();
                if (cboScreens.SelectedItem != null)
                {
                    cboControls.Text = ((ControlItem)cboControls.SelectedItem).ControlName;
                    mFinderPresenter.LoadKeywords(cboScreens.SelectedItem.ToString(), cboControls.Text);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDeleteStep_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTestStepRecord selectedItem = dgFindTest.SelectedItem as DlkTestStepRecord;
                String msg = string.Format("{0}{1} from the list?", DlkUserMessages.ASK_DELETE_STEP, selectedItem.mStepNumber);
                MessageBoxResult result = MessageBox.Show(msg, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    mFinderPresenter.DeleteStep(new List<DlkTestStepRecord> { selectedItem });
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClearSteps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(DlkUserMessages.ASK_CLEAR_TEST, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    mFinderPresenter.ClearSteps();
                    dgFindTest.DataContext = null;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkIncludeParameters_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIncludeParametersCanceled)
                {
                    mIncludeParametersCanceled = false;
                    return;
                }
                if (DlkUserMessages.ShowQuestionYesNo("Changing this setting will clear the current find results. Proceed?")
                    == MessageBoxResult.Yes)
                {
                    IncludeParameters = true;
                    FinderMatches = new List<Match>();
                    tbFindResults.Text = FIND_RESULTS;
                }
                else
                {
                    mIncludeParametersCanceled = true;
                    chkIncludeParameters.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkIncludeParameters_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mIncludeParametersCanceled)
                {
                    mIncludeParametersCanceled = false;
                    return;
                }
                if (DlkUserMessages.ShowQuestionYesNo("Changing this setting will clear the current find results. Proceed?")
                    == MessageBoxResult.Yes)
                {
                    IncludeParameters = false;
                    FinderMatches = new List<Match>();
                    tbFindResults.Text = FIND_RESULTS;
                }
                else
                {
                    mIncludeParametersCanceled = true;
                    chkIncludeParameters.IsChecked = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnFindTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mFinderPresenter.FindTest();
                tbFindResults.Text = string.Format("Find Results ({0})", FinderMatches.Count());
                if (FinderMatches.Count == 0)
                {
                    DlkUserMessages.ShowInfo("0 matches found");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboScreens.Text))
                {
                    return;
                }
                CurrentScreen = cboScreens.Text;
                CurrentControl = cboControls.Text;
                CurrentKeyword = cboKeywords.Text;
                CurrentParameter = GetParametersFromGrid(); //txtParameters.Text; // change when UI changes
                dgParameters.ItemsSource = mKeywordParameters;
                dgParameters.Items.Refresh();

                if (dgFindTest.Items.Count == 0 || dgFindTest.SelectedItem.GetType() != typeof(DlkTestStepRecord))
                {
                    mFinderPresenter.AddStep();
                    dgFindTest.SelectedItem = dgFindTest.Items[dgFindTest.Items.Count - 1];
                }
                else
                {
                    mFinderPresenter.EditStep(((DlkTestStepRecord)dgFindTest.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgFindTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgFindTest.SelectedItem != null)
                {
                    if (dgFindTest.SelectedItem.GetType() != typeof(DlkTestStepRecord))
                    {
                        cboScreens.Text = string.Empty;
                        cboControls.Text = string.Empty;
                        cboKeywords.Text = string.Empty;
                        //txtParameters.Clear();
                    }
                    else
                    {
                        DlkTestStepRecord tsr = dgFindTest.SelectedItem as DlkTestStepRecord;
                        cboScreens.SelectedItem = null; // flush?
                        cboScreens.SelectedItem = tsr.mScreen;
                        if (string.IsNullOrEmpty(tsr.mControl))
                        {
                            cboControls.Text = string.Empty;
                        }
                        else
                        {
                            cboControls.SelectedItem = tsr.mControl;
                        }
                        if (string.IsNullOrEmpty(tsr.mKeyword))
                        {
                            cboKeywords.Text = string.Empty;
                        }
                        else
                        {
                            cboKeywords.SelectedItem = tsr.mKeyword;
                        }
                        //txtParameters.Text = tsr.mParameters == null || tsr.mParameters.Count == 0 ? string.Empty : tsr.mParameters.First();

                        string[] myParams = tsr.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                        mKeywordParameters.Clear();
                        dgParameters.DataContext = null;
                        for (int idx = 0; idx < mParameterList.Count(); idx++)
                        {
                            DlkKeywordParameterRecord kpr = new DlkKeywordParameterRecord(mParameterList[idx].mParameterName, myParams[idx].Replace("^", string.Empty)
                                , idx);
                            kpr.mContains = myParams[idx].Contains("^");
                            mKeywordParameters.Add(kpr);
                        }
                        dgParameters.ItemsSource = mKeywordParameters;
                        dgParameters.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Match selectedTest = ((TextBlock)e.OriginalSource).DataContext as Match;
                TestDetails td = new TestDetails(selectedTest);
                td.Owner = this;
                td.Show();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Folder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Hyperlink link = e.OriginalSource as Hyperlink;
                string targetPath = string.Empty;

                if (((FrameworkElement)sender).DataContext is Match)
                {
                    Match targetMatch = ((FrameworkElement)sender).DataContext as Match;
                    targetPath = targetMatch.Path;
                }
                else if (((FrameworkElement)sender).DataContext is DlkTest)
                {
                    DlkTest targetTest = ((FrameworkElement)sender).DataContext as DlkTest;
                    targetPath = targetTest.mTestPath;
                }
                mFinderPresenter.GoToFolder(targetPath);

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTestLinkRecord link = ((FrameworkElement)sender).DataContext as DlkTestLinkRecord;
                System.Diagnostics.Process.Start(link.LinkPath);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Goto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTest data = ((FrameworkElement)sender).DataContext as DlkTest;
                SelectTest(data.mTestPath, "");
                SelectTab(TAB_IDX_TESTS);
                rdoTests.IsChecked = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void filterInfo_Show(object sender, RoutedEventArgs e)
        {
            try
            {
                Image info = sender as Image;
                if(info.Name == "descriptionInfo")
                {
                    string desccription = "Exact Match = true : \n" +
                                  "Test description should be equal to the input (case-sensitive). \n\n" +
                                  "Exact Match = false : \n" +
                                  "Test description should atleast contains the input  (case-insensitive).";
                    DlkUserMessages.ShowInfo(desccription, "Description Filter Information");
                }
                else if(info.Name == "tagsInfo")
                {
                    string tags = "Exact Match = true : \n" +
                                  "Tag Name should be equal to the item (case-sensitive). \n\n" +
                                  "Exact Match = false : \n" +
                                  "Tag Name should atleast contains the item (case-sensitive). \n\n" +
                                  "AND : \n" +
                                  "All item should match. \n\n" +
                                  "OR : \n" +
                                  "Atleast one item should match.";
                    DlkUserMessages.ShowInfo(tags, "Tags Filter Information");

                }
                else if (info.Name == "linksInfo")
                {
                    string links = "Exact Match = true : \n" +
                                  "Link Name or Link Path should be equal to the item (case-sensitive). \n\n" +
                                  "Exact Match = false : \n" +
                                  "Link Name or Link Path should atleast contains the item (case-sensitive). \n\n" +
                                  "AND : \n" +
                                  "All item should match. \n\n" +
                                  "OR : \n" +
                                  "Atleast one item should match.";
                    DlkUserMessages.ShowInfo(links, "Links Filter Information");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnImportTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mFinderPresenter.ImportTest();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event handler for searching suites after keystroke
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchSuites_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int typeLettersInSuccessionDelay = 2500;
                //timer where filtering occurs only ~3 seconds after the user pressed a key.
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
                    mTxtSearchTimer.Tick += new EventHandler(txtSearchSuites_typingFinished);
                    mTxtSearchTimer.Interval = typeLettersInSuccessionDelay;
                    mTxtSearchTimer.Start();
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
        private void txtSearchSuites_typingFinished(object sender, System.EventArgs e)
        {
            try
            {
                String mTestSuiteFilter = txtSearchSuites.Text;

                if (!string.IsNullOrEmpty(mTestSuiteFilter))
                {
                    // populate filteredSuites
                    mTestSuitesPresenter.SearchSuites(mTestSuiteFilter);
                    if (FilteredSuites.Count > 0)
                    {
                        // if there are hits with search keyword, re-bind datacontext
                        tvwAllSuites.DataContext = FilteredSuites.GroupBy(name => name.Name).Select(i => i.First());
                        txtStatus.Text = String.Format("{0} results found", FilteredSuites.Count);
                    }
                    else
                    {
                        // if no hits, display all suites
                        tvwAllSuites.DataContext = Suites;
                        txtStatus.Text = "Ready";
                    }
                }
                else
                {
                    tvwAllSuites.DataContext = Suites;
                    txtStatus.Text = "Ready";
                }
                // set to null, re-instantiate timer in KeyUp
                if (mTxtSearchTimer != null)
                {
                    mTxtSearchTimer.Stop();
                    mTxtSearchTimer.Dispose();
                    mTxtSearchTimer = null;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtSearchTests_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int typeLettersInSuccessionDelay = 2500;
                //timer where filtering occurs only ~3 seconds after the user pressed a key.
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
                    mTxtSearchTimer.Tick += new EventHandler(txtSearchTests_typingFinished);
                    mTxtSearchTimer.Interval = typeLettersInSuccessionDelay;
                    mTxtSearchTimer.Start();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtSearchTests_typingFinished(object sender, EventArgs e)
        {
            try
            {
                String mTestFilter = txtSearchTests.Text;

                if (!string.IsNullOrEmpty(mTestFilter))
                {
                    // populate filteredSuites
                    mTestPresenter.SearchTests(mTestFilter);
                    if (FilteredTests.Count > 0)
                    {
                        // if there are hits with search keyword, re-bind datacontext
                        tvwAllTests.DataContext = mFilteredTests;
                        txtStatus.Text = String.Format("{0} results found", FilteredTests.Count);
                    }
                    else
                    {
                        // if no hits, display all suites
                        tvwAllTests.DataContext = Tests;
                        txtStatus.Text = "Ready";
                    }
                }
                else
                {
                    tvwAllTests.DataContext = Tests;
                    txtStatus.Text = "Ready";
                }
                // set to null, re-instantiate timer in KeyUp
                if (mTxtSearchTimer != null)
                {
                    mTxtSearchTimer.Stop();
                    mTxtSearchTimer.Dispose();
                    mTxtSearchTimer = null;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void cboKeywords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboKeywords.SelectedValue != null)
                {
                    mKeywordParameters = new ObservableCollection<DlkKeywordParameterRecord>(mParameterList);
                    dgParameters.ItemsSource = mKeywordParameters;
                    dgParameters.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
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
                                tb.SelectAll();
                            }), System.Windows.Threading.DispatcherPriority.Input);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

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

        private void ctxAddTags_Click(object sender, EventArgs e)
        {
            try
            {
                KwDirItem SelectedNode = tvwAllTests.SelectedItem as KwDirItem;
                TagBasket tb = new TagBasket(SelectedNode, Tags, mFinderPresenter.LoadedTests);
                tb.Owner = this;
                tb.ShowDialog();
                if ((bool)tb.DialogResult)
                {
                    RefreshTagList();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }

        }
        private void ctxFindDuplicate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tvwAllTests.SelectedItem != null)
                {
                    List<KwDirItem> SelectedNodes = new List<KwDirItem>(mTreeItemSelectionList.Values);
                    DuplicateFinder df = new DuplicateFinder(SelectedNodes, ObjectStoreFiles, Screens, mFinderPresenter.LoadedTests);
                    df.Owner = this;
                    df.Show();
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ctxEditTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tvwAllTests.SelectedItem != null)
                {
                    KwDirItem SelectedNode = tvwAllTests.SelectedItem as KwDirItem;
                    if (SelectedNode is TestRunner.Common.KwFile)
                    {
                        EditTest(SelectedNode.Path);
                    }
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ctxAddEditTags_Click(object sender, EventArgs e)
        {
            try
            {
                KwDirItem SelectedNode = tvwAllSuites.SelectedItem as KwDirItem;
                DisplayedSuites = mTestPresenter.AllSuites;
                AddEditSuiteTags tb = new AddEditSuiteTags(SelectedNode, Tags, DisplayedSuites);
                tb.Owner = this;
                tb.ShowDialog();
                if ((bool)tb.DialogResult)
                {
                    RefreshTagList();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ctxEditSuite_Click(object sender, EventArgs e)
        {
            try
            {
                if (tvwAllSuites.SelectedItem != null)
                {
                    KwDirItem SelectedNode = tvwAllSuites.SelectedItem as KwDirItem;
                    string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string mRootPath = Directory.GetParent(binDir).FullName;
                    while (new DirectoryInfo(mRootPath).GetDirectories()
                        .Where(x => x.FullName.Contains("Products")).Count() == 0)
                    {
                        mRootPath = Directory.GetParent(mRootPath).FullName;
                    }

                    SuiteEditorForm suiteEditorForm = new SuiteEditorForm();
                    suiteEditorForm.Owner = this;
                    suiteEditorForm.LoadedSuite = SelectedNode.Path;
                    suiteEditorForm.ShowDialog();

                    tvwAllSuites.DataContext = Suites;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void tvwAllTests_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TreeViewItem item = sender as TreeViewItem;
                if (item != null)
                {
                    item.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = dgFindTest.SelectedIndex;
                int lIndex = FinderTest.Count - 1;
                if (index == lIndex || index == -1)
                {
                    return;
                }

                DlkTestStepRecord recToMove = (DlkTestStepRecord)FinderTest[index];

                FinderTest.RemoveAt(index);
                FinderTest.Insert(index + 1, recToMove);
                FinderTest[index + 1].mStepNumber = index + 2;
                FinderTest[index].mStepNumber = index + 1;

                dgFindTest.SelectedIndex = index + 1;
                dgFindTest.ScrollIntoView(FinderTest[index + 1]);

                dgFindTest.DataContext = FinderTest;
                dgFindTest.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = dgFindTest.SelectedIndex;
                if (index == 0 || index == -1)
                {
                    return;
                }

                DlkTestStepRecord recToMove = (DlkTestStepRecord)FinderTest[index];

                FinderTest.RemoveAt(index);
                FinderTest.Insert(index - 1, recToMove);
                FinderTest[index - 1].mStepNumber = index;
                FinderTest[index].mStepNumber = index + 1;

                dgFindTest.SelectedIndex = index - 1;
                dgFindTest.ScrollIntoView(FinderTest[index - 1]);

                dgFindTest.DataContext = FinderTest;
                dgFindTest.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for selection changed event of Test treeview
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void GetSelectedItem(object sender, RoutedEventArgs e)
        {
            try
            {
                TreeViewItem tvi = e.OriginalSource as TreeViewItem;
                KwDirItem test = (KwDirItem)(tvwAllTests).SelectedItem;
                if (mIsLastMouseClickInTestTreeRight && mTreeItemSelectionList.ContainsKey(tvi))
                {
                    return;
                }
                if (IsControlPressed())
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
                ScrollSelectedTreeViewItem(sender, e);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        /// <summary>
        /// Handler for left click preview of test treeview node
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tvwAllTests_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mIsLastMouseClickInTestTreeRight = false;
        }

        /// <summary>
        /// Handler for right click preview of test treeview node
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tvwAllTests_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            mIsLastMouseClickInTestTreeRight = true;
        }

        /// <summary>
        /// Handler for opening event of test treeview node
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tvwAllTests_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            try
            {
                ContextMenu ctx = null;
                StackPanel sp = sender as StackPanel;
                if (sp != null)
                {
                    ctx = sp.ContextMenu;
                }

                if (ctx != null)
                {
                    foreach (object itm in ctx.Items.OfType<MenuItem>())
                    {
                        MenuItem mi = itm as MenuItem;
                        mi.IsEnabled = mi.Header.ToString().Contains("Find Duplicates") ? true : mTreeItemSelectionList.Count <= 1;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Work completed handler for loading worker
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void MBgw_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (mLoadingScreen != null && mLoadingScreen.IsVisible)
            {
                //mLoadingScreen.Close();
                mLoadingScreen.Visibility = Visibility.Hidden;
            }
            mBgw.RunWorkerCompleted -= MBgw_RunWorkerCompleted;
        }

        /// <summary>
        /// Do work handler for loading worker
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void MBgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            LoadResources();
            mBgw.DoWork -= MBgw_DoWork;
        }

        private void dgQuery_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Name" || e.PropertyName == "TotalValue" || e.PropertyName == "Color")
                e.Cancel = true;
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTag newTag = new DlkTag(string.Empty, string.Empty);
                AddEditTag dlg = new AddEditTag(newTag, Tags);
                dlg.Owner = this;
                if ((bool)dlg.ShowDialog())
                {
                    TagsToAdd.Add(newTag);
                    mMainPresenter.AddNewTags();
                    RefreshTagList();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEditTag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTag editTag = Tags.Find(x => x.Name == lbxTags.SelectedItem.ToString());
                AddEditTag dlg = new AddEditTag(editTag, Tags);
                dlg.Owner = this;
                if ((bool)dlg.ShowDialog())
                {
                    mMainPresenter.SaveTags();
                    RefreshTagList();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddEditConflict_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkTag selectedTag = Tags.Find(x => x.Name == lbxTags.SelectedItem.ToString());
                List<DlkTag> conflictTags = new List<DlkTag>(selectedTag.TagConflicts);
                List<DlkTag> tagList = new List<DlkTag>(Tags);
                tagList.Remove(selectedTag); // automatically conflicts itself
                ManageTags dlg = new ManageTags(tagList, conflictTags, true);
                dlg.Title = "Manage Tag Conflicts - " + selectedTag.Name;
                dlg.lblCurrentTags.Content = "Tag Conflicts";
                dlg.btnAddNewTag.Visibility = Visibility.Collapsed;
                dlg.Owner = this;
                DlkTag tagToEdit = Tags.Find(x => x.Id == selectedTag.Id);
                if ((bool)dlg.ShowDialog())
                {
                    // add right listbox tags to conflicts
                    foreach (var item in dlg.lbxCurrentTags.Items)
                    {
                        DlkTag tag = (DlkTag)item;
                        tagToEdit.AddTagConflict(tag.Id);
                        // mutually add conflicts
                        DlkTag mutualTag = Tags.Find(x => x.Id == tag.Id);
                        mutualTag.AddTagConflict(tagToEdit.Id);
                    }

                    //remove left hand listbox tags from conflicts
                    foreach (var item in dlg.lbxAvailableTags.Items)
                    {
                        DlkTag tag = (DlkTag)item;
                        tagToEdit.RemoveTagConflict(tag.Id);
                        // mutually remove conflicts
                        DlkTag mutualTag = Tags.Find(x => x.Id == tag.Id);
                        mutualTag.RemoveTagConflict(tagToEdit.Id);
                    }

                    mMainPresenter.SaveTags();
                    lbxTags.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QueryRow newQr = new QueryRow(dgQuery.Items.Count, "NEW QUERY", Enumerations.QueryRowColor.Gray);

                AddQuery aq = new AddQuery(Enumerations.AddQueryMode.AddNewQuery, Tags, newQr, newQr.TotalColumn, QueryCols, QueryRows);

                aq.Owner = this;
                if ((bool)aq.ShowDialog())
                {
                    QueryRows.Add(newQr);
                    mMainPresenter.SaveQuery();
                    mMainPresenter.LoadQuery();
                    RefreshRowColors();
                    // update view
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnRunQuery_Click(object sender, RoutedEventArgs e)
        {
            mMainPresenter.LoadQuery();
            RefreshRowColors();
        }

        private void ContextMenuStates(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgQuery.SelectedCells.Count > 0)
            {
                int selectedColumnIndex = dgQuery.Columns.IndexOf(dgQuery.SelectedCells.First().Column) - 2;

                mnuMoveLeft.IsEnabled = true;
                mnuMoveRight.IsEnabled = true;
                if (selectedColumnIndex == 0)
                {
                    mnuMoveLeft.IsEnabled = false;
                }
                else if (selectedColumnIndex == QueryCols.Count - 1)
                {
                    mnuMoveRight.IsEnabled = false;
                }
            }

        }

        private void mnuMoveLeft_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgQuery.Columns.IndexOf(dgQuery.SelectedCells.First().Column) - 2;
            if (selectedIndex > 0)
            {
                QueryCol selectedQc = QueryCols[selectedIndex];
                QueryCol previousQc = QueryCols[selectedIndex - 1];
                selectedQc.Index = selectedIndex - 1;
                previousQc.Index = selectedIndex;
                QueryCols[selectedIndex] = previousQc;
                QueryCols[selectedIndex - 1] = selectedQc;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void mnuMoveRight_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = dgQuery.Columns.IndexOf(dgQuery.SelectedCells.First().Column) - 2;
            if (selectedIndex < QueryCols.Count - 1)
            {
                QueryCol selectedQc = QueryCols[selectedIndex];
                QueryCol nextQc = QueryCols[selectedIndex + 1];
                selectedQc.Index = selectedIndex + 1;
                nextQc.Index = selectedIndex;
                QueryCols[selectedIndex] = nextQc;
                QueryCols[selectedIndex + 1] = selectedQc;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void mnuEditRow_Click(object sender, RoutedEventArgs e)
        {
            QueryRow target = QueryRows[dgQuery.Items.IndexOf(dgQuery.CurrentItem)];

            //QueryRow target = QueryRows[dgQuery.SelectedIndex];
            AddQuery aq = new AddQuery(Enumerations.AddQueryMode.EditQuery, Tags, target, target.TotalColumn, QueryCols, QueryRows);

            aq.Owner = this;
            if ((bool)aq.ShowDialog())
            {
                //QueryRows.Add(newQr);
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
                // update view
            }
        }

        private void mnuDeleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (DlkUserMessages.ShowQuestionYesNo("Are you sure you want to delete this row?") == MessageBoxResult.Yes)
            {
                mMainPresenter.DeleteRow(dgQuery.Items.IndexOf(dgQuery.CurrentItem));
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }

        }

        private void btnAddCol_Click(object sender, RoutedEventArgs e)
        {
            //Button btn = sender as Button;
            //btn.ContextMenu.IsOpen = true;
            try
            {
                QueryRow newQr = new QueryRow(dgQuery.Items.Count, "NEW COLUMN", Enumerations.QueryRowColor.Gray);
                QueryCol newQc = new QueryCol(Guid.NewGuid().ToString(), "NEW COLUMN", QueryCols.Count, new List<QueryTag>(), new List<QueryCol>(), Enumerations.QueryOperator.And,
                    Enumerations.QueryTagType.SubQuery, false, 0);


                AddQuery aq = new AddQuery(Enumerations.AddQueryMode.AddQueryModifier, Tags, newQr, newQc, QueryCols);

                aq.Owner = this;
                if ((bool)aq.ShowDialog())
                {
                    if ((bool)aq.rdoColumnOperation.IsChecked)
                    {
                        newQc.Type = Enumerations.QueryTagType.Column;
                        newQc.IsPercentage = (bool)aq.chkPercentage.IsChecked;
                        int decimalplaces;
                        int.TryParse(aq.cboDecimalPlaces.Text, out decimalplaces);
                        newQc.DecimalPlaces = decimalplaces;
                        int column1Index, column2Index;
                        string column1Name = aq.cboColumn1.Text.Substring(4, aq.cboColumn1.Text.Length - 4);
                        string column2Name = aq.cboColumn2.Text.Substring(4, aq.cboColumn2.Text.Length - 4);
                        int.TryParse(aq.cboColumn1.Text.Substring(0, 1), out column1Index);
                        int.TryParse(aq.cboColumn2.Text.Substring(0, 1), out column2Index);
                        string column1Id = "-1";
                        string column2Id = "-1";
                        if (column1Name != "TOTAL")
                        {
                            column1Id = QueryCols.Find(x => x.Name == column1Name).Id;
                        }
                        if (column2Name != "TOTAL")
                        {
                            column2Id = QueryCols.Find(x => x.Name == column2Name).Id;
                        }
                        newQc.QCols.Add(new QueryCol(column1Id, column1Name));
                        newQc.QCols.Add(new QueryCol(column2Id, column2Name));
                    }
                    QueryCols.Add(newQc);
                    mMainPresenter.SaveQuery();
                    mMainPresenter.LoadQuery();
                    RefreshRowColors();
                    // update view
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mnuAddModifier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuAddColOperation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuEditCol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QueryRow newQr = new QueryRow(dgQuery.Items.Count, "NEW COLUMN", Enumerations.QueryRowColor.Gray);
                QueryCol selectedQc = QueryCols[dgQuery.Columns.IndexOf(dgQuery.SelectedCells.First().Column) - 2];
                QueryCol newQc = new QueryCol(selectedQc.Id, selectedQc.Name, selectedQc.Index, selectedQc.QTags, selectedQc.QCols, selectedQc.Operator, selectedQc.Type, selectedQc.IsPercentage, selectedQc.DecimalPlaces);

                int column1Index, column2Index;
                AddQuery aq = new AddQuery(Enumerations.AddQueryMode.EditQueryModifier, Tags, newQr, newQc, QueryCols);
                aq.Title = "Edit Column";
                if (newQc.Type == Enumerations.QueryTagType.Column)
                {
                    aq.rdoColumnOperation.IsChecked = true;
                    column1Index = -1;
                    column2Index = -1;
                    if (newQc.QCols[0].Id != "-1")
                        column1Index = QueryCols.Find(x => x.Id == newQc.QCols[0].Id).Index;
                    if (newQc.QCols[1].Id != "-1")
                        column2Index = QueryCols.Find(x => x.Id == newQc.QCols[1].Id).Index;
                    aq.cboColumn1.SelectedIndex = column1Index + 1;
                    aq.cboColumn2.SelectedIndex = column2Index + 1;
                    aq.cboDecimalPlaces.Text = newQc.DecimalPlaces.ToString();
                    switch (newQc.Operator)
                    {
                        case Enumerations.QueryOperator.Sum:
                            aq.cboColOperators.SelectedIndex = 2;
                            break;
                        case Enumerations.QueryOperator.Difference:
                            aq.cboColOperators.SelectedIndex = 3;
                            break;
                        case Enumerations.QueryOperator.Product:
                            aq.cboColOperators.SelectedIndex = 1;
                            break;
                        case Enumerations.QueryOperator.Quotient:
                            aq.cboColOperators.SelectedIndex = 0;
                            break;
                    }
                    aq.chkPercentage.IsChecked = newQc.IsPercentage;
                }
                aq.Owner = this;
                if ((bool)aq.ShowDialog())
                {
                    if ((bool)aq.rdoColumnOperation.IsChecked)
                    {
                        newQc.Name = aq.txtName.Text;
                        newQc.IsPercentage = (bool)aq.chkPercentage.IsChecked;
                        int decimalplaces;
                        int.TryParse(aq.cboDecimalPlaces.Text, out decimalplaces);
                        newQc.DecimalPlaces = decimalplaces;
                        string column1Name = aq.cboColumn1.Text.Substring(4, aq.cboColumn1.Text.Length - 4);
                        string column2Name = aq.cboColumn2.Text.Substring(4, aq.cboColumn2.Text.Length - 4);
                        newQc.QCols.First().Name = column1Name;
                        newQc.QCols.Last().Name = column2Name;
                        string column1Id = "-1";
                        string column2Id = "-1";
                        if (column1Name != "TOTAL")
                            column1Id = QueryCols.Find(x => x.Name == column1Name).Id;
                        if (column2Name != "TOTAL")
                            column2Id = QueryCols.Find(x => x.Name == column2Name).Id;
                        newQc.QCols.First().Id = column1Id;
                        newQc.QCols.Last().Id = column2Id;
                        int.TryParse(aq.cboColumn1.Text.Substring(0, 1), out column1Index);
                        int.TryParse(aq.cboColumn2.Text.Substring(0, 1), out column2Index);
                        newQc.QCols.First().Index = column1Index - 2;
                        newQc.QCols.Last().Index = column2Index - 2;

                    }
                    //QueryCols.Add(newQc);
                    mMainPresenter.SaveQuery();
                    mMainPresenter.LoadQuery();
                    RefreshRowColors();
                    // update view
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mnuDeleteCol_Click(object sender, RoutedEventArgs e)
        {
            if (DlkUserMessages.ShowQuestionYesNo("Are you sure you want to delete this column?") == MessageBoxResult.Yes)
            {
                mMainPresenter.DeleteColumn(dgQuery.Columns.IndexOf(dgQuery.SelectedCells.First().Column) - 2);
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void cboQueryTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboQueryTypes.SelectedItem != null)
            {
                switch (cboQueryTypes.SelectedItem.ToString())
                {
                    case "Test":
                        CurrentQueryType = Enumerations.QueryType.Test;
                        break;
                    case "Suite":
                        CurrentQueryType = Enumerations.QueryType.Suite;
                        break;
                }
                this.mMainPresenter.LoadQueryOfSelectedType();
                RefreshRowColors();
            }
        }

                private void cboCurrentQuery_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCurrentQuery.SelectedItem != null)
            {
                CurrentQueryPath = System.IO.Path.Combine(DlkEnvironment.mDirFramework, "Library", "Queries", cboCurrentQuery.SelectedItem.ToString());
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
            else if (cboCurrentQuery.Items.Count == 0)
            {
                CurrentQueryPath = string.Empty;
                CurrentQueryDataSource = null;
            }

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

        private void dgQuery_AutoGeneratedColumns(object sender, EventArgs e)
        {

        }

        private void btnNewQuery_Click(object sender, RoutedEventArgs e)
        {
            var inputBox = new DialogBox("Query Name", "Name:", true);
            inputBox.Owner = this;
            inputBox.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            string folder = System.IO.Path.Combine(DlkEnvironment.mDirFramework, "Library", "Queries");

            if (inputBox.ShowDialog() == true)
            {
                string name = inputBox.TextBoxValue;

                if (!name.EndsWith(".xml"))
                {
                    name += ".xml";
                }

                while (System.IO.File.Exists(System.IO.Path.Combine(folder, name))
                    || name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0
                    || string.IsNullOrEmpty(System.IO.Path.GetFileNameWithoutExtension(name)))
                {
                    var secondBox = new DialogBox("Query Name", "Name: [Invalid file name!]", true, System.IO.Path.GetFileNameWithoutExtension(name));
                    secondBox.Owner = this;
                    secondBox.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                    if (secondBox.ShowDialog() == true)
                    {
                        name = secondBox.TextBoxValue;
                        if (!name.EndsWith(".xml"))
                        {
                            name += ".xml";
                        }
                    }
                    else
                    {
                        return;
                    }

                }

                string qPath = System.IO.Path.Combine(folder, name);
                mMainPresenter.NewQuery(qPath);
            }
        }

        private void btnDeleteQuery_Click(object sender, RoutedEventArgs e)
        {
            if (DlkUserMessages.ShowQuestionYesNo("Are you sure you want to delete this query?") == MessageBoxResult.Yes)
            {
                mMainPresenter.DeleteQuery();
                //string folder = System.IO.Path.Combine(DlkEnvironment.mDirFramework, "Library", "Queries");
                //string newSelectedPath = System.IO.Path.Combine(folder, cboCurrentQuery.Text);

            }
        }

        private void btnSaveAs_Click(object sender, RoutedEventArgs e)
        {
            var inputBox = new DialogBox("Query Name", "Name:", true);
            inputBox.Owner = this;
            inputBox.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            string folder = System.IO.Path.Combine(DlkEnvironment.mDirFramework, "Library", "Queries");

            if (inputBox.ShowDialog() == true)
            {
                string name = inputBox.TextBoxValue;

                if (!name.EndsWith(".xml"))
                {
                    name += ".xml";
                }

                while (System.IO.File.Exists(System.IO.Path.Combine(folder, name))
                    || name.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0
                    || string.IsNullOrEmpty(System.IO.Path.GetFileNameWithoutExtension(name)))
                {
                    var secondBox = new DialogBox("Query Name", "Name: [Invalid file name!]", true, System.IO.Path.GetFileNameWithoutExtension(name));
                    secondBox.Owner = this;
                    secondBox.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                    if (secondBox.ShowDialog() == true)
                    {
                        name = secondBox.TextBoxValue;
                        if (!name.EndsWith(".xml"))
                        {
                            name += ".xml";
                        }
                    }
                    else
                    {
                        return;
                    }

                }

                string qPath = System.IO.Path.Combine(folder, name);
                mMainPresenter.SaveAsQuery(qPath);
            }
        }

        private void dgQuery_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject DepObject = (DependencyObject)e.OriginalSource;

            while ((DepObject != null) && !(DepObject is DataGridCell)) //loop until datagrid cell is found
            {
                if (DepObject is ScrollViewer) // datagrid space
                {
                    break;
                }
                DepObject = VisualTreeHelper.GetParent(DepObject);
            }

            if (DepObject == null)
            {
                return;
            }

            if (DepObject is DataGridCell)
            {
                dgQuery.ContextMenu.Visibility = Visibility.Visible;
            }
            else
            {
                dgQuery.ContextMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void lbxTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxTags.SelectedItem != null)
            {
                DlkTag selectedTag = Tags.Find(x => x.Name == lbxTags.SelectedItem.ToString());
                txtID.Text = selectedTag.Id;
                txtName.Text = selectedTag.Name;
                txtDescription.Text = selectedTag.Description;
                lbxTagConflicts.ItemsSource = BuildConflicts(selectedTag.Conflicts).Select(x => x.Name).ToList();
                AdminButtonStates(true);
            }
            else
            {
                txtID.Text = String.Empty;
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lbxTagConflicts.ItemsSource = null;
                AdminButtonStates(false);
            }
        }

        private void mnuMoveUp_Click(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = dgQuery.Items.IndexOf(dgQuery.CurrentItem);
            if (selectedRowIndex > 0)
            {
                QueryRow selectedQr = QueryRows[selectedRowIndex];
                QueryRow previousQr = QueryRows[selectedRowIndex - 1];
                selectedQr.Index = selectedRowIndex - 1;
                previousQr.Index = selectedRowIndex;
                QueryRows[selectedRowIndex] = previousQr;
                QueryRows[selectedRowIndex - 1] = selectedQr;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void mnuMoveDown_Click(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = dgQuery.Items.IndexOf(dgQuery.CurrentItem);
            if (selectedRowIndex < QueryRows.Count - 1)
            {
                QueryRow selectedQr = QueryRows[selectedRowIndex];
                QueryRow nextQr = QueryRows[selectedRowIndex + 1];
                selectedQr.Index = selectedRowIndex + 1;
                nextQr.Index = selectedRowIndex;
                QueryRows[selectedRowIndex] = nextQr;
                QueryRows[selectedRowIndex + 1] = selectedQr;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void mnuGrayColor_Click(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = dgQuery.Items.IndexOf(dgQuery.CurrentItem);
            if (selectedRowIndex >= 0)
            {
                QueryRows[selectedRowIndex].Color = Enumerations.QueryRowColor.Gray;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void mnuYellowColor_Click(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = dgQuery.Items.IndexOf(dgQuery.CurrentItem);
            if (selectedRowIndex >= 0)
            {
                QueryRows[selectedRowIndex].Color = Enumerations.QueryRowColor.Yellow;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void mnuOrangeColor_Click(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = dgQuery.Items.IndexOf(dgQuery.CurrentItem);
            if (selectedRowIndex >= 0)
            {
                QueryRows[selectedRowIndex].Color = Enumerations.QueryRowColor.Orange;
                mMainPresenter.SaveQuery();
                mMainPresenter.LoadQuery();
                RefreshRowColors();
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            LaunchSaveDialog();
            if (mSaveDialogLaunched)
            {
                List<string> colorList = new List<string>();
                foreach (QueryRow row in QueryRows)
                {
                    colorList.Add(row.Color.ToString());
                }
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                dt = ((DataView)dgQuery.ItemsSource).ToTable();
                ds.Tables.Add(dt);
                List<string> names = new List<string>();
                names.Add("Test Library Report");
                DlkExcelHelper.IsColumnHeaderStyled = true;
                DlkExcelHelper.ExportAndSaveToExcel(ds, mSaveDialogFileName, names, colorList);
            }
        }

        private void mnuEditTest_Click(object sender, RoutedEventArgs e)
        {
            Match match = (Match)dgFindResults.SelectedItem;
            if (dgFindResults.SelectedItem != null)
            {
                if (EditTest(match.Path))
                {
                    if (tvwAllTests.SelectedItem != null)
                    {
                        var treeViewItem = tvwAllTests.ItemContainerGenerator.ContainerFromItem(tvwAllTests.SelectedItem) as TreeViewItem;
                        treeViewItem.IsSelected = false;
                        dgTestStepsTest.ItemsSource = null;
                    }
                }
            }
        }

        private void ctxBatchReplace_Click(object sender, RoutedEventArgs e)
        {
            if (tvwAllTests.SelectedItem != null)
            {
                var selected = (KwFolder)tvwAllTests.SelectedItem;
                BatchReplace batchWindow = new BatchReplace(((KwDirItem)tvwAllTests.SelectedItem).Path, selected.DirItems, null);
                batchWindow.Owner = this;
                batchWindow.Show();
            }
        }

        private void ctxBatchReplaceSuite_Click(object sender, RoutedEventArgs e)
        {
            if (tvwAllSuites.SelectedItem != null)
            {
                List<DlkExecutionQueueRecord> testRecs = new List<DlkExecutionQueueRecord>();
                List<KwDirItem> testDirItems = new List<KwDirItem>();

                testRecs = DlkTestSuiteXmlHandler.LoadPartial(((KwDirItem)tvwAllSuites.SelectedItem).Path);
                foreach(DlkExecutionQueueRecord rec in testRecs)
                {
                    var target = SearchForTests._globalCollectionOfTests.Find(x => x.Path == rec.fullpath);
                    var toAdd = new KwFile();
                    toAdd.Name = target.Name;
                    toAdd.Path = target.Path;
                    toAdd.Tags = target.Tags;
                    toAdd.IsSelected = target.IsSelected;
                    toAdd.IsExpanded = target.IsExpanded;
                    testDirItems.Add(toAdd);
                }
                BatchReplace batchWindow = new BatchReplace(((KwDirItem)tvwAllSuites.SelectedItem).Path, testDirItems, testRecs);
                batchWindow.Owner = this;
                batchWindow.Show();
            }
        }

        private void cboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboProduct.SelectedItem != null)
            {
                SelectedProduct = cboProduct.SelectedItem as DlkTargetApplication;
            }
            else
            {
                SelectedProduct = SetTargetProduct(cboProduct.Text);
            }
            if (SelectedProduct.ProductFolder != DlkEnvironment.mProductFolder)
            {
                mDirectoryFilter = mMainPresenter.RetrieveDirectoryFilter(DlkEnvironment.mDirProductsRoot + SelectedProduct.ProductFolder +
                            "\\Framework\\Library\\Directory\\Directory.xml");
            }
            else
            {
                mDirectoryFilter = mMainPresenter.RetrieveDirectoryFilter(DIR_FILE);
            }
            txtSuiteDirectory.Text = mDirectoryFilter;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var browseDialog = new System.Windows.Forms.FolderBrowserDialog();
            browseDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            browseDialog.SelectedPath = mDirectoryFilter;
            browseDialog.Description = "Select Suite Directory";

            if (browseDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSuiteDirectory.Text = browseDialog.SelectedPath;
            }
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            SelectedProduct = cboProduct.SelectedItem as DlkTargetApplication;
            string mDirectoryFilterXML = DlkEnvironment.mDirProductsRoot + SelectedProduct.ProductFolder +
                            "\\Framework\\Library\\Directory\\Directory.xml";

            mMainPresenter.SaveDirectory(txtSuiteDirectory.Text, mDirectoryFilterXML);

            //Reinitialize environment if product was changed
            if (SelectedProduct.ProductFolder != DlkEnvironment.mProductFolder)
            {
                DlkEnvironment.InitializeEnvironment(SelectedProduct.ProductFolder, DlkEnvironment.mRootDir, SelectedProduct.Library);
            }

            //Reload TL
            cboScreens.ItemsSource = null;  //reset the data binding of cboScreen and then rebinding the source through Initalize()
            InitializeComponent();
            Initialize();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                mLoadingScreen.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void chkShowQueryResults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowQueryResult = (bool)((sender as CheckBox).IsChecked);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void QueryGridCell_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                var dgc = sender as DataGridCell;
                var drv = dgc.DataContext as DataRowView;
                var rowname = drv.Row.ItemArray.FirstOrDefault();
                var colindex = dgc.Column.DisplayIndex;
                var colname = colindex == 1 ? "TOTAL" : dgc.Column.SortMemberPath;
                mMainPresenter.UpdateSelectedQueryResult(rowname + Query.STR_QUERYNAME_COLUMNINDEX_DELIMITER + colname);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void dgQueryResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var target = (sender as DataGrid).SelectedItem is null ? null : (sender as DataGrid).SelectedItem as DlkTest;
                SelectedQueryResultTest = target;
                SelectedQueryResultTestStepCount = target == null ? "" : "(" + target.mTestSteps.Count.ToString() + ")"; 
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void grdTestQueryResults_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (rdQueryResults.Height.Value > new GridLength(0, GridUnitType.Pixel).Value)
                {
                    if (chkShowQueryResults.IsChecked == false)
                    {
                        chkShowQueryResults.IsChecked = true;
                    }
                }
                else
                {
                    if (chkShowQueryResults.IsChecked == true)
                    {
                        chkShowQueryResults.IsChecked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void ctxQueryResultsEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgQueryResults.SelectedItem is DlkTest)
                {
                    EditTest((dgQueryResults.SelectedItem as DlkTest).mTestPath);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
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
        #endregion

        #region IMainView
        public List<KwDirItem> Suites
        {
            get
            {
                return mSuites;
            }
            set
            {
                mSuites = value;
            }
        }

        public List<KwDirItem> FilteredSuites
        {
            get
            {
                return mFilteredSuites;
            }
            set
            {
                if (value != null)
                {
                    mFilteredSuites = value;
                }
            }
        }

        public List<KwDirItem> Tests
        {
            get
            {
                return mTests;
            }
            set
            {
                mTests = value;
            }
        }

        public List<DlkObjectStoreFileRecord> ObjectStoreFiles
        {
            get
            {
                return mObjectStoreFiles;
            }
            set
            {
                mObjectStoreFiles = value;
            }
        }

        public List<DlkTest> QueryResultsTest
        {
            get
            {
                return mQueryResultsTest;
            }
            set
            {
                mQueryResultsTest = value;
                dgQueryResults.DataContext = value;
                dgQueryResults.Items.Refresh();
                if (value == null || !value.Any())
                {
                    dgSelectedQueryResultSteps.DataContext = null;
                    dgSelectedQueryResultSteps.Items.Refresh();
                }
            }
        }

        public DlkTest SelectedQueryResultTest
        {
            get
            {
                return mSelectedQueryResultTest;
            }
            set
            {
                mSelectedQueryResultTest = value;
                if (value != null)
                {
                    dgSelectedQueryResultSteps.DataContext = mSelectedQueryResultTest.mTestSteps;
                    dgSelectedQueryResultSteps.Items.Refresh();
                }
            }
        }

        public bool ShowQueryResult
        {
            get
            {
                return mShowQueryResults;
            }
            set
            {
                mShowQueryResults = value;
                if (mShowQueryResults)
                {
                    rdQueryResults.Height = new GridLength(1, GridUnitType.Star);
                    rdQueryTable.Height = new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    rdQueryResults.Height = new GridLength(0, GridUnitType.Pixel);
                }
            }
        }

        public Dictionary<string, List<DlkTest>> AllQueryResultTest
        {
            get
            {
                return mAllQueryResultTest;
            }
            set
            {
                mAllQueryResultTest = value;
            }
        }

        #endregion

        #region ITestSuitesView
        public List<KwDirItem> TestSuiteRoot
        {
            get
            {
                return Suites;
            }
            set
            {
                Suites = value;
            }
        }

        public TLSuite TargetSuite
        {
            get
            {
                return mTargetSuite;
            }
            set
            {
                mTargetSuite = value;
            }
        }

        public DlkExecutionQueueRecord TargetSuiteTests
        {
            get
            {
                return mTargetSuiteTests;
            }
            set
            {
                mTargetSuiteTests = value;
            }
        }

        public List<DlkTestStepRecord> SelectedTestSteps
        {
            get
            {
                return mSelectedTestSteps;
            }
            set
            {
                mSelectedTestSteps = value;
            }
        }
        #endregion

        #region ITestView
        public List<KwDirItem> TestRoot
        {
            get
            {
                return Tests;
            }
            set
            {
                Tests = value;
            }
        }

        public List<KwDirItem> FilteredTests
        {
            get
            {
                return mFilteredTests;
            }
            set
            {
                if (value != null)
                {
                    mFilteredTests = value;
                }
            }
        }

        public List<TLSuite> ContainingSuites
        {
            get
            {
                return mContainingSuites;
            }
            set
            {
                mContainingSuites = value;
                dgContainingSuites.ItemsSource = mContainingSuites;
                dgContainingSuites.Items.Refresh();

            }
        }

        public DlkTest TargetTest
        {
            get
            {
                return mTargetTest;
            }
            set
            {
                mTargetTest = value;
            }
        }

        public List<DlkExecutionQueueRecord> SelectedSuiteTests
        {
            get
            {
                return mSelectedSuiteTests;
            }
            set
            {
                mSelectedSuiteTests = value;
                dgTestsInContainingSuite.ItemsSource = value;
                dgTestsInContainingSuite.Items.Refresh();
            }
        }

        public List<DlkTestStepRecord> SelectedSuiteTestSteps
        {
            get
            {
                return mSelectedSuiteTestSteps;
            }
            set
            {
                mSelectedSuiteTestSteps = value;
            }
        }
        public List<TLSuite> DisplayedSuites
        {
            get
            {
                return mAllSuites;
            }
            set
            {
                mAllSuites = value;
            }
        }
        #endregion

        #region IFinderView
        public List<string> Screens
        {
            get
            {
                return mScreens;
            }
            set
            {
                mScreens = value;
            }
        }

        public List<string> Controls
        {
            get
            {
                return mControls;
            }
            set
            {
                mControls = value;
                cboControls.ItemsSource = ControlItems;
                cboControls.Items.Refresh();
            }
        }

        public List<string> Keywords
        {
            get
            {
                return mKeywords;
            }
            set
            {
                mKeywords = value;
                cboKeywords.ItemsSource = Keywords;
                cboKeywords.Items.Refresh();
            }
        }

        public List<Match> FinderMatches
        {
            get
            {
                return mFinderMatches;
            }
            set
            {
                mFinderMatches = value;
                dgFindResults.ItemsSource = mFinderMatches;
                dgFindResults.Items.Refresh();
            }
        }

        public string CurrentControl
        {
            get
            {
                return mCurrentControl;
            }
            set
            {
                mCurrentControl = value;
            }
        }

        public string CurrentKeyword
        {
            get
            {
                return mCurrentKeyword;
            }
            set
            {
                mCurrentKeyword = value;
            }
        }

        public string CurrentParameter
        {
            get
            {
                return mCurrentParameter;
            }
            set
            {
                mCurrentParameter = value;
            }
        }

        public string CurrentScreen
        {
            get
            {
                return mCurrentScreen;
            }
            set
            {
                mCurrentScreen = value;
            }
        }

        public List<DlkTestStepRecord> FinderTest
        {
            get
            {
                return mFinderTest;
            }
            set
            {
                mFinderTest = value;
                dgFindTest.DataContext = value;
                dgFindTest.Items.Refresh();
            }
        }

        public bool IncludeParameters
        {
            get
            {
                return mIncludeParameters;
            }
            set
            {
                mIncludeParameters = value;
            }
        }

        private List<DlkKeywordParameterRecord> mParameterList
        {
            get
            {
                //List<String> mRes = "";
                List<DlkKeywordParameterRecord> prms = new List<DlkKeywordParameterRecord>();
                if (cboScreens.SelectedItem != null)
                {
                    String mControl = "";
                    if (String.IsNullOrEmpty(cboControls.Text))
                    {
                        mControl = cboScreens.SelectedItem.ToString();
                    }
                    else if (cboControls.Text == "Function" && cboScreens.Text == "Function")
                    {
                        mControl = "Function";
                    }
                    else
                    {
                        mControl = DlkDynamicObjectStoreHandler.GetControlType(cboScreens.SelectedItem.ToString(), cboControls.Text);
                    }
                    for (int idx = 0; idx < DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, cboKeywords.SelectedValue.ToString()).Count; idx++)
                    {
                        prms.Add(new DlkKeywordParameterRecord(DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, cboKeywords.SelectedValue.ToString())[idx], "", idx));
                    }
                }
                return prms;
            }
        }

        public DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
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
        public Filter Filter { get; set; }
        #endregion
    }

    /// <summary>
    /// Converter class for Step string content in Test grid
    /// </summary>
    public class StepsTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string st = value.ToString();

            return st.Substring(st.IndexOf("/") + 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Converter class for Step string content in Test grid
    /// </summary>
    public class QueryRowColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enumerations.ConvertToString((Enumerations.QueryRowColor)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.Parse(typeof(Enumerations.QueryRowColor), value.ToString());
        }
    }

    /// <summary>
    /// Converter class for Step string content in Test grid
    /// </summary>
    public class FinderParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return value.ToString().Replace("^", "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converter class to determine IsExpanded state of Test node from Test path
    /// </summary>
    public class IsExpandedTestInSuiteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ret = false;
            try
            {
                ret = LibraryMainForm.mCurrentSelectedTestInSuite.Contains(value.ToString());
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary>
    /// Converter class to determine IsExpanded state of Suite node from Suite path
    /// </summary>
    public class IsExpandedParentSuiteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool ret = false;
            try
            {
                ret = LibraryMainForm.mCurrentSelectedParentSuiteInTest.Contains(value.ToString());
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    /// <summary>
    /// Class for ControlItem that will hold the Control Name and Type for display in the Control field in the Finder page
    /// </summary>
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


    public static class TestDescriptions
    {
        public static ConcurrentDictionary<string, TestDescriptionItem> Items { get; set; }
    }

    public class TestDescriptionItem
    {
        public TestDescriptionItem(string desc, List<DlkExecutionQueueRecord> eqr)
        {
            description = desc;
            testAsRecordsOnSuites = eqr;
        }
        public string description { get; set; }
        public List<DlkExecutionQueueRecord> testAsRecordsOnSuites { get; set; }
    }

}

public class Filter
{
    public String Description { get; set; }
    public Boolean DescExactMatch { get; set; }
    public String Tags { get; set; }
    public Boolean TagsAnd { get; set; }
    public Boolean TagsOr { get; set; }
    public Boolean TagsExactMatch { get; set; }
    public String Links { get; set; }
    public Boolean LinksAnd { get; set; }
    public Boolean LinksOr { get; set; }
    public Boolean LinksExactMatch { get; set; }
}
