using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDashboard.DlkRecords
{
    /// <summary>
    /// This record holds the summary stats for the dashbaord
    /// </summary>
    class DlkSummaryStatsRecord
    {
        /// <summary>
        /// How many suites the dashboard is viewing
        /// </summary>
        public int mSuiteCount
        {
            get
            {
                return _SuiteCount;
            }
        }
        private int _SuiteCount;

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


        public DlkSummaryStatsRecord(int SuiteCount, int TestCount, int TestsExecuted, int TestsPassed, int TestsFailed)
        {
            _SuiteCount = SuiteCount;
            _TestCount = TestCount;
            _TestsExecuted = TestsExecuted;
            _TestsPassed = TestsPassed;
            _TestsFailed = TestsFailed;

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
