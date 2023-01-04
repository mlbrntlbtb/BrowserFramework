 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMAPPRP_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Manage Sales Order Approval Processes", "xpath","//div[@class='navItem'][.='Manage Sales Order Approval Processes']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMAPPRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMAPPRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExists on ApprovalProcessCode...", Logger.MessageType.INF);
			Control OEMAPPRP_ApprovalProcessCode = new Control("ApprovalProcessCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OE_APPR_PROC_CD']");
			CPCommon.AssertEqual(true,OEMAPPRP_ApprovalProcessCode.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRP";
							CPCommon.WaitControlDisplayed(OEMAPPRP_MainForm);
IWebElement formBttn = OEMAPPRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMAPPRP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMAPPRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMAPPRP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPRP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("APPROVALTITLES");


												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExist on ApprovalTitlesTable...", Logger.MessageType.INF);
			Control OEMAPPRP_ApprovalTitlesTable = new Control("ApprovalTitlesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRP_OEAPPRPROCTITLE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPRP_ApprovalTitlesTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming ClickButton on ApprovalTitlesForm...", Logger.MessageType.INF);
			Control OEMAPPRP_ApprovalTitlesForm = new Control("ApprovalTitlesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRP_OEAPPRPROCTITLE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMAPPRP_ApprovalTitlesForm);
formBttn = OEMAPPRP_ApprovalTitlesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPRP_ApprovalTitlesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPRP_ApprovalTitlesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMAPPRP";
							CPCommon.AssertEqual(true,OEMAPPRP_ApprovalTitlesForm.Exists());

													
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExists on ApprovalTitles_Sequence...", Logger.MessageType.INF);
			Control OEMAPPRP_ApprovalTitles_Sequence = new Control("ApprovalTitles_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRP_OEAPPRPROCTITLE_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,OEMAPPRP_ApprovalTitles_Sequence.Exists());

											Driver.SessionLogger.WriteLine("VIEWEMPLOYEEAPPROVAL");


												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming Click on ApprovalTitles_ViewEmployeeApprovalLink...", Logger.MessageType.INF);
			Control OEMAPPRP_ApprovalTitles_ViewEmployeeApprovalLink = new Control("ApprovalTitles_ViewEmployeeApprovalLink", "ID", "lnk_1001117_OEMAPPRP_OEAPPRPROCTITLE");
			CPCommon.WaitControlDisplayed(OEMAPPRP_ApprovalTitles_ViewEmployeeApprovalLink);
OEMAPPRP_ApprovalTitles_ViewEmployeeApprovalLink.Click(1.5);


												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExists on TableWindow_ViewEmployeeApprovalForm...", Logger.MessageType.INF);
			Control OEMAPPRP_TableWindow_ViewEmployeeApprovalForm = new Control("TableWindow_ViewEmployeeApprovalForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRP_OEAPPRTITLEUSER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPRP_TableWindow_ViewEmployeeApprovalForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExists on TableWindow_ViewEmployeeApproval_PreferSeq...", Logger.MessageType.INF);
			Control OEMAPPRP_TableWindow_ViewEmployeeApproval_PreferSeq = new Control("TableWindow_ViewEmployeeApproval_PreferSeq", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRP_OEAPPRTITLEUSER_']/ancestor::form[1]/descendant::*[@id='PREF_SEQ_NO']");
			CPCommon.AssertEqual(true,OEMAPPRP_TableWindow_ViewEmployeeApproval_PreferSeq.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRP";
							CPCommon.WaitControlDisplayed(OEMAPPRP_TableWindow_ViewEmployeeApprovalForm);
formBttn = OEMAPPRP_TableWindow_ViewEmployeeApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMAPPRP_TableWindow_ViewEmployeeApprovalForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMAPPRP_TableWindow_ViewEmployeeApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMAPPRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRP] Perfoming VerifyExist on TableWindow_ViewEmployeeApprovalTable...", Logger.MessageType.INF);
			Control OEMAPPRP_TableWindow_ViewEmployeeApprovalTable = new Control("TableWindow_ViewEmployeeApprovalTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRP_OEAPPRTITLEUSER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPRP_TableWindow_ViewEmployeeApprovalTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRP";
							CPCommon.WaitControlDisplayed(OEMAPPRP_TableWindow_ViewEmployeeApprovalForm);
formBttn = OEMAPPRP_TableWindow_ViewEmployeeApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "OEMAPPRP";
							CPCommon.WaitControlDisplayed(OEMAPPRP_MainForm);
formBttn = OEMAPPRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

