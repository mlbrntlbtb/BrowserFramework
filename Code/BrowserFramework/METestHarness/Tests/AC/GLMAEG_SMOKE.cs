 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMAEG_SMOKE : TestScript
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
new Control("Accounts", "xpath","//div[@class='navItem'][.='Accounts']").Click();
new Control("Configure Account Entry Groups", "xpath","//div[@class='navItem'][.='Configure Account Entry Groups']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "GLMAEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMAEG] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control GLMAEG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMAEG_MainForm);
IWebElement formBttn = GLMAEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMAEG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMAEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMAEG";
							CPCommon.AssertEqual(true,GLMAEG_MainForm.Exists());

													
				CPCommon.CurrentComponent = "GLMAEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMAEG] Perfoming VerifyExists on AccountEntryGroup...", Logger.MessageType.INF);
			Control GLMAEG_AccountEntryGroup = new Control("AccountEntryGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ENTR_GRP_CD']");
			CPCommon.AssertEqual(true,GLMAEG_AccountEntryGroup.Exists());

												
				CPCommon.CurrentComponent = "GLMAEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMAEG] Perfoming VerifyExist on AvailableEntryScreensFormTable...", Logger.MessageType.INF);
			Control GLMAEG_AvailableEntryScreensFormTable = new Control("AvailableEntryScreensFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMAEG_SACCTENTRYSCR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMAEG_AvailableEntryScreensFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMAEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMAEG] Perfoming VerifyExists on AvailableEntryScreensForm...", Logger.MessageType.INF);
			Control GLMAEG_AvailableEntryScreensForm = new Control("AvailableEntryScreensForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMAEG_SACCTENTRYSCR_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMAEG_AvailableEntryScreensForm.Exists());

												
				CPCommon.CurrentComponent = "GLMAEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMAEG] Perfoming VerifyExist on ScreensValidForThisEntryGroupFormTable...", Logger.MessageType.INF);
			Control GLMAEG_ScreensValidForThisEntryGroupFormTable = new Control("ScreensValidForThisEntryGroupFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMAEG_ACCTENTRYRULES_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMAEG_ScreensValidForThisEntryGroupFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMAEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMAEG] Perfoming VerifyExists on ScreensValidForThisEntryGroupForm...", Logger.MessageType.INF);
			Control GLMAEG_ScreensValidForThisEntryGroupForm = new Control("ScreensValidForThisEntryGroupForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMAEG_ACCTENTRYRULES_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMAEG_ScreensValidForThisEntryGroupForm.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLMAEG";
							CPCommon.WaitControlDisplayed(GLMAEG_MainForm);
formBttn = GLMAEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

