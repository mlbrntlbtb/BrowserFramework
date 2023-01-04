using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkTestResultsRowRecord
    {
        public int mResultId { get; set; }
        public int mRunId { get; set; }
        public String mTestName { get; set; }
        public String mTestFile { get; set; }
        public int mTestInstance { get; set; }
        public String mTestStatus { get; set; }
        public int mTestStepFailed { get; set; }
        public Nullable<DateTime> mTestStart { get; set; }
        public Nullable<DateTime> mTestEnd { get; set; }
        public String mElapsed { get; set; }
        public String mLogLocation { get; set; }
        public String mOverrideStatus { get; set; }
        public String mOverrideInitials { get; set; }
        public String mComment { get; set; }
        public String mTestStepFailType { get; set; }
        public String mTestStepFailDetails { get; set; }

        public DlkTestResultsRowRecord() { }

        public DlkTestResultsRowRecord(int _ResultId, int _RunId, String _TestName, String _TestFile, int _TestInstance,
            String _TestStatus, int _TestStepFailed, Nullable<DateTime> _TestStart, Nullable<DateTime> _TestEnd, 
            String _Elapsed, String _LogLocation, String _OverrideStatus, String _OverrideInitials, String _Comment,
            String _TestStepFailType, String _TestStepFailDetails) 
        {
            mResultId = _ResultId;
            mRunId = _RunId;
            mTestName = _TestName;
            mTestFile = _TestFile;
            mTestInstance = _TestInstance;
            mTestStatus = _TestStatus;
            mTestStepFailed = _TestStepFailed;
            mTestStart = _TestStart;
            mTestEnd = _TestEnd;
            mElapsed = _Elapsed;
            mLogLocation = _LogLocation;
            mOverrideStatus = _OverrideStatus;
            mOverrideInitials = _OverrideInitials;
            mComment = _Comment;
            mTestStepFailType = _TestStepFailType;
            mTestStepFailDetails = _TestStepFailDetails;
        }
    }
}
