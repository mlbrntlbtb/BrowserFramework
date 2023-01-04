using CommonLib.DlkSystem;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for GenerateDashboard.xaml
    /// </summary>
    public partial class GenerateDashboardWindow : Window
    {
        private BackgroundWorker bw { get; set; }

        public GenerateDashboardWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Generate Dashboard 
                bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_Execute);
                bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                bw.WorkerReportsProgress = true;
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_Completed);
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void bw_Execute(object sender, DoWorkEventArgs e)
        {
            try
            {
                int i = 5;
                GenerateDashboard.GenerateDashboardApi.Initialize();
                ThreadStart mThreadJob = new ThreadStart(ExecuteGenerateDashboard);
                Thread mThread = new Thread(mThreadJob);
                mThread.Start();

                // check for process existence and report progress
                while (true)
                {
                    if (GenerateDashboard.GenerateDashboardApi.mIsDashboardGenerationRunning)
                    {
                        if (i < 90)
                        {
                            i = i + 5;
                        }
                        else
                        {
                            if (i < 98)
                            {
                                i = i + 1;
                            }
                        }
                        bw.ReportProgress(i);
                    }
                    else
                    {
                        bw.ReportProgress(99);
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                // move the progress bar
                int iCreep = 5;
                if (e.ProgressPercentage > 95)
                {
                    iCreep = 2;
                }
                UpdateProgressBar(iCreep, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Runs async at the end of execution.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bw_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


        private static void ExecuteGenerateDashboard()
        {
            GenerateDashboard.GenerateDashboardApi.GenerateDashboard();
        }

        /// <summary>
        /// updates the display of our progress
        /// </summary>
        /// <param name="iDurationSecondsToPerformAnimation"></param>
        /// <param name="iPercentToMoveTo"></param>
        private void UpdateProgressBar(int iDurationSecondsToPerformAnimation, int iPercentToMoveTo)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(iDurationSecondsToPerformAnimation));
            DoubleAnimation doubleanimation = new DoubleAnimation(iPercentToMoveTo, duration);
            prgGenerateDashboard.Value = iPercentToMoveTo;
            prgGenerateDashboard.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        }
    }
}
