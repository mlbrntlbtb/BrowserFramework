 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMCSSRB_SMOKE : TestScript
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
new Control("Cost Schedule Status Report", "xpath","//div[@class='navItem'][.='Cost Schedule Status Report']").Click();
new Control("Manage CSSR Budgets", "xpath","//div[@class='navItem'][.='Manage CSSR Budgets']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("C/SSR Budgets", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMCSSRB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCSSRB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMCSSRB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMCSSRB_MainForm);
IWebElement formBttn = PJMCSSRB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCSSRB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCSSRB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMCSSRB";
							CPCommon.AssertEqual(true,PJMCSSRB_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMCSSRB_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMCSSRB_Project.Exists());

											Driver.SessionLogger.WriteLine("C/SSR BUDGET DETAILS");


												
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming VerifyExists on CSSRBudgetDetailsLink...", Logger.MessageType.INF);
			Control PJMCSSRB_CSSRBudgetDetailsLink = new Control("CSSRBudgetDetailsLink", "ID", "lnk_3817_PJMCSSRB_PROJCSSRINFO_HDR");
			CPCommon.AssertEqual(true,PJMCSSRB_CSSRBudgetDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming VerifyExist on CSSRBudgetDetailsFormTable...", Logger.MessageType.INF);
			Control PJMCSSRB_CSSRBudgetDetailsFormTable = new Control("CSSRBudgetDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSSRB_PROJCSSRBUD_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMCSSRB_CSSRBudgetDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming ClickButton on CSSRBudgetDetailsForm...", Logger.MessageType.INF);
			Control PJMCSSRB_CSSRBudgetDetailsForm = new Control("CSSRBudgetDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSSRB_PROJCSSRBUD_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMCSSRB_CSSRBudgetDetailsForm);
formBttn = PJMCSSRB_CSSRBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMCSSRB_CSSRBudgetDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMCSSRB_CSSRBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMCSSRB";
							CPCommon.AssertEqual(true,PJMCSSRB_CSSRBudgetDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMCSSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMCSSRB] Perfoming VerifyExists on CSSRBudgetDetails_WBSProject...", Logger.MessageType.INF);
			Control PJMCSSRB_CSSRBudgetDetails_WBSProject = new Control("CSSRBudgetDetails_WBSProject", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMCSSRB_PROJCSSRBUD_TBL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMCSSRB_CSSRBudgetDetails_WBSProject.Exists());

												
				CPCommon.CurrentComponent = "PJMCSSRB";
							CPCommon.WaitControlDisplayed(PJMCSSRB_CSSRBudgetDetailsForm);
formBttn = PJMCSSRB_CSSRBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMCSSRB";
							CPCommon.WaitControlDisplayed(PJMCSSRB_MainForm);
formBttn = PJMCSSRB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

