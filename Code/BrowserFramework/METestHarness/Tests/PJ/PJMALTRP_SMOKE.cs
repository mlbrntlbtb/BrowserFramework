 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMALTRP_SMOKE : TestScript
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
new Control("Alternate Projects", "xpath","//div[@class='navItem'][.='Alternate Projects']").Click();
new Control("Manage Alternate Projects", "xpath","//div[@class='navItem'][.='Manage Alternate Projects']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMALTRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMALTRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExists on ProjectReportName...", Logger.MessageType.INF);
			Control PJMALTRP_ProjectReportName = new Control("ProjectReportName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_RPT_ID']");
			CPCommon.AssertEqual(true,PJMALTRP_ProjectReportName.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
							CPCommon.WaitControlDisplayed(PJMALTRP_MainForm);
IWebElement formBttn = PJMALTRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMALTRP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMALTRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMALTRP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMALTRP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PJMALTRP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_TOPLVLRPT_TBLVIEW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMALTRP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PJMALTRP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_TOPLVLRPT_TBLVIEW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMALTRP_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("SELECTED PROJECTS");


												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExists on ProjectsForm...", Logger.MessageType.INF);
			Control PJMALTRP_ProjectsForm = new Control("ProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_PROJEDIT_PROJ_FROMCTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMALTRP_ProjectsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExist on ProjectsFormTable...", Logger.MessageType.INF);
			Control PJMALTRP_ProjectsFormTable = new Control("ProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_PROJEDIT_PROJ_FROMCTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMALTRP_ProjectsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExists on Projects_Select...", Logger.MessageType.INF);
			Control PJMALTRP_Projects_Select = new Control("Projects_Select", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_PROJEDIT_PROJ_FROMCTW_']/ancestor::form[1]/descendant::*[contains(@id,'BUTTON__SELECT') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,PJMALTRP_Projects_Select.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExists on SelectedProjectsForm...", Logger.MessageType.INF);
			Control PJMALTRP_SelectedProjectsForm = new Control("SelectedProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_PROJRPTPROJ_TOCTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMALTRP_SelectedProjectsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMALTRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMALTRP] Perfoming VerifyExist on SelectedProjectsFormTable...", Logger.MessageType.INF);
			Control PJMALTRP_SelectedProjectsFormTable = new Control("SelectedProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMALTRP_PROJRPTPROJ_TOCTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMALTRP_SelectedProjectsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMALTRP";
							CPCommon.WaitControlDisplayed(PJMALTRP_MainForm);
formBttn = PJMALTRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

