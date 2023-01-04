 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMSHIP_SMOKE : TestScript
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
new Control("Sales Order Shipping", "xpath","//div[@class='navItem'][.='Sales Order Shipping']").Click();
new Control("Manage Shipping Transactions", "xpath","//div[@class='navItem'][.='Manage Shipping Transactions']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on SOHeaderDocumentsLink...", Logger.MessageType.INF);
			Control OEMSHIP_SOHeaderDocumentsLink = new Control("SOHeaderDocumentsLink", "ID", "lnk_1007725_OEMSHIP_OEMTRN_HEADER");
			CPCommon.AssertEqual(true,OEMSHIP_SOHeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_SOHeaderDocumentsLink);
OEMSHIP_SOHeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on SOHeaderDocumentsForm...", Logger.MessageType.INF);
			Control OEMSHIP_SOHeaderDocumentsForm = new Control("SOHeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_HDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_SOHeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on SOHeaderDocumentsTable...", Logger.MessageType.INF);
			Control OEMSHIP_SOHeaderDocumentsTable = new Control("SOHeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_SOHeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_SOHeaderDocumentsForm);
IWebElement formBttn = OEMSHIP_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMSHIP_SOHeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMSHIP_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on SOHeaderDocuments_Document...", Logger.MessageType.INF);
			Control OEMSHIP_SOHeaderDocuments_Document = new Control("SOHeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMSHIP_SOHeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_SOHeaderDocumentsForm);
formBttn = OEMSHIP_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMSHIP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainForm);
formBttn = OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMSHIP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMSHIP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainForm);
formBttn = OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMSHIP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on Identification_PackingSlipNo...", Logger.MessageType.INF);
			Control OEMSHIP_Identification_PackingSlipNo = new Control("Identification_PackingSlipNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PS_ID']");
			CPCommon.AssertEqual(true,OEMSHIP_Identification_PackingSlipNo.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control OEMSHIP_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMSHIP_MainTab);
IWebElement mTab = OEMSHIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shipping Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on ShippingInformation_ShippingInformation_ShipVia...", Logger.MessageType.INF);
			Control OEMSHIP_ShippingInformation_ShippingInformation_ShipVia = new Control("ShippingInformation_ShippingInformation_ShipVia", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SHIP_VIA_FLD']");
			CPCommon.AssertEqual(true,OEMSHIP_ShippingInformation_ShippingInformation_ShipVia.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainTab);
mTab = OEMSHIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customs Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on CustomsInformation_ValueAddedTaxInfo_TaxID...", Logger.MessageType.INF);
			Control OEMSHIP_CustomsInformation_ValueAddedTaxInfo_TaxID = new Control("CustomsInformation_ValueAddedTaxInfo_TaxID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,OEMSHIP_CustomsInformation_ValueAddedTaxInfo_TaxID.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainTab);
mTab = OEMSHIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Ship to Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on ShipToAddress_Address_Line1...", Logger.MessageType.INF);
			Control OEMSHIP_ShipToAddress_Address_Line1 = new Control("ShipToAddress_Address_Line1", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LN_1_ADDR']");
			CPCommon.AssertEqual(true,OEMSHIP_ShipToAddress_Address_Line1.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainTab);
mTab = OEMSHIP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("RFID Details");


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on RFIDDetailsLink...", Logger.MessageType.INF);
			Control OEMSHIP_RFIDDetailsLink = new Control("RFIDDetailsLink", "ID", "lnk_1008097_OEMSHIP_OEMTRN_HEADER");
			CPCommon.AssertEqual(true,OEMSHIP_RFIDDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_RFIDDetailsLink);
OEMSHIP_RFIDDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on RFIDDetailsForm...", Logger.MessageType.INF);
			Control OEMSHIP_RFIDDetailsForm = new Control("RFIDDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_RFID_HEADER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_RFIDDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on RFIDDetails2Table...", Logger.MessageType.INF);
			Control OEMSHIP_RFIDDetails2Table = new Control("RFIDDetails2Table", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_RFID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_RFIDDetails2Table.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on RFIDDetails2Form...", Logger.MessageType.INF);
			Control OEMSHIP_RFIDDetails2Form = new Control("RFIDDetails2Form", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_RFID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_RFIDDetails2Form.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_RFIDDetails2Form);
formBttn = OEMSHIP_RFIDDetails2Form.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMSHIP_RFIDDetails2Form.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMSHIP_RFIDDetails2Form.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on RFIDDetails_RFIDDetails_SOLine...", Logger.MessageType.INF);
			Control OEMSHIP_RFIDDetails_RFIDDetails_SOLine = new Control("RFIDDetails_RFIDDetails_SOLine", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_RFID_']/ancestor::form[1]/descendant::*[@id='SO_LN_NO']");
			CPCommon.AssertEqual(true,OEMSHIP_RFIDDetails_RFIDDetails_SOLine.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_RFIDDetailsForm);
formBttn = OEMSHIP_RFIDDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PS Std Text");


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on PSStdTextLink...", Logger.MessageType.INF);
			Control OEMSHIP_PSStdTextLink = new Control("PSStdTextLink", "ID", "lnk_1003609_OEMSHIP_OEMTRN_HEADER");
			CPCommon.AssertEqual(true,OEMSHIP_PSStdTextLink.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_PSStdTextLink);
OEMSHIP_PSStdTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on PSStdTextForm...", Logger.MessageType.INF);
			Control OEMSHIP_PSStdTextForm = new Control("PSStdTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SHIPTRNTEXT_DETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_PSStdTextForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on PSStdTextTable...", Logger.MessageType.INF);
			Control OEMSHIP_PSStdTextTable = new Control("PSStdTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SHIPTRNTEXT_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_PSStdTextTable.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_PSStdTextForm);
formBttn = OEMSHIP_PSStdTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SO Header Documents");


											Driver.SessionLogger.WriteLine("WAWF");


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on WAWFLink...", Logger.MessageType.INF);
			Control OEMSHIP_WAWFLink = new Control("WAWFLink", "ID", "lnk_1008087_OEMSHIP_OEMTRN_HEADER");
			CPCommon.AssertEqual(true,OEMSHIP_WAWFLink.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_WAWFLink);
OEMSHIP_WAWFLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on WAWFForm...", Logger.MessageType.INF);
			Control OEMSHIP_WAWFForm = new Control("WAWFForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SOINVCHDR_WAWF_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_WAWFForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on WAWF_Invoice...", Logger.MessageType.INF);
			Control OEMSHIP_WAWF_Invoice = new Control("WAWF_Invoice", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SOINVCHDR_WAWF_']/ancestor::form[1]/descendant::*[@id='SO_INVC_ID']");
			CPCommon.AssertEqual(true,OEMSHIP_WAWF_Invoice.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_WAWFForm);
formBttn = OEMSHIP_WAWFForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMSHIP_WAWFForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMSHIP_WAWFForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on WAWFTable...", Logger.MessageType.INF);
			Control OEMSHIP_WAWFTable = new Control("WAWFTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SOINVCHDR_WAWF_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_WAWFTable.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_WAWFForm);
formBttn = OEMSHIP_WAWFForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Query to populate records in Child Form and Sub Forms");


												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainForm);
formBttn = OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? OEMSHIP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control OEMSHIP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SHIPTRNISSUE_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on ChildForm_ChildTable...", Logger.MessageType.INF);
			Control OEMSHIP_ChildForm_ChildTable = new Control("ChildForm_ChildTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SHIPTRNISSUE_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_ChildForm_ChildTable.Exists());

											Driver.SessionLogger.WriteLine("Issue Detail");


												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on ChildForm_IssueDetailLink...", Logger.MessageType.INF);
			Control OEMSHIP_ChildForm_IssueDetailLink = new Control("ChildForm_IssueDetailLink", "ID", "lnk_1004379_OEMSHIP_SHIPTRNISSUE_DTL");
			CPCommon.AssertEqual(true,OEMSHIP_ChildForm_IssueDetailLink.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_ChildForm_IssueDetailLink);
OEMSHIP_ChildForm_IssueDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on ChildForm_IssueDetailForm...", Logger.MessageType.INF);
			Control OEMSHIP_ChildForm_IssueDetailForm = new Control("ChildForm_IssueDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SOISSUELN_DETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMSHIP_ChildForm_IssueDetailForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExist on ChildForm_IssueDetailTable...", Logger.MessageType.INF);
			Control OEMSHIP_ChildForm_IssueDetailTable = new Control("ChildForm_IssueDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SOISSUELN_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSHIP_ChildForm_IssueDetailTable.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_ChildForm_IssueDetailForm);
formBttn = OEMSHIP_ChildForm_IssueDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMSHIP_ChildForm_IssueDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMSHIP_ChildForm_IssueDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEMSHIP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSHIP] Perfoming VerifyExists on ChildForm_IssueDetail_SOLine...", Logger.MessageType.INF);
			Control OEMSHIP_ChildForm_IssueDetail_SOLine = new Control("ChildForm_IssueDetail_SOLine", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMSHIP_SOISSUELN_DETAIL_']/ancestor::form[1]/descendant::*[@id='SO_LN_NO']");
			CPCommon.AssertEqual(true,OEMSHIP_ChildForm_IssueDetail_SOLine.Exists());

												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_ChildForm_IssueDetailForm);
formBttn = OEMSHIP_ChildForm_IssueDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


											Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "OEMSHIP";
							CPCommon.WaitControlDisplayed(OEMSHIP_MainForm);
formBttn = OEMSHIP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

