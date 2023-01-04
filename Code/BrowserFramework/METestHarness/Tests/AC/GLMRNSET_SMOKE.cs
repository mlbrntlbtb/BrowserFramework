 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMRNSET_SMOKE : TestScript
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
new Control("Reference Numbers", "xpath","//div[@class='navItem'][.='Reference Numbers']").Click();
new Control("Manage Reference Structures", "xpath","//div[@class='navItem'][.='Manage Reference Structures']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMRNSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMRNSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMRNSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRNSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNSET] Perfoming VerifyExists on ReferenceNo...", Logger.MessageType.INF);
			Control GLMRNSET_ReferenceNo = new Control("ReferenceNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REF_STRUC_ID']");
			CPCommon.AssertEqual(true,GLMRNSET_ReferenceNo.Exists());

												
				CPCommon.CurrentComponent = "GLMRNSET";
							CPCommon.WaitControlDisplayed(GLMRNSET_MainForm);
IWebElement formBttn = GLMRNSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMRNSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMRNSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMRNSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMRNSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRNSET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child");


												
				CPCommon.CurrentComponent = "GLMRNSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNSET] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLMRNSET_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRNSET_TOPREFSTRUCLVLS_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMRNSET_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRNSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRNSET] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLMRNSET_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRNSET_TOPREFSTRUCLVLS_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRNSET_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLMRNSET";
							CPCommon.WaitControlDisplayed(GLMRNSET_MainForm);
formBttn = GLMRNSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

