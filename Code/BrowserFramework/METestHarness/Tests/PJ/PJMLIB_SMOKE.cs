 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMLIB_SMOKE : TestScript
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
new Control("Encumbrance Tracking", "xpath","//div[@class='navItem'][.='Encumbrance Tracking']").Click();
new Control("Manage Project Line Item Budgets", "xpath","//div[@class='navItem'][.='Manage Project Line Item Budgets']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMLIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMLIB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMLIB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMLIB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMLIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMLIB] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMLIB_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMLIB_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMLIB";
							CPCommon.WaitControlDisplayed(PJMLIB_MainForm);
IWebElement formBttn = PJMLIB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMLIB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMLIB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMLIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMLIB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMLIB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMLIB_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("LINE ITEM BUDGET AMOUNT");


												
				CPCommon.CurrentComponent = "PJMLIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMLIB] Perfoming VerifyExists on LineItemBudgetAmountsLink...", Logger.MessageType.INF);
			Control PJMLIB_LineItemBudgetAmountsLink = new Control("LineItemBudgetAmountsLink", "ID", "lnk_3919_PJMLIB_PROJLNITEMBUD_HDR");
			CPCommon.AssertEqual(true,PJMLIB_LineItemBudgetAmountsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMLIB";
							CPCommon.WaitControlDisplayed(PJMLIB_LineItemBudgetAmountsLink);
PJMLIB_LineItemBudgetAmountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMLIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMLIB] Perfoming VerifyExist on LineItemBudgetAmountsFormTable...", Logger.MessageType.INF);
			Control PJMLIB_LineItemBudgetAmountsFormTable = new Control("LineItemBudgetAmountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMLIB_PROJLNITEMBUD_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMLIB_LineItemBudgetAmountsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMLIB";
							CPCommon.WaitControlDisplayed(PJMLIB_MainForm);
formBttn = PJMLIB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

