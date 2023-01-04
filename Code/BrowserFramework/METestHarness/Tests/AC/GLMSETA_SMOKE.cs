 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMSETA_SMOKE : TestScript
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
new Control("Journal Entry Processing", "xpath","//div[@class='navItem'][.='Journal Entry Processing']").Click();
new Control("Assign Journal Entry Approvers to Users", "xpath","//div[@class='navItem'][.='Assign Journal Entry Approvers to Users']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMSETA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMSETA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSETA_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSETA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMSETA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMSETA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSETA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETA] Perfoming VerifyExists on LinkUsersLink...", Logger.MessageType.INF);
			Control GLMSETA_LinkUsersLink = new Control("LinkUsersLink", "ID", "lnk_3519_GLMSETA_JEAPPRVR_HDR");
			CPCommon.AssertEqual(true,GLMSETA_LinkUsersLink.Exists());

												
				CPCommon.CurrentComponent = "GLMSETA";
							CPCommon.WaitControlDisplayed(GLMSETA_LinkUsersLink);
GLMSETA_LinkUsersLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMSETA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETA] Perfoming VerifyExist on LinkUsersFormTable...", Logger.MessageType.INF);
			Control GLMSETA_LinkUsersFormTable = new Control("LinkUsersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETA_JEAPPRVRUSER_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSETA_LinkUsersFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSETA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETA] Perfoming VerifyExists on LinkUsersForm...", Logger.MessageType.INF);
			Control GLMSETA_LinkUsersForm = new Control("LinkUsersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETA_JEAPPRVRUSER_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMSETA_LinkUsersForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSETA";
							CPCommon.WaitControlDisplayed(GLMSETA_LinkUsersForm);
IWebElement formBttn = GLMSETA_LinkUsersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMSETA";
							CPCommon.WaitControlDisplayed(GLMSETA_MainForm);
formBttn = GLMSETA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

