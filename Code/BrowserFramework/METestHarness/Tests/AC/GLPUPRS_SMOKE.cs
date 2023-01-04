 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPUPRS_SMOKE : TestScript
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
new Control("Update Reference Summary Balances", "xpath","//div[@class='navItem'][.='Update Reference Summary Balances']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLPUPRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPRS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLPUPRS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLPUPRS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLPUPRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPRS] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLPUPRS_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLPUPRS_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "GLPUPRS";
							GLPUPRS_FiscalYear.Click();
GLPUPRS_FiscalYear.SendKeys("Y2010", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLPUPRS_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												Driver.SessionLogger.WriteLine("BalancesForm");


												
				CPCommon.CurrentComponent = "GLPUPRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPRS] Perfoming VerifyExist on BalancesFormTable...", Logger.MessageType.INF);
			Control GLPUPRS_BalancesFormTable = new Control("BalancesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPUPRS_CTW_BALANCE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPUPRS_BalancesFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPUPRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPRS] Perfoming ClickButton on BalancesForm...", Logger.MessageType.INF);
			Control GLPUPRS_BalancesForm = new Control("BalancesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPUPRS_CTW_BALANCE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLPUPRS_BalancesForm);
IWebElement formBttn = GLPUPRS_BalancesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLPUPRS_BalancesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLPUPRS_BalancesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLPUPRS";
							CPCommon.AssertEqual(true,GLPUPRS_BalancesForm.Exists());

													
				CPCommon.CurrentComponent = "GLPUPRS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPUPRS] Perfoming VerifyExists on Balances_Account...", Logger.MessageType.INF);
			Control GLPUPRS_Balances_Account = new Control("Balances_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPUPRS_CTW_BALANCE_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLPUPRS_Balances_Account.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLPUPRS";
							CPCommon.WaitControlDisplayed(GLPUPRS_MainForm);
formBttn = GLPUPRS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

