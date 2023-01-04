 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMEXPD_SMOKE : TestScript
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
new Control("Expedite Purchase Orders", "xpath","//div[@class='navItem'][.='Expedite Purchase Orders']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMEXPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMEXPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on Buyer...", Logger.MessageType.INF);
			Control POMEXPD_Buyer = new Control("Buyer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BUYER_ID']");
			CPCommon.AssertEqual(true,POMEXPD_Buyer.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMEXPD_ChildForm);
IWebElement formBttn = POMEXPD_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? POMEXPD_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
POMEXPD_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


											Driver.SessionLogger.WriteLine("QUERY");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control POMEXPD_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMEXPD_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm);
formBttn = POMEXPD_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMEXPD_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMEXPD_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.AssertEqual(true,POMEXPD_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_PurchaseOrder...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_PurchaseOrder = new Control("ChildForm_PurchaseOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_PurchaseOrder.Exists());

											Driver.SessionLogger.WriteLine("TAB");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_LineDetails_LineType...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_LineDetails_LineType = new Control("ChildForm_LineDetails_LineType", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='S_PO_LN_TYPE']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_LineDetails_LineType.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming Select on ChildForm_Tab...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_Tab = new Control("ChildForm_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_Tab);
IWebElement mTab = POMEXPD_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Quantities and Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_QuantitiesAndAmounts_LineAmounts_ExtendedAmt...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_QuantitiesAndAmounts_LineAmounts_ExtendedAmt = new Control("ChildForm_QuantitiesAndAmounts_LineAmounts_ExtendedAmt", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='TRN_PO_LN_EXT_AMT']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_QuantitiesAndAmounts_LineAmounts_ExtendedAmt.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_Tab);
mTab = POMEXPD_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Vendor").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_Vendor_Vendor...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_Vendor_Vendor = new Control("ChildForm_Vendor_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_Vendor_Vendor.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_Tab);
mTab = POMEXPD_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_Notes_POLineExpeditingNotes...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_Notes_POLineExpeditingNotes = new Control("ChildForm_Notes_POLineExpeditingNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POHDR_POLNHEADER_']/ancestor::form[1]/descendant::*[@id='PO_LN_EXPDT_NOTES']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_Notes_POLineExpeditingNotes.Exists());

											Driver.SessionLogger.WriteLine("PO HEADER EXPEDITE NOTES LINK");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_POHeaderExpediteNotesLink...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POHeaderExpediteNotesLink = new Control("ChildForm_POHeaderExpediteNotesLink", "ID", "lnk_1003333_POMEXPD_POHDR_POLNHEADER");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_POHeaderExpediteNotesLink.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_POHeaderExpediteNotesLink);
POMEXPD_ChildForm_POHeaderExpediteNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_POHeaderExpediteNotesForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POHeaderExpediteNotesForm = new Control("ChildForm_POHeaderExpediteNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POEXPDTNOTE_SUBTASK_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_POHeaderExpediteNotesForm.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_POHeaderExpediteNotes_POHeaderExpediteNotes...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POHeaderExpediteNotes_POHeaderExpediteNotes = new Control("ChildForm_POHeaderExpediteNotes_POHeaderExpediteNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POEXPDTNOTE_SUBTASK_']/ancestor::form[1]/descendant::*[@id='PO_EXPDT_TX']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_POHeaderExpediteNotes_POHeaderExpediteNotes.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_POHeaderExpediteNotesForm);
formBttn = POMEXPD_ChildForm_POHeaderExpediteNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("HEADER DOCUMENT LINK");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_HeaderDocumentsLink = new Control("ChildForm_HeaderDocumentsLink", "ID", "lnk_1007833_POMEXPD_POHDR_POLNHEADER");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_HeaderDocumentsLink);
POMEXPD_ChildForm_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExist on ChildForm_HeaderDocumentsTable...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_HeaderDocumentsTable = new Control("ChildForm_HeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_HeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming ClickButton on ChildForm_HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_HeaderDocumentsForm = new Control("ChildForm_HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_HeaderDocumentsForm);
formBttn = POMEXPD_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMEXPD_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMEXPD_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.AssertEqual(true,POMEXPD_ChildForm_HeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_HeaderDocuments_Document = new Control("ChildForm_HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_HeaderDocumentsForm);
formBttn = POMEXPD_ChildForm_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PO LINE ACCOUNTS LINK");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_POLineAccountsLink...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POLineAccountsLink = new Control("ChildForm_POLineAccountsLink", "ID", "lnk_1003350_POMEXPD_POHDR_POLNHEADER");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_POLineAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_POLineAccountsLink);
POMEXPD_ChildForm_POLineAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExist on ChildForm_POLineAccountsFormTable...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POLineAccountsFormTable = new Control("ChildForm_POLineAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POLNACCT_POLNINFO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_POLineAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming ClickButton on ChildForm_POLineAccountsForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POLineAccountsForm = new Control("ChildForm_POLineAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POLNACCT_POLNINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_POLineAccountsForm);
formBttn = POMEXPD_ChildForm_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMEXPD_ChildForm_POLineAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMEXPD_ChildForm_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.AssertEqual(true,POMEXPD_ChildForm_POLineAccountsForm.Exists());

													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_POLineAccounts_Project...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_POLineAccounts_Project = new Control("ChildForm_POLineAccounts_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_POLNACCT_POLNINFO_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_POLineAccounts_Project.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_POLineAccountsForm);
formBttn = POMEXPD_ChildForm_POLineAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LINE DOCUMENTS LINK");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_LineDocumentsLink...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_LineDocumentsLink = new Control("ChildForm_LineDocumentsLink", "ID", "lnk_1007835_POMEXPD_POHDR_POLNHEADER");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_LineDocumentsLink);
POMEXPD_ChildForm_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExist on ChildForm_LineDocumentsTable...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_LineDocumentsTable = new Control("ChildForm_LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming ClickButton on ChildForm_LineDocumentsForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_LineDocumentsForm = new Control("ChildForm_LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_LineDocumentsForm);
formBttn = POMEXPD_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMEXPD_ChildForm_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMEXPD_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.AssertEqual(true,POMEXPD_ChildForm_LineDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_LineDocuments_Document...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_LineDocuments_Document = new Control("ChildForm_LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_LineDocumentsForm);
formBttn = POMEXPD_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("VENDOR INFO LINK");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_VendorInfoLink...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_VendorInfoLink = new Control("ChildForm_VendorInfoLink", "ID", "lnk_1003359_POMEXPD_POHDR_POLNHEADER");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_VendorInfoLink.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_VendorInfoLink);
POMEXPD_ChildForm_VendorInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_VendorInfoForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_VendorInfoForm = new Control("ChildForm_VendorInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_VENDADDR_HDRINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_VendorInfoForm.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_VendorInfo_Vendor...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_VendorInfo_Vendor = new Control("ChildForm_VendorInfo_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__POMEXPD_VENDADDR_HDRINFO_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_VendorInfo_Vendor.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_VendorInfoForm);
formBttn = POMEXPD_ChildForm_VendorInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EXCHANGE RATES LINK");


												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_1003357_POMEXPD_POHDR_POLNHEADER");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_ExchangeRatesLink);
POMEXPD_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_ExchangeRatesForm...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_ExchangeRatesForm = new Control("ChildForm_ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPD] Perfoming VerifyExists on ChildForm_ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control POMEXPD_ChildForm_ExchangeRates_TransactionCurrency = new Control("ChildForm_ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,POMEXPD_ChildForm_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_ChildForm_ExchangeRatesForm);
formBttn = POMEXPD_ChildForm_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "POMEXPD";
							CPCommon.WaitControlDisplayed(POMEXPD_MainForm);
formBttn = POMEXPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

