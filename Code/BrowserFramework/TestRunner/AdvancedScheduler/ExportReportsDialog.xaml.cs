using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.Common;
using TestRunner.AdvancedScheduler;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.AdvancedScheduler.Presenter;
using TestRunner.AdvancedScheduler.View;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Data;
using System.ComponentModel;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Collections;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ExportReportsDialog.xaml
    /// </summary>
    public partial class ExportReportsDialog : System.Windows.Window, IExportView
    {

        #region PRIVATE MEMBERS
        private string mSaveDialogFileName = String.Empty;
        private bool mAllHistoryResults = false;

        AdvancedSchedulerMainForm advSchedulerMainForm;

        private ExportPresenter mExportPresenter = null;
        private DataGrid mTestLineupGrid = null;
        private DataGrid mAgentsGrid = null;
        private ObservableCollection<ExecutionHistory> mHistory;

        #endregion

        #region IMPLEMENTATIONS
        //IExportView implemented properties
        public ObservableCollection<TestLineupRecord> TestLineup
        {
            get
            {
                return advSchedulerMainForm.TestLineup;
            }
        }

        public ObservableCollection<Agent> Agents
        {
            get
            {
                return advSchedulerMainForm.AgentsPool;
            }
        }
           
        #endregion

        #region CONSTRUCTORS
        public ExportReportsDialog(AdvancedSchedulerMainForm Owner)
        {
            InitializeComponent();
            this.Owner = Owner;
            advSchedulerMainForm = Owner;
            mExportPresenter = new ExportPresenter(this);
            mTestLineupGrid = advSchedulerMainForm.dgTestLineup;
            mAgentsGrid = advSchedulerMainForm.dgAgentLineup;
        }
        #endregion

        #region EVENTS
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                chkSchedulesInTestLineup.IsChecked = true;
                optLatestOnly.IsChecked = true;
                optLatestOnly.IsEnabled = false;
                optAll.IsEnabled = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
   
        private void chkResultsHistory_Checked(object sender, RoutedEventArgs e)
        {
            optAll.IsEnabled = true;
            optLatestOnly.IsEnabled = true;
        }

        private void chkResultsHistory_Unchecked(object sender, RoutedEventArgs e)
        {
            optAll.IsEnabled = false;
            optLatestOnly.IsEnabled = false;
        }

        private void optAll_Checked(object sender, RoutedEventArgs e)
        {
            mAllHistoryResults = true;
        }

        private void optLatestOnly_Checked(object sender, RoutedEventArgs e)
        {
            mAllHistoryResults = false;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((chkSchedulesInTestLineup.IsChecked == true) || (chkResultsHistory.IsChecked == true) || (chkAgentsInformation.IsChecked == true))
                {
                    if (LaunchSaveDialog())
                    {
                        mExportPresenter.GetDataColumnHeaders();

                        if (chkSchedulesInTestLineup.IsChecked == true)
                        {
                            mExportPresenter.CreateTestLineupDataTable(TestLineup);
                        }

                        if (chkResultsHistory.IsChecked == true)
                        {
                            if (mAllHistoryResults)
                            {
                                mHistory = new ObservableCollection<ExecutionHistory>(mExportPresenter.RetrieveScheduleHistoryGridData(mTestLineupGrid));
                                mExportPresenter.CreateCompleteResultsHistoryDataTable(mHistory);
                            }
                            else
                            {
                                mExportPresenter.CreateResultsHistoryDataTable(TestLineup);
                            }
                        }

                        if (chkAgentsInformation.IsChecked == true)
                        {
                            mHistory = new ObservableCollection<ExecutionHistory>(mExportPresenter.RetrieveAgentHistoryGridData(mAgentsGrid));
                            mExportPresenter.CreateAgentInfoDataTable(Agents, mHistory);
                        }

                        //Save the excel file
                        mExportPresenter.SaveExcelFile(mSaveDialogFileName);
                        DlkUserMessages.ShowInfo(DlkUserMessages.INF_SCHEDULER_EXPORT_COMPLETED, "Export");

                        this.Close();
                    }
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_INFO_SELECTED_TO_EXPORT, "Export Error");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region PRIVATE METHODS
       
        /// <summary>
        /// Displays the Save File Dialog before the actual export process is started.
        /// </summary>
        /// <returns>True if Save was clicked and False if otherwise</returns>
        private bool LaunchSaveDialog()
        {
            bool hasSaved = false;
            // Displays a SaveFileDialog so the can select where to save the exported file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx;";
            saveFileDialog.Title = "Save File To Export";
            saveFileDialog.FileName = string.Format("scheduler_{0}", DateTime.Now.ToString("yyyyMMddHHmmss")); 
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if ((bool)saveFileDialog.ShowDialog())
            {
                mSaveDialogFileName = saveFileDialog.FileName.ToString();
                hasSaved = true;
            }
            return hasSaved;
        }

        #endregion

    }

}
