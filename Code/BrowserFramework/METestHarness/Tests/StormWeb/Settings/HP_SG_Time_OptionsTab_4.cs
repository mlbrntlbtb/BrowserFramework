using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HP_SG_Time_OptionsTab_4 : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {

            bool ret = true;
            ErrorMessage = string.Empty;
            string currentDate = DateTime.Now.ToString("MM/dd/yyyy");

            try
            {
                //Environment Login
                //StormCommon.Login("Mel_2.0", out ErrorMessage);
                Driver.Instance.Url = "http://makapt618vs/DeltekPS/app/#!";

                //Set UserID
                Driver.SessionLogger.WriteLine("Set UserID");
                Control User = new Control("TextBox", "XPATH", "//input[@id='userID']");
                User.SendKeys("ADMIN");

                //Set Password
                Driver.SessionLogger.WriteLine("Set Password");
                Control Pass = new Control("TextBox", "XPATH", "//input[@id='password']");
                Pass.SendKeys("");

                //Set Database
                Driver.SessionLogger.WriteLine("Set Database");
                Control DB = new Control("ComboBox", "XPATH", "//input[@name='dbDdwn']");
                DB.SendKeys("AutoDB_FWDev_03 (MAKAPT676VS)");

                //Click Database Item
                Driver.SessionLogger.WriteLine("Click Database Item");
                Control DBItem = new Control("ComboBox", "XPATH", "//div[@class='ddwnTableDiv']//div[@class='string truncate_text'][contains(text(),'AutoDB_FWDev_03')]");
                DBItem.Click();

                //Click Login
                Driver.SessionLogger.WriteLine("Click Login");
                Control Login = new Control("Button", "XPATH", "//button[@data-id='login']");
                Login.Click();
                StormCommon.WaitScreenGetsReady();
                
                //1. Navigate to Time
                Driver.SessionLogger.WriteLine("Navigate to Time > Options");
                Control Nav = new Control("SideBar", "XPATH", "//div[@class='app-list']//*[@data-app-id='TimeConfig']");
                Nav.Click();
                StormCommon.WaitScreenGetsReady();
                Nav = new Control("SideBar", "XPATH", "//div[@class='app-list']//*[@data-app-id='TimeSettingsOptions']");
                Nav.Click();
                StormCommon.WaitScreenGetsReady();

                //2 Set Time Entered In
                Driver.SessionLogger.WriteLine("Set Time Entered In");
                Control Time = new Control("ComboBox", "XPATH", "//input[@name='Increment']");
                Time.SendKeys("Quarter");

                //3 Click Time Item
                Driver.SessionLogger.WriteLine("Click Time Item");
                Control TimeItem = new Control("ComboBox", "XPATH", "//div[@class='ddwnTableDiv']//div[@class='string truncate_text'][contains(text(),'Tenth')]");
                TimeItem.Click();

                //4 Set Start End Time Entry
                Driver.SessionLogger.WriteLine("Set Start End Time Entry");
                Control StartEnd = new Control("ComboBox", "XPATH", "//input[@name='StartEndTimeBy']");
                StartEnd.SendKeys("By Day");

                //5 Click Start End Time Entry Item
                Driver.SessionLogger.WriteLine("Click Start End Time Entry Item");
                Control StartEndItem = new Control("ComboBox", "XPATH", "//div[@class='ddwnTableDiv']//div[@class='string truncate_text'][contains(text(),'By Project')]");
                StartEndItem.Click();

                //6 Set Check Hours Against Expected
                Driver.SessionLogger.WriteLine("Set Check Hours Against Expected");
                Control CheckHours = new Control("ComboBox", "XPATH", "//input[@name='CheckHours']");
                CheckHours.SendKeys("Warning if Under");                

                //7 Click Save
                Driver.SessionLogger.WriteLine("Click Save");
                Control Save = new Control("ComboBox", "XPATH", "//button[@data-id='save']");
                Save.Click();                
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
