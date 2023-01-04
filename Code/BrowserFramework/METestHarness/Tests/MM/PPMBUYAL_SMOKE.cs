 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMBUYAL_SMOKE : TestScript
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
new Control("Assign Purchase Requisition Lines to Buyers", "xpath","//div[@class='navItem'][.='Assign Purchase Requisition Lines to Buyers']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMBUYAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMBUYAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on DefaultAssignmentDate...", Logger.MessageType.INF);
			Control PPMBUYAL_DefaultAssignmentDate = new Control("DefaultAssignmentDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DEF_ASSIGN_DATE']");
			CPCommon.AssertEqual(true,PPMBUYAL_DefaultAssignmentDate.Exists());

											Driver.SessionLogger.WriteLine("Requisition Lines");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExist on RequisitionLines_Table...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_Table = new Control("RequisitionLines_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAL_RQLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_Table.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_Form...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_Form = new Control("RequisitionLines_Form", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAL_RQLN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_Form.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_Form);
IWebElement formBttn = PPMBUYAL_RequisitionLines_Form.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAL_RequisitionLines_Form.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAL_RequisitionLines_Form.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Requisition Lines Tab");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming Select on RequisitionLines_RequisitionLineDetails_RequisitionLineTab...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab = new Control("RequisitionLines_RequisitionLineDetails_RequisitionLineTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAL_RQLN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab);
IWebElement mTab = PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Requisition Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_RequisitionLineDetails_Item...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_RequisitionLineDetails_Item = new Control("RequisitionLines_RequisitionLineDetails_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAL_RQLN_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_RequisitionLineDetails_Item.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab);
mTab = PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_CostDetails_EstUnitCost...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_CostDetails_EstUnitCost = new Control("RequisitionLines_CostDetails_EstUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAL_RQLN_']/ancestor::form[1]/descendant::*[@id='TRN_NET_UNIT_AMT']");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_CostDetails_EstUnitCost.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab);
mTab = PPMBUYAL_RequisitionLines_RequisitionLineDetails_RequisitionLineTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Delivery Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_DeliveryInformation_ShipID...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_DeliveryInformation_ShipID = new Control("RequisitionLines_DeliveryInformation_ShipID", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMBUYAL_RQLN_']/ancestor::form[1]/descendant::*[@id='SHIP_ID']");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_DeliveryInformation_ShipID.Exists());

											Driver.SessionLogger.WriteLine("Approval Process");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_RequisitionLineDetails_ApprovalProcessLink...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_RequisitionLineDetails_ApprovalProcessLink = new Control("RequisitionLines_RequisitionLineDetails_ApprovalProcessLink", "ID", "lnk_1007276_PPMBUYAL_RQLN");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_RequisitionLineDetails_ApprovalProcessLink.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_RequisitionLineDetails_ApprovalProcessLink);
PPMBUYAL_RequisitionLines_RequisitionLineDetails_ApprovalProcessLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExist on RequisitionLines_ApprovalProcess_Table...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_ApprovalProcess_Table = new Control("RequisitionLines_ApprovalProcess_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PP_APPRVL_COMMONDISABLED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_ApprovalProcess_Table.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_ApprovalProcess_Form...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_ApprovalProcess_Form = new Control("RequisitionLines_ApprovalProcess_Form", "xpath", "//div[translate(@id,'0123456789','')='pr__PP_APPRVL_COMMONDISABLED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_ApprovalProcess_Form.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_ApprovalProcess_Form);
formBttn = PPMBUYAL_RequisitionLines_ApprovalProcess_Form.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAL_RequisitionLines_ApprovalProcess_Form.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAL_RequisitionLines_ApprovalProcess_Form.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_ApprovalProcess_ApprovalRevision...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_ApprovalProcess_ApprovalRevision = new Control("RequisitionLines_ApprovalProcess_ApprovalRevision", "xpath", "//div[translate(@id,'0123456789','')='pr__PP_APPRVL_COMMONDISABLED_']/ancestor::form[1]/descendant::*[@id='RVSN_NO']");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_ApprovalProcess_ApprovalRevision.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_ApprovalProcess_Form);
formBttn = PPMBUYAL_RequisitionLines_ApprovalProcess_Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Hdr Documents");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_RequisitionLineDetails_HdrDocumentsLink...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_RequisitionLineDetails_HdrDocumentsLink = new Control("RequisitionLines_RequisitionLineDetails_HdrDocumentsLink", "ID", "lnk_1007760_PPMBUYAL_RQLN");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_RequisitionLineDetails_HdrDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_RequisitionLineDetails_HdrDocumentsLink);
PPMBUYAL_RequisitionLines_RequisitionLineDetails_HdrDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExist on HdrDocumentsTable...", Logger.MessageType.INF);
			Control PPMBUYAL_HdrDocumentsTable = new Control("HdrDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAL_HdrDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on HdrDocumentsForm...", Logger.MessageType.INF);
			Control PPMBUYAL_HdrDocumentsForm = new Control("HdrDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAL_HdrDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_HdrDocumentsForm);
formBttn = PPMBUYAL_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAL_HdrDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAL_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on HdrDocuments_Document...", Logger.MessageType.INF);
			Control PPMBUYAL_HdrDocuments_Document = new Control("HdrDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_HDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMBUYAL_HdrDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_HdrDocumentsForm);
formBttn = PPMBUYAL_HdrDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on RequisitionLines_RequisitionLineDetails_LineDocumentsLink...", Logger.MessageType.INF);
			Control PPMBUYAL_RequisitionLines_RequisitionLineDetails_LineDocumentsLink = new Control("RequisitionLines_RequisitionLineDetails_LineDocumentsLink", "ID", "lnk_1007754_PPMBUYAL_RQLN");
			CPCommon.AssertEqual(true,PPMBUYAL_RequisitionLines_RequisitionLineDetails_LineDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_RequisitionLines_RequisitionLineDetails_LineDocumentsLink);
PPMBUYAL_RequisitionLines_RequisitionLineDetails_LineDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExist on LineDocumentsTable...", Logger.MessageType.INF);
			Control PPMBUYAL_LineDocumentsTable = new Control("LineDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMBUYAL_LineDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on LineDocumentsForm...", Logger.MessageType.INF);
			Control PPMBUYAL_LineDocumentsForm = new Control("LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMBUYAL_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_LineDocumentsForm);
formBttn = PPMBUYAL_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMBUYAL_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMBUYAL_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMBUYAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMBUYAL] Perfoming VerifyExists on LineDocuments_Document...", Logger.MessageType.INF);
			Control PPMBUYAL_LineDocuments_Document = new Control("LineDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_LN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMBUYAL_LineDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_LineDocumentsForm);
formBttn = PPMBUYAL_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMBUYAL";
							CPCommon.WaitControlDisplayed(PPMBUYAL_MainForm);
formBttn = PPMBUYAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

