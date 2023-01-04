 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRFQV_SMOKE : TestScript
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
new Control("Vendor Quotes", "xpath","//div[@class='navItem'][.='Vendor Quotes']").Click();
new Control("Manage Request for Quotes By Vendor", "xpath","//div[@class='navItem'][.='Manage Request for Quotes By Vendor']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on Identification_RequestForQuoteID...", Logger.MessageType.INF);
			Control PPMRFQV_Identification_RequestForQuoteID = new Control("Identification_RequestForQuoteID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RFQ_ID']");
			CPCommon.AssertEqual(true,PPMRFQV_Identification_RequestForQuoteID.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRFQV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_MainForm);
IWebElement formBttn = PPMRFQV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMRFQV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMRFQV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPMRFQV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQV_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("HEADER STANDARD");


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming Click on HeaderStandardTextFormLink...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderStandardTextFormLink = new Control("HeaderStandardTextFormLink", "ID", "lnk_1003647_PPMRFQV_RFQHDR_VENDOR");
			CPCommon.WaitControlDisplayed(PPMRFQV_HeaderStandardTextFormLink);
PPMRFQV_HeaderStandardTextFormLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on HeaderStandardTextForm...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderStandardTextForm = new Control("HeaderStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_HDRTXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_HeaderStandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExist on HeaderStandardTextFormTable...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderStandardTextFormTable = new Control("HeaderStandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_HDRTXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQV_HeaderStandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_HeaderStandardTextForm);
formBttn = PPMRFQV_HeaderStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("header documents");


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming Click on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007842_PPMRFQV_RFQHDR_VENDOR");
			CPCommon.WaitControlDisplayed(PPMRFQV_HeaderDocumentsLink);
PPMRFQV_HeaderDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQHDR_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_HeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExist on HeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderDocumentsFormTable = new Control("HeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQHDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQV_HeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_HeaderDocumentsForm);
formBttn = PPMRFQV_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRFQV_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRFQV_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on HeaderDocuments_Document...", Logger.MessageType.INF);
			Control PPMRFQV_HeaderDocuments_Document = new Control("HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQHDR_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,PPMRFQV_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_HeaderDocumentsForm);
formBttn = PPMRFQV_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("cf_line standard text");


												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_MainForm);
formBttn = PPMRFQV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PPMRFQV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PPMRFQV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_RFQLN_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming Click on ChildForm_LineStandardTextLink...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineStandardTextLink = new Control("ChildForm_LineStandardTextLink", "ID", "lnk_1003651_PPMRFQV_RFQLN_CHILD");
			CPCommon.WaitControlDisplayed(PPMRFQV_ChildForm_LineStandardTextLink);
PPMRFQV_ChildForm_LineStandardTextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on ChildForm_LineStandardTextForm...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineStandardTextForm = new Control("ChildForm_LineStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_RFQLNTEXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineStandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExist on ChildForm_LineStandardTextFormTable...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineStandardTextFormTable = new Control("ChildForm_LineStandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_RFQLNTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineStandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_ChildForm_LineStandardTextForm);
formBttn = PPMRFQV_ChildForm_LineStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("cf_line quantity");


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming Click on ChildForm_LineQuantityBreakpointsLink...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineQuantityBreakpointsLink = new Control("ChildForm_LineQuantityBreakpointsLink", "ID", "lnk_1004479_PPMRFQV_RFQLN_CHILD");
			CPCommon.WaitControlDisplayed(PPMRFQV_ChildForm_LineQuantityBreakpointsLink);
PPMRFQV_ChildForm_LineQuantityBreakpointsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on ChildForm_LineQuantityBreakpointsForm...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineQuantityBreakpointsForm = new Control("ChildForm_LineQuantityBreakpointsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_RFQLNBRK_QUBR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineQuantityBreakpointsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExist on ChildForm_LineQuantityBreakpointsFormTable...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineQuantityBreakpointsFormTable = new Control("ChildForm_LineQuantityBreakpointsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMRFQV_RFQLNBRK_QUBR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineQuantityBreakpointsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_ChildForm_LineQuantityBreakpointsForm);
formBttn = PPMRFQV_ChildForm_LineQuantityBreakpointsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("cf_line documents");


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming Click on ChildForm_LineDocumentsLink...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineDocumentsLink = new Control("ChildForm_LineDocumentsLink", "ID", "lnk_1007843_PPMRFQV_RFQLN_CHILD");
			CPCommon.WaitControlDisplayed(PPMRFQV_ChildForm_LineDocumentsLink);
PPMRFQV_ChildForm_LineDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExist on ChildForm_LineDocumentsFormTable...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineDocumentsFormTable = new Control("ChildForm_LineDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on ChildForm_LineDocumentsForm...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineDocumentsForm = new Control("ChildForm_LineDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQLN_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_ChildForm_LineDocumentsForm);
formBttn = PPMRFQV_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRFQV_ChildForm_LineDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRFQV_ChildForm_LineDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMRFQV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRFQV] Perfoming VerifyExists on ChildForm_LineDocuments_Rev...", Logger.MessageType.INF);
			Control PPMRFQV_ChildForm_LineDocuments_Rev = new Control("ChildForm_LineDocuments_Rev", "xpath", "//div[translate(@id,'0123456789','')='pr__PPM_RFQLN_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_RVSN_ID']");
			CPCommon.AssertEqual(true,PPMRFQV_ChildForm_LineDocuments_Rev.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMRFQV";
							CPCommon.WaitControlDisplayed(PPMRFQV_MainForm);
formBttn = PPMRFQV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

