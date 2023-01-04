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

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for CreateExecutionConditionDialog.xaml
    /// </summary>
    public partial class CreateExecutionConditionDialog : Window
    {
        #region PRIVATE MEMBERS

        private List<DlkExecutionQueueRecord> mAllTest = null;

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
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "first test", "preceding test" });
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
                ObservableCollection<string> ret = new ObservableCollection<string>(new string[] { "PASSED", "FAILED", "NOT RUN" });
                return ret;
            }
        }

        #endregion

        #region PUBLIC MEMBERS
        public bool HasChanges = false;
        #endregion

        #region CONSTRUCTORS
        public CreateExecutionConditionDialog(List<DlkExecutionQueueRecord> AllTest, Window Owner)
        {
            InitializeComponent();
            mAllTest = AllTest;
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
                cboExecuteTest.ItemsSource = ExecuteList;
                cboExecuteTest.SelectedIndex = 0;

                cboTest.ItemsSource = TestList;
                cboTest.SelectedIndex = 1;

                cboTestResult.ItemsSource = ResultList;
                cboTestResult.SelectedIndex = 0;

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
            foreach (DlkExecutionQueueRecord eqr in mAllTest)
            {
                if (mAllTest.IndexOf(eqr) > 0)
                {
                    string executeValue = DlkString.ToUpperIndex(cboExecuteTest.Text.ToLower(), 0);
                    string dependencyResult = cboTestResult.Text.ToLower();
                    if (!HasChanges)
                    {
                        int dependencyIndex = mAllTest.IndexOf(eqr) - 1;
                        HasChanges = eqr.dependent != "True" || eqr.execute != executeValue || eqr.executedependencyresult != dependencyResult || HasChanges;
                        HasChanges = cboTest.Text == "first test" ? eqr.executedependency != mAllTest[0] || HasChanges : eqr.executedependency != mAllTest[dependencyIndex] || HasChanges;
                    }
                    eqr.dependent = "True";
                    eqr.execute = executeValue;

                    if (cboTest.Text == "first test")
                    {
                        eqr.executedependency = mAllTest[0];
                    }

                    if (cboTest.Text == "preceding test")
                    {
                        int testDepIdx = mAllTest.IndexOf(eqr) - 1;
                        eqr.executedependency = mAllTest[testDepIdx];
                    }
                    eqr.executedependencyresult = dependencyResult;
                }
                else
                {
                    eqr.execute = "True";
                }
            }
        }

        #endregion
    }
}
