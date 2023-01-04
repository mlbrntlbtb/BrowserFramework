 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMGLCP_SMOKE : TestScript
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
new Control("Manage GLC Budgets By Period", "xpath","//div[@class='navItem'][.='Manage GLC Budgets By Period']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMGLCP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMGLCP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming VerifyExists on ProjectID...", Logger.MessageType.INF);
			Control PJMGLCP_ProjectID = new Control("ProjectID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMGLCP_ProjectID.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCP";
							CPCommon.WaitControlDisplayed(PJMGLCP_MainForm);
IWebElement formBttn = PJMGLCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMGLCP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMGLCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMGLCP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMGLCP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("BUDGETS BY PERIOD");


												
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming VerifyExists on BudgetsByPeriodLink...", Logger.MessageType.INF);
			Control PJMGLCP_BudgetsByPeriodLink = new Control("BudgetsByPeriodLink", "ID", "lnk_1001749_PJMGLCP_PROJBUDGLC_HDR");
			CPCommon.AssertEqual(true,PJMGLCP_BudgetsByPeriodLink.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCP";
							CPCommon.WaitControlDisplayed(PJMGLCP_BudgetsByPeriodLink);
PJMGLCP_BudgetsByPeriodLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming VerifyExist on BudgetsByPeriodFormTable...", Logger.MessageType.INF);
			Control PJMGLCP_BudgetsByPeriodFormTable = new Control("BudgetsByPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCP_PROJBUDGLC_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMGLCP_BudgetsByPeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming ClickButton on BudgetsByPeriodForm...", Logger.MessageType.INF);
			Control PJMGLCP_BudgetsByPeriodForm = new Control("BudgetsByPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCP_PROJBUDGLC_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMGLCP_BudgetsByPeriodForm);
formBttn = PJMGLCP_BudgetsByPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMGLCP_BudgetsByPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMGLCP_BudgetsByPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMGLCP";
							CPCommon.AssertEqual(true,PJMGLCP_BudgetsByPeriodForm.Exists());

													
				CPCommon.CurrentComponent = "PJMGLCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGLCP] Perfoming VerifyExists on BudgetsByPeriod_GLCDetails_GLC...", Logger.MessageType.INF);
			Control PJMGLCP_BudgetsByPeriod_GLCDetails_GLC = new Control("BudgetsByPeriod_GLCDetails_GLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMGLCP_PROJBUDGLC_CHLD_']/ancestor::form[1]/descendant::*[@id='GENL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMGLCP_BudgetsByPeriod_GLCDetails_GLC.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMGLCP";
							CPCommon.WaitControlDisplayed(PJMGLCP_MainForm);
formBttn = PJMGLCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

