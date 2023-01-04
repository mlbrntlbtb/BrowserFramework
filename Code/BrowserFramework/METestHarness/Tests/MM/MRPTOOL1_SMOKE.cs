 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRPTOOL1_SMOKE : TestScript
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
new Control("Material Requirements Planning", "xpath","//div[@class='deptItem'][.='Material Requirements Planning']").Click();
new Control("MRP Utilities", "xpath","//div[@class='navItem'][.='MRP Utilities']").Click();
new Control("Convert MO Status from Planned to Firmed Planned", "xpath","//div[@class='navItem'][.='Convert MO Status from Planned to Firmed Planned']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MRPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPTOOL1] Perfoming VerifyExists on UpdateMOStatusForm...", Logger.MessageType.INF);
			Control MRPTOOL1_UpdateMOStatusForm = new Control("UpdateMOStatusForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRPTOOL1_UpdateMOStatusForm.Exists());

												
				CPCommon.CurrentComponent = "MRPTOOL1";
							CPCommon.WaitControlDisplayed(MRPTOOL1_UpdateMOStatusForm);
IWebElement formBttn = MRPTOOL1_UpdateMOStatusForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

