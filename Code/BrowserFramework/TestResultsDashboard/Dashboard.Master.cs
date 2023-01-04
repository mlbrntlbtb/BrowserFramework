using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLib.DlkRecords;

namespace TestResultsDashboard
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        public static DlkResultsDatabaseConfigRecord mConfigRec
        {
            get
            {
                if (_ConfigRec == null)
                {
                    _ConfigRec = new DlkResultsDatabaseConfigRecord(@"MAKAPT294VS", "DashboardResults", "sa", "Password1");
                }
                return _ConfigRec;
            }
        }
        private static DlkResultsDatabaseConfigRecord _ConfigRec;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }

            /*old: allowed adhoc*/
            //Boolean bIncludeAdhoc = false;
            // Handle if it is an adhoc report
            //String mIncludeAdhoc = Request.QueryString["IncludeAdhoc"];
            //if (mIncludeAdhoc != null)
            //{
            //    bIncludeAdhoc = Convert.ToBoolean(mIncludeAdhoc);
            //}
            //lnkSummary.NavigateUrl = "~/default.aspx?IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary1.NavigateUrl = @"~/default.aspx?NumberOfDays=1&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary3.NavigateUrl = @"~/default.aspx?NumberOfDays=3&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary5.NavigateUrl = @"~/default.aspx?NumberOfDays=5&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary7.NavigateUrl = @"~/default.aspx?NumberOfDays=7&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary14.NavigateUrl = @"~/default.aspx?NumberOfDays=14&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary30.NavigateUrl = @"~/default.aspx?NumberOfDays=30&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary60.NavigateUrl = @"~/default.aspx?NumberOfDays=60&IncludeAdhoc=" + bIncludeAdhoc.ToString();
            //lnkSummary90.NavigateUrl = @"~/default.aspx?NumberOfDays=90&IncludeAdhoc=" + bIncludeAdhoc.ToString();

            lnkSummary.NavigateUrl = "~/default.aspx?Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary1.NavigateUrl = @"~/default.aspx?NumberOfDays=1&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary3.NavigateUrl = @"~/default.aspx?NumberOfDays=3&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary5.NavigateUrl = @"~/default.aspx?NumberOfDays=5&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary7.NavigateUrl = @"~/default.aspx?NumberOfDays=7&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary14.NavigateUrl = @"~/default.aspx?NumberOfDays=14&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary30.NavigateUrl = @"~/default.aspx?NumberOfDays=30&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary60.NavigateUrl = @"~/default.aspx?NumberOfDays=60&Product=" + ddlProjects.SelectedValue.ToString();
            lnkSummary90.NavigateUrl = @"~/default.aspx?NumberOfDays=90&Product=" + ddlProjects.SelectedValue.ToString();
            //lnkRunningSuites.NavigateUrl = @"~/CurrentlyRunningSuites.aspx?&Product=" + ddlProjects.SelectedValue.ToString();
        }

        private void LoadData()
        {
            ddlProjects.Items.Clear();
            ddlProjects.Items.Add("Costpoint_701");
            ddlProjects.Items.Add("Costpoint_711");
            ddlProjects.Items.Add("TimeAndExpense");
            ddlProjects.DataBind();

            String mProduct = Request.QueryString["Product"];
            if (String.IsNullOrWhiteSpace(mProduct))
            {
                ddlProjects.SelectedIndex = ddlProjects.Items.IndexOf(ddlProjects.Items.FindByText("Costpoint_711"));
            }
            else
            {
                try
                {
                    ddlProjects.SelectedIndex = ddlProjects.Items.IndexOf(ddlProjects.Items.FindByText(mProduct));
                }
                catch //if non-existing product is placed in URL, go to default costpoint instead
                {
                    ddlProjects.SelectedIndex = ddlProjects.Items.IndexOf(ddlProjects.Items.FindByText("Costpoint_711"));
                }
            }
        }

        protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                string path = new Uri(url).GetLeftPart(UriPartial.Path);
                string query = "?Product=" + ddlProjects.SelectedValue;
                if (url.Contains("?"))
                {
                    Response.Redirect(path + query);
                }
                else
                {
                    Response.Redirect(url + query);
                }
            }
            catch
            {
                //do nothing
            }
        }
    }
}