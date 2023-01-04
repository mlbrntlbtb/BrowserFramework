 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSST_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Set on SearchApplications...", Logger.MessageType.INF);
			Control CP7Main_SearchApplications = new Control("SearchApplications", "ID", "appFltrFld");
			CP7Main_SearchApplications.Click();
CP7Main_SearchApplications.SendKeys("AOMESSST", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


												
				CPCommon.CurrentComponent = "AOMESSST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMESSST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMESSST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSST] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control AOMESSST_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,AOMESSST_State.Exists());

												
				CPCommon.CurrentComponent = "AOMESSST";
							CPCommon.WaitControlDisplayed(AOMESSST_MainForm);
IWebElement formBttn = AOMESSST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSST] Perfoming VerifyExists on FilingStatusForm...", Logger.MessageType.INF);
			Control AOMESSST_FilingStatusForm = new Control("FilingStatusForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSST_ESSSTATETAXLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSST_FilingStatusForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSST] Perfoming VerifyExist on FilingStatusFormTable...", Logger.MessageType.INF);
			Control AOMESSST_FilingStatusFormTable = new Control("FilingStatusFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSST_ESSSTATETAXLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSST_FilingStatusFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSST";
							CPCommon.WaitControlDisplayed(AOMESSST_FilingStatusForm);
formBttn = AOMESSST_FilingStatusForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMESSST_FilingStatusForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMESSST_FilingStatusForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "AOMESSST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSST] Perfoming VerifyExists on FilingStatus_FilingStatus...", Logger.MessageType.INF);
			Control AOMESSST_FilingStatus_FilingStatus = new Control("FilingStatus_FilingStatus", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSST_ESSSTATETAXLN_CTW_']/ancestor::form[1]/descendant::*[@id='S_ST_FIL_STAT_CD']");
			CPCommon.AssertEqual(true,AOMESSST_FilingStatus_FilingStatus.Exists());

												
				CPCommon.CurrentComponent = "AOMESSST";
							CPCommon.WaitControlDisplayed(AOMESSST_MainForm);
formBttn = AOMESSST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

