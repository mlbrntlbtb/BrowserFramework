using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRunner.Common
{
    public class DlkReportQueryRecord
    {
        public String tag { get; set; }
        public String date { get; set; }
        public String status { get; set; }

        public DlkReportQueryRecord()
        {
            tag = "";
            date = "";
            status = "";
        }
        public DlkReportQueryRecord(String _tag, String _date, String _status)
        {
            tag = _tag;
            date = _date;
            status = _status;
        }
    }
}
