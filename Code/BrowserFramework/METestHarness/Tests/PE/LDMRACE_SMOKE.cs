 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMRACE_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee Controls", "xpath","//div[@class='navItem'][.='Employee Controls']").Click();
new Control("Manage Race and Ethnicity Codes", "xpath","//div[@class='navItem'][.='Manage Race and Ethnicity Codes']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMRACE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMRACE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming VerifyExist on RaceEthnicityCodes_RaceEthnicityCodesTable...", Logger.MessageType.INF);
			Control LDMRACE_RaceEthnicityCodes_RaceEthnicityCodesTable = new Control("RaceEthnicityCodes_RaceEthnicityCodesTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMRACE_RaceEthnicityCodes_RaceEthnicityCodesTable.Exists());

											Driver.SessionLogger.WriteLine("Affirmative Action Code Mappings");


												
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming VerifyExists on RaceEthnicityCodes_AffirmativeActionCodeMappingsLink...", Logger.MessageType.INF);
			Control LDMRACE_RaceEthnicityCodes_AffirmativeActionCodeMappingsLink = new Control("RaceEthnicityCodes_AffirmativeActionCodeMappingsLink", "ID", "lnk_4300_LDMRACE_HDR");
			CPCommon.AssertEqual(true,LDMRACE_RaceEthnicityCodes_AffirmativeActionCodeMappingsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMRACE";
							CPCommon.WaitControlDisplayed(LDMRACE_RaceEthnicityCodes_AffirmativeActionCodeMappingsLink);
LDMRACE_RaceEthnicityCodes_AffirmativeActionCodeMappingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming VerifyExist on AffirmativeActionCodeMappingsTable...", Logger.MessageType.INF);
			Control LDMRACE_AffirmativeActionCodeMappingsTable = new Control("AffirmativeActionCodeMappingsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMRACE_AFF_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMRACE_AffirmativeActionCodeMappingsTable.Exists());

												
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming Close on AffirmativeActionCodeMappingsForm...", Logger.MessageType.INF);
			Control LDMRACE_AffirmativeActionCodeMappingsForm = new Control("AffirmativeActionCodeMappingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMRACE_AFF_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(LDMRACE_AffirmativeActionCodeMappingsForm);
IWebElement formBttn = LDMRACE_AffirmativeActionCodeMappingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("EEO-1 Code Mappings");


												
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming VerifyExists on RaceEthnicityCodes_EEO1CodeMappingsLink...", Logger.MessageType.INF);
			Control LDMRACE_RaceEthnicityCodes_EEO1CodeMappingsLink = new Control("RaceEthnicityCodes_EEO1CodeMappingsLink", "ID", "lnk_4302_LDMRACE_HDR");
			CPCommon.AssertEqual(true,LDMRACE_RaceEthnicityCodes_EEO1CodeMappingsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMRACE";
							CPCommon.WaitControlDisplayed(LDMRACE_RaceEthnicityCodes_EEO1CodeMappingsLink);
LDMRACE_RaceEthnicityCodes_EEO1CodeMappingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMRACE] Perfoming VerifyExist on EEO1CodeMappingsTable...", Logger.MessageType.INF);
			Control LDMRACE_EEO1CodeMappingsTable = new Control("EEO1CodeMappingsTable", "xpath", "//div[starts-with(@id,'pr__LDMRACE_EEO1_CHILD_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMRACE_EEO1CodeMappingsTable.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "LDMRACE";
							CPCommon.WaitControlDisplayed(LDMRACE_MainForm);
formBttn = LDMRACE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

