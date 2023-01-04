 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPESSUD_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Set on SearchApplications...", Logger.MessageType.INF);
			Control CP7Main_SearchApplications = new Control("SearchApplications", "ID", "appFltrFld");
			CP7Main_SearchApplications.Click();
CP7Main_SearchApplications.SendKeys("AOPESSUD", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


												
				CPCommon.CurrentComponent = "AOPESSUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPESSUD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPESSUD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPESSUD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPESSUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPESSUD] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPESSUD_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPESSUD_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPESSUD";
							CPCommon.WaitControlDisplayed(AOPESSUD_MainForm);
IWebElement formBttn = AOPESSUD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
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

