using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// A scheduler record
    /// </summary>
    public class DlkSchedulerRecord
    {
        //public String msuite { get; set; }
        public DateTime mtasktime { get; set; }
        public DlkScheduleRecord mschedule { get; set; }

        public DlkSchedulerRecord()
        {
            //msuite = "";
            mtasktime = DateTime.Now;
            mschedule = null;
        }

        public DlkSchedulerRecord(DlkScheduleRecord Schedule, DateTime TaskTime)
        {
            mschedule = Schedule;
            //msuite = Schedule.msuite;
            mtasktime = TaskTime;
        }
    }
}