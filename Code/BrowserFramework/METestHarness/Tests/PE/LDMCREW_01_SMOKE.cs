 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMCREW_01_SMOKE : TestScript
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
new Control("Union Information Controls", "xpath","//div[@class='navItem'][.='Union Information Controls']").Click();
new Control("Manage Assignment of Employees to Crews", "xpath","//div[@class='navItem'][.='Manage Assignment of Employees to Crews']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMCREW_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMCREW_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExists on CrewID...", Logger.MessageType.INF);
			Control LDMCREW_CrewID = new Control("CrewID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CREW_ID']");
			CPCommon.AssertEqual(true,LDMCREW_CrewID.Exists());

												
				CPCommon.CurrentComponent = "LDMCREW";
							CPCommon.WaitControlDisplayed(LDMCREW_MainForm);
IWebElement formBttn = LDMCREW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMCREW_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMCREW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMCREW_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCREW_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Employees");


												
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExists on EmployeesForm...", Logger.MessageType.INF);
			Control LDMCREW_EmployeesForm = new Control("EmployeesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCREW_EMPL_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMCREW_EmployeesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExist on EmployeesFormTable...", Logger.MessageType.INF);
			Control LDMCREW_EmployeesFormTable = new Control("EmployeesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCREW_EMPL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCREW_EmployeesFormTable.Exists());

											Driver.SessionLogger.WriteLine("Selected Employees");


												
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExists on SelectedEmployeesForm...", Logger.MessageType.INF);
			Control LDMCREW_SelectedEmployeesForm = new Control("SelectedEmployeesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCREW_CREWASSIGNSETUP_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMCREW_SelectedEmployeesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMCREW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMCREW] Perfoming VerifyExist on SelectedEmployeesFormTable...", Logger.MessageType.INF);
			Control LDMCREW_SelectedEmployeesFormTable = new Control("SelectedEmployeesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMCREW_CREWASSIGNSETUP_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMCREW_SelectedEmployeesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMCREW";
							CPCommon.WaitControlDisplayed(LDMCREW_MainForm);
formBttn = LDMCREW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

