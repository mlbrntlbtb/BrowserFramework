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
    public partial class WebForm1 : System.Web.UI.Page
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
        Boolean bIncludeAdhoc { get; set; }

        /// <summary>
        /// executes when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            bIncludeAdhoc = false;
            PopulateTables();
        }

        /// <summary>
        /// populates the table data by querying the database and building the html tables
        /// </summary>
        private void PopulateTables()
        {
            tblLatest.Controls.Clear();
            tblSummary.Controls.Clear();
            plcTitle.Controls.Clear();

            int mSuiteCount = 0, mTestsCount = 0, mTestsExecuted = 0, mTestsPassed = 0, mTestsFailed = 0, mDays = 0, mTestsFailedAssertions = 0;

            // Handle various summary reports by days
            String sNumberOfDays = Request.QueryString["NumberOfDays"];
            String mTitle="";
            if (sNumberOfDays == null)
            {
                mTitle = "Summary View - Summary";
                mDays = 0;
            }
            else
            {
                mTitle = "Summary View - " + sNumberOfDays + " Day";
                mDays = Convert.ToInt32(sNumberOfDays);
            }
            Label mTitleCntrl = new Label();
            mTitleCntrl.Text = mTitle;
            plcTitle.Controls.Add(mTitleCntrl);

             // Handle if it is an adhoc report
            //String mIncludeAdhoc = Request.QueryString["IncludeAdhoc"];
            //if (mIncludeAdhoc != null)
            //{
            //    bIncludeAdhoc = Convert.ToBoolean(mIncludeAdhoc);
            //}

            // Handle Product
            String mProduct = Request.QueryString["Product"];
            if (String.IsNullOrEmpty(mProduct))
            {
                mProduct = "Costpoint_711";
            }

            // Provide the link to turn adhoc on/off
            //if (bIncludeAdhoc)
            //{
            //    if (mDays == 0)
            //    {
            //        lnkIncludeAdhoc.Text = "Remove Adhoc Results";
            //        lnkIncludeAdhoc.NavigateUrl = @"~/default.aspx?IncludeAdhoc=false";
            //    }
            //    else
            //    {
            //        lnkIncludeAdhoc.Text = "Remove Adhoc Results";
            //        lnkIncludeAdhoc.NavigateUrl = @"~/default.aspx?IncludeAdhoc=false&NumberOfDays=" + mDays.ToString();
            //    }
            //}
            //else
            //{
            //    if (mDays == 0)
            //    {
            //        lnkIncludeAdhoc.Text = "Include Adhoc Results";
            //        lnkIncludeAdhoc.NavigateUrl = @"~/default.aspx?IncludeAdhoc=true";
            //    }
            //    else
            //    {
            //        lnkIncludeAdhoc.Text = "Include Adhoc Results";
            //        lnkIncludeAdhoc.NavigateUrl = @"~/default.aspx?IncludeAdhoc=true&NumberOfDays=" + mDays.ToString();
            //    }
            //}

            List<DlkTestRunRowRecord> mRecs = DlkDatabaseAPI.GetSummaryResults(Dashboard.mConfigRec, mDays, mProduct.ToLower(), bIncludeAdhoc, out mSuiteCount, out mTestsCount,
                out mTestsExecuted, out mTestsPassed, out mTestsFailed, out mTestsFailedAssertions);

            // create Fail Prct
            double mOverallFailPrct = (double)mTestsFailed / (double)mTestsExecuted;
            mOverallFailPrct = mOverallFailPrct * 100;
            mOverallFailPrct = Math.Round(mOverallFailPrct, 2, MidpointRounding.AwayFromZero);

            // create Failed Assertion Prct
            double mOverallFailedAssertionPrct = (double)mTestsFailedAssertions / (double)mTestsExecuted;
            mOverallFailedAssertionPrct = mOverallFailedAssertionPrct * 100;
            mOverallFailedAssertionPrct = Math.Round(mOverallFailedAssertionPrct, 2, MidpointRounding.AwayFromZero);

            // create the summary table
            TableHeaderRow mHeaderRowSummary = DlkTableHelper.CreateHeaderRow(new List<string>() { 
                "Suite Count", "Test Count", "Tests Executed", "Tests Passed", "Tests Failed", "% Failed"});
            tblSummary.Controls.Add(mHeaderRowSummary);

            TableRow mSummaryRow = DlkTableHelper.CreateRow(0, new List<string>() { 
                mSuiteCount.ToString(), mTestsCount.ToString(), mTestsExecuted.ToString(), mTestsPassed.ToString(), mTestsFailed.ToString(),
                mOverallFailPrct.ToString() + @"%"
            });
            tblSummary.Controls.Add(mSummaryRow);

            // create the latest results table
            TableHeaderRow mHeaderRow = DlkTableHelper.CreateHeaderRow(
                new List<string>() { "Suite Name", "Machine", "Width:125px~Test Suite Start", "Width:125px~Test Suite End", 
                "Width:75px~Total Tests", "Width:95px~Tests Executed", "Passed", "Failed","Width:75px~% Failed",
                "Width:95px~% Failed Prev.", "Width:95px~% Failed Prev. 2", "Width:95px~% Failed Prev. 3"});
            tblLatest.Controls.Add(mHeaderRow);

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
                    @"&RunId=" + mRec.mRunId.ToString() + @"&Product=" + mProduct + "\">" + mSuiteName + @"</a>";

                List<double> mFailPcts = DlkDatabaseAPI.GetFailedPercent(Dashboard.mConfigRec, mRec.mTestSuiteId, 4);
                List<String> mFailPctDisp = new List<string>();
                foreach (double mFailPct in mFailPcts)
                {
                    if (mFailPct < 0)
                    {
                        mFailPctDisp.Add("-");
                    }
                    else
                    {
                        mFailPctDisp.Add(mFailPct.ToString() + @"%");
                    }
                }
                TableRow mRow = DlkTableHelper.CreateRow(i, new List<string>() { mSuiteLink,
                    mRec.mMachine,mStart,mEnd, mRec.mTestCount.ToString(),mRec.mExecutedCount.ToString(),
                    mRec.mPassCount.ToString(),mRec.mFailCount.ToString(),
                    mFailPctDisp[0].ToString(), mFailPctDisp[1].ToString(), 
                    mFailPctDisp[2].ToString(), mFailPctDisp[3].ToString() 
                });
                tblLatest.Controls.Add(mRow);
            }
        }
    }
}