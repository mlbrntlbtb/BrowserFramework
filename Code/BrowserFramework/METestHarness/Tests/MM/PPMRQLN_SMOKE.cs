 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQLN_SMOKE : TestScript
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
new Control("Apply PO Info to Purchase Requisitions by Line", "xpath","//div[@class='navItem'][.='Apply PO Info to Purchase Requisitions by Line']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRQLN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRQLN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on Buyer...", Logger.MessageType.INF);
			Control PPMRQLN_Buyer = new Control("Buyer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFSBUYERID']");
			CPCommon.AssertEqual(true,PPMRQLN_Buyer.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control PPMRQLN_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_ChildForm);
IWebElement formBttn = PPMRQLN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PPMRQLN_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PPMRQLN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Query...", Logger.MessageType.INF);
			Control Query_Query = new Control("Query", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Query);
if (Query_Query.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Query.Click(5,5);
else Query_Query.Click(4.5);


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PPMRQLN_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_ChildForm);
formBttn = PPMRQLN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PPMRQLN_MainFormTab = new Control("MainFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRQLN_MainFormTab);
IWebElement mTab = PPMRQLN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on BasicInformation_Item...", Logger.MessageType.INF);
			Control PPMRQLN_BasicInformation_Item = new Control("BasicInformation_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_BasicInformation_Item.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_MainFormTab);
mTab = PPMRQLN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Requisition Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on RequisitionHeader_RequisitionDate...", Logger.MessageType.INF);
			Control PPMRQLN_RequisitionHeader_RequisitionDate = new Control("RequisitionHeader_RequisitionDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='RQ_DT']");
			CPCommon.AssertEqual(true,PPMRQLN_RequisitionHeader_RequisitionDate.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_MainFormTab);
mTab = PPMRQLN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purchasing Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on PurchasingInformation_Buyer...", Logger.MessageType.INF);
			Control PPMRQLN_PurchasingInformation_Buyer = new Control("PurchasingInformation_Buyer", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='BUYER_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_PurchasingInformation_Buyer.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_MainFormTab);
mTab = PPMRQLN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping & Receiving").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ShippingAndReceiving_Warehouse...", Logger.MessageType.INF);
			Control PPMRQLN_ShippingAndReceiving_Warehouse = new Control("ShippingAndReceiving_Warehouse", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_ShippingAndReceiving_Warehouse.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_MainFormTab);
mTab = PPMRQLN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on OtherInformation_ApprovalProcess...", Logger.MessageType.INF);
			Control PPMRQLN_OtherInformation_ApprovalProcess = new Control("OtherInformation_ApprovalProcess", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='RQ_APPR_PROC_CDHDR']");
			CPCommon.AssertEqual(true,PPMRQLN_OtherInformation_ApprovalProcess.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_MainFormTab);
mTab = PPMRQLN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on Notes_HeaderNotes_Text...", Logger.MessageType.INF);
			Control PPMRQLN_Notes_HeaderNotes_Text = new Control("Notes_HeaderNotes_Text", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQLN_RQHDR_HEADER_']/ancestor::form[1]/descendant::*[@id='RQ_NOTES']");
			CPCommon.AssertEqual(true,PPMRQLN_Notes_HeaderNotes_Text.Exists());

											Driver.SessionLogger.WriteLine("Hdr Text");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on HdrTextLink...", Logger.MessageType.INF);
			Control PPMRQLN_HdrTextLink = new Control("HdrTextLink", "ID", "lnk_1005498_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_HdrTextLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_HdrTextLink);
PPMRQLN_HdrTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on HdrTextTable...", Logger.MessageType.INF);
			Control PPMRQLN_HdrTextTable = new Control("HdrTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQHDRTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_HdrTextTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on HdrTextForm...", Logger.MessageType.INF);
			Control PPMRQLN_HdrTextForm = new Control("HdrTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQHDRTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_HdrTextForm);
formBttn = PPMRQLN_HdrTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_HdrTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_HdrTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_HdrTextForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on HdrText_Sequence...", Logger.MessageType.INF);
			Control PPMRQLN_HdrText_Sequence = new Control("HdrText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQHDRTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,PPMRQLN_HdrText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_HdrTextForm);
formBttn = PPMRQLN_HdrTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Hdr Approval");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on HdrApprovalsLink...", Logger.MessageType.INF);
			Control PPMRQLN_HdrApprovalsLink = new Control("HdrApprovalsLink", "ID", "lnk_2693_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_HdrApprovalsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_HdrApprovalsLink);
PPMRQLN_HdrApprovalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on HdrApprovalsTable...", Logger.MessageType.INF);
			Control PPMRQLN_HdrApprovalsTable = new Control("HdrApprovalsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQHDRAPPRVL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_HdrApprovalsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on HdrApprovalsForm...", Logger.MessageType.INF);
			Control PPMRQLN_HdrApprovalsForm = new Control("HdrApprovalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQHDRAPPRVL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_HdrApprovalsForm);
formBttn = PPMRQLN_HdrApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_HdrApprovalsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_HdrApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_HdrApprovalsForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on HdrApprovals_ApprovalRevision...", Logger.MessageType.INF);
			Control PPMRQLN_HdrApprovals_ApprovalRevision = new Control("HdrApprovals_ApprovalRevision", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQHDRAPPRVL_']/ancestor::form[1]/descendant::*[@id='RVSN_NO']");
			CPCommon.AssertEqual(true,PPMRQLN_HdrApprovals_ApprovalRevision.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_HdrApprovalsForm);
formBttn = PPMRQLN_HdrApprovalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ExchangeRateLink...", Logger.MessageType.INF);
			Control PPMRQLN_ExchangeRateLink = new Control("ExchangeRateLink", "ID", "lnk_1004187_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_ExchangeRateLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_ExchangeRateLink);
PPMRQLN_ExchangeRateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ExchangeRateForm...", Logger.MessageType.INF);
			Control PPMRQLN_ExchangeRateForm = new Control("ExchangeRateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQLN_ExchangeRateForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ExchangeRate_TransactionCurrency_RateDate...", Logger.MessageType.INF);
			Control PPMRQLN_ExchangeRate_TransactionCurrency_RateDate = new Control("ExchangeRate_TransactionCurrency_RateDate", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_DT']");
			CPCommon.AssertEqual(true,PPMRQLN_ExchangeRate_TransactionCurrency_RateDate.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_ExchangeRateForm);
formBttn = PPMRQLN_ExchangeRateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Hdr Documents");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on HdrDocumentsLink...", Logger.MessageType.INF);
			Control PPMRQLN_HdrDocumentsLink = new Control("HdrDocumentsLink", "ID", "lnk_1007778_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_HdrDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_HdrDocumentsLink);
PPMRQLN_HdrDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on HdrDocumentsTable...", Logger.MessageType.INF);
			Control PPMRQLN_HdrDocumentsTable = new Control("HdrDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_HdrDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on HdrDocumentsForm...", Logger.MessageType.INF);
			Control PPMRQLN_HdrDocumentsForm = new Control("HdrDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_HdrDocumentsForm);
formBttn = PPMRQLN_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_HdrDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_HdrDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on HdrDocuments_Document...", Logger.MessageType.INF);
			Control PPMRQLN_HdrDocuments_Document = new Control("HdrDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_HdrDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_HdrDocumentsForm);
formBttn = PPMRQLN_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ACCOUNTXS");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on AccountsLink...", Logger.MessageType.INF);
			Control PPMRQLN_AccountsLink = new Control("AccountsLink", "ID", "lnk_1005523_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_AccountsLink);
PPMRQLN_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on AccountsTable...", Logger.MessageType.INF);
			Control PPMRQLN_AccountsTable = new Control("AccountsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_AccountsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on AccountsForm...", Logger.MessageType.INF);
			Control PPMRQLN_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNACCT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_AccountsForm);
formBttn = PPMRQLN_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_AccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_AccountsForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on Accounts_Project...", Logger.MessageType.INF);
			Control PPMRQLN_Accounts_Project = new Control("Accounts_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNACCT_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_Accounts_Project.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_AccountsForm);
formBttn = PPMRQLN_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Currency Line");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on CurrencyLineLink...", Logger.MessageType.INF);
			Control PPMRQLN_CurrencyLineLink = new Control("CurrencyLineLink", "ID", "lnk_2685_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_CurrencyLineLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_CurrencyLineLink);
PPMRQLN_CurrencyLineLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on CurrencyLineForm...", Logger.MessageType.INF);
			Control PPMRQLN_CurrencyLineForm = new Control("CurrencyLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQLN_CurrencyLineForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on CurrencyLine_LineAmounts_Currency...", Logger.MessageType.INF);
			Control PPMRQLN_CurrencyLine_LineAmounts_Currency = new Control("CurrencyLine_LineAmounts_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,PPMRQLN_CurrencyLine_LineAmounts_Currency.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_CurrencyLineForm);
formBttn = PPMRQLN_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMRQLN_CurrencyLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMRQLN_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on CurrencyLineTable...", Logger.MessageType.INF);
			Control PPMRQLN_CurrencyLineTable = new Control("CurrencyLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_CurrencyLineTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_CurrencyLineForm);
formBttn = PPMRQLN_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Text");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineTextLink...", Logger.MessageType.INF);
			Control PPMRQLN_LineTextLink = new Control("LineTextLink", "ID", "lnk_2686_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_LineTextLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineTextLink);
PPMRQLN_LineTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on LineTextTable...", Logger.MessageType.INF);
			Control PPMRQLN_LineTextTable = new Control("LineTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_LineTextTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on LineTextForm...", Logger.MessageType.INF);
			Control PPMRQLN_LineTextForm = new Control("LineTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_LineTextForm);
formBttn = PPMRQLN_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_LineTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_LineTextForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineText_Sequence...", Logger.MessageType.INF);
			Control PPMRQLN_LineText_Sequence = new Control("LineText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNTEXT_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,PPMRQLN_LineText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineTextForm);
formBttn = PPMRQLN_LineTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Charges");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineChargesLink...", Logger.MessageType.INF);
			Control PPMRQLN_LineChargesLink = new Control("LineChargesLink", "ID", "lnk_1005528_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_LineChargesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineChargesLink);
PPMRQLN_LineChargesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on LineChargesTable...", Logger.MessageType.INF);
			Control PPMRQLN_LineChargesTable = new Control("LineChargesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNCHG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_LineChargesTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on LineChargesForm...", Logger.MessageType.INF);
			Control PPMRQLN_LineChargesForm = new Control("LineChargesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNCHG_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_LineChargesForm);
formBttn = PPMRQLN_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_LineChargesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_LineChargesForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineCharges_ChargeType...", Logger.MessageType.INF);
			Control PPMRQLN_LineCharges_ChargeType = new Control("LineCharges_ChargeType", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNCHG_']/ancestor::form[1]/descendant::*[@id='LN_CHG_TYPE']");
			CPCommon.AssertEqual(true,PPMRQLN_LineCharges_ChargeType.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineChargesForm);
formBttn = PPMRQLN_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Ref Quotes");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on RefQuoutesLink...", Logger.MessageType.INF);
			Control PPMRQLN_RefQuoutesLink = new Control("RefQuoutesLink", "ID", "lnk_1005521_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_RefQuoutesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_RefQuoutesLink);
PPMRQLN_RefQuoutesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on RefQuoutesTable...", Logger.MessageType.INF);
			Control PPMRQLN_RefQuoutesTable = new Control("RefQuoutesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNQUOTES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_RefQuoutesTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on RefQuoutesForm...", Logger.MessageType.INF);
			Control PPMRQLN_RefQuoutesForm = new Control("RefQuoutesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNQUOTES_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_RefQuoutesForm);
formBttn = PPMRQLN_RefQuoutesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_RefQuoutesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_RefQuoutesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_RefQuoutesForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on RefQuotes_Quote_Quote...", Logger.MessageType.INF);
			Control PPMRQLN_RefQuotes_Quote_Quote = new Control("RefQuotes_Quote_Quote", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNQUOTES_']/ancestor::form[1]/descendant::*[@id='QUOTE_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_RefQuotes_Quote_Quote.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_RefQuoutesForm);
formBttn = PPMRQLN_RefQuoutesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("RFQs");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on RFQLink...", Logger.MessageType.INF);
			Control PPMRQLN_RFQLink = new Control("RFQLink", "ID", "lnk_1005515_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_RFQLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_RFQLink);
PPMRQLN_RFQLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on RFQTable...", Logger.MessageType.INF);
			Control PPMRQLN_RFQTable = new Control("RFQTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RFQLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_RFQTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on RFQForm...", Logger.MessageType.INF);
			Control PPMRQLN_RFQForm = new Control("RFQForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RFQLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_RFQForm);
formBttn = PPMRQLN_RFQForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_RFQForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_RFQForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_RFQForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on RFQs_RequestForQuote_RFQ...", Logger.MessageType.INF);
			Control PPMRQLN_RFQs_RequestForQuote_RFQ = new Control("RFQs_RequestForQuote_RFQ", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RFQLN_']/ancestor::form[1]/descendant::*[@id='RFQ_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_RFQs_RequestForQuote_RFQ.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_RFQForm);
formBttn = PPMRQLN_RFQForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assign PO");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on AssignPOLink...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPOLink = new Control("AssignPOLink", "ID", "lnk_2973_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_AssignPOLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_AssignPOLink);
PPMRQLN_AssignPOLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on AssignPOTable...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPOTable = new Control("AssignPOTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNPO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_AssignPOTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on AssignPOForm...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPOForm = new Control("AssignPOForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNPO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_AssignPOForm);
formBttn = PPMRQLN_AssignPOForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_AssignPOForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_AssignPOForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_AssignPOForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on AssignPO_ReadyForPO...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPO_ReadyForPO = new Control("AssignPO_ReadyForPO", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNPO_']/ancestor::form[1]/descendant::*[@id='RDY_FOR_PO_FL']");
			CPCommon.AssertEqual(true,PPMRQLN_AssignPO_ReadyForPO.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming Select on AssignPOFormTab...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPOFormTab = new Control("AssignPOFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNPO_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRQLN_AssignPOFormTab);
mTab = PPMRQLN_AssignPOFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purchase Order Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on AssignPO_PurchaseOrderInformation_Vendor...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPO_PurchaseOrderInformation_Vendor = new Control("AssignPO_PurchaseOrderInformation_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNPO_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_AssignPO_PurchaseOrderInformation_Vendor.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_AssignPOFormTab);
mTab = PPMRQLN_AssignPOFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Functional Currency Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on AssignPO_FunctionalCurrencyAmounts_GrossUnitCost...", Logger.MessageType.INF);
			Control PPMRQLN_AssignPO_FunctionalCurrencyAmounts_GrossUnitCost = new Control("AssignPO_FunctionalCurrencyAmounts_GrossUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNPO_']/ancestor::form[1]/descendant::*[@id='NEG_GROSS_UNIT_AMT']");
			CPCommon.AssertEqual(true,PPMRQLN_AssignPO_FunctionalCurrencyAmounts_GrossUnitCost.Exists());

											Driver.SessionLogger.WriteLine("ChildForm_SerialLotInfoForm");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on SerialLotLink...", Logger.MessageType.INF);
			Control PPMRQLN_SerialLotLink = new Control("SerialLotLink", "ID", "lnk_18822_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_SerialLotLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_ExchangeRateLink.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_AssignPOForm);
formBttn = PPMRQLN_AssignPOForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Approvals");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineApprovalLink...", Logger.MessageType.INF);
			Control PPMRQLN_LineApprovalLink = new Control("LineApprovalLink", "ID", "lnk_1005503_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_LineApprovalLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineApprovalLink);
PPMRQLN_LineApprovalLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on LineApprovalTable...", Logger.MessageType.INF);
			Control PPMRQLN_LineApprovalTable = new Control("LineApprovalTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNAPPRVL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_LineApprovalTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on LineApprovalForm...", Logger.MessageType.INF);
			Control PPMRQLN_LineApprovalForm = new Control("LineApprovalForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNAPPRVL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_LineApprovalForm);
formBttn = PPMRQLN_LineApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_LineApprovalForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_LineApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_LineApprovalForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineApprovals_ApprovalRevision...", Logger.MessageType.INF);
			Control PPMRQLN_LineApprovals_ApprovalRevision = new Control("LineApprovals_ApprovalRevision", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMENTRQ_RQLNAPPRVL_']/ancestor::form[1]/descendant::*[@id='RQ_LN_RVSN_NO']");
			CPCommon.AssertEqual(true,PPMRQLN_LineApprovals_ApprovalRevision.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineApprovalForm);
formBttn = PPMRQLN_LineApprovalForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineDocumentsLink...", Logger.MessageType.INF);
			Control PPMRQLN_LineDocumentsLink = new Control("LineDocumentsLink", "ID", "lnk_1007779_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineDocumentsLink);
PPMRQLN_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on LineDocumentsTable...", Logger.MessageType.INF);
			Control PPMRQLN_LineDocumentsTable = new Control("LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming ClickButton on LineDocumentsForm...", Logger.MessageType.INF);
			Control PPMRQLN_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMRQLN_LineDocumentsForm);
formBttn = PPMRQLN_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.AssertEqual(true,PPMRQLN_LineDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control PPMRQLN_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMRQLN_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_LineDocumentsForm);
formBttn = PPMRQLN_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitutes");


												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ProjSubPartsLink...", Logger.MessageType.INF);
			Control PPMRQLN_ProjSubPartsLink = new Control("ProjSubPartsLink", "ID", "lnk_16471_PPMRQLN_RQHDR_HEADER");
			CPCommon.AssertEqual(true,PPMRQLN_ProjSubPartsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_ProjSubPartsLink);
PPMRQLN_ProjSubPartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ProjSubPartsForm...", Logger.MessageType.INF);
			Control PPMRQLN_ProjSubPartsForm = new Control("ProjSubPartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSUBSTPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQLN_ProjSubPartsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExist on ProjSubPartsTable...", Logger.MessageType.INF);
			Control PPMRQLN_ProjSubPartsTable = new Control("ProjSubPartsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSUBSTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQLN_ProjSubPartsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_ProjSubPartsForm);
formBttn = PPMRQLN_ProjSubPartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQLN_ProjSubPartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQLN_ProjSubPartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQLN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQLN] Perfoming VerifyExists on ProjSubParts_Sequence...", Logger.MessageType.INF);
			Control PPMRQLN_ProjSubParts_Sequence = new Control("ProjSubParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSUBSTPART_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,PPMRQLN_ProjSubParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_ProjSubPartsForm);
formBttn = PPMRQLN_ProjSubPartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PPMRQLN";
							CPCommon.WaitControlDisplayed(PPMRQLN_MainForm);
formBttn = PPMRQLN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

