 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMUNBDP_SMOKE : TestScript
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
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Period Budgets", "xpath","//div[@class='navItem'][.='Period Budgets']").Click();
new Control("Manage Project Unit Budgets By Period", "xpath","//div[@class='navItem'][.='Manage Project Unit Budgets By Period']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMUNBDP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMUNBDP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMUNBDP_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMUNBDP_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMUNBDP";
							CPCommon.WaitControlDisplayed(PJMUNBDP_MainForm);
IWebElement formBttn = PJMUNBDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMUNBDP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMUNBDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMUNBDP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMUNBDP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Project Unit Budgets By Period Detail");


												
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming VerifyExists on ProjectUnitBudgetsByPeriodDetailLink...", Logger.MessageType.INF);
			Control PJMUNBDP_ProjectUnitBudgetsByPeriodDetailLink = new Control("ProjectUnitBudgetsByPeriodDetailLink", "ID", "lnk_1001808_PJMUNBDP_PROJBUDUNITS_HDR");
			CPCommon.AssertEqual(true,PJMUNBDP_ProjectUnitBudgetsByPeriodDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJMUNBDP";
							CPCommon.WaitControlDisplayed(PJMUNBDP_ProjectUnitBudgetsByPeriodDetailLink);
PJMUNBDP_ProjectUnitBudgetsByPeriodDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming VerifyExist on ProjectUnitBudgetsByPeriodChildFormTable...", Logger.MessageType.INF);
			Control PJMUNBDP_ProjectUnitBudgetsByPeriodChildFormTable = new Control("ProjectUnitBudgetsByPeriodChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMUNBDP_PROJBUDUNITS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMUNBDP_ProjectUnitBudgetsByPeriodChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming ClickButton on ProjectUnitBudgetsByPeriodChildForm...", Logger.MessageType.INF);
			Control PJMUNBDP_ProjectUnitBudgetsByPeriodChildForm = new Control("ProjectUnitBudgetsByPeriodChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMUNBDP_PROJBUDUNITS_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMUNBDP_ProjectUnitBudgetsByPeriodChildForm);
formBttn = PJMUNBDP_ProjectUnitBudgetsByPeriodChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMUNBDP_ProjectUnitBudgetsByPeriodChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMUNBDP_ProjectUnitBudgetsByPeriodChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMUNBDP";
							CPCommon.AssertEqual(true,PJMUNBDP_ProjectUnitBudgetsByPeriodChildForm.Exists());

													
				CPCommon.CurrentComponent = "PJMUNBDP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUNBDP] Perfoming VerifyExists on ProjectUnitBudgetsByPeriodChild_Period...", Logger.MessageType.INF);
			Control PJMUNBDP_ProjectUnitBudgetsByPeriodChild_Period = new Control("ProjectUnitBudgetsByPeriodChild_Period", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMUNBDP_PROJBUDUNITS_CHILD_']/ancestor::form[1]/descendant::*[@id='PD_NO']");
			CPCommon.AssertEqual(true,PJMUNBDP_ProjectUnitBudgetsByPeriodChild_Period.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMUNBDP";
							CPCommon.WaitControlDisplayed(PJMUNBDP_MainForm);
formBttn = PJMUNBDP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

