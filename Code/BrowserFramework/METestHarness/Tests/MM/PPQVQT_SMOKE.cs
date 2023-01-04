 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPQVQT_SMOKE : TestScript
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
new Control("VENDOR Quotes", "xpath","//div[@class='navItem'][.='VENDOR Quotes']").Click();
new Control("View VENDOR Quotes", "xpath","//div[@class='navItem'][.='View VENDOR Quotes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPQVQT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPQVQT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on MainForm_Vendor...", Logger.MessageType.INF);
			Control PPQVQT_MainForm_Vendor = new Control("MainForm_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,PPQVQT_MainForm_Vendor.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPQVQT_ChildForm);
IWebElement formBttn = PPQVQT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PPQVQT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PPQVQT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PPQVQT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPQVQT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_ChildForm);
formBttn = PPQVQT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPQVQT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPQVQT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.AssertEqual(true,PPQVQT_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_Quote...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_Quote = new Control("ChildForm_Quote", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]/descendant::*[@id='QUOTE_ID']");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_Quote.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming Select on ChildForm_tab1...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_tab1 = new Control("ChildForm_tab1", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPQVQT_ChildForm_tab1);
IWebElement mTab = PPQVQT_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Quote Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_QuoteLineDetails_LineType...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_QuoteLineDetails_LineType = new Control("ChildForm_QuoteLineDetails_LineType", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]/descendant::*[@id='S_PO_LN_TYPE']");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_QuoteLineDetails_LineType.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_ChildForm_tab1);
mTab = PPQVQT_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manufacturer/VEND Parts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_ManufacturerVendParts_Manufacturer...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_ManufacturerVendParts_Manufacturer = new Control("ChildForm_ManufacturerVendParts_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]/descendant::*[@id='MANUF_NAME']");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_ManufacturerVendParts_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_ChildForm_tab1);
mTab = PPQVQT_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_ChildForm_tab1);
mTab = PPQVQT_ChildForm_tab1.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_Header_Terms...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_Header_Terms = new Control("ChildForm_Header_Terms", "xpath", "//div[translate(@id,'0123456789','')='pr__PPQVQT_CTW_MAIN_']/ancestor::form[1]/descendant::*[@id='TERMS_DC']");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_Header_Terms.Exists());

											Driver.SessionLogger.WriteLine("Qty Breakpts");


												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_QtyBreakptsLink...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_QtyBreakptsLink = new Control("ChildForm_QtyBreakptsLink", "ID", "lnk_1007404_PPQVQT_CTW_MAIN");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_QtyBreakptsLink.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_ChildForm_QtyBreakptsLink);
PPQVQT_ChildForm_QtyBreakptsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExist on QtyBreakptsFormTable...", Logger.MessageType.INF);
			Control PPQVQT_QtyBreakptsFormTable = new Control("QtyBreakptsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNBRK_QTYBRPTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPQVQT_QtyBreakptsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming ClickButton on QtyBreakptsForm...", Logger.MessageType.INF);
			Control PPQVQT_QtyBreakptsForm = new Control("QtyBreakptsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNBRK_QTYBRPTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPQVQT_QtyBreakptsForm);
formBttn = PPQVQT_QtyBreakptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPQVQT_QtyBreakptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPQVQT_QtyBreakptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.AssertEqual(true,PPQVQT_QtyBreakptsForm.Exists());

													
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_QtyBreakpts_MinimumQuantity...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_QtyBreakpts_MinimumQuantity = new Control("ChildForm_QtyBreakpts_MinimumQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNBRK_QTYBRPTS_']/ancestor::form[1]/descendant::*[@id='MIN_QTY']");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_QtyBreakpts_MinimumQuantity.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_QtyBreakptsForm);
formBttn = PPQVQT_QtyBreakptsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Charges");


												
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming VerifyExists on ChildForm_LineChargesLink...", Logger.MessageType.INF);
			Control PPQVQT_ChildForm_LineChargesLink = new Control("ChildForm_LineChargesLink", "ID", "lnk_1007405_PPQVQT_CTW_MAIN");
			CPCommon.AssertEqual(true,PPQVQT_ChildForm_LineChargesLink.Exists());

												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_ChildForm_LineChargesLink);
PPQVQT_ChildForm_LineChargesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPQVQT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPQVQT] Perfoming Close on LineChargesForm...", Logger.MessageType.INF);
			Control PPQVQT_LineChargesForm = new Control("LineChargesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNCHG_LINE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPQVQT_LineChargesForm);
formBttn = PPQVQT_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PPQVQT";
							CPCommon.WaitControlDisplayed(PPQVQT_MainForm);
formBttn = PPQVQT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

