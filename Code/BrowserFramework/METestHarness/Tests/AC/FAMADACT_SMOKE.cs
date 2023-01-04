 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMADACT_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Controls", "xpath","//div[@class='navItem'][.='Fixed Assets Controls']").Click();
new Control("Manage Accumulated Depreciation Account Codes", "xpath","//div[@class='navItem'][.='Manage Accumulated Depreciation Account Codes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMADACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMADACT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMADACT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMADACT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMADACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMADACT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMADACT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMADACT_MainForm);
IWebElement formBttn = FAMADACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMADACT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMADACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMADACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMADACT] Perfoming VerifyExists on AccumDeprAcctCode...", Logger.MessageType.INF);
			Control FAMADACT_AccumDeprAcctCode = new Control("AccumDeprAcctCode", "ID", "ACCUM_DEPR_ACCT_CD");
			CPCommon.AssertEqual(true,FAMADACT_AccumDeprAcctCode.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMADACT";
							CPCommon.WaitControlDisplayed(FAMADACT_MainForm);
formBttn = FAMADACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

