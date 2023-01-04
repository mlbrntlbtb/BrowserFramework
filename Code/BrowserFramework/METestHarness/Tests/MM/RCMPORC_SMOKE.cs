 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RCMPORC_SMOKE : TestScript
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
new Control("Receiving", "xpath","//div[@class='deptItem'][.='Receiving']").Click();
new Control("Receiving", "xpath","//div[@class='navItem'][.='Receiving']").Click();
new Control("Manage Purchase Order Receipts", "xpath","//div[@class='navItem'][.='Manage Purchase Order Receipts']").Click();


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RCMPORC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RCMPORC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_MainForm);
IWebElement formBttn = RCMPORC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RCMPORC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RCMPORC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RCMPORC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_MainForm);
formBttn = RCMPORC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on Identification_Warehouse...", Logger.MessageType.INF);
			Control RCMPORC_Identification_Warehouse = new Control("Identification_Warehouse", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,RCMPORC_Identification_Warehouse.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORM");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control RCMPORC_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMPORC_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMPORC_POLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm);
formBttn = RCMPORC_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on ChildForm_Line...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_Line = new Control("ChildForm_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMPORC_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,RCMPORC_ChildForm_Line.Exists());

											Driver.SessionLogger.WriteLine("POLINEDOCUMENTS");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Click on ChildForm_POLineDocumentsLink...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_POLineDocumentsLink = new Control("ChildForm_POLineDocumentsLink", "ID", "lnk_1007851_RCMPORC_POLN_CTW");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm_POLineDocumentsLink);
RCMPORC_ChildForm_POLineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on POLineDocumentsTable...", Logger.MessageType.INF);
			Control RCMPORC_POLineDocumentsTable = new Control("POLineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_POLineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on POLineDocumentsForm...", Logger.MessageType.INF);
			Control RCMPORC_POLineDocumentsForm = new Control("POLineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_POLineDocumentsForm);
formBttn = RCMPORC_POLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_POLineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_POLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_POLineDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on POLineDocuments_Document...", Logger.MessageType.INF);
			Control RCMPORC_POLineDocuments_Document = new Control("POLineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,RCMPORC_POLineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_POLineDocumentsForm);
formBttn = RCMPORC_POLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("POLINEACCOUNTS");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Click on ChildForm_POLineAccountsLink...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_POLineAccountsLink = new Control("ChildForm_POLineAccountsLink", "ID", "lnk_1004412_RCMPORC_POLN_CTW");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm_POLineAccountsLink);
RCMPORC_ChildForm_POLineAccountsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on POLineAccountsTable...", Logger.MessageType.INF);
			Control RCMPORC_POLineAccountsTable = new Control("POLineAccountsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMPORC_POLNACCT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_POLineAccountsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on POLineAccountsForm...", Logger.MessageType.INF);
			Control RCMPORC_POLineAccountsForm = new Control("POLineAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMPORC_POLNACCT_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_POLineAccountsForm);
formBttn = RCMPORC_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_POLineAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_POLineAccountsForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on POLineAccounts_Project...", Logger.MessageType.INF);
			Control RCMPORC_POLineAccounts_Project = new Control("POLineAccounts_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMPORC_POLNACCT_CTW_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,RCMPORC_POLineAccounts_Project.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_POLineAccountsForm);
formBttn = RCMPORC_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SERIALLOTNFO");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Click on ChildForm_SerialLotInfoLink...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_SerialLotInfoLink = new Control("ChildForm_SerialLotInfoLink", "ID", "lnk_1004413_RCMPORC_POLN_CTW");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm_SerialLotInfoLink);
RCMPORC_ChildForm_SerialLotInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on SerialLotInfoTable...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfoTable = new Control("SerialLotInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_SerialLotInfoTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on SerialLotInfoForm...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfoForm = new Control("SerialLotInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfoForm);
formBttn = RCMPORC_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_SerialLotInfoForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on SerialLotInfo_SerialNumber...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_SerialNumber = new Control("SerialLotInfo_SerialNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='SERIAL_ID']");
			CPCommon.AssertEqual(true,RCMPORC_SerialLotInfo_SerialNumber.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Select on SerialLotInfo_SerialLotInfoTab...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_SerialLotInfoTab = new Control("SerialLotInfo_SerialLotInfoTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfo_SerialLotInfoTab);
IWebElement mTab = RCMPORC_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on SerialLotInfo_BasicInformation_ReceiptQuantity...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_BasicInformation_ReceiptQuantity = new Control("SerialLotInfo_BasicInformation_ReceiptQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='TRN_QTY']");
			CPCommon.AssertEqual(true,RCMPORC_SerialLotInfo_BasicInformation_ReceiptQuantity.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfo_SerialLotInfoTab);
mTab = RCMPORC_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manufacturer/Vendor Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer = new Control("SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,RCMPORC_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfo_SerialLotInfoTab);
mTab = RCMPORC_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Sales Order/Warranty Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrderTagNo...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrderTagNo = new Control("SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrderTagNo", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='SO_TAG_ID']");
			CPCommon.AssertEqual(true,RCMPORC_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrderTagNo.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfo_SerialLotInfoTab);
mTab = RCMPORC_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "User-Defined Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on SerialLotInfo_UID_UIDDetails_UID...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_UID_UIDDetails_UID = new Control("SerialLotInfo_UID_UIDDetails_UID", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='UID_CD']");
			CPCommon.AssertEqual(false,RCMPORC_SerialLotInfo_UID_UIDDetails_UID.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfo_SerialLotInfoTab);
mTab = RCMPORC_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on SerialLotInfo_Notes_Notes...", Logger.MessageType.INF);
			Control RCMPORC_SerialLotInfo_Notes_Notes = new Control("SerialLotInfo_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='NOTES_NT']");
			CPCommon.AssertEqual(true,RCMPORC_SerialLotInfo_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfo_SerialLotInfoTab);
mTab = RCMPORC_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shelf Life").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType = new Control("ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='S_SHELF_LIFE_TYPE']");
			CPCommon.AssertEqual(true,RCMPORC_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_SerialLotInfoForm);
formBttn = RCMPORC_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("FIXEDASSETS");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Click on ChildForm_FixedAssetsLink...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_FixedAssetsLink = new Control("ChildForm_FixedAssetsLink", "ID", "lnk_1004414_RCMPORC_POLN_CTW");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm_FixedAssetsLink);
RCMPORC_ChildForm_FixedAssetsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on FixedAssetsTable...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssetsTable = new Control("FixedAssetsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_FixedAssetsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on FixedAssetsForm...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssetsForm = new Control("FixedAssetsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_FixedAssetsForm);
formBttn = RCMPORC_FixedAssetsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_FixedAssetsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_FixedAssetsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_FixedAssetsForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Select on FixedAssets_FixedAssetsTab...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssets_FixedAssetsTab = new Control("FixedAssets_FixedAssetsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RCMPORC_FixedAssets_FixedAssetsTab);
mTab = RCMPORC_FixedAssets_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc/Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on FixedAssets_DescPurchInfo_DescriptiveInformation_Template...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssets_DescPurchInfo_DescriptiveInformation_Template = new Control("FixedAssets_DescPurchInfo_DescriptiveInformation_Template", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='FA_TMPLT_ID']");
			CPCommon.AssertEqual(true,RCMPORC_FixedAssets_DescPurchInfo_DescriptiveInformation_Template.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_FixedAssets_FixedAssetsTab);
mTab = RCMPORC_FixedAssets_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Location Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup = new Control("FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='FA_LOC_GRP_CD']");
			CPCommon.AssertEqual(true,RCMPORC_FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_FixedAssets_FixedAssetsTab);
mTab = RCMPORC_FixedAssets_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on FixedAssets_GovtInfo_TagNo...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssets_GovtInfo_TagNo = new Control("FixedAssets_GovtInfo_TagNo", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='TAG_NO_S']");
			CPCommon.AssertEqual(true,RCMPORC_FixedAssets_GovtInfo_TagNo.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_FixedAssets_FixedAssetsTab);
mTab = RCMPORC_FixedAssets_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "UID").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on FixedAssets_UID_UIDDetails_UID...", Logger.MessageType.INF);
			Control RCMPORC_FixedAssets_UID_UIDDetails_UID = new Control("FixedAssets_UID_UIDDetails_UID", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='UID_CD']");
			CPCommon.AssertEqual(true,RCMPORC_FixedAssets_UID_UIDDetails_UID.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_FixedAssetsForm);
formBttn = RCMPORC_FixedAssetsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PARTDOCUMENTS");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Click on ChildForm_PartDocumentsLink...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_PartDocumentsLink = new Control("ChildForm_PartDocumentsLink", "ID", "lnk_1004415_RCMPORC_POLN_CTW");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm_PartDocumentsLink);
RCMPORC_ChildForm_PartDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on PartDocumentsTable...", Logger.MessageType.INF);
			Control RCMPORC_PartDocumentsTable = new Control("PartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_PartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on PartDocumentsForm...", Logger.MessageType.INF);
			Control RCMPORC_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_PartDocumentsForm);
formBttn = RCMPORC_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control RCMPORC_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,RCMPORC_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_PartDocumentsForm);
formBttn = RCMPORC_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("BACKORDERS");


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming Click on ChildForm_BackOrdersLink...", Logger.MessageType.INF);
			Control RCMPORC_ChildForm_BackOrdersLink = new Control("ChildForm_BackOrdersLink", "ID", "lnk_2906_RCMPORC_POLN_CTW");
			CPCommon.WaitControlDisplayed(RCMPORC_ChildForm_BackOrdersLink);
RCMPORC_ChildForm_BackOrdersLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExist on BackOrdersTable...", Logger.MessageType.INF);
			Control RCMPORC_BackOrdersTable = new Control("BackOrdersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGBKORD_RESLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMPORC_BackOrdersTable.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming ClickButton on BackOrdersForm...", Logger.MessageType.INF);
			Control RCMPORC_BackOrdersForm = new Control("BackOrdersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGBKORD_RESLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMPORC_BackOrdersForm);
formBttn = RCMPORC_BackOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMPORC_BackOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMPORC_BackOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.AssertEqual(true,RCMPORC_BackOrdersForm.Exists());

													
				CPCommon.CurrentComponent = "RCMPORC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMPORC] Perfoming VerifyExists on BackOrders_NeedDate...", Logger.MessageType.INF);
			Control RCMPORC_BackOrders_NeedDate = new Control("BackOrders_NeedDate", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGBKORD_RESLN_CTW_']/ancestor::form[1]/descendant::*[@id='NEED_DT']");
			CPCommon.AssertEqual(true,RCMPORC_BackOrders_NeedDate.Exists());

												
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_BackOrdersForm);
formBttn = RCMPORC_BackOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RCMPORC";
							CPCommon.WaitControlDisplayed(RCMPORC_MainForm);
formBttn = RCMPORC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

