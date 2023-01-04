 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMVENDH_SMOKE : TestScript
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
new Control("Manage Vendor History", "xpath","//div[@class='navItem'][.='Manage Vendor History']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMVENDH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMVENDH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on Identification_VoucherNo...", Logger.MessageType.INF);
			Control APMVENDH_Identification_VoucherNo = new Control("Identification_VoucherNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VCHR_NO']");
			CPCommon.AssertEqual(true,APMVENDH_Identification_VoucherNo.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainForm);
IWebElement formBttn = APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMVENDH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMVENDH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVENDH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainForm);
formBttn = APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVENDH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on MainTab...", Logger.MessageType.INF);
			Control APMVENDH_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,APMVENDH_MainTab.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainTab);
IWebElement mTab = APMVENDH_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Vendor History").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on VendorHistory_Invoice_Number...", Logger.MessageType.INF);
			Control APMVENDH_VendorHistory_Invoice_Number = new Control("VendorHistory_Invoice_Number", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,APMVENDH_VendorHistory_Invoice_Number.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainTab);
mTab = APMVENDH_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on HeaderInfo_VATInfo_TaxID...", Logger.MessageType.INF);
			Control APMVENDH_HeaderInfo_VATInfo_TaxID = new Control("HeaderInfo_VATInfo_TaxID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,APMVENDH_HeaderInfo_VATInfo_TaxID.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainTab);
mTab = APMVENDH_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on Address_Select_PayVendor...", Logger.MessageType.INF);
			Control APMVENDH_Address_Select_PayVendor = new Control("Address_Select_PayVendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_VEND_ID']");
			CPCommon.AssertEqual(true,APMVENDH_Address_Select_PayVendor.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainTab);
mTab = APMVENDH_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Subcontractor Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformanceDate...", Logger.MessageType.INF);
			Control APMVENDH_SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformanceDate = new Control("SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformanceDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_POP_DT']");
			CPCommon.AssertEqual(true,APMVENDH_SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformanceDate.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainTab);
mTab = APMVENDH_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes/Doc Loc").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on NotesDocLoc_PrintNoteOnCheck...", Logger.MessageType.INF);
			Control APMVENDH_NotesDocLoc_PrintNoteOnCheck = new Control("NotesDocLoc_PrintNoteOnCheck", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PRINT_NOTE_FL']");
			CPCommon.AssertEqual(true,APMVENDH_NotesDocLoc_PrintNoteOnCheck.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainTab);
mTab = APMVENDH_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Recalculate Lines").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on RecalculateLines_RecalculationMethod...", Logger.MessageType.INF);
			Control APMVENDH_RecalculateLines_RecalculationMethod = new Control("RecalculateLines_RecalculationMethod", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RECAL_METH']");
			CPCommon.AssertEqual(true,APMVENDH_RecalculateLines_RecalculationMethod.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control APMVENDH_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1004176_APMVENDH_VCHRHDRHS_HDR");
			CPCommon.AssertEqual(true,APMVENDH_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on VoucherTotalsLink...", Logger.MessageType.INF);
			Control APMVENDH_VoucherTotalsLink = new Control("VoucherTotalsLink", "ID", "lnk_5457_APMVENDH_VCHRHDRHS_HDR");
			CPCommon.AssertEqual(true,APMVENDH_VoucherTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ExchangeRatesLink);
APMVENDH_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control APMVENDH_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDH_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ExchangeRates_Currencies_Transaction...", Logger.MessageType.INF);
			Control APMVENDH_ExchangeRates_Currencies_Transaction = new Control("ExchangeRates_Currencies_Transaction", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,APMVENDH_ExchangeRates_Currencies_Transaction.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ExchangeRatesForm);
formBttn = APMVENDH_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_VoucherTotalsLink);
APMVENDH_VoucherTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on VoucherTotalsForm...", Logger.MessageType.INF);
			Control APMVENDH_VoucherTotalsForm = new Control("VoucherTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVTOT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDH_VoucherTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on VoucherTotals_VoucherTotals_CostAmountTransaction...", Logger.MessageType.INF);
			Control APMVENDH_VoucherTotals_VoucherTotals_CostAmountTransaction = new Control("VoucherTotals_VoucherTotals_CostAmountTransaction", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVTOT_']/ancestor::form[1]/descendant::*[@id='TRN_CST_AMT']");
			CPCommon.AssertEqual(true,APMVENDH_VoucherTotals_VoucherTotals_CostAmountTransaction.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_VoucherTotalsForm);
formBttn = APMVENDH_VoucherTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_LN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control APMVENDH_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_LN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVENDH_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildForm);
formBttn = APMVENDH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVENDH_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVENDH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildFormTab...", Logger.MessageType.INF);
			Control APMVENDH_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_LN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,APMVENDH_ChildFormTab.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildFormTab);
mTab = APMVENDH_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Account Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_AccountInfo_LineNo...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_AccountInfo_LineNo = new Control("ChildForm_AccountInfo_LineNo", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_LN_']/ancestor::form[1]/descendant::*[@id='VCHR_LN_NO']");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_AccountInfo_LineNo.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildFormTab);
mTab = APMVENDH_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_OtherInfo_Vendor1099s_1099...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_OtherInfo_Vendor1099s_1099 = new Control("ChildForm_OtherInfo_Vendor1099s_1099", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_LN_']/ancestor::form[1]/descendant::*[@id='AP_1099_FL']");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_OtherInfo_Vendor1099s_1099.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildFormTab);
mTab = APMVENDH_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_LineNotes_Notes...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_LineNotes_Notes = new Control("ChildForm_LineNotes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_LN_']/ancestor::form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_LineNotes_Notes.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_VendorLaborLink...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_VendorLaborLink = new Control("ChildForm_VendorLaborLink", "ID", "lnk_5453_APMVENDH_LN");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_VendorLaborLink.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_CurrencyLineLink...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_CurrencyLineLink = new Control("ChildForm_CurrencyLineLink", "ID", "lnk_5454_APMVENDH_LN");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_CurrencyLineLink.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_CustomsInfoLink...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_CustomsInfoLink = new Control("ChildForm_CustomsInfoLink", "ID", "lnk_5456_APMVENDH_LN");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_CustomsInfoLink.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.AssertEqual(true,APMVENDH_ChildForm_LineNotes_Notes.Exists());

													
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildForm_VendorLaborLink);
APMVENDH_ChildForm_VendorLaborLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on Childform_VendorLaborForm...", Logger.MessageType.INF);
			Control APMVENDH_Childform_VendorLaborForm = new Control("Childform_VendorLaborForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_VCHRLABVENDHS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDH_Childform_VendorLaborForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExist on Childform_VendorLaborFormTable...", Logger.MessageType.INF);
			Control APMVENDH_Childform_VendorLaborFormTable = new Control("Childform_VendorLaborFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_VCHRLABVENDHS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVENDH_Childform_VendorLaborFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_Childform_VendorLaborForm);
formBttn = APMVENDH_Childform_VendorLaborForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVENDH_Childform_VendorLaborForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVENDH_Childform_VendorLaborForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_VendorLabor_SubLine...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_VendorLabor_SubLine = new Control("ChildForm_VendorLabor_SubLine", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVENDH_VCHRLABVENDHS_']/ancestor::form[1]/descendant::*[@id='SUB_LN_NO']");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_VendorLabor_SubLine.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_Childform_VendorLaborForm);
formBttn = APMVENDH_Childform_VendorLaborForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildForm_CurrencyLineLink);
APMVENDH_ChildForm_CurrencyLineLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_CurrencyLineForm...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_CurrencyLineForm = new Control("ChildForm_CurrencyLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPCURLN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_CurrencyLineForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_CurrencyLine_LineAmounts_Transaction_Currency...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_CurrencyLine_LineAmounts_Transaction_Currency = new Control("ChildForm_CurrencyLine_LineAmounts_Transaction_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPCURLN_']/ancestor::form[1]/descendant::*[@id='T_CURRENCY']");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_CurrencyLine_LineAmounts_Transaction_Currency.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildForm_CurrencyLineForm);
formBttn = APMVENDH_ChildForm_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildForm_CustomsInfoLink);
APMVENDH_ChildForm_CustomsInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_CustomsInfoForm...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_CustomsInfoForm = new Control("ChildForm_CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_CustomsInfoForm.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVENDH] Perfoming VerifyExists on ChildForm_CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control APMVENDH_ChildForm_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("ChildForm_CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,APMVENDH_ChildForm_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_ChildForm_CustomsInfoForm);
formBttn = APMVENDH_ChildForm_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainForm);
formBttn = APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? APMVENDH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");


												
				CPCommon.CurrentComponent = "APMVENDH";
							CPCommon.WaitControlDisplayed(APMVENDH_MainForm);
formBttn = APMVENDH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

