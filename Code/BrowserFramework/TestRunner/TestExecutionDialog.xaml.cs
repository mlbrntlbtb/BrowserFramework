using CommonLib.DlkSystem;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using TestRunner.Common;


namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestExecutionDialog.xaml
    /// </summary>
    public partial class TestExecutionDialog : Window
    {

        #region DECLARATIONS
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private static int mTestsInSuite;
        private static int mStepInTest;
        private static int mTestCounter;
        private const int INT_MIN_DELAY = 1000;

        private BackgroundWorker mMyWorker = new BackgroundWorker();

        public static Boolean bCanceled = false;
        public static Boolean bCanceledAll = false;

        #endregion 

        #region PROPERTIES

        public bool IsRunningSuite { get; set; }

        #endregion

        #region CONSTRUCTOR
        
        /// <summary>
        /// Initializes Test Execution Dialog for Test Suites/Ad Hoc Runs
        /// </summary>
        /// <param name="TestPath">Test or Suite Path</param>
        /// <param name="IsSuite">Denotes if the TestPath parameter is a suite or not</param>
        /// <param name="TestsToExecute">Number of Test in a Suite</param>
        /// <param name="StepsToExecute">Number os Steps in a Test</param>
        public TestExecutionDialog(String TestPath, bool IsSuite = false, int TestsToExecute = 0, int StepsToExecute = 0)
        {
            InitializeComponent();
            Initialize();
            IsRunningSuite = IsSuite;

            if (IsRunningSuite)
            {
                mTestsInSuite = TestsToExecute;
                if (TestsToExecute > 1)
                {
                    this.Title = "Executing Test " + DlkTestRunnerApi.CurrentRunningTest + " of " + mTestsInSuite + ":";
                    txtTestStatus.Text = string.IsNullOrEmpty(TestPath) ? "Running tests in queue..." : TestPath;
                }
                else
                {
                    this.Title = "Executing Test " + DlkTestRunnerApi.CurrentRunningTest + " of " + mTestsInSuite + ":";
                    txtTestStatus.Text = "Executing test...";
                }
            }
            else
            {
			    txtTestStatus.Text = TestPath;
                mStepInTest = StepsToExecute;
                btnCancelAllTests.Visibility = System.Windows.Visibility.Collapsed;
                Title = "Executing Step " + DlkTestRunnerApi.CurrentRunningStep + " of " + mStepInTest;
            }
        }

        #endregion

        #region EVENTS
        private void btnCancelCurrentTest_Click(object sender, RoutedEventArgs e)
        {
            try   
            {
                /* Pre-cancellation UI updates */
                txtTestStatus.Text = "Canceling test...";
                btnCancelCurrentTest.IsEnabled = false;
                btnCancelAllTests.IsEnabled = false;
                this.Topmost = true;

                /* Set cancellation flag to true */
                DlkTestRunnerApi.StopCurrentExecution();
            }
            catch
            {
                // do nothing
            }
        }

        private void btnCancelAllTests_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Pre-cancellation UI updates */
                txtTestStatus.Text = "Canceling test suite...";
                btnCancelCurrentTest.IsEnabled = false;
                btnCancelAllTests.IsEnabled = false;
                this.Topmost = true;

                /* Set abortion flag to true */
                DlkTestRunnerApi.StopExecution();
            }
            catch
            {
                // do nothing
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

                mMyWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mMyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                int i = 0;
                int prog = 0;
                decimal computeProg;

                if (IsRunningSuite)
                {
                    mTestCounter = DlkTestRunnerApi.mTestsRan;
                    computeProg = (Convert.ToDecimal(mTestCounter) / Convert.ToDecimal(mTestsInSuite)) * 100;
                }
                else
                {
                    mTestCounter = DlkTestRunnerApi.CurrentRunningStep;
                    computeProg = (Convert.ToDecimal(mTestCounter) / Convert.ToDecimal(mStepInTest)) * 100;
                }

                prog = Convert.ToInt32(computeProg);
                mMyWorker.ReportProgress(prog);

                while (DlkTestRunnerApi.mExecutionStatus == "running")
                {
                    Thread.Sleep(INT_MIN_DELAY);
                    if (DlkTestRunnerApi.mTestsRan != i)
                    {
                        mMyWorker.ReportProgress(prog);
                        prog += prog;
                        i = DlkTestRunnerApi.mTestsRan;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mMyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int iMove = 10;
                if (e.ProgressPercentage > 95)
                {
                    iMove = 2;
                }

                DisplayProgressBarUpdate(iMove, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mMyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                prgBarStatus.Value = 100;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region METHODS
        private void Initialize()
        {
            double dScreenHeight = SystemParameters.FullPrimaryScreenHeight;
            double dScreenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = dScreenHeight - this.Height - 5;
            this.Left = dScreenWidth - this.Width - 5;
            DisplayProgressBarUpdate(1, 0);

            mMyWorker.DoWork += new DoWorkEventHandler(mMyWorker_DoWork);
            mMyWorker.ProgressChanged += new ProgressChangedEventHandler(mMyWorker_ProgressChanged);
            mMyWorker.WorkerReportsProgress = true;
            mMyWorker.RunWorkerCompleted += mMyWorker_RunWorkerCompleted;
        }

        public void DisplayProgressBarUpdate(int iDurationSecondsToPerformAnimation, int iPercentToMoveTo)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(iDurationSecondsToPerformAnimation));
            DoubleAnimation doubleanimation = new DoubleAnimation(iPercentToMoveTo, duration);
            prgBarStatus.Value = iPercentToMoveTo;
            prgBarStatus.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        }

        /// <summary>
        /// Updates TestExecution Dialog Message for Tes Editor's Adhoc Run
        /// </summary>
        /// <param name="TestPath">File path of Test being executed</param>
        /// <param name="StepsToExecute">Number of Steps in a Test</param>
        public void UpdateTestExecutionDialog(String TestPath, int StepsToExecute = 0)
        {
            txtTestStatus.Text = TestPath;
            mStepInTest = StepsToExecute;
            btnCancelAllTests.Visibility = System.Windows.Visibility.Collapsed;
            Title = "Executing Step " + DlkTestRunnerApi.CurrentRunningStep + " of " + mStepInTest;
        }
        #endregion
    }
}
