 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBDGLC_SMOKE : TestScript
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
new Control("Total Budgets", "xpath","//div[@class='navItem'][.='Total Budgets']").Click();
new Control("Manage GLC Total Budget", "xpath","//div[@class='navItem'][.='Manage GLC Total Budget']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBDGLC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBDGLC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMBDGLC_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMBDGLC_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMBDGLC";
							CPCommon.WaitControlDisplayed(PJMBDGLC_MainForm);
IWebElement formBttn = PJMBDGLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMBDGLC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMBDGLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMBDGLC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDGLC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("GLC BUDGET DETAILS");


												
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming VerifyExists on GLCBudgetDetailsLink...", Logger.MessageType.INF);
			Control PJMBDGLC_GLCBudgetDetailsLink = new Control("GLCBudgetDetailsLink", "ID", "lnk_3790_PJMBDGLC_PROJTOTBUDGLC_HDR");
			CPCommon.AssertEqual(true,PJMBDGLC_GLCBudgetDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming VerifyExist on GLCBudgetDetailsFormTable...", Logger.MessageType.INF);
			Control PJMBDGLC_GLCBudgetDetailsFormTable = new Control("GLCBudgetDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDGLC_PROJTOTBUDGLC_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDGLC_GLCBudgetDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming ClickButton on GLCBudgetDetailsForm...", Logger.MessageType.INF);
			Control PJMBDGLC_GLCBudgetDetailsForm = new Control("GLCBudgetDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDGLC_PROJTOTBUDGLC_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBDGLC_GLCBudgetDetailsForm);
formBttn = PJMBDGLC_GLCBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBDGLC_GLCBudgetDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBDGLC_GLCBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMBDGLC";
							CPCommon.AssertEqual(true,PJMBDGLC_GLCBudgetDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMBDGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDGLC] Perfoming VerifyExists on GLCBudgetDetails_GLC...", Logger.MessageType.INF);
			Control PJMBDGLC_GLCBudgetDetails_GLC = new Control("GLCBudgetDetails_GLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDGLC_PROJTOTBUDGLC_CHILD_']/ancestor::form[1]/descendant::*[@id='GENL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMBDGLC_GLCBudgetDetails_GLC.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMBDGLC";
							CPCommon.WaitControlDisplayed(PJMBDGLC_MainForm);
formBttn = PJMBDGLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

