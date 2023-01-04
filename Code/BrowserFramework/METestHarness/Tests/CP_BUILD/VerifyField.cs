using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class VerifyField : TestScript
    {
        public override bool TestExecute(out string ErrorMessage, string TestEnvironment)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {
                /* Log-in */
                CPCommon.Login(TestEnvironment, out ErrorMessage);

                /* 1 - Navigate to Manage Users */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed)
                        new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Admin", "xpath", "//div[@class='busItem'][.='Admin']").Click();
                    new Control("Security", "xpath", "//div[@class='deptItem'][.='Security']").Click();
                    new Control("System Security", "xpath", "//div[@class='navItem'][.='System Security']").Click();
                    new Control("Manage Users", "xpath", "//div[@class='navItem'][.='Manage Users']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error navigating to Manage Users app ", ex.Message));
                }

                /* 2 - Verify MainForm exists*/
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    CPCommon.AssertEqual(true, new Control("MainForm", "xpath", "//div[@id='0']/form[1]").Exists());
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying main form ", ex.Message));
                }

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
