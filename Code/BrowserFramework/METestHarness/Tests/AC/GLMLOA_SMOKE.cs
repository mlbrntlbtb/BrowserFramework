 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMLOA_SMOKE : TestScript
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
new Control("Mass Link Accounts/Organizations", "xpath","//div[@class='navItem'][.='Mass Link Accounts/Organizations']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMLOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMLOA] Perfoming VerifyExist on AccountsFormTable...", Logger.MessageType.INF);
			Control GLMLOA_AccountsFormTable = new Control("AccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMLOA_ACCT_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMLOA_AccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMLOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMLOA] Perfoming VerifyExist on OrganizationsFormTable...", Logger.MessageType.INF);
			Control GLMLOA_OrganizationsFormTable = new Control("OrganizationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMLOA_ORG_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMLOA_OrganizationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMLOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMLOA] Perfoming VerifyExist on SelectedAccountsOrganizationsFormTable...", Logger.MessageType.INF);
			Control GLMLOA_SelectedAccountsOrganizationsFormTable = new Control("SelectedAccountsOrganizationsFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMLOA_SelectedAccountsOrganizationsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMLOA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMLOA] Perfoming ClickButton on OrganizationsForm...", Logger.MessageType.INF);
			Control GLMLOA_OrganizationsForm = new Control("OrganizationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMLOA_ORG_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMLOA_OrganizationsForm);
IWebElement formBttn = GLMLOA_OrganizationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Count <= 0 ? GLMLOA_OrganizationsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Close')]")).FirstOrDefault() :
GLMLOA_OrganizationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Close not found ");


												
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

