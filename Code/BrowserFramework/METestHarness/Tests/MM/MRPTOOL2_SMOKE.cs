 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRPTOOL2_SMOKE : TestScript
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
new Control("Update Netting Flags for MRB Locations", "xpath","//div[@class='navItem'][.='Update Netting Flags for MRB Locations']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MRPTOOL2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPTOOL2] Perfoming VerifyExists on ChangeMRBNettingFlagsForm...", Logger.MessageType.INF);
			Control MRPTOOL2_ChangeMRBNettingFlagsForm = new Control("ChangeMRBNettingFlagsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRPTOOL2_ChangeMRBNettingFlagsForm.Exists());

												
				CPCommon.CurrentComponent = "MRPTOOL2";
							CPCommon.WaitControlDisplayed(MRPTOOL2_ChangeMRBNettingFlagsForm);
IWebElement formBttn = MRPTOOL2_ChangeMRBNettingFlagsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

