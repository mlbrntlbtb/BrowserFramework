 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMCOST_SMOKE : TestScript
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
new Control("Overtime Controls", "xpath","//div[@class='navItem'][.='Overtime Controls']").Click();
new Control("Configure Weighted Average Overtime Settings", "xpath","//div[@class='navItem'][.='Configure Weighted Average Overtime Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCOST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMCOST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMCOST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCOST] Perfoming VerifyExists on TimesheetCycle...", Logger.MessageType.INF);
			Control LDMCOST_TimesheetCycle = new Control("TimesheetCycle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TS_PD_CD']");
			CPCommon.AssertEqual(true,LDMCOST_TimesheetCycle.Exists());

												
				CPCommon.CurrentComponent = "LDMCOST";
							CPCommon.WaitControlDisplayed(LDMCOST_MainForm);
IWebElement formBttn = LDMCOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMCOST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMCOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCOST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMCOST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCOST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCOST] Perfoming VerifyExists on WeightedAveragePeriodsForm...", Logger.MessageType.INF);
			Control LDMCOST_WeightedAveragePeriodsForm = new Control("WeightedAveragePeriodsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCOST_WTAVGPDSCH_WEIGAVGPRD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMCOST_WeightedAveragePeriodsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCOST] Perfoming VerifyExist on WeightedAveragePeriodsFormTable...", Logger.MessageType.INF);
			Control LDMCOST_WeightedAveragePeriodsFormTable = new Control("WeightedAveragePeriodsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCOST_WTAVGPDSCH_WEIGAVGPRD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCOST_WeightedAveragePeriodsFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMCOST";
							CPCommon.WaitControlDisplayed(LDMCOST_WeightedAveragePeriodsForm);
formBttn = LDMCOST_WeightedAveragePeriodsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMCOST_WeightedAveragePeriodsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMCOST_WeightedAveragePeriodsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCOST] Perfoming VerifyExists on WeightedAveragePeriods_StartDate...", Logger.MessageType.INF);
			Control LDMCOST_WeightedAveragePeriods_StartDate = new Control("WeightedAveragePeriods_StartDate", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCOST_WTAVGPDSCH_WEIGAVGPRD_']/ancestor::form[1]/descendant::*[@id='START_DT']");
			CPCommon.AssertEqual(true,LDMCOST_WeightedAveragePeriods_StartDate.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDMCOST";
							CPCommon.WaitControlDisplayed(LDMCOST_MainForm);
formBttn = LDMCOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

