 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INQPINV_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Reports/Inquiries", "xpath","//div[@class='navItem'][.='Inventory Reports/Inquiries']").Click();
new Control("View Part Inventory", "xpath","//div[@class='navItem'][.='View Part Inventory']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INQPINV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INQPINV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control INQPINV_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,INQPINV_Part.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							INQPINV_Part.Click();
INQPINV_Part.SendKeys("#3", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
INQPINV_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("PART INVENTORY FORM");


											Driver.SessionLogger.WriteLine("PART DOCUMENTS");


												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_PartDocumentsLink...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_PartDocumentsLink = new Control("PartInventory_PartDocumentsLink", "ID", "lnk_1006735_INQPINV_PART_CTW");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_PartDocumentsLink);
INQPINV_PartInventory_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExist on PartInventory_PartDocumentsFormTable...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_PartDocumentsFormTable = new Control("PartInventory_PartDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_PartDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming ClickButton on PartInventory_PartDocumentsForm...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_PartDocumentsForm = new Control("PartInventory_PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INQPINV_PartInventory_PartDocumentsForm);
IWebElement formBttn = INQPINV_PartInventory_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INQPINV_PartInventory_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INQPINV_PartInventory_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.AssertEqual(true,INQPINV_PartInventory_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_PartDocuments_Type...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_PartDocuments_Type = new Control("PartInventory_PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_PartDocumentsForm);
formBttn = INQPINV_PartInventory_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MOS");


												
				CPCommon.CurrentComponent = "INQPINV";
							INQPINV_Part.Click();
INQPINV_Part.SendKeys("MKA-001", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
INQPINV_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
							CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming ClickButton on PartInventoryForm...", Logger.MessageType.INF);
			Control INQPINV_PartInventoryForm = new Control("PartInventoryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_PART_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INQPINV_PartInventoryForm);
formBttn = INQPINV_PartInventoryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INQPINV_PartInventoryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INQPINV_PartInventoryForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventoryForm);
formBttn = INQPINV_PartInventoryForm.mElement.FindElements(By.CssSelector("*[title*='Next']")).Count <= 0 ? INQPINV_PartInventoryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Next')]")).FirstOrDefault() :
INQPINV_PartInventoryForm.mElement.FindElements(By.CssSelector("*[title*='Next']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Next not found ");


													
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventoryForm);
formBttn = INQPINV_PartInventoryForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? INQPINV_PartInventoryForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
INQPINV_PartInventoryForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_MOsLink...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOsLink = new Control("PartInventory_MOsLink", "ID", "lnk_1006731_INQPINV_PART_CTW");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_MOsLink.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_MOsLink);
INQPINV_PartInventory_MOsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExist on PartInventory_MOsFormTable...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOsFormTable = new Control("PartInventory_MOsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_MOsFormTable.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming ClickButton on PartInventory_MOsForm...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOsForm = new Control("PartInventory_MOsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INQPINV_PartInventory_MOsForm);
formBttn = INQPINV_PartInventory_MOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INQPINV_PartInventory_MOsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INQPINV_PartInventory_MOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.AssertEqual(true,INQPINV_PartInventory_MOsForm.Exists());

													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_MOs_ManufacturingOrder...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOs_ManufacturingOrder = new Control("PartInventory_MOs_ManufacturingOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_MOs_ManufacturingOrder.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming Select on PartInventory_MOsTab...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOsTab = new Control("PartInventory_MOsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INQPINV_PartInventory_MOsTab);
IWebElement mTab = INQPINV_PartInventory_MOsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_MOs_Details_PartEffectivity_Configuration...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOs_Details_PartEffectivity_Configuration = new Control("PartInventory_MOs_Details_PartEffectivity_Configuration", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]/descendant::*[@id='BOM_CONFIG_ID']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_MOs_Details_PartEffectivity_Configuration.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_MOsTab);
mTab = INQPINV_PartInventory_MOsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Additional Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_MOs_AdditionalInfo_Organization...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOs_AdditionalInfo_Organization = new Control("PartInventory_MOs_AdditionalInfo_Organization", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_MOs_AdditionalInfo_Organization.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_MOsTab);
mTab = INQPINV_PartInventory_MOsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_MOs_Notes_MOHeaderNotes...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_MOs_Notes_MOHeaderNotes = new Control("PartInventory_MOs_Notes_MOHeaderNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__INQPINV_MOHDR_']/ancestor::form[1]/descendant::*[@id='MO_NOTES']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_MOs_Notes_MOHeaderNotes.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_MOsForm);
formBttn = INQPINV_PartInventory_MOsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("QUERY ECNS");


												
				CPCommon.CurrentComponent = "INQPINV";
							INQPINV_Part.Click();
INQPINV_Part.SendKeys("#3", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
INQPINV_Part.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
							CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												Driver.SessionLogger.WriteLine("ECNs");


												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_ECNsLink...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_ECNsLink = new Control("PartInventory_ECNsLink", "ID", "lnk_1006742_INQPINV_PART_CTW");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_ECNsLink.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_ECNsLink);
INQPINV_PartInventory_ECNsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExist on PartInventory_ECNsFormTable...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_ECNsFormTable = new Control("PartInventory_ECNsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_ECNsFormTable.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming ClickButton on PartInventory_ECNsForm...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_ECNsForm = new Control("PartInventory_ECNsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(INQPINV_PartInventory_ECNsForm);
formBttn = INQPINV_PartInventory_ECNsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INQPINV_PartInventory_ECNsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INQPINV_PartInventory_ECNsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.AssertEqual(true,INQPINV_PartInventory_ECNsForm.Exists());

													
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_ECNs_ECN...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_ECNs_ECN = new Control("PartInventory_ECNs_ECN", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='ECN_ID']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_ECNs_ECN.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming Select on PartInventory_ECNsTab...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_ECNsTab = new Control("PartInventory_ECNsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INQPINV_PartInventory_ECNsTab);
mTab = INQPINV_PartInventory_ECNsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "ECN Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INQPINV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INQPINV] Perfoming VerifyExists on PartInventory_ECNs_ECNDetails_Type...", Logger.MessageType.INF);
			Control INQPINV_PartInventory_ECNs_ECNDetails_Type = new Control("PartInventory_ECNs_ECNDetails_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__MRGMRSUB_ECNPART_ECN_']/ancestor::form[1]/descendant::*[@id='EC_TYPE_CD']");
			CPCommon.AssertEqual(true,INQPINV_PartInventory_ECNs_ECNDetails_Type.Exists());

												
				CPCommon.CurrentComponent = "INQPINV";
							CPCommon.WaitControlDisplayed(INQPINV_PartInventory_ECNsTab);
mTab = INQPINV_PartInventory_ECNsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Part Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "OEQSTAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSTAT] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control OEQSTAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(OEQSTAT_MainForm);
formBttn = OEQSTAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

