 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMWORK_SMOKE : TestScript
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
new Control("Manage Work Schedules", "xpath","//div[@class='navItem'][.='Manage Work Schedules']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "LDMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMWORK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMWORK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMWORK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMWORK] Perfoming VerifyExists on WorkScheduleCode...", Logger.MessageType.INF);
			Control LDMWORK_WorkScheduleCode = new Control("WorkScheduleCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TC_WORK_SCHED_CD']");
			CPCommon.AssertEqual(true,LDMWORK_WorkScheduleCode.Exists());

												
				CPCommon.CurrentComponent = "LDMWORK";
							CPCommon.WaitControlDisplayed(LDMWORK_MainForm);
IWebElement formBttn = LDMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMWORK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMWORK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMWORK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMWORK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMWORK";
							CPCommon.WaitControlDisplayed(LDMWORK_MainForm);
formBttn = LDMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMWORK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "LDMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMWORK] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control LDMWORK_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMWORK_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMWORK_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "LDMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMWORK] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control LDMWORK_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMWORK_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMWORK_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close Main Form");


												
				CPCommon.CurrentComponent = "LDMWORK";
							CPCommon.WaitControlDisplayed(LDMWORK_MainForm);
formBttn = LDMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

