 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RCMSET_SMOKE : TestScript
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
new Control("Receiving", "xpath","//div[@class='deptItem'][.='Receiving']").Click();
new Control("Receiving Controls", "xpath","//div[@class='navItem'][.='Receiving Controls']").Click();
new Control("Configure Receiving Settings", "xpath","//div[@class='navItem'][.='Configure Receiving Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "RCMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RCMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RCMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RCMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMSET] Perfoming VerifyExists on HardEditOnQCRequiredPOLines...", Logger.MessageType.INF);
			Control RCMSET_HardEditOnQCRequiredPOLines = new Control("HardEditOnQCRequiredPOLines", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='QC_REQD_HRD_EDT_FL']");
			CPCommon.AssertEqual(true,RCMSET_HardEditOnQCRequiredPOLines.Exists());

												
				CPCommon.CurrentComponent = "RCMSET";
							CPCommon.WaitControlDisplayed(RCMSET_MainForm);
IWebElement formBttn = RCMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

