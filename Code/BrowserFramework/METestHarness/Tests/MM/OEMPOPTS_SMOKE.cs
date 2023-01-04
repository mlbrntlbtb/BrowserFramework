 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMPOPTS_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Configure Sales Order Print Options", "xpath","//div[@class='navItem'][.='Configure Sales Order Print Options']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEMPOPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMPOPTS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMPOPTS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMPOPTS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMPOPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMPOPTS] Perfoming VerifyExists on PageXOfY...", Logger.MessageType.INF);
			Control OEMPOPTS_PageXOfY = new Control("PageXOfY", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='X_OF_Y_FL']");
			CPCommon.AssertEqual(true,OEMPOPTS_PageXOfY.Exists());

												
				CPCommon.CurrentComponent = "OEMPOPTS";
							CPCommon.WaitControlDisplayed(OEMPOPTS_MainForm);
IWebElement formBttn = OEMPOPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

