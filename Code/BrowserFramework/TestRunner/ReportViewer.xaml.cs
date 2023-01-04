using CommonLib.DlkSystem;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        private DlkHistoryReport _HistoryReportModel;
        public DlkHistoryReport mHistoryReportModel
        {
            get
            {
                if(_HistoryReportModel == null)
                {
                    _HistoryReportModel = new DlkHistoryReport();
                }
                return _HistoryReportModel;
            }
        }

        private DlkSummaryReport _SummaryReportModel;
        public DlkSummaryReport mSummaryReportModel
        {
            get
            {
                if(_SummaryReportModel == null)
                {
                    _SummaryReportModel = new DlkSummaryReport();
                }
                return _SummaryReportModel;
            }
        }

        public ObservableCollection<DlkReportContainer> ReportCharts { get; set; }

        public ReportViewer()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //ReportCharts.Add(new DlkReportContainer(mSummaryReportModel));
                //Charts.DataContext = ReportCharts;

                chartSummary.DataContext = mSummaryReportModel;
                chartSuite.DataContext = mSummaryReportModel;
                chartHistory.DataContext = mHistoryReportModel;
                dgExecutions.DataContext = mSummaryReportModel;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
