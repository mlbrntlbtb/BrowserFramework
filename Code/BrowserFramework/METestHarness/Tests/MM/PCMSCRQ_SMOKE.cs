 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMSCRQ_SMOKE : TestScript
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
new Control("Create MO Subcontractor Requisitions", "xpath","//div[@class='navItem'][.='Create MO Subcontractor Requisitions']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMSCRQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMSCRQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on Planner...", Logger.MessageType.INF);
			Control PCMSCRQ_Planner = new Control("Planner", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PLANNER_ID']");
			CPCommon.AssertEqual(true,PCMSCRQ_Planner.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							PCMSCRQ_Planner.Click();
PCMSCRQ_Planner.SendKeys("BCOMPHER", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PCMSCRQ_Planner.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming Set on Warehouse...", Logger.MessageType.INF);
			Control PCMSCRQ_Warehouse = new Control("Warehouse", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID']");
			PCMSCRQ_Warehouse.Click();
PCMSCRQ_Warehouse.SendKeys("WHSE2", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PCMSCRQ_Warehouse.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExist on ManufacturingOrdersTable...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrdersTable = new Control("ManufacturingOrdersTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrdersTable.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming ClickButton on ManufacturingOrdersForm...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrdersForm = new Control("ManufacturingOrdersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrdersForm);
IWebElement formBttn = PCMSCRQ_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMSCRQ_ManufacturingOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMSCRQ_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrdersForm.Exists());

													
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_MO...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_MO = new Control("ManufacturingOrders_MO", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_CTW_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_MO.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrdersForm);
formBttn = PCMSCRQ_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PCMSCRQ_ManufacturingOrdersForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PCMSCRQ_ManufacturingOrdersForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


											Driver.SessionLogger.WriteLine("Requisition Info");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_RequisitionInfoLink...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_RequisitionInfoLink = new Control("ManufacturingOrders_RequisitionInfoLink", "ID", "lnk_4261_PCMSCRQ_CTW");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_RequisitionInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_RequisitionInfoLink);
PCMSCRQ_ManufacturingOrders_RequisitionInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_RequisitionInfoForm...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_RequisitionInfoForm = new Control("ManufacturingOrders_RequisitionInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_REQUISITION_DETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_RequisitionInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming Select on ManufacturingOrders_RequisitionInfoTab...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_RequisitionInfoTab = new Control("ManufacturingOrders_RequisitionInfoTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_REQUISITION_DETAIL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_RequisitionInfoTab);
IWebElement mTab = PCMSCRQ_ManufacturingOrders_RequisitionInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_RequisitionInfo_Header_Status...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_RequisitionInfo_Header_Status = new Control("ManufacturingOrders_RequisitionInfo_Header_Status", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_REQUISITION_DETAIL_']/ancestor::form[1]/descendant::*[@id='S_STATUS_CD']");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_RequisitionInfo_Header_Status.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_RequisitionInfoTab);
mTab = PCMSCRQ_ManufacturingOrders_RequisitionInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_RequisitionInfo_Line_Item...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_RequisitionInfo_Line_Item = new Control("ManufacturingOrders_RequisitionInfo_Line_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSCRQ_REQUISITION_DETAIL_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_RequisitionInfo_Line_Item.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_RequisitionInfoForm);
formBttn = PCMSCRQ_ManufacturingOrders_RequisitionInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SubContractor Reqs/Pos");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_SubcontractorReqsPOsLink...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsLink = new Control("ManufacturingOrders_SubcontractorReqsPOsLink", "ID", "lnk_4269_PCMSCRQ_CTW");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsLink);
PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExist on ManufacturingOrders_SubcontractorReqsPOsTable...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsTable = new Control("ManufacturingOrders_SubcontractorReqsPOsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming ClickButton on ManufacturingOrders_SubcontractorReqsPOsForm...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm = new Control("ManufacturingOrders_SubcontractorReqsPOsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm);
formBttn = PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm.Exists());

													
				CPCommon.CurrentComponent = "PCMSCRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSCRQ] Perfoming VerifyExists on ManufacturingOrders_SubcontractorReqsPOs_Requisition_Requisition...", Logger.MessageType.INF);
			Control PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOs_Requisition_Requisition = new Control("ManufacturingOrders_SubcontractorReqsPOs_Requisition_Requisition", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]/descendant::*[@id='RQ_ID']");
			CPCommon.AssertEqual(true,PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOs_Requisition_Requisition.Exists());

												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm);
formBttn = PCMSCRQ_ManufacturingOrders_SubcontractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PCMSCRQ";
							CPCommon.WaitControlDisplayed(PCMSCRQ_MainForm);
formBttn = PCMSCRQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

