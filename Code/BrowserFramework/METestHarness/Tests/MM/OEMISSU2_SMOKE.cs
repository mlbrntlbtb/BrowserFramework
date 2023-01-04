 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMISSU2_SMOKE : TestScript
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
new Control("Sales Order Material Processing", "xpath","//div[@class='navItem'][.='Sales Order Material Processing']").Click();
new Control("Manage Sales Order Non-Inventory Issues", "xpath","//div[@class='navItem'][.='Manage Sales Order Non-Inventory Issues']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMISSU2_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMISSU2_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on Identification_IssueID...", Logger.MessageType.INF);
			Control OEMISSU2_Identification_IssueID = new Control("Identification_IssueID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SO_ISSUE_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_Identification_IssueID.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_MainForm);
IWebElement formBttn = OEMISSU2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMISSU2_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMISSU2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMISSU2_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU2_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on AutoloadTable...", Logger.MessageType.INF);
			Control OEMISSU2_AutoloadTable = new Control("AutoloadTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PB_AUTOLOADISSUE___T']");
			CPCommon.AssertEqual(true,OEMISSU2_AutoloadTable.Exists());

											Driver.SessionLogger.WriteLine("Accounting Period Form");


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on AccountingPeriodLink...", Logger.MessageType.INF);
			Control OEMISSU2_AccountingPeriodLink = new Control("AccountingPeriodLink", "ID", "lnk_3243_OEMISSU_SOISSUEHDR_SALESISSUE");
			CPCommon.AssertEqual(true,OEMISSU2_AccountingPeriodLink.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_AccountingPeriodLink);
OEMISSU2_AccountingPeriodLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on AccountingPeriodForm...", Logger.MessageType.INF);
			Control OEMISSU2_AccountingPeriodForm = new Control("AccountingPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMISSU2_AccountingPeriodForm.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on AccountingPeriod_FiscalYear...", Logger.MessageType.INF);
			Control OEMISSU2_AccountingPeriod_FiscalYear = new Control("AccountingPeriod_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='DFS_FYCD_ST']");
			CPCommon.AssertEqual(true,OEMISSU2_AccountingPeriod_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_AccountingPeriodForm);
formBttn = OEMISSU2_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMISSU2_AccountingPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMISSU2_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExist on AccountingPeriodFormTable...", Logger.MessageType.INF);
			Control OEMISSU2_AccountingPeriodFormTable = new Control("AccountingPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU2_AccountingPeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_AccountingPeriodForm);
formBttn = OEMISSU2_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Header Documents Form");


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control OEMISSU2_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007711_OEMISSU_SOISSUEHDR_SALESISSUE");
			CPCommon.AssertEqual(true,OEMISSU2_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_HeaderDocumentsLink);
OEMISSU2_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExist on HeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control OEMISSU2_HeaderDocumentsFormTable = new Control("HeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU2_HeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming ClickButton on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control OEMISSU2_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU2_HeaderDocumentsForm);
formBttn = OEMISSU2_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU2_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU2_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.AssertEqual(true,OEMISSU2_HeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on HeaderDocuments_Document...", Logger.MessageType.INF);
			Control OEMISSU2_HeaderDocuments_Document = new Control("HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_HeaderDocumentsForm);
formBttn = OEMISSU2_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SO Issue Lines");


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control OEMISSU2_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm);
formBttn = OEMISSU2_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU2_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU2_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.AssertEqual(true,OEMISSU2_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SOLine...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SOLine = new Control("ChildForm_SOLine", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='SO_LN_NO']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SOLine.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming Select on ChildFormTab...", Logger.MessageType.INF);
			Control OEMISSU2_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMISSU2_ChildFormTab);
IWebElement mTab = OEMISSU2_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Issue Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_IssueDetails_Item...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_IssueDetails_Item = new Control("ChildForm_IssueDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_IssueDetails_Item.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildFormTab);
mTab = OEMISSU2_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "SO Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SOLineDetails_SOUM...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SOLineDetails_SOUM = new Control("ChildForm_SOLineDetails_SOUM", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='UM_CD']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SOLineDetails_SOUM.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildFormTab);
mTab = OEMISSU2_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_Notes_Notes...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_Notes_Notes = new Control("ChildForm_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_WORKTABLE_TEMP_']/ancestor::form[1]/descendant::*[@id='SO_ISSUE_LN_NOTES']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_Notes_Notes.Exists());

											Driver.SessionLogger.WriteLine("Serial/Lot Info Form");


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfoLink...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfoLink = new Control("ChildForm_SerialLotInfoLink", "ID", "lnk_1007510_OEMISSU_WORKTABLE_TEMP");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfoLink.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfoLink);
OEMISSU2_ChildForm_SerialLotInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExist on ChildForm_SerialLotInfoFormTable...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfoFormTable = new Control("ChildForm_SerialLotInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_GenerateUIDsTable...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_GenerateUIDsTable = new Control("ChildForm_SerialLotInfo_GenerateUIDsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='GENERATE_UID___T']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_GenerateUIDsTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming ClickButton on ChildForm_SerialLotInfoForm...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfoForm = new Control("ChildForm_SerialLotInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfoForm);
formBttn = OEMISSU2_ChildForm_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU2_ChildForm_SerialLotInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU2_ChildForm_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfoForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_SerialNumber...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_SerialNumber = new Control("ChildForm_SerialLotInfo_SerialNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='SERIAL_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_SerialNumber.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming Select on ChildForm_SerialLotInfo_SerialLotInfoTab...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab = new Control("ChildForm_SerialLotInfo_SerialLotInfoTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab);
mTab = OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_BasicInformation_AvailableQuantity...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_BasicInformation_AvailableQuantity = new Control("ChildForm_SerialLotInfo_BasicInformation_AvailableQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='TOT_IWL_QTY']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_BasicInformation_AvailableQuantity.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab);
mTab = OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manufacturer/Vendor Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer = new Control("ChildForm_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_ManufacturerVendorInformation_Manufacturer_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab);
mTab = OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Sales Order/Warranty Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder = new Control("ChildForm_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='MAINT_SO_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_SalesOrderWarrantyInformation_SalesOrderInformation_SalesOrder.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab);
mTab = OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_Notes_Notes...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_Notes_Notes = new Control("ChildForm_SerialLotInfo_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='NOTES_NT']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab);
mTab = OEMISSU2_ChildForm_SerialLotInfo_SerialLotInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Shelf Life").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType = new Control("ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMISSU_INVTTRNLNSRLT_CHILD_']/ancestor::form[1]/descendant::*[@id='S_SHELF_LIFE_TYPE']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_SerialLotInfo_ShelfLife_ShelfLife_ShelfLifeType.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_SerialLotInfoForm);
formBttn = OEMISSU2_ChildForm_SerialLotInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_LineDocumentsLink...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_LineDocumentsLink = new Control("ChildForm_LineDocumentsLink", "ID", "lnk_1007712_OEMISSU_WORKTABLE_TEMP");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_LineDocumentsLink);
OEMISSU2_ChildForm_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExist on ChildForm_LineDocumentsFormTable...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_LineDocumentsFormTable = new Control("ChildForm_LineDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_LineDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming ClickButton on ChildForm_LineDocumentsForm...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_LineDocumentsForm = new Control("ChildForm_LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_LineDocumentsForm);
formBttn = OEMISSU2_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMISSU2_ChildForm_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMISSU2_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.AssertEqual(true,OEMISSU2_ChildForm_LineDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "OEMISSU2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMISSU2] Perfoming VerifyExists on ChildForm_LineDocuments_Document...", Logger.MessageType.INF);
			Control OEMISSU2_ChildForm_LineDocuments_Document = new Control("ChildForm_LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEM_SOLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEMISSU2_ChildForm_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_ChildForm_LineDocumentsForm);
formBttn = OEMISSU2_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "OEMISSU2";
							CPCommon.WaitControlDisplayed(OEMISSU2_MainForm);
formBttn = OEMISSU2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

