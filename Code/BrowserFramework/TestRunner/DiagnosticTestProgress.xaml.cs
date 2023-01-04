using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using TRDiagnosticsCore.Utility;
using TRDiagnosticsCore;
using System.Windows.Controls;
using TestRunner.Common;
using CommonLib.DlkSystem;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for DiagnosticTestProgress.xaml
    /// </summary>
    public partial class DiagnosticTestProgress : Window,INotifyPropertyChanged
    {
        #region Private
        private readonly List<DiagnosticTestProgress> _execTest;
        private List<TestRecord> testRecords;
        private DlkBackgroundWorkerWithAbort bw;
        private TRDiagnosticsCore.DiagnosticTest mDiagnosticTest;
        private string trPath;
        private string mCurrentTestName;
        private bool mShowReport;
        private string mCurrentTestProcessing;
        private int mTotalTestCount;
        private readonly int mProgressMaxValue;
        private int mTotalProgressValue;
        private string mDiagnosticTestOutputLog;
        private string mLogPath;
        private string _dirOptions;

        #endregion

        #region Properties
        public string TestName { get; set; }

        public string DirOption { get; set; }

        public bool IsSelected { get; set; }

        public string CurrentTestName
        {
            get
            {
                return mCurrentTestName;
            }
            private set
            {
                mCurrentTestName = value;
                OnPropertyChanged("CurrentTestName");
            }
        }

        public bool ShowReport
        {
            get
            {
                return mShowReport;
            }
            private set
            {
                mShowReport = value;
                OnPropertyChanged("ShowReport");
            }
        }

        public string CurrentTestProcessing
        {
            get
            {
                return mCurrentTestProcessing;
            }
            private set
            {
                mCurrentTestProcessing = value;
                OnPropertyChanged("CurrentTestProcessing");
            }
        }

        public string DiagnosticTestOutputLog
        {
            get
            {
                return mDiagnosticTestOutputLog.ToString();
            }
            private set
            {
                mDiagnosticTestOutputLog = value;
                OnPropertyChanged("DiagnosticTestOutputLog");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public DiagnosticTestProgress(List<DiagnosticTestProgress> Test, string DirOptions, string TRPath)
        {
            InitializeComponent();
            trPath = TRPath;
            Initialize();

            _execTest = Test.Where(_test => _test.IsSelected).ToList();
            _dirOptions = DirOptions;
            mProgressMaxValue = 100 / _execTest.Count;
            mTotalProgressValue = mProgressMaxValue;
            ExecuteTest();
        }

        public DiagnosticTestProgress() { }

        private void Initialize()
        {
            testRecords = new List<TestRecord>();
            DiagnosticTestOutputLog = "";
            tbToolPath.Text = trPath;
            spExecute.Visibility = Visibility.Visible;
            spFinish.Visibility = Visibility.Hidden;
            btnCancel.Content = "Cancel";
            lblFinish.Content = "";
            Closing += DiagnosticTestProgress_Closing;
        }

        /// <summary>
        /// Handle closing window while diagnostic test is running
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void DiagnosticTestProgress_Closing(object sender, CancelEventArgs e)
        {
            if (bw.IsBusy)
            {
                DlkUserMessages.ShowWarning(DlkUserMessages.WRN_DIAGNOSTIC_TEST_CLOSING);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Execute diagnostic test for the list of selected tests
        /// </summary>
        private void ExecuteTest()
        {
            lblTestExecuted.DataContext = this;
            lblTestProgress.DataContext = this;
            txtOutputLog.DataContext = this;

            UILogger.TRDiagnosticLogHandler += UILogger_TRDiagnosticLogHandler;
            bw = new DlkBackgroundWorkerWithAbort();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.WorkerReportsProgress = true;
            
            if(!bw.IsBusy)
                bw.RunWorkerAsync();
        }

        /// <summary>
        /// Diagnostic test log handler
        /// </summary>
        /// <param name="type">Message Log Type</param>
        /// <param name="message">message details</param>
        private void UILogger_TRDiagnosticLogHandler(Logger.MessageType type, string message)
        {
            void writeLog(string text) => DiagnosticTestOutputLog += ((DiagnosticTestOutputLog.Length > 0 ? Environment.NewLine : "") + text);

            switch (type)
            {
                case Logger.MessageType.COUNTER:
                    bw.ReportProgress(UILogger.CurrentTestCount);
                    CurrentTestProcessing = message;
                    break;
                case Logger.MessageType.NULL:
                        mTotalTestCount = mDiagnosticTest.TotalTestCount;
                    writeLog(message);
                    break;
                case Logger.MessageType.INFO:                    
                case Logger.MessageType.SUCCESS:
                case Logger.MessageType.WARNING:
                case Logger.MessageType.ERROR:
                case Logger.MessageType.FILEPROGRESS:
                    if((type == Logger.MessageType.INFO && message.ToLower().Contains("checking")) || type == Logger.MessageType.FILEPROGRESS)
                        CurrentTestProcessing = message;

                    if (type != Logger.MessageType.FILEPROGRESS)
                    {
                        var lastLog = mDiagnosticTest.DiagnosticLogger.Logs.Last();
                        writeLog($"[{lastLog.MessageType}] {lastLog.MessageDetails}");
                    }
                    break;
            }            
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            try
            {
                foreach (DiagnosticTestProgress test in _execTest)
                {
                    CurrentTestName = test.TestName;
                    mDiagnosticTest = GetTestType(test.TestName);
                    mDiagnosticTest.Run(trPath, false, _dirOptions);
                    mTotalProgressValue += mProgressMaxValue;
                    SaveTestLog(i);
                    i++;
                }
            }
            catch (ThreadAbortException) 
            {
                SaveTestLog(i);
                e.Cancel = true;
                throw; // throw to base class for proper disposing of thread.
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double newProgressValue = (Convert.ToDouble(e.ProgressPercentage) / Convert.ToDouble(mTotalTestCount) * mProgressMaxValue);
            if (mProgressMaxValue == mTotalProgressValue)
            {                
                pbExecution.Value = newProgressValue;
            }
            else
            {
                pbExecution.Value = (mTotalProgressValue - mProgressMaxValue) + newProgressValue;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(mLogPath))
                    DiagnosticTestOutputLog += $"\n\n{UILogger.HEADER}\nDiagnostic test {(e.Cancelled ? "canceled" : "completed")}.\nDiagnostic test logs saved at: <{mLogPath}>\n{UILogger.HEADER}";

                lblFinish.Content = e.Cancelled ? "Diagnostic Test Canceled" : "Diagnostic Test Finished";
                string htmlFileOutput = SaveLogsHandler.GetLogFilePath(mLogPath, true);
                SaveLogsHandler.GenerateHTMLReport(htmlFileOutput, testRecords);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            finally
            {
                UILogger.TRDiagnosticLogHandler -= UILogger_TRDiagnosticLogHandler;
                spExecute.Visibility = Visibility.Hidden;
                spFinish.Visibility = Visibility.Visible;
                btnCancel.Content = "Close";
            }
        }

        /// <summary>
        /// Scroll to end while populating log report
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event argument</param>
        private void txtOutputLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.CaretIndex = tb.Text.Length;
            tb.ScrollToEnd();
        }
        
        /// <summary>
        /// Cancel diagnostic test
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event argument</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (btnCancel.Content.ToString() == "Cancel")
            {
                mDiagnosticTest.TestCancelled = true;
                bw.Abort();
                bw.Dispose();
                spExecute.Visibility = Visibility.Hidden;
                spFinish.Visibility = Visibility.Visible;
                btnCancel.Content = "Close";
            }
            else if (btnCancel.Content.ToString() == "Close")
            {
                this.Close();
            }
        }

        /// <summary>
        /// View diagnostic html log report
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">event argument</param>
        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            if (!SaveLogsHandler.OpenHTMLFile())
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_DIAGNOSTIC_HTML_REPORT_NOTFOUND);
            }
        }
        
        /// <summary>
        /// Get instance for selected diagnostic test type
        /// </summary>
        /// <param name="testName">Defined test name</param>
        /// <returns>Diagnostic test class</returns>
        private TRDiagnosticsCore.DiagnosticTest GetTestType(string testName)
        {
            switch (testName)
            {
                case "Minimum System Requirements":
                    return new TRDiagnosticsCore.Tests.SystemReqTest();
                case "Core Directories and Files":
                    return new TRDiagnosticsCore.Tests.CoreFilesTest();
                case "Installed Browser and WebDriver Version":
                    return new TRDiagnosticsCore.Tests.BrowserTest();
                case "Shared Folder and Permissions":
                    return new TRDiagnosticsCore.Tests.ShareFolderTest();
                default:
                    throw new Exception($"Test {testName} not supported.");
            }
        }

        private void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        /// <summary>
        /// Handle error while saving log and identifies whether to append existing logfile or not
        /// </summary>
        /// <param name="testLogIndex"></param>
        /// <returns>log full path</returns>
        private void SaveTestLog(int testLogIndex)
        {
            SaveLog(testLogIndex != 0, out string saveLogError);

            if (!string.IsNullOrEmpty(saveLogError))
                DiagnosticTestOutputLog += $"\n\n{UILogger.HEADER}\nSomething went wrong while saving diagnostic logs. {_execTest[testLogIndex]} Diagnostics logs not saved.\n{UILogger.HEADER}\n";
        }

        /// <summary>
        /// Save log file
        /// </summary>
        /// <param name="appendLogs">true=append | else not</param>
        /// <param name="errorMessage">returned error</param>
        /// <returns>log path</returns>
        private void SaveLog(bool appendLogs,out string errorMessage)
        {
            try
            {
                errorMessage = "";
                testRecords.Add(new TestRecord(mDiagnosticTest.TestName, mDiagnosticTest.DiagnosticLogger.Logs, mDiagnosticTest.Recommendations));
                SaveLogsHandler.SaveLogs(trPath, mDiagnosticTest.DiagnosticLogger.Logs, mDiagnosticTest.Recommendations, mDiagnosticTest.TestName,
                    appendLogs, out mLogPath, out bool isLogsAppended);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                mLogPath = "";
            }
        }
    }
}
