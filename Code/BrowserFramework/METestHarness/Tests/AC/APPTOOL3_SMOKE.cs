 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPTOOL3_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Utilities", "xpath","//div[@class='navItem'][.='Accounts Payable Utilities']").Click();
new Control("Convert EFT File to Non-US Format", "xpath","//div[@class='navItem'][.='Convert EFT File to Non-US Format']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL3] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPTOOL3_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPTOOL3_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL3] Perfoming VerifyExists on InputFile_FileLocation...", Logger.MessageType.INF);
			Control APPTOOL3_InputFile_FileLocation = new Control("InputFile_FileLocation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INPUT_ALT_FILE_LOC']");
			CPCommon.AssertEqual(true,APPTOOL3_InputFile_FileLocation.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL3] Perfoming Click on Generate_OutputFile_OptionsLink...", Logger.MessageType.INF);
			Control APPTOOL3_Generate_OutputFile_OptionsLink = new Control("Generate_OutputFile_OptionsLink", "ID", "lnk_1006236_APPTOOL3_EFT");
			CPCommon.WaitControlDisplayed(APPTOOL3_Generate_OutputFile_OptionsLink);
APPTOOL3_Generate_OutputFile_OptionsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "APPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL3] Perfoming VerifyExists on OptionsForm...", Logger.MessageType.INF);
			Control APPTOOL3_OptionsForm = new Control("OptionsForm", "xpath", "//div[starts-with(@id,'pr__APPTOOL3_CLIEOP03_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,APPTOOL3_OptionsForm.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL3] Perfoming VerifyExists on Options_SenderID...", Logger.MessageType.INF);
			Control APPTOOL3_Options_SenderID = new Control("Options_SenderID", "xpath", "//div[starts-with(@id,'pr__APPTOOL3_CLIEOP03_')]/ancestor::form[1]/descendant::*[@id='SENDERID']");
			CPCommon.AssertEqual(true,APPTOOL3_Options_SenderID.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL3";
							CPCommon.WaitControlDisplayed(APPTOOL3_OptionsForm);
IWebElement formBttn = APPTOOL3_OptionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APPTOOL3";
							CPCommon.WaitControlDisplayed(APPTOOL3_MainForm);
formBttn = APPTOOL3_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

