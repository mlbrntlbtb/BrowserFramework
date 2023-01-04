 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMCOMP_SMOKE : TestScript
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
new Control("Manage MO Operation Completions", "xpath","//div[@class='navItem'][.='Manage MO Operation Completions']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMCOMP_MainForm = new Control("MainForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MAINHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on Warehouse...", Logger.MessageType.INF);
			Control PCMCOMP_Warehouse = new Control("Warehouse", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MAINHDR_']/ancestor::form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,PCMCOMP_Warehouse.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm = new Control("ChildForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNCOMPL_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm);
IWebElement formBttn = PCMCOMP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMCOMP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMCOMP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_MO...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_MO = new Control("ChildForm_MO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_MO.Exists());

											Driver.SessionLogger.WriteLine("Header Detail");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_HeaderDetailLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_HeaderDetailLink = new Control("ChildForm_HeaderDetailLink", "ID", "lnk_19144_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_HeaderDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_HeaderDetailLink);
PCMCOMP_ChildForm_HeaderDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on HeaderDetailForm...", Logger.MessageType.INF);
			Control PCMCOMP_HeaderDetailForm = new Control("HeaderDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDR_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_HeaderDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on HeaderDetail_MO...", Logger.MessageType.INF);
			Control PCMCOMP_HeaderDetail_MO = new Control("HeaderDetail_MO", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDR_DTL_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMCOMP_HeaderDetail_MO.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_HeaderDetailForm);
formBttn = PCMCOMP_HeaderDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MO Text");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_MOTextLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_MOTextLink = new Control("ChildForm_MOTextLink", "ID", "lnk_19145_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_MOTextLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_MOTextLink);
PCMCOMP_ChildForm_MOTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on MOTextForm...", Logger.MessageType.INF);
			Control PCMCOMP_MOTextForm = new Control("MOTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDRTEXT_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_MOTextForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_MOTextForm);
formBttn = PCMCOMP_MOTextForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCMCOMP_MOTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCMCOMP_MOTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on MOText_Table...", Logger.MessageType.INF);
			Control PCMCOMP_MOText_Table = new Control("MOText_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDRTEXT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_MOText_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_MOTextForm);
formBttn = PCMCOMP_MOTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routings");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_RoutingsLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_RoutingsLink = new Control("ChildForm_RoutingsLink", "ID", "lnk_19146_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_RoutingsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_RoutingsLink);
PCMCOMP_ChildForm_RoutingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on Routings_Table...", Logger.MessageType.INF);
			Control PCMCOMP_Routings_Table = new Control("Routings_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_Routings_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming ClickButtonIfExists on RoutingsForm...", Logger.MessageType.INF);
			Control PCMCOMP_RoutingsForm = new Control("RoutingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTING_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCMCOMP_RoutingsForm);
formBttn = PCMCOMP_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMCOMP_RoutingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMCOMP_RoutingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.AssertEqual(true,PCMCOMP_RoutingsForm.Exists());

													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on Routings_Operation_Sequence...", Logger.MessageType.INF);
			Control PCMCOMP_Routings_Operation_Sequence = new Control("Routings_Operation_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='MO_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,PCMCOMP_Routings_Operation_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_RoutingsForm);
formBttn = PCMCOMP_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routing Header Notes");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_RoutingHeaderNotesLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_RoutingHeaderNotesLink = new Control("ChildForm_RoutingHeaderNotesLink", "ID", "lnk_19147_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_RoutingHeaderNotesLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_RoutingHeaderNotesLink);
PCMCOMP_ChildForm_RoutingHeaderNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on RoutingHeaderNotesForm...", Logger.MessageType.INF);
			Control PCMCOMP_RoutingHeaderNotesForm = new Control("RoutingHeaderNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_ROUTINGHDR_NOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_RoutingHeaderNotesForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_RoutingHeaderNotesForm);
formBttn = PCMCOMP_RoutingHeaderNotesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCMCOMP_RoutingHeaderNotesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCMCOMP_RoutingHeaderNotesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on RoutingHeaderNotes_Table...", Logger.MessageType.INF);
			Control PCMCOMP_RoutingHeaderNotes_Table = new Control("RoutingHeaderNotes_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_ROUTINGHDR_NOTES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_RoutingHeaderNotes_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_RoutingHeaderNotesForm);
formBttn = PCMCOMP_RoutingHeaderNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("QC Inspections");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_QCInspectionsLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_QCInspectionsLink = new Control("ChildForm_QCInspectionsLink", "ID", "lnk_19148_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_QCInspectionsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_QCInspectionsLink);
PCMCOMP_ChildForm_QCInspectionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on QCInspectionsForm...", Logger.MessageType.INF);
			Control PCMCOMP_QCInspectionsForm = new Control("QCInspectionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNINSP_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_QCInspectionsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on QCInspections_Table...", Logger.MessageType.INF);
			Control PCMCOMP_QCInspections_Table = new Control("QCInspections_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNINSP_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_QCInspections_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_QCInspectionsForm);
formBttn = PCMCOMP_QCInspectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMCOMP_QCInspectionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMCOMP_QCInspectionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on QCInspections_InspectionReport...", Logger.MessageType.INF);
			Control PCMCOMP_QCInspections_InspectionReport = new Control("QCInspections_InspectionReport", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNINSP_DTL_']/ancestor::form[1]/descendant::*[@id='INSP_RPT_ID']");
			CPCommon.AssertEqual(true,PCMCOMP_QCInspections_InspectionReport.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_QCInspectionsForm);
formBttn = PCMCOMP_QCInspectionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Subcontractor Reqs/Pos");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_SubcontractorReqsPOsLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_SubcontractorReqsPOsLink = new Control("ChildForm_SubcontractorReqsPOsLink", "ID", "lnk_19149_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_SubcontractorReqsPOsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_SubcontractorReqsPOsLink);
PCMCOMP_ChildForm_SubcontractorReqsPOsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on SubContractorReqsPOsForm...", Logger.MessageType.INF);
			Control PCMCOMP_SubContractorReqsPOsForm = new Control("SubContractorReqsPOsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_SubContractorReqsPOsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on SubContractorReqsPOs_PurchaseOrder_Table...", Logger.MessageType.INF);
			Control PCMCOMP_SubContractorReqsPOs_PurchaseOrder_Table = new Control("SubContractorReqsPOs_PurchaseOrder_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_SubContractorReqsPOs_PurchaseOrder_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_SubContractorReqsPOsForm);
formBttn = PCMCOMP_SubContractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMCOMP_SubContractorReqsPOsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMCOMP_SubContractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on SubContractorReqsPOs_Requisition_Requisition...", Logger.MessageType.INF);
			Control PCMCOMP_SubContractorReqsPOs_Requisition_Requisition = new Control("SubContractorReqsPOs_Requisition_Requisition", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]/descendant::*[@id='RQ_ID']");
			CPCommon.AssertEqual(true,PCMCOMP_SubContractorReqsPOs_Requisition_Requisition.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_SubContractorReqsPOsForm);
formBttn = PCMCOMP_SubContractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_DocumentsLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_DocumentsLink = new Control("ChildForm_DocumentsLink", "ID", "lnk_19150_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_DocumentsLink);
PCMCOMP_ChildForm_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on DocumentsForm...", Logger.MessageType.INF);
			Control PCMCOMP_DocumentsForm = new Control("DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_DocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on Documents_Table...", Logger.MessageType.INF);
			Control PCMCOMP_Documents_Table = new Control("Documents_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_Documents_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_DocumentsForm);
formBttn = PCMCOMP_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMCOMP_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMCOMP_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on Documents_Document...", Logger.MessageType.INF);
			Control PCMCOMP_Documents_Document = new Control("Documents_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PCMCOMP_Documents_Document.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_DocumentsForm);
formBttn = PCMCOMP_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on ChildForm_PartDocumentsLink...", Logger.MessageType.INF);
			Control PCMCOMP_ChildForm_PartDocumentsLink = new Control("ChildForm_PartDocumentsLink", "ID", "lnk_19151_PCMSFR_MOROUTLNCOMPL_HDR");
			CPCommon.AssertEqual(true,PCMCOMP_ChildForm_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_ChildForm_PartDocumentsLink);
PCMCOMP_ChildForm_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on PartDocumentsForm...", Logger.MessageType.INF);
			Control PCMCOMP_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMCOMP_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExist on PartDocuments_Table...", Logger.MessageType.INF);
			Control PCMCOMP_PartDocuments_Table = new Control("PartDocuments_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMCOMP_PartDocuments_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_PartDocumentsForm);
formBttn = PCMCOMP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMCOMP_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMCOMP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMCOMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMCOMP] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control PCMCOMP_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,PCMCOMP_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_PartDocumentsForm);
formBttn = PCMCOMP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "PCMCOMP";
							CPCommon.WaitControlDisplayed(PCMCOMP_MainForm);
formBttn = PCMCOMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

