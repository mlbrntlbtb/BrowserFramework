using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;


namespace TestRunner.Common
{
    public static class DlkReportGenerator
    {
        private static OleDbConnection mConn;
        private static String mReportDB = "Report.accdb";
        private static Boolean bInitialize = false;

        public static void Initialize()
        {
            if (!bInitialize)
            {
                mConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DlkEnvironment.DirReport + mReportDB);
                mConn.Open();

                bInitialize = true;
            }
        }

        public static void ClearReportDB()
        {
            Initialize();
            OleDbCommand cmd = mConn.CreateCommand();
            cmd.CommandText = "DELETE FROM TestSuite";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DELETE FROM TestResults";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DELETE FROM TestSuiteResults";
            cmd.ExecuteNonQuery();
        }

        public static void AddResultManifestRecToDB(DlkTestSuiteManifestRecord rec)
        {
            Initialize();
            OleDbCommand cmd = mConn.CreateCommand();
            cmd.CommandText = "SELECT TestSuite FROM TestSuite WHERE TestSuite='" + rec.suitepath + "'";
            OleDbDataReader oleReader = cmd.ExecuteReader();
            Boolean bHasRows = oleReader.HasRows;
            oleReader.Close();
            if (!bHasRows)
            {
                cmd.CommandText = "INSERT INTO TestSuite (TestSuite, Tags) VALUES ('" + rec.suitepath + "', '" + rec.tag + "')";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "INSERT INTO TestSuiteResults (TestSuite, ResultDirectory) " +
                              "VALUES ('" + rec.suitepath + "', '" + rec.resultsdirectory + "')";
            cmd.ExecuteNonQuery();
        }

        public static void AddResultsToDB(String Suite, String ExecutionDate)
        {
            Initialize();
            List<DlkExecutionQueueRecord> lstResults = DlkTestSuiteResultsFileHandler.GetResults(ExecutionDate);

            foreach (DlkExecutionQueueRecord result in lstResults)
            {
                try
                {
                    OleDbCommand cmd = mConn.CreateCommand();
                    cmd.CommandText = "INSERT INTO TestResults (Script, TestID, ExecutionDate, TestStatus, TestSuite, Duration, Assigned) " +
                                      "VALUES ('" + result.script + "', '" + result.testid + "', '" + ExecutionDate + "', '" + result.teststatus +
                                      "', '" + Suite + "', '" + result.duration + "', '" + result.assigned + "')";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
            }

            //DlkTestSuiteResultsFileHandler.

        }
    }
}
