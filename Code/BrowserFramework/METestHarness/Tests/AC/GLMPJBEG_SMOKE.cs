 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMPJBEG_SMOKE : TestScript
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
new Control("Manage Project Beginning Balances", "xpath","//div[@class='navItem'][.='Manage Project Beginning Balances']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMPJBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMPJBEG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMPJBEG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMPJBEG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMPJBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMPJBEG] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLMPJBEG_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLMPJBEG_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "GLMPJBEG";
							CPCommon.WaitControlDisplayed(GLMPJBEG_MainForm);
IWebElement formBttn = GLMPJBEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMPJBEG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMPJBEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMPJBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMPJBEG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMPJBEG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMPJBEG_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD");


												
				CPCommon.CurrentComponent = "GLMPJBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMPJBEG] Perfoming VerifyExist on AccountOrganizationDetailsTable...", Logger.MessageType.INF);
			Control GLMPJBEG_AccountOrganizationDetailsTable = new Control("AccountOrganizationDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMPJBEG_GLPOSTSUMACCT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMPJBEG_AccountOrganizationDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "GLMPJBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMPJBEG] Perfoming ClickButton on AccountOrganizationDetailsForm...", Logger.MessageType.INF);
			Control GLMPJBEG_AccountOrganizationDetailsForm = new Control("AccountOrganizationDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMPJBEG_GLPOSTSUMACCT_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMPJBEG_AccountOrganizationDetailsForm);
formBttn = GLMPJBEG_AccountOrganizationDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMPJBEG_AccountOrganizationDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMPJBEG_AccountOrganizationDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMPJBEG";
							CPCommon.AssertEqual(true,GLMPJBEG_AccountOrganizationDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "GLMPJBEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMPJBEG] Perfoming VerifyExists on AccountOrganizationDetails_Account...", Logger.MessageType.INF);
			Control GLMPJBEG_AccountOrganizationDetails_Account = new Control("AccountOrganizationDetails_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMPJBEG_GLPOSTSUMACCT_DTL_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMPJBEG_AccountOrganizationDetails_Account.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMPJBEG";
							CPCommon.WaitControlDisplayed(GLMPJBEG_MainForm);
formBttn = GLMPJBEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

