 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMQTI_SMOKE : TestScript
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
new Control("Manage Vendor Quotes By Item", "xpath","//div[@class='navItem'][.='Manage Vendor Quotes By Item']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMQTI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMQTI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on Item...", Logger.MessageType.INF);
			Control PPMQTI_Item = new Control("Item", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PPMQTI_Item.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							PPMQTI_Item.Click();
PPMQTI_Item.SendKeys(" 3105C28-20", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PPMQTI_Item.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExist on ChildTable...", Logger.MessageType.INF);
			Control PPMQTI_ChildTable = new Control("ChildTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMQTI_ChildTable.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMQTI_ChildForm);
IWebElement formBttn = PPMQTI_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMQTI_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMQTI_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.AssertEqual(true,PPMQTI_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming Select on ChildForm_HeaderDetails_ChildFormTab...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_HeaderDetails_ChildFormTab = new Control("ChildForm_HeaderDetails_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_HeaderDetails_ChildFormTab);
IWebElement mTab = PPMQTI_ChildForm_HeaderDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_HeaderDetails_Terms...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_HeaderDetails_Terms = new Control("ChildForm_HeaderDetails_Terms", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]/descendant::*[@id='TERMS_DC']");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_HeaderDetails_Terms.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_HeaderDetails_ChildFormTab);
mTab = PPMQTI_ChildForm_HeaderDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_LineDetails_QuoteLine...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_LineDetails_QuoteLine = new Control("ChildForm_LineDetails_QuoteLine", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]/descendant::*[@id='QT_LN_NO']");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_LineDetails_QuoteLine.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_HeaderDetails_ChildFormTab);
mTab = PPMQTI_ChildForm_HeaderDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "RFQ Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_RFQInfo_RFQ...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_RFQInfo_RFQ = new Control("ChildForm_RFQInfo_RFQ", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]/descendant::*[@id='ORIG_RFQ_ID']");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_RFQInfo_RFQ.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_HeaderDetails_ChildFormTab);
mTab = PPMQTI_ChildForm_HeaderDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Manuf/Vend Parts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_ManufVendParts_Manufacturer...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_ManufVendParts_Manufacturer = new Control("ChildForm_ManufVendParts_Manufacturer", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLN_CHILD_']/ancestor::form[1]/descendant::*[@id='MANUF_ID']");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_ManufVendParts_Manufacturer.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_HeaderDetails_ChildFormTab);
mTab = PPMQTI_ChildForm_HeaderDetails_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("Qty Breakpts");


												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_QtyBreakptsLink...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_QtyBreakptsLink = new Control("ChildForm_QtyBreakptsLink", "ID", "lnk_1003502_PPMQTI_QTLN_CHILD");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_QtyBreakptsLink.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_QtyBreakptsLink);
PPMQTI_ChildForm_QtyBreakptsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExist on QtyBreakptsTable...", Logger.MessageType.INF);
			Control PPMQTI_QtyBreakptsTable = new Control("QtyBreakptsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNBRK_QTYBRPTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMQTI_QtyBreakptsTable.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming ClickButton on QtyBreakptsForm...", Logger.MessageType.INF);
			Control PPMQTI_QtyBreakptsForm = new Control("QtyBreakptsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNBRK_QTYBRPTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMQTI_QtyBreakptsForm);
formBttn = PPMQTI_QtyBreakptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMQTI_QtyBreakptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMQTI_QtyBreakptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.AssertEqual(true,PPMQTI_QtyBreakptsForm.Exists());

													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on QtyBreakpts_MinimumQuantity...", Logger.MessageType.INF);
			Control PPMQTI_QtyBreakpts_MinimumQuantity = new Control("QtyBreakpts_MinimumQuantity", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_QTLNBRK_QTYBRPTS_']/ancestor::form[1]/descendant::*[@id='MIN_QTY']");
			CPCommon.AssertEqual(true,PPMQTI_QtyBreakpts_MinimumQuantity.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_QtyBreakptsForm);
formBttn = PPMQTI_QtyBreakptsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Line Charges");


												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_LineChargesLink...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_LineChargesLink = new Control("ChildForm_LineChargesLink", "ID", "lnk_1003503_PPMQTI_QTLN_CHILD");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_LineChargesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_LineChargesLink);
PPMQTI_ChildForm_LineChargesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExist on LineCharges_Table...", Logger.MessageType.INF);
			Control PPMQTI_LineCharges_Table = new Control("LineCharges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLNCHG_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMQTI_LineCharges_Table.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming Close on LineChargesForm...", Logger.MessageType.INF);
			Control PPMQTI_LineChargesForm = new Control("LineChargesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMQTI_QTLNCHG_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PPMQTI_LineChargesForm);
formBttn = PPMQTI_LineChargesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control PPMQTI_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_1003987_PPMQTI_QTLN_CHILD");
			CPCommon.AssertEqual(true,PPMQTI_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ChildForm_ExchangeRatesLink);
PPMQTI_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control PPMQTI_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMQTI_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMQTI] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control PPMQTI_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,PPMQTI_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_ExchangeRatesForm);
formBttn = PPMQTI_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PPMQTI";
							CPCommon.WaitControlDisplayed(PPMQTI_MainForm);
formBttn = PPMQTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

