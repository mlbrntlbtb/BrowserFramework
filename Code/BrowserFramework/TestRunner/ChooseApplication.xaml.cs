using System;
using System.Windows;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using TestRunner.Common;

namespace TestRunner
{
    public partial class ChooseApplication : Window
    {
        private bool mContinue = false;
        private string mTestRunnerVersion;

        public ChooseApplication(string TestRunnerVersion)
        {
            InitializeComponent();
            mTestRunnerVersion = TestRunnerVersion;
        }

        private void Initialize()
        {
                cboTargetApplication.ItemsSource = DlkTestRunnerSettingsHandler.ApplicationList;
                lblTestRunner.Content += " " + mTestRunnerVersion;
                if (!cboTargetApplication.HasItems)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_EMPTY_PRODUCT_FOLDER, "Re-check files' integrity");
                    System.Environment.Exit(0);
                }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Initialize();
                this.Activate();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
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

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // check if dropdown item exists
                int indexToCheck = DlkTestRunnerSettingsHandler.ApplicationList.FindIndex(item => item.DisplayName == cboTargetApplication.Text);
                if (indexToCheck < 0)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_EMPTY_APPLICATION_DROPDOWN, "Invalid product chosen");
                    return;
                }
                DlkTestRunnerSettingsHandler.ApplicationUnderTest = (DlkTargetApplication)cboTargetApplication.SelectedItem;

                //DlkTestRunnerSettingsHandler.IsFirstTimeLaunch = false;
                mContinue = true;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.DialogResult = mContinue;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
