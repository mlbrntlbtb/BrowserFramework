 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMACT_SMOKE : TestScript
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
new Control("Accounts", "xpath","//div[@class='navItem'][.='Accounts']").Click();
new Control("Manage Accounts", "xpath","//div[@class='navItem'][.='Manage Accounts']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "GLMACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACT] Perfoming VerifyExists on Account_Account...", Logger.MessageType.INF);
			Control GLMACT_Account_Account = new Control("Account_Account", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMACT_Account_Account.Exists());

												
				CPCommon.CurrentComponent = "GLMACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control GLMACT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMACT_MainForm);
IWebElement formBttn = GLMACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMACT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "GLMACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMACT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMACT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACT] Perfoming VerifyExists on Account_LinkOrganizationsLink...", Logger.MessageType.INF);
			Control GLMACT_Account_LinkOrganizationsLink = new Control("Account_LinkOrganizationsLink", "ID", "lnk_1000861_GLMACT_ACCT_HDR");
			CPCommon.AssertEqual(true,GLMACT_Account_LinkOrganizationsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMACT";
							CPCommon.WaitControlDisplayed(GLMACT_Account_LinkOrganizationsLink);
GLMACT_Account_LinkOrganizationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACT] Perfoming VerifyExists on LinkToOrganizationsForm...", Logger.MessageType.INF);
			Control GLMACT_LinkToOrganizationsForm = new Control("LinkToOrganizationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMACT_LINKORGS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMACT_LinkToOrganizationsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACT] Perfoming VerifyExists on LinkToOrganizations_Org...", Logger.MessageType.INF);
			Control GLMACT_LinkToOrganizations_Org = new Control("LinkToOrganizations_Org", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMACT_LINKORGS_']/ancestor::form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,GLMACT_LinkToOrganizations_Org.Exists());

												
				CPCommon.CurrentComponent = "GLMACT";
							CPCommon.WaitControlDisplayed(GLMACT_LinkToOrganizationsForm);
formBttn = GLMACT_LinkToOrganizationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLMACT";
							CPCommon.WaitControlDisplayed(GLMACT_MainForm);
formBttn = GLMACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

