 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPAW_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Charging Information", "xpath","//div[@class='navItem'][.='Charging Information']").Click();
new Control("Link Projects/Accounts", "xpath","//div[@class='navItem'][.='Link Projects/Accounts']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExist on ProjectAcctsFormTable...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAcctsFormTable = new Control("ProjectAcctsFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAcctsFormTable.Exists());

											Driver.SessionLogger.WriteLine("Wild Card Option");


												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExists on ProjectAccts_WildcardOptionLink...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_WildcardOptionLink = new Control("ProjectAccts_WildcardOptionLink", "ID", "lnk_1004306_PJM_PROJACCTWILDCARD_HDR");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_WildcardOptionLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
							CPCommon.WaitControlDisplayed(PJMPAW_ProjectAccts_WildcardOptionLink);
PJMPAW_ProjectAccts_WildcardOptionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExists on ProjectAccts_WildCardOptionsForm...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_WildCardOptionsForm = new Control("ProjectAccts_WildCardOptionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__WILD_CARD_OPTION_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_WildCardOptionsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExists on ProjectAccts_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel = new Control("ProjectAccts_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel", "xpath", "//span[@id='LBL1']/ancestor::form[1]/descendant::*[@id='LBL1']");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
							CPCommon.WaitControlDisplayed(PJMPAW_ProjectAccts_WildCardOptionsForm);
IWebElement formBttn = PJMPAW_ProjectAccts_WildCardOptionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Sample Projects");


												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExists on ProjectAccts_SampleProjectsLink...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_SampleProjectsLink = new Control("ProjectAccts_SampleProjectsLink", "ID", "lnk_1006845_PJM_PROJACCTWILDCARD_HDR");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_SampleProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
							CPCommon.WaitControlDisplayed(PJMPAW_ProjectAccts_SampleProjectsLink);
PJMPAW_ProjectAccts_SampleProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExist on ProjectAccts_SampleProjectsFormTable...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_SampleProjectsFormTable = new Control("ProjectAccts_SampleProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJPAW_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_SampleProjectsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming Close on ProjectAccts_SampleProjectsForm...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_SampleProjectsForm = new Control("ProjectAccts_SampleProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJPAW_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPAW_ProjectAccts_SampleProjectsForm);
formBttn = PJMPAW_ProjectAccts_SampleProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Sample Accounts");


												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExists on ProjectAccts_SampleAccountsLink...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_SampleAccountsLink = new Control("ProjectAccts_SampleAccountsLink", "ID", "lnk_1006844_PJM_PROJACCTWILDCARD_HDR");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_SampleAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
							CPCommon.WaitControlDisplayed(PJMPAW_ProjectAccts_SampleAccountsLink);
PJMPAW_ProjectAccts_SampleAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming VerifyExist on ProjectAccts_SampleAccountsFormTable...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_SampleAccountsFormTable = new Control("ProjectAccts_SampleAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_ACCTPAW_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPAW_ProjectAccts_SampleAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming Close on ProjectAccts_SampleAccountsForm...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAccts_SampleAccountsForm = new Control("ProjectAccts_SampleAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_ACCTPAW_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPAW_ProjectAccts_SampleAccountsForm);
formBttn = PJMPAW_ProjectAccts_SampleAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMPAW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPAW] Perfoming Close on ProjectAcctsForm...", Logger.MessageType.INF);
			Control PJMPAW_ProjectAcctsForm = new Control("ProjectAcctsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMPAW_ProjectAcctsForm);
formBttn = PJMPAW_ProjectAcctsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

