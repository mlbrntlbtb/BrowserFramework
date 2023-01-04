 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRFQI_SMOKE : TestScript
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
new Control("Vendor Quotes", "xpath","//div[@class='navItem'][.='Vendor Quotes']").Click();
new Control("Manage Request for Quotes By Item", "xpath","//div[@class='navItem'][.='Manage Request for Quotes By Item']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRFQI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on Header_ItemInformation_Rev...", Logger.MessageType.INF);
			Control PPMRFQI_Header_ItemInformation_Rev = new Control("Header_ItemInformation_Rev", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REV']");
			CPCommon.AssertEqual(true,PPMRFQI_Header_ItemInformation_Rev.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control PPMRFQI_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRFQI_MainTab);
IWebElement mTab = PPMRFQI_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Set on Header_ItemInformation_Item...", Logger.MessageType.INF);
			Control PPMRFQI_Header_ItemInformation_Item = new Control("Header_ItemInformation_Item", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			PPMRFQI_Header_ItemInformation_Item.Click();
PPMRFQI_Header_ItemInformation_Item.SendKeys(" 3108C28-24", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PPMRFQI_Header_ItemInformation_Item.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("Invalid object Id: undefined.");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Set on Header_Buyer...", Logger.MessageType.INF);
			Control PPMRFQI_Header_Buyer = new Control("Header_Buyer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BUYER']");
			PPMRFQI_Header_Buyer.Click();
PPMRFQI_Header_Buyer.SendKeys("KK1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PPMRFQI_Header_Buyer.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_MainTab);
mTab = PPMRFQI_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Defaults").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on LineDefaults_DesiredLeadTimeARO...", Logger.MessageType.INF);
			Control PPMRFQI_LineDefaults_DesiredLeadTimeARO = new Control("LineDefaults_DesiredLeadTimeARO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_LT_DAYS_NO']");
			CPCommon.AssertEqual(true,PPMRFQI_LineDefaults_DesiredLeadTimeARO.Exists());

											Driver.SessionLogger.WriteLine("DefaultLineQuantity");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Click on DefaultLineQuantityBreakpointsFormLink...", Logger.MessageType.INF);
			Control PPMRFQI_DefaultLineQuantityBreakpointsFormLink = new Control("DefaultLineQuantityBreakpointsFormLink", "ID", "lnk_1004734_PPMRFQI_RFQHDR");
			CPCommon.WaitControlDisplayed(PPMRFQI_DefaultLineQuantityBreakpointsFormLink);
PPMRFQI_DefaultLineQuantityBreakpointsFormLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExist on DefaultLineQuantityBreakpointsFormTable...", Logger.MessageType.INF);
			Control PPMRFQI_DefaultLineQuantityBreakpointsFormTable = new Control("DefaultLineQuantityBreakpointsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_QUANBRK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQI_DefaultLineQuantityBreakpointsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on DefaultLineQuantityBreakpointsForm...", Logger.MessageType.INF);
			Control PPMRFQI_DefaultLineQuantityBreakpointsForm = new Control("DefaultLineQuantityBreakpointsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_QUANBRK_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_DefaultLineQuantityBreakpointsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_DefaultLineQuantityBreakpointsForm);
IWebElement formBttn = PPMRFQI_DefaultLineQuantityBreakpointsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("rfqLines");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLinesForm...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLinesForm = new Control("RfqLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLinesForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExist on RfqLinesFormTable...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLinesFormTable = new Control("RfqLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLinesForm);
formBttn = PPMRFQI_RfqLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRFQI_RfqLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRFQI_RfqLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_RfqDetails_Rfq...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_RfqDetails_Rfq = new Control("RfqLines_RfqDetails_Rfq", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]/descendant::*[@id='RFQ_ID']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_RfqDetails_Rfq.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Select on RfqTab...", Logger.MessageType.INF);
			Control PPMRFQI_RfqTab = new Control("RfqTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRFQI_RfqTab);
mTab = PPMRFQI_RfqTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Delivery").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_Delivery_Delivery_DesiredLeadTimeARO...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_Delivery_Delivery_DesiredLeadTimeARO = new Control("RfqLines_Delivery_Delivery_DesiredLeadTimeARO", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]/descendant::*[@id='LT_DAYS_ARO_NO']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_Delivery_Delivery_DesiredLeadTimeARO.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqTab);
mTab = PPMRFQI_RfqTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manuf/Vend Parts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_ManufVendParts_ManufVendParts_Manufacturer...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_ManufVendParts_ManufVendParts_Manufacturer = new Control("RfqLines_ManufVendParts_ManufVendParts_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_ManufVendParts_ManufVendParts_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqTab);
mTab = PPMRFQI_RfqTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_Notes_RfqHeaderNotes...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_Notes_RfqHeaderNotes = new Control("RfqLines_Notes_RfqHeaderNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLN_CHLD_']/ancestor::form[1]/descendant::*[@id='RFQ_NOTES']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_Notes_RfqHeaderNotes.Exists());

											Driver.SessionLogger.WriteLine("Header Standard Text");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Click on RfqLines_HeaderStandardTextLink...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderStandardTextLink = new Control("RfqLines_HeaderStandardTextLink", "ID", "lnk_3602_PPMRFQI_RFQLN_CHLD");
			CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_HeaderStandardTextLink);
PPMRFQI_RfqLines_HeaderStandardTextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_HeaderStandardTextForm...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderStandardTextForm = new Control("RfqLines_HeaderStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_HDRTXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_HeaderStandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExist on RfqLines_HeaderStandardTextFormTable...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderStandardTextFormTable = new Control("RfqLines_HeaderStandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_HDRTXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_HeaderStandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_HeaderStandardTextForm);
formBttn = PPMRFQI_RfqLines_HeaderStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Header Documents");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Click on RfqLines_HeaderDocumentsLink...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderDocumentsLink = new Control("RfqLines_HeaderDocumentsLink", "ID", "lnk_1007839_PPMRFQI_RFQLN_CHLD");
			CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_HeaderDocumentsLink);
PPMRFQI_RfqLines_HeaderDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_HeaderDocumentsForm...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderDocumentsForm = new Control("RfqLines_HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQHDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_HeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExist on RfqLines_HeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderDocumentsFormTable = new Control("RfqLines_HeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQHDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_HeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_HeaderDocumentsForm);
formBttn = PPMRFQI_RfqLines_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRFQI_RfqLines_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRFQI_RfqLines_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_HeaderDocuments_Rev...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_HeaderDocuments_Rev = new Control("RfqLines_HeaderDocuments_Rev", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQHDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_RVSN_ID']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_HeaderDocuments_Rev.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_HeaderDocumentsForm);
formBttn = PPMRFQI_RfqLines_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Vendor Item Info");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Click on Rfq_VendorItemInformationLink...", Logger.MessageType.INF);
			Control PPMRFQI_Rfq_VendorItemInformationLink = new Control("Rfq_VendorItemInformationLink", "ID", "lnk_1003378_PPMRFQI_RFQLN_CHLD");
			CPCommon.WaitControlDisplayed(PPMRFQI_Rfq_VendorItemInformationLink);
PPMRFQI_Rfq_VendorItemInformationLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_VendorItemInformationForm...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_VendorItemInformationForm = new Control("RfqLines_VendorItemInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_VEND_VINF_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_VendorItemInformationForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_VendorItemInformation_Vendor...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_VendorItemInformation_Vendor = new Control("RfqLines_VendorItemInformation_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_VEND_VINF_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_VendorItemInformation_Vendor.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_VendorItemInformationForm);
formBttn = PPMRFQI_RfqLines_VendorItemInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Quantity");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Click on RfqLines_LineQuantityBreakpointsLink...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineQuantityBreakpointsLink = new Control("RfqLines_LineQuantityBreakpointsLink", "ID", "lnk_1003379_PPMRFQI_RFQLN_CHLD");
			CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_LineQuantityBreakpointsLink);
PPMRFQI_RfqLines_LineQuantityBreakpointsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_LineQuantityBreakpointsForm...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineQuantityBreakpointsForm = new Control("RfqLines_LineQuantityBreakpointsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLNBRK_QUBR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_LineQuantityBreakpointsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExist on RfqLines_LineQuantityBreakpointsFormTable...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineQuantityBreakpointsFormTable = new Control("RfqLines_LineQuantityBreakpointsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQI_RFQLNBRK_QUBR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_LineQuantityBreakpointsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_LineQuantityBreakpointsForm);
formBttn = PPMRFQI_RfqLines_LineQuantityBreakpointsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Quantity");


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming Click on RfqLines_LineDocumentsLink...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineDocumentsLink = new Control("RfqLines_LineDocumentsLink", "ID", "lnk_1007831_PPMRFQI_RFQLN_CHLD");
			CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_LineDocumentsLink);
PPMRFQI_RfqLines_LineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_LineDocumentsForm...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineDocumentsForm = new Control("RfqLines_LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQLN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExist on RfqLines_LineDocumentsFormTable...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineDocumentsFormTable = new Control("RfqLines_LineDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_LineDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_RfqLines_LineDocumentsForm);
formBttn = PPMRFQI_RfqLines_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRFQI_RfqLines_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRFQI_RfqLines_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRFQI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQI] Perfoming VerifyExists on RfqLines_LineDocuments_Document...", Logger.MessageType.INF);
			Control PPMRFQI_RfqLines_LineDocuments_Document = new Control("RfqLines_LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMRFQI_RfqLines_LineDocuments_Document.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMRFQI";
							CPCommon.WaitControlDisplayed(PPMRFQI_MainForm);
formBttn = PPMRFQI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

