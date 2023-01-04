 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMORMNT_SMOKE : TestScript
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
new Control("Organizations", "xpath","//div[@class='navItem'][.='Organizations']").Click();
new Control("Manage Organization Elements", "xpath","//div[@class='navItem'][.='Manage Organization Elements']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLMORMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORMNT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMORMNT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMORMNT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMORMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORMNT] Perfoming VerifyExists on Organization...", Logger.MessageType.INF);
			Control GLMORMNT_Organization = new Control("Organization", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,GLMORMNT_Organization.Exists());

												
				CPCommon.CurrentComponent = "GLMORMNT";
							CPCommon.WaitControlDisplayed(GLMORMNT_MainForm);
IWebElement formBttn = GLMORMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMORMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMORMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMORMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORMNT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMORMNT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMORMNT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Link To Accounts Form");


												
				CPCommon.CurrentComponent = "GLMORMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORMNT] Perfoming VerifyExists on LinkToAccountsLink...", Logger.MessageType.INF);
			Control GLMORMNT_LinkToAccountsLink = new Control("LinkToAccountsLink", "ID", "lnk_1002067_GLMORMNT_ORG_PARENT");
			CPCommon.AssertEqual(true,GLMORMNT_LinkToAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMORMNT";
							CPCommon.WaitControlDisplayed(GLMORMNT_LinkToAccountsLink);
GLMORMNT_LinkToAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMORMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORMNT] Perfoming VerifyExists on LinkToAccountsForm...", Logger.MessageType.INF);
			Control GLMORMNT_LinkToAccountsForm = new Control("LinkToAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMORMNT_ACCT_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMORMNT_LinkToAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMORMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORMNT] Perfoming VerifyExists on LinkToAccounts_Acct...", Logger.MessageType.INF);
			Control GLMORMNT_LinkToAccounts_Acct = new Control("LinkToAccounts_Acct", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMORMNT_ACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMORMNT_LinkToAccounts_Acct.Exists());

												
				CPCommon.CurrentComponent = "GLMORMNT";
							CPCommon.WaitControlDisplayed(GLMORMNT_LinkToAccountsForm);
formBttn = GLMORMNT_LinkToAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLMORMNT";
							CPCommon.WaitControlDisplayed(GLMORMNT_MainForm);
formBttn = GLMORMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

