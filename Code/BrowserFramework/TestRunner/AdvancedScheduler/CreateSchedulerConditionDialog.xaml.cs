using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using TestRunner.Common;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.AdvancedScheduler.Model;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for CreateSchedulerConditionDialog.xaml
    /// </summary>
    public partial class CreateSchedulerConditionDialog : Window
    {
        #region PRIVATE MEMBERS

        private List<TestLineupRecord> mAllSuite = null;

        /// <summary>
        /// Item source of Execute dropdown list
        /// </summary>
        private ObservableCollection<string> ExecuteList
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "TRUE", "FALSE" });
                return ret;
            }
        }

        /// <summary>
        /// Item source of Test Order dropdown list
        /// </summary>
        private ObservableCollection<string> TestList
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "first suite", "preceding suite" });
                return ret;
            }
        }

        /// <summary>
        /// Item source of Result dropdown list
        /// </summary>
        private ObservableCollection<string> ResultList
        {
            get
            {
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "PASSED", "FAILED", "SKIPPED" });
                return ret;
            }
        }

        #endregion

        #region CONSTRUCTORS
        public CreateSchedulerConditionDialog(List<TestLineupRecord> AllSuite, Window Owner)
        {
            InitializeComponent();
            mAllSuite = AllSuite;
            this.Owner = Owner;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        }

        #endregion

        #region EVENTS
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ApplyExecutionCondition();
                this.DialogResult = true;
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
                chkEnableCondition.IsChecked = true;
                TestLineupRecord rec = mAllSuite[1]; // second suite - first suite has different properties compared to rest in group
                bool hasCondition = (rec.Dependent == "True");
                chkEnableCondition.IsChecked = hasCondition;
                UpdateControlStates(hasCondition);
                cboExecuteTest.ItemsSource = ExecuteList;
                cboExecuteTest.SelectedIndex = 0;
                cboExecuteTest.SelectedValue = rec.Execute.ToString().ToUpper();

                cboTest.ItemsSource = TestList;
                cboTest.SelectedIndex = 1;
                cboTest.SelectedIndex = rec.ExecuteDependencySuiteRow == "first" ? 0 : 1;
                
                cboTestResult.ItemsSource = ResultList;
                cboTestResult.SelectedIndex = 0;
                cboTestResult.SelectedValue = !String.IsNullOrEmpty(rec.ExecuteDependencyResult) ? rec.ExecuteDependencyResult.ToUpper() : "PASSED";

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Performs the actual setting of the dependency condition for all tests in the test queue.
        /// </summary>
        private void ApplyExecutionCondition()
        {
            foreach (TestLineupRecord eqr in mAllSuite)
            {
                if (mAllSuite.IndexOf(eqr) > 0)
                {
                    if ((bool)chkEnableCondition.IsChecked)
                    {
                        eqr.Dependent = "True";
                        eqr.Execute = Convert.ToBoolean(cboExecuteTest.Text.ToLower());
                        eqr.ExecuteDependencySuiteRow = cboTest.Text == "first suite" ? "first" : "preceding";
                        eqr.ExecuteDependencyResult = cboTestResult.Text.ToLower();
                    }
                    else
                    {
                        eqr.Dependent = "false";
                        eqr.Execute = true;
                        eqr.Enabled = true;
                        eqr.ExecuteDependencySuiteRow = "";
                        eqr.ExecuteDependencyResult = "";
                    }
                        
                }
                else
                {
                    eqr.Enabled = true;
                }
            }
        }

        #endregion

        private void chkEnableCondition_Click(object sender, RoutedEventArgs e)
        {
            UpdateControlStates((bool)chkEnableCondition.IsChecked);
        }

        private void UpdateControlStates(bool state)
        {
            cboTest.IsEnabled = state;
            cboTestResult.IsEnabled = state;
        }
    }
}
