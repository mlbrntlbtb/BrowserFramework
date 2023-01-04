 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMRNBEG_SMOKE : TestScript
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
new Control("General Ledger Beginning Balances", "xpath","//div[@class='navItem'][.='General Ledger Beginning Balances']").Click();
new Control("Manage Reference Beginning Balances", "xpath","//div[@class='navItem'][.='Manage Reference Beginning Balances']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMRNBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNBEG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMRNBEG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMRNBEG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRNBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNBEG] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLMRNBEG_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLMRNBEG_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "GLMRNBEG";
							CPCommon.WaitControlDisplayed(GLMRNBEG_MainForm);
IWebElement formBttn = GLMRNBEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMRNBEG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMRNBEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMRNBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNBEG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMRNBEG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRNBEG_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "GLMRNBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNBEG] Perfoming VerifyExist on AccountDetailsFormTable...", Logger.MessageType.INF);
			Control GLMRNBEG_AccountDetailsFormTable = new Control("AccountDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRNBEG_REFSUM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRNBEG_AccountDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMRNBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNBEG] Perfoming ClickButton on AccountDetailsForm...", Logger.MessageType.INF);
			Control GLMRNBEG_AccountDetailsForm = new Control("AccountDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRNBEG_REFSUM_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMRNBEG_AccountDetailsForm);
formBttn = GLMRNBEG_AccountDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMRNBEG_AccountDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMRNBEG_AccountDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMRNBEG";
							CPCommon.AssertEqual(true,GLMRNBEG_AccountDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "GLMRNBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNBEG] Perfoming VerifyExists on AccountDetails_Account...", Logger.MessageType.INF);
			Control GLMRNBEG_AccountDetails_Account = new Control("AccountDetails_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRNBEG_REFSUM_CTW_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMRNBEG_AccountDetails_Account.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLMRNBEG";
							CPCommon.WaitControlDisplayed(GLMRNBEG_MainForm);
formBttn = GLMRNBEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

