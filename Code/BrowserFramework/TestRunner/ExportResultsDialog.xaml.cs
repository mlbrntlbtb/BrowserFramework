using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ExportResultsDialog.xaml
    /// </summary>
    public partial class ExportResultsDialog : Window
    {
        private List<DlkExecutionQueueRecord> _dlkExecutionQueueRecords = null;
        private List<TestResult> _testResult = null;
        private DlkTest _mTest = null;
        private bool IsTestEditor = false;
        private MainWindow _mOwner;
        private string _mFileDisplayType = "";
        private string[] _emailSubject = { 
            "Test Result: ",
            "Test Queue Summary Results"
        };


        /// <summary>
        /// List of available output options
        /// </summary>
        private enum OutputType
        {
            HTML,
            Excel,
            Email
        }

        /// <summary>
        /// Constructor for dialog called from TestEditor
        /// </summary>
        public ExportResultsDialog(DlkTest mTest)
        {
            InitializeComponent();
            TestEditorForm();
            _mTest = mTest;
            IsTestEditor = true;
            SetDefaults();
        }

        /// <summary>
        /// Constructor for dialog called from MainWindow
        /// </summary>
        public ExportResultsDialog(MainWindow owner, string fileDisplayType)
        {
            InitializeComponent();
            _mOwner = owner;
            _mFileDisplayType = fileDisplayType;
            _dlkExecutionQueueRecords = new List<DlkExecutionQueueRecord>();
            _dlkExecutionQueueRecords = _mOwner.ExecutionQueueRecords.ToList();
            LoadTestResultsList();
            IsTestEditor = false;
            SetDefaults();
        }

        #region EVENT HANDLERS

        private void rdExcel_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = "";
            txtEmail.IsEnabled = false;
        }

        private void rdHtml_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = "";
            txtEmail.IsEnabled = false;
        }

        private void rdEmail_Click(object sender, RoutedEventArgs e)
        {
            txtEmail.IsEnabled = true;
        }

        private void rdTestQueueSummary_Click(object sender, RoutedEventArgs e)
        {
            cboTest.IsEnabled = false;
        }

        private void rdTestResult_Click(object sender, RoutedEventArgs e)
        {
            cboTest.IsEnabled = true;
        }


        private void btnPublish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool bSuccess = false;
               OutputType outputSelected = GetOutputType();
               switch (outputSelected)
                {
                    case OutputType.HTML:
                    case OutputType.Excel:
                        if (LaunchSaveDialog(outputSelected, out string fileName))
                        {
                            if (ExportResultsToFile(outputSelected, fileName))
                            {
                                DlkUserMessages.ShowInfo(DlkUserMessages.INF_SCHEDULER_EXPORT_COMPLETED, "Publish");
                                bSuccess = true;
                            }
                        }
                        break;
                    case OutputType.Email:
                        if (ValidateEmails(txtEmail.Text))
                        {
                            bool ret = false;
                            if (IsTestEditor)
                            {
                                ret = DlkExportResultsHandler.ExportTestResultToEmail(_mTest, txtEmail.Text, $"{_emailSubject[0]}{_mTest.mTestName}");
                                bSuccess = SendEmailNotificationMessage(ret, "Test Result");
                                break;
                            }

                            if(rdTestResult.IsChecked == true)
                            {
                                string resultsFile = _mOwner.GetLogName(cboTest.SelectedIndex);
                                if (File.Exists(resultsFile))
                                {
                                    DlkTest mResult = new DlkTest(resultsFile);
                                    ret = DlkExportResultsHandler.ExportTestResultToEmail(mResult, txtEmail.Text, $"{_emailSubject[0]}{mResult.mTestName}");
                                    bSuccess = SendEmailNotificationMessage(ret, "Test Result");
                                    break;
                                }
                                else
                                {
                                    DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_RESULTSFILE_NOT_FOUND, resultsFile));
                                    break;
                                }
                            }

                            if(rdTestQueueSummary.IsChecked == true)
                            {
                                ret = DlkExportResultsHandler.ExportTestQueueSummaryToEmail(_dlkExecutionQueueRecords, txtEmail.Text, _emailSubject[1], _mFileDisplayType);
                                bSuccess = SendEmailNotificationMessage(ret, "Test Queue Summary Results");
                                break;
                            }
                        }
                        else
                        {
                            MessageBox.Show(DlkUserMessages.ERR_EMAIL_INVALID, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                        break;
                }

                /* Close if publish successful. If errors encountered, allow user to try different export options or cancel */
                if (bSuccess)
                {
                    this.Close();
                }
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
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region PRIVATE METHODS
        private void TestEditorForm()
        {
            spExportOptions.Visibility = Visibility.Collapsed;
            this.Height = 200;
        }

        private void LoadTestResultsList()
        {
            _testResult = new List<TestResult>();
            foreach (DlkExecutionQueueRecord rec in _dlkExecutionQueueRecords)
            {
                _testResult.Add(new TestResult
                {
                    TestId = rec.testrow,
                    TestName = rec.name
                });
            }
            cboTest.DataContext = _testResult;
            if (_testResult.Count > 0)
            {
                cboTest.SelectedIndex = 0;
            }
        }

        private bool SendEmailNotificationMessage(bool Flag, string ResultType)
        {
            if (Flag)
            {
                MessageBox.Show(string.Format(DlkUserMessages.INF_TESTRESULT_EXPORT_EMAIL_SENT, ResultType), "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateEmails(string input)
        {
            String eMails = input;
            string[] eMailAdds = eMails.Split(';');

            if (!eMailAdds.Any(s => String.IsNullOrWhiteSpace(s)))
            {
                foreach (string m in eMailAdds)
                {
                    if (DlkString.IsValidEmail(m) == true)
                        continue;
                    else
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Displays the Save File Dialog before the actual export process is started.
        /// </summary>
        private bool LaunchSaveDialog(OutputType outputType, out string fileName)
        {
            fileName = "";
            string sFilter = "";
            string defaultFilePrefix = (rdTestQueueSummary.IsChecked == true) ? "TestQueueSummary_{0}" : "TestResult_{0}";
            
            switch (outputType)
            {
                case OutputType.HTML:
                    sFilter = "HTML Files|*.html;";
                    break;
                case OutputType.Excel:
                    sFilter = "Excel Files|*.xlsx;";
                    break;
            }
            bool hasSaved = false;

            // Displays a SaveFileDialog so user can select where to save the exported file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = sFilter;
            saveFileDialog.Title = "Save File To Export";
            saveFileDialog.FileName = string.Format(defaultFilePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"));
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)saveFileDialog.ShowDialog())
            {
                fileName = saveFileDialog.FileName.ToString();
                hasSaved = true;
            }
            return hasSaved;
        }

        /// <summary>
        /// Handles the exporting of files based on the chosen output type. Returns true if successful, false if not.
        /// </summary>
        private bool ExportResultsToFile(OutputType outputType, string fileName)
        {
            try
            {
                if (rdTestQueueSummary.IsChecked == true)
                {
                    switch (outputType)
                    {
                        case OutputType.HTML:
                            DlkExportResultsHandler.ExportTestQueueSummaryToFile(_dlkExecutionQueueRecords, fileName, DlkExportResultsHandler.OutputOption.HTML, _mFileDisplayType);
                            break;
                        case OutputType.Excel:
                            DlkExportResultsHandler.ExportTestQueueSummaryToFile(_dlkExecutionQueueRecords, fileName, DlkExportResultsHandler.OutputOption.Excel, _mFileDisplayType);
                            break;
                    }
                }
                if (rdTestResult.IsChecked == true)
                {
                    string resultsFile = _mOwner.GetLogName(cboTest.SelectedIndex);
                    if (File.Exists(resultsFile))
                    {
                        DlkTest mResult = new DlkTest(resultsFile);
                        switch (outputType)
                        {
                            case OutputType.HTML:
                                DlkExportResultsHandler.ExportTestResultToFile(mResult, fileName, DlkExportResultsHandler.OutputOption.HTML);
                                break;
                            case OutputType.Excel:
                                DlkExportResultsHandler.ExportTestResultToFile(mResult, fileName, DlkExportResultsHandler.OutputOption.Excel);
                                break;
                        }
                    }
                    else
                    {
                        DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_RESULTSFILE_NOT_FOUND, resultsFile));
                        return false;
                    }

                }
                if (IsTestEditor)
                {
                    switch (outputType)
                    {
                        case OutputType.HTML:
                            DlkExportResultsHandler.ExportTestResultToFile(_mTest, fileName, DlkExportResultsHandler.OutputOption.HTML);
                            break;
                        case OutputType.Excel:
                            DlkExportResultsHandler.ExportTestResultToFile(_mTest, fileName, DlkExportResultsHandler.OutputOption.Excel);
                            break;
                    }
                }
                return true;
            }
            catch (IOException)
            {
                DlkUserMessages.ShowError(String.Format(DlkUserMessages.ERR_FILE_ACCESS_ANOTHER_PROCESS, fileName));
                return false;
            }
            catch
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_UNEXPECTED_ERROR);
                return false;
            }
        }

        /// <summary>
        /// Set default selected radio buttons to make sure publish will work
        /// </summary>
        private void SetDefaults()
        {
            if (!IsTestEditor) rdTestQueueSummary.IsChecked = true;
            rdHtml.IsChecked = true;
        }

        private OutputType GetOutputType()
        {
            if (rdHtml.IsChecked == true)
            {
                return OutputType.HTML;
            }
            if (rdExcel.IsChecked == true)
            {
                return OutputType.Excel;
            }
            if (rdEmail.IsChecked == true)
            {
                return OutputType.Email;
            }
            //default
            return OutputType.HTML;
        }
        #endregion
    }

    public class TestResult
    {
        public String TestId { get; set; }
        public String TestName { get; set; }
    }
}
