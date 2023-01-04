 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMALTFY_SMOKE : TestScript
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
new Control("Company Calendar", "xpath","//div[@class='navItem'][.='Company Calendar']").Click();
new Control("Manage Alternate Fiscal Year Mapping", "xpath","//div[@class='navItem'][.='Manage Alternate Fiscal Year Mapping']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMALTFY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMALTFY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExists on AlternateFiscalYear...", Logger.MessageType.INF);
			Control GLMALTFY_AlternateFiscalYear = new Control("AlternateFiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALT_FY_CD']");
			CPCommon.AssertEqual(true,GLMALTFY_AlternateFiscalYear.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
							CPCommon.WaitControlDisplayed(GLMALTFY_MainForm);
IWebElement formBttn = GLMALTFY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMALTFY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMALTFY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMALTFY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMALTFY_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLMALTFY_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMALTFY_FYPDSUBPD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMALTFY_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLMALTFY_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMALTFY_FYPDSUBPD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMALTFY_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExists on AlternateFYBeginningBalancesLink...", Logger.MessageType.INF);
			Control GLMALTFY_AlternateFYBeginningBalancesLink = new Control("AlternateFYBeginningBalancesLink", "ID", "lnk_16852_GLMALTFY_FY");
			CPCommon.AssertEqual(true,GLMALTFY_AlternateFYBeginningBalancesLink.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
							CPCommon.WaitControlDisplayed(GLMALTFY_AlternateFYBeginningBalancesLink);
GLMALTFY_AlternateFYBeginningBalancesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExists on AlternateFYBeginningBalancesForm...", Logger.MessageType.INF);
			Control GLMALTFY_AlternateFYBeginningBalancesForm = new Control("AlternateFYBeginningBalancesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMALTFY_BEGBAL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMALTFY_AlternateFYBeginningBalancesForm.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMALTFY] Perfoming VerifyExist on AlternateFYBeginningBalancesFormTable...", Logger.MessageType.INF);
			Control GLMALTFY_AlternateFYBeginningBalancesFormTable = new Control("AlternateFYBeginningBalancesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMALTFY_BEGBAL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMALTFY_AlternateFYBeginningBalancesFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMALTFY";
							CPCommon.WaitControlDisplayed(GLMALTFY_AlternateFYBeginningBalancesForm);
formBttn = GLMALTFY_AlternateFYBeginningBalancesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLMALTFY";
							CPCommon.WaitControlDisplayed(GLMALTFY_MainForm);
formBttn = GLMALTFY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

