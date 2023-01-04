 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMCR_SMOKE : TestScript
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
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Cash Receipts Processing", "xpath","//div[@class='navItem'][.='Cash Receipts Processing']").Click();
new Control("Manage Cash Receipts", "xpath","//div[@class='navItem'][.='Manage Cash Receipts']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on Identification_Receipt_Date...", Logger.MessageType.INF);
			Control ARMCR_Identification_Receipt_Date = new Control("Identification_Receipt_Date", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RECPT_DT']");
			CPCommon.AssertEqual(true,ARMCR_Identification_Receipt_Date.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control ARMCR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ARMCR_MainForm);
IWebElement formBttn = ARMCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ARMCR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ARMCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control ARMCR_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCR_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Invoice Information");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Click on Identification_InvoiceInformationLink...", Logger.MessageType.INF);
			Control ARMCR_Identification_InvoiceInformationLink = new Control("Identification_InvoiceInformationLink", "ID", "lnk_1001886_ARMCR_CASH_RECPT_HDR");
			CPCommon.WaitControlDisplayed(ARMCR_Identification_InvoiceInformationLink);
ARMCR_Identification_InvoiceInformationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on InvoiceInformation_CashAccount...", Logger.MessageType.INF);
			Control ARMCR_InvoiceInformation_CashAccount = new Control("InvoiceInformation_CashAccount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_INVOICE_HDR_']/ancestor::form[1]/descendant::*[@id='TRNSF_ACCT_DC']");
			CPCommon.AssertEqual(true,ARMCR_InvoiceInformation_CashAccount.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExist on InvoiceInformationDetail_Table...", Logger.MessageType.INF);
			Control ARMCR_InvoiceInformationDetail_Table = new Control("InvoiceInformationDetail_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_INVOICE_TRN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCR_InvoiceInformationDetail_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming ClickButton on InvoiceInformationDetailForm...", Logger.MessageType.INF);
			Control ARMCR_InvoiceInformationDetailForm = new Control("InvoiceInformationDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_INVOICE_TRN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCR_InvoiceInformationDetailForm);
formBttn = ARMCR_InvoiceInformationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? ARMCR_InvoiceInformationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
ARMCR_InvoiceInformationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "ARMCR";
							CPCommon.WaitControlDisplayed(ARMCR_InvoiceInformationDetailForm);
formBttn = ARMCR_InvoiceInformationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCR_InvoiceInformationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCR_InvoiceInformationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on InvoiceInformationDetail_Invoice...", Logger.MessageType.INF);
			Control ARMCR_InvoiceInformationDetail_Invoice = new Control("InvoiceInformationDetail_Invoice", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_INVOICE_TRN_']/ancestor::form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,ARMCR_InvoiceInformationDetail_Invoice.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Select on InvoiceInformationDetail_Tab...", Logger.MessageType.INF);
			Control ARMCR_InvoiceInformationDetail_Tab = new Control("InvoiceInformationDetail_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_INVOICE_TRN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ARMCR_InvoiceInformationDetail_Tab);
IWebElement mTab = ARMCR_InvoiceInformationDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Charge Distribution").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ARMCR";
							CPCommon.WaitControlDisplayed(ARMCR_InvoiceInformationDetail_Tab);
mTab = ARMCR_InvoiceInformationDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Functional Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Close on InvoiceInformationForm...", Logger.MessageType.INF);
			Control ARMCR_InvoiceInformationForm = new Control("InvoiceInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_INVOICE_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCR_InvoiceInformationForm);
formBttn = ARMCR_InvoiceInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Transfers");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Click on Identification_TransfersLink...", Logger.MessageType.INF);
			Control ARMCR_Identification_TransfersLink = new Control("Identification_TransfersLink", "ID", "lnk_1001888_ARMCR_CASH_RECPT_HDR");
			CPCommon.WaitControlDisplayed(ARMCR_Identification_TransfersLink);
ARMCR_Identification_TransfersLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on Transfers_TransferAmount...", Logger.MessageType.INF);
			Control ARMCR_Transfers_TransferAmount = new Control("Transfers_TransferAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_TRANSFER_']/ancestor::form[1]/descendant::*[@id='PAY_TRN_AMT']");
			CPCommon.AssertEqual(true,ARMCR_Transfers_TransferAmount.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Click on Identification_ExchangeRatesLink...", Logger.MessageType.INF);
			Control ARMCR_Identification_ExchangeRatesLink = new Control("Identification_ExchangeRatesLink", "ID", "lnk_1003297_ARMCR_CASH_RECPT_HDR");
			CPCommon.WaitControlDisplayed(ARMCR_Identification_ExchangeRatesLink);
ARMCR_Identification_ExchangeRatesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on ExchangeRates_PayCurrency...", Logger.MessageType.INF);
			Control ARMCR_ExchangeRates_PayCurrency = new Control("ExchangeRates_PayCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,ARMCR_ExchangeRates_PayCurrency.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Click on ExchangeRates_Apply...", Logger.MessageType.INF);
			Control ARMCR_ExchangeRates_Apply = new Control("ExchangeRates_Apply", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(ARMCR_ExchangeRates_Apply);
if (ARMCR_ExchangeRates_Apply.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
ARMCR_ExchangeRates_Apply.Click(5,5);
else ARMCR_ExchangeRates_Apply.Click(4.5);


											Driver.SessionLogger.WriteLine("Cash Receipt Details");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control ARMCR_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCR_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming ClickButton on CashReceiptDetailForm...", Logger.MessageType.INF);
			Control ARMCR_CashReceiptDetailForm = new Control("CashReceiptDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCR_CashReceiptDetailForm);
formBttn = ARMCR_CashReceiptDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCR_CashReceiptDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCR_CashReceiptDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on CashReceiptDetail_Line...", Logger.MessageType.INF);
			Control ARMCR_CashReceiptDetail_Line = new Control("CashReceiptDetail_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]/descendant::*[@id='LN_NO']");
			CPCommon.AssertEqual(true,ARMCR_CashReceiptDetail_Line.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming Select on CashReceiptDetail_Tab...", Logger.MessageType.INF);
			Control ARMCR_CashReceiptDetail_Tab = new Control("CashReceiptDetail_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ARMCR_CashReceiptDetail_Tab);
mTab = ARMCR_CashReceiptDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Charge Distribution").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on CashReceiptDetail_ChargeDistribution_Account...", Logger.MessageType.INF);
			Control ARMCR_CashReceiptDetail_ChargeDistribution_Account = new Control("CashReceiptDetail_ChargeDistribution_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,ARMCR_CashReceiptDetail_ChargeDistribution_Account.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
							CPCommon.WaitControlDisplayed(ARMCR_CashReceiptDetail_Tab);
mTab = ARMCR_CashReceiptDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Miscellaneous").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on CashReceiptDetail_Miscellaneous_Check...", Logger.MessageType.INF);
			Control ARMCR_CashReceiptDetail_Miscellaneous_Check = new Control("CashReceiptDetail_Miscellaneous_Check", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]/descendant::*[@id='CHK_NO']");
			CPCommon.AssertEqual(true,ARMCR_CashReceiptDetail_Miscellaneous_Check.Exists());

												
				CPCommon.CurrentComponent = "ARMCR";
							CPCommon.WaitControlDisplayed(ARMCR_CashReceiptDetail_Tab);
mTab = ARMCR_CashReceiptDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Functional Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCR] Perfoming VerifyExists on CashReceiptDetail_FunctionalAmounts_FuncAmountRecvd...", Logger.MessageType.INF);
			Control ARMCR_CashReceiptDetail_FunctionalAmounts_FuncAmountRecvd = new Control("CashReceiptDetail_FunctionalAmounts_FuncAmountRecvd", "xpath", "//div[translate(@id,'0123456789','')='pr__ARMCR_CASH_RECPT_TRN_']/ancestor::form[1]/descendant::*[@id='TRN_AMT']");
			CPCommon.AssertEqual(true,ARMCR_CashReceiptDetail_FunctionalAmounts_FuncAmountRecvd.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ARMCR";
							CPCommon.WaitControlDisplayed(ARMCR_MainForm);
formBttn = ARMCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

