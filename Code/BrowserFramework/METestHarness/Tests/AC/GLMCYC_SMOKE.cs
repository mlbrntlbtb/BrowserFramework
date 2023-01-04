 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMCYC_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Journal Entry Processing", "xpath","//div[@class='navItem'][.='Journal Entry Processing']").Click();
new Control("Configure Journal Entry Cycles", "xpath","//div[@class='navItem'][.='Configure Journal Entry Cycles']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMCYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCYC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMCYC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMCYC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMCYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCYC] Perfoming VerifyExists on Cycle...", Logger.MessageType.INF);
			Control GLMCYC_Cycle = new Control("Cycle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CYCLE_DC']");
			CPCommon.AssertEqual(true,GLMCYC_Cycle.Exists());

												
				CPCommon.CurrentComponent = "GLMCYC";
							CPCommon.WaitControlDisplayed(GLMCYC_MainForm);
IWebElement formBttn = GLMCYC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMCYC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMCYC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMCYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCYC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMCYC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCYC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD");


												
				CPCommon.CurrentComponent = "GLMCYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCYC] Perfoming VerifyExists on PeriodsForm...", Logger.MessageType.INF);
			Control GLMCYC_PeriodsForm = new Control("PeriodsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMCYC_CYCLERULE_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMCYC_PeriodsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMCYC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCYC] Perfoming VerifyExist on PeriodsFormTable...", Logger.MessageType.INF);
			Control GLMCYC_PeriodsFormTable = new Control("PeriodsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMCYC_CYCLERULE_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCYC_PeriodsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMCYC";
							CPCommon.WaitControlDisplayed(GLMCYC_MainForm);
formBttn = GLMCYC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

