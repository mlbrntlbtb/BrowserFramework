using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for StartBrowserDialog.xaml
    /// </summary>
    public partial class StartBrowserDialog : Window
    {
        public DlkStartBrowserData mStartBrowserData;

        public ObservableCollection<DlkLoginConfigRecord> EnvironmentIDs
        {
            get
            {
                DlkLoginConfigHandler loginHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile);

                return loginHandler.mLoginConfigRecords;
            }
        }

        public StartBrowserDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (EnvironmentIDs.Count > 0)
                {
                    mStartBrowserData = new DlkStartBrowserData(EnvironmentIDs[0].mID);
                }
                else
                {
                    mStartBrowserData = new DlkStartBrowserData("");
                }

                cboEnvironment.ItemsSource = EnvironmentIDs;
                cboEnvironment.DisplayMemberPath = "mID";
                cboEnvironment.DataContext = mStartBrowserData;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mStartBrowserData.EnvID != null && mStartBrowserData.EnvID != "")
                {
                    DlkLoginConfigHandler configHandler = new DlkLoginConfigHandler(DlkEnvironment.mLoginConfigFile, mStartBrowserData.EnvID);
                    DlkEnvironment.mBrowser = "firefox";
                    DlkEnvironment.StartBrowser();
                    DlkEnvironment.AutoDriver.Url = configHandler.mUrl;
                    DlkEnvironment.AutoDriver.Navigate();

                    this.DialogResult = true;
                    this.Close();

                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_ENVIRONMENT);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
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
    }
}
