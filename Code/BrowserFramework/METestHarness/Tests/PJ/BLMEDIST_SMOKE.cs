 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMEDIST_SMOKE : TestScript
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
new Control("Billing Controls", "xpath","//div[@class='navItem'][.='Billing Controls']").Click();
new Control("Configure Billing EDI Settings", "xpath","//div[@class='navItem'][.='Configure Billing EDI Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMEDIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEDIST] Perfoming VerifyExists on SenderInformationForm...", Logger.MessageType.INF);
			Control BLMEDIST_SenderInformationForm = new Control("SenderInformationForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMEDIST_SenderInformationForm.Exists());

												
				CPCommon.CurrentComponent = "BLMEDIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMEDIST] Perfoming VerifyExists on SenderInformation_CAGECode...", Logger.MessageType.INF);
			Control BLMEDIST_SenderInformation_CAGECode = new Control("SenderInformation_CAGECode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EDI_SENDER_CAGE_CD']");
			CPCommon.AssertEqual(true,BLMEDIST_SenderInformation_CAGECode.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMEDIST";
							CPCommon.WaitControlDisplayed(BLMEDIST_SenderInformationForm);
IWebElement formBttn = BLMEDIST_SenderInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

