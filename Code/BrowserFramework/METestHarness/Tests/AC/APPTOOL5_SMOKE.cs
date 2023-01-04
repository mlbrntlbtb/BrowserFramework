 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPTOOL5_SMOKE : TestScript
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

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Utilities", "xpath","//div[@class='navItem'][.='Accounts Payable Utilities']").Click();
new Control("Update Discount Method for Posted Vouchers", "xpath","//div[@class='navItem'][.='Update Discount Method for Posted Vouchers']").Click();


												
				CPCommon.CurrentComponent = "APPTOOL5";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL5] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPTOOL5_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPTOOL5_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL5";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL5] Perfoming VerifyExists on SelectionRanges_Option_FiscalYear...", Logger.MessageType.INF);
			Control APPTOOL5_SelectionRanges_Option_FiscalYear = new Control("SelectionRanges_Option_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ONE_RANGE']");
			CPCommon.AssertEqual(true,APPTOOL5_SelectionRanges_Option_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL5";
							CPCommon.WaitControlDisplayed(APPTOOL5_MainForm);
IWebElement formBttn = APPTOOL5_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

