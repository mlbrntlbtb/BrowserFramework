 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMGBILL_SMOKE : TestScript
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
new Control("Standard Bills Processing", "xpath","//div[@class='navItem'][.='Standard Bills Processing']").Click();
new Control("Manage Standard Bills", "xpath","//div[@class='navItem'][.='Manage Standard Bills']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMGBILL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMGBILL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMGBILL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMGBILL_MainForm);
IWebElement formBttn = BLMGBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMGBILL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMGBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.AssertEqual(true,BLMGBILL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMGBILL_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMGBILL_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BLMGBILL_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMGBILL_MainTab);
IWebElement mTab = BLMGBILL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Standard Billing Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on StandardBillingInfo_InvoiceInfo_InvoiceNumber...", Logger.MessageType.INF);
			Control BLMGBILL_StandardBillingInfo_InvoiceInfo_InvoiceNumber = new Control("StandardBillingInfo_InvoiceInfo_InvoiceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_ID_DISP']");
			CPCommon.AssertEqual(true,BLMGBILL_StandardBillingInfo_InvoiceInfo_InvoiceNumber.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_MainTab);
mTab = BLMGBILL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on Header_BillDetails_UsedFor...", Logger.MessageType.INF);
			Control BLMGBILL_Header_BillDetails_UsedFor = new Control("Header_BillDetails_UsedFor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='USED_FOR']");
			CPCommon.AssertEqual(true,BLMGBILL_Header_BillDetails_UsedFor.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_MainTab);
mTab = BLMGBILL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Totals").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on Totals_OtherCharges_Code1...", Logger.MessageType.INF);
			Control BLMGBILL_Totals_OtherCharges_Code1 = new Control("Totals_OtherCharges_Code1", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OTH_CHG_CD1']");
			CPCommon.AssertEqual(true,BLMGBILL_Totals_OtherCharges_Code1.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_MainTab);
mTab = BLMGBILL_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Addresses").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on Addresses_BillTo_Address...", Logger.MessageType.INF);
			Control BLMGBILL_Addresses_BillTo_Address = new Control("Addresses_BillTo_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ADDR_DC']");
			CPCommon.AssertEqual(true,BLMGBILL_Addresses_BillTo_Address.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMGBILL_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1007102_BLMGBILL_BILLEDITINVCHDR");
			CPCommon.AssertEqual(true,BLMGBILL_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_ExchangeRatesLink);
BLMGBILL_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMGBILL_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_BLREXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMGBILL_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on ExchangeRates_BillingCurrency...", Logger.MessageType.INF);
			Control BLMGBILL_ExchangeRates_BillingCurrency = new Control("ExchangeRates_BillingCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_BLREXR_']/ancestor::form[1]/descendant::*[@id='PAY_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMGBILL_ExchangeRates_BillingCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_ExchangeRatesForm);
formBttn = BLMGBILL_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control BLMGBILL_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMGBILL_BILLEDITDETL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMGBILL_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control BLMGBILL_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMGBILL_BILLEDITDETL_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMGBILL_ChildForm.Exists());

											Driver.SessionLogger.WriteLine("Detail");


												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on ChildForm_DetailLink...", Logger.MessageType.INF);
			Control BLMGBILL_ChildForm_DetailLink = new Control("ChildForm_DetailLink", "ID", "lnk_1002836_BLMGBILL_BILLEDITDETL_CHILD");
			CPCommon.AssertEqual(true,BLMGBILL_ChildForm_DetailLink.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_ChildForm_DetailLink);
BLMGBILL_ChildForm_DetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExist on DetailFormTable...", Logger.MessageType.INF);
			Control BLMGBILL_DetailFormTable = new Control("DetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMGBILL_BILLEDITDETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMGBILL_DetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on Detail_Recalculate...", Logger.MessageType.INF);
			Control BLMGBILL_Detail_Recalculate = new Control("Detail_Recalculate", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMGBILL_BILLEDITDETL_']/ancestor::form[1]/descendant::*[contains(@id,'ACTION_BUTTON') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,BLMGBILL_Detail_Recalculate.Exists());

											Driver.SessionLogger.WriteLine("Customs Info");


												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on Detail_CustomsInfoLink...", Logger.MessageType.INF);
			Control BLMGBILL_Detail_CustomsInfoLink = new Control("Detail_CustomsInfoLink", "ID", "lnk_1003931_BLMGBILL_BILLEDITDETL");
			CPCommon.AssertEqual(true,BLMGBILL_Detail_CustomsInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_Detail_CustomsInfoLink);
BLMGBILL_Detail_CustomsInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on CustomsInfoForm...", Logger.MessageType.INF);
			Control BLMGBILL_CustomsInfoForm = new Control("CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMGBILL_CustomsInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMGBILL] Perfoming VerifyExists on CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control BLMGBILL_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,BLMGBILL_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_CustomsInfoForm);
formBttn = BLMGBILL_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLMGBILL";
							CPCommon.WaitControlDisplayed(BLMGBILL_MainForm);
formBttn = BLMGBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

