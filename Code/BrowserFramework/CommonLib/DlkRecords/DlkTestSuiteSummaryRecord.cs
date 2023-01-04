using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkRecords
{
    public class DlkTestSuiteSummaryRecord
    {
        public String suite { get; set; }
        public String suitepath { get; set; }
        public String resultsdirectory { get; set; }
        public int counttotal { get; set; }
        public int countpass { get; set; }
        public int countfail { get; set; }
        public List<DlkExecutionQueueRecord> testrecs { get; set; }

        public DlkTestSuiteSummaryRecord()
        {
            suite = "";
            suitepath = "";
            resultsdirectory = "";
            counttotal = -1;
            countpass = -1;
            countfail = -1;
            testrecs = new List<DlkExecutionQueueRecord>();
        }
        public DlkTestSuiteSummaryRecord(String _suite, String _suitepath, String _resultsdirectory,
            int _counttotal, int _countpass, int _countfail,
            List<DlkExecutionQueueRecord> _testrecs)
        {
            suite = _suite;
            suitepath = _suitepath;
            resultsdirectory = _resultsdirectory;
            counttotal = _counttotal;
            countpass = _countpass;
            countfail = _countfail;
            testrecs = _testrecs;
        }
    }
}
