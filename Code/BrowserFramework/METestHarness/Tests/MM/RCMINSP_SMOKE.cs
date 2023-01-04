 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RCMINSP_SMOKE : TestScript
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
new Control("Manage Quality Control Inspections", "xpath","//div[@class='navItem'][.='Manage Quality Control Inspections']").Click();


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RCMINSP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RCMINSP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on MainForm_Identification_Warehouse...", Logger.MessageType.INF);
			Control RCMINSP_MainForm_Identification_Warehouse = new Control("MainForm_Identification_Warehouse", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,RCMINSP_MainForm_Identification_Warehouse.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_MainForm);
IWebElement formBttn = RCMINSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RCMINSP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RCMINSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RCMINSP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_MainForm);
formBttn = RCMINSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("POHEADERDOCUMENTS");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on MainForm_POHeaderDocumentsLink...", Logger.MessageType.INF);
			Control RCMINSP_MainForm_POHeaderDocumentsLink = new Control("MainForm_POHeaderDocumentsLink", "id", "lnk_1007809_RCMINSP_INSPHDR_QCINSPECTIONS");
			CPCommon.WaitControlDisplayed(RCMINSP_MainForm_POHeaderDocumentsLink);
RCMINSP_MainForm_POHeaderDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on POHeaderDocumentsForm...", Logger.MessageType.INF);
			Control RCMINSP_POHeaderDocumentsForm = new Control("POHeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RCMINSP_POHeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on POHeaderDocuments_Document...", Logger.MessageType.INF);
			Control RCMINSP_POHeaderDocuments_Document = new Control("POHeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,RCMINSP_POHeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_POHeaderDocumentsForm);
formBttn = RCMINSP_POHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RCMINSP_POHeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RCMINSP_POHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on POHeaderDocumentsTable...", Logger.MessageType.INF);
			Control RCMINSP_POHeaderDocumentsTable = new Control("POHeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_POHeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_POHeaderDocumentsForm);
formBttn = RCMINSP_POHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control RCMINSP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm);
formBttn = RCMINSP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on ChildForm_POLine...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_POLine = new Control("ChildForm_POLine", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,RCMINSP_ChildForm_POLine.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Select on ChildFormTab...", Logger.MessageType.INF);
			Control RCMINSP_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildFormTab);
IWebElement mTab = RCMINSP_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "QC Inspection Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on ChildForm_QCInspectionDetails_InspectionReportID...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_QCInspectionDetails_InspectionReportID = new Control("ChildForm_QCInspectionDetails_InspectionReportID", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]/descendant::*[@id='INSP_RPT_ID']");
			CPCommon.AssertEqual(true,RCMINSP_ChildForm_QCInspectionDetails_InspectionReportID.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_ChildFormTab);
mTab = RCMINSP_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Rejections").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on ChildForm_Rejections_RejectedQtyInvUM...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_Rejections_RejectedQtyInvUM = new Control("ChildForm_Rejections_RejectedQtyInvUM", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]/descendant::*[@id='REJ_QTY_INV']");
			CPCommon.AssertEqual(true,RCMINSP_ChildForm_Rejections_RejectedQtyInvUM.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_ChildFormTab);
mTab = RCMINSP_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PO Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on ChildForm_POLineDetails_QCInspectionReqd...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_POLineDetails_QCInspectionReqd = new Control("ChildForm_POLineDetails_QCInspectionReqd", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLN_INSPDETAILS_']/ancestor::form[1]/descendant::*[@id='QC_REQD_FL']");
			CPCommon.AssertEqual(true,RCMINSP_ChildForm_POLineDetails_QCInspectionReqd.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_ChildFormTab);
mTab = RCMINSP_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Inspection Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("REJECTIONINFO");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_RejectionInfoLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_RejectionInfoLink = new Control("ChildForm_RejectionInfoLink", "ID", "lnk_1004107_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_RejectionInfoLink);
RCMINSP_ChildForm_RejectionInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on RejectionInfoTable...", Logger.MessageType.INF);
			Control RCMINSP_RejectionInfoTable = new Control("RejectionInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLNREJ_REJECTIONINF_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_RejectionInfoTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on RejectionInfoForm...", Logger.MessageType.INF);
			Control RCMINSP_RejectionInfoForm = new Control("RejectionInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLNREJ_REJECTIONINF_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_RejectionInfoForm);
formBttn = RCMINSP_RejectionInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_RejectionInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_RejectionInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_RejectionInfoForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on RejectionInfo_RejectedQtyPOUM...", Logger.MessageType.INF);
			Control RCMINSP_RejectionInfo_RejectedQtyPOUM = new Control("RejectionInfo_RejectedQtyPOUM", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_INSPLNREJ_REJECTIONINF_']/ancestor::form[1]/descendant::*[@id='REJ_QTY']");
			CPCommon.AssertEqual(true,RCMINSP_RejectionInfo_RejectedQtyPOUM.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_RejectionInfoForm);
formBttn = RCMINSP_RejectionInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SERIAL/LOTINFO");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_SerialLotInfoLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_SerialLotInfoLink = new Control("ChildForm_SerialLotInfoLink", "ID", "lnk_1004108_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_SerialLotInfoLink);
RCMINSP_ChildForm_SerialLotInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on SerialLotInfoTable...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfoTable = new Control("SerialLotInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_SerialLotInfoTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on SerialLotInfoForm...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfoForm = new Control("SerialLotInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoForm);
formBttn = RCMINSP_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_SerialLotInfoForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on SerialLotInfo_SerialNumber...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfo_SerialNumber = new Control("SerialLotInfo_SerialNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='SERIAL_ID']");
			CPCommon.AssertEqual(true,RCMINSP_SerialLotInfo_SerialNumber.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Select on SerialLotInfoTab...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfoTab = new Control("SerialLotInfoTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoTab);
mTab = RCMINSP_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on SerialLotInfo_BasicInformation_ReceiptQuantity...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfo_BasicInformation_ReceiptQuantity = new Control("SerialLotInfo_BasicInformation_ReceiptQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='TRN_QTY']");
			CPCommon.AssertEqual(true,RCMINSP_SerialLotInfo_BasicInformation_ReceiptQuantity.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoTab);
mTab = RCMINSP_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manufacturer/Vendor Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer = new Control("SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,RCMINSP_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoTab);
mTab = RCMINSP_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Sales Order/Warranty Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder = new Control("SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='MAINT_SO_ID']");
			CPCommon.AssertEqual(true,RCMINSP_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoTab);
mTab = RCMINSP_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "User-Defined Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on SerialLotInfo_UID_UIDDetails_UID...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfo_UID_UIDDetails_UID = new Control("SerialLotInfo_UID_UIDDetails_UID", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='UID_CD']");
			CPCommon.AssertEqual(false,RCMINSP_SerialLotInfo_UID_UIDDetails_UID.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoTab);
mTab = RCMINSP_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on SerialLotInfo_Notes_Notes...", Logger.MessageType.INF);
			Control RCMINSP_SerialLotInfo_Notes_Notes = new Control("SerialLotInfo_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='NOTES_NT']");
			CPCommon.AssertEqual(true,RCMINSP_SerialLotInfo_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoTab);
mTab = RCMINSP_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shelf Life").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType = new Control("ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSRLT_INVTTRNLNSRLT_COMMON_']/ancestor::form[1]/descendant::*[@id='S_SHELF_LIFE_TYPE']");
			CPCommon.AssertEqual(true,RCMINSP_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_SerialLotInfoForm);
formBttn = RCMINSP_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("FIXEDASSETS");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_FixedAssetsLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_FixedAssetsLink = new Control("ChildForm_FixedAssetsLink", "ID", "lnk_1004109_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_FixedAssetsLink);
RCMINSP_ChildForm_FixedAssetsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on FixedAssetsTable...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssetsTable = new Control("FixedAssetsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_FixedAssetsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on FixedAssetsForm...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssetsForm = new Control("FixedAssetsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_FixedAssetsForm);
formBttn = RCMINSP_FixedAssetsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_FixedAssetsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_FixedAssetsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_FixedAssetsForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Select on FixedAssetsTab...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssetsTab = new Control("FixedAssetsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(RCMINSP_FixedAssetsTab);
mTab = RCMINSP_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Desc/Purch Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on FixedAssets_DescPurchInfo_PurchaseInformation_UnitOfMeasure...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssets_DescPurchInfo_PurchaseInformation_UnitOfMeasure = new Control("FixedAssets_DescPurchInfo_PurchaseInformation_UnitOfMeasure", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='UM_S']");
			CPCommon.AssertEqual(true,RCMINSP_FixedAssets_DescPurchInfo_PurchaseInformation_UnitOfMeasure.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_FixedAssetsTab);
mTab = RCMINSP_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Location Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup = new Control("FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='FA_LOC_GRP_CD']");
			CPCommon.AssertEqual(true,RCMINSP_FixedAssets_LocationInfo_LocationGroupInfo_LocationGroup.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_FixedAssetsTab);
mTab = RCMINSP_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Govt Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on FixedAssets_GovtInfo_TagNo...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssets_GovtInfo_TagNo = new Control("FixedAssets_GovtInfo_TagNo", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='TAG_NO_S']");
			CPCommon.AssertEqual(true,RCMINSP_FixedAssets_GovtInfo_TagNo.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_FixedAssetsTab);
mTab = RCMINSP_FixedAssetsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "UID").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on FixedAssets_UID_UIDDetails_UID...", Logger.MessageType.INF);
			Control RCMINSP_FixedAssets_UID_UIDDetails_UID = new Control("FixedAssets_UID_UIDDetails_UID", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMAUTOC_DTL_']/ancestor::form[1]/descendant::*[@id='UID_CD']");
			CPCommon.AssertEqual(true,RCMINSP_FixedAssets_UID_UIDDetails_UID.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_FixedAssetsForm);
formBttn = RCMINSP_FixedAssetsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ACCOUNTDISTRIBUTION");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_AccountDistributionLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_AccountDistributionLink = new Control("ChildForm_AccountDistributionLink", "ID", "lnk_1004110_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_AccountDistributionLink);
RCMINSP_ChildForm_AccountDistributionLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on AccountDistributionTable...", Logger.MessageType.INF);
			Control RCMINSP_AccountDistributionTable = new Control("AccountDistributionTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_POLNACCT_ACCTDISTRIBUT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_AccountDistributionTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on AccountDistributionForm...", Logger.MessageType.INF);
			Control RCMINSP_AccountDistributionForm = new Control("AccountDistributionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_POLNACCT_ACCTDISTRIBUT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_AccountDistributionForm);
formBttn = RCMINSP_AccountDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_AccountDistributionForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_AccountDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_AccountDistributionForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on AccountDistribution_Project...", Logger.MessageType.INF);
			Control RCMINSP_AccountDistribution_Project = new Control("AccountDistribution_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_POLNACCT_ACCTDISTRIBUT_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,RCMINSP_AccountDistribution_Project.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_AccountDistributionForm);
formBttn = RCMINSP_AccountDistributionForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("POLINETEXT");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_POLineTextLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_POLineTextLink = new Control("ChildForm_POLineTextLink", "ID", "lnk_1004111_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_POLineTextLink);
RCMINSP_ChildForm_POLineTextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on POLineTextTable...", Logger.MessageType.INF);
			Control RCMINSP_POLineTextTable = new Control("POLineTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_POLNTEXT_POLINESTDTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_POLineTextTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on POLineTextForm...", Logger.MessageType.INF);
			Control RCMINSP_POLineTextForm = new Control("POLineTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_POLNTEXT_POLINESTDTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_POLineTextForm);
formBttn = RCMINSP_POLineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_POLineTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_POLineTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_POLineTextForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on POLineText_TextCode...", Logger.MessageType.INF);
			Control RCMINSP_POLineText_TextCode = new Control("POLineText_TextCode", "xpath", "//div[translate(@id,'0123456789','')='pr__RCMINSP_POLNTEXT_POLINESTDTEXT_']/ancestor::form[1]/descendant::*[@id='TEXT_CD']");
			CPCommon.AssertEqual(true,RCMINSP_POLineText_TextCode.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_POLineTextForm);
formBttn = RCMINSP_POLineTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("POLINEDOCUMENTS");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_POLineDocumentsLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_POLineDocumentsLink = new Control("ChildForm_POLineDocumentsLink", "ID", "lnk_1007808_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_POLineDocumentsLink);
RCMINSP_ChildForm_POLineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on POLineDocumentsForm...", Logger.MessageType.INF);
			Control RCMINSP_POLineDocumentsForm = new Control("POLineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RCMINSP_POLineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on POLineDocuments_Document...", Logger.MessageType.INF);
			Control RCMINSP_POLineDocuments_Document = new Control("POLineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,RCMINSP_POLineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_POLineDocumentsForm);
formBttn = RCMINSP_POLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RCMINSP_POLineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RCMINSP_POLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on POLineDocumentsTable...", Logger.MessageType.INF);
			Control RCMINSP_POLineDocumentsTable = new Control("POLineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RCM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_POLineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_POLineDocumentsForm);
formBttn = RCMINSP_POLineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PARTDOCUMENTS");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_PartDocumentsLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_PartDocumentsLink = new Control("ChildForm_PartDocumentsLink", "ID", "lnk_1004112_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_PartDocumentsLink);
RCMINSP_ChildForm_PartDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on PartDocumentsTable...", Logger.MessageType.INF);
			Control RCMINSP_PartDocumentsTable = new Control("PartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_PartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on PartDocumentsForm...", Logger.MessageType.INF);
			Control RCMINSP_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_PartDocumentsForm);
formBttn = RCMINSP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control RCMINSP_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,RCMINSP_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_PartDocumentsForm);
formBttn = RCMINSP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("BACKORDERS");


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming Click on ChildForm_BackOrdersLink...", Logger.MessageType.INF);
			Control RCMINSP_ChildForm_BackOrdersLink = new Control("ChildForm_BackOrdersLink", "ID", "lnk_2905_RCMINSP_INSPLN_INSPDETAILS");
			CPCommon.WaitControlDisplayed(RCMINSP_ChildForm_BackOrdersLink);
RCMINSP_ChildForm_BackOrdersLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExist on BackOrdersTable...", Logger.MessageType.INF);
			Control RCMINSP_BackOrdersTable = new Control("BackOrdersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGBKORD_RESLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMINSP_BackOrdersTable.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming ClickButton on BackOrdersForm...", Logger.MessageType.INF);
			Control RCMINSP_BackOrdersForm = new Control("BackOrdersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGBKORD_RESLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(RCMINSP_BackOrdersForm);
formBttn = RCMINSP_BackOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RCMINSP_BackOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RCMINSP_BackOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.AssertEqual(true,RCMINSP_BackOrdersForm.Exists());

													
				CPCommon.CurrentComponent = "RCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMINSP] Perfoming VerifyExists on BackOrders_NeedDate...", Logger.MessageType.INF);
			Control RCMINSP_BackOrders_NeedDate = new Control("BackOrders_NeedDate", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGBKORD_RESLN_CTW_']/ancestor::form[1]/descendant::*[@id='NEED_DT']");
			CPCommon.AssertEqual(true,RCMINSP_BackOrders_NeedDate.Exists());

												
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_BackOrdersForm);
formBttn = RCMINSP_BackOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RCMINSP";
							CPCommon.WaitControlDisplayed(RCMINSP_MainForm);
formBttn = RCMINSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

