 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMSUSJE_SMOKE : TestScript
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
new Control("General Ledger Interfaces", "xpath","//div[@class='navItem'][.='General Ledger Interfaces']").Click();
new Control("Manage Journal Entry Preprocessor Settings", "xpath","//div[@class='navItem'][.='Manage Journal Entry Preprocessor Settings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "AOMSUSJE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUSJE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMSUSJE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMSUSJE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMSUSJE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUSJE] Perfoming VerifyExists on IfAccountFailsReplace...", Logger.MessageType.INF);
			Control AOMSUSJE_IfAccountFailsReplace = new Control("IfAccountFailsReplace", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_PROC_ACCT_CD']");
			CPCommon.AssertEqual(true,AOMSUSJE_IfAccountFailsReplace.Exists());

												
				CPCommon.CurrentComponent = "AOMSUSJE";
							CPCommon.WaitControlDisplayed(AOMSUSJE_MainForm);
IWebElement formBttn = AOMSUSJE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMSUSJE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMSUSJE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMSUSJE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSUSJE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMSUSJE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMSUSJE_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMSUSJE";
							CPCommon.WaitControlDisplayed(AOMSUSJE_MainForm);
formBttn = AOMSUSJE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

