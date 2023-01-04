 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMECAPP_SMOKE : TestScript
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
new Control("Manage Engineering Change Approval Processes", "xpath","//div[@class='navItem'][.='Manage Engineering Change Approval Processes']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMECAPP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMECAPP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ECApprovalProcessCode...", Logger.MessageType.INF);
			Control ECMECAPP_ECApprovalProcessCode = new Control("ECApprovalProcessCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EC_APP_PROC_CD']");
			CPCommon.AssertEqual(true,ECMECAPP_ECApprovalProcessCode.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_MainForm);
IWebElement formBttn = ECMECAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ECMECAPP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ECMECAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control ECMECAPP_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_MainTable.Exists());

											Driver.SessionLogger.WriteLine("ChangeTypeLink");


												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ChangeTypeLink...", Logger.MessageType.INF);
			Control ECMECAPP_ChangeTypeLink = new Control("ChangeTypeLink", "ID", "lnk_1005723_ECMECAPP_ECAPPPROC_HDR");
			CPCommon.AssertEqual(true,ECMECAPP_ChangeTypeLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_ChangeTypeLink);
ECMECAPP_ChangeTypeLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on EngineeringChangeTypesTableWindowTable...", Logger.MessageType.INF);
			Control ECMECAPP_EngineeringChangeTypesTableWindowTable = new Control("EngineeringChangeTypesTableWindowTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECTYPE_ECT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_EngineeringChangeTypesTableWindowTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on EngineeringChangeTypesTableWindowForm...", Logger.MessageType.INF);
			Control ECMECAPP_EngineeringChangeTypesTableWindowForm = new Control("EngineeringChangeTypesTableWindowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECTYPE_ECT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECAPP_EngineeringChangeTypesTableWindowForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on SelectedEngineeringChangeTypesTableWindowTable...", Logger.MessageType.INF);
			Control ECMECAPP_SelectedEngineeringChangeTypesTableWindowTable = new Control("SelectedEngineeringChangeTypesTableWindowTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECTYPE_SECT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_SelectedEngineeringChangeTypesTableWindowTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on SelectedEngineeringChangeTypesTableWindowForm...", Logger.MessageType.INF);
			Control ECMECAPP_SelectedEngineeringChangeTypesTableWindowForm = new Control("SelectedEngineeringChangeTypesTableWindowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECTYPE_SECT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECAPP_SelectedEngineeringChangeTypesTableWindowForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_SelectedEngineeringChangeTypesTableWindowForm);
formBttn = ECMECAPP_SelectedEngineeringChangeTypesTableWindowForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LinkOrgsLink");


												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on LinkOrgsLink...", Logger.MessageType.INF);
			Control ECMECAPP_LinkOrgsLink = new Control("LinkOrgsLink", "ID", "lnk_1005725_ECMECAPP_ECAPPPROC_HDR");
			CPCommon.AssertEqual(true,ECMECAPP_LinkOrgsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_LinkOrgsLink);
ECMECAPP_LinkOrgsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on OrganizationTableWindowTable...", Logger.MessageType.INF);
			Control ECMECAPP_OrganizationTableWindowTable = new Control("OrganizationTableWindowTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ORG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_OrganizationTableWindowTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on OrganizationTableWindowForm...", Logger.MessageType.INF);
			Control ECMECAPP_OrganizationTableWindowForm = new Control("OrganizationTableWindowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ORG_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECAPP_OrganizationTableWindowForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on SelectedOrganizationsTableWindowTable...", Logger.MessageType.INF);
			Control ECMECAPP_SelectedOrganizationsTableWindowTable = new Control("SelectedOrganizationsTableWindowTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ORG_SECI_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_SelectedOrganizationsTableWindowTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on SelectedOrganizationsTableWindowForm...", Logger.MessageType.INF);
			Control ECMECAPP_SelectedOrganizationsTableWindowForm = new Control("SelectedOrganizationsTableWindowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ORG_SECI_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMECAPP_SelectedOrganizationsTableWindowForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_SelectedOrganizationsTableWindowForm);
formBttn = ECMECAPP_SelectedOrganizationsTableWindowForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ApproverDetailsForm");


												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on ApproverDetailsTable...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetailsTable = new Control("ApproverDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECAPPPROCTITLE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming ClickButton on ApproverDetailsForm...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetailsForm = new Control("ApproverDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECAPPPROCTITLE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetailsForm);
formBttn = ECMECAPP_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECAPP_ApproverDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECAPP_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.AssertEqual(true,ECMECAPP_ApproverDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ApproverDetails_PrefSequence...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_PrefSequence = new Control("ApproverDetails_PrefSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECAPPPROCTITLE_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_PrefSequence.Exists());

											Driver.SessionLogger.WriteLine("UsersLink");


												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ApproverDetails_UsersLink...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UsersLink = new Control("ApproverDetails_UsersLink", "ID", "lnk_1002316_ECMECAPP_ECAPPPROCTITLE");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UsersLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetails_UsersLink);
ECMECAPP_ApproverDetails_UsersLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on ApproverDetails_UsersTable...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UsersTable = new Control("ApproverDetails_UsersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECAPPTITLEUSER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UsersTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming ClickButton on ApproverDetails_UsersForm...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UsersForm = new Control("ApproverDetails_UsersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECAPPTITLEUSER_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetails_UsersForm);
formBttn = ECMECAPP_ApproverDetails_UsersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECAPP_ApproverDetails_UsersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECAPP_ApproverDetails_UsersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UsersForm.Exists());

													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ApproverDetails_Users_PrefSeq...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_Users_PrefSeq = new Control("ApproverDetails_Users_PrefSeq", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_ECAPPTITLEUSER_']/ancestor::form[1]/descendant::*[@id='PREF_SEQ_NO']");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_Users_PrefSeq.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetails_UsersForm);
formBttn = ECMECAPP_ApproverDetails_UsersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("UsersProjectsLink");


												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ApproverDetails_UserProjectsLink...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UserProjectsLink = new Control("ApproverDetails_UserProjectsLink", "ID", "lnk_1002321_ECMECAPP_ECAPPPROCTITLE");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UserProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetails_UserProjectsLink);
ECMECAPP_ApproverDetails_UserProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExist on ApproverDetails_UserProjectsTable...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UserProjectsTable = new Control("ApproverDetails_UserProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_USERPROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UserProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming ClickButton on ApproverDetails_UserProjectsForm...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UserProjectsForm = new Control("ApproverDetails_UserProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_USERPROJ_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetails_UserProjectsForm);
formBttn = ECMECAPP_ApproverDetails_UserProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECAPP_ApproverDetails_UserProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECAPP_ApproverDetails_UserProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UserProjectsForm.Exists());

													
				CPCommon.CurrentComponent = "ECMECAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPP] Perfoming VerifyExists on ApproverDetails_UserProjects_User...", Logger.MessageType.INF);
			Control ECMECAPP_ApproverDetails_UserProjects_User = new Control("ApproverDetails_UserProjects_User", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPP_USERPROJ_']/ancestor::form[1]/descendant::*[@id='USER_ID']");
			CPCommon.AssertEqual(true,ECMECAPP_ApproverDetails_UserProjects_User.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_ApproverDetails_UserProjectsForm);
formBttn = ECMECAPP_ApproverDetails_UserProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "ECMECAPP";
							CPCommon.WaitControlDisplayed(ECMECAPP_MainForm);
formBttn = ECMECAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

