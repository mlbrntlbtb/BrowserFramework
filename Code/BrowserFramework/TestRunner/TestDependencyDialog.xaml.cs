using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
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
using CommonLib.DlkSystem;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestDependencyDialog.xaml
    /// </summary>
    public partial class TestDependencyDialog : Window
    {
        #region PRIVATE MEMBERS
        private const string NOT_RUN = "NOT RUN";
        private const string PASSED = "PASSED";
        private const string FAILED = "FAILED";
        private const string EXECUTE = "EXECUTE";
        private const string SKIP = "SKIP";
        private const string DEFAULT = "default";
        private const string CTRL_CURRENT_TEST_CONTEXT = "ctxDisplayNameDependency1";
        private const string CTRL_PRECEDING_TEST_CONTEXT = "ctxDisplayNameDependency2";

        private const int FAILED_INDEX = 1;
        private const int PASSED_INDEX = 0;
        private const int NOTRUN_INDEX = 2;

        private bool mResult = false;
        private DlkExecutionQueueRecord mCurrentTest = null;
        private ObservableCollection<DlkExecutionQueueRecord> mCurrentTestContainer = new ObservableCollection<DlkExecutionQueueRecord>();
        private List<DlkExecutionQueueRecord> mAllTest = null;
        private ObservableCollection<DlkExecutionQueueRecord> mPrecedingTests = new ObservableCollection<DlkExecutionQueueRecord>();
        private TextBlock mDisplayNameTextBlock = new TextBlock();
        private TextBlock mDisplayNameTextBlock2 = new TextBlock();
        #endregion

        #region PUBLIC
        public bool HasChanges = false;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="CurrentTest">Selected test</param>
        /// <param name="AllTest">All tests in test queue</param>
        /// <param name="Owner">Owner window</param>
        public TestDependencyDialog(DlkExecutionQueueRecord CurrentTest, List<DlkExecutionQueueRecord> AllTest, Window Owner)
        {
            InitializeComponent();
            mCurrentTest = CurrentTest;
            mCurrentTestContainer.Add(mCurrentTest);
            mAllTest = AllTest;
            this.Owner = Owner;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            Initialize();
        }

        /// <summary>
        /// List containing all test display type
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
        /// Item source of result dropdown list
        /// </summary>
        public ObservableCollection<string> ResultList
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { PASSED, FAILED, NOT_RUN });
                return ret;
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initialize critical objects
        /// </summary>
        private void Initialize()
        {
            /* Init Current test grid */
            dgCurrentTest.DataContext = mCurrentTestContainer;
            dgCurrentTest.Items.Refresh();

            /* Init result combobox */
            switch(mCurrentTest.executedependencyresult.ToUpper())
            {
                case FAILED:
                    cboDependencyTestResult.SelectedIndex = FAILED_INDEX;
                    break;
                case NOT_RUN:
                    cboDependencyTestResult.SelectedIndex = NOTRUN_INDEX;
                    break;
                case PASSED:
                default:
                    cboDependencyTestResult.SelectedIndex = PASSED_INDEX;
                    break;
            }

            /* Init other UI values */
            rdoYes.DataContext = mCurrentTest;
            rdoNo.DataContext = mCurrentTest;
            chkExecuteDependency.DataContext = mCurrentTest;
            grpExecutionCondition.DataContext = mCurrentTest;
            cboDependencyTestResult.DataContext = this;

            /* Init Preceding tests srid selected item */
            if (mCurrentTest.executedependency != null && mPrecedingTests.ToList().FindAll(x => x.identifier == mCurrentTest.executedependency.identifier).Count() > 0)
            {
                dgPrecedingTests.SelectedItem = mPrecedingTests.ToList().FindAll(x => x.identifier == mCurrentTest.executedependency.identifier).First();
            }

            /* Init context menus */
            InitializeContextMenus();
        }

        /// <summary>
        /// Initializes context menus in grid
        /// </summary>
        private void InitializeContextMenus()
        {
            ContextMenu displayName1 = ((ContextMenu)FindResource(CTRL_CURRENT_TEST_CONTEXT));
            ContextMenu displayName2 = ((ContextMenu)FindResource(CTRL_PRECEDING_TEST_CONTEXT));

            displayName1.Items.Clear();
            displayName2.Items.Clear();

            foreach (string itm in DisplayNames)
            {
                MenuItem mnu = new MenuItem();
                mnu.Header = itm;
                mnu.Click += new RoutedEventHandler(CurrentTestDisplayNameContextMenuClicked);
                displayName1.Items.Add(mnu);
                mnu.IsChecked = mnu.Header.ToString().Contains(DEFAULT);
            }

            foreach (string itm in DisplayNames)
            {
                MenuItem mnu = new MenuItem();
                mnu.Header = itm;
                mnu.Click += new RoutedEventHandler(PrecedingTestsDisplayNameContextMenuClicked);
                displayName2.Items.Add(mnu);
                mnu.IsChecked = mnu.Header.ToString().Contains(DEFAULT);
            }
        }

        /// <summary>
        /// Initialize grid containing preceding tests
        /// </summary>
        private void InitializePrecedingTests()
        {
            if (mPrecedingTests == null)
            {
                mPrecedingTests = new ObservableCollection<DlkExecutionQueueRecord>();
            }

            mPrecedingTests.Clear();
            foreach (DlkExecutionQueueRecord tst in mAllTest)
            {
                if (int.Parse(tst.testrow) < int.Parse(mCurrentTest.testrow))
                {
                    mPrecedingTests.Add(tst);
                }
                else
                {
                    break;
                }
            }

            dgPrecedingTests.DataContext = mPrecedingTests;
            dgPrecedingTests.Items.Refresh();
            txtSelectedDependency.DataContext = dgPrecedingTests.SelectedItem;
            txtTestInstance.DataContext = dgPrecedingTests.SelectedItem;
            txtTestNumber.DataContext = dgPrecedingTests.SelectedItem;

            if (mCurrentTest.executedependency != null && mPrecedingTests.ToList().FindAll(x => x.identifier == mCurrentTest.executedependency.identifier).Count() > 0)
            {
                dgPrecedingTests.SelectedItem = mPrecedingTests.ToList().FindAll(x => x.identifier == mCurrentTest.executedependency.identifier).First();
            }
        }

        /// <summary>
        /// Save form updates
        /// </summary>
        /// <returns>True if Save successful, False otherwise</returns>
        private bool SaveExecutionQueueRecord()
        {
            string executeValue = (bool)rdoYes.IsChecked ? bool.TrueString : bool.FalseString;
            HasChanges = executeValue != mCurrentTest.execute;
            mCurrentTest.execute = executeValue;
            mCurrentTest.dependent = (bool)chkExecuteDependency.IsChecked ? bool.TrueString : bool.FalseString;
            if ((bool)chkExecuteDependency.IsChecked)
            {
                if (dgPrecedingTests.SelectedItem == null)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_SELECT_ACTIVATOR_TEST);
                    return false;
                }
                DlkExecutionQueueRecord eqr = dgPrecedingTests.SelectedItem as DlkExecutionQueueRecord;
                string dependencyResult = cboDependencyTestResult.Text.ToLower();
                HasChanges = mCurrentTest.executedependency != eqr || mCurrentTest.executedependencyresult != dependencyResult || HasChanges;
                mCurrentTest.executedependency = eqr;
                mCurrentTest.executedependencyresult = dependencyResult;
            }
            else
            {
                HasChanges = mCurrentTest.executedependency != null || mCurrentTest.executedependencyresult != String.Empty || HasChanges;
                mCurrentTest.executedependency = null;
                mCurrentTest.executedependencyresult = string.Empty;
            }
            return true;
        }
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// Display display name context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowPrecedingTestsDisplayNameContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgPrecedingTests.Items.Count > 0)
                {
                    this.dgPrecedingTests.CancelEdit();
                    ((ContextMenu)FindResource(CTRL_PRECEDING_TEST_CONTEXT)).IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Display display name context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowCurrentTestDisplayNameContextMenu(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgCurrentTest.Items.Count > 0)
                {
                    this.dgCurrentTest.CancelEdit();
                    ((ContextMenu)FindResource(CTRL_CURRENT_TEST_CONTEXT)).IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Loaded event of txtDisplayNameHeaderCurrent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDisplayNameHeaderCurrent_Loaded(object sender, RoutedEventArgs e)
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
        /// Loaded event of DisplayNameHeaderPreceding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDisplayNameHeaderPreceding_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mDisplayNameTextBlock2 = sender as TextBlock;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Checked event of chkExecuteDependency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkExecuteDependency_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgPrecedingTests.IsEnabled = true;
                InitializePrecedingTests();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Unchecked event of chkExecuteDependency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkExecuteDependency_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgPrecedingTests.IsEnabled = false;
                mPrecedingTests.Clear();
                dgPrecedingTests.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Selection changed event of dgPrecedingTests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPrecedingTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid senderD = sender as DataGrid;
                DlkExecutionQueueRecord eqr = senderD.SelectedItem as DlkExecutionQueueRecord;
                txtSelectedDependency.DataContext = eqr;
                txtTestInstance.DataContext = eqr;
                txtTestNumber.DataContext = eqr;
            }
            catch
            {
                txtSelectedDependency.Clear();
            }
        }

        /// <summary>
        /// Event hanlder for Display Name context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentTestDisplayNameContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgCurrentTest.CancelEdit();
                foreach (MenuItem itm in ((ContextMenu)FindResource(CTRL_CURRENT_TEST_CONTEXT)).Items)
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
                CurrentTestDisplayName.Binding = bnd;
                dgCurrentTest.Items.Refresh();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Event hanlder for Display Name context menu item clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrecedingTestsDisplayNameContextMenuClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                dgCurrentTest.CancelEdit();
                foreach (MenuItem itm in ((ContextMenu)FindResource(CTRL_PRECEDING_TEST_CONTEXT)).Items)
                {
                    itm.IsChecked = false;
                }
                string displayName = ((MenuItem)sender).Header.ToString().ToLower();
                var bnd = new Binding("file");
                if (displayName.Contains("test"))
                {
                    bnd = new Binding("name");
                    mDisplayNameTextBlock2.Text = "Test Name";
                    txtFileName.Text = "Test Name:";
                }
                else if (displayName.Contains("path"))
                {
                    bnd = new Binding("fullpath");
                    mDisplayNameTextBlock2.Text = "Full Path";
                    txtFileName.Text = "Full Path:";
                }
                else
                {
                    mDisplayNameTextBlock2.Text = "File Name";
                    txtFileName.Text = "File Name:";
                }
                MenuItem mnu = sender as MenuItem;
                mnu.IsChecked = true;
                bnd.Mode = BindingMode.OneWay;
                PrecedingTestDisplayName.Binding = bnd;
                dgPrecedingTests.Items.Refresh();
                txtSelectedDependency.SetBinding(TextBox.TextProperty, bnd);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Click handler of btnOK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SaveExecutionQueueRecord())
                {
                    mResult = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Click handler for Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Closing handler for Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                this.DialogResult = mResult;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
