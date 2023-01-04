using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Data;
using CommonLib.DlkHandlers;
using TestRunner.Common;
using CommonLib.DlkRecords;
using System.Linq;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for TestExecutionParametersDialog.xaml
    /// </summary>
    public partial class TestExecutionParametersDialog : Window
    {
        private ListCollectionView mBrowsers;
        private List<String> mEnvironmentIDs;
        private List<String> mIterations;
        DlkProductConfigHandler mProdConfigHandler = new DlkProductConfigHandler(Path.Combine(DlkEnvironment.mDirFramework, "Configs\\ProdConfig.xml"));

        public TestExecutionParametersDialog(ListCollectionView Browsers, List<string> EnvironmentIDs, int IterationCount)
        {
            mBrowsers = Browsers;
            mEnvironmentIDs = EnvironmentIDs;
            mIterations = new List<string>();
            for (int idx = 1; idx <= IterationCount; idx++)
            {
                mIterations.Add("Test : " + string.Format("{0:00000}", idx));
            }
            InitializeComponent();
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
                ((TestEditor)this.Owner).CurrentBrowser = cboExecutionBrowser.SelectedValue.ToString();
                ((TestEditor)this.Owner).CurrentEnvironment = cboExecutionEnvID.SelectedItem.ToString();
                ((TestEditor)this.Owner).CurrentInstance = cboIteration.SelectedIndex + 1;
                
                mProdConfigHandler.UpdateConfigValue("defaultenvironment", cboExecutionEnvID.Text);
                mProdConfigHandler.UpdateConfigValue("recentbrowser", cboExecutionBrowser.SelectedValue.ToString());
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
                cboExecutionBrowser.ItemsSource = mBrowsers;
                cboExecutionEnvID.ItemsSource = mEnvironmentIDs;
                cboIteration.ItemsSource = mIterations;
                cboIteration.SelectedIndex = 0;

                var defaultEnvironment = mProdConfigHandler.GetConfigValue("defaultenvironment");                
                var envIndex = cboExecutionEnvID.Items.IndexOf(defaultEnvironment);           

                cboExecutionEnvID.SelectedIndex = envIndex >= 0 ? envIndex : 0; 
                cboExecutionBrowser.SelectedIndex = GetDefaultBrowserIndex();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private int GetDefaultBrowserIndex()
        {
            string recentBrowser = mProdConfigHandler.GetConfigValue("recentbrowser");
            string defaultBrowser = DlkEnvironment.GetDefaultBrowserNameOrIndex(returnName: true);
            int recentBrowserIndex = -1;
            int defaultBrowserIndex = -1;

            for (int i = 0; i < cboExecutionBrowser.Items.Count; i++)
            {
                DlkBrowser browser = cboExecutionBrowser.Items[i] as DlkBrowser;

                if (recentBrowserIndex < 0 && recentBrowser == browser.Name)
                {
                    recentBrowserIndex = i;
                    if (new[] { "Mobile", "Remote" }.Contains(browser.BrowserType))
                        return recentBrowserIndex;
                }

                if (defaultBrowserIndex < 0 && defaultBrowser == browser.Name)
                    defaultBrowserIndex = i;

                if (defaultBrowserIndex > -1 && recentBrowserIndex > -1)
                    break;
            }

            return defaultBrowserIndex > -1 ? defaultBrowserIndex : (recentBrowserIndex > -1 ? recentBrowserIndex : 0);
        }
    }
}
