 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSLE_SMOKE : TestScript
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
CP7Main_SearchApplications.SendKeys("AOMESSLE", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "AOMESSLE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSLE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMESSLE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMESSLE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSLE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSLE] Perfoming VerifyExists on LifeEvent...", Logger.MessageType.INF);
			Control AOMESSLE_LifeEvent = new Control("LifeEvent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='QUAL_EVENT_CD']");
			CPCommon.AssertEqual(true,AOMESSLE_LifeEvent.Exists());

												
				CPCommon.CurrentComponent = "AOMESSLE";
							CPCommon.WaitControlDisplayed(AOMESSLE_MainForm);
IWebElement formBttn = AOMESSLE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSLE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSLE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSLE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSLE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSLE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSLE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("ESS User Flow");


												
				CPCommon.CurrentComponent = "AOMESSLE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSLE] Perfoming VerifyExists on ESSUserFlowForm...", Logger.MessageType.INF);
			Control AOMESSLE_ESSUserFlowForm = new Control("ESSUserFlowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSLE_SESSMODULE_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSLE_ESSUserFlowForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSLE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSLE] Perfoming VerifyExist on ESSUserFlowFormTable...", Logger.MessageType.INF);
			Control AOMESSLE_ESSUserFlowFormTable = new Control("ESSUserFlowFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSLE_SESSMODULE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSLE_ESSUserFlowFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSLE";
							CPCommon.WaitControlDisplayed(AOMESSLE_MainForm);
formBttn = AOMESSLE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

