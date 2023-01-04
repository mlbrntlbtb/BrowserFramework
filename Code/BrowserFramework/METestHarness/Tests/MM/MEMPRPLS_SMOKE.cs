 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEMPRPLS_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Proposal Item Costing", "xpath","//div[@class='navItem'][.='Proposal Item Costing']").Click();
new Control("Manage Proposal BOM Cost Estimates - Summarized", "xpath","//div[@class='navItem'][.='Manage Proposal BOM Cost Estimates - Summarized']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEMPRPLS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_Proposal = new Control("Proposal", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							MEMPRPLS_Proposal.Click();
MEMPRPLS_Proposal.SendKeys("NW-TESTINGPROP12345Z", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
MEMPRPLS_Proposal.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Proposal Cost Details Form");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetailsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetailsFormTable = new Control("ProposalCostDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetailsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetailsForm = new Control("ProposalCostDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetailsForm);
IWebElement formBttn = MEMPRPLS_ProposalCostDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Item...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Item = new Control("ProposalCostDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='PROP_ITEM_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Item.Exists());

											Driver.SessionLogger.WriteLine("Proposal Cost Details Tab");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming Select on ProposalCostDetailsTab...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetailsTab = new Control("ProposalCostDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetailsTab);
IWebElement mTab = MEMPRPLS_ProposalCostDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Costs").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Costs_ProposalCostAmounts_DirectUnit...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Costs_ProposalCostAmounts_DirectUnit = new Control("ProposalCostDetails_Costs_ProposalCostAmounts_DirectUnit", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='COLNDIRECTUNITCSTAMT']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Costs_ProposalCostAmounts_DirectUnit.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetailsTab);
mTab = MEMPRPLS_ProposalCostDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Proposal Quantity Breakpoints").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalQuantityBreakpoints_Breakpoint1...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalQuantityBreakpoints_Breakpoint1 = new Control("ProposalCostDetails_ProposalQuantityBreakpoints_Breakpoint1", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='BRK_1_QTY']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalQuantityBreakpoints_Breakpoint1.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetailsTab);
mTab = MEMPRPLS_ProposalCostDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Item Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ItemDetails_MakeBuy...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ItemDetails_MakeBuy = new Control("ProposalCostDetails_ItemDetails_MakeBuy", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='S_MAKE_BUY_CD']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ItemDetails_MakeBuy.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetailsTab);
mTab = MEMPRPLS_ProposalCostDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Proposal Item Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemNotes_ProposalItemNotes...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemNotes_ProposalItemNotes = new Control("ProposalCostDetails_ProposalItemNotes_ProposalItemNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PROPITEMDTL _DTL_']/ancestor::form[1]/descendant::*[@id='PROP_ITEM_NT']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemNotes_ProposalItemNotes.Exists());

											Driver.SessionLogger.WriteLine("Proposal Item Costs Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCostsLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCostsLink = new Control("ProposalCostDetails_ProposalItemCostsLink", "ID", "lnk_5474_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCostsLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_ProposalItemCostsLink);
MEMPRPLS_ProposalCostDetails_ProposalItemCostsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCostsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCostsForm = new Control("ProposalCostDetails_ProposalItemCostsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPITEMCST_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCostsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCosts_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_Proposal = new Control("ProposalCostDetails_ProposalItemCosts_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPITEMCST_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_ProposalItemCosts_CostRecordsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsFormTable = new Control("ProposalCostDetails_ProposalItemCosts_CostRecordsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_ProposalItemCosts_CostRecordsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsForm = new Control("ProposalCostDetails_ProposalItemCosts_CostRecordsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsForm);
formBttn = MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCosts_CostRecords_MinimumQuantity...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_MinimumQuantity = new Control("ProposalCostDetails_ProposalItemCosts_CostRecords_MinimumQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='MIN_QTY']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_MinimumQuantity.Exists());

											Driver.SessionLogger.WriteLine("Cost Records Tab");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming Select on ProposalCostDetails_ProposalItemCosts_CostRecordsTab...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab = new Control("ProposalCostDetails_ProposalItemCosts_CostRecordsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab);
mTab = MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Incremental Costs").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCosts_CostRecords_IncrementalCosts_ThisLevelDirectUnitCost...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_IncrementalCosts_ThisLevelDirectUnitCost = new Control("ProposalCostDetails_ProposalItemCosts_CostRecords_IncrementalCosts_ThisLevelDirectUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='TL_DIR_UNIT_CST']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_IncrementalCosts_ThisLevelDirectUnitCost.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab);
mTab = MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Source Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCosts_CostRecords_CostSourceDetails_Vendor...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_CostSourceDetails_Vendor = new Control("ProposalCostDetails_ProposalItemCosts_CostRecords_CostSourceDetails_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_CostSourceDetails_Vendor.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab);
mTab = MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecordsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_ProposalItemCosts_CostRecords_Notes_Notes...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_Notes_Notes = new Control("ProposalCostDetails_ProposalItemCosts_CostRecords_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_PROPOSITEMCST_CTW_']/ancestor::form[1]/descendant::*[@id='ITEM_CST_NT']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_ProposalItemCosts_CostRecords_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_ProposalItemCostsForm);
formBttn = MEMPRPLS_ProposalCostDetails_ProposalItemCostsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Matl Costs WS Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWSLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWSLink = new Control("ProposalCostDetails_MatlCostsWSLink", "ID", "lnk_5476_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWSLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_MatlCostsWSLink);
MEMPRPLS_ProposalCostDetails_MatlCostsWSLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWSForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWSForm = new Control("ProposalCostDetails_MatlCostsWSForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MATLCST_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWSForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWS_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_Proposal = new Control("ProposalCostDetails_MatlCostsWS_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MATLCST_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_MatlCostsWS_MaterialCostsWSFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSFormTable = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWSFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm);
formBttn = MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWS_MaterialCostsWS_MinimumQuantity...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_MinimumQuantity = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWS_MinimumQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]/descendant::*[@id='MIN_QT']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_MinimumQuantity.Exists());

											Driver.SessionLogger.WriteLine("Material Costs WS Tab");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming Select on ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab);
mTab = MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostInformation_OrderQuoteGrossUnitCost...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostInformation_OrderQuoteGrossUnitCost = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostInformation_OrderQuoteGrossUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]/descendant::*[@id='GROSS_UNIT_CST_AMT']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostInformation_OrderQuoteGrossUnitCost.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab);
mTab = MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Source Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostSourceDetails_VendorDetails_Vendor...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostSourceDetails_VendorDetails_Vendor = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostSourceDetails_VendorDetails_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_CostSourceDetails_VendorDetails_Vendor.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab);
mTab = MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWSTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_MatlCostsWS_MaterialCostsWS_Notes_Notes...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_Notes_Notes = new Control("ProposalCostDetails_MatlCostsWS_MaterialCostsWS_Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEMATLCST_CTW_']/ancestor::form[1]/descendant::*[@id='NOTES_DISP']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_MatlCostsWS_MaterialCostsWS_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_MatlCostsWSForm);
formBttn = MEMPRPLS_ProposalCostDetails_MatlCostsWSForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("RFQs Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RFQsLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQsLink = new Control("ProposalCostDetails_RFQsLink", "ID", "lnk_5479_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQsLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RFQsLink);
MEMPRPLS_ProposalCostDetails_RFQsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RFQsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQsForm = new Control("ProposalCostDetails_RFQsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_RFQ_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RFQs_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_Proposal = new Control("ProposalCostDetails_RFQs_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_RFQ_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQs_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_RFQs_RequestForQuotesFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesFormTable = new Control("ProposalCostDetails_RFQs_RequestForQuotesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MERFQS_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_RFQs_RequestForQuotesForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesForm = new Control("ProposalCostDetails_RFQs_RequestForQuotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MERFQS_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesForm);
formBttn = MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RFQs_RequestForQuotes_Vendor...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotes_Vendor = new Control("ProposalCostDetails_RFQs_RequestForQuotes_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MERFQS_CTW_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotes_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Request For Quotes Tab");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming Select on ProposalCostDetails_RFQs_RequestForQuotesTab...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesTab = new Control("ProposalCostDetails_RFQs_RequestForQuotesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MERFQS_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesTab);
mTab = MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RFQs_RequestForQuotes_Details_RFQCreated...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotes_Details_RFQCreated = new Control("ProposalCostDetails_RFQs_RequestForQuotes_Details_RFQCreated", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MERFQS_CTW_']/ancestor::form[1]/descendant::*[@id='RFQ_FL']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotes_Details_RFQCreated.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesTab);
mTab = MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "RFQ Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RFQs_RequestForQuotes_RFQLineNotes_RFQLineNotes...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotes_RFQLineNotes_RFQLineNotes = new Control("ProposalCostDetails_RFQs_RequestForQuotes_RFQLineNotes_RFQLineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MERFQS_CTW_']/ancestor::form[1]/descendant::*[@id='RFQ_LN_NOTES']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RFQs_RequestForQuotes_RFQLineNotes_RFQLineNotes.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RFQsForm);
formBttn = MEMPRPLS_ProposalCostDetails_RFQsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Inventory Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_InventoryLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_InventoryLink = new Control("ProposalCostDetails_InventoryLink", "ID", "lnk_5482_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_InventoryLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_InventoryLink);
MEMPRPLS_ProposalCostDetails_InventoryLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_InventoryForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_InventoryForm = new Control("ProposalCostDetails_InventoryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_InventoryForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Inventory_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_Proposal = new Control("ProposalCostDetails_Inventory_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_Inventory_InventoryDetailsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsFormTable = new Control("ProposalCostDetails_Inventory_InventoryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_Inventory_InventoryDetailsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsForm = new Control("ProposalCostDetails_Inventory_InventoryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsForm);
formBttn = MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Inventory_InventoryDetails_Project...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_Project = new Control("ProposalCostDetails_Inventory_InventoryDetails_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_Project.Exists());

											Driver.SessionLogger.WriteLine("Inventory Details Tab");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming Select on ProposalCostDetails_Inventory_InventoryDetailsTab...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab = new Control("ProposalCostDetails_Inventory_InventoryDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab);
mTab = MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Inventory Quantities").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Inventory_InventoryDetails_InventoryQuantities_Quantities_NetAvailable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_InventoryQuantities_Quantities_NetAvailable = new Control("ProposalCostDetails_Inventory_InventoryDetails_InventoryQuantities_Quantities_NetAvailable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]/descendant::*[@id='NET_AVAIL_QTY']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_InventoryQuantities_Quantities_NetAvailable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab);
mTab = MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Unit Cost").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Inventory_InventoryDetails_UnitCost_UnitCosts_DirectMaterial...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_UnitCost_UnitCosts_DirectMaterial = new Control("ProposalCostDetails_Inventory_InventoryDetails_UnitCost_UnitCosts_DirectMaterial", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]/descendant::*[@id='MATL_CST_AMT']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_UnitCost_UnitCosts_DirectMaterial.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab);
mTab = MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Expediting Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Inventory_InventoryDetails_ExpeditingNotes_ExpeditingNotes...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_ExpeditingNotes_ExpeditingNotes = new Control("ProposalCostDetails_Inventory_InventoryDetails_ExpeditingNotes_ExpeditingNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_INVT_MEINVT_']/ancestor::form[1]/descendant::*[@id='EXPDT_TX']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Inventory_InventoryDetails_ExpeditingNotes_ExpeditingNotes.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_InventoryForm);
formBttn = MEMPRPLS_ProposalCostDetails_InventoryForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Alternate Parts Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AlternatePartsLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AlternatePartsLink = new Control("ProposalCostDetails_AlternatePartsLink", "ID", "lnk_5485_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AlternatePartsLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AlternatePartsLink);
MEMPRPLS_ProposalCostDetails_AlternatePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AlternatePartsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AlternatePartsForm = new Control("ProposalCostDetails_AlternatePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ALTPART_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AlternatePartsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AlternateParts_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AlternateParts_Proposal = new Control("ProposalCostDetails_AlternateParts_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ALTPART_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AlternateParts_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_AlternateParts_AlternatePartsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsFormTable = new Control("ProposalCostDetails_AlternateParts_AlternatePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ALTPART_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_AlternateParts_AlternatePartsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsForm = new Control("ProposalCostDetails_AlternateParts_AlternatePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ALTPART_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsForm);
formBttn = MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AlternateParts_AlternatePartsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AlternateParts_AlternateParts_Sequence...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AlternateParts_AlternateParts_Sequence = new Control("ProposalCostDetails_AlternateParts_AlternateParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ALTPART_CHILD_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AlternateParts_AlternateParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AlternatePartsForm);
formBttn = MEMPRPLS_ProposalCostDetails_AlternatePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PBOM Detail Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_PBOMDetailLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PBOMDetailLink = new Control("ProposalCostDetails_PBOMDetailLink", "ID", "lnk_5461_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PBOMDetailLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_PBOMDetailLink);
MEMPRPLS_ProposalCostDetails_PBOMDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_PBOMDetailForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PBOMDetailForm = new Control("ProposalCostDetails_PBOMDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PBOMDTL_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PBOMDetailForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_PBOMDetail_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PBOMDetail_Proposal = new Control("ProposalCostDetails_PBOMDetail_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PBOMDTL_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PBOMDetail_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_PBOMDetail_PBOMAssembliesFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesFormTable = new Control("ProposalCostDetails_PBOMDetail_PBOMAssembliesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PBOMLN_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_PBOMDetail_PBOMAssembliesForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesForm = new Control("ProposalCostDetails_PBOMDetail_PBOMAssembliesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PBOMLN_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesForm);
formBttn = MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssembliesForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_PBOMDetail_PBOMAssemblies_Line...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssemblies_Line = new Control("ProposalCostDetails_PBOMDetail_PBOMAssemblies_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__MEMPRPLS_PBOMLN_DTL_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PBOMDetail_PBOMAssemblies_Line.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_PBOMDetailForm);
formBttn = MEMPRPLS_ProposalCostDetails_PBOMDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Other Cost WS Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_OtherCostWSLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_OtherCostWSLink = new Control("ProposalCostDetails_OtherCostWSLink", "ID", "lnk_5489_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_OtherCostWSLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_OtherCostWSLink);
MEMPRPLS_ProposalCostDetails_OtherCostWSLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_OtherCostWSForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_OtherCostWSForm = new Control("ProposalCostDetails_OtherCostWSForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_OTHERCST_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_OtherCostWSForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_OtherCostWS_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_OtherCostWS_Proposal = new Control("ProposalCostDetails_OtherCostWS_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_OTHERCST_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_OtherCostWS_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_OtherCostWS_OtherCostsWSFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSFormTable = new Control("ProposalCostDetails_OtherCostWS_OtherCostsWSFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEOTHERCST_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_OtherCostWS_OtherCostsWSForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSForm = new Control("ProposalCostDetails_OtherCostWS_OtherCostsWSForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEOTHERCST_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSForm);
formBttn = MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWSForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_OtherCostWS_OtherCostsWS_MinimumQuantity...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWS_MinimumQuantity = new Control("ProposalCostDetails_OtherCostWS_OtherCostsWS_MinimumQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_MEOTHERCST_CTW_']/ancestor::form[1]/descendant::*[@id='MIN_QTY']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_OtherCostWS_OtherCostsWS_MinimumQuantity.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_OtherCostWSForm);
formBttn = MEMPRPLS_ProposalCostDetails_OtherCostWSForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Add Quotes Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotesLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotesLink = new Control("ProposalCostDetails_AddQuotesLink", "ID", "lnk_5491_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotesLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AddQuotesLink);
MEMPRPLS_ProposalCostDetails_AddQuotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotesForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotesForm = new Control("ProposalCostDetails_AddQuotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_VENDQT_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotesForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotes_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_Proposal = new Control("ProposalCostDetails_AddQuotes_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_VENDQT_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_AddQuotes_VendorQuotesFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesFormTable = new Control("ProposalCostDetails_AddQuotes_VendorQuotesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_AddQuotes_VendorQuotesForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesForm = new Control("ProposalCostDetails_AddQuotes_VendorQuotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesForm);
formBttn = MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotes_VendorQuotes_Vendor...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_Vendor = new Control("ProposalCostDetails_AddQuotes_VendorQuotes_Vendor", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Vendor Quotes Tab");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming Select on ProposalCostDetails_AddQuotes_VendorQuotesTab...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab = new Control("ProposalCostDetails_AddQuotes_VendorQuotesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab);
mTab = MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotes_VendorQuotes_Details_QuoteType...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_Details_QuoteType = new Control("ProposalCostDetails_AddQuotes_VendorQuotes_Details_QuoteType", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]/descendant::*[@id='QT_TYPE_CD']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_Details_QuoteType.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab);
mTab = MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Alternate Part Numbers").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotes_VendorQuotes_AlternatePartNumbers_ManufacturerPart_Manufacturer...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_AlternatePartNumbers_ManufacturerPart_Manufacturer = new Control("ProposalCostDetails_AddQuotes_VendorQuotes_AlternatePartNumbers_ManufacturerPart_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_AlternatePartNumbers_ManufacturerPart_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab);
mTab = MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Quote Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_AddQuotes_VendorQuotes_QuoteLineNotes_QuoteLineNotes...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_QuoteLineNotes_QuoteLineNotes = new Control("ProposalCostDetails_AddQuotes_VendorQuotes_QuoteLineNotes_QuoteLineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ADDVENDQT_CTW_']/ancestor::form[1]/descendant::*[@id='QTLNNOTES']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_AddQuotes_VendorQuotes_QuoteLineNotes_QuoteLineNotes.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_AddQuotesForm);
formBttn = MEMPRPLS_ProposalCostDetails_AddQuotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routings Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RoutingsLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RoutingsLink = new Control("ProposalCostDetails_RoutingsLink", "ID", "lnk_5493_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RoutingsLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RoutingsLink);
MEMPRPLS_ProposalCostDetails_RoutingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_RoutingsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_RoutingsForm = new Control("ProposalCostDetails_RoutingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ROUTING_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_RoutingsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Routings_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Routings_Proposal = new Control("ProposalCostDetails_Routings_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ROUTING_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Routings_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_Routings_RoutingDetailsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsFormTable = new Control("ProposalCostDetails_Routings_RoutingDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_Routings_RoutingDetailsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsForm = new Control("ProposalCostDetails_Routings_RoutingDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ROUTINGLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsForm);
formBttn = MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Routings_RoutingDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_Routings_RoutingDetails_OperationSequence...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_Routings_RoutingDetails_OperationSequence = new Control("ProposalCostDetails_Routings_RoutingDetails_OperationSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_ROUTINGLN_CTW_']/ancestor::form[1]/descendant::*[@id='ROUT_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_Routings_RoutingDetails_OperationSequence.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_RoutingsForm);
formBttn = MEMPRPLS_ProposalCostDetails_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitute Parts Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_SubstitutePartsLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_SubstitutePartsLink = new Control("ProposalCostDetails_SubstitutePartsLink", "ID", "lnk_5495_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_SubstitutePartsLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_SubstitutePartsLink);
MEMPRPLS_ProposalCostDetails_SubstitutePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_SubstitutePartsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_SubstitutePartsForm = new Control("ProposalCostDetails_SubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_SUBTPART_SUBTASK_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_SubstitutePartsForm.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_SubstituteParts_Proposal...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_SubstituteParts_Proposal = new Control("ProposalCostDetails_SubstituteParts_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_SUBTPART_SUBTASK_HDR_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_SubstituteParts_Proposal.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_SubstituteParts_SubstitutePartsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsFormTable = new Control("ProposalCostDetails_SubstituteParts_SubstitutePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_SUBSTPART_SUBTASK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_SubstituteParts_SubstitutePartsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsForm = new Control("ProposalCostDetails_SubstituteParts_SubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_SUBSTPART_SUBTASK_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsForm);
formBttn = MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstitutePartsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_SubstituteParts_SubstituteParts_Sequence...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstituteParts_Sequence = new Control("ProposalCostDetails_SubstituteParts_SubstituteParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMESUB_SUBSTPART_SUBTASK_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_SubstituteParts_SubstituteParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_SubstitutePartsForm);
formBttn = MEMPRPLS_ProposalCostDetails_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents Link");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_PartDocumentsLink...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PartDocumentsLink = new Control("ProposalCostDetails_PartDocumentsLink", "ID", "lnk_5497_MEMPRPLS_PROPITEMDTL _DTL");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_PartDocumentsLink);
MEMPRPLS_ProposalCostDetails_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExist on ProposalCostDetails_PartDocumentsFormTable...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PartDocumentsFormTable = new Control("ProposalCostDetails_PartDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PartDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming ClickButton on ProposalCostDetails_PartDocumentsForm...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PartDocumentsForm = new Control("ProposalCostDetails_PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_PartDocumentsForm);
formBttn = MEMPRPLS_ProposalCostDetails_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEMPRPLS_ProposalCostDetails_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEMPRPLS_ProposalCostDetails_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "MEMPRPLS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMPRPLS] Perfoming VerifyExists on ProposalCostDetails_PartDocuments_Type...", Logger.MessageType.INF);
			Control MEMPRPLS_ProposalCostDetails_PartDocuments_Type = new Control("ProposalCostDetails_PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,MEMPRPLS_ProposalCostDetails_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_ProposalCostDetails_PartDocumentsForm);
formBttn = MEMPRPLS_ProposalCostDetails_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "MEMPRPLS";
							CPCommon.WaitControlDisplayed(MEMPRPLS_MainForm);
formBttn = MEMPRPLS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

