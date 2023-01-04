 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POQCHNG_SMOKE : TestScript
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
new Control("View Purchase Order Change Orders", "xpath","//div[@class='navItem'][.='View Purchase Order Change Orders']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POQCHNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POQCHNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on PO...", Logger.MessageType.INF);
			Control POQCHNG_PO = new Control("PO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POQCHNG_PO.Exists());

											Driver.SessionLogger.WriteLine("SET DATA");


												
				CPCommon.CurrentComponent = "POQCHNG";
							POQCHNG_PO.Click();
POQCHNG_PO.SendKeys("16701", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
POQCHNG_PO.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming Set on Release...", Logger.MessageType.INF);
			Control POQCHNG_Release = new Control("Release", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_RLSE_NO']");
			POQCHNG_Release.Click();
POQCHNG_Release.SendKeys("0", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
POQCHNG_Release.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("CHILD FORM - CURRENT CHANGE ORDER FORM");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrderForm...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrderForm = new Control("CurrentChangeOrderForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDR_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrderForm.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_ChangeOrder...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_ChangeOrder = new Control("CurrentChangeOrder_ChangeOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDR_HDR_']/ancestor::form[1]/descendant::*[@id='PO_CHNG_ORD_NO']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_ChangeOrder.Exists());

											Driver.SessionLogger.WriteLine("HEADERDOCUMENTS LINKS");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_HeaderDocumentsLink = new Control("CurrentChangeOrder_HeaderDocumentsLink", "ID", "lnk_1007742_POQCHNG_POHDR_HDR");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_HeaderDocumentsLink);
POQCHNG_CurrentChangeOrder_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExist on CurrentChangeOrder_HeaderDocumentsTable...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_HeaderDocumentsTable = new Control("CurrentChangeOrder_HeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_HeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming ClickButton on CurrentChangeOrder_HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_HeaderDocumentsForm = new Control("CurrentChangeOrder_HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_HeaderDocumentsForm);
IWebElement formBttn = POQCHNG_CurrentChangeOrder_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCHNG_CurrentChangeOrder_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCHNG_CurrentChangeOrder_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_HeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_HeaderDocuments_Document = new Control("CurrentChangeOrder_HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_HeaderDocumentsForm);
formBttn = POQCHNG_CurrentChangeOrder_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CURRENTCOLINE FORM");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_CurrentCOLineLink...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLineLink = new Control("CurrentChangeOrder_CurrentCOLineLink", "ID", "lnk_2672_POQCHNG_POHDR_HDR");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLineLink.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLineLink);
POQCHNG_CurrentChangeOrder_CurrentCOLineLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExist on CurrentChangeOrder_CurrentCOLineTable...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLineTable = new Control("CurrentChangeOrder_CurrentCOLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLineTable.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming ClickButton on CurrentChangeOrder_CurrentCOLineForm...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLineForm = new Control("CurrentChangeOrder_CurrentCOLineForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLineForm);
formBttn = POQCHNG_CurrentChangeOrder_CurrentCOLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCHNG_CurrentChangeOrder_CurrentCOLineForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCHNG_CurrentChangeOrder_CurrentCOLineForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLineForm.Exists());

												Driver.SessionLogger.WriteLine("CURRENTCOLINE TAB");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_CurrentCOLine_LineDetail_Line...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_LineDetail_Line = new Control("CurrentChangeOrder_CurrentCOLine_LineDetail_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLine_LineDetail_Line.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming Select on CurrentChangeOrder_CurrentCOLine_Tab...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab = new Control("CurrentChangeOrder_CurrentCOLine_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab);
IWebElement mTab = POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_CurrentCOLine_OtherInfo_OrderReference...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_OtherInfo_OrderReference = new Control("CurrentChangeOrder_CurrentCOLine_OtherInfo_OrderReference", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='ORDER_REF_ID']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLine_OtherInfo_OrderReference.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab);
mTab = POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_CurrentCOLine_CostInfo_GrossUnitCost...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_CostInfo_GrossUnitCost = new Control("CurrentChangeOrder_CurrentCOLine_CostInfo_GrossUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='TRN_GR_UN_CST_AMT']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLine_CostInfo_GrossUnitCost.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab);
mTab = POQCHNG_CurrentChangeOrder_CurrentCOLine_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_CurrentCOLine_Notes_LineNotes_LineNotes...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_Notes_LineNotes_LineNotes = new Control("CurrentChangeOrder_CurrentCOLine_Notes_LineNotes_LineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_TX']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLine_Notes_LineNotes_LineNotes.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLineForm);
formBttn = POQCHNG_CurrentChangeOrder_CurrentCOLineForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CURRENTCOLINE OLDCHANGEORDER FORM FORMTABLE");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExist on CurrentChangeOrder_CurrentCOLine_OldChangeOrderTable...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderTable = new Control("CurrentChangeOrder_CurrentCOLine_OldChangeOrderTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRCHNG_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderTable.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming ClickButton on CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm = new Control("CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRCHNG_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm);
formBttn = POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_CurrentCOLine_OldChangeOrderForm.Exists());

												Driver.SessionLogger.WriteLine("CURRENTCOLINE OLDCHANGEORDER TAB");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_Header_ChangeOrder...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_Header_ChangeOrder = new Control("CurrentChangeOrder_OldChangeOrder_Header_ChangeOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRCHNG_CHLD_']/ancestor::form[1]/descendant::*[@id='PO_CHNG_ORD_NO']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_Header_ChangeOrder.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming Select on CurrentChangeOrder_OldChangeOrder_Tab...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_Tab = new Control("CurrentChangeOrder_OldChangeOrder_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRCHNG_CHLD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_Tab);
mTab = POQCHNG_CurrentChangeOrder_OldChangeOrder_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_Details_VendorContact_LastName...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_Details_VendorContact_LastName = new Control("CurrentChangeOrder_OldChangeOrder_Details_VendorContact_LastName", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRCHNG_CHLD_']/ancestor::form[1]/descendant::*[@id='CNTACT_LAST_NAME']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_Details_VendorContact_LastName.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_Tab);
mTab = POQCHNG_CurrentChangeOrder_OldChangeOrder_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("CURRENTCOLINE OLDCHANGEORDER OLD CO LINE LINK");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_OldCOLineLink...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLineLink = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLineLink", "ID", "lnk_2661_POQCHNG_POHDRCHNG_CHLD");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLineLink.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLineLink);
POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLineLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExist on CurrentChangeOrder_OldChangeOrder_OldCOLineTable...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLineTable = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLineTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLineTable.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming ClickButton on CurrentChangeOrder_OldChangeOrder_OldCOLine...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLine", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine);
formBttn = POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine.Exists());

												Driver.SessionLogger.WriteLine("CURRENTCOLINE OLDCHANGEORDER OLD CO LINE TAB");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_OldCOLine_LineDetail_Line...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_LineDetail_Line = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLine_LineDetail_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_LineDetail_Line.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming Select on CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab);
mTab = POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_OldCOLine_OtherInfo_OrderReference...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_OtherInfo_OrderReference = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLine_OtherInfo_OrderReference", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]/descendant::*[@id='ORDER_REF_ID']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_OtherInfo_OrderReference.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab);
mTab = POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_OldCOLine_CostInfo_GrossUnitCost...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_CostInfo_GrossUnitCost = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLine_CostInfo_GrossUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]/descendant::*[@id='TRN_GR_UN_CST_AMT']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_CostInfo_GrossUnitCost.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab);
mTab = POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_OldCOLine_Notes_LineNotes...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Notes_LineNotes = new Control("CurrentChangeOrder_OldChangeOrder_OldCOLine_Notes_LineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POLNCHNG_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_TX']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine_Notes_LineNotes.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine);
formBttn = POQCHNG_CurrentChangeOrder_OldChangeOrder_OldCOLine.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PO HEADER DOCUMENT");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsLink...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsLink = new Control("CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsLink", "ID", "lnk_1007743_POQCHNG_POHDRCHNG_CHLD");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsLink);
POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExist on CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsTable...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsTable = new Control("CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENTCHNG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming ClickButton on CurrentChangeOrder_OldChangeOrder_POHeaderDocuments...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments = new Control("CurrentChangeOrder_OldChangeOrder_POHeaderDocuments", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENTCHNG_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments);
formBttn = POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments.Exists());

													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_POHeaderDocuments_Document...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments_Document = new Control("CurrentChangeOrder_OldChangeOrder_POHeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENTCHNG_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments);
formBttn = POQCHNG_CurrentChangeOrder_OldChangeOrder_POHeaderDocuments.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("HEADER TEXT");


												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_HeaderTextLink...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderTextLink = new Control("CurrentChangeOrder_OldChangeOrder_HeaderTextLink", "ID", "lnk_2649_POQCHNG_POHDRCHNG_CHLD");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderTextLink.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderTextLink);
POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExist on CurrentChangeOrder_OldChangeOrder_HeaderTextTable...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderTextTable = new Control("CurrentChangeOrder_OldChangeOrder_HeaderTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderTextTable.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming ClickButton on CurrentChangeOrder_OldChangeOrder_HeaderText...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText = new Control("CurrentChangeOrder_OldChangeOrder_HeaderText", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRTEXT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText);
formBttn = POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText.Exists());

													
				CPCommon.CurrentComponent = "POQCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POQCHNG] Perfoming VerifyExists on CurrentChangeOrder_OldChangeOrder_HeaderText_TextCode...", Logger.MessageType.INF);
			Control POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText_TextCode = new Control("CurrentChangeOrder_OldChangeOrder_HeaderText_TextCode", "xpath", "//div[translate(@id,'0123456789','')='pr__POQCHNG_POHDRTEXT_']/ancestor::form[1]/descendant::*[@id='TEXT_CD']");
			CPCommon.AssertEqual(true,POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText_TextCode.Exists());

												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText);
formBttn = POQCHNG_CurrentChangeOrder_OldChangeOrder_HeaderText.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "POQCHNG";
							CPCommon.WaitControlDisplayed(POQCHNG_MainForm);
formBttn = POQCHNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

