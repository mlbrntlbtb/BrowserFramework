 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMPRPD_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Processing", "xpath","//div[@class='navItem'][.='Payroll Processing']").Click();
new Control("Manage Pay Periods", "xpath","//div[@class='navItem'][.='Manage Pay Periods']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMPRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMPRPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMPRPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRPD] Perfoming VerifyExists on PayCycle...", Logger.MessageType.INF);
			Control PRMPRPD_PayCycle = new Control("PayCycle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,PRMPRPD_PayCycle.Exists());

												
				CPCommon.CurrentComponent = "PRMPRPD";
							CPCommon.WaitControlDisplayed(PRMPRPD_MainForm);
IWebElement formBttn = PRMPRPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMPRPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMPRPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMPRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRPD] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMPRPD_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPRPD_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Pay Cycle Schedule");


												
				CPCommon.CurrentComponent = "PRMPRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRPD] Perfoming VerifyExists on PayCycleScheduleLink...", Logger.MessageType.INF);
			Control PRMPRPD_PayCycleScheduleLink = new Control("PayCycleScheduleLink", "ID", "lnk_1002132_PRMPRPD_PAYPD");
			CPCommon.AssertEqual(true,PRMPRPD_PayCycleScheduleLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPRPD";
							CPCommon.WaitControlDisplayed(PRMPRPD_PayCycleScheduleLink);
PRMPRPD_PayCycleScheduleLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRPD] Perfoming VerifyExists on PayCycleScheduleForm...", Logger.MessageType.INF);
			Control PRMPRPD_PayCycleScheduleForm = new Control("PayCycleScheduleForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPRPD_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPRPD_PayCycleScheduleForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPRPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPRPD] Perfoming VerifyExist on PayCycleScheduleTable...", Logger.MessageType.INF);
			Control PRMPRPD_PayCycleScheduleTable = new Control("PayCycleScheduleTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPRPD_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPRPD_PayCycleScheduleTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPRPD";
							CPCommon.WaitControlDisplayed(PRMPRPD_PayCycleScheduleForm);
formBttn = PRMPRPD_PayCycleScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMPRPD";
							CPCommon.WaitControlDisplayed(PRMPRPD_MainForm);
formBttn = PRMPRPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

