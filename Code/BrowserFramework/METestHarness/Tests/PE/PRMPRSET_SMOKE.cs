 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMPRSET_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Controls", "xpath","//div[@class='navItem'][.='Payroll Controls']").Click();
new Control("Configure Payroll Settings", "xpath","//div[@class='navItem'][.='Configure Payroll Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMPRSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMPRSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on PostingAccounts_FederalIncomeTaxWH...", Logger.MessageType.INF);
			Control PRMPRSET_PostingAccounts_FederalIncomeTaxWH = new Control("PostingAccounts_FederalIncomeTaxWH", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FED_WH_ACCT_ID']");
			CPCommon.AssertEqual(true,PRMPRSET_PostingAccounts_FederalIncomeTaxWH.Exists());

											Driver.SessionLogger.WriteLine("Direct Charge Options Form");


												
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on PostingAccounts_DirectChargeOptionsLink...", Logger.MessageType.INF);
			Control PRMPRSET_PostingAccounts_DirectChargeOptionsLink = new Control("PostingAccounts_DirectChargeOptionsLink", "ID", "lnk_1729_PRMPRSET_PRSETTINGS");
			CPCommon.AssertEqual(true,PRMPRSET_PostingAccounts_DirectChargeOptionsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
							CPCommon.WaitControlDisplayed(PRMPRSET_PostingAccounts_DirectChargeOptionsLink);
PRMPRSET_PostingAccounts_DirectChargeOptionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on DirectChargeOptionsForm...", Logger.MessageType.INF);
			Control PRMPRSET_DirectChargeOptionsForm = new Control("DirectChargeOptionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPRSET_PRSETTINGS_DIR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPRSET_DirectChargeOptionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on DirectChargeOptions_DirectChargeSocialSecurityExpensePosting_SocialSecurityExpenseAccount...", Logger.MessageType.INF);
			Control PRMPRSET_DirectChargeOptions_DirectChargeSocialSecurityExpensePosting_SocialSecurityExpenseAccount = new Control("DirectChargeOptions_DirectChargeSocialSecurityExpensePosting_SocialSecurityExpenseAccount", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPRSET_PRSETTINGS_DIR_']/ancestor::form[1]/descendant::*[@id='DC_SS_EXP_ACCT_ID']");
			CPCommon.AssertEqual(true,PRMPRSET_DirectChargeOptions_DirectChargeSocialSecurityExpensePosting_SocialSecurityExpenseAccount.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
							CPCommon.WaitControlDisplayed(PRMPRSET_DirectChargeOptionsForm);
IWebElement formBttn = PRMPRSET_DirectChargeOptionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Reference Number Settings Form");


												
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on PostingAccounts_ReferenceNumberSettingsLink...", Logger.MessageType.INF);
			Control PRMPRSET_PostingAccounts_ReferenceNumberSettingsLink = new Control("PostingAccounts_ReferenceNumberSettingsLink", "ID", "lnk_3835_PRMPRSET_PRSETTINGS");
			CPCommon.AssertEqual(true,PRMPRSET_PostingAccounts_ReferenceNumberSettingsLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
							CPCommon.WaitControlDisplayed(PRMPRSET_PostingAccounts_ReferenceNumberSettingsLink);
PRMPRSET_PostingAccounts_ReferenceNumberSettingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on PostingForm...", Logger.MessageType.INF);
			Control PRMPRSET_PostingForm = new Control("PostingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPRSET_PRSETTINGS_REFNUMBER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPRSET_PostingForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRSET] Perfoming VerifyExists on PostingAccounts_PayrollVariance...", Logger.MessageType.INF);
			Control PRMPRSET_PostingAccounts_PayrollVariance = new Control("PostingAccounts_PayrollVariance", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PR_VAR_ACCT_ID']");
			CPCommon.AssertEqual(true,PRMPRSET_PostingAccounts_PayrollVariance.Exists());

												
				CPCommon.CurrentComponent = "PRMPRSET";
							CPCommon.WaitControlDisplayed(PRMPRSET_PostingForm);
formBttn = PRMPRSET_PostingForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMPRSET";
							CPCommon.WaitControlDisplayed(PRMPRSET_MainForm);
formBttn = PRMPRSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

