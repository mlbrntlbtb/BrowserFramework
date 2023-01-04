 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMSETNG_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Accounts Receivable Controls", "xpath","//div[@class='navItem'][.='Accounts Receivable Controls']").Click();
new Control("Configure Accounts Receivable Settings", "xpath","//div[@class='navItem'][.='Configure Accounts Receivable Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMSETNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARMSETNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARMSETNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMSETNG] Perfoming VerifyExists on FinanceChargeInformation_NumberOfGraceDays...", Logger.MessageType.INF);
			Control ARMSETNG_FinanceChargeInformation_NumberOfGraceDays = new Control("FinanceChargeInformation_NumberOfGraceDays", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='GRACE_DAYS_NO']");
			CPCommon.AssertEqual(true,ARMSETNG_FinanceChargeInformation_NumberOfGraceDays.Exists());

												
				CPCommon.CurrentComponent = "ARMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMSETNG] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control ARMSETNG_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMSETNG_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMSETNG_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARMSETNG";
							CPCommon.WaitControlDisplayed(ARMSETNG_MainForm);
IWebElement formBttn = ARMSETNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

