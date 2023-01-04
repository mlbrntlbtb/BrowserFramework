using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;


namespace METestHarness.Tests
{
    public class HP_ACT_CreateSearchAndDeleteActivities : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            try
            {
                StormCommon.Login("Mel_2.0", out ErrorMessage);
                StormCommon.WaitScreenGetsReady();

                //1. navigate to Activity
                Driver.SessionLogger.WriteLine("Navigate to Activity");
                Control sideBar_Activity = new Control("SideBar", "XPATH", "//*[contains(@class,'nav-item')][@data-app-id='Activity']");
                sideBar_Activity.Click();

                //2. click new activity link
                StormCommon.WaitScreenGetsReady();
                Driver.SessionLogger.WriteLine("Click New Activity Link");
                new Control("NewActivityLink", "XPATH", "//*[@title='New Activity']//*[contains(@class,'link')]").Click();

                //3. Select from dropdown: Activity Name = "Staff Meeting"
                StormCommon.WaitScreenGetsReady();
                Driver.SessionLogger.WriteLine("Set Activity Name Dropdown value 'Staff Meeting'");
                new Control("ActivityName", "XPATH", "//*[@data-id='Activity.Subject']//input").SendKeys("Staff Meeting");

                //Select from dropdown: OverviewTab>Type = "Appointment"
                //StormCommon.WaitScreenGetsReady();
                //Driver.SessionLogger.WriteLine("Set Type dropdown value 'Appointment'");
                //new Control("ActivityType","XPATH", "//*[@data-id='Activity.Type']//input").SendKeys("Appointment");

                //4. Click Save
                new Control("SaveButton", "XPATH", "//*[@data-action-key='core-save']").Click();
                Thread.Sleep(1000);

                //5. Verify Activity Name
                StormCommon.WaitScreenGetsReady();
                var activityName = new Control("ActivityName", "XPATH", "//*[@data-id='Activity.Subject']//*[contains(@class,'Text')]");
                StormCommon.WaitControlDisplayed(activityName);
                string ActivityName = activityName.GetAttributeValue("textContent");
                if (ActivityName != "Staff Meeting")
                    throw new Exception("Incorrect Activity name");
                Thread.Sleep(1000);

                //Verify QuickEdit Value When = current date
                
                //Verify QuickEdit Value Type = "Appointment"

                //Set Search Activity "Anderson & Associates, LLC"

                //Select without enter: "Anderson & Associates, LLC"

                //Set Search Activity "Staff Meeting"

                //Select without enter: "Staff Meeting"

                //Delete record
                //////new Control("ActionsMenu", "XPATH", "//*[@action-bar-data='actionsMenu']").Click();
                //////new Control("ListItem", "XPATH_DISPLAY", "//ul//a[.='Delete']").Click();

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

