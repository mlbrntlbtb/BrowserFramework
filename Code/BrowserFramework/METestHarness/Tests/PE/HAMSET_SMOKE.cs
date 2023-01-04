 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HAMSET_SMOKE : TestScript
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
new Control("Affirmative Action", "xpath","//div[@class='deptItem'][.='Affirmative Action']").Click();
new Control("Affirmative Action Controls", "xpath","//div[@class='navItem'][.='Affirmative Action Controls']").Click();
new Control("Configure Affirmative Action Settings", "xpath","//div[@class='navItem'][.='Configure Affirmative Action Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HAMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HAMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HAMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HAMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMSET] Perfoming VerifyExists on Controls_DefaultAffirmativeActionPlan...", Logger.MessageType.INF);
			Control HAMSET_Controls_DefaultAffirmativeActionPlan = new Control("Controls_DefaultAffirmativeActionPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_AFF_PLAN_CD']");
			CPCommon.AssertEqual(true,HAMSET_Controls_DefaultAffirmativeActionPlan.Exists());

												
				CPCommon.CurrentComponent = "HAMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMSET] Perfoming VerifyExists on EEOSetupLink...", Logger.MessageType.INF);
			Control HAMSET_EEOSetupLink = new Control("EEOSetupLink", "ID", "lnk_4084_HAMSET_HAFFSETTINGS");
			CPCommon.AssertEqual(true,HAMSET_EEOSetupLink.Exists());

												
				CPCommon.CurrentComponent = "HAMSET";
							CPCommon.WaitControlDisplayed(HAMSET_EEOSetupLink);
HAMSET_EEOSetupLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HAMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMSET] Perfoming VerifyExists on EEOSetupForm...", Logger.MessageType.INF);
			Control HAMSET_EEOSetupForm = new Control("EEOSetupForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMSET_EEOSETTINGS_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HAMSET_EEOSetupForm.Exists());

												
				CPCommon.CurrentComponent = "HAMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMSET] Perfoming VerifyExists on EEOSetup_EEOReportingForm_EEO1Report...", Logger.MessageType.INF);
			Control HAMSET_EEOSetup_EEOReportingForm_EEO1Report = new Control("EEOSetup_EEOReportingForm_EEO1Report", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMSET_EEOSETTINGS_CTW_']/ancestor::form[1]/descendant::*[@id='S_EEO_1OR4_CD' and @value='1']");
			CPCommon.AssertEqual(true,HAMSET_EEOSetup_EEOReportingForm_EEO1Report.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HAMSET";
							CPCommon.WaitControlDisplayed(HAMSET_MainForm);
IWebElement formBttn = HAMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

