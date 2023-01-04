 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQAPT_SMOKE : TestScript
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
new Control("Manage Purchase Requisition Approval Titles", "xpath","//div[@class='navItem'][.='Manage Purchase Requisition Approval Titles']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRQAPT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRQAPT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming VerifyExists on ApprovalTitle...", Logger.MessageType.INF);
			Control PPMRQAPT_ApprovalTitle = new Control("ApprovalTitle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_APPR_TITLE_DC']");
			CPCommon.AssertEqual(true,PPMRQAPT_ApprovalTitle.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPT";
							CPCommon.WaitControlDisplayed(PPMRQAPT_MainForm);
IWebElement formBttn = PPMRQAPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMRQAPT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMRQAPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPMRQAPT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Approvers details");


												
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming Click on UsersLink...", Logger.MessageType.INF);
			Control PPMRQAPT_UsersLink = new Control("UsersLink", "ID", "lnk_1007251_PPMRQAPT_RQAPPRVLTITLE_REQAP");
			CPCommon.WaitControlDisplayed(PPMRQAPT_UsersLink);
PPMRQAPT_UsersLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming VerifyExists on ApproverDetailsForm...", Logger.MessageType.INF);
			Control PPMRQAPT_ApproverDetailsForm = new Control("ApproverDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPT_RQAPPRTITLEUSER_APROV_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPT_ApproverDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming VerifyExist on ApproverDetailsFormTable...", Logger.MessageType.INF);
			Control PPMRQAPT_ApproverDetailsFormTable = new Control("ApproverDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPT_RQAPPRTITLEUSER_APROV_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPT_ApproverDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPT";
							CPCommon.WaitControlDisplayed(PPMRQAPT_ApproverDetailsForm);
formBttn = PPMRQAPT_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPT_ApproverDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPT_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPT] Perfoming VerifyExists on ApproverDetails_PreferredSeq...", Logger.MessageType.INF);
			Control PPMRQAPT_ApproverDetails_PreferredSeq = new Control("ApproverDetails_PreferredSeq", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPT_RQAPPRTITLEUSER_APROV_']/ancestor::form[1]/descendant::*[@id='PREF_SEQ_NO']");
			CPCommon.AssertEqual(true,PPMRQAPT_ApproverDetails_PreferredSeq.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPT";
							CPCommon.WaitControlDisplayed(PPMRQAPT_ApproverDetailsForm);
formBttn = PPMRQAPT_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMRQAPT";
							CPCommon.WaitControlDisplayed(PPMRQAPT_MainForm);
formBttn = PPMRQAPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

