 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMLEAVE_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Leave", "xpath","//div[@class='deptItem'][.='Leave']").Click();
new Control("Leave Controls", "xpath","//div[@class='navItem'][.='Leave Controls']").Click();
new Control("Configure Leave Settings", "xpath","//div[@class='navItem'][.='Configure Leave Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMLEAVE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEAVE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMLEAVE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMLEAVE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLEAVE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEAVE] Perfoming VerifyExists on DefaultLeaveCycle_DefaultLeaveCycle...", Logger.MessageType.INF);
			Control LDMLEAVE_DefaultLeaveCycle_DefaultLeaveCycle = new Control("DefaultLeaveCycle_DefaultLeaveCycle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_LV_PD_CD']");
			CPCommon.AssertEqual(true,LDMLEAVE_DefaultLeaveCycle_DefaultLeaveCycle.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMLEAVE";
							CPCommon.WaitControlDisplayed(LDMLEAVE_MainForm);
IWebElement formBttn = LDMLEAVE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

