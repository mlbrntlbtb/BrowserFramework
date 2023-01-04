 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMBEGFY_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Beginning Balances", "xpath","//div[@class='navItem'][.='General Ledger Beginning Balances']").Click();
new Control("Manage PY Non-Project Comparative Balances", "xpath","//div[@class='navItem'][.='Manage PY Non-Project Comparative Balances']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMBEGFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGFY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMBEGFY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMBEGFY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGFY] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLMBEGFY_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLMBEGFY_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGFY";
							CPCommon.WaitControlDisplayed(GLMBEGFY_MainForm);
IWebElement formBttn = GLMBEGFY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMBEGFY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMBEGFY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMBEGFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGFY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMBEGFY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBEGFY_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGFY] Perfoming VerifyExists on NonProjectComparativeBalancesForm...", Logger.MessageType.INF);
			Control GLMBEGFY_NonProjectComparativeBalancesForm = new Control("NonProjectComparativeBalancesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBEGFY_GLPOSTSUM_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMBEGFY_NonProjectComparativeBalancesForm.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGFY] Perfoming VerifyExist on NonProjectComparativeBalancesFormTable...", Logger.MessageType.INF);
			Control GLMBEGFY_NonProjectComparativeBalancesFormTable = new Control("NonProjectComparativeBalancesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBEGFY_GLPOSTSUM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBEGFY_NonProjectComparativeBalancesFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGFY";
							CPCommon.WaitControlDisplayed(GLMBEGFY_NonProjectComparativeBalancesForm);
formBttn = GLMBEGFY_NonProjectComparativeBalancesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMBEGFY_NonProjectComparativeBalancesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMBEGFY_NonProjectComparativeBalancesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMBEGFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGFY] Perfoming VerifyExists on NonProjectComparativeBalances_AccountOrganization_Account...", Logger.MessageType.INF);
			Control GLMBEGFY_NonProjectComparativeBalances_AccountOrganization_Account = new Control("NonProjectComparativeBalances_AccountOrganization_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBEGFY_GLPOSTSUM_CTW_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMBEGFY_NonProjectComparativeBalances_AccountOrganization_Account.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGFY";
							CPCommon.WaitControlDisplayed(GLMBEGFY_MainForm);
formBttn = GLMBEGFY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

