 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMSTAT_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchase Orders", "xpath","//div[@class='navItem'][.='Purchase Orders']").Click();
new Control("Approve Pending Purchase Orders", "xpath","//div[@class='navItem'][.='Approve Pending Purchase Orders']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMSTAT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSTAT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMSTAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMSTAT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_MainForm);
IWebElement formBttn = POMSTAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSTAT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSTAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on Identification_PO...", Logger.MessageType.INF);
			Control POMSTAT_Identification_PO = new Control("Identification_PO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POMSTAT_Identification_PO.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control POMSTAT_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1004349_POMSTAT_POHDR_HDR");
			CPCommon.AssertEqual(true,POMSTAT_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_ExchangeRatesLink);
POMSTAT_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control POMSTAT_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSTAT_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control POMSTAT_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,POMSTAT_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_ExchangeRatesForm);
formBttn = POMSTAT_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Header Documents");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POMSTAT_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007759_POMSTAT_POHDR_HDR");
			CPCommon.AssertEqual(true,POMSTAT_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_HeaderDocumentsLink);
POMSTAT_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExist on HeaderDocumentsTable...", Logger.MessageType.INF);
			Control POMSTAT_HeaderDocumentsTable = new Control("HeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSTAT_HeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POMSTAT_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSTAT_HeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_HeaderDocumentsForm);
formBttn = POMSTAT_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSTAT_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSTAT_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POMSTAT_HeaderDocuments_Document = new Control("HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMSTAT_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_HeaderDocumentsForm);
formBttn = POMSTAT_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("POLine Information");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExist on POLineInformationTable...", Logger.MessageType.INF);
			Control POMSTAT_POLineInformationTable = new Control("POLineInformationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_POLN_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSTAT_POLineInformationTable.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on POLineInformationForm...", Logger.MessageType.INF);
			Control POMSTAT_POLineInformationForm = new Control("POLineInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_POLN_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSTAT_POLineInformationForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_POLineInformationForm);
formBttn = POMSTAT_POLineInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSTAT_POLineInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSTAT_POLineInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on POLineInformation_Line...", Logger.MessageType.INF);
			Control POMSTAT_POLineInformation_Line = new Control("POLineInformation_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_POLN_DTL_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,POMSTAT_POLineInformation_Line.Exists());

											Driver.SessionLogger.WriteLine("Purchase Order Line Detail Inquiry");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on POLineInformation_PurchaseOrderLineDetailInquiryLink...", Logger.MessageType.INF);
			Control POMSTAT_POLineInformation_PurchaseOrderLineDetailInquiryLink = new Control("POLineInformation_PurchaseOrderLineDetailInquiryLink", "ID", "lnk_1003277_POMSTAT_POLN_DTL");
			CPCommon.AssertEqual(true,POMSTAT_POLineInformation_PurchaseOrderLineDetailInquiryLink.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_POLineInformation_PurchaseOrderLineDetailInquiryLink);
POMSTAT_POLineInformation_PurchaseOrderLineDetailInquiryLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExist on PurchaseOrderLineDetailInquiryTable...", Logger.MessageType.INF);
			Control POMSTAT_PurchaseOrderLineDetailInquiryTable = new Control("PurchaseOrderLineDetailInquiryTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_POLINEDETAILINQUIRY_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSTAT_PurchaseOrderLineDetailInquiryTable.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on PurchaseOrderLineDetailInquiryForm...", Logger.MessageType.INF);
			Control POMSTAT_PurchaseOrderLineDetailInquiryForm = new Control("PurchaseOrderLineDetailInquiryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_POLINEDETAILINQUIRY_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSTAT_PurchaseOrderLineDetailInquiryForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_PurchaseOrderLineDetailInquiryForm);
formBttn = POMSTAT_PurchaseOrderLineDetailInquiryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSTAT_PurchaseOrderLineDetailInquiryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSTAT_PurchaseOrderLineDetailInquiryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on PurchaseOrderLineDetailInquiry_PROject...", Logger.MessageType.INF);
			Control POMSTAT_PurchaseOrderLineDetailInquiry_PROject = new Control("PurchaseOrderLineDetailInquiry_PROject", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_POLINEDETAILINQUIRY_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,POMSTAT_PurchaseOrderLineDetailInquiry_PROject.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_PurchaseOrderLineDetailInquiryForm);
formBttn = POMSTAT_PurchaseOrderLineDetailInquiryForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Subcontract PO Line Detail");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on POLineInformation_SubcontractPOLineDetailLink...", Logger.MessageType.INF);
			Control POMSTAT_POLineInformation_SubcontractPOLineDetailLink = new Control("POLineInformation_SubcontractPOLineDetailLink", "ID", "lnk_1003278_POMSTAT_POLN_DTL");
			CPCommon.AssertEqual(true,POMSTAT_POLineInformation_SubcontractPOLineDetailLink.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_POLineInformation_SubcontractPOLineDetailLink);
POMSTAT_POLineInformation_SubcontractPOLineDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExist on SubContractPOLineDetailTable...", Logger.MessageType.INF);
			Control POMSTAT_SubContractPOLineDetailTable = new Control("SubContractPOLineDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_SUBCONTRACTLINEDTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSTAT_SubContractPOLineDetailTable.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on SubContractPOLineDetailForm...", Logger.MessageType.INF);
			Control POMSTAT_SubContractPOLineDetailForm = new Control("SubContractPOLineDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_SUBCONTRACTLINEDTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSTAT_SubContractPOLineDetailForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_SubContractPOLineDetailForm);
formBttn = POMSTAT_SubContractPOLineDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSTAT_SubContractPOLineDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSTAT_SubContractPOLineDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on SubcontractPOLineDetail_PROject...", Logger.MessageType.INF);
			Control POMSTAT_SubcontractPOLineDetail_PROject = new Control("SubcontractPOLineDetail_PROject", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSTAT_SUBCONTRACTLINEDTL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,POMSTAT_SubcontractPOLineDetail_PROject.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_SubContractPOLineDetailForm);
formBttn = POMSTAT_SubContractPOLineDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on POLineInformation_LineDocumentsLink...", Logger.MessageType.INF);
			Control POMSTAT_POLineInformation_LineDocumentsLink = new Control("POLineInformation_LineDocumentsLink", "ID", "lnk_1007761_POMSTAT_POLN_DTL");
			CPCommon.AssertEqual(true,POMSTAT_POLineInformation_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_POLineInformation_LineDocumentsLink);
POMSTAT_POLineInformation_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExist on LineDocumentsTable...", Logger.MessageType.INF);
			Control POMSTAT_LineDocumentsTable = new Control("LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSTAT_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on LineDocumentsForm...", Logger.MessageType.INF);
			Control POMSTAT_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSTAT_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_LineDocumentsForm);
formBttn = POMSTAT_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSTAT_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSTAT_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSTAT] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control POMSTAT_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMSTAT_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_LineDocumentsForm);
formBttn = POMSTAT_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "POMSTAT";
							CPCommon.WaitControlDisplayed(POMSTAT_MainForm);
formBttn = POMSTAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

