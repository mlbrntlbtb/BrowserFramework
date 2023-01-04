 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPPTER_SMOKE : TestScript
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
new Control("Multi-Company Processing", "xpath","//div[@class='navItem'][.='Multi-Company Processing']").Click();
new Control("Post Intercompany Receivables", "xpath","//div[@class='navItem'][.='Post Intercompany Receivables']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLPPTER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPPTER] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLPPTER_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLPPTER_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLPPTER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPPTER] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control GLPPTER_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,GLPPTER_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "GLPPTER";
							CPCommon.WaitControlDisplayed(GLPPTER_MainForm);
IWebElement formBttn = GLPPTER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLPPTER_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLPPTER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLPPTER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPPTER] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLPPTER_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPPTER_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLPPTER";
							CPCommon.WaitControlDisplayed(GLPPTER_MainForm);
formBttn = GLPPTER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

