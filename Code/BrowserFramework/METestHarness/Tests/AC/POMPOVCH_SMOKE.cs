 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMPOVCH_SMOKE : TestScript
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
new Control("Voucher Processing", "xpath","//div[@class='navItem'][.='Voucher Processing']").Click();
new Control("Manage Purchase Order Vouchers", "xpath","//div[@class='navItem'][.='Manage Purchase Order Vouchers']").Click();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control POMPOVCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_MainForm);
IWebElement formBttn = POMPOVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Maximize']")).Count <= 0 ? POMPOVCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Maximize')]")).FirstOrDefault() :
POMPOVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Maximize']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Maximize not found ");


												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.AssertEqual(true,POMPOVCH_MainForm.Exists());

													
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainForm);
formBttn = POMPOVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? POMPOVCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
POMPOVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainForm);
formBttn = POMPOVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on Identification_VoucherNo...", Logger.MessageType.INF);
			Control POMPOVCH_Identification_VoucherNo = new Control("Identification_VoucherNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VCHR_NO']");
			CPCommon.AssertEqual(true,POMPOVCH_Identification_VoucherNo.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control POMPOVCH_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
IWebElement mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on HeaderInfo_Invoice_Number...", Logger.MessageType.INF);
			Control POMPOVCH_HeaderInfo_Invoice_Number = new Control("HeaderInfo_Invoice_Number", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_HeaderInfo_Invoice_Number.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on Details_SalesTaxVATInfo_Taxable...", Logger.MessageType.INF);
			Control POMPOVCH_Details_SalesTaxVATInfo_Taxable = new Control("Details_SalesTaxVATInfo_Taxable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_SALES_TAX_SRC_CD']");
			CPCommon.AssertEqual(true,POMPOVCH_Details_SalesTaxVATInfo_Taxable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on Address_Select_PayVendor...", Logger.MessageType.INF);
			Control POMPOVCH_Address_Select_PayVendor = new Control("Address_Select_PayVendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_VEND_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_Address_Select_PayVendor.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Check").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on Check_PayVendor...", Logger.MessageType.INF);
			Control POMPOVCH_Check_PayVendor = new Control("Check_PayVendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY___VEND___ID___CHECKSUBTASK']");
			CPCommon.AssertEqual(true,POMPOVCH_Check_PayVendor.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Subcontractor Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformaceDate...", Logger.MessageType.INF);
			Control POMPOVCH_SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformaceDate = new Control("SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformaceDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_POP_DT']");
			CPCommon.AssertEqual(true,POMPOVCH_SubcontractorInfo_SubcontractorPaymentControl_InvoicePeriodOfPerformaceDate.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes/Doc Loc").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on NotesDocLoc_PrintNoteOnCheck...", Logger.MessageType.INF);
			Control POMPOVCH_NotesDocLoc_PrintNoteOnCheck = new Control("NotesDocLoc_PrintNoteOnCheck", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PRINT_NOTE_FL']");
			CPCommon.AssertEqual(true,POMPOVCH_NotesDocLoc_PrintNoteOnCheck.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainFormTab);
mTab = POMPOVCH_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Actions").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on Actions_InvoiceAmountDiscrepancy_AdjustHeaderInvoiceAmount...", Logger.MessageType.INF);
			Control POMPOVCH_Actions_InvoiceAmountDiscrepancy_AdjustHeaderInvoiceAmount = new Control("Actions_InvoiceAmountDiscrepancy_AdjustHeaderInvoiceAmount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVCHDR_ADJUST_CB']");
			CPCommon.AssertEqual(true,POMPOVCH_Actions_InvoiceAmountDiscrepancy_AdjustHeaderInvoiceAmount.Exists());

											Driver.SessionLogger.WriteLine("Link");


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on AutoAllocateChargesLink...", Logger.MessageType.INF);
			Control POMPOVCH_AutoAllocateChargesLink = new Control("AutoAllocateChargesLink", "ID", "lnk_1003768_POMPOVCH_VCHRHDR");
			CPCommon.WaitControlDisplayed(POMPOVCH_AutoAllocateChargesLink);
POMPOVCH_AutoAllocateChargesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on AutoAllocateCharges_ChargeType...", Logger.MessageType.INF);
			Control POMPOVCH_AutoAllocateCharges_ChargeType = new Control("AutoAllocateCharges_ChargeType", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_AUTOALLOCCHG_']/ancestor::form[1]/descendant::*[@id='LN___CHG___TYPE']");
			CPCommon.AssertEqual(true,POMPOVCH_AutoAllocateCharges_ChargeType.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Close on AutoAllocateChargesForm...", Logger.MessageType.INF);
			Control POMPOVCH_AutoAllocateChargesForm = new Control("AutoAllocateChargesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_AUTOALLOCCHG_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_AutoAllocateChargesForm);
formBttn = POMPOVCH_AutoAllocateChargesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ExchangeRatesLink...", Logger.MessageType.INF);
			Control POMPOVCH_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1003754_POMPOVCH_VCHRHDR");
			CPCommon.WaitControlDisplayed(POMPOVCH_ExchangeRatesLink);
POMPOVCH_ExchangeRatesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on ExchangeRates_Currencies_TransactionCurrency...", Logger.MessageType.INF);
			Control POMPOVCH_ExchangeRates_Currencies_TransactionCurrency = new Control("ExchangeRates_Currencies_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,POMPOVCH_ExchangeRates_Currencies_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Close on ExchangeRatesForm...", Logger.MessageType.INF);
			Control POMPOVCH_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_ExchangeRatesForm);
formBttn = POMPOVCH_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POMPOVCH_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007813_POMPOVCH_VCHRHDR");
			CPCommon.WaitControlDisplayed(POMPOVCH_HeaderDocumentsLink);
POMPOVCH_HeaderDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on HeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_HeaderDocumentsFormTable = new Control("HeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_HeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POMPOVCH_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_HeaderDocumentsForm);
formBttn = POMPOVCH_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POMPOVCH_HeaderDocuments_Document = new Control("HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_HeaderDocumentsForm);
formBttn = POMPOVCH_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


											CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on ChildForm...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm);
formBttn = POMPOVCH_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on ChildForm_VchrLine...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_VchrLine = new Control("ChildForm_VchrLine", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]/descendant::*[@id='VCHR_LN_NO']");
			CPCommon.AssertEqual(true,POMPOVCH_ChildForm_VchrLine.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Select on ChildFormTab...", Logger.MessageType.INF);
			Control POMPOVCH_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildFormTab);
mTab = POMPOVCH_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on ChildForm_VoucherLineDetails_InvQty...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_VoucherLineDetails_InvQty = new Control("ChildForm_VoucherLineDetails_InvQty", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]/descendant::*[@id='QTY']");
			CPCommon.AssertEqual(true,POMPOVCH_ChildForm_VoucherLineDetails_InvQty.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_ChildFormTab);
mTab = POMPOVCH_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PO Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on ChildForm_POLineDetails_Item...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_POLineDetails_Item = new Control("ChildForm_POLineDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_ChildForm_POLineDetails_Item.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_ChildFormTab);
mTab = POMPOVCH_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on ChildForm_Notes...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_Notes = new Control("ChildForm_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_POLN_']/ancestor::form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,POMPOVCH_ChildForm_Notes.Exists());

											Driver.SessionLogger.WriteLine("Child Form Links");


											CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_ReceiptsLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_ReceiptsLink = new Control("ChildForm_ReceiptsLink", "ID", "lnk_2818_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_ReceiptsLink);
POMPOVCH_ChildForm_ReceiptsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on ReceiptsForm...", Logger.MessageType.INF);
			Control POMPOVCH_ReceiptsForm = new Control("ReceiptsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_RECPTLN_RECEIPT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_ReceiptsForm);
formBttn = POMPOVCH_ReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_ReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_ReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on Receipts_Receipt...", Logger.MessageType.INF);
			Control POMPOVCH_Receipts_Receipt = new Control("Receipts_Receipt", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_RECPTLN_RECEIPT_']/ancestor::form[1]/descendant::*[@id='RECPT_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_Receipts_Receipt.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_ReceiptsForm);
formBttn = POMPOVCH_ReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMPOVCH_ReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMPOVCH_ReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on ReceiptsFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_ReceiptsFormTable = new Control("ReceiptsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_RECPTLN_RECEIPT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_ReceiptsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_ReceiptsForm);
formBttn = POMPOVCH_ReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_ReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_ReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_ReceiptsForm);
formBttn = POMPOVCH_ReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_VendorReturnsLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_VendorReturnsLink = new Control("ChildForm_VendorReturnsLink", "ID", "lnk_2819_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_VendorReturnsLink);
POMPOVCH_ChildForm_VendorReturnsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on VendorReturnsFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_VendorReturnsFormTable = new Control("VendorReturnsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VENDRTRNLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_VendorReturnsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on VendorReturnsForm...", Logger.MessageType.INF);
			Control POMPOVCH_VendorReturnsForm = new Control("VendorReturnsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VENDRTRNLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_VendorReturnsForm);
formBttn = POMPOVCH_VendorReturnsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_VendorReturnsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_VendorReturnsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on VendorReturns_VendorReturn...", Logger.MessageType.INF);
			Control POMPOVCH_VendorReturns_VendorReturn = new Control("VendorReturns_VendorReturn", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VENDRTRNLN_']/ancestor::form[1]/descendant::*[@id='RTRN_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_VendorReturns_VendorReturn.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_VendorReturnsForm);
formBttn = POMPOVCH_VendorReturnsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_LineChargesLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_LineChargesLink = new Control("ChildForm_LineChargesLink", "ID", "lnk_2820_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_LineChargesLink);
POMPOVCH_ChildForm_LineChargesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on LineChargesFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_LineChargesFormTable = new Control("LineChargesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLNCHG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_LineChargesFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on LineChargesForm...", Logger.MessageType.INF);
			Control POMPOVCH_LineChargesForm = new Control("LineChargesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLNCHG_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_LineChargesForm);
formBttn = POMPOVCH_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_LineChargesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_LineChargesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on LineCharges_ChargeType...", Logger.MessageType.INF);
			Control POMPOVCH_LineCharges_ChargeType = new Control("LineCharges_ChargeType", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLNCHG_']/ancestor::form[1]/descendant::*[@id='LN_CHG_TYPE']");
			CPCommon.AssertEqual(true,POMPOVCH_LineCharges_ChargeType.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_LineChargesForm);
formBttn = POMPOVCH_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_AccountsDistributionLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_AccountsDistributionLink = new Control("ChildForm_AccountsDistributionLink", "ID", "lnk_2821_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_AccountsDistributionLink);
POMPOVCH_ChildForm_AccountsDistributionLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on AccountDistributionFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_AccountDistributionFormTable = new Control("AccountDistributionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLNACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_AccountDistributionFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on AccountsDistributionForm...", Logger.MessageType.INF);
			Control POMPOVCH_AccountsDistributionForm = new Control("AccountsDistributionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLNACCT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_AccountsDistributionForm);
formBttn = POMPOVCH_AccountsDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_AccountsDistributionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_AccountsDistributionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on AccountsDistribution_Account...", Logger.MessageType.INF);
			Control POMPOVCH_AccountsDistribution_Account = new Control("AccountsDistribution_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLNACCT_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_AccountsDistribution_Account.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_AccountsDistributionForm);
formBttn = POMPOVCH_AccountsDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_VendorLaborLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_VendorLaborLink = new Control("ChildForm_VendorLaborLink", "ID", "lnk_2822_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_VendorLaborLink);
POMPOVCH_ChildForm_VendorLaborLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on VendorLaborTable...", Logger.MessageType.INF);
			Control POMPOVCH_VendorLaborTable = new Control("VendorLaborTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLABVEND_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_VendorLaborTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on VendorLaborForm...", Logger.MessageType.INF);
			Control POMPOVCH_VendorLaborForm = new Control("VendorLaborForm", "XPATH", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLABVEND_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_VendorLaborForm);
formBttn = POMPOVCH_VendorLaborForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_VendorLaborForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_VendorLaborForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on VendorLabor_SubLine...", Logger.MessageType.INF);
			Control POMPOVCH_VendorLabor_SubLine = new Control("VendorLabor_SubLine", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_VCHRLABVEND_']/ancestor::form[1]/descendant::*[@id='SUB_LN_NO']");
			CPCommon.AssertEqual(true,POMPOVCH_VendorLabor_SubLine.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_VendorLaborForm);
formBttn = POMPOVCH_VendorLaborForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_CurrencyLineLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_CurrencyLineLink = new Control("ChildForm_CurrencyLineLink", "ID", "lnk_19868_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_CurrencyLineLink);
POMPOVCH_ChildForm_CurrencyLineLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on CurrencyLine_LineAmounts_CurrencyTransaction...", Logger.MessageType.INF);
			Control POMPOVCH_CurrencyLine_LineAmounts_CurrencyTransaction = new Control("CurrencyLine_LineAmounts_CurrencyTransaction", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_CURRENCYLN_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,POMPOVCH_CurrencyLine_LineAmounts_CurrencyTransaction.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Close on CurrencyLineForm...", Logger.MessageType.INF);
			Control POMPOVCH_CurrencyLineForm = new Control("CurrencyLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_CURRENCYLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_CurrencyLineForm);
formBttn = POMPOVCH_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_CustomsInfoLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_CustomsInfoLink = new Control("ChildForm_CustomsInfoLink", "ID", "lnk_2824_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_CustomsInfoLink);
POMPOVCH_ChildForm_CustomsInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control POMPOVCH_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Close on CustomsInfoForm...", Logger.MessageType.INF);
			Control POMPOVCH_CustomsInfoForm = new Control("CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_CustomsInfoForm);
formBttn = POMPOVCH_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_SubPOLineLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_SubPOLineLink = new Control("ChildForm_SubPOLineLink", "ID", "lnk_2825_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_SubPOLineLink);
POMPOVCH_ChildForm_SubPOLineLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on SubPOLine_POLine...", Logger.MessageType.INF);
			Control POMPOVCH_SubPOLine_POLine = new Control("SubPOLine_POLine", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_SUBPOLNINFO_']/ancestor::form[1]/descendant::*[@id='PO___LN___NO']");
			CPCommon.AssertEqual(true,POMPOVCH_SubPOLine_POLine.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Close on SubPOLineForm...", Logger.MessageType.INF);
			Control POMPOVCH_SubPOLineForm = new Control("SubPOLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMPOVCH_SUBPOLNINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_SubPOLineForm);
formBttn = POMPOVCH_SubPOLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											CPCommon.ScrollDown();


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming Click on ChildForm_LineDocumentsLink...", Logger.MessageType.INF);
			Control POMPOVCH_ChildForm_LineDocumentsLink = new Control("ChildForm_LineDocumentsLink", "ID", "lnk_1007815_POMPOVCH_POLN");
			CPCommon.WaitControlDisplayed(POMPOVCH_ChildForm_LineDocumentsLink);
POMPOVCH_ChildForm_LineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExist on LineDocumentsFormTable...", Logger.MessageType.INF);
			Control POMPOVCH_LineDocumentsFormTable = new Control("LineDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMPOVCH_LineDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming ClickButtonIfExists on LineDocumentsForm...", Logger.MessageType.INF);
			Control POMPOVCH_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMPOVCH_LineDocumentsForm);
formBttn = POMPOVCH_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMPOVCH_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMPOVCH_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMPOVCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMPOVCH] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control POMPOVCH_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMPOVCH_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_LineDocumentsForm);
formBttn = POMPOVCH_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "POMPOVCH";
							CPCommon.WaitControlDisplayed(POMPOVCH_MainForm);
formBttn = POMPOVCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

