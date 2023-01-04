 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEQSALES_SMOKE : TestScript
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
new Control("Sales Order Entry Reports/Inquiries", "xpath","//div[@class='navItem'][.='Sales Order Entry Reports/Inquiries']").Click();
new Control("View Sales Analysis Information", "xpath","//div[@class='navItem'][.='View Sales Analysis Information']").Click();


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEQSALES_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEQSALES_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control OEQSALES_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,OEQSALES_Project.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control OEQSALES_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOHDR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEQSALES_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOHDR_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEQSALES_ChildForm);
IWebElement formBttn = OEQSALES_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEQSALES_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEQSALES_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SalesOrder...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrder = new Control("ChildForm_SalesOrder", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOHDR_CTW_']/ancestor::form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesOrder.Exists());

											Driver.SessionLogger.WriteLine("SALES ANALYSIS");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SalesAnalysisLink...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesAnalysisLink = new Control("ChildForm_SalesAnalysisLink", "ID", "lnk_1005236_OEQSALES_SOHDR_CTW");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesAnalysisLink.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SalesAnalysisLink);
OEQSALES_ChildForm_SalesAnalysisLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SalesAnalysis_RevenueCostFuncCurrency_Cost_Material...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesAnalysis_RevenueCostFuncCurrency_Cost_Material = new Control("ChildForm_SalesAnalysis_RevenueCostFuncCurrency_Cost_Material", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOINVCLN_']/ancestor::form[1]/descendant::*[@id='EXT_CST_AMT']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesAnalysis_RevenueCostFuncCurrency_Cost_Material.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming Close on ChildForm_SalesAnalysisForm...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesAnalysisForm = new Control("ChildForm_SalesAnalysisForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOINVCLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SalesAnalysisForm);
formBttn = OEQSALES_ChildForm_SalesAnalysisForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("SALES ORDER LINES");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SalesOrderLinesLink...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrderLinesLink = new Control("ChildForm_SalesOrderLinesLink", "ID", "lnk_1005235_OEQSALES_SOHDR_CTW");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesOrderLinesLink.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SalesOrderLinesLink);
OEQSALES_ChildForm_SalesOrderLinesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExist on ChildForm_SalesOrderLinesFormTable...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrderLinesFormTable = new Control("ChildForm_SalesOrderLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesOrderLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming ClickButton on ChildForm_SalesOrderLinesForm...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrderLinesForm = new Control("ChildForm_SalesOrderLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOLN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SalesOrderLinesForm);
formBttn = OEQSALES_ChildForm_SalesOrderLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEQSALES_ChildForm_SalesOrderLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEQSALES_ChildForm_SalesOrderLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesOrderLinesForm.Exists());

													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SalesOrderLines_LineDetails_SOLine...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrderLines_LineDetails_SOLine = new Control("ChildForm_SalesOrderLines_LineDetails_SOLine", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOLN_CTW_']/ancestor::form[1]/descendant::*[@id='SO_LN_NO']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesOrderLines_LineDetails_SOLine.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming Select on ChildForm_SalesOrderLines_SalesOrderLinesTab...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrderLines_SalesOrderLinesTab = new Control("ChildForm_SalesOrderLines_SalesOrderLinesTab", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SalesOrderLines_SalesOrderLinesTab);
IWebElement mTab = OEQSALES_ChildForm_SalesOrderLines_SalesOrderLinesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Order Status").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SalesOrderLines_OrderStatus_QuantitySalesUM_Issued...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SalesOrderLines_OrderStatus_QuantitySalesUM_Issued = new Control("ChildForm_SalesOrderLines_OrderStatus_QuantitySalesUM_Issued", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_SOLN_CTW_']/ancestor::form[1]/descendant::*[@id='ISSUE_QTY']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SalesOrderLines_OrderStatus_QuantitySalesUM_Issued.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SalesOrderLinesForm);
formBttn = OEQSALES_ChildForm_SalesOrderLinesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EXCHANGE RATES");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_ExchangeRatesLink...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_ExchangeRatesLink = new Control("ChildForm_ExchangeRatesLink", "ID", "lnk_1005237_OEQSALES_SOHDR_CTW");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_ExchangeRatesLink);
OEQSALES_ChildForm_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_ExchangeRatesForm...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_ExchangeRatesForm = new Control("ChildForm_ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_ExchangeRates_TransactionCurrency = new Control("ChildForm_ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_ExchangeRatesForm);
formBttn = OEQSALES_ChildForm_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("USER DEFINED INFO");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_UserDefinedInfoLink...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_UserDefinedInfoLink = new Control("ChildForm_UserDefinedInfoLink", "ID", "lnk_1007514_OEQSALES_SOHDR_CTW");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_UserDefinedInfoLink.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_UserDefinedInfoLink);
OEQSALES_ChildForm_UserDefinedInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming Close on ChildForm_UserDefinedInfoForm...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_UserDefinedInfoForm = new Control("ChildForm_UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_USERDEFINEDINFO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_UserDefinedInfoForm);
formBttn = OEQSALES_ChildForm_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("SO HEADER DOCUMENT");


												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SOHeaderDocumentsLink...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SOHeaderDocumentsLink = new Control("ChildForm_SOHeaderDocumentsLink", "ID", "lnk_1007511_OEQSALES_SOHDR_CTW");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SOHeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SOHeaderDocumentsLink);
OEQSALES_ChildForm_SOHeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExist on ChildForm_SOHeaderDocumentsFormTable...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SOHeaderDocumentsFormTable = new Control("ChildForm_SOHeaderDocumentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SOHeaderDocumentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming ClickButton on ChildForm_SOHeaderDocumentsForm...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SOHeaderDocumentsForm = new Control("ChildForm_SOHeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_DOCUMENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SOHeaderDocumentsForm);
formBttn = OEQSALES_ChildForm_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEQSALES_ChildForm_SOHeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEQSALES_ChildForm_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.AssertEqual(true,OEQSALES_ChildForm_SOHeaderDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "OEQSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEQSALES] Perfoming VerifyExists on ChildForm_SOHeaderDocument_Document...", Logger.MessageType.INF);
			Control OEQSALES_ChildForm_SOHeaderDocument_Document = new Control("ChildForm_SOHeaderDocument_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__OEQSALES_DOCUMENTS_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,OEQSALES_ChildForm_SOHeaderDocument_Document.Exists());

												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_ChildForm_SOHeaderDocumentsForm);
formBttn = OEQSALES_ChildForm_SOHeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "OEQSALES";
							CPCommon.WaitControlDisplayed(OEQSALES_MainForm);
formBttn = OEQSALES_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

