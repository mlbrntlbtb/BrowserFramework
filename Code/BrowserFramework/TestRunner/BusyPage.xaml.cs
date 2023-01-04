using System;
using System.Windows;
using System.ComponentModel;
using CommonLib.DlkSystem;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for BusyPage.xaml
    /// </summary>
    public partial class BusyPage : Window
    {
        public delegate void ProcessToRun(object[] parameters);
        ProcessToRun m_Process = null;
        object[] m_Parameters = null;
        private bool processRan = false;

        BackgroundWorker m_Bgw = new BackgroundWorker();

        public BusyPage(ProcessToRun RunOnLoad, object[] parameters)
        {
            InitializeComponent();
            m_Process = RunOnLoad;
            m_Parameters = parameters;

            // setup bgw
            m_Bgw.DoWork += m_Bgw_DoWork;
            m_Bgw.RunWorkerCompleted += m_Bgw_RunWorkerCompleted;
        }

        void m_Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

        void m_Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Dispatcher.Invoke(m_Process, new object[]{m_Parameters});
            }
            catch
            {
                // ignore for now
            }
        }

        private void spinner_MediaEnded(object sender, RoutedEventArgs e)
        {
            try
            {
                spinner.Position = new TimeSpan(0, 0, 1);
                spinner.Play();
                if (!processRan)
                {
                    processRan = true;
                    m_Bgw.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
