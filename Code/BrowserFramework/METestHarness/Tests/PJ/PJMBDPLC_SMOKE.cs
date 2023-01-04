 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBDPLC_SMOKE : TestScript
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
new Control("Manage PLC Total Budget", "xpath","//div[@class='navItem'][.='Manage PLC Total Budget']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBDPLC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBDPLC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMBDPLC_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMBDPLC_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPLC";
							CPCommon.WaitControlDisplayed(PJMBDPLC_MainForm);
IWebElement formBttn = PJMBDPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMBDPLC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMBDPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMBDPLC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDPLC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("GLC BUDGET DETAILS");


												
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming VerifyExists on PLCBudgetDetailsLink...", Logger.MessageType.INF);
			Control PJMBDPLC_PLCBudgetDetailsLink = new Control("PLCBudgetDetailsLink", "ID", "lnk_3796_PJMBDPLC_PROJTOTBUDPLC_HDR");
			CPCommon.AssertEqual(true,PJMBDPLC_PLCBudgetDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming VerifyExist on PLCBudgetDetailsFormTable...", Logger.MessageType.INF);
			Control PJMBDPLC_PLCBudgetDetailsFormTable = new Control("PLCBudgetDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPLC_PROJTOTBUDPLC_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDPLC_PLCBudgetDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming ClickButton on PLCBudgetDetailsForm...", Logger.MessageType.INF);
			Control PJMBDPLC_PLCBudgetDetailsForm = new Control("PLCBudgetDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPLC_PROJTOTBUDPLC_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBDPLC_PLCBudgetDetailsForm);
formBttn = PJMBDPLC_PLCBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBDPLC_PLCBudgetDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBDPLC_PLCBudgetDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMBDPLC";
							CPCommon.AssertEqual(true,PJMBDPLC_PLCBudgetDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMBDPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPLC] Perfoming VerifyExists on PLCBudgetDetails_PLC...", Logger.MessageType.INF);
			Control PJMBDPLC_PLCBudgetDetails_PLC = new Control("PLCBudgetDetails_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPLC_PROJTOTBUDPLC_CHILD_']/ancestor::form[1]/descendant::*[@id='BILL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMBDPLC_PLCBudgetDetails_PLC.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMBDPLC";
							CPCommon.WaitControlDisplayed(PJMBDPLC_MainForm);
formBttn = PJMBDPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? PJMBDPLC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
PJMBDPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "PJMBDPLC";
							CPCommon.WaitControlDisplayed(PJMBDPLC_MainForm);
formBttn = PJMBDPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

