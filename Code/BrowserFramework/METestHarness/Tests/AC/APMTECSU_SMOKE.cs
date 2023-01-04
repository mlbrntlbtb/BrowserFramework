 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMTECSU_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Interfaces", "xpath","//div[@class='navItem'][.='Accounts Payable Interfaces']").Click();
new Control("Configure TE Commitments Suspense Settings", "xpath","//div[@class='navItem'][.='Configure TE Commitments Suspense Settings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "APMTECSU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMTECSU] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMTECSU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMTECSU_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMTECSU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMTECSU] Perfoming VerifyExists on IfAccountFailsReplace...", Logger.MessageType.INF);
			Control APMTECSU_IfAccountFailsReplace = new Control("IfAccountFailsReplace", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_PROC_ACCT_CD']");
			CPCommon.AssertEqual(true,APMTECSU_IfAccountFailsReplace.Exists());

												
				CPCommon.CurrentComponent = "APMTECSU";
							CPCommon.WaitControlDisplayed(APMTECSU_MainForm);
IWebElement formBttn = APMTECSU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMTECSU_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMTECSU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMTECSU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMTECSU] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMTECSU_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMTECSU_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMTECSU";
							CPCommon.WaitControlDisplayed(APMTECSU_MainForm);
formBttn = APMTECSU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

