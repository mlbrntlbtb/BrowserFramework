using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TestResultsDashboard.DlkHtmlUtilities;

namespace TestResultsDashboard
{
    public partial class WebForm3 : System.Web.UI.Page
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
        int mRunId { get; set; }
        int mTestSuiteId { get; set; }
        String mProduct
        {
            get { return Request.QueryString["Product"]; }
        }
        /// <summary>
        /// executes when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // check to see that we have RunId and TestSuiteId if not error
            String RunId = Request.QueryString["RunId"];
            if (RunId == null)
            {
                PopulateError();
                return;
            }
            else
            {
                mRunId = Convert.ToInt32(RunId);
            }
            String SuiteId = Request.QueryString["TestSuiteId"];
            if (SuiteId == null)
            {
                PopulateError();
                return;
            }
            else
            {
                mTestSuiteId = Convert.ToInt32(SuiteId);
            }

            // if we do, create the data
            BuildPageContent();
        }

        /// <summary>
        /// The error shown if TestSuiteId or RunId is null (not supplied)
        /// </summary>
        private void PopulateError()
        {
            Label mLabel = new Label();
            mLabel.Text = "Error: Test Suite Details were not found.";
            SuiteDetailsDescription.Controls.Add(mLabel);
            PlaceHolder1.Controls.Clear();
        }

        /// <summary>
        /// populates the data
        /// </summary>
        private void BuildPageContent()
        {
            //clear the control data
            SuiteDetailsDescription.Controls.Clear();
            PlaceHolder1.Controls.Clear();

            // add the description
            String mSuiteName = DlkDatabaseAPI.GetTestSuiteNameByRunId(Dashboard.mConfigRec, mRunId);
            Label mLabel = new Label();
            mLabel.Text = "This report provides the test execution data for a given suite.";
            SuiteDetailsDescription.Controls.Add(mLabel);

            // table with summary information about the suite run
            Table mSuiteSummaryTbl = CreateSummmaryTable(mSuiteName);
            PlaceHolder1.Controls.Add(mSuiteSummaryTbl);
            HtmlGenericControl paragraph = new HtmlGenericControl("p");
            PlaceHolder1.Controls.Add(paragraph);

            // create the table detailing all the test executions
            Table mRunDetailsTable = CreateRunDetailsTable();
            PlaceHolder1.Controls.Add(mRunDetailsTable);
            paragraph = new HtmlGenericControl("p");
            PlaceHolder1.Controls.Add(paragraph);

            // create a table with links to the various runs
            Table mRunTable = CreateRunsTable(mSuiteName);
            if (mRunTable != null)
            {
                mLabel = new Label();
                mLabel.Text = "Other executions of this suite:";
                PlaceHolder1.Controls.Add(mLabel);
                PlaceHolder1.Controls.Add(mRunTable);
            }
        }

        /// <summary>
        /// Creates the table that details the basic summary information about the run
        /// </summary>
        /// <param name="SuiteName"></param>
        /// <returns></returns>
        private Table CreateSummmaryTable(String SuiteName)
        {
            Table mSuiteSummaryTbl = new Table();
            mSuiteSummaryTbl.CssClass = "TblStyle2";

            DlkTestRunRowRecord mSummaryRec = DlkDatabaseAPI.GetSummaryResultsByRunId(Dashboard.mConfigRec, mRunId);
            String mSuiteStart = "";
            if (mSummaryRec.mTestSuiteStart != null)
            {
                mSuiteStart = mSummaryRec.mTestSuiteStart.ToString();
            }
            String mSuiteEnd = "";
            if (mSummaryRec.mTestSuiteEnd != null)
            {
                mSuiteEnd = mSummaryRec.mTestSuiteEnd.ToString();
            }

            // create the latest results table
            TableHeaderRow mSuiteSummaryHeaderRow = DlkTableHelper.CreateHeaderRow(
                new List<string>() { "Suite Name", "Machine", "Test Suite Start", "Test Suite End", 
                "Total Tests", "Tests Executed", "Passed", "Failed"});
            mSuiteSummaryTbl.Controls.Add(mSuiteSummaryHeaderRow);

            TableRow mSuiteSummaryRow = DlkTableHelper.CreateRow(0, new List<string>() { SuiteName,
                    mSummaryRec.mMachine,mSuiteStart,mSuiteEnd,
                mSummaryRec.mTestCount.ToString(),mSummaryRec.mExecutedCount.ToString(),
                mSummaryRec.mPassCount.ToString(),mSummaryRec.mFailCount.ToString()});
            mSuiteSummaryTbl.Controls.Add(mSuiteSummaryRow);

            return mSuiteSummaryTbl;
        }

        /// <summary>
        /// Creates the Run Details table showing all the test results for a suite
        /// </summary>
        /// <returns></returns>
        private Table CreateRunDetailsTable()
        {
            // get the TestResults rows for this run
            List<DlkTestResultsRowRecord> mRecs = DlkDatabaseAPI.GetSuiteDetails(Dashboard.mConfigRec, mTestSuiteId, mRunId);

            Table mTable = new Table();
            mTable.CssClass = "TblStyle2";
            //TableHeaderRow mHeaderRow = DlkTableHelper.CreateHeaderRow(new List<string>() { 
            //    "Width:60px~Links","Width:175px~Test Name","Width:100px~Test Instance", "Width:100px~Test Status",
            //    "Width:105px~Fail Type","Width:255px~Fail Details",
            //    "Width:125px~Test Start", "Width:125px~Test End", "Width:125px~Time Elapsed", 
            //    "Width:350px~Test File", "Width:350px~Comment"
            //});
            TableHeaderRow mHeaderRow = DlkTableHelper.CreateHeaderRow(new List<string>() { 
                "Width:60px~Links","Width:175px~Test Name","Width:100px~Test Instance", "Width:100px~Test Status",
                "Width:105px~Fail Step", "Width:255px~Fail Details",
                "Width:125px~Test Start", "Width:125px~Test End", "Width:125px~Time Elapsed", 
                "Width:350px~Test File", "Width:350px~Comment"
            });
            mTable.Controls.Add(mHeaderRow);

            for (int i = 0; i < mRecs.Count; i++)
            {
                String mLinks = "";

                DlkTestResultsRowRecord mRec = mRecs[i];
                String mStart = "";
                if (mRec.mTestStart != null)
                {
                    mStart = mRec.mTestStart.ToString();
                }
                String mEnd = "";
                if (mRec.mTestEnd != null)
                {
                    mEnd = mRec.mTestEnd.ToString();
                }


                String mStatus = "";
                if (mRec.mOverrideStatus == "")
                {
                    if (mRec.mTestStatus.ToLower().StartsWith("pass"))
                    {
                        mStatus = "<b><font color=green>" + mRec.mTestStatus + "</font></b>";
                    }
                    else if (mRec.mTestStatus.ToLower().StartsWith("fail"))
                    {
                        mStatus = "<b><font color=red>" + mRec.mTestStatus + "</font></b>";
                        
                        // Jamie and Bob decided to turn off Override ability for now.
                        //String mOverideLink = @"<a href=" + "\"TestOverride.aspx?TestSuiteID=" + mTestSuiteId.ToString() + @"&RunId=" + mRec.mRunId.ToString() + @"&ResultId=" + mRec.mResultId.ToString() + "\">Override</a>";
                        //mLinks = mCommentLink + mOverideLink;
                    }
                    else
                    {
                        mStatus = "<b><font color=orange>" + mRec.mTestStatus + "</font></b>";
                    }
                }
                else
                {
                    mStatus = "<b><font color=purple>" + mRec.mOverrideStatus + "</font></b>";
                }
                //Show Comment regardless of test status
                String mCommentLink = @"<a href=" + "\"TestComment.aspx?TestSuiteID=" + mTestSuiteId.ToString() +
                    @"&RunId=" + mRec.mRunId.ToString() + @"&ResultId=" + mRec.mResultId.ToString() + @"&Product=" + mProduct + "\">Comment</a> &nbsp;";
                mLinks = mCommentLink;

                String mComment = mRec.mComment;
                if (mRec.mComment != "")
                {
                    mComment = "[" + mRec.mOverrideInitials + "]: " + mRec.mComment;
                }
                TableRow mRow = DlkTableHelper.CreateRow(0, new List<string>()
                {
                    mLinks, mRec.mTestName,mRec.mTestInstance.ToString(), mStatus, mRec.mTestStepFailed.ToString() != "0" ? mRec.mTestStepFailed.ToString():"", mRec.mTestStepFailDetails,
                    mStart, mEnd, mRec.mElapsed, mRec.mTestFile, mComment
                });
                mTable.Controls.Add(mRow);
            }
            return mTable;
        }

        /// <summary>
        /// Create a table documenting on execution of the suite
        /// </summary>
        /// <param name="SuiteName"></param>
        /// <returns></returns>
        private Table CreateRunsTable(String SuiteName)
        {
            Table mTable = new Table();
            mTable.CssClass = "TblStyle2";

            TableHeaderRow mHeaderRow = DlkTableHelper.CreateHeaderRow(
    new List<string>() { "Suite Name", "Machine", "Test Suite Start", "Test Suite End", 
                "Total Tests", "Tests Executed", "Passed", "Failed"});
            mTable.Controls.Add(mHeaderRow);

            List<DlkTestRunRowRecord> mRecs = DlkDatabaseAPI.GetAllSuiteRunsBySuiteId(Dashboard.mConfigRec, mTestSuiteId);
            Boolean bOtherRunsExist = false;
            for (int i = 0; i < mRecs.Count; i++)
            {
                DlkTestRunRowRecord mRec = mRecs[i];

                // don't show the currently displayed run
                if (mRec.mRunId == mRunId)
                {
                    continue;
                }
                bOtherRunsExist = true;
                String mSuiteStart = "";
                if (mRec.mTestSuiteStart != null)
                {
                    mSuiteStart = mRec.mTestSuiteStart.ToString();
                }
                String mSuiteEnd = "";
                if (mRec.mTestSuiteEnd != null)
                {
                    mSuiteEnd = mRec.mTestSuiteEnd.ToString();
                }

                String mSuiteLink = @"<a href=" + "\"SuiteDetails.aspx?TestSuiteID=" + mRec.mTestSuiteId.ToString() +
    @"&RunId=" + mRec.mRunId.ToString() + @"&Product=" + mProduct + "\">" + SuiteName + @"</a>";

                TableRow mRow = DlkTableHelper.CreateRow(0, new List<string>() { mSuiteLink,
                    mRec.mMachine,mSuiteStart,mSuiteEnd,
                mRec.mTestCount.ToString(),mRec.mExecutedCount.ToString(),mRec.mPassCount.ToString(),mRec.mFailCount.ToString()});
                mTable.Controls.Add(mRow);
            }

            if (bOtherRunsExist == false)
            {
                return null;
            }

            return mTable;
        }

    }
}