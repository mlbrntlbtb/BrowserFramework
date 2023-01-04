 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMHIST_SMOKE : TestScript
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
new Control("Accounts Receivable Utilities", "xpath","//div[@class='navItem'][.='Accounts Receivable Utilities']").Click();
new Control("Manage Accounts Receivable History", "xpath","//div[@class='navItem'][.='Manage Accounts Receivable History']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARMHIST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARMHIST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on CustomerAccount...", Logger.MessageType.INF);
			Control ARMHIST_CustomerAccount = new Control("CustomerAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,ARMHIST_CustomerAccount.Exists());

											Driver.SessionLogger.WriteLine("Customer Notes");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on CustomerNotesLink...", Logger.MessageType.INF);
			Control ARMHIST_CustomerNotesLink = new Control("CustomerNotesLink", "ID", "lnk_3038_ARM_ARHDRHS_HDR");
			CPCommon.AssertEqual(true,ARMHIST_CustomerNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_CustomerNotesLink);
ARMHIST_CustomerNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on CustomerNotesForm...", Logger.MessageType.INF);
			Control ARMHIST_CustomerNotesForm = new Control("CustomerNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTNOTES_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ARMHIST_CustomerNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on CustomerNotes_CustomerNotes...", Logger.MessageType.INF);
			Control ARMHIST_CustomerNotes_CustomerNotes = new Control("CustomerNotes_CustomerNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTNOTES_DTL_']/ancestor::form[1]/descendant::*[@id='NOTES_TX']");
			CPCommon.AssertEqual(true,ARMHIST_CustomerNotes_CustomerNotes.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_CustomerNotesForm);
IWebElement formBttn = ARMHIST_CustomerNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm);
formBttn = ARMHIST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMHIST_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMHIST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ChildFormTab);
IWebElement mTab = ARMHIST_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_Details_InvoiceNumber...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_Details_InvoiceNumber = new Control("ChildForm_Details_InvoiceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_Details_InvoiceNumber.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ChildFormTab);
mTab = ARMHIST_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Amount Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_AmountDetails_FuncDiscountAmount...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_AmountDetails_FuncDiscountAmount = new Control("ChildForm_AmountDetails_FuncDiscountAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='DISC_AMT']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_AmountDetails_FuncDiscountAmount.Exists());

											Driver.SessionLogger.WriteLine("Contacts");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_ContactsLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ContactsLink = new Control("ChildForm_ContactsLink", "ID", "lnk_2500_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_ContactsLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ContactsLink);
ARMHIST_ChildForm_ContactsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildForm_ContactsFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ContactsFormTable = new Control("ChildForm_ContactsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTADDRCNTACT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_ContactsFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming ClickButton on ChildForm_ContactsForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ContactsForm = new Control("ChildForm_ContactsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTADDRCNTACT_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ContactsForm);
formBttn = ARMHIST_ChildForm_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMHIST_ChildForm_ContactsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMHIST_ChildForm_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.AssertEqual(true,ARMHIST_ChildForm_ContactsForm.Exists());

													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_Contacts_ContactID...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_Contacts_ContactID = new Control("ChildForm_Contacts_ContactID", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTADDRCNTACT_DTL_']/ancestor::form[1]/descendant::*[@id='CNTACT_ID']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_Contacts_ContactID.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ContactsForm);
formBttn = ARMHIST_ChildForm_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_2508_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ExchangeRatesLink);
ARMHIST_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_ExchangeRatesForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ExchangeRatesForm = new Control("ChildForm_ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_ExchangeRates_TransactionCurrency = new Control("ChildForm_ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_ExchangeRatesForm);
formBttn = ARMHIST_ChildForm_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Invoice Detail");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_InvoiceDetailLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceDetailLink = new Control("ChildForm_InvoiceDetailLink", "ID", "lnk_3005_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_InvoiceDetailLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_InvoiceDetailLink);
ARMHIST_ChildForm_InvoiceDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildForm_InvoiceDetailFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceDetailFormTable = new Control("ChildForm_InvoiceDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARDETLHS_INVCDTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_InvoiceDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming ClickButton on ChildForm_InvoiceDetailForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceDetailForm = new Control("ChildForm_InvoiceDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARDETLHS_INVCDTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_InvoiceDetailForm);
formBttn = ARMHIST_ChildForm_InvoiceDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMHIST_ChildForm_InvoiceDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMHIST_ChildForm_InvoiceDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.AssertEqual(true,ARMHIST_ChildForm_InvoiceDetailForm.Exists());

													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_InvoiceDetail_FiscalYear...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceDetail_FiscalYear = new Control("ChildForm_InvoiceDetail_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARDETLHS_INVCDTL_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_InvoiceDetail_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_InvoiceDetailForm);
formBttn = ARMHIST_ChildForm_InvoiceDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Invoice Notes");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_InvoiceNotesLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceNotesLink = new Control("ChildForm_InvoiceNotesLink", "ID", "lnk_2513_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_InvoiceNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_InvoiceNotesLink);
ARMHIST_ChildForm_InvoiceNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildForm_InvoiceNotesFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceNotesFormTable = new Control("ChildForm_InvoiceNotesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARNOTESHS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_InvoiceNotesFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming Close on ChildForm_InvoiceNotesForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_InvoiceNotesForm = new Control("ChildForm_InvoiceNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARNOTESHS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_InvoiceNotesForm);
formBttn = ARMHIST_ChildForm_InvoiceNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Sales Tax");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_SalesTaxLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_SalesTaxLink = new Control("ChildForm_SalesTaxLink", "ID", "lnk_2514_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_SalesTaxLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_SalesTaxLink);
ARMHIST_ChildForm_SalesTaxLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildForm_SalesTaxFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_SalesTaxFormTable = new Control("ChildForm_SalesTaxFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARSALESTAX_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_SalesTaxFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming Close on ChildForm_SalesTaxForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_SalesTaxForm = new Control("ChildForm_SalesTaxForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARSALESTAX_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_SalesTaxForm);
formBttn = ARMHIST_ChildForm_SalesTaxForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Finance Charges Computed");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_FinanceChargesComputedLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesComputedLink = new Control("ChildForm_FinanceChargesComputedLink", "ID", "lnk_3297_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesComputedLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_FinanceChargesComputedLink);
ARMHIST_ChildForm_FinanceChargesComputedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildForm_FinanceChargesComputedFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesComputedFormTable = new Control("ChildForm_FinanceChargesComputedFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_COMPUTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesComputedFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming ClickButton on ChildForm_FinanceChargesComputedForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesComputedForm = new Control("ChildForm_FinanceChargesComputedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_COMPUTED_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_FinanceChargesComputedForm);
formBttn = ARMHIST_ChildForm_FinanceChargesComputedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMHIST_ChildForm_FinanceChargesComputedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMHIST_ChildForm_FinanceChargesComputedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesComputedForm.Exists());

													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_FinanceChargesComputed_FinanceChargeAmount...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesComputed_FinanceChargeAmount = new Control("ChildForm_FinanceChargesComputed_FinanceChargeAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_COMPUTED_']/ancestor::form[1]/descendant::*[@id='TRN_FINCHG_NET_AMT']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesComputed_FinanceChargeAmount.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_FinanceChargesComputedForm);
formBttn = ARMHIST_ChildForm_FinanceChargesComputedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Finance Charges Received");


												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_FinanceChargesReceivedLink...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesReceivedLink = new Control("ChildForm_FinanceChargesReceivedLink", "ID", "lnk_3298_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesReceivedLink.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_FinanceChargesReceivedLink);
ARMHIST_ChildForm_FinanceChargesReceivedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExist on ChildForm_FinanceChargesReceivedFormTable...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesReceivedFormTable = new Control("ChildForm_FinanceChargesReceivedFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_RECEIVED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesReceivedFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming ClickButton on ChildForm_FinanceChargesReceivedForm...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesReceivedForm = new Control("ChildForm_FinanceChargesReceivedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_RECEIVED_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_FinanceChargesReceivedForm);
formBttn = ARMHIST_ChildForm_FinanceChargesReceivedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMHIST_ChildForm_FinanceChargesReceivedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMHIST_ChildForm_FinanceChargesReceivedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesReceivedForm.Exists());

													
				CPCommon.CurrentComponent = "ARMHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMHIST] Perfoming VerifyExists on ChildForm_FinanceChargesReceived_FinanceChargeAmount...", Logger.MessageType.INF);
			Control ARMHIST_ChildForm_FinanceChargesReceived_FinanceChargeAmount = new Control("ChildForm_FinanceChargesReceived_FinanceChargeAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_RECEIVED_']/ancestor::form[1]/descendant::*[@id='TRN_FINCHG_RCV_AMT']");
			CPCommon.AssertEqual(true,ARMHIST_ChildForm_FinanceChargesReceived_FinanceChargeAmount.Exists());

												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_ChildForm_FinanceChargesReceivedForm);
formBttn = ARMHIST_ChildForm_FinanceChargesReceivedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ARMHIST";
							CPCommon.WaitControlDisplayed(ARMHIST_MainForm);
formBttn = ARMHIST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

