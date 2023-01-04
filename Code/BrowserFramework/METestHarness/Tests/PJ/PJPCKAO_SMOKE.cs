 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCKAO_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost and Revenue Processing Utilities", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Utilities']").Click();
new Control("Validate Pool Acct/Org Setups", "xpath","//div[@class='navItem'][.='Validate Pool Acct/Org Setups']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming VerifyExists on SelectionRangesForm...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRangesForm = new Control("SelectionRangesForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCKAO_SelectionRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming VerifyExists on SelectionRanges_Option_FiscalYear...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_Option_FiscalYear = new Control("SelectionRanges_Option_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_RAN']");
			CPCommon.AssertEqual(true,PJPCKAO_SelectionRanges_Option_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming Set on SelectionRanges_Start_FiscalYear...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_Start_FiscalYear = new Control("SelectionRanges_Start_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD_FR']");
			PJPCKAO_SelectionRanges_Start_FiscalYear.Click();
PJPCKAO_SelectionRanges_Start_FiscalYear.SendKeys("Y2010", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJPCKAO_SelectionRanges_Start_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming Set on SelectionRanges_End_FiscalYear...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_End_FiscalYear = new Control("SelectionRanges_End_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD_TO']");
			PJPCKAO_SelectionRanges_End_FiscalYear.Click();
PJPCKAO_SelectionRanges_End_FiscalYear.SendKeys("X2010", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJPCKAO_SelectionRanges_End_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming Set on SelectionRanges_Start_AllocationGroupNumber...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_Start_AllocationGroupNumber = new Control("SelectionRanges_Start_AllocationGroupNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALLOC_GRP_NO']");
			PJPCKAO_SelectionRanges_Start_AllocationGroupNumber.Click();
PJPCKAO_SelectionRanges_Start_AllocationGroupNumber.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJPCKAO_SelectionRanges_Start_AllocationGroupNumber.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											Driver.SessionLogger.WriteLine("ErrorDetails");


												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming VerifyExist on SelectionRanges_ErrorDetailsFormTable...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_ErrorDetailsFormTable = new Control("SelectionRanges_ErrorDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCKAO_ZPJPTOOLERRORS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCKAO_SelectionRanges_ErrorDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming ClickButton on SelectionRanges_ErrorDetailsForm...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_ErrorDetailsForm = new Control("SelectionRanges_ErrorDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCKAO_ZPJPTOOLERRORS_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJPCKAO_SelectionRanges_ErrorDetailsForm);
IWebElement formBttn = PJPCKAO_SelectionRanges_ErrorDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJPCKAO_SelectionRanges_ErrorDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJPCKAO_SelectionRanges_ErrorDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJPCKAO";
							CPCommon.AssertEqual(true,PJPCKAO_SelectionRanges_ErrorDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJPCKAO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKAO] Perfoming VerifyExists on SelectionRanges_ErrorDetails_FiscalYear...", Logger.MessageType.INF);
			Control PJPCKAO_SelectionRanges_ErrorDetails_FiscalYear = new Control("SelectionRanges_ErrorDetails_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCKAO_ZPJPTOOLERRORS_CHLD_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJPCKAO_SelectionRanges_ErrorDetails_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJPCKAO";
							CPCommon.WaitControlDisplayed(PJPCKAO_SelectionRangesForm);
formBttn = PJPCKAO_SelectionRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

