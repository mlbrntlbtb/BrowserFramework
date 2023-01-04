using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HP_ACT_SearchAndEditExistingActivitiesPerTab : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            try
            {
                StormCommon.Login("Nikka2.0", out ErrorMessage);
                Control banner = new Control("SideBar", "XPATH", "//*[contains(@class,'banner-left')]");
                StormCommon.WaitControlDisplayed(banner);
                
                //navigate to Activity
                Driver.SessionLogger.WriteLine("Navigate to Activity");
                Control sideBar_Activity = new Control("SideBar", "XPATH", "//*[contains(@class,'nav-item')][@data-app-id='Activity']");
                Thread.Sleep(1000);

                sideBar_Activity.Click();
                Thread.Sleep(1000);

                //Search Activity
                Driver.SessionLogger.WriteLine("Search Activity");
                new Control("SearchBox", "XPATH", "//*[contains(@class,'section-search')]//input").SendKeys("Activity Unit Test");
                new Control("SearchListItem", "XPATH", "//*[contains(@class,'search-result')][contains(.,'Activity Unit Test')]").Click();
                Thread.Sleep(1000);

                //Edit the Activity
                //////new Control("CopyButton", "XPATH", "//*[@action-bar-data='Follow-up']").Click();
                Thread.Sleep(1000);

                //////////TO BE UPDATED
                ////////Control activityNameControl = new Control("ActivityName", "XPATH", "//*[@data-id='Activity.Subject']//input");
                ////////activityNameControl.SendKeys("Follow-up: Activity Unit Test", true);
                ////////banner.Click();
                ////////Thread.Sleep(1000);

                //Save
                new Control("SaveButton", "XPATH", "//*[@data-action-key='core-save']").Click();
                Thread.Sleep(1000);

                //Verify Activity Name
                StormCommon.WaitScreenGetsReady();
                string ActivityName = new Control("ActivityName", "XPATH", "//*[@data-id='Activity.Subject']//*[contains(@class,'Text')]").GetAttributeValue("textContent");
                if (ActivityName != "Follow-up: Activity Unit Test")
                    throw new Exception("Incorrect Activity name");
                Thread.Sleep(1000);

                //Verify QuickEdit When
                //new Control("QuickEdit", "XPATH", "//*[@data-id='Activity.TimeRecurrence']")

                //Verify QuickEdit When
                string QuickEditType = new Control("QuickEdit", "XPATH", "//*[@data-id='Activity.Type']//*[contains(@class,'core-field')]").GetAttributeValue("textContent");
                if (QuickEditType == "TypeDesc")
                    throw new Exception("Incorrect Type");

                //Revert
            }
            catch (Exception ex)
            {
                ret = false;
                ErrorMessage = ex.Message;
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
