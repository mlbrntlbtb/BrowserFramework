using System;
using System.ComponentModel;
using System.Windows;
using CommonLib.DlkSystem;
using TestRunner.Common;
using System.Threading;
using System.Windows.Threading;

namespace TestRunner.AdvancedScheduler
{
    /// <summary>
    /// Interaction logic for BlacklistTestCheckLoadingDialog.xaml
    /// </summary>
    public partial class BlacklistTestCheckLoadingDialog : Window, INotifyPropertyChanged
    {
        public BackgroundWorker mTestCheckLoaderWorker = new BackgroundWorker();

        private bool mIsLoaderDisplayed = false;
        private int mTestCheckProgress;
        private string mCurrentItemProcessing;
        private AdvancedSchedulerMainForm mParentForm;

        public event PropertyChangedEventHandler PropertyChanged;

        public BlacklistTestCheckLoadingDialog(AdvancedSchedulerMainForm owner)
        {
            InitializeComponent();
            mParentForm = owner;
            Initialize();
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int TestCheckProgress
        {
            get
            {
                return mTestCheckProgress;
            }
            set
            {
                mTestCheckProgress = value;
                NotifyPropertyChanged("TestCheckProgress");
            }
        }

        public string CurrentItemProcessing
        {
            get
            {
                return mCurrentItemProcessing;
            }
            set
            {
                mCurrentItemProcessing = value;
                NotifyPropertyChanged("CurrentItemProcessing");
            }
        }

        private void Initialize()
        {
            {
                try
                {
                    StartTestCheckLoaderWorker();
                }
                catch
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_INITIALIZE_ERROR);
                    return;
                }
            }
        }

        private void StartTestCheckLoaderWorker()
        {
            mTestCheckLoaderWorker = new DlkBackgroundWorkerWithAbort();
            mTestCheckLoaderWorker.DoWork += mTestCheckLoaderWorker_DoWork;
            mTestCheckLoaderWorker.ProgressChanged += new ProgressChangedEventHandler(mTestCheckLoaderWorker_ProgressChanged);
            mTestCheckLoaderWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(mTestCheckLoaderWorker_RunWorkerCompleted);
            mTestCheckLoaderWorker.WorkerReportsProgress = true;
            mTestCheckLoaderWorker.RunWorkerAsync();
        }

        private void mTestCheckLoaderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                /* Self kill */
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    this.DialogResult = true;
                }));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mTestCheckLoaderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!mIsLoaderDisplayed)
                {
                    mIsLoaderDisplayed = true;
                    mParentForm.CalculateTestCount();
                    mParentForm.CheckSuiteDefaultsForBlacklisted();
                    if (mParentForm.mIsSchedulerLoaderClosing)
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_SCHEDULER_LOADER_CONFIG_CORRUPT, "Login config file corrupted");
                    }
                    else
                    {
                        mTestCheckLoaderWorker.ReportProgress(100, "Starting Scheduler...");
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mTestCheckLoaderWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                TestCheckProgress = e.ProgressPercentage;
                if (e.UserState != null) 
                {
                    CurrentItemProcessing = e.UserState.ToString();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
