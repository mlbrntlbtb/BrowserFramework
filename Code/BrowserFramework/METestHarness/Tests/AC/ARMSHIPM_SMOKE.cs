 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMSHIPM_SMOKE : TestScript
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
new Control("Manage Shipping Methods", "xpath","//div[@class='navItem'][.='Manage Shipping Methods']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARMSHIPM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMSHIPM] Perfoming VerifyExist on mainForm_mainFormTable...", Logger.MessageType.INF);
			Control ARMSHIPM_mainForm_mainFormTable = new Control("mainForm_mainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMSHIPM_mainForm_mainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMSHIPM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMSHIPM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARMSHIPM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARMSHIPM_MainForm.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARMSHIPM";
							CPCommon.WaitControlDisplayed(ARMSHIPM_MainForm);
IWebElement formBttn = ARMSHIPM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

