 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POPDM_SMOKE : TestScript
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
new Control("Create Debit Memos", "xpath","//div[@class='navItem'][.='Create Debit Memos']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POPDM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POPDM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on IncludeRejectedReplaceAmounts...", Logger.MessageType.INF);
			Control POPDM_IncludeRejectedReplaceAmounts = new Control("IncludeRejectedReplaceAmounts", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INCL_REJ_REP_FL']");
			CPCommon.AssertEqual(true,POPDM_IncludeRejectedReplaceAmounts.Exists());

											Driver.SessionLogger.WriteLine("Creata Debit Memos");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemosForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemosForm = new Control("CreateDebitMemosForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemosForm);
IWebElement formBttn = POPDM_CreateDebitMemosForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? POPDM_CreateDebitMemosForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
POPDM_CreateDebitMemosForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemosFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemosFormTable = new Control("CreateDebitMemosFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemosFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemosForm);
formBttn = POPDM_CreateDebitMemosForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemosForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemosForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemosForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_Vendor...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_Vendor = new Control("CreateDebitMemos_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_Vendor.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming Select on CreateDebitMemos_CreateDebitMemosTab...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_CreateDebitMemosTab = new Control("CreateDebitMemos_CreateDebitMemosTab", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_CreateDebitMemosTab);
IWebElement mTab = POPDM_CreateDebitMemos_CreateDebitMemosTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Debit Memo Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_DebitMemoDetails_CreateDebitMemo...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_DebitMemoDetails_CreateDebitMemo = new Control("CreateDebitMemos_DebitMemoDetails_CreateDebitMemo", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]/descendant::*[@id='CREATE_DM']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_DebitMemoDetails_CreateDebitMemo.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_CreateDebitMemosTab);
mTab = POPDM_CreateDebitMemos_CreateDebitMemosTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PO Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_POLineDetails_Item...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POLineDetails_Item = new Control("CreateDebitMemos_POLineDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POLineDetails_Item.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_CreateDebitMemosTab);
mTab = POPDM_CreateDebitMemos_CreateDebitMemosTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PO Line Quantities & Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_POLineQuantitiesAmounts_Quantities_Order...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POLineQuantitiesAmounts_Quantities_Order = new Control("CreateDebitMemos_POLineQuantitiesAmounts_Quantities_Order", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_HDR_']/ancestor::form[1]/descendant::*[@id='ORD_QTY']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POLineQuantitiesAmounts_Quantities_Order.Exists());

											Driver.SessionLogger.WriteLine("Header Documents");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_HeaderDocumentsLink = new Control("CreateDebitMemos_HeaderDocumentsLink", "ID", "lnk_1007823_POPDM_HDR");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_HeaderDocumentsLink);
POPDM_CreateDebitMemos_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemos_HeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_HeaderDocumentsFormTable = new Control("CreateDebitMemos_HeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_HeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemos_HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_HeaderDocumentsForm = new Control("CreateDebitMemos_HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_HeaderDocumentsForm);
formBttn = POPDM_CreateDebitMemos_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemos_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemos_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_HeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_HeaderDocuments_Document = new Control("CreateDebitMemos_HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_HeaderDocumentsForm);
formBttn = POPDM_CreateDebitMemos_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PO Line Accounts");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_POLineAccountsLink...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POLineAccountsLink = new Control("CreateDebitMemos_POLineAccountsLink", "ID", "lnk_1006353_POPDM_HDR");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POLineAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_POLineAccountsLink);
POPDM_CreateDebitMemos_POLineAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemos_POLineAccountsFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POLineAccountsFormTable = new Control("CreateDebitMemos_POLineAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POLNACCT_POLNINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POLineAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemos_POLineAccountsForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POLineAccountsForm = new Control("CreateDebitMemos_POLineAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POLNACCT_POLNINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_POLineAccountsForm);
formBttn = POPDM_CreateDebitMemos_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemos_POLineAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemos_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POLineAccountsForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_POLineAccounts_Project...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POLineAccounts_Project = new Control("CreateDebitMemos_POLineAccounts_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POLNACCT_POLNINFO_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POLineAccounts_Project.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_POLineAccountsForm);
formBttn = POPDM_CreateDebitMemos_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_LineDocumentsLink...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_LineDocumentsLink = new Control("CreateDebitMemos_LineDocumentsLink", "ID", "lnk_1007824_POPDM_HDR");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_LineDocumentsLink);
POPDM_CreateDebitMemos_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemos_LineDocumentsFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_LineDocumentsFormTable = new Control("CreateDebitMemos_LineDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_LineDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemos_LineDocumentsForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_LineDocumentsForm = new Control("CreateDebitMemos_LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_LineDocumentsForm);
formBttn = POPDM_CreateDebitMemos_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemos_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemos_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_LineDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_LineDocuments_Document...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_LineDocuments_Document = new Control("CreateDebitMemos_LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_LineDocumentsForm);
formBttn = POPDM_CreateDebitMemos_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PO Receipts");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_POReceiptsLink...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POReceiptsLink = new Control("CreateDebitMemos_POReceiptsLink", "ID", "lnk_2634_POPDM_HDR");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POReceiptsLink.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_POReceiptsLink);
POPDM_CreateDebitMemos_POReceiptsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemos_POReceiptsFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POReceiptsFormTable = new Control("CreateDebitMemos_POReceiptsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_RECEIPT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POReceiptsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemos_POReceiptsForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POReceiptsForm = new Control("CreateDebitMemos_POReceiptsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_RECEIPT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_POReceiptsForm);
formBttn = POPDM_CreateDebitMemos_POReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemos_POReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemos_POReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POReceiptsForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_POReceipts_Receipt...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_POReceipts_Receipt = new Control("CreateDebitMemos_POReceipts_Receipt", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_RECEIPT_']/ancestor::form[1]/descendant::*[@id='RECPT_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_POReceipts_Receipt.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_POReceiptsForm);
formBttn = POPDM_CreateDebitMemos_POReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Inspections");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_InspectionsLink...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_InspectionsLink = new Control("CreateDebitMemos_InspectionsLink", "ID", "lnk_1006355_POPDM_HDR");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_InspectionsLink.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_InspectionsLink);
POPDM_CreateDebitMemos_InspectionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemos_InspectionsFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_InspectionsFormTable = new Control("CreateDebitMemos_InspectionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_INSPLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_InspectionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemos_InspectionsForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_InspectionsForm = new Control("CreateDebitMemos_InspectionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_INSPLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_InspectionsForm);
formBttn = POPDM_CreateDebitMemos_InspectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemos_InspectionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemos_InspectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_InspectionsForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_Inspections_Inspection...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_Inspections_Inspection = new Control("CreateDebitMemos_Inspections_Inspection", "xpath", "//div[translate(@id,'0123456789','')='pr__POPDM_INSPLN_']/ancestor::form[1]/descendant::*[@id='INSP_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_Inspections_Inspection.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_InspectionsForm);
formBttn = POPDM_CreateDebitMemos_InspectionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Vend Returns");


												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_VendorReturnsLink...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_VendorReturnsLink = new Control("CreateDebitMemos_VendorReturnsLink", "ID", "lnk_2808_POPDM_HDR");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_VendorReturnsLink.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_VendorReturnsLink);
POPDM_CreateDebitMemos_VendorReturnsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExist on CreateDebitMemos_VendReturnsFormTable...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_VendReturnsFormTable = new Control("CreateDebitMemos_VendReturnsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_VENDRTRNLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_VendReturnsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming ClickButton on CreateDebitMemos_VendReturnsForm...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_VendReturnsForm = new Control("CreateDebitMemos_VendReturnsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_VENDRTRNLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_VendReturnsForm);
formBttn = POPDM_CreateDebitMemos_VendReturnsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POPDM_CreateDebitMemos_VendReturnsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POPDM_CreateDebitMemos_VendReturnsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_VendReturnsForm.Exists());

													
				CPCommon.CurrentComponent = "POPDM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPDM] Perfoming VerifyExists on CreateDebitMemos_VendReturns_VendorReturn...", Logger.MessageType.INF);
			Control POPDM_CreateDebitMemos_VendReturns_VendorReturn = new Control("CreateDebitMemos_VendReturns_VendorReturn", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_VENDRTRNLN_']/ancestor::form[1]/descendant::*[@id='RTRN_ID']");
			CPCommon.AssertEqual(true,POPDM_CreateDebitMemos_VendReturns_VendorReturn.Exists());

												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_CreateDebitMemos_VendReturnsForm);
formBttn = POPDM_CreateDebitMemos_VendReturnsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "POPDM";
							CPCommon.WaitControlDisplayed(POPDM_MainForm);
formBttn = POPDM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

