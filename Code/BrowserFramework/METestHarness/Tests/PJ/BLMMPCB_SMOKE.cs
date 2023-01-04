 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMMPCB_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Milestone Percent Complete Bills Processing", "xpath","//div[@class='navItem'][.='Milestone Percent Complete Bills Processing']").Click();
new Control("Manage Milestone Percent Complete Bills", "xpath","//div[@class='navItem'][.='Manage Milestone Percent Complete Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMMPCB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMMPCB_MainForm);
IWebElement formBttn = BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMMPCB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMMPCB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMPCB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainForm);
formBttn = BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMMPCB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.AssertEqual(true,BLMMPCB_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control BLMMPCB_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMMPCB_Identification_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BLMMPCB_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMMPCB_MainTab);
IWebElement mTab = BLMMPCB_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Bill Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on BillDetails_Customer...", Logger.MessageType.INF);
			Control BLMMPCB_BillDetails_Customer = new Control("BillDetails_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLMMPCB_BillDetails_Customer.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainTab);
mTab = BLMMPCB_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Addresses").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on Addresses_BillAddress_Address...", Logger.MessageType.INF);
			Control BLMMPCB_Addresses_BillAddress_Address = new Control("Addresses_BillAddress_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ADDR_DC']");
			CPCommon.AssertEqual(true,BLMMPCB_Addresses_BillAddress_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainTab);
mTab = BLMMPCB_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Misc Charges").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on MiscCharges_OtherCharges_Code...", Logger.MessageType.INF);
			Control BLMMPCB_MiscCharges_OtherCharges_Code = new Control("MiscCharges_OtherCharges_Code", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OTH_CHG_CD1']");
			CPCommon.AssertEqual(true,BLMMPCB_MiscCharges_OtherCharges_Code.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainTab);
mTab = BLMMPCB_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Report Options").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on ReportOptions_Columns_ScheduledValue...", Logger.MessageType.INF);
			Control BLMMPCB_ReportOptions_Columns_ScheduledValue = new Control("ReportOptions_Columns_ScheduledValue", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PRT_CONTR_VAL_FL']");
			CPCommon.AssertEqual(true,BLMMPCB_ReportOptions_Columns_ScheduledValue.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainTab);
mTab = BLMMPCB_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Summary Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on SummaryInformation_InvoiceTotals_Billing_TotalContractValueCeiling...", Logger.MessageType.INF);
			Control BLMMPCB_SummaryInformation_InvoiceTotals_Billing_TotalContractValueCeiling = new Control("SummaryInformation_InvoiceTotals_Billing_TotalContractValueCeiling", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TRN_CEILING_AMT']");
			CPCommon.AssertEqual(true,BLMMPCB_SummaryInformation_InvoiceTotals_Billing_TotalContractValueCeiling.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainTab);
mTab = BLMMPCB_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Statement Of Work").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on StatementOfWork_StatementOfWork...", Logger.MessageType.INF);
			Control BLMMPCB_StatementOfWork_StatementOfWork = new Control("StatementOfWork_StatementOfWork", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATEMENT_WRK_DESC']");
			CPCommon.AssertEqual(true,BLMMPCB_StatementOfWork_StatementOfWork.Exists());

											Driver.SessionLogger.WriteLine("LINE DETAILS");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExist on LineDetailsFormTable...", Logger.MessageType.INF);
			Control BLMMPCB_LineDetailsFormTable = new Control("LineDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_MILESTONEINVCLN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMPCB_LineDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming ClickButton on LineDetailsForm...", Logger.MessageType.INF);
			Control BLMMPCB_LineDetailsForm = new Control("LineDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_MILESTONEINVCLN_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMMPCB_LineDetailsForm);
formBttn = BLMMPCB_LineDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMMPCB_LineDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMMPCB_LineDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.AssertEqual(true,BLMMPCB_LineDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on LineDetails_Line...", Logger.MessageType.INF);
			Control BLMMPCB_LineDetails_Line = new Control("LineDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_MILESTONEINVCLN_']/ancestor::form[1]/descendant::*[@id='MILESTONE_LN_NO']");
			CPCommon.AssertEqual(true,BLMMPCB_LineDetails_Line.Exists());

											Driver.SessionLogger.WriteLine("CURRENCY LINE INFO");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on LineDetails_CurrencyLineInfoLink...", Logger.MessageType.INF);
			Control BLMMPCB_LineDetails_CurrencyLineInfoLink = new Control("LineDetails_CurrencyLineInfoLink", "ID", "lnk_1003099_BLMMPCB_MILESTONEINVCLN");
			CPCommon.AssertEqual(true,BLMMPCB_LineDetails_CurrencyLineInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_LineDetails_CurrencyLineInfoLink);
BLMMPCB_LineDetails_CurrencyLineInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on LineDetails_CurrencyLineInfoForm...", Logger.MessageType.INF);
			Control BLMMPCB_LineDetails_CurrencyLineInfoForm = new Control("LineDetails_CurrencyLineInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_CURRLINEINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMPCB_LineDetails_CurrencyLineInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on LineDetails_CurrencyLineInfo_ExchangeRates_BillingToFunctional...", Logger.MessageType.INF);
			Control BLMMPCB_LineDetails_CurrencyLineInfo_ExchangeRates_BillingToFunctional = new Control("LineDetails_CurrencyLineInfo_ExchangeRates_BillingToFunctional", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_CURRLINEINFO_']/ancestor::form[1]/descendant::*[@id='DISPLAY_TRNTOFUNC_ER']");
			CPCommon.AssertEqual(true,BLMMPCB_LineDetails_CurrencyLineInfo_ExchangeRates_BillingToFunctional.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_LineDetails_CurrencyLineInfoForm);
formBttn = BLMMPCB_LineDetails_CurrencyLineInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EXCHANGE RATES");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMMPCB_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1003383_BLMMPCB_MILESTONEINVCHDR");
			CPCommon.AssertEqual(true,BLMMPCB_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_ExchangeRatesLink);
BLMMPCB_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMMPCB_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMPCB_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control BLMMPCB_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMMPCB_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_ExchangeRatesForm);
formBttn = BLMMPCB_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("STANDARD TEXT");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on StandardTextLink...", Logger.MessageType.INF);
			Control BLMMPCB_StandardTextLink = new Control("StandardTextLink", "ID", "lnk_1003061_BLMMPCB_MILESTONEINVCHDR");
			CPCommon.AssertEqual(true,BLMMPCB_StandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_StandardTextLink);
BLMMPCB_StandardTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on StandardTextForm...", Logger.MessageType.INF);
			Control BLMMPCB_StandardTextForm = new Control("StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_MILESTONEINVCTXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMPCB_StandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control BLMMPCB_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_MILESTONEINVCTXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMPCB_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on StandardText_Ok...", Logger.MessageType.INF);
			Control BLMMPCB_StandardText_Ok = new Control("StandardText_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMPCB_MILESTONEINVCTXT_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLMMPCB_StandardText_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_StandardTextForm);
formBttn = BLMMPCB_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CUSTOMS INFO");


												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on CustomsInfoLink...", Logger.MessageType.INF);
			Control BLMMPCB_CustomsInfoLink = new Control("CustomsInfoLink", "ID", "lnk_1003933_BLMMPCB_MILESTONEINVCHDR");
			CPCommon.AssertEqual(true,BLMMPCB_CustomsInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_CustomsInfoLink);
BLMMPCB_CustomsInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on CustomsInfoForm...", Logger.MessageType.INF);
			Control BLMMPCB_CustomsInfoForm = new Control("CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMPCB_CustomsInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMPCB] Perfoming VerifyExists on CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control BLMMPCB_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,BLMMPCB_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_CustomsInfoForm);
formBttn = BLMMPCB_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainForm);
formBttn = BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? BLMMPCB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "BLMMPCB";
							CPCommon.WaitControlDisplayed(BLMMPCB_MainForm);
formBttn = BLMMPCB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

