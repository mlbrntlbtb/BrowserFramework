using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// Represents one row in the TestRun table
    /// </summary>
    public class DlkTestRunRowRecord
    {
        public int mRunId { get; set; }
        public int mTestSuiteId { get; set; }
        public String mMachine { get; set; }
        public Nullable<DateTime> mTestSuiteStart { get; set; }
        public Nullable<DateTime> mTestSuiteEnd { get; set; }
        public int mTestCount { get; set; }
        public int mExecutedCount { get; set; }
        public int mPassCount { get; set; }
        public int mFailCount { get; set; }
        public int mFailedAssertions { get; set; }

        public DlkTestRunRowRecord() { }

        public DlkTestRunRowRecord(int _RunId, int _TestSuiteId, String _Machine,
            Nullable<DateTime> _TestSuiteStart, Nullable<DateTime> _TestSuiteEnd,
            int _TestCount, int _ExecutedCount, int _PassCount, int _FailCount, int _FailedAssertions)
        {
            mRunId = _RunId;
            mTestSuiteId = _TestSuiteId;
            mMachine = _Machine;
            mTestSuiteStart = _TestSuiteStart;
            mTestSuiteEnd = _TestSuiteEnd;
            mTestCount = _TestCount;
            mExecutedCount = _ExecutedCount;
            mPassCount = _PassCount;
            mFailCount = _FailCount;
            mFailedAssertions = _FailedAssertions;
        }
    }
}
