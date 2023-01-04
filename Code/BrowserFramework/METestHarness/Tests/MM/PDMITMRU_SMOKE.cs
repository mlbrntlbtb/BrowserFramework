 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMITMRU_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Product Definition Controls", "xpath","//div[@class='navItem'][.='Product Definition Controls']").Click();
new Control("Configure Product Definition Settings", "xpath","//div[@class='navItem'][.='Configure Product Definition Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PDMITMRU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMITMRU] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMITMRU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMITMRU_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMITMRU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMITMRU] Perfoming VerifyExists on CompanyCAGECode...", Logger.MessageType.INF);
			Control PDMITMRU_CompanyCAGECode = new Control("CompanyCAGECode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CAGE_ID_FLD']");
			CPCommon.AssertEqual(true,PDMITMRU_CompanyCAGECode.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PDMITMRU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMITMRU] Perfoming VerifyExists on CorporateSettingForm...", Logger.MessageType.INF);
			Control PDMITMRU_CorporateSettingForm = new Control("CorporateSettingForm", "xpath", "//div[@class='gBxPIBHdrLabel' and contains(.,'Settings')]/ancestor::div[@class='rsltst']/descendant::form[1]");
			CPCommon.AssertEqual(true,PDMITMRU_CorporateSettingForm.Exists());

												
				CPCommon.CurrentComponent = "PDMITMRU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMITMRU] Perfoming VerifyExists on CorporateSettings_SeparateItemsByCompanyLabel...", Logger.MessageType.INF);
			Control PDMITMRU_CorporateSettings_SeparateItemsByCompanyLabel = new Control("CorporateSettings_SeparateItemsByCompanyLabel", "xpath", "//input[@id='ITEMS_BY_COMP_FL']/ancestor::form[1]/descendant::*[@id='ITEMS_BY_COMP_FL']/preceding-sibling::span[1]");
			CPCommon.AssertEqual(true,PDMITMRU_CorporateSettings_SeparateItemsByCompanyLabel.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PDMITMRU";
							CPCommon.WaitControlDisplayed(PDMITMRU_MainForm);
IWebElement formBttn = PDMITMRU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

