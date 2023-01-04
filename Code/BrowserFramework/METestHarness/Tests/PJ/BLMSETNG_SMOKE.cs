 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMSETNG_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing Controls", "xpath","//div[@class='navItem'][.='Billing Controls']").Click();
new Control("Configure Billing Settings", "xpath","//div[@class='navItem'][.='Configure Billing Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "BLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSETNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMSETNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMSETNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSETNG] Perfoming VerifyExists on Settings_Options_LastCompanyWideInvoiceNo...", Logger.MessageType.INF);
			Control BLMSETNG_Settings_Options_LastCompanyWideInvoiceNo = new Control("Settings_Options_LastCompanyWideInvoiceNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LAST_CW_INVC_ID']");
			CPCommon.AssertEqual(true,BLMSETNG_Settings_Options_LastCompanyWideInvoiceNo.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "BLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSETNG] Perfoming VerifyExists on Settings_Options_SelectedBillingFormulasLink...", Logger.MessageType.INF);
			Control BLMSETNG_Settings_Options_SelectedBillingFormulasLink = new Control("Settings_Options_SelectedBillingFormulasLink", "ID", "lnk_1002353_BLMSETNG_BILLSETTINGS_HDR");
			CPCommon.AssertEqual(true,BLMSETNG_Settings_Options_SelectedBillingFormulasLink.Exists());

												
				CPCommon.CurrentComponent = "BLMSETNG";
							CPCommon.WaitControlDisplayed(BLMSETNG_Settings_Options_SelectedBillingFormulasLink);
BLMSETNG_Settings_Options_SelectedBillingFormulasLink.Click(1.5);


												Driver.SessionLogger.WriteLine("LINK TABLE");


												
				CPCommon.CurrentComponent = "BLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSETNG] Perfoming VerifyExist on BillingFormulas_Table...", Logger.MessageType.INF);
			Control BLMSETNG_BillingFormulas_Table = new Control("BillingFormulas_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMSETNG_SBILLFORMULA_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMSETNG_BillingFormulas_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSETNG] Perfoming VerifyExist on SelectedBillingFormulas_Table...", Logger.MessageType.INF);
			Control BLMSETNG_SelectedBillingFormulas_Table = new Control("SelectedBillingFormulas_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMSETNG_BILLFORMULACOMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMSETNG_SelectedBillingFormulas_Table.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMSETNG";
							CPCommon.WaitControlDisplayed(BLMSETNG_MainForm);
IWebElement formBttn = BLMSETNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

