 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMAMORT_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Manage Deferred Revenue Amortization Schedules", "xpath","//div[@class='navItem'][.='Manage Deferred Revenue Amortization Schedules']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEMAMORT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAMORT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMAMORT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMAMORT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAMORT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAMORT] Perfoming VerifyExists on AmortizationScheduleCode...", Logger.MessageType.INF);
			Control OEMAMORT_AmortizationScheduleCode = new Control("AmortizationScheduleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='AMORT_CD']");
			CPCommon.AssertEqual(true,OEMAMORT_AmortizationScheduleCode.Exists());

												
				CPCommon.CurrentComponent = "OEMAMORT";
							CPCommon.WaitControlDisplayed(OEMAMORT_MainForm);
IWebElement formBttn = OEMAMORT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMAMORT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMAMORT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMAMORT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAMORT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMAMORT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAMORT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("AMMORTIZATIONSCHED");


												
				CPCommon.CurrentComponent = "OEMAMORT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAMORT] Perfoming VerifyExist on AmortizationScheduleFormTable...", Logger.MessageType.INF);
			Control OEMAMORT_AmortizationScheduleFormTable = new Control("AmortizationScheduleFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAMORT_AMORTSCHPD_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAMORT_AmortizationScheduleFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAMORT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAMORT] Perfoming Close on AmortizationScheduleForm...", Logger.MessageType.INF);
			Control OEMAMORT_AmortizationScheduleForm = new Control("AmortizationScheduleForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAMORT_AMORTSCHPD_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMAMORT_AmortizationScheduleForm);
formBttn = OEMAMORT_AmortizationScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "OEMAMORT";
							CPCommon.WaitControlDisplayed(OEMAMORT_MainForm);
formBttn = OEMAMORT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

