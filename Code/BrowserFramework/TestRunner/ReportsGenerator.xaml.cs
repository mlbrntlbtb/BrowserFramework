using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.Common;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ReportsGenerator.xaml
    /// </summary>
    public partial class ReportsGenerator : Window
    {

        private DlkReportQueryRecord _Query;
        public DlkReportQueryRecord Query
        {
            get
            {
                if (_Query == null)
                {
                    _Query = new DlkReportQueryRecord();
                }
                return _Query;
            }
        }

        private ObservableCollection<DlkExecutionQueueRecord> _ExecutionQueueRecords;
        public ObservableCollection<DlkExecutionQueueRecord> ExecutionQueueRecords
        {
            get
            {
                if (_ExecutionQueueRecords == null)
                {
                    _ExecutionQueueRecords = new ObservableCollection<DlkExecutionQueueRecord>();
                }
                return _ExecutionQueueRecords;
            }
        }

        public ReportsGenerator()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this;
            Results.DataContext = ExecutionQueueRecords;
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();
            List<DlkExecutionQueueRecord> results = DlkTestResultsQueryHandler.GetResultsFromQuery(Query);
            foreach (DlkExecutionQueueRecord rec in results)
            {
                ExecutionQueueRecords.Add(rec);
            }
        }

        private void ClearResults()
        {
            while (true)
            {
                if (ExecutionQueueRecords.Count > 0)
                {
                    ExecutionQueueRecords.RemoveAt(0);
                    continue;
                }
                break;
            }
        }
    }
}
