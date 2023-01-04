using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using System.Threading;
using CommonLib.DlkRecords;

namespace CommonLib.DlkSystem
{
    public static class DlkDatabaseAPI
    {
        #region DECLARATIONS
        static String mConn { get; set; }
        static SqlConnection mResultsDbConn;
        static int mDefaultDbConnectionTimeInSec = 45;

        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Reads the config file connection infomation into memory
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        private static void CreateConnectionString(DlkResultsDatabaseConfigRecord ConfigRecord)
        {
            mConn = "Server=" + ConfigRecord.mServer + ";Database=" + ConfigRecord.mDatabase +
                ";User Id=" + ConfigRecord.mUser + ";Password=" + ConfigRecord.mPassword + ";";
        }

        /// <summary>
        /// Delete rows of data by RunId from TestResults
        /// </summary>
        /// <param name="RunIds">The list of run IDs</param>
        private static void DeleteFromTestResultsByRunId(List<int> RunIds)
        {
            // written to delete up to 10 ids at once
            List<int> mRunIds = new List<int>();
            mRunIds.AddRange(RunIds);
            while (mRunIds.Count > 0)
            {
                int iCount = mRunIds.Count - 1;
                int iMax = 0;
                if (iCount > 10)
                {
                    iMax = iCount - 9;
                }
                List<int> mToDelete = new List<int>();
                for (int i = iCount; i >= iMax; i--)
                {
                    mToDelete.Add(mRunIds[i]);
                    mRunIds.RemoveAt(i);
                }
                String sRunIds = String.Join(",", mToDelete);
                String mQuery = @"delete from TestResults where RunId in (" + sRunIds + ")";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                int iRowsUpdated = mCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete rows of data by RunId from TestRun
        /// </summary>
        /// <param name="RunIds">The list of run IDs</param>
        private static void DeleteFromTestRunByRunId(List<int> RunIds)
        {
            // written to delete up to 10 ids at once
            List<int> mRunIds = new List<int>();
            mRunIds.AddRange(RunIds);
            while (mRunIds.Count > 0)
            {
                int iCount = mRunIds.Count - 1;
                int iMax = 0;
                if (iCount > 10)
                {
                    iMax = iCount - 9;
                }
                List<int> mToDelete = new List<int>();
                for (int i = iCount; i >= iMax; i--)
                {
                    mToDelete.Add(mRunIds[i]);
                    mRunIds.RemoveAt(i);
                }
                String sRunIds = String.Join(",", mToDelete);
                String mQuery = @"delete from TestRun where RunId in (" + sRunIds + ")";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                int iRowsUpdated = mCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete by TestSuiteId from TestRun
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        private static void DeleteFromTestRunByTestSuiteId(int TestSuiteId)
        {
            String mQuery = @"delete from TestRun where TestSuiteId=@TestSuiteId";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
            int iRowsUpdated = mCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Get the RunIds from TestRun by querying TestSuiteId
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        private static List<int> GetRunIdsFromTestRunBySuiteId(int TestSuiteId)
        {
            List<int> mReturn = new List<int>();

            int mRunId = -1;
            String mQuery = "select RunId FROM dbo.TestRun where TestSuiteId=@TestSuiteId";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    mRunId = (int)mReader[0];
                    mReturn.Add(mRunId);
                }
            }
            finally
            {
                mReader.Close();
            }

            return mReturn;
        }

        /// <summary>
        /// Get the RunIds from TestRun by querying TestSuiteId and a date
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        /// <param name="DateThreshold">The date threshold for the test suite end</param>
        private static List<int> GetRunIdsFromTestRunBySuiteId(int TestSuiteId, DateTime DateThreshold)
        {
            List<int> mReturn = new List<int>();

            int mRunId = -1;
            String mQuery = "select RunId FROM dbo.TestRun where TestSuiteId=@TestSuiteId and TestSuiteEnd < @DateThreshold";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
            mCommand.Parameters.AddWithValue("DateThreshold", DateThreshold);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    mRunId = (int)mReader[0];
                    mReturn.Add(mRunId);
                }
            }
            finally
            {
                mReader.Close();
            }

            return mReturn;
        }

        /// <summary>
        /// Opens a connection (assigned to a global SqlConnection object) for general use
        /// </summary>
        private static void ConnectToDatabase(int iTimeoutSec)
        {
            Stopwatch mWatch = new Stopwatch();
            mWatch.Start();
            int iTimeoutMs = iTimeoutSec * 1000;
            Boolean bIsConnected = false;
            while (mWatch.ElapsedMilliseconds < iTimeoutMs)
            {
                try
                {
                    mResultsDbConn = new SqlConnection(mConn);
                    mResultsDbConn.Open();
                    bIsConnected = true;
                    break;
                }
                catch
                {
                    // do nothing; use bIsConnected to throw the error if needed
                }
            }
            if (!bIsConnected)
            {
                throw new Exception("Unabled to connect to test results database.");
            }
        }

        /// <summary>
        /// Closes the connection. Uses try catch logic to ignore errors that might occur when disconnecting 
        /// (for example if the connection was lost)
        /// </summary>
        private static void DisconnectFromDatabase()
        {
            try
            {
                mResultsDbConn.Close();
            }
            catch
            {
                // do nothing if closing the connection throws an error... we might be calling this just to ensure it is closed
            }
        }

        /// <summary>
        ///  Get the existing Suite Id. If it doesn't exist, user CreateIdIfNotFound to optionally add that suite to the table and return the inserted Suite Id
        /// </summary>
        /// <param name="TestSuite">The name of the test suite</param>
        /// <param name="IsAdhocExecution">Determines if the execution is adhoc or not (Y/N)</param>
        /// <param name="CreateIdIfNotFound">Determines whether to create an ID if not found</param>
        private static int GetSuiteId(String TestSuite, String IsAdhocExecution, String Product, Boolean CreateIdIfNotFound)
        {
            int mSuiteId = -1;
            String mQuery = "select TestSuiteId FROM dbo.TestSuites where TestSuite=@TestSuite and Product=@Product";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("TestSuite", TestSuite);
            mCommand.Parameters.AddWithValue("Product", Product.ToLower());
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    mSuiteId = (int)mReader[0];
                }
            }
            finally
            {
                mReader.Close();
            }

            if (mSuiteId < 0)
            {
                if (CreateIdIfNotFound)
                {
                    mSuiteId = CreateSuiteId(TestSuite, IsAdhocExecution, Product);
                }
            }
            return mSuiteId;
        }

        /// <summary>
        /// Either get the existing Suite Id or if it doesn't exist, add that suite to the table and return the inserted Suite Id
        /// </summary>
        /// <param name="TestSuite">The name of the test suite</param>
        /// <param name="IsAdhocExecution">Determines if the execution is adhoc or not (Y/N)</param>
        private static int GetSuiteId(String TestSuite, String IsAdhocExecution, String Product)
        {
            return GetSuiteId(TestSuite, IsAdhocExecution, Product, true);
        }

        /// <summary>
        /// Inserts a new suite into the TestSuites table and returns the created id
        /// </summary>
        /// <param name="TestSuite">The name of the test suite</param>
        /// <param name="IsAdhocExecution">Determines if the execution is adhoc or not (Y/N)</param>
        private static int CreateSuiteId(String TestSuite, String IsAdhocExecution, String Product)
        {
            int mSuiteId = -1, mMax = 15;

            // retry creating the suite id as needed as it is a Primary Key
            for (int i = 0; i < mMax; i++)
            {
                try
                {
                    mSuiteId = GetNextSuiteId();
                    String mQuery = @"insert into dbo.TestSuites (TestSuiteId, TestSuite, IsAdhocExecution,Product) values (@TestSuiteId, @TestSuite, @IsAdhocExecution,@Product)";
                    SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                    mCommand.Parameters.AddWithValue("TestSuiteId", mSuiteId);
                    mCommand.Parameters.AddWithValue("TestSuite", TestSuite);
                    mCommand.Parameters.AddWithValue("IsAdhocExecution", IsAdhocExecution);
                    mCommand.Parameters.AddWithValue("Product", Product.ToLower());

                    int iRowsUpdated = mCommand.ExecuteNonQuery();
                    break;
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
            return mSuiteId;
        }

        /// <summary>
        /// Queries the TestSuites table for the hightest TestSuiteId; increments it and returns it
        /// </summary>
        private static int GetNextSuiteId()
        {
            int mId = -1;
            String mQuery = "select Max(TestSuiteId) as MaxId FROM dbo.TestSuites";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    try
                    {
                        mId = (int)mReader[0];
                    }
                    catch { }
                }
            }
            finally
            {
                mReader.Close();
            }

            if (mId < 0)
            {
                mId = 0;
            }
            mId++;
            return mId;
        }

        /// <summary>
        /// Inserts a new row into the TestRun table to manage the suite execution data and returns the id
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        private static int CreateRunId(String Machine, int TestSuiteId, int TestCount)
        {
            int mId = -1, mMax = 15;

            // retry creating a Run Id as needed as it is a Primary Key
            for (int i = 0; i < mMax; i++)
            {
                try
                {
                    mId = GetNextRunId();
                    String mQuery = @"insert into dbo.TestRun (RunId, TestSuiteId, Machine, TestSuiteStart, TestSuiteEnd, " +
                        "TestCount, TestsExecuted, PassCount, FailCount, FailedAssertions) values " +
                        "(@RunId, @TestSuiteId, @Machine, @TestSuiteStart, null, @TestCount, @TestsExecuted, @PassCount, @FailCount, @FailedAssertions)";

                    SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);

                    mCommand.Parameters.AddWithValue("RunId", mId);
                    mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
                    mCommand.Parameters.AddWithValue("Machine", Machine);
                    mCommand.Parameters.AddWithValue("TestSuiteStart", DateTime.Now);
                    mCommand.Parameters.AddWithValue("TestCount", TestCount);
                    mCommand.Parameters.AddWithValue("TestsExecuted", 0);
                    mCommand.Parameters.AddWithValue("PassCount", 0);
                    mCommand.Parameters.AddWithValue("FailCount", 0);
                    mCommand.Parameters.AddWithValue("FailedAssertions", 0);

                    int iRowsUpdated = mCommand.ExecuteNonQuery();
                    break;
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
            return mId;
        }

        /// <summary>
        /// Queries the TestRun table for the highest RunId, then increments and returns it
        /// </summary>
        private static int GetNextRunId()
        {
            int mId = -1;
            String mQuery = "select Max(RunId) as MaxId FROM dbo.TestRun";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    try
                    {
                        mId = (int)mReader[0];
                    }
                    catch { }
                }
            }
            finally
            {
                mReader.Close();
            }

            if (mId < 0)
            {
                mId = 0;
            }
            mId++;
            return mId;
        }

        /// <summary>
        /// Queries the TestResults table for the highest ResultId, then increments and returns it
        /// </summary>
        private static int GetNextResultId()
        {
            int mId = -1;
            String mQuery = "select Max(ResultId) as MaxId FROM dbo.TestResults";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    try
                    {
                        mId = (int)mReader[0];
                    }
                    catch { }
                }
            }
            finally
            {
                mReader.Close();
            }

            if (mId < 0)
            {
                mId = 0;
            }
            mId++;
            return mId;
        }

        /// <summary>
        /// Inserts a test result into the TestResults table
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        /// <param name="TestName">The name of the test script</param>
        /// <param name="TestFile">The filename of the test script</param>
        /// <param name="TestInstance">Number of test instance of a test</param>
        /// <param name="TestStatus">Indicates whether the test passed or failed</param>
        /// <param name="TestStepFailed">Number of test step where the error occured</param>
        /// <param name="TestStart">Start time of test execution</param>
        /// <param name="TestEnd">End time of test execution</param>
        /// <param name="LogLocation">The location of the execution result logs</param>
        /// <param name="TestStepFailType">Type of the failed test step</param>
        /// <param name="TestStepFailDetails">Details of the  failed test step</param>
        private static void CreateTestResult(int RunId, String TestName,
            String TestFile, int TestInstance, String TestStatus, int TestStepFailed,
            Nullable<DateTime> TestStart, Nullable<DateTime> TestEnd, String LogLocation,
            String TestStepFailType, String TestStepFailDetails)
        {
            int mMax = 15;
            // retries logic as we are inserting a primary key
            for (int i = 0; i < mMax; i++)
            {
                try
                {
                    SqlCommand mCommand = new SqlCommand();
                    String mQuery = "";
                    if ((TestStart != null) && (TestEnd != null))
                    {
                        mQuery = @"insert into dbo.TestResults (ResultId, RunId, TestName, TestFile, TestInstance, TestStatus, " +
                         "TestStepFailed, TestStart, TestEnd, Elapsed, LogLocation, OverrideStatus, OverrideInitials, Comment, " +
                         "TestStepFailType, TestStepFailDetails) values (" +
                         "@ResultId, @RunId, @TestName, @TestFile, @TestInstance, @TestStatus, @TestStepFailed, @TestStart, @TestEnd, " +
                         "@Elapsed, @LogLocation, @OverrideStatus, @OverrideInitials, @Comment, @TestStepFailType, @TestStepFailDetails)";

                        mCommand = new SqlCommand(mQuery, mResultsDbConn);
                        mCommand.Parameters.AddWithValue("TestStart", TestStart);
                        mCommand.Parameters.AddWithValue("TestEnd", TestEnd);
                    }
                    else if ((TestStart != null) && (TestEnd == null))
                    {
                        mQuery = @"insert into dbo.TestResults (ResultId, RunId, TestName, TestFile, TestInstance, TestStatus, " +
                         "TestStepFailed, TestStart, Elapsed, LogLocation, OverrideStatus, OverrideInitials, Comment, " +
                         "TestStepFailType, TestStepFailDetails) values (" +
                         "@ResultId, @RunId, @TestName, @TestFile, @TestInstance, @TestStatus, @TestStepFailed, @TestStart, " +
                         "@Elapsed, @LogLocation, @OverrideStatus, @OverrideInitials, @Comment, @TestStepFailType, @TestStepFailDetails)";

                        mCommand = new SqlCommand(mQuery, mResultsDbConn);
                        mCommand.Parameters.AddWithValue("TestStart", TestStart);
                    }
                    else if ((TestStart == null) && (TestEnd != null))
                    {
                        mQuery = @"insert into dbo.TestResults (ResultId, RunId, TestName, TestFile, TestInstance, TestStatus, " +
                         "TestStepFailed, TestEnd, Elapsed, LogLocation, OverrideStatus, OverrideInitials, Comment, " +
                         "TestStepFailType, TestStepFailDetails) values (" +
                         "@ResultId, @RunId, @TestName, @TestFile, @TestInstance, @TestStatus, @TestStepFailed, @TestEnd, " +
                         "@Elapsed, @LogLocation, @OverrideStatus, @OverrideInitials, @Comment, @TestStepFailType, @TestStepFailDetails)";

                        mCommand = new SqlCommand(mQuery, mResultsDbConn);
                        mCommand.Parameters.AddWithValue("TestEnd", TestEnd);
                    }
                    else if ((TestStart == null) && (TestEnd == null))
                    {
                        mQuery = @"insert into dbo.TestResults (ResultId, RunId, TestName, TestFile, TestInstance, TestStatus, " +
                         "TestStepFailed, Elapsed, LogLocation, OverrideStatus, OverrideInitials, Comment, " +
                         "TestStepFailType, TestStepFailDetails) values (" +
                         "@ResultId, @RunId, @TestName, @TestFile, @TestInstance, @TestStatus, @TestStepFailed, " +
                         "@Elapsed, @LogLocation, @OverrideStatus, @OverrideInitials, @Comment, @TestStepFailType, @TestStepFailDetails)";

                        mCommand = new SqlCommand(mQuery, mResultsDbConn);
                    }

                    int ResultId = GetNextResultId();

                    mCommand.Parameters.AddWithValue("ResultId", ResultId);
                    mCommand.Parameters.AddWithValue("RunId", RunId);
                    mCommand.Parameters.AddWithValue("TestName", TestName);
                    mCommand.Parameters.AddWithValue("TestFile", TestFile);
                    mCommand.Parameters.AddWithValue("TestInstance", TestInstance);
                    mCommand.Parameters.AddWithValue("TestStatus", TestStatus);
                    mCommand.Parameters.AddWithValue("TestStepFailed", TestStepFailed);
                    mCommand.Parameters.AddWithValue("LogLocation", LogLocation);
                    mCommand.Parameters.AddWithValue("OverrideStatus", "");
                    mCommand.Parameters.AddWithValue("OverrideInitials", "");
                    mCommand.Parameters.AddWithValue("Comment", "");
                    mCommand.Parameters.AddWithValue("TestStepFailType", TestStepFailType);
                    mCommand.Parameters.AddWithValue("TestStepFailDetails", TestStepFailDetails);

                    // Calculate Elapsed Time
                    if ((TestStart != null) && (TestEnd != null))
                    {
                        TimeSpan ts = (DateTime)TestEnd - (DateTime)TestStart;
                        String mElapsed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        mCommand.Parameters.AddWithValue("Elapsed", mElapsed);
                    }
                    else
                    {
                        mCommand.Parameters.AddWithValue("Elapsed", "");
                    }

                    int iRowsUpdated = mCommand.ExecuteNonQuery();
                    break;
                }
                catch
                {
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Run query against TestResults table using the RunId and then update summary info in the TestRun table
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        private static void UpdateTestRunResultsForSuite(int RunId, Boolean IsLastTest)
        {
            String mQuery = @"select TestStatus, TestStepFailType from TestResults where RunId=@RunId";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("RunId", RunId);
            SqlDataReader mReader = mCommand.ExecuteReader();

            int iPassed = 0;
            int iFailed = 0;
            int iExecuted = 0;
            int iFailedAssertions = 0;
            String mVal = "", mFailType = "";
            try
            {
                while (mReader.Read())
                {
                    try
                    {
                        mVal = (String)mReader[0];
                        mFailType = (String)mReader[1];
                        if (mVal.ToLower() == "passed")
                        {
                            iExecuted++;
                            iPassed++;
                        }
                        else if (mVal.ToLower() == "failed")
                        {
                            iExecuted++;
                            iFailed++;
                            if (mFailType == "TestStepAssertion")
                            {
                                iFailedAssertions++;
                            }
                        }
                    }
                    catch { }
                }
            }
            finally
            {
                mReader.Close();
            }

            if (IsLastTest)
            {
                mQuery = @"update TestRun set TestSuiteStart=@TestSuiteStart, TestSuiteEnd=@TestSuiteEnd, TestsExecuted=@TestsExecuted, PassCount=@PassCount, FailCount=@FailCount, FailedAssertions=@FailedAssertions where RunId=@RunId";
                mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("RunId", RunId);
                mCommand.Parameters.AddWithValue("TestSuiteStart", GetTestSuiteStart(RunId));
                mCommand.Parameters.AddWithValue("TestSuiteEnd", GetTestSuiteEnd(RunId));
                mCommand.Parameters.AddWithValue("TestsExecuted", iExecuted);
                mCommand.Parameters.AddWithValue("PassCount", iPassed);
                mCommand.Parameters.AddWithValue("FailCount", iFailed);
                mCommand.Parameters.AddWithValue("FailedAssertions", iFailedAssertions);
            }
            else
            {
                mQuery = @"update TestRun set TestsExecuted=@TestsExecuted, PassCount=@PassCount, FailCount=@FailCount, FailedAssertions=@FailedAssertions where RunId=@RunId";
                mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("RunId", RunId);
                mCommand.Parameters.AddWithValue("TestsExecuted", iExecuted);
                mCommand.Parameters.AddWithValue("PassCount", iPassed);
                mCommand.Parameters.AddWithValue("FailCount", iFailed);
                mCommand.Parameters.AddWithValue("FailedAssertions", iFailedAssertions);
            }

            int iRowsUpdated = mCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Looks at the TestRun table and returns the SuiteId by looking up RunId (which is the primary key)
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        private static int GetTestSuiteIdFromTestRunByRunId(int RunId)
        {
            int mReturn = -1;
            try
            {
                String mQuery = "select TestSuiteId FROM dbo.TestRun where RunId=@RunId";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("RunId", RunId);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        mReturn = (int)mReader[0];
                    }
                }
                finally
                {
                    mReader.Close();
                }
            }
            catch
            {
                // do nothing, let caller handle -1 result
            }
            return mReturn;
        }

        /// <summary>
        /// Queries the TestSuites table for all of the suite ids
        /// </summary>
        /// <param name="IncludeAdHoc">Indicates whether Adhoc suites should be included</param>
        /// <param name="Product">Determines which product to get the suites from</param>
        private static List<int> GetTestSuiteIds(Boolean IncludeAdHoc, String Product)
        {
            List<int> mReturn = new List<int>();
            String mQuery = "";
            if (IncludeAdHoc)
            {
                mQuery = "select TestSuiteId FROM dbo.TestSuites where Product=@Product";
            }
            else
            {
                mQuery = "select TestSuiteId FROM dbo.TestSuites where IsAdhocExecution='N' and Product=@Product";
            }
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("Product", Product.ToLower());
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    int mId = (int)mReader[0];
                    mReturn.Add(mId);
                }
            }
            finally
            {
                mReader.Close();
            }
            return mReturn;
        }

        /// <summary>
        /// Gets the latest RunId from TestRun for a suite where there is a TestSuiteEnd
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        private static int GetLatestRunIdForTestSuite(int TestSuiteId)
        {
            int mReturn = -1;
            try
            {
                String mQuery = "select max(RunId) FROM dbo.TestRun where TestSuiteId=@TestSuiteId and TestSuiteEnd is not null";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        mReturn = (int)mReader[0];
                    }
                }
                finally
                {
                    mReader.Close();
                }
            }
            catch
            {
                // do nothing, let caller handle -1 result
            }
            return mReturn;
        }

        /// <summary>
        /// Gets the currently running RunId from TestRun for a suite where there is no TestSuiteEnd
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        private static int GetCurrentlyRunningRunIdForTestSuite(int TestSuiteId)
        {
            int mReturn = -1;
            try
            {
                String mQuery = "select max(RunId) FROM dbo.TestRun where TestSuiteId=@TestSuiteId and TestSuiteEnd is null";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        mReturn = (int)mReader[0];
                    }
                }
                finally
                {
                    mReader.Close();
                }
            }
            catch
            {
                // do nothing, let caller handle -1 result
            }
            return mReturn;
        }

        /// <summary>
        /// Get test suites that are marked as running on a VM
        /// </summary>
        /// <param name="Machine">The machine name to be checked</param>
        private static List<int> GetCurrentlyRunningRunIdsForMachine(String Machine)
        {
            List<int> mReturn = new List<int>();
            try
            {
                String mQuery = "select RunId FROM dbo.TestRun where Machine=@Machine and TestSuiteEnd is null";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("Machine", Machine);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        mReturn.Add((int)mReader[0]);
                    }
                }
                finally
                {
                    mReader.Close();
                }
            }
            catch
            {
                // do nothing
            }
            return mReturn;
        }

        /// <summary>
        /// Queries the database and returns the row data in record format to preserve the data types
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        private static DlkTestRunRowRecord GetTestRunRow(int RunId)
        {
            DlkTestRunRowRecord mResult = new DlkTestRunRowRecord();
            try
            {
                String mQuery = "select * FROM dbo.TestRun where RunId=@RunId";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("RunId", RunId);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        int mRunId = (int)mReader[0];
                        int mTestSuiteId = (int)mReader[1];
                        String mMachine = (String)mReader[2];

                        Nullable<DateTime> mTestSuiteStart = null;
                        try
                        {
                            mTestSuiteStart = (DateTime)mReader[3];
                        }
                        catch { }

                        Nullable<DateTime> mTestSuiteEnd = null;
                        try
                        {
                            mTestSuiteEnd = (DateTime)mReader[4];
                        }
                        catch { }

                        int mTestCount = (int)mReader[5];
                        int mExecutedCount = (int)mReader[6];
                        int mPassCount = (int)mReader[7];
                        int mFailCount = (int)mReader[8];
                        int mFailedAssertions = (int)mReader[9];

                        mResult = new DlkTestRunRowRecord(mRunId, mTestSuiteId, mMachine, mTestSuiteStart, mTestSuiteEnd, mTestCount,
                            mExecutedCount, mPassCount, mFailCount, mFailedAssertions);
                    }
                }
                finally
                {
                    mReader.Close();
                }
            }
            catch
            {
                // do nothing, let caller handle -1 result
            }
            return mResult;
        }

        /// <summary>
        /// Gets the Test Result row in record form for a specified ResultId
        /// </summary>
        /// <param name="ResultId">The identifier of a test result</param>
        private static DlkTestResultsRowRecord GetTestResultRow(int ResultId)
        {
            DlkTestResultsRowRecord mResult = new DlkTestResultsRowRecord();
            try
            {
                String mQuery = "select * FROM dbo.TestResults where ResultId=@ResultId";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("ResultId", ResultId);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        int mResultId = (int)mReader[0];
                        int mRunId = (int)mReader[1];
                        String mTestName = (String)mReader[2];
                        String mTestFile = (String)mReader[3];
                        int mTestInstance = (int)mReader[4];
                        String mTestStatus = (String)mReader[5];
                        int mTestStepFailed = (int)mReader[6];

                        Nullable<DateTime> mTestStart = null;
                        try
                        {
                            mTestStart = (DateTime)mReader[7];
                        }
                        catch { }

                        Nullable<DateTime> mTestEnd = null;
                        try
                        {
                            mTestEnd = (DateTime)mReader[8];
                        }
                        catch { }

                        String mElapsed = (String)mReader[9];
                        String mLogLocation = (String)mReader[10];
                        String mOverrideStatus = (String)mReader[11];
                        String mOverrideInitials = (String)mReader[12];
                        String mComment = (String)mReader[13];
                        String mTestStepFailType = (String)mReader[14];
                        String mTestStepFailDetails = (String)mReader[15];

                        mResult = new DlkTestResultsRowRecord(mResultId, mRunId, mTestName, mTestFile, mTestInstance,
                            mTestStatus, mTestStepFailed, mTestStart, mTestEnd, mElapsed, mLogLocation,
                            mOverrideStatus, mOverrideInitials, mComment, mTestStepFailType, mTestStepFailDetails);
                    }
                }
                finally
                {
                    mReader.Close();
                }
            }
            catch
            {
                // do nothing, let caller handle -1 result
            }
            return mResult;
        }

        /// <summary>
        /// Gets the ResultIds that match the RunId
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        private static List<int> GetResultIdsFromTestResultsByRunId(int RunId)
        {
            List<int> mReturn = new List<int>();

            int mRunId = -1;
            String mQuery = "select ResultId FROM dbo.TestResults where RunId=@RunId";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("RunId", RunId);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    mRunId = (int)mReader[0];
                    mReturn.Add(mRunId);
                }
            }
            finally
            {
                mReader.Close();
            }

            return mReturn;
        }

        /// <summary>
        /// Gets the completed run ids for the suite
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        private static List<int> GetCompletedRunIdsFromTestRunBySuiteId(int TestSuiteId)
        {
            List<int> mReturn = new List<int>();

            int mRunId = -1;
            String mQuery = "select RunId FROM dbo.TestRun where TestSuiteId=@TestSuiteId and TestSuiteEnd is not null";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("TestSuiteId", TestSuiteId);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    mRunId = (int)mReader[0];
                    mReturn.Add(mRunId);
                }
            }
            finally
            {
                mReader.Close();
            }

            return mReturn;

        }

        /// <summary>
        /// Gets the start time of suite execution
        /// </summary>
        private static DateTime GetTestSuiteStart(int RunID)
        {
            DateTime ret = DateTime.Now;
            String mQuery = "Select TestStart FROM dbo.TestResults where RunId=@RunId";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("RunId", RunID);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    DateTime dt = Convert.ToDateTime(mReader[0]);
                    if(dt < ret){
                        ret = dt;
                    }
                }
            }
            finally
            {
                mReader.Close();
            }

            return ret;
        }

        /// <summary>
        /// Gets the end time of suite execution
        /// </summary>
        private static DateTime GetTestSuiteEnd(int RunID)
        {
            DateTime ret = new DateTime();
            String mQuery = "Select TestEnd FROM dbo.TestResults where RunId=@RunId";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("RunId", RunID);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                while (mReader.Read())
                {
                    DateTime dt = Convert.ToDateTime(mReader[0]);
                    if (dt > ret)
                    {
                        ret = dt;
                    }
                }
            }
            finally
            {
                mReader.Close();
            }

            return ret;
        }

        /// <summary>
        /// Checks if Test is already existing
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        /// <param name="TestFile">The filename of the test script</param>
        private static Boolean IsTestExisting(int RunID, String TestFile)
        {
            String mQuery = "Select TestFile FROM dbo.TestResults where RunId=@RunId and TestFile=@TestFile";
            SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
            mCommand.Parameters.AddWithValue("RunId", RunID);
            mCommand.Parameters.AddWithValue("TestFile", TestFile);
            SqlDataReader mReader = mCommand.ExecuteReader();
            try
            {
                if (mReader.HasRows)
                {
                    return true;
                }
            }
            finally
            {
                mReader.Close();
            }
            return false;
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Creates the records in the database indicating a suite is running. If -1 is returned then something went wrong and the caller 
        /// should determine what to do.
        /// </summary>
        /// <param name="TestSuite">The Test Suite that we initialize.</param>
        /// <param name="TestCount">Number of tests inside the Test Suite</param>
        /// <param name="IsAdhocExecution">Determines if the execution is adhoc or not (Y/N)</param>
        /// <returns>Returns the RunId to be used by the caller to update test results with</returns>
        public static int InitializeTestSuiteForExecution(DlkResultsDatabaseConfigRecord ConfigRecord, String TestSuite, int TestCount, String IsAdhocExecution, String Machine, String Product)
        {
            int mRunId = -1;
            CreateConnectionString(ConfigRecord);
            ConnectToDatabase(mDefaultDbConnectionTimeInSec);
            // clean out old hanging data
            List<int> mHangingRuns = GetCurrentlyRunningRunIdsForMachine(Machine);
            foreach (int mHangingRunId in mHangingRuns)
            {
                try
                {
                    UpdateTestRunResultsForSuite(mHangingRunId, true);
                }
                catch
                {
                    // do nothing
                }
            }

            // create test run
            try
            {
                int mSuiteId = GetSuiteId(TestSuite, IsAdhocExecution, Product);
                mRunId = CreateRunId(Machine, mSuiteId, TestCount);
            }
            catch
            {
                // try to disconnect (DisconnectFromDatabase won't throw an error if it cannot)
                DisconnectFromDatabase();

                // nothing else needed as we will return -1 to caller
            }
            return mRunId;
        }

        /// <summary>
        /// Used to add a test case result to a suite. This requires that you have already called InitializeTestSuiteForExectuion. 
        /// The caller should use try-catch logic to catch any errors and determine what to do.
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="RunId">The identifier for the suite run</param>
        /// <param name="TestName">The name of the test script</param>
        /// <param name="TestFile">The filename of the test script</param>
        /// <param name="TestInstance">Number of test instance of a test</param>
        /// <param name="TestStatus">Indicates whether the test passed or failed</param>
        /// <param name="TestStepFailed">Number of test step where the error occured</param>
        /// <param name="TestStart">Start time of test execution</param>
        /// <param name="TestEnd">End time of test execution</param>
        /// <param name="LogLocation">The location of the execution result logs</param>
        public static void UpdateTestSuiteExecution(DlkResultsDatabaseConfigRecord ConfigRecord, int RunId, String TestName,
            String TestFile, int TestInstance, String TestStatus, int TestStepFailed,
            Nullable<DateTime> TestStart, Nullable<DateTime> TestEnd, String LogLocation,
            String TestStepFailType, String TestStepFailDetails)
        {
            CreateConnectionString(ConfigRecord);

            ConnectToDatabase(mDefaultDbConnectionTimeInSec);

            // normalize data
            if (TestStatus.ToLower().Trim().StartsWith("pass"))
            {
                TestStatus = "Passed";
                TestStepFailed = 0;
            }
            else if (TestStatus.ToLower().Trim().StartsWith("fail"))
            {
                TestStatus = "Failed";
            }

            if (TestInstance < 1)
            {
                TestInstance = 1;
            }

            try
            {
                 if (!IsTestExisting(RunId, TestFile)) {
                    // insert a new record into the TestResults table
                    CreateTestResult(RunId, TestName, TestFile, TestInstance, TestStatus, TestStepFailed,
                        TestStart, TestEnd, LogLocation, TestStepFailType, TestStepFailDetails);

                    // Update the summary results for the Suite Run
                    UpdateTestRunResultsForSuite(RunId, false);
                }                
            }
            catch
            {
                // try to disconnect (DisconnectFromDatabase won't throw an error if it cannot)
                DisconnectFromDatabase();

                // raise the error post db disconnect
                throw;
            }

            DisconnectFromDatabase();
        }

        /// <summary>
        /// Finalize the results for the suite
        /// </summary>
        /// <param name="RunId">The identifier for the suite run</param>
        public static void FinalizeResults(DlkResultsDatabaseConfigRecord ConfigRecord, int RunId)
        {
            CreateConnectionString(ConfigRecord);
            ConnectToDatabase(mDefaultDbConnectionTimeInSec);
            try
            {
                UpdateTestRunResultsForSuite(RunId, true);
            }
            catch
            {
                // try to disconnect (DisconnectFromDatabase won't throw an error if it cannot)
                DisconnectFromDatabase();

                // raise the error post db disconnect
                throw;
            }
            DisconnectFromDatabase();
        }

        /// <summary>
        /// Deletes results in the TestRun and TestResults tables using TestSuiteId
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="TestSuite">The name of the test suite</param>
        public static void DeleteResultsForSuite(DlkResultsDatabaseConfigRecord ConfigRecord, String TestSuite, String Product)
        {
            // connect to dB
            CreateConnectionString(ConfigRecord);
            ConnectToDatabase(mDefaultDbConnectionTimeInSec);

            int mSuiteId = -1;
            try
            {
                // Get the Suite Id or End if less than 0
                mSuiteId = GetSuiteId(TestSuite, "false", Product, false);
                if (mSuiteId < 0)
                {
                    return;
                }

                // Get the RunIds from TestRun using SuiteId
                List<int> mRunIds = new List<int>();
                GetRunIdsFromTestRunBySuiteId(mSuiteId);
                if (mRunIds.Count < 1)
                {
                    return;
                }

                // Delete by RunId from TestResults
                DeleteFromTestResultsByRunId(mRunIds);

                // Delete by TestSuiteId from TestRun
                DeleteFromTestRunByTestSuiteId(mSuiteId);
            }
            catch
            {
                // try to disconnect (DisconnectFromDatabase won't throw an error if it cannot)
                DisconnectFromDatabase();

                // raise the error post db disconnect
                throw;
            }

            // Disconnect
            DisconnectFromDatabase();

        }

        /// <summary>
        /// Delete the results for a suite that are older than the DateThreshold
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="TestSuite">The name of the test suite</param>
        /// <param name="DateThreshold">The date threshold for the test suite end</param>
        public static void DeleteResultsForSuiteOlderThan(DlkResultsDatabaseConfigRecord ConfigRecord, String TestSuite, DateTime DateThreshold, String Product)
        {
            // connect to dB
            CreateConnectionString(ConfigRecord);
            ConnectToDatabase(mDefaultDbConnectionTimeInSec);

            int mSuiteId = -1;
            try
            {
                // Get the Suite Id or End if less than 0
                mSuiteId = GetSuiteId(TestSuite, "false", Product, false);
                if (mSuiteId < 0)
                {
                    return;
                }

                // Get the RunIds from TestRun using SuiteId
                List<int> mRunIds = GetRunIdsFromTestRunBySuiteId(mSuiteId, DateThreshold);
                if (mRunIds.Count < 1)
                {
                    return;
                }

                // Delete by RunId from TestResults
                DeleteFromTestResultsByRunId(mRunIds);

                // Delete by RunId from TestRun
                DeleteFromTestRunByRunId(mRunIds);
            }
            catch
            {
                // try to disconnect (DisconnectFromDatabase won't throw an error if it cannot)
                DisconnectFromDatabase();

                // raise the error post db disconnect
                throw;
            }

            // Disconnect
            DisconnectFromDatabase();
        }

        /// <summary>
        /// Used to test that we can connect to the Test Results Database defined by the config file
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        public static Boolean VerifyDatabaseConnection(DlkResultsDatabaseConfigRecord ConfigRecord)
        {
            CreateConnectionString(ConfigRecord);
            Boolean bResult = false;
            try
            {
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);
                DisconnectFromDatabase();
                bResult = true;
            }
            catch
            {
                DisconnectFromDatabase();
            }
            return bResult;
        }


        /// <summary>
        /// Returns the DlkTestRunRowRecords which represent the summary based on number of days requested
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="NumberOfDays">The number of days where test results will be included</param>
        /// <param name="SuiteCount">The number of suites</param>
        /// <param name="TestsCount">The number of tests</param>
        /// <param name="TestsExecuted">The number of tests executed</param>
        /// <param name="TestsPassed">The number of tests passed</param>
        /// <param name="TestsFailed">The number of tests failed</param>
        public static List<DlkTestRunRowRecord> GetSummaryResults(DlkResultsDatabaseConfigRecord ConfigRecord, int NumberOfDays, String Product, Boolean IncludeAdhoc,
            out int SuiteCount, out int TestsCount,
            out int TestsExecuted, out int TestsPassed, out int TestsFailed, out int TestsFailedAssertions)
        {
            List<DlkTestRunRowRecord> mResults = new List<DlkTestRunRowRecord>();

            int mTotal = 0, mTotalPass = 0, mTotalFail = 0, mTotalExecuted = 0, mTotalFailedAssertions = 0;

            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                // get all of the TestSuiteIds from TestSuites
                List<int> mTestSuiteIds = GetTestSuiteIds(IncludeAdhoc, Product);

                // for each TestSutieId, get the latest RunId from TestRun that has a TestSuiteEnd
                List<int> mRunIds = new List<int>();
                foreach (int iTestSuiteId in mTestSuiteIds)
                {
                    int mRunId = GetLatestRunIdForTestSuite(iTestSuiteId);
                    if (mRunId != -1)
                    {
                        mRunIds.Add(mRunId);
                    }
                }
                mRunIds.Sort();
                mRunIds.Reverse();

                DateTime mThreshold = DateTime.Now;
                if (NumberOfDays > 0)
                {
                    mThreshold = DateTime.Now.Subtract(new TimeSpan(NumberOfDays, 0, 0, 0));
                }

                // for each RunId, get the data [Machine, TestSuiteStart, TestSuiteEnd, TestCount, TestExecuted, PassCount, FailCount]
                List<int> mSuitesDuringTheRun = new List<int>();

                foreach (int iRunId in mRunIds)
                {
                    DlkTestRunRowRecord mTestRunRow = GetTestRunRow(iRunId);

                    if (NumberOfDays > 0)
                    {
                        if ((DateTime)mTestRunRow.mTestSuiteEnd > mThreshold)
                        {
                            if (!mSuitesDuringTheRun.Contains(mTestRunRow.mTestSuiteId))
                            {
                                mSuitesDuringTheRun.Add(mTestRunRow.mTestSuiteId);
                            }

                            mResults.Add(mTestRunRow);
                            mTotal = mTotal + mTestRunRow.mTestCount;
                            mTotalPass = mTotalPass + mTestRunRow.mPassCount;
                            mTotalFail = mTotalFail + mTestRunRow.mFailCount;
                            mTotalExecuted = mTotalExecuted + mTestRunRow.mExecutedCount;
                            mTotalFailedAssertions = mTotalFailedAssertions + mTestRunRow.mFailedAssertions;
                        }
                    }
                    else
                    {
                        if (!mSuitesDuringTheRun.Contains(mTestRunRow.mTestSuiteId))
                        {
                            mSuitesDuringTheRun.Add(mTestRunRow.mTestSuiteId);
                        }
                        mResults.Add(mTestRunRow);
                        mTotal = mTotal + mTestRunRow.mTestCount;
                        mTotalPass = mTotalPass + mTestRunRow.mPassCount;
                        mTotalFail = mTotalFail + mTestRunRow.mFailCount;
                        mTotalExecuted = mTotalExecuted + mTestRunRow.mExecutedCount;
                        mTotalFailedAssertions = mTotalFailedAssertions + mTestRunRow.mFailedAssertions;
                    }
                }

                SuiteCount = mSuitesDuringTheRun.Count;
                TestsCount = mTotal;
                TestsExecuted = mTotalExecuted;
                TestsPassed = mTotalPass;
                TestsFailed = mTotalFail;
                TestsFailedAssertions = mTotalFailedAssertions;
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mResults;
        }

        /// <summary>
        /// Used when you need just the TestRun row data for a specified RunId
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="RunId">The identifier for the suite run</param>
        public static DlkTestRunRowRecord GetSummaryResultsByRunId(DlkResultsDatabaseConfigRecord ConfigRecord, int RunId)
        {
            DlkTestRunRowRecord mResult = new DlkTestRunRowRecord();

            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                mResult = GetTestRunRow(RunId);
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mResult;
        }

        /// <summary>
        /// Retrieves the TestSuite name for the specified RunId
        /// </summary>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        public static String GetTestSuiteNameByRunId(DlkResultsDatabaseConfigRecord ConfigRecord, int RunId)
        {
            String mReturn = "";

            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);


                // get the TestSuiteId using the RunId in the TestRun table
                int mTestSuiteId = GetTestSuiteIdFromTestRunByRunId(RunId);
                if (mTestSuiteId == -1)
                {
                    return "";
                }

                try
                {
                    String mQuery = "select TestSuite FROM dbo.TestSuites where TestSuiteId=@TestSuiteId";
                    SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                    mCommand.Parameters.AddWithValue("TestSuiteId", mTestSuiteId);
                    SqlDataReader mReader = mCommand.ExecuteReader();
                    try
                    {
                        while (mReader.Read())
                        {
                            mReturn = (String)mReader[0];
                        }
                    }
                    finally
                    {
                        mReader.Close();
                    }
                }
                catch
                {
                    // return "" and let the caller handle
                }
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mReturn;
        }

        /// <summary>
        /// Gets the data for test suites that are currently executing
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="SuiteCount">The number of suites</param>
        /// <param name="TestsCount">The number of tests</param>
        /// <param name="TestsExecuted">The number of tests executed</param>
        /// <param name="TestsPassed">The number of tests passed</param>
        /// <param name="TestsFailed">The number of tests failed</param>
        public static List<DlkTestRunRowRecord> GetCurrentlyRunningResults(DlkResultsDatabaseConfigRecord ConfigRecord, String Product, out int SuiteCount, out int TestsCount,
    out int TestsExecuted, out int TestsPassed, out int TestsFailed, out int TestsFailedAssertions)
        {
            List<DlkTestRunRowRecord> mResults = new List<DlkTestRunRowRecord>();

            int mSuiteCount = 0, mTotal = 0, mTotalPass = 0, mTotalFail = 0, mTotalExecuted = 0, mTotalFailedAssertions = 0;

            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                // get all of the TestSuiteIds from TestSuites
                List<int> mTestSuiteIds = GetTestSuiteIds(true, Product);

                // for each TestSutieId, get the latest RunId from TestRun that does not have a TestSuiteEnd
                List<int> mRunIds = new List<int>();
                foreach (int iTestSuiteId in mTestSuiteIds)
                {
                    int mRunId = GetCurrentlyRunningRunIdForTestSuite(iTestSuiteId);
                    if (mRunId != -1)
                    {
                        mRunIds.Add(mRunId);
                    }
                }
                mSuiteCount = mRunIds.Count;

                // for each RunId, get the data [Machine, TestSuiteStart, TestSuiteEnd, TestCount, TestExecuted, PassCount, FailCount]
                foreach (int iRunId in mRunIds)
                {
                    DlkTestRunRowRecord mTestRunRow = GetTestRunRow(iRunId);

                    mResults.Add(mTestRunRow);

                    mTotal = mTotal + mTestRunRow.mTestCount;
                    mTotalPass = mTotalPass + mTestRunRow.mPassCount;
                    mTotalFail = mTotalFail + mTestRunRow.mFailCount;
                    mTotalExecuted = mTotalExecuted + mTestRunRow.mExecutedCount;
                    mTotalFailedAssertions = mTotalFailedAssertions + mTestRunRow.mFailedAssertions;
                }

                SuiteCount = mSuiteCount;
                TestsCount = mTotal;
                TestsExecuted = mTotalExecuted;
                TestsPassed = mTotalPass;
                TestsFailed = mTotalFail;
                TestsFailedAssertions = mTotalFailedAssertions;
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mResults;
        }

        /// <summary>
        /// Gets the rows that match the RunId from the TestResults table
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        /// <param name="RunId">The identifier for the suite run</param>
        public static List<DlkTestResultsRowRecord> GetSuiteDetails(DlkResultsDatabaseConfigRecord ConfigRecord, int TestSuiteId, int RunId)
        {
            List<DlkTestResultsRowRecord> mResults = new List<DlkTestResultsRowRecord>();

            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                // get a list of ResultIds given the RunId from the TestResults table
                List<int> mResultIds = GetResultIdsFromTestResultsByRunId(RunId);

                // for each ResultId, get the data 
                foreach (int mResultId in mResultIds)
                {
                    DlkTestResultsRowRecord mTestResultsRow = GetTestResultRow(mResultId);

                    mResults.Add(mTestResultsRow);
                }
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mResults;
        }

        /// <summary>
        /// Gets the rows in record form from the TestRun table for the specified suite
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        public static List<DlkTestRunRowRecord> GetAllSuiteRunsBySuiteId(DlkResultsDatabaseConfigRecord ConfigRecord, int TestSuiteId)
        {
            List<DlkTestRunRowRecord> mResults = new List<DlkTestRunRowRecord>();

            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                // get all of the RunIds from TestRun for the suite
                List<int> mRunIds = GetRunIdsFromTestRunBySuiteId(TestSuiteId);

                // for each RunId, get the data [Machine, TestSuiteStart, TestSuiteEnd, TestCount, TestExecuted, PassCount, FailCount]
                foreach (int iRunId in mRunIds)
                {
                    DlkTestRunRowRecord mTestRunRow = GetTestRunRow(iRunId);

                    mResults.Add(mTestRunRow);
                }
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mResults;
        }

        /// <summary>
        /// Gets the Test Result row in record form for a specified ResultId
        /// </summary>
        /// <param name="ResultId">The identifier of a test result</param>
        public static DlkTestResultsRowRecord GetTestResult(DlkResultsDatabaseConfigRecord ConfigRecord, int ResultId)
        {
            DlkTestResultsRowRecord mResult = new DlkTestResultsRowRecord();
            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                String mQuery = "select * FROM dbo.TestResults where ResultId=@ResultId";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("ResultId", ResultId);
                SqlDataReader mReader = mCommand.ExecuteReader();
                try
                {
                    while (mReader.Read())
                    {
                        int mResultId = (int)mReader[0];
                        int mRunId = (int)mReader[1];
                        String mTestName = (String)mReader[2];
                        String mTestFile = (String)mReader[3];
                        int mTestInstance = (int)mReader[4];
                        String mTestStatus = (String)mReader[5];
                        int mTestStepFailed = (int)mReader[6];

                        Nullable<DateTime> mTestStart = null;
                        try
                        {
                            mTestStart = (DateTime)mReader[7];
                        }
                        catch { }

                        Nullable<DateTime> mTestEnd = null;
                        try
                        {
                            mTestEnd = (DateTime)mReader[8];
                        }
                        catch { }

                        String mElapsed = (String)mReader[9];
                        String mLogLocation = (String)mReader[10];
                        String mOverrideStatus = (String)mReader[11];
                        String mOverrideInitials = (String)mReader[12];
                        String mComment = (String)mReader[13];
                        String mTestStepFailType = (String)mReader[14];
                        String mTestStepFailDetails = (String)mReader[15];

                        mResult = new DlkTestResultsRowRecord(mResultId, mRunId, mTestName, mTestFile, mTestInstance,
                            mTestStatus, mTestStepFailed, mTestStart, mTestEnd, mElapsed, mLogLocation,
                            mOverrideStatus, mOverrideInitials, mComment, mTestStepFailType, mTestStepFailDetails);
                    }
                }
                finally
                {
                    mReader.Close();
                    DisconnectFromDatabase();
                }
            }
            catch
            {
                // do nothing, let caller handle -1 result
            }
            return mResult;
        }

        /// <summary>
        /// Update the TestResult row with Override details
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="ResultId">The identifier of a test result</param>
        /// <param name="OverrideStatus">The status of the override</param>
        /// <param name="OverrideInitials">The initials of the user who performed the override</param>
        /// <param name="Comment">The comment for the test result</param>
        public static void OverrideTestStatus(DlkResultsDatabaseConfigRecord ConfigRecord, int ResultId, String OverrideStatus, String OverrideInitials, String Comment)
        {
            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                String mQuery = @"update TestResults set OverrideStatus=@OverrideStatus, OverrideInitials=@OverrideInitials, Comment=@Comment where ResultId=@ResultId";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("ResultId", ResultId);
                mCommand.Parameters.AddWithValue("OverrideStatus", OverrideStatus);
                mCommand.Parameters.AddWithValue("OverrideInitials", OverrideInitials);
                mCommand.Parameters.AddWithValue("Comment", Comment);

                int iRowsUpdated = mCommand.ExecuteNonQuery();
            }
            finally
            {
                DisconnectFromDatabase();
            }
        }

        /// <summary>
        /// Update the comments for the specified test result
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="ResultId">The identifier of a test result</param>
        /// <param name="OverrideInitials">The initials of the user who performed the override</param>
        /// <param name="Comment">The comment for the test result</param>
        public static void UpdateTestComment(DlkResultsDatabaseConfigRecord ConfigRecord, int ResultId, String OverrideInitials, String Comment)
        {
            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);

                String mQuery = @"update TestResults set OverrideInitials=@OverrideInitials, Comment=@Comment where ResultId=@ResultId";
                SqlCommand mCommand = new SqlCommand(mQuery, mResultsDbConn);
                mCommand.Parameters.AddWithValue("ResultId", ResultId);
                mCommand.Parameters.AddWithValue("OverrideInitials", OverrideInitials);
                mCommand.Parameters.AddWithValue("Comment", Comment);

                int iRowsUpdated = mCommand.ExecuteNonQuery();
            }
            finally
            {
                DisconnectFromDatabase();
            }
        }

        /// <summary>
        /// Gets the list of Fail Percents ordered by the latest run first
        /// </summary>
        /// <param name="ConfigRecord">The config record that contains the needed information to connect to the database</param>
        /// <param name="TestSuiteId">The identifier for a test suite</param>
        /// <param name="HowManyToReturn">The number of results to return</param>
        public static List<double> GetFailedPercent(DlkResultsDatabaseConfigRecord ConfigRecord, int TestSuiteId, int HowManyToReturn)
        {
            List<double> mResult = new List<double>();
            try
            {
                // connect to database
                CreateConnectionString(ConfigRecord);
                ConnectToDatabase(mDefaultDbConnectionTimeInSec);
                List<int> mRunIds = GetCompletedRunIdsFromTestRunBySuiteId(TestSuiteId);
                mRunIds.Sort();
                mRunIds.Reverse();
                for (int i = 0; i < HowManyToReturn; i++)
                {
                    if (i >= mRunIds.Count)
                    {
                        mResult.Add(-1);
                    }
                    else
                    {
                        int mRunId = mRunIds[i];
                        DlkTestRunRowRecord mTestRunRow = GetTestRunRow(mRunId);
                        if (mTestRunRow.mExecutedCount == 0)
                        {
                            mResult.Add(-1);
                        }
                        else
                        {
                            double mPrct = (double)mTestRunRow.mFailCount / (double)mTestRunRow.mExecutedCount;
                            mPrct = mPrct * 100;
                            mPrct = Math.Round(mPrct, 2, MidpointRounding.AwayFromZero);
                            mResult.Add(mPrct);
                        }
                    }
                }
            }
            finally
            {
                DisconnectFromDatabase();
            }
            return mResult;
        }
        #endregion
        
    }
}
