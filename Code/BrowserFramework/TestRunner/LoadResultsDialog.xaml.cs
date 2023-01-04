using System;
using System.Windows;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for LoadResultsDialog.xaml
    /// </summary>
    public partial class LoadResultsDialog : Window
    {
        public String mSelectedExecutionDate = "";
        public String mSuitePath = "";

        public String mSelectedLogFolder
        {
            get
            {
                String ret = "";
                String mExecDate = mSelectedExecutionDate.Replace("-", "").Replace(":", "").Replace(" ", "");
                ret = System.IO.Path.Combine(DlkEnvironment.mDirSuiteResults, System.IO.Path.GetFileNameWithoutExtension(mSuitePath), mExecDate);
                return ret;
            }
        }

        public LoadResultsDialog(String SuitePath)
        {
            InitializeComponent();
            cmbSuiteExecutions.DataContext = DlkTestSuiteResultsFileHandler.GetExecutionDatesForSuite(SuitePath);
            mSelectedExecutionDate = "";
            mSuitePath = SuitePath;
        }

        public void LoadResultsDialog_OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbSuiteExecutions.Items.Count > 0)
                {
                    cmbSuiteExecutions.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbSuiteExecutions.Text == "")
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_EXECUTION_DATE);
                    return;
                }
                mSelectedExecutionDate = cmbSuiteExecutions.Text;
                this.DialogResult = true;
                Close();
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
                Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
