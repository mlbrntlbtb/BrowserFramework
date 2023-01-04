using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkRecords;

namespace TestRunner.AdvancedScheduler.View
{
    public interface IScheduleLogDetailsView
    {
        List <DlkScheduleLogsDetailsRecord> ScheduleLogsRecord { get; set; }
        string ScheduleLogsPath { get; }
        string Passed { get; set; }
        string Failed { get; set; }
        string Error { get; set; }
        string Cancelled { get; set; }
        string Warning { get; set; }
        string Pending { get; set; }
        string Disconnected { get; set; }
        string Total { get; set; }
        string NoLogsFound { get; set; }
    }
}
