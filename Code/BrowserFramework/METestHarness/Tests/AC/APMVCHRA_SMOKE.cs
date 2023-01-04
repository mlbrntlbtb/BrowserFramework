 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMVCHRA_SMOKE : TestScript
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
new Control("Approve Vouchers", "xpath","//div[@class='navItem'][.='Approve Vouchers']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMVCHRA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMVCHRA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on Identification_Approver...", Logger.MessageType.INF);
			Control APMVCHRA_Identification_Approver = new Control("Identification_Approver", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='USER_ID']");
			CPCommon.AssertEqual(true,APMVCHRA_Identification_Approver.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_MainForm);
IWebElement formBttn = APMVCHRA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMVCHRA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMVCHRA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMVCHRA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVCHRA_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHR_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control APMVCHRA_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHR_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVCHRA_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ChildForm);
formBttn = APMVCHRA_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVCHRA_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVCHRA_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming Select on ChildFormTab...", Logger.MessageType.INF);
			Control APMVCHRA_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHR_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(APMVCHRA_ChildFormTab);
IWebElement mTab = APMVCHRA_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Voucher Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm_VoucherHeader_Vendor...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm_VoucherHeader_Vendor = new Control("ChildForm_VoucherHeader_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHR_DTL_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm_VoucherHeader_Vendor.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ChildFormTab);
mTab = APMVCHRA_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manual Check").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm_ManualCheck_CashAccountsDescription...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm_ManualCheck_CashAccountsDescription = new Control("ChildForm_ManualCheck_CashAccountsDescription", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHR_DTL_']/ancestor::form[1]/descendant::*[@id='CASH_ACCTS_DESC']");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm_ManualCheck_CashAccountsDescription.Exists());

											Driver.SessionLogger.WriteLine("Voucher Detail Form");


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm_VoucherDetailLink...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm_VoucherDetailLink = new Control("ChildForm_VoucherDetailLink", "ID", "lnk_1003043_APMVCHRA_VCHR_DTL");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm_VoucherDetailLink.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ChildForm_VoucherDetailLink);
APMVCHRA_ChildForm_VoucherDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on VoucherDetailForm...", Logger.MessageType.INF);
			Control APMVCHRA_VoucherDetailForm = new Control("VoucherDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_LN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVCHRA_VoucherDetailForm.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExist on VoucherDetailTable...", Logger.MessageType.INF);
			Control APMVCHRA_VoucherDetailTable = new Control("VoucherDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_LN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVCHRA_VoucherDetailTable.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_VoucherDetailForm);
formBttn = APMVCHRA_VoucherDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVCHRA_VoucherDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVCHRA_VoucherDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on VoucherDetail_VchrLine...", Logger.MessageType.INF);
			Control APMVCHRA_VoucherDetail_VchrLine = new Control("VoucherDetail_VchrLine", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_LN_']/ancestor::form[1]/descendant::*[@id='VCHR_LN_NO']");
			CPCommon.AssertEqual(true,APMVCHRA_VoucherDetail_VchrLine.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on VoucherDetail_CurrencyLink...", Logger.MessageType.INF);
			Control APMVCHRA_VoucherDetail_CurrencyLink = new Control("VoucherDetail_CurrencyLink", "ID", "lnk_1003046_APMVCHRA_LN");
			CPCommon.AssertEqual(true,APMVCHRA_VoucherDetail_CurrencyLink.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_VoucherDetailForm);
formBttn = APMVCHRA_VoucherDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PO Voucher Lines Form");


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm_POVoucherLinesLink...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm_POVoucherLinesLink = new Control("ChildForm_POVoucherLinesLink", "ID", "lnk_1003047_APMVCHRA_VCHR_DTL");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm_POVoucherLinesLink.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ChildForm_POVoucherLinesLink);
APMVCHRA_ChildForm_POVoucherLinesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on POVoucherLinesForm...", Logger.MessageType.INF);
			Control APMVCHRA_POVoucherLinesForm = new Control("POVoucherLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHRLNPOLN_SUBTSK_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVCHRA_POVoucherLinesForm.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExist on POVoucherLinesTable...", Logger.MessageType.INF);
			Control APMVCHRA_POVoucherLinesTable = new Control("POVoucherLinesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHRLNPOLN_SUBTSK_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVCHRA_POVoucherLinesTable.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_POVoucherLinesForm);
formBttn = APMVCHRA_POVoucherLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVCHRA_POVoucherLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVCHRA_POVoucherLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on POVoucherLines_VchrLine...", Logger.MessageType.INF);
			Control APMVCHRA_POVoucherLines_VchrLine = new Control("POVoucherLines_VchrLine", "xpath", "//div[translate(@id,'0123456789','')='pr__APMVCHRA_VCHRLNPOLN_SUBTSK_DTL_']/ancestor::form[1]/descendant::*[@id='VCHR_LN_NO']");
			CPCommon.AssertEqual(true,APMVCHRA_POVoucherLines_VchrLine.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_POVoucherLinesForm);
formBttn = APMVCHRA_POVoucherLinesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Exchange Rates Form");


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_1003235_APMVCHRA_VCHR_DTL");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ChildForm_ExchangeRatesLink);
APMVCHRA_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control APMVCHRA_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVCHRA_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ExchangeRates_Currencies_TransactionCurrency...", Logger.MessageType.INF);
			Control APMVCHRA_ExchangeRates_Currencies_TransactionCurrency = new Control("ExchangeRates_Currencies_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,APMVCHRA_ExchangeRates_Currencies_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ExchangeRatesForm);
formBttn = APMVCHRA_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMVCHRA_ExchangeRatesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMVCHRA_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExist on ExchangeRatesFormTable...", Logger.MessageType.INF);
			Control APMVCHRA_ExchangeRatesFormTable = new Control("ExchangeRatesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVCHRA_ExchangeRatesFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ExchangeRatesForm);
formBttn = APMVCHRA_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Voucher Totals Form");


												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on ChildForm_VoucherTotalsLink...", Logger.MessageType.INF);
			Control APMVCHRA_ChildForm_VoucherTotalsLink = new Control("ChildForm_VoucherTotalsLink", "ID", "lnk_1007220_APMVCHRA_VCHR_DTL");
			CPCommon.AssertEqual(true,APMVCHRA_ChildForm_VoucherTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_ChildForm_VoucherTotalsLink);
APMVCHRA_ChildForm_VoucherTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on VoucherTotalsForm...", Logger.MessageType.INF);
			Control APMVCHRA_VoucherTotalsForm = new Control("VoucherTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVTOT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVCHRA_VoucherTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVCHRA] Perfoming VerifyExists on VoucherTotals_CostAmount...", Logger.MessageType.INF);
			Control APMVCHRA_VoucherTotals_CostAmount = new Control("VoucherTotals_CostAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVTOT_']/ancestor::form[1]/descendant::*[@id='TRN_CST_AMT']");
			CPCommon.AssertEqual(true,APMVCHRA_VoucherTotals_CostAmount.Exists());

												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_VoucherTotalsForm);
formBttn = APMVCHRA_VoucherTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APMVCHRA";
							CPCommon.WaitControlDisplayed(APMVCHRA_MainForm);
formBttn = APMVCHRA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

