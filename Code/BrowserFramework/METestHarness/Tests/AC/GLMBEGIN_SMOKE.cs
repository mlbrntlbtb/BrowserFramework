 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMBEGIN_SMOKE : TestScript
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
new Control("Manage Non-Project Beginning Balances", "xpath","//div[@class='navItem'][.='Manage Non-Project Beginning Balances']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMBEGIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGIN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMBEGIN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMBEGIN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGIN] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLMBEGIN_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLMBEGIN_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGIN";
							CPCommon.WaitControlDisplayed(GLMBEGIN_MainForm);
IWebElement formBttn = GLMBEGIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMBEGIN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMBEGIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMBEGIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGIN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMBEGIN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBEGIN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGIN] Perfoming VerifyExists on AccountDetailsForm...", Logger.MessageType.INF);
			Control GLMBEGIN_AccountDetailsForm = new Control("AccountDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBEGIN_FSSUM_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMBEGIN_AccountDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGIN] Perfoming VerifyExist on AccountDetailsFormTable...", Logger.MessageType.INF);
			Control GLMBEGIN_AccountDetailsFormTable = new Control("AccountDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBEGIN_FSSUM_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBEGIN_AccountDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGIN";
							CPCommon.WaitControlDisplayed(GLMBEGIN_AccountDetailsForm);
formBttn = GLMBEGIN_AccountDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMBEGIN_AccountDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMBEGIN_AccountDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMBEGIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBEGIN] Perfoming VerifyExists on Account...", Logger.MessageType.INF);
			Control GLMBEGIN_Account = new Control("Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBEGIN_FSSUM_CTW_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMBEGIN_Account.Exists());

												
				CPCommon.CurrentComponent = "GLMBEGIN";
							CPCommon.WaitControlDisplayed(GLMBEGIN_MainForm);
formBttn = GLMBEGIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

