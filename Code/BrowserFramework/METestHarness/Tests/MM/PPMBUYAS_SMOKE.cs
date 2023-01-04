 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMBUYAS_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Purchase Requisitions", "xpath","//div[@class='navItem'][.='Purchase Requisitions']").Click();
new Control("Assign Purchase Requisitions to Buyers", "xpath","//div[@class='navItem'][.='Assign Purchase Requisitions to Buyers']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMBUYAS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMBUYAS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on DefaultAssignmentDate...", Logger.MessageType.INF);
			Control PPMBUYAS_DefaultAssignmentDate = new Control("DefaultAssignmentDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DEFAULT_ASSIGNMENT_DATE']");
			CPCommon.AssertEqual(true,PPMBUYAS_DefaultAssignmentDate.Exists());

											Driver.SessionLogger.WriteLine("Details");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExist on DetailsTable...", Logger.MessageType.INF);
			Control PPMBUYAS_DetailsTable = new Control("DetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQHDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAS_DetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on DetailsForm...", Logger.MessageType.INF);
			Control PPMBUYAS_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAS_DetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_DetailsForm);
IWebElement formBttn = PPMBUYAS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAS_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on Details_Requisition...", Logger.MessageType.INF);
			Control PPMBUYAS_Details_Requisition = new Control("Details_Requisition", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQHDR_']/ancestor::form[1]/descendant::*[@id='RQ_ID']");
			CPCommon.AssertEqual(true,PPMBUYAS_Details_Requisition.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_DetailsForm);
formBttn = PPMBUYAS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Next']")).Count <= 0 ? PPMBUYAS_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Next')]")).FirstOrDefault() :
PPMBUYAS_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Next')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Next] found and clicked.", Logger.MessageType.INF);
}


												Driver.SessionLogger.WriteLine("Approval Process");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on Details_ApprovalProcessLink...", Logger.MessageType.INF);
			Control PPMBUYAS_Details_ApprovalProcessLink = new Control("Details_ApprovalProcessLink", "ID", "lnk_1007277_PPMBUYAS_RQHDR");
			CPCommon.AssertEqual(true,PPMBUYAS_Details_ApprovalProcessLink.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_Details_ApprovalProcessLink);
PPMBUYAS_Details_ApprovalProcessLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExist on ApprovalProcessTable...", Logger.MessageType.INF);
			Control PPMBUYAS_ApprovalProcessTable = new Control("ApprovalProcessTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PP_APPRVL_COMMONDISABLED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAS_ApprovalProcessTable.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on ApprovalProcessForm...", Logger.MessageType.INF);
			Control PPMBUYAS_ApprovalProcessForm = new Control("ApprovalProcessForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PP_APPRVL_COMMONDISABLED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAS_ApprovalProcessForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_ApprovalProcessForm);
formBttn = PPMBUYAS_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAS_ApprovalProcessForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAS_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on ApprovalProcess_ApprovalRevision...", Logger.MessageType.INF);
			Control PPMBUYAS_ApprovalProcess_ApprovalRevision = new Control("ApprovalProcess_ApprovalRevision", "xpath", "//div[translate(@id,'0123456789','')='pr__PP_APPRVL_COMMONDISABLED_']/ancestor::form[1]/descendant::*[@id='RVSN_NO']");
			CPCommon.AssertEqual(true,PPMBUYAS_ApprovalProcess_ApprovalRevision.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_ApprovalProcessForm);
formBttn = PPMBUYAS_ApprovalProcessForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Assign Lines");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on Details_AssignLinesLink...", Logger.MessageType.INF);
			Control PPMBUYAS_Details_AssignLinesLink = new Control("Details_AssignLinesLink", "ID", "lnk_1007262_PPMBUYAS_RQHDR");
			CPCommon.AssertEqual(true,PPMBUYAS_Details_AssignLinesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_Details_AssignLinesLink);
PPMBUYAS_Details_AssignLinesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExist on AssignLinesTable...", Logger.MessageType.INF);
			Control PPMBUYAS_AssignLinesTable = new Control("AssignLinesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQLN_ASSIGNLINES_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAS_AssignLinesTable.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on AssignLinesForm...", Logger.MessageType.INF);
			Control PPMBUYAS_AssignLinesForm = new Control("AssignLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQLN_ASSIGNLINES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAS_AssignLinesForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_AssignLinesForm);
formBttn = PPMBUYAS_AssignLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAS_AssignLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAS_AssignLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Assign Lines Tab");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming Select on AssignLinesTab...", Logger.MessageType.INF);
			Control PPMBUYAS_AssignLinesTab = new Control("AssignLinesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQLN_ASSIGNLINES_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMBUYAS_AssignLinesTab);
IWebElement mTab = PPMBUYAS_AssignLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Requisition Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on AssignLines_RequisitionLineDetails_LineStatus...", Logger.MessageType.INF);
			Control PPMBUYAS_AssignLines_RequisitionLineDetails_LineStatus = new Control("AssignLines_RequisitionLineDetails_LineStatus", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQLN_ASSIGNLINES_']/ancestor::form[1]/descendant::*[@id='S_RQ_STATUS_CD']");
			CPCommon.AssertEqual(true,PPMBUYAS_AssignLines_RequisitionLineDetails_LineStatus.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_AssignLinesTab);
mTab = PPMBUYAS_AssignLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on AssignLines_CostDetails_EstUnitCost...", Logger.MessageType.INF);
			Control PPMBUYAS_AssignLines_CostDetails_EstUnitCost = new Control("AssignLines_CostDetails_EstUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQLN_ASSIGNLINES_']/ancestor::form[1]/descendant::*[@id='TRN_NET_UNIT_AMT']");
			CPCommon.AssertEqual(true,PPMBUYAS_AssignLines_CostDetails_EstUnitCost.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_AssignLinesTab);
mTab = PPMBUYAS_AssignLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Delivery Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on AssignLines_DeliveryInfo_ShipID...", Logger.MessageType.INF);
			Control PPMBUYAS_AssignLines_DeliveryInfo_ShipID = new Control("AssignLines_DeliveryInfo_ShipID", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAS_RQLN_ASSIGNLINES_']/ancestor::form[1]/descendant::*[@id='SHIP_ID']");
			CPCommon.AssertEqual(true,PPMBUYAS_AssignLines_DeliveryInfo_ShipID.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_AssignLinesForm);
formBttn = PPMBUYAS_AssignLinesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Hdr Documents");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on Details_HdrDocumentsLink...", Logger.MessageType.INF);
			Control PPMBUYAS_Details_HdrDocumentsLink = new Control("Details_HdrDocumentsLink", "ID", "lnk_1007785_PPMBUYAS_RQHDR");
			CPCommon.AssertEqual(true,PPMBUYAS_Details_HdrDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_Details_HdrDocumentsLink);
PPMBUYAS_Details_HdrDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExist on HdrDocumentsTable...", Logger.MessageType.INF);
			Control PPMBUYAS_HdrDocumentsTable = new Control("HdrDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAS_HdrDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on HdrDocumentsForm...", Logger.MessageType.INF);
			Control PPMBUYAS_HdrDocumentsForm = new Control("HdrDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAS_HdrDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_HdrDocumentsForm);
formBttn = PPMBUYAS_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAS_HdrDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAS_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMBUYAS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAS] Perfoming VerifyExists on HdrDocuments_Document...", Logger.MessageType.INF);
			Control PPMBUYAS_HdrDocuments_Document = new Control("HdrDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMBUYAS_HdrDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_HdrDocumentsForm);
formBttn = PPMBUYAS_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMBUYAS";
							CPCommon.WaitControlDisplayed(PPMBUYAS_MainForm);
formBttn = PPMBUYAS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

