 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQETAX_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Reports/Inquiries", "xpath","//div[@class='navItem'][.='Payroll Reports/Inquiries']").Click();
new Control("View Employee Taxes", "xpath","//div[@class='navItem'][.='View Employee Taxes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQETAX_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQETAX_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQETAX_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQETAX_Identification_Employee.Exists());

												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Inquiry Details Form");


												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetailsForm...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_InquiryDetailsForm);
IWebElement formBttn = PRQETAX_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQETAX_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQETAX_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_TransactionType...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_TransactionType = new Control("InquiryDetails_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_InquiryDetailsTab...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_InquiryDetailsTab = new Control("InquiryDetails_InquiryDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_InquiryDetailsTab.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_InquiryDetails_InquiryDetailsTab);
IWebElement mTab = PRQETAX_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Taxes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_Taxes_PayCycle...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_Taxes_PayCycle = new Control("InquiryDetails_Taxes_PayCycle", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_CTW_']/ancestor::form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_Taxes_PayCycle.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_InquiryDetails_InquiryDetailsTab);
mTab = PRQETAX_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Tax Reporting Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_TaxReportingInformation_AlaskaLabel...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_TaxReportingInformation_AlaskaLabel = new Control("InquiryDetails_TaxReportingInformation_AlaskaLabel", "xpath", "//input[@id='S_MOD_TYPE_CD']/ancestor::form[1]/descendant::*[@id='ALASKA_BOX']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_TaxReportingInformation_AlaskaLabel.Exists());

											Driver.SessionLogger.WriteLine("Multi State Tax Inquiry");


												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_MultiStateTaxInquiryLink...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_MultiStateTaxInquiryLink = new Control("InquiryDetails_MultiStateTaxInquiryLink", "ID", "lnk_5383_PRQETAX_CTW");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_MultiStateTaxInquiryLink.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_InquiryDetails_MultiStateTaxInquiryLink);
PRQETAX_InquiryDetails_MultiStateTaxInquiryLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_MultiStateTaxInquiryForm...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_MultiStateTaxInquiryForm = new Control("InquiryDetails_MultiStateTaxInquiryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_MULTI_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_MultiStateTaxInquiryForm.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExist on InquiryDetails_MultiStateTaxInquiryFormTable...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_MultiStateTaxInquiryFormTable = new Control("InquiryDetails_MultiStateTaxInquiryFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_MULTI_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_MultiStateTaxInquiryFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_InquiryDetails_MultiStateTaxInquiryForm);
formBttn = PRQETAX_InquiryDetails_MultiStateTaxInquiryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQETAX_InquiryDetails_MultiStateTaxInquiryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQETAX_InquiryDetails_MultiStateTaxInquiryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQETAX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQETAX] Perfoming VerifyExists on InquiryDetails_MultiStateTaxInquiry_TransactionType...", Logger.MessageType.INF);
			Control PRQETAX_InquiryDetails_MultiStateTaxInquiry_TransactionType = new Control("InquiryDetails_MultiStateTaxInquiry_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQETAX_MULTI_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQETAX_InquiryDetails_MultiStateTaxInquiry_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_InquiryDetails_MultiStateTaxInquiryForm);
formBttn = PRQETAX_InquiryDetails_MultiStateTaxInquiryForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQETAX";
							CPCommon.WaitControlDisplayed(PRQETAX_MainForm);
formBttn = PRQETAX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

