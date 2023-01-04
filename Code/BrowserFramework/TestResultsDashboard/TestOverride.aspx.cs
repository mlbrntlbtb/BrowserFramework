using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLib.DlkSystem;

namespace TestResultsDashboard
{
    public partial class WebForm4 : System.Web.UI.Page
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
        int mResultId { get; set; }

        /// <summary>
        /// executes when the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                OverrideStatus();
            }

            // attach Simple validation script
            ClientScript.RegisterClientScriptInclude("regJSval",@"Scripts\OverrideStatusVal.js");
            OverideStatus.Attributes.Add("onclick", "OverrideStatusVal()");

            
            // check to see that we have RunId and TestSuiteId if not error
            String RunId = Request.QueryString["RunId"];
            if (RunId == null)
            {
                PopulateError();
                return;
            }
            else
            {
                hRunId.Value = RunId;
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
                hTestSuiteID.Value = SuiteId;
                mTestSuiteId = Convert.ToInt32(SuiteId);
            }

            String ResultId = Request.QueryString["ResultId"];
            if (ResultId == null)
            {
                PopulateError();
                return;
            }
            else
            {
                hResultId.Value = ResultId;
                mResultId = Convert.ToInt32(ResultId);
            }

        }

        /// <summary>
        /// The error shown if TestSuiteId or RunId is null (not supplied)
        /// </summary>
        private void PopulateError()
        {
            Label mLabel = new Label();
            mLabel.Text = "Error: Test Suite Details were not found.";
        }

        /// <summary>
        /// overrides the status in the database and redirects the user back to the suite details page
        /// </summary>
        private void OverrideStatus()
        {
            Boolean bPerformOveride = true;
            String mInitials = "", mComment = "";
            if (Initials.Text.Length < 1)
            {
                bPerformOveride = false;
            }
            else
            {
                mInitials = Initials.Text.Trim().ToUpper();
            }
            if (Comment.Text.Length < 1)
            {
                bPerformOveride = false;
            }
            else
            {
                mComment = Comment.Text.Trim();
            }
            if (bPerformOveride)
            {
                DlkDatabaseAPI.OverrideTestStatus(Dashboard.mConfigRec, Convert.ToInt32(hResultId.Value), "Passed", mInitials, mComment);
                String mSuiteLink = @"SuiteDetails.aspx?TestSuiteID=" + hTestSuiteID.Value + @"&RunId=" + hRunId.Value;
                Response.Redirect(mSuiteLink);
            }
        }
    }
}