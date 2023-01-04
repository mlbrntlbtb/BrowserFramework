 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POQSTAT_SMOKE : TestScript
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
new Control("Purchasing Reports/Inquiries", "xpath","//div[@class='navItem'][.='Purchasing Reports/Inquiries']").Click();
new Control("View Purchase Order Status", "xpath","//div[@class='navItem'][.='View Purchase Order Status']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POQSTAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POQSTAT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on Vendor...", Logger.MessageType.INF);
			Control POQSTAT_Vendor = new Control("Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,POQSTAT_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POHDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQSTAT_ChildForm);
IWebElement formBttn = POQSTAT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? POQSTAT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
POQSTAT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Set on Find_CriteriaValue1...", Logger.MessageType.INF);
			Control Query_Find_CriteriaValue1 = new Control("Find_CriteriaValue1", "ID", "basicField0");
			Query_Find_CriteriaValue1.Click();
Query_Find_CriteriaValue1.SendKeys("A", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
Query_Find_CriteriaValue1.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control POQSTAT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POHDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQSTAT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm);
formBttn = POQSTAT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQSTAT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQSTAT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.AssertEqual(true,POQSTAT_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POHDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_ChildFormTab);
IWebElement mTab = POQSTAT_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "PO Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PODetails_PurchaseOrder...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PODetails_PurchaseOrder = new Control("ChildForm_PODetails_PurchaseOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POHDR_']/ancestor::form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PODetails_PurchaseOrder.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_ChildFormTab);
mTab = POQSTAT_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Vendor Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_VendorDetails_Vendor_Vendor...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_VendorDetails_Vendor_Vendor = new Control("ChildForm_VendorDetails_Vendor_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POHDR_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_VendorDetails_Vendor_Vendor.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_ChildFormTab);
mTab = POQSTAT_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_Notes_HeaderNotes...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_Notes_HeaderNotes = new Control("ChildForm_Notes_HeaderNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POHDR_']/ancestor::form[1]/descendant::*[@id='PO_HDR_TX']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_Notes_HeaderNotes.Exists());

											Driver.SessionLogger.WriteLine("Header Text");


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_HeaderTextLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderTextLink = new Control("ChildForm_HeaderTextLink", "ID", "lnk_1006854_POQSTAT_POHDR");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderTextLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_HeaderTextLink);
POQSTAT_ChildForm_HeaderTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExist on ChildForm_HeaderTextFormTable...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderTextFormTable = new Control("ChildForm_HeaderTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POTEXT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming ClickButton on ChildForm_HeaderTextForm...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderTextForm = new Control("ChildForm_HeaderTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POTEXT_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_HeaderTextForm);
formBttn = POQSTAT_ChildForm_HeaderTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQSTAT_ChildForm_HeaderTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQSTAT_ChildForm_HeaderTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderTextForm.Exists());

													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_HeaderText_Seq...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderText_Seq = new Control("ChildForm_HeaderText_Seq", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POTEXT_CTW_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderText_Seq.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_HeaderTextForm);
formBttn = POQSTAT_ChildForm_HeaderTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Header Documents");


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderDocumentsLink = new Control("ChildForm_HeaderDocumentsLink", "ID", "lnk_1007738_POQSTAT_POHDR");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_HeaderDocumentsLink);
POQSTAT_ChildForm_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExist on ChildForm_HeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderDocumentsFormTable = new Control("ChildForm_HeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming ClickButton on ChildForm_HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderDocumentsForm = new Control("ChildForm_HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_HeaderDocumentsForm);
formBttn = POQSTAT_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQSTAT_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQSTAT_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_HeaderDocuments_Document = new Control("ChildForm_HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_HeaderDocumentsForm);
formBttn = POQSTAT_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Purchase Order Lines");


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLinesLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLinesLink = new Control("ChildForm_PurchaseOrderLinesLink", "ID", "lnk_1007311_POQSTAT_POHDR");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLinesLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLinesLink);
POQSTAT_ChildForm_PurchaseOrderLinesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExist on ChildForm_PurchaseOrderLinesFormTable...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLinesFormTable = new Control("ChildForm_PurchaseOrderLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming ClickButton on ChildForm_PurchaseOrderLinesForm...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLinesForm = new Control("ChildForm_PurchaseOrderLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLinesForm);
formBttn = POQSTAT_ChildForm_PurchaseOrderLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQSTAT_ChildForm_PurchaseOrderLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQSTAT_ChildForm_PurchaseOrderLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLinesForm.Exists());

													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming Select on ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab = new Control("ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab);
mTab = POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_LineDetail_Line...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_LineDetail_Line = new Control("ChildForm_PurchaseOrderLines_LineDetail_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_LineDetail_Line.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab);
mTab = POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_OtherInfo_GrossUnitCost...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_OtherInfo_GrossUnitCost = new Control("ChildForm_PurchaseOrderLines_OtherInfo_GrossUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='TRN_GR_UN_CST_AMT']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_OtherInfo_GrossUnitCost.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab);
mTab = POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_Notes_POHeaderExpeditingNotes...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_Notes_POHeaderExpeditingNotes = new Control("ChildForm_PurchaseOrderLines_Notes_POHeaderExpeditingNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_EXPDT_TX']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_Notes_POHeaderExpeditingNotes.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab);
mTab = POQSTAT_ChildForm_PurchaseOrderLines_PurchaseOrderLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Subcontract Retainage PO Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_SubcontractPOInfo_Amounts_Ordered...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_SubcontractPOInfo_Amounts_Ordered = new Control("ChildForm_PurchaseOrderLines_SubcontractPOInfo_Amounts_Ordered", "xpath", "//div[translate(@id,'0123456789','')='pr__POQSTAT_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='ORDERED_AMT_SUBPO']");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_SubcontractPOInfo_Amounts_Ordered.Exists());

											Driver.SessionLogger.WriteLine("ChildForm_SerialLotInfoForm");


												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_SerialLotLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_SerialLotLink = new Control("ChildForm_PurchaseOrderLines_SerialLotLink", "ID", "lnk_16787_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_SerialLotLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_POLineAcctsLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_POLineAcctsLink = new Control("ChildForm_PurchaseOrderLines_POLineAcctsLink", "ID", "lnk_1007312_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_POLineAcctsLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_POLineChargesLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_POLineChargesLink = new Control("ChildForm_PurchaseOrderLines_POLineChargesLink", "ID", "lnk_1007345_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_POLineChargesLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_POLineTextCodesLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_POLineTextCodesLink = new Control("ChildForm_PurchaseOrderLines_POLineTextCodesLink", "ID", "lnk_1007344_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_POLineTextCodesLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_POLineNotesLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_POLineNotesLink = new Control("ChildForm_PurchaseOrderLines_POLineNotesLink", "ID", "lnk_1007343_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_POLineNotesLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_LineDocumentsLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_LineDocumentsLink = new Control("ChildForm_PurchaseOrderLines_LineDocumentsLink", "ID", "lnk_1007739_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_ReceiptsStatusUpdatesLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_ReceiptsStatusUpdatesLink = new Control("ChildForm_PurchaseOrderLines_ReceiptsStatusUpdatesLink", "ID", "lnk_1006940_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_ReceiptsStatusUpdatesLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_OpenVchrsLnLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_OpenVchrsLnLink = new Control("ChildForm_PurchaseOrderLines_OpenVchrsLnLink", "ID", "lnk_1006860_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_OpenVchrsLnLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_VchrHistLnLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_VchrHistLnLink = new Control("ChildForm_PurchaseOrderLines_VchrHistLnLink", "ID", "lnk_1006861_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_VchrHistLnLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQSTAT] Perfoming VerifyExists on ChildForm_PurchaseOrderLines_VendReturnsLink...", Logger.MessageType.INF);
			Control POQSTAT_ChildForm_PurchaseOrderLines_VendReturnsLink = new Control("ChildForm_PurchaseOrderLines_VendReturnsLink", "ID", "lnk_1006943_POQSTAT_POLN_CTW");
			CPCommon.AssertEqual(true,POQSTAT_ChildForm_PurchaseOrderLines_VendReturnsLink.Exists());

												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_ChildForm_PurchaseOrderLinesForm);
formBttn = POQSTAT_ChildForm_PurchaseOrderLinesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "POQSTAT";
							CPCommon.WaitControlDisplayed(POQSTAT_MainForm);
formBttn = POQSTAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

