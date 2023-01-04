 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMCSTOP_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Configure Manufacturing Order Costing Options", "xpath","//div[@class='navItem'][.='Configure Manufacturing Order Costing Options']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCMCSTOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCSTOP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMCSTOP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMCSTOP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCSTOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCSTOP] Perfoming VerifyExists on ManuallyCost...", Logger.MessageType.INF);
			Control PCMCSTOP_ManuallyCost = new Control("ManuallyCost", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_RELIEF_OPT_CD' and @value='M']");
			CPCommon.AssertEqual(true,PCMCSTOP_ManuallyCost.Exists());

												
				CPCommon.CurrentComponent = "PCMCSTOP";
							CPCommon.WaitControlDisplayed(PCMCSTOP_MainForm);
IWebElement formBttn = PCMCSTOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

