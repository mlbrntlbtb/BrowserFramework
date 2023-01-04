 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CheckOutTest : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
                /* Read File */
                // STUB READ file save to targetUrl
                string targetUrl = "http://resourcescheduler.deltek.com/resourcescheduler/SchedDtl.asp?ID=-1999272643";

                /* Go to URL */
                Driver.Instance.Url = targetUrl;

                /* Click Checkin */
                try
                {
                    Driver.SessionLogger.WriteLine("Clicking check-out...", Logger.MessageType.INF);
                    IWebElement checkin = Driver.Instance.FindElements(By.Id("chkCheckOut")).FirstOrDefault();
                    if (checkin is null)
                    {
                        throw new Exception("Cannot find check-out checkbox.");
                    }
                    checkin.Click();
                    Driver.SessionLogger.WriteLine("Successfully clicked check-out...", Logger.MessageType.INF);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking check-out checkbox. [ln 40]" + ex.Message);
                }

                /* Click Save */
                try
                {
                    Driver.SessionLogger.WriteLine("Clicking Save...", Logger.MessageType.INF);
                    IWebElement save = Driver.Instance.FindElements(By.XPath("//*[contains(@onclick, 'saveInstance')]")).FirstOrDefault();
                    if (save is null)
                    {
                        throw new Exception("Cannot find save button.");
                    }
                    save.Click();
                    Driver.SessionLogger.WriteLine("Successfully clicked save...", Logger.MessageType.INF);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error clicking save  button. [ln 40]" + ex.Message);
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

