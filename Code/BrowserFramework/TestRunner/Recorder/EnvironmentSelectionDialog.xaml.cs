using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using TestRunner.Common;
using TestRunner.Recorder;
using CommonLib.DlkHandlers;
using System.Linq;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for EnvironmentSelectionDialog.xaml
    /// </summary>
    public partial class EnvironmentSelectionDialog : Window
    {
        #region DECLARATIONS

        private List<DlkBrowser> mBrowsers;
        private List<String> mEnvironmentIDs;
        private List<DlkLoginConfigRecord> m_Environments;

        #endregion

        public string BrowserChoice { get; set; }

        #region CONSTRUCTORS
        public EnvironmentSelectionDialog(List<DlkBrowser> Browsers, List<string> EnvironmentIDs)
        {
            mEnvironmentIDs = EnvironmentIDs;
            mBrowsers = Browsers;
            m_Environments = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile).mLoginConfigRecords.ToList<DlkLoginConfigRecord>();
            InitializeComponent();
        }
        #endregion

        #region EVENTS
        private void btnCancelCurrentTest_Click(object sender, RoutedEventArgs e)
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

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!mEnvironmentIDs.Contains(cboExecutionEnvID.Text))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_ENVIRONMENT_INVALID, "Environment");
                    return;
                }
                if (HasIncompleteEnvironmentDetails(cboExecutionEnvID.Text))
                {
                    if (DlkUserMessages.ShowQuestionOkCancelWarning
                        (string.Format(DlkUserMessages.ASK_CONTINUE_ON_MISSING_ENV_CREDETIALS, cboExecutionEnvID.Text)) == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        BrowserChoice = cboExecutionBrowser.Text;
                        ((TestCapture)this.Owner).RecordingEnvironment = cboExecutionEnvID.SelectedItem.ToString();

                        this.DialogResult = true;
                        return;
                    }
                }

                BrowserChoice = cboExecutionBrowser.Text;
                ((TestCapture)this.Owner).RecordingEnvironment = cboExecutionEnvID.SelectedItem.ToString();

                this.DialogResult = true;
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
                cboExecutionEnvID.ItemsSource = mEnvironmentIDs;
                cboExecutionBrowser.ItemsSource = mBrowsers;
                int defaultBrowserIndex = mBrowsers.FindIndex(p => p.Name == DlkEnvironment.GetDefaultBrowserNameOrIndex(true));

                if (defaultBrowserIndex > -1)
                    cboExecutionBrowser.SelectedIndex = defaultBrowserIndex;
                else
                {
                    if (mBrowsers.FindAll(x => x.Name == "Firefox").Count > 0)
                    {
                        cboExecutionBrowser.Text = "Firefox";
                    }
                    else
                    {
                        cboExecutionBrowser.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Checks whether environment has missing user or password
        /// </summary>
        /// <param name="environmentID">Environment to be checked</param>
        /// <returns>TRUE if environment has missing user or password; FALSE otherwise</returns>
        private bool HasIncompleteEnvironmentDetails(String environmentID)
        {
            bool incompleteDetails = false;
            DlkLoginConfigRecord currentRecord = m_Environments.FirstOrDefault(x => x.mID.Equals(environmentID));

            if (string.IsNullOrWhiteSpace(currentRecord.mUser) || string.IsNullOrWhiteSpace(currentRecord.mPassword))
            {
                return true;
            }

            return incompleteDetails;
        }

        #endregion
    }
}
