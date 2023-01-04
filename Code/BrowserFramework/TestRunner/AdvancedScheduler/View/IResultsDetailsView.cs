using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using CommonLib.DlkRecords;

namespace TestRunner.AdvancedScheduler.View
{
    public interface IResultsDetailsView
    {
        List<DlkExecutionQueueRecord> ExecutionQueueRecords { get; set; }
        List<DlkTestStepRecord> Logs { get; set; }
        DlkExecutionQueueRecord SelectedTest { get; set; }
        BitmapImage ErrorScreenShot { get; set; }
        string SuitePath { get; }
        string SuiteResultsPath { get; }
        string ExecutionDate { get; set; }
        string Description { get; set; }
        string UserName { get; set; }
        string MachineName { get; set; }
        string OperatingSystem { get; set; }
        string Duration { get; set; }
        string Passed { get; set; }
        string Failed { get; set; }
        string Total { get; set; }
        string NotRun { get; set; }
        string PassRate { get; set; }
        string CompletionRate { get; set; }
        void DisplayError(string ErrorMessage);
    }
}
