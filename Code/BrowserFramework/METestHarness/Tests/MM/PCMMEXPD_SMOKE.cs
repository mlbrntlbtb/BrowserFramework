 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMMEXPD_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Manufacturing Orders", "xpath","//div[@class='navItem'][.='Manufacturing Orders']").Click();
new Control("Expedite Manufacturing Orders", "xpath","//div[@class='navItem'][.='Expedite Manufacturing Orders']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMMEXPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on Planner...", Logger.MessageType.INF);
			Control PCMMEXPD_Planner = new Control("Planner", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PLANNER']");
			CPCommon.AssertEqual(true,PCMMEXPD_Planner.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming Set on Warehouse...", Logger.MessageType.INF);
			Control PCMMEXPD_Warehouse = new Control("Warehouse", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			PCMMEXPD_Warehouse.Click();
PCMMEXPD_Warehouse.SendKeys("", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PCMMEXPD_Warehouse.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.AssertEqual(true,String.IsNullOrEmpty(PCMMEXPD_Warehouse.GetAttributeValue("value")));


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming ClickButton on ManufacturingOrdersForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrdersForm = new Control("ManufacturingOrdersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrdersForm);
IWebElement formBttn = PCMMEXPD_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PCMMEXPD_ManufacturingOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


											Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrdersTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrdersTable = new Control("ManufacturingOrdersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrdersTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrdersForm.Exists());

													
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrdersForm);
formBttn = PCMMEXPD_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_MO...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_MO = new Control("ManufacturingOrders_MO", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_MO.Exists());

											Driver.SessionLogger.WriteLine("Manufacturing OrdersTab");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming Select on ManufacturingOrdersTab...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrdersTab = new Control("ManufacturingOrdersTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrdersTab);
IWebElement mTab = PCMMEXPD_ManufacturingOrdersTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Details_Planner...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Details_Planner = new Control("ManufacturingOrders_Details_Planner", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]/descendant::*[@id='PLANNER_ID']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Details_Planner.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrdersTab);
mTab = PCMMEXPD_ManufacturingOrdersTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Additional Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_AdditionalInfo_Organization...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AdditionalInfo_Organization = new Control("ManufacturingOrders_AdditionalInfo_Organization", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AdditionalInfo_Organization.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrdersTab);
mTab = PCMMEXPD_ManufacturingOrdersTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Notes_HeaderNotes...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Notes_HeaderNotes = new Control("ManufacturingOrders_Notes_HeaderNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_MOHDR_']/ancestor::form[1]/descendant::*[@id='MO_NOTES']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Notes_HeaderNotes.Exists());

											Driver.SessionLogger.WriteLine("Allocations");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_AllocationsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AllocationsLink = new Control("ManufacturingOrders_AllocationsLink", "ID", "lnk_1004861_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AllocationsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_AllocationsLink);
PCMMEXPD_ManufacturingOrders_AllocationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_AllocationsTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AllocationsTable = new Control("ManufacturingOrders_AllocationsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOALLOCATIONS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AllocationsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_AllocationsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AllocationsForm = new Control("ManufacturingOrders_AllocationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOALLOCATIONS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AllocationsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_AllocationsForm);
formBttn = PCMMEXPD_ManufacturingOrders_AllocationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_AllocationsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_AllocationsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Allocations_AllocationInvAbbrev...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Allocations_AllocationInvAbbrev = new Control("ManufacturingOrders_Allocations_AllocationInvAbbrev", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOALLOCATIONS_']/ancestor::form[1]/descendant::*[@id='BLD_INVT_ABBRV_CD']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Allocations_AllocationInvAbbrev.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_AllocationsForm);
formBttn = PCMMEXPD_ManufacturingOrders_AllocationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_PartDocumentsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_PartDocumentsLink = new Control("ManufacturingOrders_PartDocumentsLink", "ID", "lnk_1004648_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_PartDocumentsLink);
PCMMEXPD_ManufacturingOrders_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_PartDocumentsTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_PartDocumentsTable = new Control("ManufacturingOrders_PartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_PartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_PartDocumentsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_PartDocumentsForm = new Control("ManufacturingOrders_PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_PartDocumentsForm);
formBttn = PCMMEXPD_ManufacturingOrders_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_PartDocuments_Type...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_PartDocuments_Type = new Control("ManufacturingOrders_PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_PartDocumentsForm);
formBttn = PCMMEXPD_ManufacturingOrders_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Requirements-SKIP MUNA TO!!!!!!!!!!!!!");


											Driver.SessionLogger.WriteLine("Routings");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_RoutingsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_RoutingsLink = new Control("ManufacturingOrders_RoutingsLink", "ID", "lnk_4328_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_RoutingsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_RoutingsLink);
PCMMEXPD_ManufacturingOrders_RoutingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_RoutingsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_RoutingsForm = new Control("ManufacturingOrders_RoutingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOHDR_ROUTHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_RoutingsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Routings_RoutingNo...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingNo = new Control("ManufacturingOrders_Routings_RoutingNo", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOHDR_ROUTHDR_']/ancestor::form[1]/descendant::*[@id='ROUT_NO']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Routings_RoutingNo.Exists());

											Driver.SessionLogger.WriteLine("Routing Lines");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_Routings_RoutingLinesTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTable = new Control("ManufacturingOrders_Routings_RoutingLinesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Routings_RoutingLinesForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesForm = new Control("ManufacturingOrders_Routings_RoutingLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOROUTING_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesForm);
formBttn = PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Routings_RoutingLines_Sequence...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingLines_Sequence = new Control("ManufacturingOrders_Routings_RoutingLines_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='MO_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Routings_RoutingLines_Sequence.Exists());

											Driver.SessionLogger.WriteLine("Routing Lines Tab");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming Select on ManufacturingOrders_Routings_RoutingLinesTab...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTab = new Control("ManufacturingOrders_Routings_RoutingLinesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTab);
mTab = PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Operation").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Routings_RoutingLines_Operation_OperationDetails_RunType...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingLines_Operation_OperationDetails_RunType = new Control("ManufacturingOrders_Routings_RoutingLines_Operation_OperationDetails_RunType", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='S_RUN_TYPE']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Routings_RoutingLines_Operation_OperationDetails_RunType.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTab);
mTab = PCMMEXPD_ManufacturingOrders_Routings_RoutingLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Routing Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Routings_RoutingLines_RoutingLineNotes_RoutingLineNotes...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Routings_RoutingLines_RoutingLineNotes_RoutingLineNotes = new Control("ManufacturingOrders_Routings_RoutingLines_RoutingLineNotes_RoutingLineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='MO_ROUT_LN_NOTES']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Routings_RoutingLines_RoutingLineNotes_RoutingLineNotes.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_RoutingsForm);
formBttn = PCMMEXPD_ManufacturingOrders_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Requisitions");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_RequisitionsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_RequisitionsLink = new Control("ManufacturingOrders_RequisitionsLink", "ID", "lnk_1004001_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_RequisitionsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_RequisitionsLink);
PCMMEXPD_ManufacturingOrders_RequisitionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_RequisitionsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_RequisitionsForm = new Control("ManufacturingOrders_RequisitionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_REQUISITIONS_FILTER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_RequisitionsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Requisitions_Project...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Requisitions_Project = new Control("ManufacturingOrders_Requisitions_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_REQUISITIONS_FILTER_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Requisitions_Project.Exists());

											Driver.SessionLogger.WriteLine("Requirement Requisition");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_Requisitions_RequirementRequisitionsTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsTable = new Control("ManufacturingOrders_Requisitions_RequirementRequisitionsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_RQLN_REQUISITIONS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Requisitions_RequirementRequisitionsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsForm = new Control("ManufacturingOrders_Requisitions_RequirementRequisitionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_RQLN_REQUISITIONS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsForm);
formBttn = PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Requisitions_RequirementRequisitions_RequirementInformation_LineNo...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitions_RequirementInformation_LineNo = new Control("ManufacturingOrders_Requisitions_RequirementRequisitions_RequirementInformation_LineNo", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_RQLN_REQUISITIONS_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Requisitions_RequirementRequisitions_RequirementInformation_LineNo.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_RequisitionsForm);
formBttn = PCMMEXPD_ManufacturingOrders_RequisitionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Pos");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_POsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_POsLink = new Control("ManufacturingOrders_POsLink", "ID", "lnk_1003999_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_POsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_POsLink);
PCMMEXPD_ManufacturingOrders_POsLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Requirement Purchase Orders");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_POs_RequirementPurchaseOrdersTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersTable = new Control("ManufacturingOrders_POs_RequirementPurchaseOrdersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_POLN_PO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_POs_RequirementPurchaseOrdersForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersForm = new Control("ManufacturingOrders_POs_RequirementPurchaseOrdersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_POLN_PO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersForm);
formBttn = PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_POs_RequirementPurchaseOrders_Line...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrders_Line = new Control("ManufacturingOrders_POs_RequirementPurchaseOrders_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_POLN_PO_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_POs_RequirementPurchaseOrders_Line.Exists());

											Driver.SessionLogger.WriteLine("Requirement Purchase Orders Tab");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming Close on ManufacturingOrders_POsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_POsForm = new Control("ManufacturingOrders_POsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_PO_FILTER_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_POsForm);
formBttn = PCMMEXPD_ManufacturingOrders_POsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Subcontractor Reqs/Pos");


											Driver.SessionLogger.WriteLine("Costs");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_CostsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_CostsLink = new Control("ManufacturingOrders_CostsLink", "ID", "lnk_1004871_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_CostsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_CostsLink);
PCMMEXPD_ManufacturingOrders_CostsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_CostsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_CostsForm = new Control("ManufacturingOrders_CostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_COSTINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_CostsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Costs_MOTotalCosts_Direct_Material...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Costs_MOTotalCosts_Direct_Material = new Control("ManufacturingOrders_Costs_MOTotalCosts_Direct_Material", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_COSTINFO_']/ancestor::form[1]/descendant::*[@id='TOT_MATL_CST']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Costs_MOTotalCosts_Direct_Material.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_CostsForm);
formBttn = PCMMEXPD_ManufacturingOrders_CostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assy Part Demand");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_AssyPartDemandLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AssyPartDemandLink = new Control("ManufacturingOrders_AssyPartDemandLink", "ID", "lnk_4247_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AssyPartDemandLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_AssyPartDemandLink);
PCMMEXPD_ManufacturingOrders_AssyPartDemandLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_AssyPartDemandTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AssyPartDemandTable = new Control("ManufacturingOrders_AssyPartDemandTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_GROSSRQMT_SUBTASK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AssyPartDemandTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_AssyPartDemandForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AssyPartDemandForm = new Control("ManufacturingOrders_AssyPartDemandForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_GROSSRQMT_SUBTASK_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AssyPartDemandForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_AssyPartDemandForm);
formBttn = PCMMEXPD_ManufacturingOrders_AssyPartDemandForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_AssyPartDemandForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_AssyPartDemandForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_AssyPartDemand_NeedDate...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_AssyPartDemand_NeedDate = new Control("ManufacturingOrders_AssyPartDemand_NeedDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMMEXPD_GROSSRQMT_SUBTASK_']/ancestor::form[1]/descendant::*[@id='NEED_DT']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_AssyPartDemand_NeedDate.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_AssyPartDemandForm);
formBttn = PCMMEXPD_ManufacturingOrders_AssyPartDemandForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_DocumentsLink...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_DocumentsLink = new Control("ManufacturingOrders_DocumentsLink", "ID", "lnk_4857_PCMMEXPD_MOHDR");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_DocumentsLink);
PCMMEXPD_ManufacturingOrders_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExist on ManufacturingOrders_DocumentsTable...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_DocumentsTable = new Control("ManufacturingOrders_DocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PC_MODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_DocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_DocumentsForm...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_DocumentsForm = new Control("ManufacturingOrders_DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PC_MODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_DocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_DocumentsForm);
formBttn = PCMMEXPD_ManufacturingOrders_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMMEXPD_ManufacturingOrders_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMMEXPD_ManufacturingOrders_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMMEXPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMEXPD] Perfoming VerifyExists on ManufacturingOrders_Documents_ReferenceDocument...", Logger.MessageType.INF);
			Control PCMMEXPD_ManufacturingOrders_Documents_ReferenceDocument = new Control("ManufacturingOrders_Documents_ReferenceDocument", "xpath", "//div[translate(@id,'0123456789','')='pr__PC_MODOCUMENT_']/ancestor::form[1]/descendant::*[@id='REF_DOC_FL']");
			CPCommon.AssertEqual(true,PCMMEXPD_ManufacturingOrders_Documents_ReferenceDocument.Exists());

												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_ManufacturingOrders_DocumentsForm);
formBttn = PCMMEXPD_ManufacturingOrders_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PCMMEXPD";
							CPCommon.WaitControlDisplayed(PCMMEXPD_MainForm);
formBttn = PCMMEXPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

