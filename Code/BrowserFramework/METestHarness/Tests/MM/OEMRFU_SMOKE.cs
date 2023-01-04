 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMRFU_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Invoices", "xpath","//div[@class='navItem'][.='Sales Order Invoices']").Click();
new Control("Select Invoices Ready for Use/Acceptance", "xpath","//div[@class='navItem'][.='Select Invoices Ready for Use/Acceptance']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMRFU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMRFU_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on SalesOrder...", Logger.MessageType.INF);
			Control OEMRFU_SalesOrder = new Control("SalesOrder", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,OEMRFU_SalesOrder.Exists());

											Driver.SessionLogger.WriteLine("InvoiceDetailsForm");


												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExist on InvoiceDetailsFormTable...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetailsFormTable = new Control("InvoiceDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCHDR_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming ClickButton on InvoiceDetailsForm...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetailsForm = new Control("InvoiceDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCHDR_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetailsForm);
IWebElement formBttn = OEMRFU_InvoiceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMRFU_InvoiceDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMRFU_InvoiceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.AssertEqual(true,OEMRFU_InvoiceDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_GeneralInfo_SalesOrder...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_GeneralInfo_SalesOrder = new Control("InvoiceDetails_GeneralInfo_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCHDR_DTL_']/ancestor::form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_GeneralInfo_SalesOrder.Exists());

											Driver.SessionLogger.WriteLine("Query");


												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetailsForm);
formBttn = OEMRFU_InvoiceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? OEMRFU_InvoiceDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
OEMRFU_InvoiceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Query...", Logger.MessageType.INF);
			Control Query_Query = new Control("Query", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Query);
if (Query_Query.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Query.Click(5,5);
else Query_Query.Click(4.5);


											Driver.SessionLogger.WriteLine("InvoiceLineInformation Link");


												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_InvoiceLineInformationLink...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_InvoiceLineInformationLink = new Control("InvoiceDetails_InvoiceLineInformationLink", "ID", "lnk_3570_OEMRFU_SOINVCHDR_DTL");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_InvoiceLineInformationLink.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_InvoiceLineInformationLink);
OEMRFU_InvoiceDetails_InvoiceLineInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExist on InvoiceDetails_InvoiceLineInformationFormTable...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_InvoiceLineInformationFormTable = new Control("InvoiceDetails_InvoiceLineInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCLN_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_InvoiceLineInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming ClickButton on InvoiceDetails_InvoiceLineInformationForm...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_InvoiceLineInformationForm = new Control("InvoiceDetails_InvoiceLineInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCLN_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_InvoiceLineInformationForm);
formBttn = OEMRFU_InvoiceDetails_InvoiceLineInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMRFU_InvoiceDetails_InvoiceLineInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMRFU_InvoiceDetails_InvoiceLineInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_InvoiceLineInformationForm.Exists());

												Driver.SessionLogger.WriteLine("InvoiceLineInformationTab");


												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming Select on InvoiceDetails_InvoiceLineInformationTab...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_InvoiceLineInformationTab = new Control("InvoiceDetails_InvoiceLineInformationTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCLN_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_InvoiceLineInformationTab);
IWebElement mTab = OEMRFU_InvoiceDetails_InvoiceLineInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Item Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_InvoiceLineInformation_ItemDetails_InvoiceLine...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_InvoiceLineInformation_ItemDetails_InvoiceLine = new Control("InvoiceDetails_InvoiceLineInformation_ItemDetails_InvoiceLine", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCLN_DTL_']/ancestor::form[1]/descendant::*[@id='SO_INVC_LN_NO']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_InvoiceLineInformation_ItemDetails_InvoiceLine.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_InvoiceLineInformationTab);
mTab = OEMRFU_InvoiceDetails_InvoiceLineInformationTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Amounts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_InvoiceLineInformation_Amounts_GrossUnitPrice...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_InvoiceLineInformation_Amounts_GrossUnitPrice = new Control("InvoiceDetails_InvoiceLineInformation_Amounts_GrossUnitPrice", "xpath", "//div[translate(@id,'0123456789','')='pr__OEMRFU_SOINVCLN_DTL_']/ancestor::form[1]/descendant::*[@id='GROSS_UNIT_PRC_AMT']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_InvoiceLineInformation_Amounts_GrossUnitPrice.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_InvoiceLineInformationForm);
formBttn = OEMRFU_InvoiceDetails_InvoiceLineInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("WAWF Link");


												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_WAWFLink...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_WAWFLink = new Control("InvoiceDetails_WAWFLink", "ID", "lnk_3571_OEMRFU_SOINVCHDR_DTL");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_WAWFLink.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_WAWFLink);
OEMRFU_InvoiceDetails_WAWFLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_WAWFForm...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_WAWFForm = new Control("InvoiceDetails_WAWFForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OE_SOINVCHDR_WAWF_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_WAWFForm.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExists on InvoiceDetails_WAWF_Invoice...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_WAWF_Invoice = new Control("InvoiceDetails_WAWF_Invoice", "xpath", "//div[translate(@id,'0123456789','')='pr__OE_SOINVCHDR_WAWF_']/ancestor::form[1]/descendant::*[@id='SO_INVC_ID']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_WAWF_Invoice.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_WAWFForm);
formBttn = OEMRFU_InvoiceDetails_WAWFForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEMRFU_InvoiceDetails_WAWFForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEMRFU_InvoiceDetails_WAWFForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEMRFU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMRFU] Perfoming VerifyExist on InvoiceDetails_WAWFFormTable...", Logger.MessageType.INF);
			Control OEMRFU_InvoiceDetails_WAWFFormTable = new Control("InvoiceDetails_WAWFFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OE_SOINVCHDR_WAWF_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMRFU_InvoiceDetails_WAWFFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_InvoiceDetails_WAWFForm);
formBttn = OEMRFU_InvoiceDetails_WAWFForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OEMRFU";
							CPCommon.WaitControlDisplayed(OEMRFU_MainForm);
formBttn = OEMRFU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

