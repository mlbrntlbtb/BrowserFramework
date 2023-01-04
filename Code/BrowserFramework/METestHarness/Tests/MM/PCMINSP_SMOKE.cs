 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMINSP_SMOKE : TestScript
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
new Control("Manage MO Quality Control Inspection Results", "xpath","//div[@class='navItem'][.='Manage MO Quality Control Inspection Results']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMINSP_MainForm = new Control("MainForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MAINHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on Warehouse...", Logger.MessageType.INF);
			Control PCMINSP_Warehouse = new Control("Warehouse", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MAINHDR_']/ancestor::form[1]/descendant::*[@id='WHSE_ID']");
			CPCommon.AssertEqual(true,PCMINSP_Warehouse.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm = new Control("ChildForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm);
IWebElement formBttn = PCMINSP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMINSP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMINSP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_MO...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_MO = new Control("ChildForm_MO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_MO.Exists());

											Driver.SessionLogger.WriteLine("Header Detail");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_HeaderDetailLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_HeaderDetailLink = new Control("ChildForm_HeaderDetailLink", "ID", "lnk_19171_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_HeaderDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_HeaderDetailLink);
PCMINSP_ChildForm_HeaderDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on HeaderDetailForm...", Logger.MessageType.INF);
			Control PCMINSP_HeaderDetailForm = new Control("HeaderDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDR_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_HeaderDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on HeaderDetail_MO...", Logger.MessageType.INF);
			Control PCMINSP_HeaderDetail_MO = new Control("HeaderDetail_MO", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDR_DTL_']/ancestor::form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMINSP_HeaderDetail_MO.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_HeaderDetailForm);
formBttn = PCMINSP_HeaderDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MO Text");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_MOTextLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_MOTextLink = new Control("ChildForm_MOTextLink", "ID", "lnk_19172_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_MOTextLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_MOTextLink);
PCMINSP_ChildForm_MOTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on MOTextForm...", Logger.MessageType.INF);
			Control PCMINSP_MOTextForm = new Control("MOTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDRTEXT_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_MOTextForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_MOTextForm);
formBttn = PCMINSP_MOTextForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCMINSP_MOTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCMINSP_MOTextForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on MOText_Table...", Logger.MessageType.INF);
			Control PCMINSP_MOText_Table = new Control("MOText_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOHDRTEXT_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_MOText_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_MOTextForm);
formBttn = PCMINSP_MOTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routings");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_RoutingsLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_RoutingsLink = new Control("ChildForm_RoutingsLink", "ID", "lnk_19173_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_RoutingsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_RoutingsLink);
PCMINSP_ChildForm_RoutingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on RoutingsForm...", Logger.MessageType.INF);
			Control PCMINSP_RoutingsForm = new Control("RoutingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTING_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_RoutingsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on Routings_Table...", Logger.MessageType.INF);
			Control PCMINSP_Routings_Table = new Control("Routings_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_Routings_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_RoutingsForm);
formBttn = PCMINSP_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMINSP_RoutingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMINSP_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on Routings_Operation_Sequence...", Logger.MessageType.INF);
			Control PCMINSP_Routings_Operation_Sequence = new Control("Routings_Operation_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTING_DTL_']/ancestor::form[1]/descendant::*[@id='MO_OPER_SEQ_NO']");
			CPCommon.AssertEqual(true,PCMINSP_Routings_Operation_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_RoutingsForm);
formBttn = PCMINSP_RoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Routing Header Notes");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_RoutingHeaderNotesLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_RoutingHeaderNotesLink = new Control("ChildForm_RoutingHeaderNotesLink", "ID", "lnk_19174_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_RoutingHeaderNotesLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_RoutingHeaderNotesLink);
PCMINSP_ChildForm_RoutingHeaderNotesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on RoutingHeaderNotesForm...", Logger.MessageType.INF);
			Control PCMINSP_RoutingHeaderNotesForm = new Control("RoutingHeaderNotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_ROUTINGHDR_NOTES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_RoutingHeaderNotesForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_RoutingHeaderNotesForm);
formBttn = PCMINSP_RoutingHeaderNotesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCMINSP_RoutingHeaderNotesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCMINSP_RoutingHeaderNotesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on RoutingHeaderNotes_Table...", Logger.MessageType.INF);
			Control PCMINSP_RoutingHeaderNotes_Table = new Control("RoutingHeaderNotes_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_ROUTINGHDR_NOTES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_RoutingHeaderNotes_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_RoutingHeaderNotesForm);
formBttn = PCMINSP_RoutingHeaderNotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MO Completions");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_MOCompletionsLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_MOCompletionsLink = new Control("ChildForm_MOCompletionsLink", "ID", "lnk_19175_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_MOCompletionsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_MOCompletionsLink);
PCMINSP_ChildForm_MOCompletionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on MOCompletionsForm...", Logger.MessageType.INF);
			Control PCMINSP_MOCompletionsForm = new Control("MOCompletionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNCOMPL_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_MOCompletionsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on MOCompletions_Table...", Logger.MessageType.INF);
			Control PCMINSP_MOCompletions_Table = new Control("MOCompletions_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNCOMPL_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_MOCompletions_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_MOCompletionsForm);
formBttn = PCMINSP_MOCompletionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMINSP_MOCompletionsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMINSP_MOCompletionsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on MOCompletions_TransactionDate...", Logger.MessageType.INF);
			Control PCMINSP_MOCompletions_TransactionDate = new Control("MOCompletions_TransactionDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFR_MOROUTLNCOMPL_DTL_']/ancestor::form[1]/descendant::*[@id='TRANS_DT']");
			CPCommon.AssertEqual(true,PCMINSP_MOCompletions_TransactionDate.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_MOCompletionsForm);
formBttn = PCMINSP_MOCompletionsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Subcontractor Reqs/Pos");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_SubcontractorReqsPOsLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_SubcontractorReqsPOsLink = new Control("ChildForm_SubcontractorReqsPOsLink", "ID", "lnk_19176_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_SubcontractorReqsPOsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_SubcontractorReqsPOsLink);
PCMINSP_ChildForm_SubcontractorReqsPOsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on SubContractorReqsPOsForm...", Logger.MessageType.INF);
			Control PCMINSP_SubContractorReqsPOsForm = new Control("SubContractorReqsPOsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_SubContractorReqsPOsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on SubContractorReqsPOs_PurchaseOrder_Table...", Logger.MessageType.INF);
			Control PCMINSP_SubContractorReqsPOs_PurchaseOrder_Table = new Control("SubContractorReqsPOs_PurchaseOrder_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_SubContractorReqsPOs_PurchaseOrder_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_SubContractorReqsPOsForm);
formBttn = PCMINSP_SubContractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMINSP_SubContractorReqsPOsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMINSP_SubContractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on SubContractorReqsPOs_Requisition_Requisition...", Logger.MessageType.INF);
			Control PCMINSP_SubContractorReqsPOs_Requisition_Requisition = new Control("SubContractorReqsPOs_Requisition_Requisition", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_SUBREQPOS_']/ancestor::form[1]/descendant::*[@id='RQ_ID']");
			CPCommon.AssertEqual(true,PCMINSP_SubContractorReqsPOs_Requisition_Requisition.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_SubContractorReqsPOsForm);
formBttn = PCMINSP_SubContractorReqsPOsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Documents");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_DocumentsLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_DocumentsLink = new Control("ChildForm_DocumentsLink", "ID", "lnk_19177_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_DocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_DocumentsLink);
PCMINSP_ChildForm_DocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on DocumentsForm...", Logger.MessageType.INF);
			Control PCMINSP_DocumentsForm = new Control("DocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_DocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on Documents_Table...", Logger.MessageType.INF);
			Control PCMINSP_Documents_Table = new Control("Documents_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_Documents_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_DocumentsForm);
formBttn = PCMINSP_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMINSP_DocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMINSP_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on Documents_Document...", Logger.MessageType.INF);
			Control PCMINSP_Documents_Document = new Control("Documents_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PCM_MODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PCMINSP_Documents_Document.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_DocumentsForm);
formBttn = PCMINSP_DocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on ChildForm_PartDocumentsLink...", Logger.MessageType.INF);
			Control PCMINSP_ChildForm_PartDocumentsLink = new Control("ChildForm_PartDocumentsLink", "ID", "lnk_19178_PCMSFR_MOROUTLNINSP_HDR");
			CPCommon.AssertEqual(true,PCMINSP_ChildForm_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_ChildForm_PartDocumentsLink);
PCMINSP_ChildForm_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on PartDocumentsForm...", Logger.MessageType.INF);
			Control PCMINSP_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCMINSP_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExist on PartDocuments_Table...", Logger.MessageType.INF);
			Control PCMINSP_PartDocuments_Table = new Control("PartDocuments_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMINSP_PartDocuments_Table.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_PartDocumentsForm);
formBttn = PCMINSP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMINSP_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMINSP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PCMINSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMINSP] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control PCMINSP_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,PCMINSP_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_PartDocumentsForm);
formBttn = PCMINSP_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "PCMINSP";
							CPCommon.WaitControlDisplayed(PCMINSP_MainForm);
formBttn = PCMINSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

