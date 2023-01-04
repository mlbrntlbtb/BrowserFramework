 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPUPFS_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Utilities", "xpath","//div[@class='navItem'][.='General Ledger Utilities']").Click();
new Control("Update Financial Statement Summary Balances", "xpath","//div[@class='navItem'][.='Update Financial Statement Summary Balances']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLPUPFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPFS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLPUPFS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLPUPFS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLPUPFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPFS] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLPUPFS_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLPUPFS_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("BALANCES");


												
				CPCommon.CurrentComponent = "GLPUPFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPFS] Perfoming VerifyExist on BalancesFormTable...", Logger.MessageType.INF);
			Control GLPUPFS_BalancesFormTable = new Control("BalancesFormTable", "xpath", "//div[starts-with(@id,'pr__GLPUPFS1_PARAM_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPUPFS_BalancesFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPUPFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPFS] Perfoming ClickButton on BalancesForm...", Logger.MessageType.INF);
			Control GLPUPFS_BalancesForm = new Control("BalancesForm", "xpath", "//div[starts-with(@id,'pr__GLPUPFS1_PARAM_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLPUPFS_BalancesForm);
IWebElement formBttn = GLPUPFS_BalancesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLPUPFS_BalancesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLPUPFS_BalancesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLPUPFS";
							CPCommon.AssertEqual(true,GLPUPFS_BalancesForm.Exists());

													
				CPCommon.CurrentComponent = "GLPUPFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPFS] Perfoming VerifyExists on Balances_Account...", Logger.MessageType.INF);
			Control GLPUPFS_Balances_Account = new Control("Balances_Account", "xpath", "//div[starts-with(@id,'pr__GLPUPFS1_PARAM_')]/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLPUPFS_Balances_Account.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLPUPFS";
							CPCommon.WaitControlDisplayed(GLPUPFS_MainForm);
formBttn = GLPUPFS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

