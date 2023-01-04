 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMCPBIL_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Customer Product Bills Processing", "xpath","//div[@class='navItem'][.='Customer Product Bills Processing']").Click();
new Control("Manage Customer Product Bills", "xpath","//div[@class='navItem'][.='Manage Customer Product Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMCPBIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMCPBIL_MainForm);
IWebElement formBttn = BLMCPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? BLMCPBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
BLMCPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Customer Product Bills", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMCPBIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCPBIL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_MainForm);
formBttn = BLMCPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMCPBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMCPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.AssertEqual(true,BLMCPBIL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on Identification_Invoice...", Logger.MessageType.INF);
			Control BLMCPBIL_Identification_Invoice = new Control("Identification_Invoice", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,BLMCPBIL_Identification_Invoice.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BLMCPBIL_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMCPBIL_MainTab);
IWebElement mTab = BLMCPBIL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Invoice Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceDetails_Customer...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceDetails_Customer = new Control("InvoiceDetails_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceDetails_Customer.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_MainTab);
mTab = BLMCPBIL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Addresses").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on Addresses_BillAddress_Address...", Logger.MessageType.INF);
			Control BLMCPBIL_Addresses_BillAddress_Address = new Control("Addresses_BillAddress_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ADDR_DC']");
			CPCommon.AssertEqual(true,BLMCPBIL_Addresses_BillAddress_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_MainTab);
mTab = BLMCPBIL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Charges").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on OtherCharges_ChargeDetails_Code...", Logger.MessageType.INF);
			Control BLMCPBIL_OtherCharges_ChargeDetails_Code = new Control("OtherCharges_ChargeDetails_Code", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OTH_CHG_CD1']");
			CPCommon.AssertEqual(true,BLMCPBIL_OtherCharges_ChargeDetails_Code.Exists());

											Driver.SessionLogger.WriteLine("INVOICE LINE DETAILS");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExist on InvoiceLineDetailsFormTable...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetailsFormTable = new Control("InvoiceLineDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CUSTPRODINVCLN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming ClickButton on InvoiceLineDetailsForm...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetailsForm = new Control("InvoiceLineDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CUSTPRODINVCLN_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceLineDetailsForm);
formBttn = BLMCPBIL_InvoiceLineDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMCPBIL_InvoiceLineDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMCPBIL_InvoiceLineDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_Line...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_Line = new Control("InvoiceLineDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CUSTPRODINVCLN_CHLD_']/ancestor::form[1]/descendant::*[@id='PROD_INVC_LN_NO']");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_Line.Exists());

											Driver.SessionLogger.WriteLine("CURRENCY LINE INFO");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_CurrencyLineInfoLink...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoLink = new Control("InvoiceLineDetails_CurrencyLineInfoLink", "ID", "lnk_1002580_BLMCPBIL_CUSTPRODINVCLN_CHLD");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoLink);
BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_CurrencyLineInfoForm...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoForm = new Control("InvoiceLineDetails_CurrencyLineInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CURRENCYLINEINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_CurrencyLineInfo_Line...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_CurrencyLineInfo_Line = new Control("InvoiceLineDetails_CurrencyLineInfo_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CURRENCYLINEINFO_']/ancestor::form[1]/descendant::*[@id='LINE']");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_CurrencyLineInfo_Line.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoForm);
formBttn = BLMCPBIL_InvoiceLineDetails_CurrencyLineInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CUSTOMS INFO");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_CustomsInfoLink...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_CustomsInfoLink = new Control("InvoiceLineDetails_CustomsInfoLink", "ID", "lnk_1004056_BLMCPBIL_CUSTPRODINVCLN_CHLD");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_CustomsInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceLineDetails_CustomsInfoLink);
BLMCPBIL_InvoiceLineDetails_CustomsInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_CustomsInfoForm...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_CustomsInfoForm = new Control("InvoiceLineDetails_CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_CustomsInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceLineDetails_CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceLineDetails_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("InvoiceLineDetails_CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceLineDetails_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceLineDetails_CustomsInfoForm);
formBttn = BLMCPBIL_InvoiceLineDetails_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("STANDARD TEXT");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on StandardTextLink...", Logger.MessageType.INF);
			Control BLMCPBIL_StandardTextLink = new Control("StandardTextLink", "ID", "lnk_1002579_BLMCPBIL_CUSTPRODINVCHDR");
			CPCommon.AssertEqual(true,BLMCPBIL_StandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_StandardTextLink);
BLMCPBIL_StandardTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on StandardTextForm...", Logger.MessageType.INF);
			Control BLMCPBIL_StandardTextForm = new Control("StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CUSTPRODINVCTXT_STD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCPBIL_StandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control BLMCPBIL_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CUSTPRODINVCTXT_STD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCPBIL_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on StandardText_Ok...", Logger.MessageType.INF);
			Control BLMCPBIL_StandardText_Ok = new Control("StandardText_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMCPBIL_CUSTPRODINVCTXT_STD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLMCPBIL_StandardText_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_StandardTextForm);
formBttn = BLMCPBIL_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EXCHANGE RATES");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMCPBIL_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1003467_BLMCPBIL_CUSTPRODINVCHDR");
			CPCommon.AssertEqual(true,BLMCPBIL_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_ExchangeRatesLink);
BLMCPBIL_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMCPBIL_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCPBIL_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control BLMCPBIL_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMCPBIL_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_ExchangeRatesForm);
formBttn = BLMCPBIL_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("INVOICE TOTALS");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceTotalsLink...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceTotalsLink = new Control("InvoiceTotalsLink", "ID", "lnk_1002582_BLMCPBIL_CUSTPRODINVCHDR");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceTotalsLink);
BLMCPBIL_InvoiceTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceTotalsForm...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceTotalsForm = new Control("InvoiceTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPBLINVT_INVCTOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCPBIL] Perfoming VerifyExists on InvoiceTotals_ExchangeRates_BillingToFunctional...", Logger.MessageType.INF);
			Control BLMCPBIL_InvoiceTotals_ExchangeRates_BillingToFunctional = new Control("InvoiceTotals_ExchangeRates_BillingToFunctional", "xpath", "//div[translate(@id,'0123456789','')='pr__CPBLINVT_INVCTOTALS_']/ancestor::form[1]/descendant::*[@id='TRANS_TO_FUNC_ER']");
			CPCommon.AssertEqual(true,BLMCPBIL_InvoiceTotals_ExchangeRates_BillingToFunctional.Exists());

												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_InvoiceTotalsForm);
formBttn = BLMCPBIL_InvoiceTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMCPBIL";
							CPCommon.WaitControlDisplayed(BLMCPBIL_MainForm);
formBttn = BLMCPBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

