 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSCS_SMOKE : TestScript
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

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Set on SearchApplications...", Logger.MessageType.INF);
			Control CP7Main_SearchApplications = new Control("SearchApplications", "ID", "appFltrFld");
			CP7Main_SearchApplications.Click();
CP7Main_SearchApplications.SendKeys("AOMESSCS", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMESSCS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMESSCS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on TaxableEntity...", Logger.MessageType.INF);
			Control AOMESSCS_TaxableEntity = new Control("TaxableEntity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY_ID']");
			CPCommon.AssertEqual(true,AOMESSCS_TaxableEntity.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
							CPCommon.WaitControlDisplayed(AOMESSCS_MainForm);
IWebElement formBttn = AOMESSCS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSCS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSCS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSCS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSCS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Severance Pay Types");


												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on SeverancePayTypesLink...", Logger.MessageType.INF);
			Control AOMESSCS_SeverancePayTypesLink = new Control("SeverancePayTypesLink", "ID", "lnk_4875_AOMESSCS_ESSSETTINGS_HDR");
			CPCommon.AssertEqual(true,AOMESSCS_SeverancePayTypesLink.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
							CPCommon.WaitControlDisplayed(AOMESSCS_SeverancePayTypesLink);
AOMESSCS_SeverancePayTypesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on SeverancePayTypesForm...", Logger.MessageType.INF);
			Control AOMESSCS_SeverancePayTypesForm = new Control("SeverancePayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_ESSSEVPAYTP_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSCS_SeverancePayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExist on SeverancePayTypesFormTable...", Logger.MessageType.INF);
			Control AOMESSCS_SeverancePayTypesFormTable = new Control("SeverancePayTypesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_ESSSEVPAYTP_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSCS_SeverancePayTypesFormTable.Exists());

											Driver.SessionLogger.WriteLine("Deferred Compensation Settings");


												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on DeferredCompensationSettingsLink...", Logger.MessageType.INF);
			Control AOMESSCS_DeferredCompensationSettingsLink = new Control("DeferredCompensationSettingsLink", "ID", "lnk_4874_AOMESSCS_ESSSETTINGS_HDR");
			CPCommon.AssertEqual(true,AOMESSCS_DeferredCompensationSettingsLink.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
							CPCommon.WaitControlDisplayed(AOMESSCS_DeferredCompensationSettingsLink);
AOMESSCS_DeferredCompensationSettingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on DefferedCompensationSettingsForm...", Logger.MessageType.INF);
			Control AOMESSCS_DefferedCompensationSettingsForm = new Control("DefferedCompensationSettingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_DEFRCOMPSETNG_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSCS_DefferedCompensationSettingsForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on DefferedCompensationSettings_MinimumPercent...", Logger.MessageType.INF);
			Control AOMESSCS_DefferedCompensationSettings_MinimumPercent = new Control("DefferedCompensationSettings_MinimumPercent", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_DEFRCOMPSETNG_TBL_']/ancestor::form[1]/descendant::*[@id='MIN_DC_PCT']");
			CPCommon.AssertEqual(true,AOMESSCS_DefferedCompensationSettings_MinimumPercent.Exists());

											Driver.SessionLogger.WriteLine("Open Change Periods");


												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on DefferedCompensationSettings_OpenChangePeriodsForm...", Logger.MessageType.INF);
			Control AOMESSCS_DefferedCompensationSettings_OpenChangePeriodsForm = new Control("DefferedCompensationSettings_OpenChangePeriodsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_ESSSETTINGSDT_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSCS_DefferedCompensationSettings_OpenChangePeriodsForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExist on DefferedCompensationSettings_OpenChangePeriodsFormTable...", Logger.MessageType.INF);
			Control AOMESSCS_DefferedCompensationSettings_OpenChangePeriodsFormTable = new Control("DefferedCompensationSettings_OpenChangePeriodsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_ESSSETTINGSDT_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSCS_DefferedCompensationSettings_OpenChangePeriodsFormTable.Exists());

											Driver.SessionLogger.WriteLine("Deductions/Methods");


												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExists on DefferedCompensationSettings_DeductionsMethodsForm...", Logger.MessageType.INF);
			Control AOMESSCS_DefferedCompensationSettings_DeductionsMethodsForm = new Control("DefferedCompensationSettings_DeductionsMethodsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_ESSSETTINGSDED_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSCS_DefferedCompensationSettings_DeductionsMethodsForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCS] Perfoming VerifyExist on DefferedCompensationSettings_DeductionsMethodsFormTable...", Logger.MessageType.INF);
			Control AOMESSCS_DefferedCompensationSettings_DeductionsMethodsFormTable = new Control("DefferedCompensationSettings_DeductionsMethodsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCS_ESSSETTINGSDED_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSCS_DefferedCompensationSettings_DeductionsMethodsFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCS";
							CPCommon.WaitControlDisplayed(AOMESSCS_MainForm);
formBttn = AOMESSCS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

