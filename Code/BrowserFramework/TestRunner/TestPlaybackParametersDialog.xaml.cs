using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Windows;
using TestRunner.Common;
using TestRunner.Recorder;
using CommonLib.DlkRecords;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestPlaybackParametersDialog.xaml
    /// </summary>
    public partial class TestPlaybackParametersDialog : Window
    {
        private List<String> mEnvironmentIDs;
        private List<String> mIterations;
        private List<DlkBrowser> mBrowsers;

        public TestPlaybackParametersDialog(List<string> EnvironmentIDs, List<DlkBrowser> Browsers, int IterationCount, String Caption)
        {
            mEnvironmentIDs = EnvironmentIDs;
            mBrowsers = Browsers;
            mIterations = new List<string>();
            for (int idx = 1; idx <= IterationCount; idx++)
            {
                mIterations.Add("Test : " + string.Format("{0:00000}", idx));
            }
            InitializeComponent();
            this.Title = Caption;
            cboIteration.IsEnabled = IterationCount > 1;
        }

        private void btnCancelCurrentTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                //this.Close();
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
                if (cboExecutionEnvID.SelectedItem == null || string.IsNullOrEmpty(cboExecutionEnvID.Text)
                || !mEnvironmentIDs.Contains(cboExecutionEnvID.Text))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_ENVIRONMENT_INVALID, "Environment");
                    return;
                }
                ((TestCapture)this.Owner).PlaybackEnvironment = cboExecutionEnvID.SelectedItem.ToString();
                ((TestCapture)this.Owner).PlaybackInstance = cboIteration.SelectedIndex + 1;
                ((TestCapture)this.Owner).PlaybackBrowser = cboExecutionBrowser.Text;

                this.DialogResult = true;
                //this.Close();
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
                //this.DialogResult = false;
                cboExecutionEnvID.ItemsSource = mEnvironmentIDs;
                cboIteration.ItemsSource = mIterations;
                cboExecutionEnvID.SelectedIndex = 0;
                cboIteration.SelectedIndex = 0;

                cboExecutionBrowser.ItemsSource = mBrowsers;
                if (mBrowsers.FindAll(x => x.Name == "Firefox").Count > 0)
                {
                    cboExecutionBrowser.Text = "Firefox";
                }
                else
                {
                    cboExecutionBrowser.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
