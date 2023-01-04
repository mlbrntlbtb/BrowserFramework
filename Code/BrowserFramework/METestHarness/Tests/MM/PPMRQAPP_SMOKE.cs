 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQAPP_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Procurement Planning Controls", "xpath","//div[@class='navItem'][.='Procurement Planning Controls']").Click();
new Control("Manage Purchase Requisition Approval Processes", "xpath","//div[@class='navItem'][.='Manage Purchase Requisition Approval Processes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRQAPP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRQAPP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExists on RequisitionApprovalProcessCode...", Logger.MessageType.INF);
			Control PPMRQAPP_RequisitionApprovalProcessCode = new Control("RequisitionApprovalProcessCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_APPR_PROC_CD']");
			CPCommon.AssertEqual(true,PPMRQAPP_RequisitionApprovalProcessCode.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.WaitControlDisplayed(PPMRQAPP_MainForm);
IWebElement formBttn = PPMRQAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMRQAPP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMRQAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPMRQAPP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Approval Titles");


												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExists on ApprovalTitlesLink...", Logger.MessageType.INF);
			Control PPMRQAPP_ApprovalTitlesLink = new Control("ApprovalTitlesLink", "ID", "lnk_1001588_PPMRQAPP_RQAPPRVLPROC_HDR");
			CPCommon.AssertEqual(true,PPMRQAPP_ApprovalTitlesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.WaitControlDisplayed(PPMRQAPP_ApprovalTitlesLink);
PPMRQAPP_ApprovalTitlesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExist on RequisitionApprovalProcessDetailsTable...", Logger.MessageType.INF);
			Control PPMRQAPP_RequisitionApprovalProcessDetailsTable = new Control("RequisitionApprovalProcessDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_APPRVLPROCTITLE_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPP_RequisitionApprovalProcessDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming ClickButton on RequisitionApprovalProcessDetailsForm...", Logger.MessageType.INF);
			Control PPMRQAPP_RequisitionApprovalProcessDetailsForm = new Control("RequisitionApprovalProcessDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_APPRVLPROCTITLE_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQAPP_RequisitionApprovalProcessDetailsForm);
formBttn = PPMRQAPP_RequisitionApprovalProcessDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPP_RequisitionApprovalProcessDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPP_RequisitionApprovalProcessDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.AssertEqual(true,PPMRQAPP_RequisitionApprovalProcessDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExists on RequisitionApprovalProcessDetails_Sequence...", Logger.MessageType.INF);
			Control PPMRQAPP_RequisitionApprovalProcessDetails_Sequence = new Control("RequisitionApprovalProcessDetails_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_APPRVLPROCTITLE_DTL_']/ancestor::form[1]/descendant::*[@id='SEQUENCE_NO']");
			CPCommon.AssertEqual(true,PPMRQAPP_RequisitionApprovalProcessDetails_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.WaitControlDisplayed(PPMRQAPP_RequisitionApprovalProcessDetailsForm);
formBttn = PPMRQAPP_RequisitionApprovalProcessDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Link Acct/Org");


												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExists on LinkAcctOrgLink...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkAcctOrgLink = new Control("LinkAcctOrgLink", "ID", "lnk_1002401_PPMRQAPP_RQAPPRVLPROC_HDR");
			CPCommon.AssertEqual(true,PPMRQAPP_LinkAcctOrgLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.WaitControlDisplayed(PPMRQAPP_LinkAcctOrgLink);
PPMRQAPP_LinkAcctOrgLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExist on LinkAcctOrg_OrgTable...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkAcctOrg_OrgTable = new Control("LinkAcctOrg_OrgTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_ORGACCT_DESELECT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPP_LinkAcctOrg_OrgTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExist on LinkAcctOrg_AcctTable...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkAcctOrg_AcctTable = new Control("LinkAcctOrg_AcctTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_ORGACCT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPP_LinkAcctOrg_AcctTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming Close on LinkAcctOrg_AcctForm...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkAcctOrg_AcctForm = new Control("LinkAcctOrg_AcctForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_ORGACCT_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQAPP_LinkAcctOrg_AcctForm);
formBttn = PPMRQAPP_LinkAcctOrg_AcctForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Link Project");


												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExists on LinkProjectLink...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkProjectLink = new Control("LinkProjectLink", "ID", "lnk_1002403_PPMRQAPP_RQAPPRVLPROC_HDR");
			CPCommon.AssertEqual(true,PPMRQAPP_LinkProjectLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.WaitControlDisplayed(PPMRQAPP_LinkProjectLink);
PPMRQAPP_LinkProjectLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExist on LinkProject_Project1Table...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkProject_Project1Table = new Control("LinkProject_Project1Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_PROJ_DESELECT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPP_LinkProject_Project1Table.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming VerifyExist on LinkProject_Project2Table...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkProject_Project2Table = new Control("LinkProject_Project2Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_PROJ_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPP_LinkProject_Project2Table.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPP] Perfoming Close on LinkProject_Project2Form...", Logger.MessageType.INF);
			Control PPMRQAPP_LinkProject_Project2Form = new Control("LinkProject_Project2Form", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPP_PROJ_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQAPP_LinkProject_Project2Form);
formBttn = PPMRQAPP_LinkProject_Project2Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PPMRQAPP";
							CPCommon.WaitControlDisplayed(PPMRQAPP_MainForm);
formBttn = PPMRQAPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

