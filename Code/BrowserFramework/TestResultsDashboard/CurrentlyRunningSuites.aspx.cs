using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestResultsDashboard.DlkHtmlUtilities;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;

namespace TestResultsDashboard
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        /// <summary>
        /// This is the pointer to the xml file that contains the database connection details
        /// </summary>
        String mConfig
        {
            get
            {
                return Request.PhysicalApplicationPath + @"Configs\ResultsDatabaseConfig.xml";
            }
        }

        /// <summary>
        /// executes when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateTables();
        }

        /// <summary>
        /// populates the table data by querying the database and building the html tables
        /// </summary>
        private void PopulateTables()
        {
            tblCurrent.Controls.Clear();
            tblCurrentSummary.Controls.Clear();

            int mSuiteCount = 0, mTestsCount = 0, mTestsExecuted = 0, mTestsPassed = 0, mTestsFailed = 0, mTestsFailedAssertions = 0;

            // Handle Product
            String mProduct = Request.QueryString["Product"];
            if (mProduct != null)
            {
                mProduct = "Costpoint_711";
            }
            List<DlkTestRunRowRecord> mRecs = DlkDatabaseAPI.GetCurrentlyRunningResults(Dashboard.mConfigRec, mProduct, out mSuiteCount, out mTestsCount,
                out mTestsExecuted, out mTestsPassed, out mTestsFailed, out mTestsFailedAssertions);

            // create the summary table
            TableHeaderRow mHeaderRowSummary = DlkTableHelper.CreateHeaderRow(new List<string>() { 
                "Suite Count", "Test Count", "Tests Executed", "Tests Passed", "Tests Failed"});
            tblCurrentSummary.Controls.Add(mHeaderRowSummary);
            TableRow mSummaryRow = DlkTableHelper.CreateRow(0, new List<string>() { 
                mSuiteCount.ToString(), mTestsCount.ToString(), mTestsExecuted.ToString(), mTestsPassed.ToString(), mTestsFailed.ToString()});
            tblCurrentSummary.Controls.Add(mSummaryRow);

            // create the latest results table
            TableHeaderRow mHeaderRow = DlkTableHelper.CreateHeaderRow(
                new List<string>() { "Suite Name", "Machine", "Test Suite Start", "Test Suite End", 
                "Total Tests", "Tests Executed", "Passed", "Failed", "Failed Assertions"});
            tblCurrent.Controls.Add(mHeaderRow);

            for (int i = 0; i < mRecs.Count; i++)
            {
                DlkTestRunRowRecord mRec = mRecs[i];
                String mSuiteName = DlkDatabaseAPI.GetTestSuiteNameByRunId(Dashboard.mConfigRec, mRec.mRunId);

                String mStart = "";
                if (mRec.mTestSuiteStart != null)
                {
                    mStart = mRec.mTestSuiteStart.ToString();
                }

                String mEnd = "";
                if (mRec.mTestSuiteEnd != null)
                {
                    mEnd = mRec.mTestSuiteEnd.ToString();
                }

                String mSuiteLink = @"<a href=" + "\"SuiteDetails.aspx?TestSuiteID=" + mRec.mTestSuiteId.ToString() +
    @"&RunId=" + mRec.mRunId.ToString() + "\">" + mSuiteName + @"</a>";

                TableRow mRow = DlkTableHelper.CreateRow(i, new List<string>() 
                {  
                    mSuiteLink,mRec.mMachine,mStart,mEnd,
                    mRec.mTestCount.ToString(),mRec.mExecutedCount.ToString(),
                    mRec.mPassCount.ToString(),mRec.mFailCount.ToString(),mRec.mFailedAssertions.ToString()
                });
                tblCurrent.Controls.Add(mRow);
            }
        }

    }
}