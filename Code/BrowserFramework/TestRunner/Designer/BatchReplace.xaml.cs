using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using TestRunner.Common;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using TestRunner.Designer.View;

namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for BatchReplace.xaml
    /// </summary>
    public partial class BatchReplace : Window, IBatchReplaceView
    {
        #region PRIVATE MEMBERS
        private BatchReplacePresenter mBatchPresenter;
        private DlkTest mTargetTest;
        private String mSelectedTestDirPath = string.Empty;

        private enum NavigationButtonStates
        {
            Default, // All disabled
            AllEnabled,
            BackEnabled,
            ForwardEnabled
        }
        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Initialize critical resources and UI states
        /// </summary>
        private void Initialize()
        {
            /* Load resources */
            mBatchPresenter = new BatchReplacePresenter(this);
            TestsForSaving = new List<DlkTest>();
            UpdatedTests = new List<string>();

            /* Load all tests */
            mBatchPresenter.LoadAllTests();

            /* Set data bindings */
            tvwAllTests.DataContext = Tests;

            /* Select first test node in tree. JPV: Does this work? */
            var firstTest = Tests.FindAll(x => x is KwFile).FirstOrDefault();
            if (firstTest != null)
            {
                firstTest.IsSelected = true;
            }
        }

        /// <summary>
        /// Display test steps in the preview pane based on selected test from treeview
        /// </summary>
        /// <param name="sourceTest">selected test in treeview</param>
        /// <param name="destinationGrid">preview grid</param>
        private void DisplaySelectedTestinfo(DlkTest sourceTest, DataGrid destinationGrid)
        {
            if (sourceTest == null)
            {
                txtDataRowCurrent.Text = string.Empty;
                txtDataRowTotal.Text = string.Empty;
                destinationGrid.ItemsSource = null;
                UpdateNavigationButtons(NavigationButtonStates.Default);
            }
            else
            {
                mBatchPresenter.LoadSelectedTestSteps(sourceTest, true);
                txtDataRowCurrent.Text = sourceTest.mTestInstanceExecuted.ToString();
                txtDataRowTotal.Text = sourceTest.mInstanceCount.ToString();
                destinationGrid.ItemsSource = SelectedTestSteps;
                var state = sourceTest.mInstanceCount == 1 && sourceTest.mTestInstanceExecuted == 1 ? NavigationButtonStates.Default
                    : (sourceTest.mInstanceCount == sourceTest.mTestInstanceExecuted ? NavigationButtonStates.BackEnabled
                    : (sourceTest.mTestInstanceExecuted == 1 ? NavigationButtonStates.ForwardEnabled 
                    : NavigationButtonStates.AllEnabled));
                UpdateNavigationButtons(state);
            }
            /* refresh UI */
            destinationGrid.Items.Refresh();
        }

        /// <summary>
        /// Perform replace to be shown in Preview pane (grid)
        /// </summary>
        /// <param name="TextToSearch">Text to find in the parameter or control field</param>
        /// <param name="NewText">Text to replace</param>
        private void PerformReplaceForPreview()
        {
            try
            {
                mBatchPresenter.UpdateTests();
                DisplaySelectedTestinfo(TargetTest, dgTestSteps);
            }
            catch
            {
                /* Do nothing */
            }
        }

        /// <summary>
        /// Validate replace
        /// </summary>
        /// <returns>True if the required fields are existing</returns>
        private bool ValidateReplace()
        {
            bool ret = false;
            bool bReplaceFieldsError = false;

            // validate
            bReplaceFieldsError = (string.IsNullOrEmpty(txtFindWhat.Text) && string.IsNullOrEmpty(txtReplace.Text));

            if (bReplaceFieldsError)
            {
                DlkUserMessages.ShowError("Please specify a text to find and replace", "Error");
            }
            ret = !bReplaceFieldsError;

            return ret;
        }

        /// <summary>
        /// Handle the change of color for updated fields (Parameter & Control) in the preview pane (grid).
        /// </summary>
        private void DisplaySelectedTestChanges()
        {
            string controlVal = "";
            bool bInitPass = true;
            int updatedLinesPerTest = 0;
            int ctr = 0;
            const int INT_CONTROL_COL = 3;
            const int INT_DATAGRID_COL_HEADERS = 7;
            const int INT_DATAGRID_CTR_COL = 6;

            try
            {
                if (SelectedTestSteps.Count > 0)
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

                        if ((bool)rdoParameter.IsChecked)
                        {
                            if (block.Name == "txtParams" && !String.IsNullOrEmpty(txtReplace.Text))
                            {
                                string[] paramFields = block.Text.Split('|');
                                block.Inlines.Clear();
                                int fieldCount = 1; // counter for number of parameters
                                bool isUpdated = false;
                                foreach (string param in paramFields)
                                {
                                    if (param.Contains(txtReplace.Text)) // data-driven fields
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.Maroon, FontWeight = FontWeights.Bold });
                                        isUpdated = true;
                                    }
                                    else
                                    {
                                        block.Inlines.Add(new Run(param) { Foreground = Brushes.Black, FontWeight = FontWeights.Normal });
                                    }
                                    if (fieldCount < paramFields.Length)
                                    {
                                        block.Inlines.Add(new Run("|") { Foreground = Brushes.Black });
                                    }
                                    fieldCount++;
                                }
                                if (isUpdated)
                                {
                                    updatedLinesPerTest++;
                                }
                            }
                        }

                        else if ((bool)rdoControl.IsChecked)
                        {
                            if (ctr == INT_CONTROL_COL && !String.IsNullOrEmpty(txtReplace.Text))
                            {
                                controlVal = block.Text;
                                if (controlVal.Contains(txtReplace.Text))
                                {
                                    block.Inlines.Clear();
                                    block.Inlines.Add(new Run(controlVal) { Foreground = Brushes.Maroon, FontWeight = FontWeights.Bold });
                                    updatedLinesPerTest++;
                                }
                            }
                            else if (ctr == INT_DATAGRID_CTR_COL)
                            {
                                ctr = 0; // To reset the counter
                            }
                        }
                    }
                    /* Update line per test count label text */
                    lblTestUpdates.Text = "Number of lines replaced per test: " + updatedLinesPerTest.ToString();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Check if a test has been updated after the replace was performed
        /// </summary>
        /// <param name="SourceTest">Target test - test selected in the treeview</param>
        /// <param name="UpdatedItemsPerTest">Dictionary of the test & its updated items</param>
        /// <returns>True if test has updates and False is not</returns>
        private bool TestContainsUpdate(DlkTest SourceTest)
        {
            var updatedKey = SourceTest.mTestPath + ":" + SourceTest.mTestInstanceExecuted.ToString();
            return UpdatedTests.Contains(updatedKey);
        }

        /// <summary>
        /// Update state of test instance navigation buttons
        /// </summary>
        /// <param name="state">State</param>
        private void UpdateNavigationButtons(NavigationButtonStates state)
        {
            if (IsSuite)
            {
                btnDataRowFirst.IsEnabled = false;
                btnDataRowLast.IsEnabled = false;
                btnDataRowNext.IsEnabled = false;
                btnDataRowPrevious.IsEnabled = false;
                return;
            }
            switch (state) // allow navigation only for non-suite
            {
                case NavigationButtonStates.AllEnabled:
                    btnDataRowFirst.IsEnabled = true;
                    btnDataRowLast.IsEnabled = true;
                    btnDataRowNext.IsEnabled = true;
                    btnDataRowPrevious.IsEnabled = true;
                    break;
                case NavigationButtonStates.ForwardEnabled:
                    btnDataRowFirst.IsEnabled = false;
                    btnDataRowLast.IsEnabled = true;
                    btnDataRowNext.IsEnabled = true;
                    btnDataRowPrevious.IsEnabled = false;
                    break;
                case NavigationButtonStates.BackEnabled:
                    btnDataRowFirst.IsEnabled = true;
                    btnDataRowLast.IsEnabled = false;
                    btnDataRowNext.IsEnabled = false;
                    btnDataRowPrevious.IsEnabled = true;
                    break;
                case NavigationButtonStates.Default:
                default:
                    btnDataRowFirst.IsEnabled = false;
                    btnDataRowLast.IsEnabled = false;
                    btnDataRowNext.IsEnabled = false;
                    btnDataRowPrevious.IsEnabled = false;
                    break;
            }
        }
        #endregion

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

        #region PUBLIC PROPERTIES
        public List<KwDirItem> Tests { get; set; }

        public DlkTest TargetTest
        {
            get
            {
                return mTargetTest;
            }
            set
            {
                mTargetTest = value;
                CurrentTarget = value;
                DisplaySelectedTestinfo(mTargetTest, dgTestSteps);
            }
        }

        public static DlkTest CurrentTarget { get; set; }
        public string SelectedTestDirPath { get; set; }
        public List<DlkTestStepRecord> SelectedTestSteps { get; set; }

        public int FilesUpdated { get; set; }

        public List<DlkTest> TestsForSaving { get; set; }

        public List<DlkExecutionQueueRecord> TestsInSuite { get; set; }

        public bool IsSuite
        {
            get
            {
                return !(TestsInSuite is null);
            }
        }

        public Enumerations.BatchReplaceType ReplaceType
        {
            get
            {
                if ((bool)rdoControl.IsChecked)
                {
                    return Enumerations.BatchReplaceType.Control;
                }
                else /* replace with else-if if new types are introduced */
                {
                    return Enumerations.BatchReplaceType.Parameter;
                }
            }
        }

        public bool IsExactMatch
        {
            get
            {
                return (bool)chkExactMatch.IsChecked;
            }
        }

        public string NewText
        {
            get
            {
                return txtReplace.Text;
            }
        }

        public string TextToSearch
        {
            get
            {
                return txtFindWhat.Text;
            }
        }

        public List<string> UpdatedTests { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="Path">Path of test</param>
        /// <param name="TestsToLoad">KwDirItems to display</param>
        /// <param name="TestRecords">ExecutionQueuerecords for Suite batch replace</param>
        public BatchReplace(string Path, List<KwDirItem> TestsToLoad, List<DlkExecutionQueueRecord> TestRecords)
        {
            TestsInSuite = TestRecords;
            Tests = TestsToLoad;
            SelectedTestDirPath = Path;
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region EVENTS
        private void tvwAllTests_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                TreeView tv = sender as TreeView;
                if (mBatchPresenter != null && tv.SelectedItem != null)
                {
                    KwFile file = tv.SelectedItem as KwFile;

                    int instance = 1;
                    try
                    {
                        instance = IsSuite ? int.Parse(TestsInSuite[tv.Items.IndexOf(file)].instance) 
                            : mBatchPresenter.LoadedTests.First(x => x.mTestPath == file.Path).mTestInstanceExecuted;
                    }
                    catch
                    {
                        instance = 1;
                    }

                    mBatchPresenter.LoadTargetTest(file.Path, instance);

                    if (TestContainsUpdate(TargetTest))
                    {
                        Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                         new Action(delegate()
                         {
                             DisplaySelectedTestChanges();
                         }));
                    }
                    else
                    {
                        lblTestUpdates.Text = "Number of lines replaced per test: 0";
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAdvanced_Click(object sender, RoutedEventArgs e)
        {
            // TO DO: Discuss on Advanced button functionality
        }

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            FilesUpdated = 0;
           
            if (ValidateReplace())
            {
                /* Clear UpdatedTests list for every Replace click */
                UpdatedTests.Clear();

                /* Perform Replace */
                PerformReplaceForPreview();

                /* Re-set value of files per test updated label */
                lblTotalUpdates.Text = "Number of files updated: 0";

                if (TestContainsUpdate(TargetTest))
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                       new Action(delegate()
                       {
                           DisplaySelectedTestChanges();
                       }));
                }
                else
                {
                    lblTestUpdates.Text = "Number of lines replaced per test: 0";
                }
            }     
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (TestsForSaving.Any())
            {
                if (mBatchPresenter.SaveTests())
                {
                    DlkUserMessages.ShowInfo(FilesUpdated + DlkUserMessages.INF_FILES_UPDATED + 
                        (TestsForSaving.Count > FilesUpdated ? "\n\n" + (TestsForSaving.Count - FilesUpdated) 
                        + DlkUserMessages.INF_FILES_NOT_SAVED_READONLY : string.Empty));
                    lblTotalUpdates.Text = "Number of files updated: " + FilesUpdated;
                    TestsForSaving.Clear(); // clear to reset updated test list
                }
                else
                {
                    return;
                }
            }
            else
            {
                DlkUserMessages.ShowError("No changes to save.");
                return;
            }
        }

        private void btnDataRowFirst_Click(object sender, RoutedEventArgs e)
        {
            TargetTest.mTestInstanceExecuted = 1;
            DisplaySelectedTestinfo(TargetTest, dgTestSteps);
            if (TestContainsUpdate(TargetTest))
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                   new Action(delegate ()
                   {
                       DisplaySelectedTestChanges();
                   }));
            }
            else
            {
                lblTestUpdates.Text = "Number of lines replaced per test: 0";
            }
        }

        private void btnDataRowPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (TargetTest.mTestInstanceExecuted - 1 > TargetTest.mInstanceCount)
            {
                TargetTest.mTestInstanceExecuted = TargetTest.mInstanceCount;
            }
            else if (TargetTest.mTestInstanceExecuted - 1 < 1)
            {
                TargetTest.mTestInstanceExecuted = 1;
            }
            else
            {
                TargetTest.mTestInstanceExecuted--;
            }
            DisplaySelectedTestinfo(TargetTest, dgTestSteps);

            if (TestContainsUpdate(TargetTest))
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                   new Action(delegate ()
                   {
                       DisplaySelectedTestChanges();
                   }));
            }
            else
            {
                lblTestUpdates.Text = "Number of lines replaced per test: 0";
            }
        }

        private void btnDataRowNext_Click(object sender, RoutedEventArgs e)
        {
            if (TargetTest.mTestInstanceExecuted + 1 > TargetTest.mInstanceCount)
            {
                TargetTest.mTestInstanceExecuted = TargetTest.mInstanceCount;
            }
            else if (TargetTest.mTestInstanceExecuted + 1 < 1)
            {
                TargetTest.mTestInstanceExecuted = 1;
            }
            else
            {
                TargetTest.mTestInstanceExecuted++;
            }
            DisplaySelectedTestinfo(TargetTest, dgTestSteps);

            if (TestContainsUpdate(TargetTest))
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                   new Action(delegate ()
                   {
                       DisplaySelectedTestChanges();
                   }));
            }
            else
            {
                lblTestUpdates.Text = "Number of lines replaced per test: 0";
            }
        }

        private void btnDataRowLast_Click(object sender, RoutedEventArgs e)
        {
            TargetTest.mTestInstanceExecuted = TargetTest.mInstanceCount;
            DisplaySelectedTestinfo(TargetTest, dgTestSteps);
            if (TestContainsUpdate(TargetTest))
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ContextIdle,
                   new Action(delegate ()
                   {
                       DisplaySelectedTestChanges();
                   }));
            }
            else
            {
                lblTestUpdates.Text = "Number of lines replaced per test: 0";
            }
        }
        #endregion
    }

    public class DlkDataParamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ret = value.ToString();

            if (ret.Contains("D{"))
            {
                if (BatchReplace.CurrentTarget != null && BatchReplace.CurrentTarget.Data != null)
                {
                    ret = DlkData.GetValue(ret, BatchReplace.CurrentTarget, false);
                }
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
