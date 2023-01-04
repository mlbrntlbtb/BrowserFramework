 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APRCK_SMOKE : TestScript
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
new Control("Payment Processing", "xpath","//div[@class='navItem'][.='Payment Processing']").Click();
new Control("Print/Void Checks", "xpath","//div[@class='navItem'][.='Print/Void Checks']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APRCK_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APRCK_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming Select on MoreOptionsTab...", Logger.MessageType.INF);
			Control APRCK_MoreOptionsTab = new Control("MoreOptionsTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(APRCK_MoreOptionsTab);
IWebElement mTab = APRCK_MoreOptionsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Signature").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming VerifyExists on MoreOptions_Signature_PrintSignatureOnCheck...", Logger.MessageType.INF);
			Control APRCK_MoreOptions_Signature_PrintSignatureOnCheck = new Control("MoreOptions_Signature_PrintSignatureOnCheck", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SIGNATURE_FL']");
			CPCommon.AssertEqual(true,APRCK_MoreOptions_Signature_PrintSignatureOnCheck.Exists());

												
				CPCommon.CurrentComponent = "APRCK";
							CPCommon.WaitControlDisplayed(APRCK_MoreOptionsTab);
mTab = APRCK_MoreOptionsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Company Logo").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming VerifyExists on MoreOptions_CompanyLogo_LogoLocation...", Logger.MessageType.INF);
			Control APRCK_MoreOptions_CompanyLogo_LogoLocation = new Control("MoreOptions_CompanyLogo_LogoLocation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LOGO_LOCATION']");
			CPCommon.AssertEqual(true,APRCK_MoreOptions_CompanyLogo_LogoLocation.Exists());

												
				CPCommon.CurrentComponent = "APRCK";
							CPCommon.WaitControlDisplayed(APRCK_MoreOptionsTab);
mTab = APRCK_MoreOptionsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Check Stub").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming VerifyExists on MoreOptions_CheckStub_AsColumn2...", Logger.MessageType.INF);
			Control APRCK_MoreOptions_CheckStub_AsColumn2 = new Control("MoreOptions_CheckStub_AsColumn2", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STUB_COLUMN2']");
			CPCommon.AssertEqual(true,APRCK_MoreOptions_CheckStub_AsColumn2.Exists());

												
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APRCK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APRCK_MainForm);
IWebElement formBttn = APRCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APRCK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APRCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "APRCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRCK] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control APRCK_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APRCK_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "APRCK";
							CPCommon.WaitControlDisplayed(APRCK_MainForm);
formBttn = APRCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

