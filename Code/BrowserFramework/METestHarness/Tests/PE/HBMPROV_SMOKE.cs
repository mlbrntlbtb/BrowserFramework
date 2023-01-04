 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMPROV_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Benefits", "xpath","//div[@class='deptItem'][.='Benefits']").Click();
new Control("Benefit Controls", "xpath","//div[@class='navItem'][.='Benefit Controls']").Click();
new Control("Manage Benefit Providers", "xpath","//div[@class='navItem'][.='Manage Benefit Providers']").Click();


												
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMPROV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMPROV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming VerifyExists on ProviderCode...", Logger.MessageType.INF);
			Control HBMPROV_ProviderCode = new Control("ProviderCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROVIDER_CD']");
			CPCommon.AssertEqual(true,HBMPROV_ProviderCode.Exists());

												
				CPCommon.CurrentComponent = "HBMPROV";
							CPCommon.WaitControlDisplayed(HBMPROV_MainForm);
IWebElement formBttn = HBMPROV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMPROV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMPROV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMPROV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPROV_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming Click on ContactInformationLink...", Logger.MessageType.INF);
			Control HBMPROV_ContactInformationLink = new Control("ContactInformationLink", "ID", "lnk_1001720_HBMPROV_HBPROVIDER_HDR");
			CPCommon.WaitControlDisplayed(HBMPROV_ContactInformationLink);
HBMPROV_ContactInformationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming VerifyExists on ContactInformationForm...", Logger.MessageType.INF);
			Control HBMPROV_ContactInformationForm = new Control("ContactInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPROV_HBPROVIDERPHONE_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMPROV_ContactInformationForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming VerifyExist on ContactInformationFormTable...", Logger.MessageType.INF);
			Control HBMPROV_ContactInformationFormTable = new Control("ContactInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPROV_HBPROVIDERPHONE_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPROV_ContactInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPROV";
							CPCommon.WaitControlDisplayed(HBMPROV_ContactInformationForm);
formBttn = HBMPROV_ContactInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMPROV_ContactInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMPROV_ContactInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMPROV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPROV] Perfoming VerifyExists on ContactInformation_SequenceNumber...", Logger.MessageType.INF);
			Control HBMPROV_ContactInformation_SequenceNumber = new Control("ContactInformation_SequenceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPROV_HBPROVIDERPHONE_DTL_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,HBMPROV_ContactInformation_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "HBMPROV";
							CPCommon.WaitControlDisplayed(HBMPROV_MainForm);
formBttn = HBMPROV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");


												
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

