 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPYUNT_SMOKE : TestScript
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
new Control("Project History", "xpath","//div[@class='navItem'][.='Project History']").Click();
new Control("Manage Prior Year Unit Revenue", "xpath","//div[@class='navItem'][.='Manage Prior Year Unit Revenue']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMPYUNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYUNT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPYUNT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPYUNT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPYUNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYUNT] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJMPYUNT_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJMPYUNT_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJMPYUNT";
							CPCommon.WaitControlDisplayed(PJMPYUNT_MainForm);
IWebElement formBttn = PJMPYUNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPYUNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPYUNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPYUNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYUNT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPYUNT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYUNT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Prior Year Units");


												
				CPCommon.CurrentComponent = "PJMPYUNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYUNT] Perfoming VerifyExist on PriorYearUnitsFormTable...", Logger.MessageType.INF);
			Control PJMPYUNT_PriorYearUnitsFormTable = new Control("PriorYearUnitsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYUNT_PYUNITSPRICING_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYUNT_PriorYearUnitsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPYUNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYUNT] Perfoming ClickButton on PriorYearUnitsForm...", Logger.MessageType.INF);
			Control PJMPYUNT_PriorYearUnitsForm = new Control("PriorYearUnitsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYUNT_PYUNITSPRICING_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPYUNT_PriorYearUnitsForm);
formBttn = PJMPYUNT_PriorYearUnitsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPYUNT_PriorYearUnitsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPYUNT_PriorYearUnitsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPYUNT";
							CPCommon.AssertEqual(true,PJMPYUNT_PriorYearUnitsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPYUNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYUNT] Perfoming VerifyExists on PriorYearUnits_CLIN...", Logger.MessageType.INF);
			Control PJMPYUNT_PriorYearUnits_CLIN = new Control("PriorYearUnits_CLIN", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYUNT_PYUNITSPRICING_CHLD_']/ancestor::form[1]/descendant::*[@id='CLIN_ID']");
			CPCommon.AssertEqual(true,PJMPYUNT_PriorYearUnits_CLIN.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMPYUNT";
							CPCommon.WaitControlDisplayed(PJMPYUNT_MainForm);
formBttn = PJMPYUNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

