 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMOREC_SMOKE : TestScript
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


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARMOREC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARMOREC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on MainForm_CustomerAccount...", Logger.MessageType.INF);
			Control ARMOREC_MainForm_CustomerAccount = new Control("MainForm_CustomerAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,ARMOREC_MainForm_CustomerAccount.Exists());

											Driver.SessionLogger.WriteLine("Customer Notes");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on MainForm_CustomerNotesLink...", Logger.MessageType.INF);
			Control ARMOREC_MainForm_CustomerNotesLink = new Control("MainForm_CustomerNotesLink", "ID", "lnk_3038_ARM_ARHDRHS_HDR");
			CPCommon.AssertEqual(true,ARMOREC_MainForm_CustomerNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_MainForm_CustomerNotesLink);
ARMOREC_MainForm_CustomerNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on CustomerNotesForm...", Logger.MessageType.INF);
			Control ARMOREC_CustomerNotesForm = new Control("CustomerNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTNOTES_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ARMOREC_CustomerNotesForm.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on CustomerNotes_NotesTextarea...", Logger.MessageType.INF);
			Control ARMOREC_CustomerNotes_NotesTextarea = new Control("CustomerNotes_NotesTextarea", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTNOTES_DTL_']/ancestor::form[1]/descendant::*[@id='NOTES_TX']");
			CPCommon.AssertEqual(true,ARMOREC_CustomerNotes_NotesTextarea.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_CustomerNotesForm);
IWebElement formBttn = ARMOREC_CustomerNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_ChildForm);
formBttn = ARMOREC_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMOREC_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMOREC_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming Select on ChildForm_Tab...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_Tab = new Control("ChildForm_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_Tab);
IWebElement mTab = ARMOREC_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_Details_InvoiceNumber...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_Details_InvoiceNumber = new Control("ChildForm_Details_InvoiceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_Details_InvoiceNumber.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_Tab);
mTab = ARMOREC_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Amount Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_AmountDetails_FuncDiscountAmount...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_AmountDetails_FuncDiscountAmount = new Control("ChildForm_AmountDetails_FuncDiscountAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARHDRHS_DTL_']/ancestor::form[1]/descendant::*[@id='DISC_AMT']");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_AmountDetails_FuncDiscountAmount.Exists());

											Driver.SessionLogger.WriteLine("Contacts");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_ContactsLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_ContactsLink = new Control("ChildForm_ContactsLink", "ID", "lnk_2500_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_ContactsLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_ContactsLink);
ARMOREC_ChildForm_ContactsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on Contacts_Table...", Logger.MessageType.INF);
			Control ARMOREC_Contacts_Table = new Control("Contacts_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTADDRCNTACT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_Contacts_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming ClickButton on ContactsForm...", Logger.MessageType.INF);
			Control ARMOREC_ContactsForm = new Control("ContactsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTADDRCNTACT_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_ContactsForm);
formBttn = ARMOREC_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMOREC_ContactsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMOREC_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.AssertEqual(true,ARMOREC_ContactsForm.Exists());

													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on Contacts_ContactID...", Logger.MessageType.INF);
			Control ARMOREC_Contacts_ContactID = new Control("Contacts_ContactID", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_CUSTADDRCNTACT_DTL_']/ancestor::form[1]/descendant::*[@id='CNTACT_ID']");
			CPCommon.AssertEqual(true,ARMOREC_Contacts_ContactID.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ContactsForm);
formBttn = ARMOREC_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_2508_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_ExchangeRatesLink);
ARMOREC_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control ARMOREC_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='ph__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ARMOREC_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control ARMOREC_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='ph__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,ARMOREC_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ExchangeRatesForm);
formBttn = ARMOREC_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Invoice Detail");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_InvoiceDetailLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_InvoiceDetailLink = new Control("ChildForm_InvoiceDetailLink", "ID", "lnk_3005_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_InvoiceDetailLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_InvoiceDetailLink);
ARMOREC_ChildForm_InvoiceDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on InvoiceDetail_Table...", Logger.MessageType.INF);
			Control ARMOREC_InvoiceDetail_Table = new Control("InvoiceDetail_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARDETLHS_INVCDTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_InvoiceDetail_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming ClickButton on InvoiceDetailForm...", Logger.MessageType.INF);
			Control ARMOREC_InvoiceDetailForm = new Control("InvoiceDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARDETLHS_INVCDTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_InvoiceDetailForm);
formBttn = ARMOREC_InvoiceDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMOREC_InvoiceDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMOREC_InvoiceDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.AssertEqual(true,ARMOREC_InvoiceDetailForm.Exists());

													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on InvoiceDetail_FiscalYear...", Logger.MessageType.INF);
			Control ARMOREC_InvoiceDetail_FiscalYear = new Control("InvoiceDetail_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARDETLHS_INVCDTL_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,ARMOREC_InvoiceDetail_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_InvoiceDetailForm);
formBttn = ARMOREC_InvoiceDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Invoice Notes");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_InvoiceNotesLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_InvoiceNotesLink = new Control("ChildForm_InvoiceNotesLink", "ID", "lnk_2513_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_InvoiceNotesLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_InvoiceNotesLink);
ARMOREC_ChildForm_InvoiceNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on InvoiceNotes_Table...", Logger.MessageType.INF);
			Control ARMOREC_InvoiceNotes_Table = new Control("InvoiceNotes_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARNOTESHS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_InvoiceNotes_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming Close on InvoiceNotesForm...", Logger.MessageType.INF);
			Control ARMOREC_InvoiceNotesForm = new Control("InvoiceNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARNOTESHS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_InvoiceNotesForm);
formBttn = ARMOREC_InvoiceNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Sales Tax");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_SalesTaxLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_SalesTaxLink = new Control("ChildForm_SalesTaxLink", "ID", "lnk_2514_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_SalesTaxLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_SalesTaxLink);
ARMOREC_ChildForm_SalesTaxLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on SalesTax_Table...", Logger.MessageType.INF);
			Control ARMOREC_SalesTax_Table = new Control("SalesTax_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARSALESTAX_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_SalesTax_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming Close on SalesTaxForm...", Logger.MessageType.INF);
			Control ARMOREC_SalesTaxForm = new Control("SalesTaxForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_ARSALESTAX_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_SalesTaxForm);
formBttn = ARMOREC_SalesTaxForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Finance Charges Computed");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_FinanceChargesComputedLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_FinanceChargesComputedLink = new Control("ChildForm_FinanceChargesComputedLink", "ID", "lnk_3297_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_FinanceChargesComputedLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_FinanceChargesComputedLink);
ARMOREC_ChildForm_FinanceChargesComputedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on FinanceChargesComputed_Table...", Logger.MessageType.INF);
			Control ARMOREC_FinanceChargesComputed_Table = new Control("FinanceChargesComputed_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_COMPUTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_FinanceChargesComputed_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming ClickButton on FinanceChargesComputedForm...", Logger.MessageType.INF);
			Control ARMOREC_FinanceChargesComputedForm = new Control("FinanceChargesComputedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_COMPUTED_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_FinanceChargesComputedForm);
formBttn = ARMOREC_FinanceChargesComputedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMOREC_FinanceChargesComputedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMOREC_FinanceChargesComputedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.AssertEqual(true,ARMOREC_FinanceChargesComputedForm.Exists());

													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on FinanceChargesComputed_FinanceChargeAmount...", Logger.MessageType.INF);
			Control ARMOREC_FinanceChargesComputed_FinanceChargeAmount = new Control("FinanceChargesComputed_FinanceChargeAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_COMPUTED_']/ancestor::form[1]/descendant::*[@id='TRN_FINCHG_NET_AMT']");
			CPCommon.AssertEqual(true,ARMOREC_FinanceChargesComputed_FinanceChargeAmount.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_FinanceChargesComputedForm);
formBttn = ARMOREC_FinanceChargesComputedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Finance Charges Received");


												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on ChildForm_FinanceChargesReceivedLink...", Logger.MessageType.INF);
			Control ARMOREC_ChildForm_FinanceChargesReceivedLink = new Control("ChildForm_FinanceChargesReceivedLink", "ID", "lnk_3298_ARHISTCOM_ARHDRHS_DTL");
			CPCommon.AssertEqual(true,ARMOREC_ChildForm_FinanceChargesReceivedLink.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_ChildForm_FinanceChargesReceivedLink);
ARMOREC_ChildForm_FinanceChargesReceivedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExist on FinanceChargesReceived_Table...", Logger.MessageType.INF);
			Control ARMOREC_FinanceChargesReceived_Table = new Control("FinanceChargesReceived_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_RECEIVED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMOREC_FinanceChargesReceived_Table.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming ClickButton on FinanceChargesReceivedForm...", Logger.MessageType.INF);
			Control ARMOREC_FinanceChargesReceivedForm = new Control("FinanceChargesReceivedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_RECEIVED_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMOREC_FinanceChargesReceivedForm);
formBttn = ARMOREC_FinanceChargesReceivedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMOREC_FinanceChargesReceivedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMOREC_FinanceChargesReceivedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.AssertEqual(true,ARMOREC_FinanceChargesReceivedForm.Exists());

													
				CPCommon.CurrentComponent = "ARMOREC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMOREC] Perfoming VerifyExists on FinanceChargesReceived_FinanceChargeAmount...", Logger.MessageType.INF);
			Control ARMOREC_FinanceChargesReceived_FinanceChargeAmount = new Control("FinanceChargesReceived_FinanceChargeAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__ARHISTCOM_FINCHARGES_RECEIVED_']/ancestor::form[1]/descendant::*[@id='TRN_FINCHG_RCV_AMT']");
			CPCommon.AssertEqual(true,ARMOREC_FinanceChargesReceived_FinanceChargeAmount.Exists());

												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_FinanceChargesReceivedForm);
formBttn = ARMOREC_FinanceChargesReceivedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ARMOREC";
							CPCommon.WaitControlDisplayed(ARMOREC_MainForm);
formBttn = ARMOREC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

