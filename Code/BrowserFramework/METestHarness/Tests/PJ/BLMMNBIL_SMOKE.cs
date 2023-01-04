 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMMNBIL_SMOKE : TestScript
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
new Control("Manual Bills Processing", "xpath","//div[@class='navItem'][.='Manual Bills Processing']").Click();
new Control("Manage Manual Bills", "xpath","//div[@class='navItem'][.='Manage Manual Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMMNBIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMMNBIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on PeriodInformation_FiscalYear...", Logger.MessageType.INF);
			Control BLMMNBIL_PeriodInformation_FiscalYear = new Control("PeriodInformation_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,BLMMNBIL_PeriodInformation_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_MainForm);
IWebElement formBttn = BLMMNBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMMNBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMMNBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMMNBIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMNBIL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExist on BillingInformationFormTable...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformationFormTable = new Control("BillingInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMNBIL_MANUALBILLEDIT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming ClickButton on BillingInformationForm...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformationForm = new Control("BillingInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMNBIL_MANUALBILLEDIT_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformationForm);
formBttn = BLMMNBIL_BillingInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMMNBIL_BillingInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMMNBIL_BillingInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.AssertEqual(true,BLMMNBIL_BillingInformationForm.Exists());

													
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_InvoiceNumber...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_InvoiceNumber = new Control("BillingInformation_InvoiceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMMNBIL_MANUALBILLEDIT_CHLD_']/ancestor::form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_InvoiceNumber.Exists());

											Driver.SessionLogger.WriteLine("EXCHANGE RATES");


												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_ExchangeRatesLink = new Control("BillingInformation_ExchangeRatesLink", "ID", "lnk_1003407_BLMMNBIL_MANUALBILLEDIT_CHLD");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformation_ExchangeRatesLink);
BLMMNBIL_BillingInformation_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_ExchangeRatesForm = new Control("BillingInformation_ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_ExchangeRates_TransactionCurrency = new Control("BillingInformation_ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformation_ExchangeRatesForm);
formBttn = BLMMNBIL_BillingInformation_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("INVOICE TOTALS");


												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_InvoiceTotalsLink...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_InvoiceTotalsLink = new Control("BillingInformation_InvoiceTotalsLink", "ID", "lnk_1002865_BLMMNBIL_MANUALBILLEDIT_CHLD");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_InvoiceTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformation_InvoiceTotalsLink);
BLMMNBIL_BillingInformation_InvoiceTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_InvoiceTotalsForm...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_InvoiceTotalsForm = new Control("BillingInformation_InvoiceTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPBLINVT_INVCTOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_InvoiceTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_InvoiceTotals_InvoiceTotals_ExchangeRates_BillingToFunctional...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_InvoiceTotals_InvoiceTotals_ExchangeRates_BillingToFunctional = new Control("BillingInformation_InvoiceTotals_InvoiceTotals_ExchangeRates_BillingToFunctional", "xpath", "//div[translate(@id,'0123456789','')='pr__CPBLINVT_INVCTOTALS_']/ancestor::form[1]/descendant::*[@id='TRANS_TO_FUNC_ER']");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_InvoiceTotals_InvoiceTotals_ExchangeRates_BillingToFunctional.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformation_InvoiceTotalsForm);
formBttn = BLMMNBIL_BillingInformation_InvoiceTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CUSTOMS INFO");


												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_CustomsInfoLink...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_CustomsInfoLink = new Control("BillingInformation_CustomsInfoLink", "ID", "lnk_1002866_BLMMNBIL_MANUALBILLEDIT_CHLD");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_CustomsInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformation_CustomsInfoLink);
BLMMNBIL_BillingInformation_CustomsInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_CustomsInfoForm...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_CustomsInfoForm = new Control("BillingInformation_CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_CustomsInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMNBIL] Perfoming VerifyExists on BillingInformation_CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control BLMMNBIL_BillingInformation_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("BillingInformation_CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,BLMMNBIL_BillingInformation_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_BillingInformation_CustomsInfoForm);
formBttn = BLMMNBIL_BillingInformation_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_MainForm);
formBttn = BLMMNBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? BLMMNBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
BLMMNBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "BLMMNBIL";
							CPCommon.WaitControlDisplayed(BLMMNBIL_MainForm);
formBttn = BLMMNBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

