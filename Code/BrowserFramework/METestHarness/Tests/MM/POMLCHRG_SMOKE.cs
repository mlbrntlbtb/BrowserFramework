 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMLCHRG_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchasing Codes", "xpath","//div[@class='navItem'][.='Purchasing Codes']").Click();
new Control("Manage Line Charge Types", "xpath","//div[@class='navItem'][.='Manage Line Charge Types']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "POMLCHRG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMLCHRG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMLCHRG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMLCHRG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMLCHRG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMLCHRG] Perfoming VerifyExists on ChargeTypeCode...", Logger.MessageType.INF);
			Control POMLCHRG_ChargeTypeCode = new Control("ChargeTypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LN_CHG_TYPE']");
			CPCommon.AssertEqual(true,POMLCHRG_ChargeTypeCode.Exists());

												
				CPCommon.CurrentComponent = "POMLCHRG";
							CPCommon.WaitControlDisplayed(POMLCHRG_MainForm);
IWebElement formBttn = POMLCHRG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMLCHRG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMLCHRG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POMLCHRG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMLCHRG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMLCHRG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMLCHRG_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "POMLCHRG";
							CPCommon.WaitControlDisplayed(POMLCHRG_MainForm);
formBttn = POMLCHRG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

