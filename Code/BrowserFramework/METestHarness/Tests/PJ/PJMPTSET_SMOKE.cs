 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPTSET_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Project Transfer Processing", "xpath","//div[@class='navItem'][.='Project Transfer Processing']").Click();
new Control("Manage Project Transfer Information", "xpath","//div[@class='navItem'][.='Manage Project Transfer Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPTSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPTSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMPTSET_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPTSET_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMPTSET";
							CPCommon.WaitControlDisplayed(PJMPTSET_MainForm);
IWebElement formBttn = PJMPTSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPTSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPTSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPTSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPTSET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("ACCOUNTS");


												
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExists on AccountsLink...", Logger.MessageType.INF);
			Control PJMPTSET_AccountsLink = new Control("AccountsLink", "ID", "lnk_1001092_PJMPTSET_PROJXFER_HDR");
			CPCommon.AssertEqual(true,PJMPTSET_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPTSET";
							CPCommon.WaitControlDisplayed(PJMPTSET_AccountsLink);
PJMPTSET_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExist on AccountsFormTable...", Logger.MessageType.INF);
			Control PJMPTSET_AccountsFormTable = new Control("AccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPTSET_PROJXFERACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPTSET_AccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExists on AccountsForm...", Logger.MessageType.INF);
			Control PJMPTSET_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPTSET_PROJXFERACCT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMPTSET_AccountsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPTSET] Perfoming VerifyExists on Accounts_Ok...", Logger.MessageType.INF);
			Control PJMPTSET_Accounts_Ok = new Control("Accounts_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPTSET_PROJXFERACCT_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,PJMPTSET_Accounts_Ok.Exists());

												
				CPCommon.CurrentComponent = "PJMPTSET";
							CPCommon.WaitControlDisplayed(PJMPTSET_AccountsForm);
formBttn = PJMPTSET_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMPTSET";
							CPCommon.WaitControlDisplayed(PJMPTSET_MainForm);
formBttn = PJMPTSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

