 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPLC_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
								
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Labor", "xpath","//div[@class='navItem'][.='Project Labor']").Click();
new Control("Manage Project Labor Categories (PLC)", "xpath","//div[@class='navItem'][.='Manage Project Labor Categories (PLC)']").Click();


											Driver.SessionLogger.WriteLine("MAINFORMTABLE");


												
				CPCommon.CurrentComponent = "PJMPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLC] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMPLC_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLC_MainTable.Exists());

											Driver.SessionLogger.WriteLine("BILLING RATES");


												
				CPCommon.CurrentComponent = "PJMPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLC] Perfoming VerifyExists on BillingRatesLink...", Logger.MessageType.INF);
			Control PJMPLC_BillingRatesLink = new Control("BillingRatesLink", "ID", "lnk_1000642_PJMPLC_BILLLABCAT_PLC");
			CPCommon.AssertEqual(true,PJMPLC_BillingRatesLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPLC";
							CPCommon.WaitControlDisplayed(PJMPLC_BillingRatesLink);
PJMPLC_BillingRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLC] Perfoming VerifyExist on BillingRatesTable...", Logger.MessageType.INF);
			Control PJMPLC_BillingRatesTable = new Control("BillingRatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLC_LABCATRTSCH_BILL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLC_BillingRatesTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLC] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control PJMPLC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMPLC_MainForm);
IWebElement formBttn = PJMPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

