 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMTSPD_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Entry/Creation", "xpath","//div[@class='navItem'][.='Timesheet Entry/Creation']").Click();
new Control("Manage Timesheet Periods", "xpath","//div[@class='navItem'][.='Manage Timesheet Periods']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMTSPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTSPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMTSPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMTSPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMTSPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTSPD] Perfoming VerifyExists on TimesheetCycleCode...", Logger.MessageType.INF);
			Control LDMTSPD_TimesheetCycleCode = new Control("TimesheetCycleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TS_PD_CD']");
			CPCommon.AssertEqual(true,LDMTSPD_TimesheetCycleCode.Exists());

												
				CPCommon.CurrentComponent = "LDMTSPD";
							CPCommon.WaitControlDisplayed(LDMTSPD_MainForm);
IWebElement formBttn = LDMTSPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMTSPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMTSPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMTSPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTSPD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMTSPD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMTSPD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("TIMESHEET PERIODS DETAILS");


												
				CPCommon.CurrentComponent = "LDMTSPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMTSPD] Perfoming VerifyExist on TimesheetPeriodsDetailsTable...", Logger.MessageType.INF);
			Control LDMTSPD_TimesheetPeriodsDetailsTable = new Control("TimesheetPeriodsDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMTSPD_TSPDSCH_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMTSPD_TimesheetPeriodsDetailsTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMTSPD";
							CPCommon.WaitControlDisplayed(LDMTSPD_MainForm);
formBttn = LDMTSPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

