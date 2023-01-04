 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMRELS_SMOKE : TestScript
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
new Control("Create Blanket Purchase Order Releases", "xpath","//div[@class='navItem'][.='Create Blanket Purchase Order Releases']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMRELS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMRELS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMRELS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMRELS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_MainForm);
IWebElement formBttn = POMRELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMRELS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMRELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on BlanketPO...", Logger.MessageType.INF);
			Control POMRELS_BlanketPO = new Control("BlanketPO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POMRELS_BlanketPO.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control POMRELS_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POMRELS_MainFormTab);
IWebElement mTab = POMRELS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on Details_POBlanketInfo_ChangeOrder...", Logger.MessageType.INF);
			Control POMRELS_Details_POBlanketInfo_ChangeOrder = new Control("Details_POBlanketInfo_ChangeOrder", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_CHNG_ORD_NO']");
			CPCommon.AssertEqual(true,POMRELS_Details_POBlanketInfo_ChangeOrder.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_MainFormTab);
mTab = POMRELS_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Restrictions").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on Restrictions_BlanketPORestrictions_BlanketTotals_TotalBlanketAmount...", Logger.MessageType.INF);
			Control POMRELS_Restrictions_BlanketPORestrictions_BlanketTotals_TotalBlanketAmount = new Control("Restrictions_BlanketPORestrictions_BlanketTotals_TotalBlanketAmount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TRN_BLKT_AMT']");
			CPCommon.AssertEqual(true,POMRELS_Restrictions_BlanketPORestrictions_BlanketTotals_TotalBlanketAmount.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates ");


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control POMRELS_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1003800_POMRELS_POHDR_HDRINFO");
			CPCommon.AssertEqual(true,POMRELS_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_ExchangeRatesLink);
POMRELS_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control POMRELS_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMRELS_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ExchangeRates_RateGroup...", Logger.MessageType.INF);
			Control POMRELS_ExchangeRates_RateGroup = new Control("ExchangeRates_RateGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='RATE_GRP_ID']");
			CPCommon.AssertEqual(true,POMRELS_ExchangeRates_RateGroup.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_ExchangeRatesForm);
formBttn = POMRELS_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Header Documents");


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POMRELS_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007820_POMRELS_POHDR_HDRINFO");
			CPCommon.AssertEqual(true,POMRELS_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_HeaderDocumentsLink);
POMRELS_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POMRELS_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMRELS_HeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExist on HeaderDocumentsTable...", Logger.MessageType.INF);
			Control POMRELS_HeaderDocumentsTable = new Control("HeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMRELS_HeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_HeaderDocumentsForm);
formBttn = POMRELS_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMRELS_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMRELS_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on HeaderDocuments_CAGE...", Logger.MessageType.INF);
			Control POMRELS_HeaderDocuments_CAGE = new Control("HeaderDocuments_CAGE", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='CAGE_ID_FLD']");
			CPCommon.AssertEqual(true,POMRELS_HeaderDocuments_CAGE.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_HeaderDocumentsForm);
formBttn = POMRELS_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control POMRELS_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMRELS_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control POMRELS_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMRELS_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_ChildForm);
formBttn = POMRELS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMRELS_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMRELS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control POMRELS_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POMRELS_ChildForm_ChildFormTab);
mTab = POMRELS_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ChildForm_BasicInfo_Line...", Logger.MessageType.INF);
			Control POMRELS_ChildForm_BasicInfo_Line = new Control("ChildForm_BasicInfo_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,POMRELS_ChildForm_BasicInfo_Line.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_ChildForm_ChildFormTab);
mTab = POMRELS_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Release Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ChildForm_ReleaseDetails_LineInfo_ShipID...", Logger.MessageType.INF);
			Control POMRELS_ChildForm_ReleaseDetails_LineInfo_ShipID = new Control("ChildForm_ReleaseDetails_LineInfo_ShipID", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='SHIP_ID']");
			CPCommon.AssertEqual(true,POMRELS_ChildForm_ReleaseDetails_LineInfo_ShipID.Exists());

											Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on ChildForm_LineDocumentsLink...", Logger.MessageType.INF);
			Control POMRELS_ChildForm_LineDocumentsLink = new Control("ChildForm_LineDocumentsLink", "ID", "lnk_1007825_POMRELS_POLN_CTW");
			CPCommon.AssertEqual(true,POMRELS_ChildForm_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_ChildForm_LineDocumentsLink);
POMRELS_ChildForm_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on LineDocumentsForm...", Logger.MessageType.INF);
			Control POMRELS_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLNDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMRELS_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExist on LineDocumentsTable...", Logger.MessageType.INF);
			Control POMRELS_LineDocumentsTable = new Control("LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMRELS_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_LineDocumentsForm);
formBttn = POMRELS_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMRELS_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMRELS_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMRELS] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control POMRELS_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POMRELS_POLNDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMRELS_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_LineDocumentsForm);
formBttn = POMRELS_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "POMRELS";
							CPCommon.WaitControlDisplayed(POMRELS_MainForm);
formBttn = POMRELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

