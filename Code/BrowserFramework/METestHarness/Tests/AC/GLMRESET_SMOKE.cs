 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMRESET_SMOKE : TestScript
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
new Control("Reorganizations", "xpath","//div[@class='navItem'][.='Reorganizations']").Click();
new Control("Manage Reorganization Structures", "xpath","//div[@class='navItem'][.='Manage Reorganization Structures']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLMRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRESET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMRESET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMRESET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRESET] Perfoming VerifyExists on Reorganization...", Logger.MessageType.INF);
			Control GLMRESET_Reorganization = new Control("Reorganization", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REORG_ID']");
			CPCommon.AssertEqual(true,GLMRESET_Reorganization.Exists());

												
				CPCommon.CurrentComponent = "GLMRESET";
							CPCommon.WaitControlDisplayed(GLMRESET_MainForm);
IWebElement formBttn = GLMRESET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMRESET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMRESET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRESET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMRESET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRESET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "GLMRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRESET] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLMRESET_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRESET_REORGLVL_REORG_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMRESET_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRESET] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLMRESET_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRESET_REORGLVL_REORG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRESET_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLMRESET";
							CPCommon.WaitControlDisplayed(GLMRESET_MainForm);
formBttn = GLMRESET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

