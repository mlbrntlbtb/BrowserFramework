 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMOPEN_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Payment Processing", "xpath","//div[@class='navItem'][.='Payment Processing']").Click();
new Control("Edit Voucher Payment Status", "xpath","//div[@class='navItem'][.='Edit Voucher Payment Status']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMOPEN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMOPEN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PayVendor...", Logger.MessageType.INF);
			Control APMOPEN_PayVendor = new Control("PayVendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_VEND_ID']");
			CPCommon.AssertEqual(true,APMOPEN_PayVendor.Exists());

											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_OPENAP_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExist on ChildTable...", Logger.MessageType.INF);
			Control APMOPEN_ChildTable = new Control("ChildTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_OPENAP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMOPEN_ChildTable.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm);
IWebElement formBttn = APMOPEN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMOPEN_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMOPEN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_OPENAP_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_ChildFormTab);
IWebElement mTab = APMOPEN_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Accounts Payable").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_AccountsPayable_VendorInfo_VoucherVendor...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_AccountsPayable_VendorInfo_VoucherVendor = new Control("ChildForm_AccountsPayable_VendorInfo_VoucherVendor", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_OPENAP_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_AccountsPayable_VendorInfo_VoucherVendor.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_ChildFormTab);
mTab = APMOPEN_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Voucher Header Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_VoucherHeaderDetail_InvoiceDetail_InvoiceNumber...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_VoucherHeaderDetail_InvoiceDetail_InvoiceNumber = new Control("ChildForm_VoucherHeaderDetail_InvoiceDetail_InvoiceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_OPENAP_']/ancestor::form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_VoucherHeaderDetail_InvoiceDetail_InvoiceNumber.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_TotalsLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_TotalsLink = new Control("ChildForm_TotalsLink", "ID", "lnk_1007263_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_TotalsLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_TotalsLink);
APMOPEN_ChildForm_TotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on Totals_FunctionalCurrencyTotals_AmountPaid...", Logger.MessageType.INF);
			Control APMOPEN_Totals_FunctionalCurrencyTotals_AmountPaid = new Control("Totals_FunctionalCurrencyTotals_AmountPaid", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_FCTOTALS_']/ancestor::form[1]/descendant::*[@id='PAID_AMT']");
			CPCommon.AssertEqual(true,APMOPEN_Totals_FunctionalCurrencyTotals_AmountPaid.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on TotalsForm...", Logger.MessageType.INF);
			Control APMOPEN_TotalsForm = new Control("TotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_FCTOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_TotalsForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_TotalsForm);
formBttn = APMOPEN_TotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_VendorTotalsLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_VendorTotalsLink = new Control("ChildForm_VendorTotalsLink", "ID", "lnk_1007266_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_VendorTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_VendorTotalsLink);
APMOPEN_ChildForm_VendorTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VendorTotals_FunctionalCurrencyTotals_DiscountAmount...", Logger.MessageType.INF);
			Control APMOPEN_VendorTotals_FunctionalCurrencyTotals_DiscountAmount = new Control("VendorTotals_FunctionalCurrencyTotals_DiscountAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__AP_FUNCCRNCY_TOTALS_']/ancestor::form[1]/descendant::*[@id='DISC_AMT']");
			CPCommon.AssertEqual(true,APMOPEN_VendorTotals_FunctionalCurrencyTotals_DiscountAmount.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VendorTotalsForm...", Logger.MessageType.INF);
			Control APMOPEN_VendorTotalsForm = new Control("VendorTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AP_FUNCCRNCY_TOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_VendorTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_VendorTotalsForm);
formBttn = APMOPEN_VendorTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_VoucherTotalsLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_VoucherTotalsLink = new Control("ChildForm_VoucherTotalsLink", "ID", "lnk_1007269_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_VoucherTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_VoucherTotalsLink);
APMOPEN_ChildForm_VoucherTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VoucherTotals_VoucherTotals_CostAmountLabel...", Logger.MessageType.INF);
			Control APMOPEN_VoucherTotals_VoucherTotals_CostAmountLabel = new Control("VoucherTotals_VoucherTotals_CostAmountLabel", "xpath", "//input[@id='PAY_ADDR_DC']/ancestor::div[@class='rsltst']/following-sibling::div[@class='rsltst']/form/descendant::*[@id='TRN_CST_AMT']/preceding-sibling::span[1]");
			CPCommon.AssertEqual(true,APMOPEN_VoucherTotals_VoucherTotals_CostAmountLabel.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VoucherTotalsForm...", Logger.MessageType.INF);
			Control APMOPEN_VoucherTotalsForm = new Control("VoucherTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVTOT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_VoucherTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_VoucherTotalsForm);
formBttn = APMOPEN_VoucherTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_1003203_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_ExchangeRatesLink);
APMOPEN_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ExchangeRates_Currencies_Pay...", Logger.MessageType.INF);
			Control APMOPEN_ExchangeRates_Currencies_Pay = new Control("ExchangeRates_Currencies_Pay", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]/descendant::*[@id='PAY_CRNCY_CD']");
			CPCommon.AssertEqual(true,APMOPEN_ExchangeRates_Currencies_Pay.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control APMOPEN_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ExchangeRatesForm);
formBttn = APMOPEN_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_VoucherDetailLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_VoucherDetailLink = new Control("ChildForm_VoucherDetailLink", "ID", "lnk_1003204_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_VoucherDetailLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_VoucherDetailLink);
APMOPEN_ChildForm_VoucherDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VoucherTotalForm...", Logger.MessageType.INF);
			Control APMOPEN_VoucherTotalForm = new Control("VoucherTotalForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_VOUCHERTOT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_VoucherTotalForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VoucherTotal_AmountPaid...", Logger.MessageType.INF);
			Control APMOPEN_VoucherTotal_AmountPaid = new Control("VoucherTotal_AmountPaid", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_VOUCHERTOT_']/ancestor::form[1]/descendant::*[@id='PAID_AMT']");
			CPCommon.AssertEqual(true,APMOPEN_VoucherTotal_AmountPaid.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VoucherDetailForm...", Logger.MessageType.INF);
			Control APMOPEN_VoucherDetailForm = new Control("VoucherDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_LN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_VoucherDetailForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExist on VoucherDetailFormTable...", Logger.MessageType.INF);
			Control APMOPEN_VoucherDetailFormTable = new Control("VoucherDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_LN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMOPEN_VoucherDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_VoucherDetailForm);
formBttn = APMOPEN_VoucherDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMOPEN_VoucherDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMOPEN_VoucherDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on VoucherDetail_LineDetails_OrgName...", Logger.MessageType.INF);
			Control APMOPEN_VoucherDetail_LineDetails_OrgName = new Control("VoucherDetail_LineDetails_OrgName", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_LN_']/ancestor::form[1]/descendant::*[@id='ORG_NAME']");
			CPCommon.AssertEqual(true,APMOPEN_VoucherDetail_LineDetails_OrgName.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_VoucherTotalForm);
formBttn = APMOPEN_VoucherTotalForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_PODetailsLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_PODetailsLink = new Control("ChildForm_PODetailsLink", "ID", "lnk_1003206_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_PODetailsLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_PODetailsLink);
APMOPEN_ChildForm_PODetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExist on PODetailsFormTable...", Logger.MessageType.INF);
			Control APMOPEN_PODetailsFormTable = new Control("PODetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_POLN_PODTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMOPEN_PODetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PODetailsForm...", Logger.MessageType.INF);
			Control APMOPEN_PODetailsForm = new Control("PODetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_POLN_PODTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_PODetailsForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_PODetailsForm);
formBttn = APMOPEN_PODetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMOPEN_PODetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMOPEN_PODetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PODetails_ItemDetails_Line...", Logger.MessageType.INF);
			Control APMOPEN_PODetails_ItemDetails_Line = new Control("PODetails_ItemDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_POLN_PODTL_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,APMOPEN_PODetails_ItemDetails_Line.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_PODetailsForm);
formBttn = APMOPEN_PODetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on ChildForm_PayWhenPaidLink...", Logger.MessageType.INF);
			Control APMOPEN_ChildForm_PayWhenPaidLink = new Control("ChildForm_PayWhenPaidLink", "ID", "lnk_1007291_APMOPEN_OPENAP");
			CPCommon.AssertEqual(true,APMOPEN_ChildForm_PayWhenPaidLink.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_ChildForm_PayWhenPaidLink);
APMOPEN_ChildForm_PayWhenPaidLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PayWhenPaidForm...", Logger.MessageType.INF);
			Control APMOPEN_PayWhenPaidForm = new Control("PayWhenPaidForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_PWPDTOT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_PayWhenPaidForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PayWhenPaid_TotalsBilledAmount...", Logger.MessageType.INF);
			Control APMOPEN_PayWhenPaid_TotalsBilledAmount = new Control("PayWhenPaid_TotalsBilledAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_PWPDTOT_']/ancestor::form[1]/descendant::*[@id='BILLED_AMT']");
			CPCommon.AssertEqual(true,APMOPEN_PayWhenPaid_TotalsBilledAmount.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PayWhenPaidChildForm...", Logger.MessageType.INF);
			Control APMOPEN_PayWhenPaidChildForm = new Control("PayWhenPaidChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_PAYWPDVCHRHS_PAYWPD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMOPEN_PayWhenPaidChildForm.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExist on PayWhenPaidChildFormTable...", Logger.MessageType.INF);
			Control APMOPEN_PayWhenPaidChildFormTable = new Control("PayWhenPaidChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_PAYWPDVCHRHS_PAYWPD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMOPEN_PayWhenPaidChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_PayWhenPaidChildForm);
formBttn = APMOPEN_PayWhenPaidChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMOPEN_PayWhenPaidChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMOPEN_PayWhenPaidChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMOPEN] Perfoming VerifyExists on PayWhenPaidChildForm_TotalBillAmount...", Logger.MessageType.INF);
			Control APMOPEN_PayWhenPaidChildForm_TotalBillAmount = new Control("PayWhenPaidChildForm_TotalBillAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__APMOPEN_PAYWPDVCHRHS_PAYWPD_']/ancestor::form[1]/descendant::*[@id='BILLED_AMT']");
			CPCommon.AssertEqual(true,APMOPEN_PayWhenPaidChildForm_TotalBillAmount.Exists());

												
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_PayWhenPaidForm);
formBttn = APMOPEN_PayWhenPaidForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "APMOPEN";
							CPCommon.WaitControlDisplayed(APMOPEN_MainForm);
formBttn = APMOPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

