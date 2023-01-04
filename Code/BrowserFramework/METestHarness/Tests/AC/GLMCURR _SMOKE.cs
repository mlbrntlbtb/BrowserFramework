 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMCURR__SMOKE : TestScript
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
new Control("Multicurrency", "xpath","//div[@class='deptItem'][.='Multicurrency']").Click();
new Control("Currency Controls", "xpath","//div[@class='navItem'][.='Currency Controls']").Click();
new Control("Manage Currencies", "xpath","//div[@class='navItem'][.='Manage Currencies']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMCURR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCURR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMCURR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMCURR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMCURR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCURR] Perfoming VerifyExists on CurrencyISOCode...", Logger.MessageType.INF);
			Control GLMCURR_CurrencyISOCode = new Control("CurrencyISOCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_CRNCY_CD']");
			CPCommon.AssertEqual(true,GLMCURR_CurrencyISOCode.Exists());

												
				CPCommon.CurrentComponent = "GLMCURR";
							CPCommon.WaitControlDisplayed(GLMCURR_MainForm);
IWebElement formBttn = GLMCURR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMCURR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMCURR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMCURR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCURR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMCURR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCURR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMCURR";
							CPCommon.WaitControlDisplayed(GLMCURR_MainForm);
formBttn = GLMCURR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

