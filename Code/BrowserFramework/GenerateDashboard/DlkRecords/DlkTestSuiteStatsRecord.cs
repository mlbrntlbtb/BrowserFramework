using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDashboard.DlkRecords
{
    class DlkTestSuiteStatsRecord
    {
        public String mTestSuitePath
        {
            get
            {
                return _TestSuitePath;
            }
        }
        private String _TestSuitePath;

        public String mTestSuiteResultPath
        {
            get
            {
                return _TestSuiteResultPath;
            }
        }
        private String _TestSuiteResultPath;

        public String mTestSuiteName
        {
            get
            {
                return _TestSuiteName;
            }
        }
        private String _TestSuiteName;

        /// <summary>
        /// number of tests in the suites
        /// </summary>
        public int mTestCount
        {
            get
            {
                return _TestCount;
            }
        }
        private int _TestCount;

        /// <summary>
        /// How many tests were executed (not halted)
        /// </summary>
        public int mTestsExecuted
        {
            get
            {
                return _TestsExecuted;
            }
        }
        private int _TestsExecuted;

        /// <summary>
        /// how many have the status of "Passed"
        /// </summary>
        public int mTestsPassed
        {
            get
            {
                return _TestsPassed;
            }
        }
        private int _TestsPassed;

        /// <summary>
        /// how many have the status of "Failed"
        /// </summary>
        public int mTestsFailed
        {
            get
            {
                return _TestsFailed;
            }
        }
        private int _TestsFailed;

        /// <summary>
        /// total failed divided by total executed
        /// </summary>
        public double mTestsFailedPercentage
        {
            get
            {
                return _TestsFailedPercentage;
            }
        }
        private double _TestsFailedPercentage;

        public string mExecutionDate
        {
            get
            {
                return _ExecutionDate;
            }
        }
        private string _ExecutionDate;

        public bool mIsLatest
        {
            get
            {
                return _IsLatest;
            }
        }
        private bool _IsLatest;
        
        public DlkTestSuiteStatsRecord(String TestSuiteResultPath, String TestSuitePath, String TestSuiteName,
            int TestCount, int TestsExecuted, int TestsPassed, int TestsFailed, String ExecutionDate, bool IsLatest)
        {
            _TestSuiteResultPath = TestSuiteResultPath;
            _TestSuitePath = TestSuitePath;
            _TestSuiteName = TestSuiteName;
            _TestCount = TestCount;
            _TestsExecuted = TestsExecuted;
            _TestsPassed = TestsPassed;
            _TestsFailed = TestsFailed;
            _ExecutionDate = ExecutionDate;
            _IsLatest = IsLatest;
            if (_TestsFailed > 0)
            {
                _TestsFailedPercentage = Math.Round((((double)_TestsFailed / (double)_TestsExecuted) * 100), 2);
            }
            else
            {
                _TestsFailedPercentage = 0.0;
            }

        }
    }
}
