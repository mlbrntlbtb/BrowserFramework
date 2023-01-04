 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMWORK_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Labor", "xpath","//div[@class='navItem'][.='Project Labor']").Click();
new Control("Manage Employee Work Force", "xpath","//div[@class='navItem'][.='Manage Employee Work Force']").Click();


											Driver.SessionLogger.WriteLine("Query an existing record");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMWORK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMWORK_MainForm);
IWebElement formBttn = PJMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJMWORK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMWORK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMWORK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
							CPCommon.WaitControlDisplayed(PJMWORK_MainForm);
formBttn = PJMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMWORK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJMWORK";
							CPCommon.AssertEqual(true,PJMWORK_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control PJMWORK_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMWORK_Identification_Project.Exists());

											Driver.SessionLogger.WriteLine("Employees");


											Driver.SessionLogger.WriteLine("SelectedEmployees");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on SelectedEmployeesForm...", Logger.MessageType.INF);
			Control PJMWORK_SelectedEmployeesForm = new Control("SelectedEmployeesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPL_CHILDTO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMWORK_SelectedEmployeesForm.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExist on SelectedEmployees_Table...", Logger.MessageType.INF);
			Control PJMWORK_SelectedEmployees_Table = new Control("SelectedEmployees_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPL_CHILDTO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMWORK_SelectedEmployees_Table.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on SelectedEmployees_AssignPLCToEmployeeWorkForceLink...", Logger.MessageType.INF);
			Control PJMWORK_SelectedEmployees_AssignPLCToEmployeeWorkForceLink = new Control("SelectedEmployees_AssignPLCToEmployeeWorkForceLink", "ID", "lnk_17452_PJM_PROJEMPL_HDR");
			CPCommon.AssertEqual(true,PJMWORK_SelectedEmployees_AssignPLCToEmployeeWorkForceLink.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
							CPCommon.WaitControlDisplayed(PJMWORK_SelectedEmployees_AssignPLCToEmployeeWorkForceLink);
PJMWORK_SelectedEmployees_AssignPLCToEmployeeWorkForceLink.Click(1.5);


												Driver.SessionLogger.WriteLine("AssignPLCToEmployeeWorkForce_PLCs");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on AssignPLCToEmployeeWorkForce_PLCsForm...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCToEmployeeWorkForce_PLCsForm = new Control("AssignPLCToEmployeeWorkForce_PLCsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_PLC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCToEmployeeWorkForce_PLCsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExist on AssignPLCToEmployeeWorkForce_PLCsFormTable...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCToEmployeeWorkForce_PLCsFormTable = new Control("AssignPLCToEmployeeWorkForce_PLCsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_PLC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCToEmployeeWorkForce_PLCsFormTable.Exists());

											Driver.SessionLogger.WriteLine("AssignPLCToEmployeeWorkForce_SelectedEmployees");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on AssignPLCToEmployeeWorkForce_SelectedEmployeesForm...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCToEmployeeWorkForce_SelectedEmployeesForm = new Control("AssignPLCToEmployeeWorkForce_SelectedEmployeesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_EMPL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCToEmployeeWorkForce_SelectedEmployeesForm.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExist on AssignPLCToEmployeeWorkForce_SelectedEmployeesFormTable...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCToEmployeeWorkForce_SelectedEmployeesFormTable = new Control("AssignPLCToEmployeeWorkForce_SelectedEmployeesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_EMPL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCToEmployeeWorkForce_SelectedEmployeesFormTable.Exists());

											Driver.SessionLogger.WriteLine("AssignPLCToEmployeeWorkForce");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on AssignPLCToEmployeeWorkForceForm...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCToEmployeeWorkForceForm = new Control("AssignPLCToEmployeeWorkForceForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPL_LABCAT_PLCWKFRCE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCToEmployeeWorkForceForm.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExist on AssignPLCtoEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceTable...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCtoEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceTable = new Control("AssignPLCtoEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_PLCWK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCtoEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceTable.Exists());

												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming ClickButton on AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm = new Control("AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_PLCWK_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMWORK_AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm);
formBttn = PJMWORK_AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMWORK_AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMWORK_AssignPLCToEmployeeWorkForce_PLCsAssignedToEmployeeWorkForceForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMWORK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMWORK] Perfoming VerifyExists on AssignPLCtoEmployeeWorkForce_AssignPLCToEmployeeWorkForce_Employee...", Logger.MessageType.INF);
			Control PJMWORK_AssignPLCtoEmployeeWorkForce_AssignPLCToEmployeeWorkForce_Employee = new Control("AssignPLCtoEmployeeWorkForce_AssignPLCToEmployeeWorkForce_Employee", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJEMPLLABCAT_PLCWK_']/ancestor::form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PJMWORK_AssignPLCtoEmployeeWorkForce_AssignPLCToEmployeeWorkForce_Employee.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMWORK";
							CPCommon.WaitControlDisplayed(PJMWORK_MainForm);
formBttn = PJMWORK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

