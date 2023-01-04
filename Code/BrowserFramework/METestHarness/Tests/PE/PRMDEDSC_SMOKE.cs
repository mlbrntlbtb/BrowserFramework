 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMDEDSC_SMOKE : TestScript
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

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Deductions", "xpath","//div[@class='navItem'][.='Deductions']").Click();
new Control("Manage Deduction Schedules", "xpath","//div[@class='navItem'][.='Manage Deduction Schedules']").Click();


												
				CPCommon.CurrentComponent = "PRMDEDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDEDSC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMDEDSC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMDEDSC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMDEDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDEDSC] Perfoming VerifyExists on DeductionScheduleCode...", Logger.MessageType.INF);
			Control PRMDEDSC_DeductionScheduleCode = new Control("DeductionScheduleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DED_SCH_CD']");
			CPCommon.AssertEqual(true,PRMDEDSC_DeductionScheduleCode.Exists());

												
				CPCommon.CurrentComponent = "PRMDEDSC";
							CPCommon.WaitControlDisplayed(PRMDEDSC_MainForm);
IWebElement formBttn = PRMDEDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMDEDSC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMDEDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMDEDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDEDSC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMDEDSC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMDEDSC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMDEDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDEDSC] Perfoming Click on DeductionSchedulesLink...", Logger.MessageType.INF);
			Control PRMDEDSC_DeductionSchedulesLink = new Control("DeductionSchedulesLink", "ID", "lnk_3820_PRMDEDSC_DEDSCH_HDR");
			CPCommon.WaitControlDisplayed(PRMDEDSC_DeductionSchedulesLink);
PRMDEDSC_DeductionSchedulesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PRMDEDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDEDSC] Perfoming VerifyExists on DeductionSchedulesForm...", Logger.MessageType.INF);
			Control PRMDEDSC_DeductionSchedulesForm = new Control("DeductionSchedulesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMDEDSC_DEDSCHLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMDEDSC_DeductionSchedulesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMDEDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDEDSC] Perfoming VerifyExist on DeductionSchedulesFormTable...", Logger.MessageType.INF);
			Control PRMDEDSC_DeductionSchedulesFormTable = new Control("DeductionSchedulesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMDEDSC_DEDSCHLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMDEDSC_DeductionSchedulesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMDEDSC";
							CPCommon.WaitControlDisplayed(PRMDEDSC_DeductionSchedulesForm);
formBttn = PRMDEDSC_DeductionSchedulesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

