 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMVEND_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Vendors", "xpath","//div[@class='navItem'][.='Vendors']").Click();
new Control("Manage Vendors", "xpath","//div[@class='navItem'][.='Manage Vendors']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMVEND_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMVEND_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorID...", Logger.MessageType.INF);
			Control APMVEND_VendorID = new Control("VendorID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,APMVEND_VendorID.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control APMVEND_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(APMVEND_MainFormTab);
IWebElement mTab = APMVEND_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Header").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Header_LongName...", Logger.MessageType.INF);
			Control APMVEND_Header_LongName = new Control("Header_LongName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_LONG_NAME']");
			CPCommon.AssertEqual(true,APMVEND_Header_LongName.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_MainFormTab);
mTab = APMVEND_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Defaults").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Defaults_PayVendorName...", Logger.MessageType.INF);
			Control APMVEND_Defaults_PayVendorName = new Control("Defaults_PayVendorName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_NAME1']");
			CPCommon.AssertEqual(true,APMVEND_Defaults_PayVendorName.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_MainFormTab);
mTab = APMVEND_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Notes_Text...", Logger.MessageType.INF);
			Control APMVEND_Notes_Text = new Control("Notes_Text", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_NOTES']");
			CPCommon.AssertEqual(true,APMVEND_Notes_Text.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_MainForm);
IWebElement formBttn = APMVEND_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMVEND_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMVEND_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMVEND_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming Close on AddressesForm...", Logger.MessageType.INF);
			Control APMVEND_AddressesForm = new Control("AddressesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDADDR_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMVEND_AddressesForm);
formBttn = APMVEND_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("VENDOR EMPLOYEES");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorEMPLOYEESLink...", Logger.MessageType.INF);
			Control APMVEND_VendorEMPLOYEESLink = new Control("VendorEMPLOYEESLink", "ID", "lnk_1005727_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_VendorEMPLOYEESLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VendorEMPLOYEESLink);
APMVEND_VendorEMPLOYEESLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorEmployeeDetailsForm...", Logger.MessageType.INF);
			Control APMVEND_VendorEmployeeDetailsForm = new Control("VendorEmployeeDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVENDE_VENDEMPL_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_VendorEmployeeDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorEmployeeDetails_VendorEMPLOYEEID...", Logger.MessageType.INF);
			Control APMVEND_VendorEmployeeDetails_VendorEMPLOYEEID = new Control("VendorEmployeeDetails_VendorEMPLOYEEID", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVENDE_VENDEMPL_HDR_']/ancestor::form[1]/descendant::*[@id='VEND_EMPL_ID']");
			CPCommon.AssertEqual(true,APMVEND_VendorEmployeeDetails_VendorEMPLOYEEID.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VendorEmployeeDetailsForm);
formBttn = APMVEND_VendorEmployeeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMVEND_VendorEmployeeDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMVEND_VendorEmployeeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on VendorEmployeeDetailsFormTable...", Logger.MessageType.INF);
			Control APMVEND_VendorEmployeeDetailsFormTable = new Control("VendorEmployeeDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVENDE_VENDEMPL_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_VendorEmployeeDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VendorEmployeeDetailsForm);
formBttn = APMVEND_VendorEmployeeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("VENDOR CLASSIFICATION");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorClassificationLink...", Logger.MessageType.INF);
			Control APMVEND_VendorClassificationLink = new Control("VendorClassificationLink", "ID", "lnk_1005132_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_VendorClassificationLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VendorClassificationLink);
APMVEND_VendorClassificationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorClassificationForm...", Logger.MessageType.INF);
			Control APMVEND_VendorClassificationForm = new Control("VendorClassificationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VEND_CLASSIF_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_VendorClassificationForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorClassification_DefaultSize_Large...", Logger.MessageType.INF);
			Control APMVEND_VendorClassification_DefaultSize_Large = new Control("VendorClassification_DefaultSize_Large", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VEND_CLASSIF_']/ancestor::form[1]/descendant::*[@id='S_CL_SM_BUS_CD' and @value='L']");
			CPCommon.AssertEqual(true,APMVEND_VendorClassification_DefaultSize_Large.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VendorClassificationChildForm...", Logger.MessageType.INF);
			Control APMVEND_VendorClassificationChildForm = new Control("VendorClassificationChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDINDCLASS_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_VendorClassificationChildForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on VendorClassificationChildFormTable...", Logger.MessageType.INF);
			Control APMVEND_VendorClassificationChildFormTable = new Control("VendorClassificationChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDINDCLASS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_VendorClassificationChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VendorClassificationForm);
formBttn = APMVEND_VendorClassificationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CURRENCIES");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CurrenciesLink...", Logger.MessageType.INF);
			Control APMVEND_CurrenciesLink = new Control("CurrenciesLink", "ID", "lnk_1005729_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_CurrenciesLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_CurrenciesLink);
APMVEND_CurrenciesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CurrenciesForm...", Logger.MessageType.INF);
			Control APMVEND_CurrenciesForm = new Control("CurrenciesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VEND_CURRENCY_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_CurrenciesForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Currencies_RateGroup...", Logger.MessageType.INF);
			Control APMVEND_Currencies_RateGroup = new Control("Currencies_RateGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VEND_CURRENCY_']/ancestor::form[1]/descendant::*[@id='DFLT_RT_GRP_ID']");
			CPCommon.AssertEqual(true,APMVEND_Currencies_RateGroup.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Currencies_TransactionCurrenciesForm...", Logger.MessageType.INF);
			Control APMVEND_Currencies_TransactionCurrenciesForm = new Control("Currencies_TransactionCurrenciesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDLIMITCRNCY_TRANS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_Currencies_TransactionCurrenciesForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on Currencies_TransactionCurrenciesFormTable...", Logger.MessageType.INF);
			Control APMVEND_Currencies_TransactionCurrenciesFormTable = new Control("Currencies_TransactionCurrenciesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDLIMITCRNCY_TRANS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_Currencies_TransactionCurrenciesFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Currencies_PayCurrenciesForm...", Logger.MessageType.INF);
			Control APMVEND_Currencies_PayCurrenciesForm = new Control("Currencies_PayCurrenciesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDLIMITCRNCY_PAY_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_Currencies_PayCurrenciesForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on Currencies_PayCurrenciesFormTable...", Logger.MessageType.INF);
			Control APMVEND_Currencies_PayCurrenciesFormTable = new Control("Currencies_PayCurrenciesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDLIMITCRNCY_PAY_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_Currencies_PayCurrenciesFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_CurrenciesForm);
formBttn = APMVEND_CurrenciesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("SUBCONTRACTOR INFO");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on SubcontractorInfoLink...", Logger.MessageType.INF);
			Control APMVEND_SubcontractorInfoLink = new Control("SubcontractorInfoLink", "ID", "lnk_1005150_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_SubcontractorInfoLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_SubcontractorInfoLink);
APMVEND_SubcontractorInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on SubContractorInfoForm...", Logger.MessageType.INF);
			Control APMVEND_SubContractorInfoForm = new Control("SubContractorInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VEND_SUBPAY_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_SubContractorInfoForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on SubcontractorInfo_PaymentControl...", Logger.MessageType.INF);
			Control APMVEND_SubcontractorInfo_PaymentControl = new Control("SubcontractorInfo_PaymentControl", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VEND_SUBPAY_']/ancestor::form[1]/descendant::*[@id='SUBCTR_FL']");
			CPCommon.AssertEqual(true,APMVEND_SubcontractorInfo_PaymentControl.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_SubContractorInfoForm);
formBttn = APMVEND_SubContractorInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CREDIT CARD INFO");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CreditCardInfoLink...", Logger.MessageType.INF);
			Control APMVEND_CreditCardInfoLink = new Control("CreditCardInfoLink", "ID", "lnk_1005599_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_CreditCardInfoLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_CreditCardInfoLink);
APMVEND_CreditCardInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on CreditCardInfoFormTable...", Logger.MessageType.INF);
			Control APMVEND_CreditCardInfoFormTable = new Control("CreditCardInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVCCI_CRCARDVENDINFO_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_CreditCardInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming ClickButton on CreditCardInfoForm...", Logger.MessageType.INF);
			Control APMVEND_CreditCardInfoForm = new Control("CreditCardInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVCCI_CRCARDVENDINFO_HDR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMVEND_CreditCardInfoForm);
formBttn = APMVEND_CreditCardInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVEND_CreditCardInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVEND_CreditCardInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.AssertEqual(true,APMVEND_CreditCardInfoForm.Exists());

													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CreditCardInfo_CreditCardType...", Logger.MessageType.INF);
			Control APMVEND_CreditCardInfo_CreditCardType = new Control("CreditCardInfo_CreditCardType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVCCI_CRCARDVENDINFO_HDR_']/ancestor::form[1]/descendant::*[@id='CR_CARD_TYPE']");
			CPCommon.AssertEqual(true,APMVEND_CreditCardInfo_CreditCardType.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_CreditCardInfoForm);
formBttn = APMVEND_CreditCardInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ADDRESSES");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on AddressesLink...", Logger.MessageType.INF);
			Control APMVEND_AddressesLink = new Control("AddressesLink", "ID", "lnk_1005127_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_AddressesLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_AddressesLink);
APMVEND_AddressesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on AddressesFormTable...", Logger.MessageType.INF);
			Control APMVEND_AddressesFormTable = new Control("AddressesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDADDR_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_AddressesFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_AddressesForm);
formBttn = APMVEND_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVEND_AddressesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVEND_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.AssertEqual(true,APMVEND_AddressesForm.Exists());

													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on Addresses_AddressCode...", Logger.MessageType.INF);
			Control APMVEND_Addresses_AddressCode = new Control("Addresses_AddressCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDADDR_CHLD_']/ancestor::form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,APMVEND_Addresses_AddressCode.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_AddressesForm);
formBttn = APMVEND_AddressesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DEFAULT EXPENSE ACCOUNTS");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on DefaultExpenseAccountsLink...", Logger.MessageType.INF);
			Control APMVEND_DefaultExpenseAccountsLink = new Control("DefaultExpenseAccountsLink", "ID", "lnk_1005129_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_DefaultExpenseAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_DefaultExpenseAccountsLink);
APMVEND_DefaultExpenseAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on DefaultExpenseAccountsFormTable...", Logger.MessageType.INF);
			Control APMVEND_DefaultExpenseAccountsFormTable = new Control("DefaultExpenseAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDEXPACCT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_DefaultExpenseAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming ClickButton on DefaultExpenseAccountsForm...", Logger.MessageType.INF);
			Control APMVEND_DefaultExpenseAccountsForm = new Control("DefaultExpenseAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDEXPACCT_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMVEND_DefaultExpenseAccountsForm);
formBttn = APMVEND_DefaultExpenseAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVEND_DefaultExpenseAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVEND_DefaultExpenseAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.AssertEqual(true,APMVEND_DefaultExpenseAccountsForm.Exists());

													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on DefaultExpenseAccounts_Line...", Logger.MessageType.INF);
			Control APMVEND_DefaultExpenseAccounts_Line = new Control("DefaultExpenseAccounts_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDEXPACCT_CHLD_']/ancestor::form[1]/descendant::*[@id='LN_NO']");
			CPCommon.AssertEqual(true,APMVEND_DefaultExpenseAccounts_Line.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_DefaultExpenseAccountsForm);
formBttn = APMVEND_DefaultExpenseAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("VAT INFO");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VATInfoLink...", Logger.MessageType.INF);
			Control APMVEND_VATInfoLink = new Control("VATInfoLink", "ID", "lnk_1005130_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_VATInfoLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VATInfoLink);
APMVEND_VATInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on VATInfoForm...", Logger.MessageType.INF);
			Control APMVEND_VATInfoForm = new Control("VATInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDVATINFO_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_VATInfoForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on VATInfoFormTable...", Logger.MessageType.INF);
			Control APMVEND_VATInfoFormTable = new Control("VATInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDVATINFO_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_VATInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_VATInfoForm);
formBttn = APMVEND_VATInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CIS INFO");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CISInfoLink...", Logger.MessageType.INF);
			Control APMVEND_CISInfoLink = new Control("CISInfoLink", "ID", "lnk_1005131_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_CISInfoLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_CISInfoLink);
APMVEND_CISInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CISInfoForm...", Logger.MessageType.INF);
			Control APMVEND_CISInfoForm = new Control("CISInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDCISINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMVEND_CISInfoForm.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on CISInfo_DetailData_CISCode...", Logger.MessageType.INF);
			Control APMVEND_CISInfo_DetailData_CISCode = new Control("CISInfo_DetailData_CISCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMVEND_VENDCISINFO_']/ancestor::form[1]/descendant::*[@id='CIS_CD']");
			CPCommon.AssertEqual(true,APMVEND_CISInfo_DetailData_CISCode.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_CISInfoForm);
formBttn = APMVEND_CISInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("USER-DEFINED INFO");


												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on UserDefinedInfoLink...", Logger.MessageType.INF);
			Control APMVEND_UserDefinedInfoLink = new Control("UserDefinedInfoLink", "ID", "lnk_1005157_CPMVEND_VEND");
			CPCommon.AssertEqual(true,APMVEND_UserDefinedInfoLink.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_UserDefinedInfoLink);
APMVEND_UserDefinedInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExist on UserDefinedInfoFormTable...", Logger.MessageType.INF);
			Control APMVEND_UserDefinedInfoFormTable = new Control("UserDefinedInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVEND_UserDefinedInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control APMVEND_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMVEND_UserDefinedInfoForm);
formBttn = APMVEND_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVEND_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVEND_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.AssertEqual(true,APMVEND_UserDefinedInfoForm.Exists());

													
				CPCommon.CurrentComponent = "APMVEND";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVEND] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control APMVEND_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,APMVEND_UserDefinedInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_UserDefinedInfoForm);
formBttn = APMVEND_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APMVEND";
							CPCommon.WaitControlDisplayed(APMVEND_MainForm);
formBttn = APMVEND_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

