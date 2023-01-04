using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HP_ACT_SearchCopyAndDeleteActivities : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            try
            {
                StormCommon.Login("Mel_2.0", out ErrorMessage);
                Control banner = new Control("SideBar", "XPATH", "//*[contains(@class,'banner-left')]");
                StormCommon.WaitControlDisplayed(banner);
                
                //1. navigate to Activity
                Driver.SessionLogger.WriteLine("Navigate to Activity");
                Control sideBar_Activity = new Control("SideBar", "XPATH", "//*[contains(@class,'nav-item')][@data-app-id='Activity']");
                Thread.Sleep(1000);

                sideBar_Activity.Click();
                Thread.Sleep(1000);

                //2. Search Activity
                Driver.SessionLogger.WriteLine("Search Activity");
                new Control("SearchBox", "XPATH", "//*[contains(@class,'section-search')]//input").SendKeys("Activity Dette One");
                Thread.Sleep(500);
                //3. Select Activity
                new Control("SearchListItem", "XPATH", "//*[contains(@class,'search-result')][contains(.,'Activity Dette One')]").Click();
                Thread.Sleep(1000);

                //4. Copy the Activity
                new Control("CopyButton", "XPATH", "//*[@action-bar-data='Follow-up']").Click();
                Thread.Sleep(1000);

                //5. Set Activity Name to Follow-up: Activity Dette One
                Control activityNameControl = new Control("ActivityName", "XPATH", "//*[@data-id='Activity.Subject']//input");
                activityNameControl.SendKeys("Follow-up: Activity Dette One", true);
                banner.Click();
                Thread.Sleep(1000);

                //6. Save
                new Control("SaveButton", "XPATH", "//*[@data-action-key='core-save']").Click();
                Thread.Sleep(1000);

                //7. Verify Activity Name
                StormCommon.WaitScreenGetsReady();
                var activityName = new Control("ActivityName", "XPATH", "//*[@data-id='Activity.Subject']//*[contains(@class,'Text')]");
                StormCommon.WaitControlDisplayed(activityName);
                string ActivityName = activityName.GetAttributeValue("textContent");
                if (ActivityName != "Follow-up: Activity Dette One")
                    throw new Exception("Incorrect Activity name");
                Thread.Sleep(1000);

                //Verify QuickEdit When
                //new Control("QuickEdit", "XPATH", "//*[@data-id='Activity.TimeRecurrence']")

                //Verify QuickEdit When
                //string QuickEditType = new Control("QuickEdit", "XPATH", "//*[@data-id='Activity.Type']//*[contains(@class,'core-field')]").GetAttributeValue("textContent");
                //if (QuickEditType == "TypeDesc")
                //    throw new Exception("Incorrect Type");
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
