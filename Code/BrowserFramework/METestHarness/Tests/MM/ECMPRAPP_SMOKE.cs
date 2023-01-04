 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMPRAPP_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("Engineering Change Controls", "xpath","//div[@class='navItem'][.='Engineering Change Controls']").Click();
new Control("Manage Engineering Change Project Approvers", "xpath","//div[@class='navItem'][.='Manage Engineering Change Project Approvers']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECMPRAPP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMPRAPP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMPRAPP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMPRAPP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMPRAPP";
							CPCommon.WaitControlDisplayed(ECMPRAPP_MainForm);
IWebElement formBttn = ECMPRAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMPRAPP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMPRAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExists on ApprovalTitle...", Logger.MessageType.INF);
			Control ECMPRAPP_ApprovalTitle = new Control("ApprovalTitle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EC_APP_TITLE_DC']");
			CPCommon.AssertEqual(true,ECMPRAPP_ApprovalTitle.Exists());

											Driver.SessionLogger.WriteLine("Approval Title Users");


												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExist on ApprovalTitleUsersTable...", Logger.MessageType.INF);
			Control ECMPRAPP_ApprovalTitleUsersTable = new Control("ApprovalTitleUsersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMPRAPP_ECAPPTITLEUSER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMPRAPP_ApprovalTitleUsersTable.Exists());

												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExists on ApprovalTitleUsersForm...", Logger.MessageType.INF);
			Control ECMPRAPP_ApprovalTitleUsersForm = new Control("ApprovalTitleUsersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMPRAPP_ECAPPTITLEUSER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMPRAPP_ApprovalTitleUsersForm.Exists());

											Driver.SessionLogger.WriteLine("Projects");


												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExist on ProjectTable...", Logger.MessageType.INF);
			Control ECMPRAPP_ProjectTable = new Control("ProjectTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMPRAPP_PROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMPRAPP_ProjectTable.Exists());

												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExists on ProjectForm...", Logger.MessageType.INF);
			Control ECMPRAPP_ProjectForm = new Control("ProjectForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMPRAPP_PROJ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMPRAPP_ProjectForm.Exists());

											Driver.SessionLogger.WriteLine("User Projects");


												
				CPCommon.CurrentComponent = "ECMPRAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMPRAPP] Perfoming VerifyExist on UserProjectTable...", Logger.MessageType.INF);
			Control ECMPRAPP_UserProjectTable = new Control("UserProjectTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMPRAPP_ECAPPTTLUSRPRJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMPRAPP_UserProjectTable.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "ECMPRAPP";
							CPCommon.WaitControlDisplayed(ECMPRAPP_MainForm);
formBttn = ECMPRAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

