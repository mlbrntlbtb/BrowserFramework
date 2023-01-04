 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMSET_SMOKE : TestScript
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
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("Engineering Change Controls", "xpath","//div[@class='navItem'][.='Engineering Change Controls']").Click();
new Control("Configure Engineering Change Notice Settings", "xpath","//div[@class='navItem'][.='Configure Engineering Change Notice Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "ECMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMSET] Perfoming VerifyExists on LastECNID...", Logger.MessageType.INF);
			Control ECMSET_LastECNID = new Control("LastECNID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LAST_ECN_ID']");
			CPCommon.AssertEqual(true,ECMSET_LastECNID.Exists());

												
				CPCommon.CurrentComponent = "ECMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMSET] Perfoming VerifyExists on ECNCorporateSettings...", Logger.MessageType.INF);
			Control ECMSET_ECNCorporateSettings = new Control("ECNCorporateSettings", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMSET_ECSETTINGSCORP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMSET_ECNCorporateSettings.Exists());

												
				CPCommon.CurrentComponent = "ECMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMSET] Perfoming VerifyExists on ECNCorporateSettings_AllowMultipleOpenECNsForTheSamePart...", Logger.MessageType.INF);
			Control ECMSET_ECNCorporateSettings_AllowMultipleOpenECNsForTheSamePart = new Control("ECNCorporateSettings_AllowMultipleOpenECNsForTheSamePart", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMSET_ECSETTINGSCORP_']/ancestor::form[1]/descendant::*[@id='ALLOW_MULT_ECN_FL']");
			CPCommon.AssertEqual(true,ECMSET_ECNCorporateSettings_AllowMultipleOpenECNsForTheSamePart.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "ECMSET";
							CPCommon.WaitControlDisplayed(ECMSET_MainForm);
IWebElement formBttn = ECMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

