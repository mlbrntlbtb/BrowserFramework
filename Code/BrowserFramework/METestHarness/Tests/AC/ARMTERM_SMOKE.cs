 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMTERM_SMOKE : TestScript
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
new Control("Manage Customer Terms", "xpath","//div[@class='navItem'][.='Manage Customer Terms']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARMTERM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMTERM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARMTERM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARMTERM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARMTERM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMTERM] Perfoming VerifyExists on Description...", Logger.MessageType.INF);
			Control ARMTERM_Description = new Control("Description", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_TERMS_DC']");
			CPCommon.AssertEqual(true,ARMTERM_Description.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARMTERM";
							CPCommon.WaitControlDisplayed(ARMTERM_MainForm);
IWebElement formBttn = ARMTERM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

