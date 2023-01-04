 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLPOBDFC_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Administrative Utilities", "xpath","//div[@class='navItem'][.='Administrative Utilities']").Click();
new Control("Validate Open Billing Detail Account Function Codes", "xpath","//div[@class='navItem'][.='Validate Open Billing Detail Account Function Codes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLPOBDFC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPOBDFC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLPOBDFC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLPOBDFC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLPOBDFC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPOBDFC] Perfoming VerifyExists on Execute...", Logger.MessageType.INF);
			Control BLPOBDFC_Execute = new Control("Execute", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'EXECUTE') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,BLPOBDFC_Execute.Exists());

												
				CPCommon.CurrentComponent = "BLPOBDFC";
							CPCommon.WaitControlDisplayed(BLPOBDFC_MainForm);
IWebElement formBttn = BLPOBDFC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

