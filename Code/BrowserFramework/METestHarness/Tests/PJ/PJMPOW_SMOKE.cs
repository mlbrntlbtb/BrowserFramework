 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPOW_SMOKE : TestScript
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
new Control("Link Projects/Organizations", "xpath","//div[@class='navItem'][.='Link Projects/Organizations']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExist on ProjectOrgsFormTable...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgsFormTable = new Control("ProjectOrgsFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgsFormTable.Exists());

											Driver.SessionLogger.WriteLine("Wild Card Option");


												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExists on ProjectOrgs_WildcardOptionLink...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_WildcardOptionLink = new Control("ProjectOrgs_WildcardOptionLink", "ID", "lnk_1004365_PJM_PROJORGWILDCARD_CTW");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_WildcardOptionLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
							CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgs_WildcardOptionLink);
PJMPOW_ProjectOrgs_WildcardOptionLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExists on ProjectOrgs_WildCardOptionsForm...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_WildCardOptionsForm = new Control("ProjectOrgs_WildCardOptionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__WILD_CARD_OPTION_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_WildCardOptionsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExists on ProjectOrgs_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel = new Control("ProjectOrgs_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel", "xpath", "//span[@id='LBL1']/ancestor::form[1]/descendant::*[@id='LBL1']");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_WildCardOptions_UseOr_AsWildcardExamplesAreShownBelowLabel.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
							CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgs_WildCardOptionsForm);
IWebElement formBttn = PJMPOW_ProjectOrgs_WildCardOptionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Sample Projects");


												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExists on ProjectOrgs_SampleProjectsLink...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_SampleProjectsLink = new Control("ProjectOrgs_SampleProjectsLink", "ID", "lnk_1006847_PJM_PROJORGWILDCARD_CTW");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_SampleProjectsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
							CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgs_SampleProjectsLink);
PJMPOW_ProjectOrgs_SampleProjectsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExist on ProjectOrgs_SampleProjectsFormTable...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_SampleProjectsFormTable = new Control("ProjectOrgs_SampleProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_POWPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_SampleProjectsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming Close on ProjectOrgs_SampleProjectsForm...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_SampleProjectsForm = new Control("ProjectOrgs_SampleProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_POWPROJ_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgs_SampleProjectsForm);
formBttn = PJMPOW_ProjectOrgs_SampleProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Sample Accounts");


												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExists on ProjectOrgs_SampleOrganizationsLink...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_SampleOrganizationsLink = new Control("ProjectOrgs_SampleOrganizationsLink", "ID", "lnk_1006846_PJM_PROJORGWILDCARD_CTW");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_SampleOrganizationsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
							CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgs_SampleOrganizationsLink);
PJMPOW_ProjectOrgs_SampleOrganizationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming VerifyExist on ProjectOrgs_SampleOrganizationsFormTable...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_SampleOrganizationsFormTable = new Control("ProjectOrgs_SampleOrganizationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_POWORG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOW_ProjectOrgs_SampleOrganizationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming Close on ProjectOrgs_SampleOrganizationsForm...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgs_SampleOrganizationsForm = new Control("ProjectOrgs_SampleOrganizationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_POWORG_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgs_SampleOrganizationsForm);
formBttn = PJMPOW_ProjectOrgs_SampleOrganizationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMPOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOW] Perfoming Close on ProjectOrgsForm...", Logger.MessageType.INF);
			Control PJMPOW_ProjectOrgsForm = new Control("ProjectOrgsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMPOW_ProjectOrgsForm);
formBttn = PJMPOW_ProjectOrgsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

