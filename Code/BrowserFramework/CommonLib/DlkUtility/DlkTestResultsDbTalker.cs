using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;

namespace CommonLib.DlkUtility
{
    /// <summary>
    /// This class handles communication with the results database
    /// </summary>
    public class DlkTestResultsDbTalker
    {
        #region DECLARATIONS
        private static int _TestResultsRemainingToReport = 0;
        private static int _RunId = -1;
        private ThreadStart mThreadJob;
        private Thread mTestRunThread;
        private static Boolean PerformDbUpdates = true;

        /// <summary>
        /// Test Name used in async update call 
        /// </summary>
        String _UpdTestName { get; set; }

        /// <summary>
        /// Test File used in async update call
        /// </summary>
        String _UpdTestFile { get; set; }

        /// <summary>
        /// Test Instance used in async update call
        /// </summary>
        int _UpdTestInstance { get; set; }

        /// <summary>
        /// Test Status used in async update call
        /// </summary>
        String _UpdTestStatus { get; set; }

        /// <summary>
        /// Test Start used in async update call
        /// </summary>
        DateTime _UpdTestStart { get; set; }

        /// <summary>
        /// Test End used in async update call
        /// </summary>
        DateTime _UpdTestEnd { get; set; }

        /// <summary>
        /// Log location used in async update call
        /// </summary>
        String _UpdLogLocation { get; set; }

        /// <summary>
        /// An identifier assigned when a test step fails
        /// </summary>
        String _UpdTestStepFailType { get; set; }

        /// <summary>
        /// Details describing the test step failure
        /// </summary>
        String _UpdTestStepFailDetails { get; set; }

        /// <summary>
        /// The step that failed
        /// </summary>
        int _UpdFailedTestStep { get; set; }

        public static int mTestResultsRemainingToReport
        {
            get
            {
                return _TestResultsRemainingToReport;
            }
        }

        /// <summary>
        /// There is only 1 RunId assigned per test suite execution
        /// </summary>
        public static int mRunId
        {
            get
            {
                return _RunId;
            }
        }

        /// <summary>
        /// if the update calls fail, they keep retrying until this is turned to false
        /// </summary>
        #endregion

        #region PUBLIC METHODS
        public DlkTestResultsDbTalker() { }

        /// <summary>
        /// Initializes the dB to record the test results for the suite.
        /// This is a syncronous transaction as we will prompt the user what to do if there is a problem connecting
        /// </summary>
        /// <param name="TestCount"></param>
        /// <param name="IsAdhocSuite"></param>
        /// <returns></returns>
        public static void InitializeDatabaseForResults(String mTestSuite, int TestCount, String Product)
        {
            // check if this is an adhoc execution
            String mIsAdhoc = "N";
            if (mTestSuite == "ADHOC")
            {
                mIsAdhoc = "Y";
            }

            // verify we can connect
            Boolean bResult = DlkDatabaseAPI.VerifyDatabaseConnection(DlkConfigHandler.mResultsDbConfigRecord);
            if (!bResult)
            {
                _RunId = -1;
                return;
            }

            // initialize the dB
            int mRunId = -1;
            Stopwatch mWatch = new Stopwatch();
            mWatch.Start();
            int iTimeoutMs = 5 * 60 * 1000; // 5 mins
            while (mWatch.ElapsedMilliseconds < iTimeoutMs)
            {
                try
                {
                    mRunId = DlkDatabaseAPI.InitializeTestSuiteForExecution(DlkConfigHandler.mResultsDbConfigRecord,
                         mTestSuite, TestCount, mIsAdhoc, Environment.MachineName, Product);
                    break;
                }
                catch
                {
                    mRunId = -1;
                    Thread.Sleep(3000);
                }
            }
            _RunId = mRunId;
        }

        /// <summary>
        /// Used to update the results database for a single test result
        /// </summary>
        /// <param name="RunId"></param>
        /// <param name="TestName"></param>
        /// <param name="TestFile"></param>
        /// <param name="TestInstance"></param>
        /// <param name="TestStatus"></param>
        /// <param name="TestStart"></param>
        /// <param name="TestEnd"></param>
        /// <param name="TestMachine"></param>
        /// <param name="LogLocation"></param>
        public void UpdateDatabaseResults(String TestName, String TestFile, int TestInstance,
            String TestStatus, DateTime TestStart, DateTime TestEnd, String LogLocation,
            int FailedTestStep, String TestStepFailType, String TestStepFailDetails)
        {
            if (mRunId < 0)
            {
                return;
            }

            if (FailedTestStep < 1)
            {
                FailedTestStep = -1;
            }

            _UpdTestName = TestName;
            _UpdTestFile = TestFile;
            _UpdTestInstance = TestInstance;
            _UpdTestStatus = TestStatus;
            _UpdTestStart = TestStart;
            _UpdTestEnd = TestEnd;
            _UpdLogLocation = LogLocation;
            _UpdTestStepFailType = TestStepFailType;
            _UpdTestStepFailDetails = TestStepFailDetails;
            _UpdFailedTestStep = FailedTestStep;

            mThreadJob = new ThreadStart(_UpdateDatabaseResults);
            mTestRunThread = new Thread(mThreadJob);
            mTestRunThread.Start();
        }

        /// <summary>
        /// used externally to halt the dB connections
        /// </summary>
        public static void HaltTestResultStorage()
        {
            PerformDbUpdates = false;
        }

        /// <summary>
        /// Finalize the database results for the suite
        /// </summary>
        /// <param name="RunId"></param>
        public static Boolean FinalizeDbResults()
        {
            if (mRunId < 0)
            {
                return false;
            }

            Boolean bResult = false;
            Stopwatch mWatch = new Stopwatch();
            mWatch.Start();
            int iTimeoutMs = 5 * 60 * 1000; // 5 mins
            while (mWatch.ElapsedMilliseconds < iTimeoutMs)
            {
                try
                {
                    DlkDatabaseAPI.FinalizeResults(DlkConfigHandler.mResultsDbConfigRecord, mRunId);
                    bResult = true;
                    break;
                }
                catch
                {
                    Thread.Sleep(3000);
                }
            }
            mWatch.Stop();
            return bResult;
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// This is the async thread called to update the dB
        /// </summary>
        private void _UpdateDatabaseResults()
        {
            if (mRunId < 0)
            {
                return;
            }

            _TestResultsRemainingToReport++;
            while (PerformDbUpdates)
            {
                try
                {
                    DlkDatabaseAPI.UpdateTestSuiteExecution(DlkConfigHandler.mResultsDbConfigRecord,
                        mRunId, _UpdTestName, _UpdTestFile, _UpdTestInstance, _UpdTestStatus, _UpdFailedTestStep, _UpdTestStart,
                        _UpdTestEnd, _UpdLogLocation, _UpdTestStepFailType, _UpdTestStepFailDetails
                           );
                    break;
                }
                catch
                {
                    Thread.Sleep(3000);
                }
            }
            _TestResultsRemainingToReport--;
        }
        #endregion

    }
}
