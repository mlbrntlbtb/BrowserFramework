 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMAPPSO_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Orders", "xpath","//div[@class='navItem'][.='Sales Orders']").Click();
new Control("Approve Sales Orders", "xpath","//div[@class='navItem'][.='Approve Sales Orders']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming ClickButtonIfExists on MainForm...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm);
IWebElement formBttn = OEMAPPSO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_Identification_SO...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_Identification_SO = new Control("MainForm_Identification_SO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_Identification_SO.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.AssertEqual(true,OEMAPPSO_MainForm.Exists());

													
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm);
formBttn = OEMAPPSO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMAPPSO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMAPPSO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMAPPSO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("ApprovalProcessLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_ApprovalProcessLink...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_ApprovalProcessLink = new Control("MainForm_ApprovalProcessLink", "ID", "lnk_1003221_OEMAPPSO_SOHDRAPPRVL_HEADER");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_ApprovalProcessLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm_ApprovalProcessLink);
OEMAPPSO_MainForm_ApprovalProcessLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on ApprovalProcessFormTable...", Logger.MessageType.INF);
			Control OEMAPPSO_ApprovalProcessFormTable = new Control("ApprovalProcessFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_ApprovalProcessFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ApprovalProcessForm...", Logger.MessageType.INF);
			Control OEMAPPSO_ApprovalProcessForm = new Control("ApprovalProcessForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_ApprovalProcessForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ApprovalProcessForm);
formBttn = OEMAPPSO_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_ApprovalProcessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ApprovalProcess_Seq...", Logger.MessageType.INF);
			Control OEMAPPSO_ApprovalProcess_Seq = new Control("ApprovalProcess_Seq", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDR_']/ancestor::form[1]/descendant::*[@id='APPRVL_SEQ_NO']");
			CPCommon.AssertEqual(true,OEMAPPSO_ApprovalProcess_Seq.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ApprovalProcessForm);
formBttn = OEMAPPSO_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TotalsLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_TotalsLink...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_TotalsLink = new Control("MainForm_TotalsLink", "ID", "lnk_1003227_OEMAPPSO_SOHDRAPPRVL_HEADER");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_TotalsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm_TotalsLink);
OEMAPPSO_MainForm_TotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on TotalsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_TotalsForm = new Control("TotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SQ_TOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_TotalsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on Totals_Currency...", Logger.MessageType.INF);
			Control OEMAPPSO_Totals_Currency = new Control("Totals_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SQ_TOTALS_']/ancestor::form[1]/descendant::*[@id='TRANS_CURR_CODE']");
			CPCommon.AssertEqual(true,OEMAPPSO_Totals_Currency.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_TotalsForm);
formBttn = OEMAPPSO_TotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ExchangeRatesLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_ExchangeRatesLink = new Control("MainForm_ExchangeRatesLink", "ID", "lnk_1003807_OEMAPPSO_SOHDRAPPRVL_HEADER");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm_ExchangeRatesLink);
OEMAPPSO_MainForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control OEMAPPSO_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control OEMAPPSO_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,OEMAPPSO_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ExchangeRatesForm);
formBttn = OEMAPPSO_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SODetailsLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_SODetailsLink...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_SODetailsLink = new Control("MainForm_SODetailsLink", "ID", "lnk_1003688_OEMAPPSO_SOHDRAPPRVL_HEADER");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_SODetailsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm_SODetailsLink);
OEMAPPSO_MainForm_SODetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on SODetailsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_SODetailsForm = new Control("SODetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDRINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_SODetailsForm.Exists());

											Driver.SessionLogger.WriteLine("SODetailsTab");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming Select on SODetailsTab...", Logger.MessageType.INF);
			Control OEMAPPSO_SODetailsTab = new Control("SODetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDRINFO_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMAPPSO_SODetailsTab);
IWebElement mTab = OEMAPPSO_SODetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Addresses").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on SODetails_Addresses_BillTo...", Logger.MessageType.INF);
			Control OEMAPPSO_SODetails_Addresses_BillTo = new Control("SODetails_Addresses_BillTo", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDRINFO_']/ancestor::form[1]/descendant::*[@id='BILL_TO_ADDR_DC']");
			CPCommon.AssertEqual(true,OEMAPPSO_SODetails_Addresses_BillTo.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_SODetailsTab);
mTab = OEMAPPSO_SODetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Credit Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on SODetails_CreditInformation_Territory...", Logger.MessageType.INF);
			Control OEMAPPSO_SODetails_CreditInformation_Territory = new Control("SODetails_CreditInformation_Territory", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDRINFO_']/ancestor::form[1]/descendant::*[@id='SALES_TERR_DC']");
			CPCommon.AssertEqual(true,OEMAPPSO_SODetails_CreditInformation_Territory.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_SODetailsTab);
mTab = OEMAPPSO_SODetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on SODetails_HeaderInformation_CustomerInfo_LastName...", Logger.MessageType.INF);
			Control OEMAPPSO_SODetails_HeaderInformation_CustomerInfo_LastName = new Control("SODetails_HeaderInformation_CustomerInfo_LastName", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_HDRINFO_']/ancestor::form[1]/descendant::*[@id='CNTACT_LAST_NAME']");
			CPCommon.AssertEqual(true,OEMAPPSO_SODetails_HeaderInformation_CustomerInfo_LastName.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_SODetailsForm);
formBttn = OEMAPPSO_SODetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("UDFInfoLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_UDFInfoLink...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_UDFInfoLink = new Control("MainForm_UDFInfoLink", "ID", "lnk_1007520_OEMAPPSO_SOHDRAPPRVL_HEADER");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_UDFInfoLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm_UDFInfoLink);
OEMAPPSO_MainForm_UDFInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on UDFInfoTable...", Logger.MessageType.INF);
			Control OEMAPPSO_UDFInfoTable = new Control("UDFInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_USERDEFINEDINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_UDFInfoTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on UDFInfoForm...", Logger.MessageType.INF);
			Control OEMAPPSO_UDFInfoForm = new Control("UDFInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_UDFInfoForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_UDFInfoForm);
formBttn = OEMAPPSO_UDFInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SOHeaderDocumentsLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on MainForm_SOHeaderDocumentsLink...", Logger.MessageType.INF);
			Control OEMAPPSO_MainForm_SOHeaderDocumentsLink = new Control("MainForm_SOHeaderDocumentsLink", "ID", "lnk_1007521_OEMAPPSO_SOHDRAPPRVL_HEADER");
			CPCommon.AssertEqual(true,OEMAPPSO_MainForm_SOHeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm_SOHeaderDocumentsLink);
OEMAPPSO_MainForm_SOHeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on SOHeaderDocumentsTable...", Logger.MessageType.INF);
			Control OEMAPPSO_SOHeaderDocumentsTable = new Control("SOHeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_SOHeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on SOHeaderDocumentsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_SOHeaderDocumentsForm = new Control("SOHeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_SOHeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_SOHeaderDocumentsForm);
formBttn = OEMAPPSO_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_SOHeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on SOHeaderDocuments_Document...", Logger.MessageType.INF);
			Control OEMAPPSO_SOHeaderDocuments_Document = new Control("SOHeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_SOHeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_SOHeaderDocumentsForm);
formBttn = OEMAPPSO_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Approval Sales Order Details Form");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm);
formBttn = OEMAPPSO_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Approval Sales Order Details Tab");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming Select on ChildFormTab...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMAPPSO_ChildFormTab);
mTab = OEMAPPSO_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Item Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_ItemDetails_SOLine...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_ItemDetails_SOLine = new Control("ChildForm_ItemDetails_SOLine", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='SO_LN_NO']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_ItemDetails_SOLine.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildFormTab);
mTab = OEMAPPSO_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Inventory").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_Inventory_WAREHOUSEM...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_Inventory_WAREHOUSEM = new Control("ChildForm_Inventory_WAREHOUSEM", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_Inventory_WAREHOUSEM.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildFormTab);
mTab = OEMAPPSO_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Price Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_PriceDetails_GrossUnitPrice...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_PriceDetails_GrossUnitPrice = new Control("ChildForm_PriceDetails_GrossUnitPrice", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='TRN_GR_UNIT_PR_AMT']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_PriceDetails_GrossUnitPrice.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildFormTab);
mTab = OEMAPPSO_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_Shipping_ShipID...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_Shipping_ShipID = new Control("ChildForm_Shipping_ShipID", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='SHIP_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_Shipping_ShipID.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildFormTab);
mTab = OEMAPPSO_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_Billing_InstallmentBill...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_Billing_InstallmentBill = new Control("ChildForm_Billing_InstallmentBill", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='INSTALL_BILL_FL']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_Billing_InstallmentBill.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildFormTab);
mTab = OEMAPPSO_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_OtherInfo_NumberOfUsers...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_OtherInfo_NumberOfUsers = new Control("ChildForm_OtherInfo_NumberOfUsers", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_DETAIL_']/ancestor::form[1]/descendant::*[@id='USERS_NO']");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_OtherInfo_NumberOfUsers.Exists());

											Driver.SessionLogger.WriteLine("AccountsLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_AccountsLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_AccountsLink = new Control("ChildForm_AccountsLink", "ID", "lnk_1002949_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_AccountsLink);
OEMAPPSO_ChildForm_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on AccountsTable...", Logger.MessageType.INF);
			Control OEMAPPSO_AccountsTable = new Control("AccountsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SALESGROUPACCTS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_AccountsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on AccountsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SALESGROUPACCTS_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_AccountsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_AccountsForm);
formBttn = OEMAPPSO_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_AccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on Accounts_TransactionDesc...", Logger.MessageType.INF);
			Control OEMAPPSO_Accounts_TransactionDesc = new Control("Accounts_TransactionDesc", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SALESGROUPACCTS_DTL_']/ancestor::form[1]/descendant::*[@id='PROD_TRN_TYPE_DESC']");
			CPCommon.AssertEqual(true,OEMAPPSO_Accounts_TransactionDesc.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_AccountsForm);
formBttn = OEMAPPSO_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ComponentsLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_ComponentsLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_ComponentsLink = new Control("ChildForm_ComponentsLink", "ID", "lnk_1002980_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_ComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_ComponentsLink);
OEMAPPSO_ChildForm_ComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on ComponentsTable...", Logger.MessageType.INF);
			Control OEMAPPSO_ComponentsTable = new Control("ComponentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOLNCOMP_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_ComponentsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ComponentsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_ComponentsForm = new Control("ComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOLNCOMP_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_ComponentsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ComponentsForm);
formBttn = OEMAPPSO_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_ComponentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on Components_Line...", Logger.MessageType.INF);
			Control OEMAPPSO_Components_Line = new Control("Components_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOLNCOMP_DTL_']/ancestor::form[1]/descendant::*[@id='SO_LN_COMP_NO']");
			CPCommon.AssertEqual(true,OEMAPPSO_Components_Line.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ComponentsForm);
formBttn = OEMAPPSO_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LineChargesLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_LineChargesLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_LineChargesLink = new Control("ChildForm_LineChargesLink", "ID", "lnk_1003016_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_LineChargesLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_LineChargesLink);
OEMAPPSO_ChildForm_LineChargesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on LineChargesTable...", Logger.MessageType.INF);
			Control OEMAPPSO_LineChargesTable = new Control("LineChargesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOLNCHG_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_LineChargesTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on LineChargesForm...", Logger.MessageType.INF);
			Control OEMAPPSO_LineChargesForm = new Control("LineChargesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOLNCHG_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_LineChargesForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_LineChargesForm);
formBttn = OEMAPPSO_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_LineChargesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on LineCharges_ChargeType...", Logger.MessageType.INF);
			Control OEMAPPSO_LineCharges_ChargeType = new Control("LineCharges_ChargeType", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOLNCHG_DTL_']/ancestor::form[1]/descendant::*[@id='LN_CHG_TYPE']");
			CPCommon.AssertEqual(true,OEMAPPSO_LineCharges_ChargeType.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_LineChargesForm);
formBttn = OEMAPPSO_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CurrencyLine Information Link");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_CurrencyLineInformationLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_CurrencyLineInformationLink = new Control("ChildForm_CurrencyLineInformationLink", "ID", "lnk_1003636_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_CurrencyLineInformationLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_CurrencyLineInformationLink);
OEMAPPSO_ChildForm_CurrencyLineInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on CurrencyLineInformationForm...", Logger.MessageType.INF);
			Control OEMAPPSO_CurrencyLineInformationForm = new Control("CurrencyLineInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPOESOLN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_CurrencyLineInformationForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on CurrencyLineInformation_LineAmounts_Currency...", Logger.MessageType.INF);
			Control OEMAPPSO_CurrencyLineInformation_LineAmounts_Currency = new Control("CurrencyLineInformation_LineAmounts_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPOESOLN_']/ancestor::form[1]/descendant::*[@id='TRANS_CURR_CODE']");
			CPCommon.AssertEqual(true,OEMAPPSO_CurrencyLineInformation_LineAmounts_Currency.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_CurrencyLineInformationForm);
formBttn = OEMAPPSO_CurrencyLineInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Customs Information Link");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_CustomsInformationLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_CustomsInformationLink = new Control("ChildForm_CustomsInformationLink", "ID", "lnk_1003689_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_CustomsInformationLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_CustomsInformationLink);
OEMAPPSO_ChildForm_CustomsInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on CustomsInformationForm...", Logger.MessageType.INF);
			Control OEMAPPSO_CustomsInformationForm = new Control("CustomsInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_CUSTLNDTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_CustomsInformationForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on CustomsInformation_ValueAddedTaxInfo_TaxID...", Logger.MessageType.INF);
			Control OEMAPPSO_CustomsInformation_ValueAddedTaxInfo_TaxID = new Control("CustomsInformation_ValueAddedTaxInfo_TaxID", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_SOHDRAPPRVL_CUSTLNDTL_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_CustomsInformation_ValueAddedTaxInfo_TaxID.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_CustomsInformationForm);
formBttn = OEMAPPSO_CustomsInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LineDocumentsLink");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_LineDocumentsLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_LineDocumentsLink = new Control("ChildForm_LineDocumentsLink", "ID", "lnk_1007702_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_LineDocumentsLink);
OEMAPPSO_ChildForm_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on LineDocumentsTable...", Logger.MessageType.INF);
			Control OEMAPPSO_LineDocumentsTable = new Control("LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on LineDocumentsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_LineDocumentsForm);
formBttn = OEMAPPSO_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control OEMAPPSO_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_LineDocumentsForm);
formBttn = OEMAPPSO_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Consume Forecast Link");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ChildForm_ConsumeForecastLink...", Logger.MessageType.INF);
			Control OEMAPPSO_ChildForm_ConsumeForecastLink = new Control("ChildForm_ConsumeForecastLink", "ID", "lnk_1007816_OEMAPPSO_SOHDRAPPRVL_DETAIL");
			CPCommon.AssertEqual(true,OEMAPPSO_ChildForm_ConsumeForecastLink.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ChildForm_ConsumeForecastLink);
OEMAPPSO_ChildForm_ConsumeForecastLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ConsumeForcastForm...", Logger.MessageType.INF);
			Control OEMAPPSO_ConsumeForcastForm = new Control("ConsumeForcastForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_MPSFORECASTSOLN_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_ConsumeForcastForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ConsumeForecast_Part...", Logger.MessageType.INF);
			Control OEMAPPSO_ConsumeForecast_Part = new Control("ConsumeForecast_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_MPSFORECASTSOLN_HDR_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_ConsumeForecast_Part.Exists());

											Driver.SessionLogger.WriteLine("Forecasts");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExist on ForecastsTable...", Logger.MessageType.INF);
			Control OEMAPPSO_ForecastsTable = new Control("ForecastsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_MPSFORECASTSOLN_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMAPPSO_ForecastsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on ForcastsForm...", Logger.MessageType.INF);
			Control OEMAPPSO_ForcastsForm = new Control("ForcastsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_MPSFORECASTSOLN_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMAPPSO_ForcastsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ForcastsForm);
formBttn = OEMAPPSO_ForcastsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMAPPSO_ForcastsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMAPPSO_ForcastsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMAPPSO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMAPPSO] Perfoming VerifyExists on Forecasts_ForecastPart...", Logger.MessageType.INF);
			Control OEMAPPSO_Forecasts_ForecastPart = new Control("Forecasts_ForecastPart", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMAPPSO_MPSFORECASTSOLN_DTL_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,OEMAPPSO_Forecasts_ForecastPart.Exists());

												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_ConsumeForcastForm);
formBttn = OEMAPPSO_ConsumeForcastForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OEMAPPSO";
							CPCommon.WaitControlDisplayed(OEMAPPSO_MainForm);
formBttn = OEMAPPSO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

