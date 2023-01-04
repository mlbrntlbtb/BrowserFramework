using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using TestRunner.Common;
using TestRunner.Controls;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestConnectionDialog.xaml
    /// </summary>
    public partial class TestConnectionDialog : Window
    {
        #region DECLARATIONS
        private DlkBackgroundWorkerWithAbort mTestConnectionWorker = new DlkBackgroundWorkerWithAbort();
        private Modal mTestConnectModalDlg = null;
        private string mSelectedBrowser;
        #endregion

        #region PROPERTIES
        public string EnvironmentID { get; set; }

        public List<String> AvailableBrowsers
        {
            get { return DlkEnvironment.mAvailableBrowsers.Select(x => x.Alias).ToList(); }
        }

        public string SelectedBrowser { get => mSelectedBrowser; set => mSelectedBrowser = value; }

        #endregion

        #region METHODS
        public TestConnectionDialog(string id)
        {
            InitializeComponent();
            Initialize();
            EnvironmentID = id;
        }
       
        /// <summary>
        /// Initialize TestConnection Dialog window and backgroundworkers
        /// </summary>
        public void Initialize()
        {
            cboTargetBrowser.ItemsSource = AvailableBrowsers;
            cboTargetBrowser.DataContext = AvailableBrowsers;
            cboTargetBrowser.SelectedIndex = 0;
            
            /* Test Connection */
            mTestConnectionWorker = new DlkBackgroundWorkerWithAbort();
            mTestConnectionWorker.DoWork += mTestConnectionWorker_DoWork;
            mTestConnectionWorker.RunWorkerCompleted += mTestConnectionWorker_RunWorkerCompleted;
        }
        #endregion

        #region EVENTS
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mTestConnectModalDlg = new Modal(this, "Testing connection...");
                SelectedBrowser = cboTargetBrowser.SelectedValue.ToString();
                mTestConnectionWorker.RunWorkerAsync(EnvironmentID);
                mTestConnectModalDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            finally /* Ensure worker is retired and modal is closed */
            {
                mTestConnectionWorker.DoWork -= mTestConnectionWorker_DoWork;
                mTestConnectionWorker.RunWorkerCompleted -= mTestConnectionWorker_RunWorkerCompleted;
                mTestConnectModalDlg?.Close();
                this.Close();
            }

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
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

        #endregion

        #region BACKGROUNDWORKERS

        /// <summary>
        /// Do work handler for Test Connection worker
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mTestConnectionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                /* Get connect DAT test */
                string testPath = System.IO.Path.Combine(DlkEnvironment.mDirTests, DlkEnvironment.STR_TEST_CONNECT_FILE);

                /* Ensure a copy of connection test is in Test folder */
                if (!File.Exists(testPath))
                {
                    File.Copy(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DlkEnvironment.STR_TEST_CONNECT_FILE), testPath);
                }

                string env = e.Argument != null ? e.Argument.ToString() : string.Empty;

                /* Use selected browser for testing connection */
                DlkEnvironment.mBrowser = SelectedBrowser;

                /* Kill all running browser driver */
                DlkEnvironment.KillProcesses(new string[] { "IEDriverServer", "chromedriver", "geckodriver", "msedgedriver" });
                
                /* Run connect test */
                DlkEnvironment.StartBrowser(true);
                DlkTestRunnerApi.ExecuteTestPlayback(testPath, false, env,
                    testPath.Replace(DlkEnvironment.mDirTests, string.Empty).Trim('\\'), 1);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Work completed handler for Test Connection worker
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void mTestConnectionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                /* Check results if existing */
                if (Directory.Exists(DlkTestRunnerApi.mResultsPath))
                {
                    DirectoryInfo mDir = new DirectoryInfo(System.IO.Path.Combine(DlkEnvironment.mDirTestResults,
                        DlkTestRunnerApi.mResultsPath));
                    FileInfo file = mDir.GetFiles("*.dat").First();
                    DlkTest test = new DlkTest(file.FullName);

                    if (mTestConnectModalDlg != null && mTestConnectModalDlg.IsVisible)
                    {
                        mTestConnectModalDlg.Close();
                    }

                    if (test.mTestStatus.ToLower() == "passed")
                    {
                        if (IsAutoLogin())
                        {
                            DlkUserMessages.ShowInfo(GetWindow(this), DlkUserMessages.INF_CONNECTION_TEST_SUCCESSFUL);
                        }
                        else
                        {
                            DlkUserMessages.ShowWarning(GetWindow(this), DlkUserMessages.WRN_TEST_CONNECTION_SKIPLOGIN);
                        }
                    }
                    else
                    {
                        DlkUserMessages.ShowError(GetWindow(this), DlkUserMessages.ERR_CONNECTION_TEST_FAILED);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
            finally /* Ensure modal is closed */
            {
                if (mTestConnectModalDlg != null && mTestConnectModalDlg.IsVisible)
                {
                    mTestConnectModalDlg.Close();
                }
            }
        }

        /// <summary>
        /// Check whether an environment has autologin or not. Blank username and password will not proceed with autologin
        /// </summary>
        private bool IsAutoLogin()
        {
            DlkLoginConfigHandler handler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, EnvironmentID);
            return !String.IsNullOrEmpty(handler.mUser) && !String.IsNullOrEmpty(handler.mPassword);
        }
        #endregion


    }
}
