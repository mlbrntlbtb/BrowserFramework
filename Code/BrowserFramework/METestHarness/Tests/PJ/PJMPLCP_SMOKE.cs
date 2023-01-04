 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPLCP_SMOKE : TestScript
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
new Control("Manage PLC Budgets By Period", "xpath","//div[@class='navItem'][.='Manage PLC Budgets By Period']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPLCP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPLCP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMPLCP_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPLCP_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCP";
							CPCommon.WaitControlDisplayed(PJMPLCP_MainForm);
IWebElement formBttn = PJMPLCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPLCP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPLCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPLCP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("BUDGETS BY PERIOD");


												
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming VerifyExists on BudgetsByPeriodLink...", Logger.MessageType.INF);
			Control PJMPLCP_BudgetsByPeriodLink = new Control("BudgetsByPeriodLink", "ID", "lnk_1000887_PJMPLCP_PROJBUDPLC_HDR");
			CPCommon.AssertEqual(true,PJMPLCP_BudgetsByPeriodLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCP";
							CPCommon.WaitControlDisplayed(PJMPLCP_BudgetsByPeriodLink);
PJMPLCP_BudgetsByPeriodLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming VerifyExist on BudgetsByPeriodFormTable...", Logger.MessageType.INF);
			Control PJMPLCP_BudgetsByPeriodFormTable = new Control("BudgetsByPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCP_PROJBUDPLC_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCP_BudgetsByPeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming ClickButton on BudgetsByPeriodForm...", Logger.MessageType.INF);
			Control PJMPLCP_BudgetsByPeriodForm = new Control("BudgetsByPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCP_PROJBUDPLC_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPLCP_BudgetsByPeriodForm);
formBttn = PJMPLCP_BudgetsByPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPLCP_BudgetsByPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPLCP_BudgetsByPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCP] Perfoming VerifyExists on BudgetsByPeriod_PLCDetails_PLC...", Logger.MessageType.INF);
			Control PJMPLCP_BudgetsByPeriod_PLCDetails_PLC = new Control("BudgetsByPeriod_PLCDetails_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPLCP_PROJBUDPLC_CHILD_']/ancestor::form[1]/descendant::*[@id='BILL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMPLCP_BudgetsByPeriod_PLCDetails_PLC.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMPLCP";
							CPCommon.WaitControlDisplayed(PJMPLCP_MainForm);
formBttn = PJMPLCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

