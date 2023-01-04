 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMSET_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='deptItem'][.='Bills of Material']").Click();
new Control("Bills of Material Controls", "xpath","//div[@class='navItem'][.='Bills of Material Controls']").Click();
new Control("Configure Bills of Material Settings", "xpath","//div[@class='navItem'][.='Configure Bills of Material Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BMMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMSET] Perfoming VerifyExists on MBOMReleaseControl_UserSeparateMBOMReleaseFunction...", Logger.MessageType.INF);
			Control BMMSET_MBOMReleaseControl_UserSeparateMBOMReleaseFunction = new Control("MBOMReleaseControl_UserSeparateMBOMReleaseFunction", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='USE_REL_FUNC_FL']");
			CPCommon.AssertEqual(true,BMMSET_MBOMReleaseControl_UserSeparateMBOMReleaseFunction.Exists());

											Driver.SessionLogger.WriteLine("BOM CORPORATE SETTINGS");


												
				CPCommon.CurrentComponent = "BMMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMSET] Perfoming VerifyExists on BOMCorporateSettingsForm...", Logger.MessageType.INF);
			Control BMMSET_BOMCorporateSettingsForm = new Control("BOMCorporateSettingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMSET_BOMSETTINGSCORP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMSET_BOMCorporateSettingsForm.Exists());

												
				CPCommon.CurrentComponent = "BMMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMSET] Perfoming VerifyExists on BOMCorporateSettings_UseConfigurationIDs...", Logger.MessageType.INF);
			Control BMMSET_BOMCorporateSettings_UseConfigurationIDs = new Control("BOMCorporateSettings_UseConfigurationIDs", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMSET_BOMSETTINGSCORP_']/ancestor::form[1]/descendant::*[@id='USE_CID_FL']");
			CPCommon.AssertEqual(true,BMMSET_BOMCorporateSettings_UseConfigurationIDs.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BMMSET";
							CPCommon.WaitControlDisplayed(BMMSET_MainForm);
IWebElement formBttn = BMMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

