 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMAPPRT_SMOKE : TestScript
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
new Control("Manage Sales Order Approval Titles", "xpath","//div[@class='navItem'][.='Manage Sales Order Approval Titles']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEMAPPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMAPPRT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMAPPRT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRT] Perfoming VerifyExists on ApprovalTitleCode...", Logger.MessageType.INF);
			Control OEMAPPRT_ApprovalTitleCode = new Control("ApprovalTitleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OE_APPR_TITLE_DC']");
			CPCommon.AssertEqual(true,OEMAPPRT_ApprovalTitleCode.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRT";
							CPCommon.WaitControlDisplayed(OEMAPPRT_MainForm);
IWebElement formBttn = OEMAPPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMAPPRT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMAPPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMAPPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMAPPRT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPRT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Childform");


												
				CPCommon.CurrentComponent = "OEMAPPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRT] Perfoming VerifyExist on ApproverDetailsFormTable...", Logger.MessageType.INF);
			Control OEMAPPRT_ApproverDetailsFormTable = new Control("ApproverDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRT_CHILD_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPRT_ApproverDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRT] Perfoming ClickButton on ApproverDetailsForm...", Logger.MessageType.INF);
			Control OEMAPPRT_ApproverDetailsForm = new Control("ApproverDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRT_CHILD_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMAPPRT_ApproverDetailsForm);
formBttn = OEMAPPRT_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPRT_ApproverDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPRT_ApproverDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMAPPRT";
							CPCommon.AssertEqual(true,OEMAPPRT_ApproverDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "OEMAPPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPRT] Perfoming VerifyExists on ApproverDetails_PreferredSequence...", Logger.MessageType.INF);
			Control OEMAPPRT_ApproverDetails_PreferredSequence = new Control("ApproverDetails_PreferredSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPRT_CHILD_TBL_']/ancestor::form[1]/descendant::*[@id='PREF_SEQ_NO']");
			CPCommon.AssertEqual(true,OEMAPPRT_ApproverDetails_PreferredSequence.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPRT";
							CPCommon.WaitControlDisplayed(OEMAPPRT_MainForm);
formBttn = OEMAPPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

