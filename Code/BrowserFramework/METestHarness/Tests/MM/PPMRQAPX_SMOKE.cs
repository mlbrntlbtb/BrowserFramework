 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQAPX_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Purchase Requisitions", "xpath","//div[@class='navItem'][.='Purchase Requisitions']").Click();
new Control("Approve Purchase Requisitions", "xpath","//div[@class='navItem'][.='Approve Purchase Requisitions']").Click();


											Driver.SessionLogger.WriteLine("Main");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRQAPX_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PPMRQAPX_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPX_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_MainForm);
IWebElement formBttn = PPMRQAPX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on Identification_Requisition...", Logger.MessageType.INF);
			Control PPMRQAPX_Identification_Requisition = new Control("Identification_Requisition", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_ID']");
			CPCommon.AssertEqual(true,PPMRQAPX_Identification_Requisition.Exists());

											Driver.SessionLogger.WriteLine("Requisition Header Documents Link");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on RequisitionHeaderDocumentsLink...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionHeaderDocumentsLink = new Control("RequisitionHeaderDocumentsLink", "ID", "lnk_3232_PPMRQAPX_RQHDR_HDR");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionHeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_RequisitionHeaderDocumentsLink);
PPMRQAPX_RequisitionHeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on RequisitionHeaderDocumentsForm...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionHeaderDocumentsForm = new Control("RequisitionHeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQHDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionHeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExist on RequisitionHeaderDocumentsTable...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionHeaderDocumentsTable = new Control("RequisitionHeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQHDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionHeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_RequisitionHeaderDocumentsForm);
formBttn = PPMRQAPX_RequisitionHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_RequisitionHeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_RequisitionHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on RequisitionHeaderDocuments_Document...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionHeaderDocuments_Document = new Control("RequisitionHeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQHDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionHeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_RequisitionHeaderDocumentsForm);
formBttn = PPMRQAPX_RequisitionHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Approvals");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on ApprovalsLink...", Logger.MessageType.INF);
			Control PPMRQAPX_ApprovalsLink = new Control("ApprovalsLink", "ID", "lnk_1007074_PPMRQAPX_RQHDR_HDR");
			CPCommon.AssertEqual(true,PPMRQAPX_ApprovalsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_ApprovalsLink);
PPMRQAPX_ApprovalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on ApprovalsForm...", Logger.MessageType.INF);
			Control PPMRQAPX_ApprovalsForm = new Control("ApprovalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQHDRAPPRVL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_ApprovalsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExist on ApprovalsTable...", Logger.MessageType.INF);
			Control PPMRQAPX_ApprovalsTable = new Control("ApprovalsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQHDRAPPRVL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPX_ApprovalsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_ApprovalsForm);
formBttn = PPMRQAPX_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_ApprovalsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on Approvals_ApprovalRevision...", Logger.MessageType.INF);
			Control PPMRQAPX_Approvals_ApprovalRevision = new Control("Approvals_ApprovalRevision", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQHDRAPPRVL_']/ancestor::form[1]/descendant::*[@id='RQ_LN_RVSN_NO']");
			CPCommon.AssertEqual(true,PPMRQAPX_Approvals_ApprovalRevision.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_ApprovalsForm);
formBttn = PPMRQAPX_ApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Accounts");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on AccountsLink...", Logger.MessageType.INF);
			Control PPMRQAPX_AccountsLink = new Control("AccountsLink", "ID", "lnk_1007087_PPMRQAPX_RQHDR_HDR");
			CPCommon.AssertEqual(true,PPMRQAPX_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_AccountsLink);
PPMRQAPX_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on AccountsForm...", Logger.MessageType.INF);
			Control PPMRQAPX_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLNACCT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_AccountsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExist on AccountsTable...", Logger.MessageType.INF);
			Control PPMRQAPX_AccountsTable = new Control("AccountsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLNACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPX_AccountsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_AccountsForm);
formBttn = PPMRQAPX_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_AccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on Accounts_Project...", Logger.MessageType.INF);
			Control PPMRQAPX_Accounts_Project = new Control("Accounts_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLNACCT_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PPMRQAPX_Accounts_Project.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_AccountsForm);
formBttn = PPMRQAPX_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control PPMRQAPX_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1006016_PPMRQAPX_RQHDR_HDR");
			CPCommon.AssertEqual(true,PPMRQAPX_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_ExchangeRatesLink);
PPMRQAPX_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control PPMRQAPX_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control PPMRQAPX_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,PPMRQAPX_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_ExchangeRatesForm);
formBttn = PPMRQAPX_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Totals");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TotalsLink...", Logger.MessageType.INF);
			Control PPMRQAPX_TotalsLink = new Control("TotalsLink", "ID", "lnk_3279_PPMRQAPX_RQHDR_HDR");
			CPCommon.AssertEqual(true,PPMRQAPX_TotalsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TotalsLink);
PPMRQAPX_TotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TotalsForm...", Logger.MessageType.INF);
			Control PPMRQAPX_TotalsForm = new Control("TotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQ_TOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_TotalsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on Totals_Totals_Currency...", Logger.MessageType.INF);
			Control PPMRQAPX_Totals_Totals_Currency = new Control("Totals_Totals_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQ_TOTALS_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,PPMRQAPX_Totals_Totals_Currency.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TotalsForm);
formBttn = PPMRQAPX_TotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Accounts");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindowForm...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindowForm = new Control("TableWindowForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindowForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExist on TableWindowTable...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindowTable = new Control("TableWindowTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindowTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindowForm);
formBttn = PPMRQAPX_TableWindowForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_TableWindowForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_TableWindowForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_Line...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_Line = new Control("TableWindow_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]/descendant::*[@id='RQ_LN_NO']");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_Line.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming Select on TableWindowTab...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindowTab = new Control("TableWindowTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindowTab);
IWebElement mTab = PPMRQAPX_TableWindowTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_BasicInformation_Item...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_BasicInformation_Item = new Control("TableWindow_BasicInformation_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_BasicInformation_Item.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindowTab);
mTab = PPMRQAPX_TableWindowTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purchasing Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_PurchasingInformation_AlternatePartNumbers_PreferredVendor...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_PurchasingInformation_AlternatePartNumbers_PreferredVendor = new Control("TableWindow_PurchasingInformation_AlternatePartNumbers_PreferredVendor", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]/descendant::*[@id='PREF_VEND_ID']");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_PurchasingInformation_AlternatePartNumbers_PreferredVendor.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindowTab);
mTab = PPMRQAPX_TableWindowTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping & Receiving").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_ShippingReceiving_Quality_SourceInspectionRequired...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_ShippingReceiving_Quality_SourceInspectionRequired = new Control("TableWindow_ShippingReceiving_Quality_SourceInspectionRequired", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_CHILD_']/ancestor::form[1]/descendant::*[@id='SRCE_INSP_FL']");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_ShippingReceiving_Quality_SourceInspectionRequired.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindowTab);
mTab = PPMRQAPX_TableWindowTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("Requisition Line Documents ");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_RequisitionLineDocumentsLink...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_RequisitionLineDocumentsLink = new Control("TableWindow_RequisitionLineDocumentsLink", "ID", "lnk_3236_PPMRQAPX_RQLN_CHILD");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_RequisitionLineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindow_RequisitionLineDocumentsLink);
PPMRQAPX_TableWindow_RequisitionLineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on RequisitionLineDocumentsForm...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionLineDocumentsForm = new Control("RequisitionLineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionLineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExist on RequisitionLineDocumentsTable...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionLineDocumentsTable = new Control("RequisitionLineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionLineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_RequisitionLineDocumentsForm);
formBttn = PPMRQAPX_RequisitionLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_RequisitionLineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_RequisitionLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on RequisitionLineDocuments_Document...", Logger.MessageType.INF);
			Control PPMRQAPX_RequisitionLineDocuments_Document = new Control("RequisitionLineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMRQAPX_RequisitionLineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_RequisitionLineDocumentsForm);
formBttn = PPMRQAPX_RequisitionLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Currency Line");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_CurrencyLineLink...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_CurrencyLineLink = new Control("TableWindow_CurrencyLineLink", "ID", "lnk_3280_PPMRQAPX_RQLN_CHILD");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_CurrencyLineLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindow_CurrencyLineLink);
PPMRQAPX_TableWindow_CurrencyLineLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on CurrencyLineForm...", Logger.MessageType.INF);
			Control PPMRQAPX_CurrencyLineForm = new Control("CurrencyLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_CurrencyLineForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on CurrencyLine_LineAmounts_Currency...", Logger.MessageType.INF);
			Control PPMRQAPX_CurrencyLine_LineAmounts_Currency = new Control("CurrencyLine_LineAmounts_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,PPMRQAPX_CurrencyLine_LineAmounts_Currency.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_CurrencyLineForm);
formBttn = PPMRQAPX_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Requisition Line Documents ");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on TableWindow_QCLineTextLink...", Logger.MessageType.INF);
			Control PPMRQAPX_TableWindow_QCLineTextLink = new Control("TableWindow_QCLineTextLink", "ID", "lnk_1005354_PPMRQAPX_RQLN_CHILD");
			CPCommon.AssertEqual(true,PPMRQAPX_TableWindow_QCLineTextLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_TableWindow_QCLineTextLink);
PPMRQAPX_TableWindow_QCLineTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on QCLineTextForm...", Logger.MessageType.INF);
			Control PPMRQAPX_QCLineTextForm = new Control("QCLineTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPX_RQLNTXT_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_QCLineTextForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on QCLineText_LineTextForm...", Logger.MessageType.INF);
			Control PPMRQAPX_QCLineText_LineTextForm = new Control("QCLineText_LineTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNTEXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPX_QCLineText_LineTextForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_QCLineText_LineTextForm);
formBttn = PPMRQAPX_QCLineText_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPX_QCLineText_LineTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPX_QCLineText_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPX";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPX] Perfoming VerifyExists on QCLineText_LineText_Sequence...", Logger.MessageType.INF);
			Control PPMRQAPX_QCLineText_LineText_Sequence = new Control("QCLineText_LineText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,PPMRQAPX_QCLineText_LineText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_QCLineTextForm);
formBttn = PPMRQAPX_QCLineTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main");


												
				CPCommon.CurrentComponent = "PPMRQAPX";
							CPCommon.WaitControlDisplayed(PPMRQAPX_MainForm);
formBttn = PPMRQAPX_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

