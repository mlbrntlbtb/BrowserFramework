 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests.AC
{
    public class ARMCUST_SMOKE : TestScript
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
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Customers", "xpath","//div[@class='navItem'][.='Customers']").Click();
new Control("Manage Customers", "xpath","//div[@class='navItem'][.='Manage Customers']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control ARMCUST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_MainForm);
IWebElement formBttn = ARMCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? ARMCUST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
ARMCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ARMCUST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_MainForm);
formBttn = ARMCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCUST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Customer_CustomerAccount...", Logger.MessageType.INF);
			Control ARMCUST_Customer_CustomerAccount = new Control("Customer_CustomerAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,ARMCUST_Customer_CustomerAccount.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control ARMCUST_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(ARMCUST_MainFormTab);
IWebElement mTab = ARMCUST_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Customer_CustomerDetails_Vendor...", Logger.MessageType.INF);
			Control ARMCUST_Customer_CustomerDetails_Vendor = new Control("Customer_CustomerDetails_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_ID']");
			CPCommon.AssertEqual(true,ARMCUST_Customer_CustomerDetails_Vendor.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_MainFormTab);
mTab = ARMCUST_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Credit Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Customer_CreditInfo_CreditInformation_ApplyFinanceCharges...", Logger.MessageType.INF);
			Control ARMCUST_Customer_CreditInfo_CreditInformation_ApplyFinanceCharges = new Control("Customer_CreditInfo_CreditInformation_ApplyFinanceCharges", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='APPLY_FIN_CHG_FL']");
			CPCommon.AssertEqual(true,ARMCUST_Customer_CreditInfo_CreditInformation_ApplyFinanceCharges.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_MainFormTab);
mTab = ARMCUST_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Sales Order").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Customer_SalesOrder_Pricing_Project...", Logger.MessageType.INF);
			Control ARMCUST_Customer_SalesOrder_Pricing_Project = new Control("Customer_SalesOrder_Pricing_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PRICE_PROJ_ID']");
			CPCommon.AssertEqual(true,ARMCUST_Customer_SalesOrder_Pricing_Project.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_MainFormTab);
mTab = ARMCUST_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "SO Address/Contacts").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Customer_SOAddressContacts_SalesContactInformation_LastName...", Logger.MessageType.INF);
			Control ARMCUST_Customer_SOAddressContacts_SalesContactInformation_LastName = new Control("Customer_SOAddressContacts_SalesContactInformation_LastName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SLS_CNT_LAST_NAME']");
			CPCommon.AssertEqual(true,ARMCUST_Customer_SOAddressContacts_SalesContactInformation_LastName.Exists());

											Driver.SessionLogger.WriteLine("Default Accts");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on Customer_DefaultAcctsLink...", Logger.MessageType.INF);
			Control ARMCUST_Customer_DefaultAcctsLink = new Control("Customer_DefaultAcctsLink", "ID", "lnk_1002896_CPMCUST_CUST_HDR");
			CPCommon.WaitControlDisplayed(ARMCUST_Customer_DefaultAcctsLink);
ARMCUST_Customer_DefaultAcctsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on DefaultAcctsTable...", Logger.MessageType.INF);
			Control ARMCUST_DefaultAcctsTable = new Control("DefaultAcctsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTDFLTACCT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_DefaultAcctsTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming ClickButton on DefaultAcctsForm...", Logger.MessageType.INF);
			Control ARMCUST_DefaultAcctsForm = new Control("DefaultAcctsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTDFLTACCT_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_DefaultAcctsForm);
formBttn = ARMCUST_DefaultAcctsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCUST_DefaultAcctsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCUST_DefaultAcctsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on DefaultAccts_TransactionType...", Logger.MessageType.INF);
			Control ARMCUST_DefaultAccts_TransactionType = new Control("DefaultAccts_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTDFLTACCT_CTW_']/ancestor::form[1]/descendant::*[@id='CUSTTRNTYPE_DESC']");
			CPCommon.AssertEqual(true,ARMCUST_DefaultAccts_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_DefaultAcctsForm);
formBttn = ARMCUST_DefaultAcctsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Notes");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on Customer_NotesLink...", Logger.MessageType.INF);
			Control ARMCUST_Customer_NotesLink = new Control("Customer_NotesLink", "ID", "lnk_1002897_CPMCUST_CUST_HDR");
			CPCommon.WaitControlDisplayed(ARMCUST_Customer_NotesLink);
ARMCUST_Customer_NotesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Notes_Notes...", Logger.MessageType.INF);
			Control ARMCUST_Notes_Notes = new Control("Notes_Notes", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTNOTES_CTW_']/ancestor::form[1]/descendant::*[@id='NOTES_TX']");
			CPCommon.AssertEqual(true,ARMCUST_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Close on NotesForm...", Logger.MessageType.INF);
			Control ARMCUST_NotesForm = new Control("NotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTNOTES_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_NotesForm);
formBttn = ARMCUST_NotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Multicurrency");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on Customer_MulticurrencyLink...", Logger.MessageType.INF);
			Control ARMCUST_Customer_MulticurrencyLink = new Control("Customer_MulticurrencyLink", "ID", "lnk_1002898_CPMCUST_CUST_HDR");
			CPCommon.WaitControlDisplayed(ARMCUST_Customer_MulticurrencyLink);
ARMCUST_Customer_MulticurrencyLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Multicurrency_RateGroup...", Logger.MessageType.INF);
			Control ARMCUST_Multicurrency_RateGroup = new Control("Multicurrency_RateGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_DEFCURR_CTW_']/ancestor::form[1]/descendant::*[@id='DFLT_RT_GRP_ID']");
			CPCommon.AssertEqual(true,ARMCUST_Multicurrency_RateGroup.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on Multicurrency_TransactionCurrenciesTable...", Logger.MessageType.INF);
			Control ARMCUST_Multicurrency_TransactionCurrenciesTable = new Control("Multicurrency_TransactionCurrenciesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_TRANSCURR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_Multicurrency_TransactionCurrenciesTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on Multicurrency_PayCurrenciesTable...", Logger.MessageType.INF);
			Control ARMCUST_Multicurrency_PayCurrenciesTable = new Control("Multicurrency_PayCurrenciesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_PAYCURR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_Multicurrency_PayCurrenciesTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Close on MulticurrencyForm...", Logger.MessageType.INF);
			Control ARMCUST_MulticurrencyForm = new Control("MulticurrencyForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_DEFCURR_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_MulticurrencyForm);
formBttn = ARMCUST_MulticurrencyForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("VAT Info");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on Customer_VATInfoLink...", Logger.MessageType.INF);
			Control ARMCUST_Customer_VATInfoLink = new Control("Customer_VATInfoLink", "ID", "lnk_1002902_CPMCUST_CUST_HDR");
			CPCommon.WaitControlDisplayed(ARMCUST_Customer_VATInfoLink);
ARMCUST_Customer_VATInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on VATInfoTable...", Logger.MessageType.INF);
			Control ARMCUST_VATInfoTable = new Control("VATInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTVATINFO_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_VATInfoTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Close on VATInfoForm...", Logger.MessageType.INF);
			Control ARMCUST_VATInfoForm = new Control("VATInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTVATINFO_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_VATInfoForm);
formBttn = ARMCUST_VATInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("User Defined");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on Customer_UserDefinedInfoLink...", Logger.MessageType.INF);
			Control ARMCUST_Customer_UserDefinedInfoLink = new Control("Customer_UserDefinedInfoLink", "ID", "lnk_1007191_CPMCUST_CUST_HDR");
			CPCommon.WaitControlDisplayed(ARMCUST_Customer_UserDefinedInfoLink);
ARMCUST_Customer_UserDefinedInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on UserDefinedInfoTable...", Logger.MessageType.INF);
			Control ARMCUST_UserDefinedInfoTable = new Control("UserDefinedInfoTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_UserDefinedInfoTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming ClickButton on UserDefinedInfoForm...", Logger.MessageType.INF);
			Control ARMCUST_UserDefinedInfoForm = new Control("UserDefinedInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_UserDefinedInfoForm);
formBttn = ARMCUST_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCUST_UserDefinedInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCUST_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on UserDefinedInfo_DataType...", Logger.MessageType.INF);
			Control ARMCUST_UserDefinedInfo_DataType = new Control("UserDefinedInfo_DataType", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDINF_UDEFLBL_CHLD_']/ancestor::form[1]/descendant::*[@id='S_DATA_TYPE']");
			CPCommon.AssertEqual(true,ARMCUST_UserDefinedInfo_DataType.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_UserDefinedInfoForm);
formBttn = ARMCUST_UserDefinedInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Customer Alias");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on Customer_CustomerAliasLink...", Logger.MessageType.INF);
			Control ARMCUST_Customer_CustomerAliasLink = new Control("Customer_CustomerAliasLink", "ID", "lnk_3565_CPMCUST_CUST_HDR");
			CPCommon.WaitControlDisplayed(ARMCUST_Customer_CustomerAliasLink);
ARMCUST_Customer_CustomerAliasLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on CustomerAliasTable...", Logger.MessageType.INF);
			Control ARMCUST_CustomerAliasTable = new Control("CustomerAliasTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTALIAS_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_CustomerAliasTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Close on CustomerAliasForm...", Logger.MessageType.INF);
			Control ARMCUST_CustomerAliasForm = new Control("CustomerAliasForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTALIAS_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_CustomerAliasForm);
formBttn = ARMCUST_CustomerAliasForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Customer Addrress");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on CustomerAddressTable...", Logger.MessageType.INF);
			Control ARMCUST_CustomerAddressTable = new Control("CustomerAddressTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTADDR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_CustomerAddressTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming ClickButton on CustomerAddressForm...", Logger.MessageType.INF);
			Control ARMCUST_CustomerAddressForm = new Control("CustomerAddressForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTADDR_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_CustomerAddressForm);
formBttn = ARMCUST_CustomerAddressForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCUST_CustomerAddressForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCUST_CustomerAddressForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on CustomerAddress_AddressCode...", Logger.MessageType.INF);
			Control ARMCUST_CustomerAddress_AddressCode = new Control("CustomerAddress_AddressCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTADDR_CTW_']/ancestor::form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,ARMCUST_CustomerAddress_AddressCode.Exists());

											Driver.SessionLogger.WriteLine("Contacts");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming Click on CustomerAddress_ContactsLink...", Logger.MessageType.INF);
			Control ARMCUST_CustomerAddress_ContactsLink = new Control("CustomerAddress_ContactsLink", "ID", "lnk_1002906_CPMCUST_CUSTADDR_CTW");
			CPCommon.WaitControlDisplayed(ARMCUST_CustomerAddress_ContactsLink);
ARMCUST_CustomerAddress_ContactsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExist on ContactsTable...", Logger.MessageType.INF);
			Control ARMCUST_ContactsTable = new Control("ContactsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTADDRCNTACT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMCUST_ContactsTable.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming ClickButton on ContactsForm...", Logger.MessageType.INF);
			Control ARMCUST_ContactsForm = new Control("ContactsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTADDRCNTACT_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARMCUST_ContactsForm);
formBttn = ARMCUST_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMCUST_ContactsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMCUST_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMCUST] Perfoming VerifyExists on Contacts_ContactID...", Logger.MessageType.INF);
			Control ARMCUST_Contacts_ContactID = new Control("Contacts_ContactID", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCUST_CUSTADDRCNTACT_CTW_']/ancestor::form[1]/descendant::*[@id='CNTACT_ID']");
			CPCommon.AssertEqual(true,ARMCUST_Contacts_ContactID.Exists());

												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_ContactsForm);
formBttn = ARMCUST_ContactsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ARMCUST";
							CPCommon.WaitControlDisplayed(ARMCUST_MainForm);
formBttn = ARMCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

