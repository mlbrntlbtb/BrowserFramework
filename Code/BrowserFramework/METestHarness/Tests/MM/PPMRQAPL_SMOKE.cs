 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQAPL_SMOKE : TestScript
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
new Control("Approve Purchase Requisition Lines", "xpath","//div[@class='navItem'][.='Approve Purchase Requisition Lines']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPMRQAPL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRQAPL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_MainForm);
IWebElement formBttn = PPMRQAPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on Requisition...", Logger.MessageType.INF);
			Control PPMRQAPL_Requisition = new Control("Requisition", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_ID']");
			CPCommon.AssertEqual(true,PPMRQAPL_Requisition.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on BasicInfo_RequisitionInfo_Requisitioner...", Logger.MessageType.INF);
			Control PPMRQAPL_BasicInfo_RequisitionInfo_Requisitioner = new Control("BasicInfo_RequisitionInfo_Requisitioner", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQST_EMPL_ID']");
			CPCommon.AssertEqual(true,PPMRQAPL_BasicInfo_RequisitionInfo_Requisitioner.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control PPMRQAPL_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRQAPL_MainTab);
IWebElement mTab = PPMRQAPL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on LineDetails_InvAbbrev...", Logger.MessageType.INF);
			Control PPMRQAPL_LineDetails_InvAbbrev = new Control("LineDetails_InvAbbrev", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVT_ABBRV_CD']");
			CPCommon.AssertEqual(true,PPMRQAPL_LineDetails_InvAbbrev.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_MainTab);
mTab = PPMRQAPL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on LineAmounts_EstCostType...", Logger.MessageType.INF);
			Control PPMRQAPL_LineAmounts_EstCostType = new Control("LineAmounts_EstCostType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_EST_CST_TYPE_CD']");
			CPCommon.AssertEqual(true,PPMRQAPL_LineAmounts_EstCostType.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_MainTab);
mTab = PPMRQAPL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Purchasing Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on ManufVendParts_ManufacturerPart...", Logger.MessageType.INF);
			Control PPMRQAPL_ManufVendParts_ManufacturerPart = new Control("ManufVendParts_ManufacturerPart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MANUF_PART_ID']");
			CPCommon.AssertEqual(true,PPMRQAPL_ManufVendParts_ManufacturerPart.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_MainTab);
mTab = PPMRQAPL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on Notes_ApprovalNotes...", Logger.MessageType.INF);
			Control PPMRQAPL_Notes_ApprovalNotes = new Control("Notes_ApprovalNotes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='APPRVL_NOTES']");
			CPCommon.AssertEqual(true,PPMRQAPL_Notes_ApprovalNotes.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_MainForm);
formBttn = PPMRQAPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PPMRQAPL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PPMRQAPL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Query] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("Hdr Documents");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on HdrDocumentsLink...", Logger.MessageType.INF);
			Control PPMRQAPL_HdrDocumentsLink = new Control("HdrDocumentsLink", "ID", "lnk_1007730_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_HdrDocumentsLink);
PPMRQAPL_HdrDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on HdrDocumentsForm...", Logger.MessageType.INF);
			Control PPMRQAPL_HdrDocumentsForm = new Control("HdrDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_HdrDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExist on HdrDocumentsFormTable...", Logger.MessageType.INF);
			Control PPMRQAPL_HdrDocumentsFormTable = new Control("HdrDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPL_HdrDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_HdrDocumentsForm);
formBttn = PPMRQAPL_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPL_HdrDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPL_HdrDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on HdrDocuments_CAGE...", Logger.MessageType.INF);
			Control PPMRQAPL_HdrDocuments_CAGE = new Control("HdrDocuments_CAGE", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='CAGE_ID_FLD']");
			CPCommon.AssertEqual(true,PPMRQAPL_HdrDocuments_CAGE.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_HdrDocumentsForm);
formBttn = PPMRQAPL_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on LineDocumentsLink...", Logger.MessageType.INF);
			Control PPMRQAPL_LineDocumentsLink = new Control("LineDocumentsLink", "ID", "lnk_1007729_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_LineDocumentsLink);
PPMRQAPL_LineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on LineDocumentsForm...", Logger.MessageType.INF);
			Control PPMRQAPL_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExist on LineDocumentsFormTable...", Logger.MessageType.INF);
			Control PPMRQAPL_LineDocumentsFormTable = new Control("LineDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPL_LineDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_LineDocumentsForm);
formBttn = PPMRQAPL_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPL_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPL_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on LineDocuments_CAGE...", Logger.MessageType.INF);
			Control PPMRQAPL_LineDocuments_CAGE = new Control("LineDocuments_CAGE", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='CAGE_ID_FLD']");
			CPCommon.AssertEqual(true,PPMRQAPL_LineDocuments_CAGE.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_LineDocumentsForm);
formBttn = PPMRQAPL_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on CurrencyLineLink...", Logger.MessageType.INF);
			Control PPMRQAPL_CurrencyLineLink = new Control("CurrencyLineLink", "ID", "lnk_2736_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_CurrencyLineLink);
PPMRQAPL_CurrencyLineLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on CurrencyLineForm...", Logger.MessageType.INF);
			Control PPMRQAPL_CurrencyLineForm = new Control("CurrencyLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_CurrencyLineForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on CurrencyLine_LineAmounts_Functional_Currency...", Logger.MessageType.INF);
			Control PPMRQAPL_CurrencyLine_LineAmounts_Functional_Currency = new Control("CurrencyLine_LineAmounts_Functional_Currency", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,PPMRQAPL_CurrencyLine_LineAmounts_Functional_Currency.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_CurrencyLineForm);
formBttn = PPMRQAPL_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMRQAPL_CurrencyLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMRQAPL_CurrencyLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExist on CurrencyLineFormTable...", Logger.MessageType.INF);
			Control PPMRQAPL_CurrencyLineFormTable = new Control("CurrencyLineFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPRQCURL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPL_CurrencyLineFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_CurrencyLineForm);
formBttn = PPMRQAPL_CurrencyLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Approval Process");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on ApprovalProcessLink...", Logger.MessageType.INF);
			Control PPMRQAPL_ApprovalProcessLink = new Control("ApprovalProcessLink", "ID", "lnk_1003961_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_ApprovalProcessLink);
PPMRQAPL_ApprovalProcessLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on ApprovalProcessForm...", Logger.MessageType.INF);
			Control PPMRQAPL_ApprovalProcessForm = new Control("ApprovalProcessForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPL_RQLNAPPRVL_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_ApprovalProcessForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExist on ApprovalProcessFormTable...", Logger.MessageType.INF);
			Control PPMRQAPL_ApprovalProcessFormTable = new Control("ApprovalProcessFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPL_RQLNAPPRVL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPL_ApprovalProcessFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_ApprovalProcessForm);
formBttn = PPMRQAPL_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPL_ApprovalProcessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPL_ApprovalProcessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on ApprovalProcess_ApprovalDateTime...", Logger.MessageType.INF);
			Control PPMRQAPL_ApprovalProcess_ApprovalDateTime = new Control("ApprovalProcess_ApprovalDateTime", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPL_RQLNAPPRVL_CHILD_']/ancestor::form[1]/descendant::*[@id='APPRVL_DTT']");
			CPCommon.AssertEqual(true,PPMRQAPL_ApprovalProcess_ApprovalDateTime.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_ApprovalProcessForm);
formBttn = PPMRQAPL_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on ExchangeRatesLink...", Logger.MessageType.INF);
			Control PPMRQAPL_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1003968_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_ExchangeRatesLink);
PPMRQAPL_ExchangeRatesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control PPMRQAPL_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on ExchangeRate_RateGroup...", Logger.MessageType.INF);
			Control PPMRQAPL_ExchangeRate_RateGroup = new Control("ExchangeRate_RateGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='RATE_GRP_ID']");
			CPCommon.AssertEqual(true,PPMRQAPL_ExchangeRate_RateGroup.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_ExchangeRatesForm);
formBttn = PPMRQAPL_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("QC Line Text");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on QCLineTextLink...", Logger.MessageType.INF);
			Control PPMRQAPL_QCLineTextLink = new Control("QCLineTextLink", "ID", "lnk_1004503_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_QCLineTextLink);
PPMRQAPL_QCLineTextLink.Click(1.5);


											Driver.SessionLogger.WriteLine("Account");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming Click on AccountsLink...", Logger.MessageType.INF);
			Control PPMRQAPL_AccountsLink = new Control("AccountsLink", "ID", "lnk_1003957_PPMRQAPL_RQLNAPPRVL_HDR");
			CPCommon.WaitControlDisplayed(PPMRQAPL_AccountsLink);
PPMRQAPL_AccountsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on AccountDistributionForm...", Logger.MessageType.INF);
			Control PPMRQAPL_AccountDistributionForm = new Control("AccountDistributionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPL_RQLNACCT_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRQAPL_AccountDistributionForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExist on AccountDistributionTable...", Logger.MessageType.INF);
			Control PPMRQAPL_AccountDistributionTable = new Control("AccountDistributionTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPL_RQLNACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQAPL_AccountDistributionTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_AccountDistributionForm);
formBttn = PPMRQAPL_AccountDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQAPL_AccountDistributionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQAPL_AccountDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRQAPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQAPL] Perfoming VerifyExists on AccountDistribution_Account...", Logger.MessageType.INF);
			Control PPMRQAPL_AccountDistribution_Account = new Control("AccountDistribution_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRQAPL_RQLNACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PPMRQAPL_AccountDistribution_Account.Exists());

												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_AccountDistributionForm);
formBttn = PPMRQAPL_AccountDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMRQAPL";
							CPCommon.WaitControlDisplayed(PPMRQAPL_MainForm);
formBttn = PPMRQAPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

