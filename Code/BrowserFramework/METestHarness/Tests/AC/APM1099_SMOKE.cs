 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APM1099_SMOKE : TestScript
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
new Control("Year-End Processing", "xpath","//div[@class='navItem'][.='Year-End Processing']").Click();
new Control("Edit 1099 Information", "xpath","//div[@class='navItem'][.='Edit 1099 Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Query...", Logger.MessageType.INF);
			Control Query_Query = new Control("Query", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Query);
if (Query_Query.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Query.Click(5,5);
else Query_Query.Click(4.5);


												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APM1099_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APM1099_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APM1099_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APM1099_MainForm);
IWebElement formBttn = APM1099_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APM1099_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APM1099_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExists on TaxableEntity...", Logger.MessageType.INF);
			Control APM1099_TaxableEntity = new Control("TaxableEntity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY_ID']");
			CPCommon.AssertEqual(true,APM1099_TaxableEntity.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
							CPCommon.AssertEqual(true,APM1099_MainForm.Exists());

												Driver.SessionLogger.WriteLine("Pay Vendor Summary Information");


												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExist on PayVendorSummaryInformationFormTable...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformationFormTable = new Control("PayVendorSummaryInformationFormTable", "xpath", "//div[starts-with(@id,'pr__APM1099_VEND1099_DTL_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExists on PayVendorSummaryInformationForm...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformationForm = new Control("PayVendorSummaryInformationForm", "xpath", "//div[starts-with(@id,'pr__APM1099_VEND1099_DTL_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformationForm.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
							CPCommon.WaitControlDisplayed(APM1099_PayVendorSummaryInformationForm);
formBttn = APM1099_PayVendorSummaryInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APM1099_PayVendorSummaryInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APM1099_PayVendorSummaryInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExists on PayVendorSummaryInformation_PayVendor...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformation_PayVendor = new Control("PayVendorSummaryInformation_PayVendor", "xpath", "//div[starts-with(@id,'pr__APM1099_VEND1099_DTL_')]/ancestor::form[1]/descendant::*[@id='PAY_VEND_ID']");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformation_PayVendor.Exists());

											Driver.SessionLogger.WriteLine("Check Detail");


												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExists on PayVendorSummaryInformation_CheckDetailLink...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformation_CheckDetailLink = new Control("PayVendorSummaryInformation_CheckDetailLink", "ID", "lnk_1002654_APM1099_VEND1099_DTL");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformation_CheckDetailLink.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
							CPCommon.WaitControlDisplayed(APM1099_PayVendorSummaryInformation_CheckDetailLink);
APM1099_PayVendorSummaryInformation_CheckDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExists on PayVendorSummaryInformation_CheckDetailForm...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformation_CheckDetailForm = new Control("PayVendorSummaryInformation_CheckDetailForm", "xpath", "//div[starts-with(@id,'pr__APM1099_VENDCHK_CHKHDR_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformation_CheckDetailForm.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExists on PayVendorSummaryInformation_CheckDetail_TotalPaidAmount...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformation_CheckDetail_TotalPaidAmount = new Control("PayVendorSummaryInformation_CheckDetail_TotalPaidAmount", "xpath", "//div[starts-with(@id,'pr__APM1099_VENDCHK_CHKHDR_')]/ancestor::form[1]/descendant::*[@id='TOTALPAIDAMT']");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformation_CheckDetail_TotalPaidAmount.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APM1099] Perfoming VerifyExist on PayVendorSummaryInformation_CheckDetail_ChildFormTable...", Logger.MessageType.INF);
			Control APM1099_PayVendorSummaryInformation_CheckDetail_ChildFormTable = new Control("PayVendorSummaryInformation_CheckDetail_ChildFormTable", "xpath", "//div[starts-with(@id,'pr__APM1099_VENDCHK_CHKDETAIL_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APM1099_PayVendorSummaryInformation_CheckDetail_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APM1099";
							CPCommon.WaitControlDisplayed(APM1099_PayVendorSummaryInformation_CheckDetailForm);
formBttn = APM1099_PayVendorSummaryInformation_CheckDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "APM1099";
							CPCommon.WaitControlDisplayed(APM1099_MainForm);
formBttn = APM1099_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

