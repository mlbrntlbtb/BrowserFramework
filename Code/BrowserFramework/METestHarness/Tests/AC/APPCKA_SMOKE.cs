 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPCKA_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Payment Processing", "xpath","//div[@class='navItem'][.='Payment Processing']").Click();
new Control("Approve Checks", "xpath","//div[@class='navItem'][.='Approve Checks']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPCKA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPCKA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExists on CashAccountDescription...", Logger.MessageType.INF);
			Control APPCKA_CashAccountDescription = new Control("CashAccountDescription", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CASH_ACCT_DESC']");
			CPCommon.AssertEqual(true,APPCKA_CashAccountDescription.Exists());

												
				CPCommon.CurrentComponent = "APPCKA";
							APPCKA_CashAccountDescription.Click();
APPCKA_CashAccountDescription.SendKeys("CASH 00111-010.1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
APPCKA_CashAccountDescription.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("CHECK SUMMARY");


												
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExist on CheckSummaryTable...", Logger.MessageType.INF);
			Control APPCKA_CheckSummaryTable = new Control("CheckSummaryTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APPCKA_CHKDETAILS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPCKA_CheckSummaryTable.Exists());

												
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming ClickButton on CheckSummaryForm...", Logger.MessageType.INF);
			Control APPCKA_CheckSummaryForm = new Control("CheckSummaryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APPCKA_CHKDETAILS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APPCKA_CheckSummaryForm);
IWebElement formBttn = APPCKA_CheckSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APPCKA_CheckSummaryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APPCKA_CheckSummaryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APPCKA";
							CPCommon.AssertEqual(true,APPCKA_CheckSummaryForm.Exists());

													
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExists on CheckSummary_PayVendorInformation_PayVendorID...", Logger.MessageType.INF);
			Control APPCKA_CheckSummary_PayVendorInformation_PayVendorID = new Control("CheckSummary_PayVendorInformation_PayVendorID", "xpath", "//div[translate(@id,'0123456789','')='pr__APPCKA_CHKDETAILS_']/ancestor::form[1]/descendant::*[@id='PAY_VEND_ID']");
			CPCommon.AssertEqual(true,APPCKA_CheckSummary_PayVendorInformation_PayVendorID.Exists());

											Driver.SessionLogger.WriteLine("CHECK DETAILS");


												
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExists on CheckSummary_CheckDetailsLink...", Logger.MessageType.INF);
			Control APPCKA_CheckSummary_CheckDetailsLink = new Control("CheckSummary_CheckDetailsLink", "ID", "lnk_1003980_APPCKA_CHKDETAILS");
			CPCommon.AssertEqual(true,APPCKA_CheckSummary_CheckDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "APPCKA";
							CPCommon.WaitControlDisplayed(APPCKA_CheckSummary_CheckDetailsLink);
APPCKA_CheckSummary_CheckDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExist on CheckSummary_CheckDetailsTable...", Logger.MessageType.INF);
			Control APPCKA_CheckSummary_CheckDetailsTable = new Control("CheckSummary_CheckDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APPCKA_CHECKDETAIL_VCHRHDRHS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPCKA_CheckSummary_CheckDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming ClickButton on CheckSummary_CheckDetailsForm...", Logger.MessageType.INF);
			Control APPCKA_CheckSummary_CheckDetailsForm = new Control("CheckSummary_CheckDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APPCKA_CHECKDETAIL_VCHRHDRHS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APPCKA_CheckSummary_CheckDetailsForm);
formBttn = APPCKA_CheckSummary_CheckDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APPCKA_CheckSummary_CheckDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APPCKA_CheckSummary_CheckDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APPCKA";
							CPCommon.AssertEqual(true,APPCKA_CheckSummary_CheckDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "APPCKA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPCKA] Perfoming VerifyExists on CheckSummary_CheckDetails_ApprovalInfo_UserID...", Logger.MessageType.INF);
			Control APPCKA_CheckSummary_CheckDetails_ApprovalInfo_UserID = new Control("CheckSummary_CheckDetails_ApprovalInfo_UserID", "xpath", "//div[translate(@id,'0123456789','')='pr__APPCKA_CHECKDETAIL_VCHRHDRHS_']/ancestor::form[1]/descendant::*[@id='APPRVR_USER_ID']");
			CPCommon.AssertEqual(true,APPCKA_CheckSummary_CheckDetails_ApprovalInfo_UserID.Exists());

												
				CPCommon.CurrentComponent = "APPCKA";
							CPCommon.WaitControlDisplayed(APPCKA_CheckSummary_CheckDetailsForm);
formBttn = APPCKA_CheckSummary_CheckDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APPCKA";
							CPCommon.WaitControlDisplayed(APPCKA_MainForm);
formBttn = APPCKA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

