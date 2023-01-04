 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMCLOSE_SMOKE : TestScript
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
new Control("Billing History", "xpath","//div[@class='navItem'][.='Billing History']").Click();
new Control("Manage Closed Billing Detail", "xpath","//div[@class='navItem'][.='Manage Closed Billing Detail']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMCLOSE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMCLOSE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on Identification_InvoiceProject...", Logger.MessageType.INF);
			Control BLMCLOSE_Identification_InvoiceProject = new Control("Identification_InvoiceProject", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_PROJ_ID']");
			CPCommon.AssertEqual(true,BLMCLOSE_Identification_InvoiceProject.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BLMCLOSE_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMCLOSE_MainTab);
IWebElement mTab = BLMCLOSE_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on BillingDetail_TransactionType...", Logger.MessageType.INF);
			Control BLMCLOSE_BillingDetail_TransactionType = new Control("BillingDetail_TransactionType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_TRN_TYPE']");
			CPCommon.AssertEqual(true,BLMCLOSE_BillingDetail_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_MainTab);
mTab = BLMCLOSE_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Labor").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on Labor_IDType...", Logger.MessageType.INF);
			Control BLMCLOSE_Labor_IDType = new Control("Labor_IDType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_ID_TYPE']");
			CPCommon.AssertEqual(true,BLMCLOSE_Labor_IDType.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_MainTab);
mTab = BLMCLOSE_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Units").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on Units_CLIN...", Logger.MessageType.INF);
			Control BLMCLOSE_Units_CLIN = new Control("Units_CLIN", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CLIN_ID']");
			CPCommon.AssertEqual(true,BLMCLOSE_Units_CLIN.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_MainTab);
mTab = BLMCLOSE_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Sales Tax").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on SalesTax_SalesTax...", Logger.MessageType.INF);
			Control BLMCLOSE_SalesTax_SalesTax = new Control("SalesTax_SalesTax", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SALES_TAX_CD']");
			CPCommon.AssertEqual(true,BLMCLOSE_SalesTax_SalesTax.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_MainTab);
mTab = BLMCLOSE_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on Other_NumberEntry_None...", Logger.MessageType.INF);
			Control BLMCLOSE_Other_NumberEntry_None = new Control("Other_NumberEntry_None", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFRADIO' and @value='0']");
			CPCommon.AssertEqual(true,BLMCLOSE_Other_NumberEntry_None.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_MainForm);
IWebElement formBttn = BLMCLOSE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMCLOSE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMCLOSE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMCLOSE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMCLOSE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("EXCHANGERATES");


												
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMCLOSE_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_16816_BLMCLOSE_BILLINGDETLHIST_HDR");
			CPCommon.AssertEqual(true,BLMCLOSE_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_ExchangeRatesLink);
BLMCLOSE_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMCLOSE_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMCLOSE_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMCLOSE] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control BLMCLOSE_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMCLOSE_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_ExchangeRatesForm);
formBttn = BLMCLOSE_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMCLOSE";
							CPCommon.WaitControlDisplayed(BLMCLOSE_MainForm);
formBttn = BLMCLOSE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

