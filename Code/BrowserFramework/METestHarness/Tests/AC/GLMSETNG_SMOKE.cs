 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMSETNG_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Controls", "xpath","//div[@class='navItem'][.='General Ledger Controls']").Click();
new Control("Configure General Ledger Settings", "xpath","//div[@class='navItem'][.='Configure General Ledger Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMSETNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMSETNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on Company...", Logger.MessageType.INF);
			Control GLMSETNG_Company = new Control("Company", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMPANY_ID']");
			CPCommon.AssertEqual(true,GLMSETNG_Company.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on CorporateSettingsLink...", Logger.MessageType.INF);
			Control GLMSETNG_CorporateSettingsLink = new Control("CorporateSettingsLink", "ID", "lnk_1001470_GLMSETNG_GLCONFIG_HDR");
			CPCommon.AssertEqual(true,GLMSETNG_CorporateSettingsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
							CPCommon.WaitControlDisplayed(GLMSETNG_CorporateSettingsLink);
GLMSETNG_CorporateSettingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on CorporateGLSettingForm...", Logger.MessageType.INF);
			Control GLMSETNG_CorporateGLSettingForm = new Control("CorporateGLSettingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETNG_GLCONFIGCORP_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMSETNG_CorporateGLSettingForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on CorporateGLSettings_AccountStructure_NoOfLevels...", Logger.MessageType.INF);
			Control GLMSETNG_CorporateGLSettings_AccountStructure_NoOfLevels = new Control("CorporateGLSettings_AccountStructure_NoOfLevels", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETNG_GLCONFIGCORP_HDR_']/ancestor::form[1]/descendant::*[@id='ACCT_LVLS_NO']");
			CPCommon.AssertEqual(true,GLMSETNG_CorporateGLSettings_AccountStructure_NoOfLevels.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExist on CorporateFLSettingChildFormTable...", Logger.MessageType.INF);
			Control GLMSETNG_CorporateFLSettingChildFormTable = new Control("CorporateFLSettingChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETNG_GLCONFIGCORP_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSETNG_CorporateFLSettingChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on CorporateFLSettingChildForm...", Logger.MessageType.INF);
			Control GLMSETNG_CorporateFLSettingChildForm = new Control("CorporateFLSettingChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETNG_GLCONFIGCORP_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMSETNG_CorporateFLSettingChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
							CPCommon.WaitControlDisplayed(GLMSETNG_CorporateGLSettingForm);
IWebElement formBttn = GLMSETNG_CorporateGLSettingForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on ApprovalSettingsLink...", Logger.MessageType.INF);
			Control GLMSETNG_ApprovalSettingsLink = new Control("ApprovalSettingsLink", "ID", "lnk_3473_GLMSETNG_GLCONFIG_HDR");
			CPCommon.AssertEqual(true,GLMSETNG_ApprovalSettingsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
							CPCommon.WaitControlDisplayed(GLMSETNG_ApprovalSettingsLink);
GLMSETNG_ApprovalSettingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExists on ApprovalSettingForm...", Logger.MessageType.INF);
			Control GLMSETNG_ApprovalSettingForm = new Control("ApprovalSettingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETNG_GLCONFIGAPPRVL_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMSETNG_ApprovalSettingForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSETNG] Perfoming VerifyExist on ApprovalSettingFormTable...", Logger.MessageType.INF);
			Control GLMSETNG_ApprovalSettingFormTable = new Control("ApprovalSettingFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSETNG_GLCONFIGAPPRVL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSETNG_ApprovalSettingFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSETNG";
							CPCommon.WaitControlDisplayed(GLMSETNG_ApprovalSettingForm);
formBttn = GLMSETNG_ApprovalSettingForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "GLMSETNG";
							CPCommon.WaitControlDisplayed(GLMSETNG_MainForm);
formBttn = GLMSETNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

