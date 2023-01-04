 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMPJPRD_SMOKE : TestScript
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
new Control("Project Product Bills Processing", "xpath","//div[@class='navItem'][.='Project Product Bills Processing']").Click();
new Control("Manage Project Product Bills", "xpath","//div[@class='navItem'][.='Manage Project Product Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMPJPRD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control BLMPJPRD_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMPJPRD_Identification_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BLMPJPRD_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMPJPRD_MainTab);
IWebElement mTab = BLMPJPRD_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on BillingDetail_Customer...", Logger.MessageType.INF);
			Control BLMPJPRD_BillingDetail_Customer = new Control("BillingDetail_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BLMPJPRD_BillingDetail_Customer.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_MainTab);
mTab = BLMPJPRD_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Address Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on AddressInfo_BillTo_Address...", Logger.MessageType.INF);
			Control BLMPJPRD_AddressInfo_BillTo_Address = new Control("AddressInfo_BillTo_Address", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ADDR_DC']");
			CPCommon.AssertEqual(true,BLMPJPRD_AddressInfo_BillTo_Address.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_MainTab);
mTab = BLMPJPRD_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on OtherInfo_OtherCharges_Code...", Logger.MessageType.INF);
			Control BLMPJPRD_OtherInfo_OtherCharges_Code = new Control("OtherInfo_OtherCharges_Code", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OTH_CHG_CD1']");
			CPCommon.AssertEqual(true,BLMPJPRD_OtherInfo_OtherCharges_Code.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_MainTab);
mTab = BLMPJPRD_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on Notes_Notes...", Logger.MessageType.INF);
			Control BLMPJPRD_Notes_Notes = new Control("Notes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,BLMPJPRD_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_MainForm);
IWebElement formBttn = BLMPJPRD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMPJPRD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMPJPRD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMPJPRD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPJPRD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT PRODUCT BILLING LINES");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExist on ProjectProductBillingLinesFormTable...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLinesFormTable = new Control("ProjectProductBillingLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODINVCLN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming ClickButton on ProjectProductBillingLinesForm...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLinesForm = new Control("ProjectProductBillingLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODINVCLN_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLinesForm);
formBttn = BLMPJPRD_ProjectProductBillingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMPJPRD_ProjectProductBillingLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMPJPRD_ProjectProductBillingLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLinesForm.Exists());

													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_Line...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_Line = new Control("ProjectProductBillingLines_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODINVCLN_CHLD_']/ancestor::form[1]/descendant::*[@id='PROD_INVC_LN_NO']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_Line.Exists());

											Driver.SessionLogger.WriteLine("CUSTOMS INFO");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_CustomsInfoLink...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CustomsInfoLink = new Control("ProjectProductBillingLines_CustomsInfoLink", "ID", "lnk_1004115_BLMPJPRD_PROJPRODINVCLN_CHLD");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CustomsInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_CustomsInfoLink);
BLMPJPRD_ProjectProductBillingLines_CustomsInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_CustomsInfoForm...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm = new Control("ProjectProductBillingLines_CustomsInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_CustomsInfo_ValueAddedTaxInformation_TaxCode...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CustomsInfo_ValueAddedTaxInformation_TaxCode = new Control("ProjectProductBillingLines_CustomsInfo_ValueAddedTaxInformation_TaxCode", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='VAT_TAX_ID']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CustomsInfo_ValueAddedTaxInformation_TaxCode.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm);
formBttn = BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExist on ProjectProductBillingLines_CustomsInfoFormTable...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CustomsInfoFormTable = new Control("ProjectProductBillingLines_CustomsInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPVATSCR_CUSTOMSVATHDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CustomsInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm);
formBttn = BLMPJPRD_ProjectProductBillingLines_CustomsInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CURRENCY LINE INFO");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_CurrencyLineInfoLink...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoLink = new Control("ProjectProductBillingLines_CurrencyLineInfoLink", "ID", "lnk_1003410_BLMPJPRD_PROJPRODINVCLN_CHLD");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoLink);
BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_CurrencyLineInfoForm...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoForm = new Control("ProjectProductBillingLines_CurrencyLineInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_CURRLINEINFO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_CurrencyLineInfo_ExchangeRates_BillingToFunctional...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfo_ExchangeRates_BillingToFunctional = new Control("ProjectProductBillingLines_CurrencyLineInfo_ExchangeRates_BillingToFunctional", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_CURRLINEINFO_']/ancestor::form[1]/descendant::*[@id='TRAN_TO_FUNC_ER']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfo_ExchangeRates_BillingToFunctional.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoForm);
formBttn = BLMPJPRD_ProjectProductBillingLines_CurrencyLineInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ACRNs");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_ACRNsLink...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_ACRNsLink = new Control("ProjectProductBillingLines_ACRNsLink", "ID", "lnk_1007746_BLMPJPRD_PROJPRODINVCLN_CHLD");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_ACRNsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_ACRNsLink);
BLMPJPRD_ProjectProductBillingLines_ACRNsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_ACRNsForm...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_ACRNsForm = new Control("ProjectProductBillingLines_ACRNsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODACRN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_ACRNsForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_ACRNs_Ok...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_ACRNs_Ok = new Control("ProjectProductBillingLines_ACRNs_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODACRN_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_ACRNs_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExist on ProjectProductBillingLines_ACRNsFormTable...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_ACRNsFormTable = new Control("ProjectProductBillingLines_ACRNsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODACRN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_ACRNsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_ACRNsForm);
formBttn = BLMPJPRD_ProjectProductBillingLines_ACRNsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MILSTRIPs");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_MILSTRIPsLink...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_MILSTRIPsLink = new Control("ProjectProductBillingLines_MILSTRIPsLink", "ID", "lnk_1007747_BLMPJPRD_PROJPRODINVCLN_CHLD");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_MILSTRIPsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_MILSTRIPsLink);
BLMPJPRD_ProjectProductBillingLines_MILSTRIPsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_MILSTRIPsForm...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_MILSTRIPsForm = new Control("ProjectProductBillingLines_MILSTRIPsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODMILSTRIPPRO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_MILSTRIPsForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExist on ProjectProductBillingLines_MILSTRIPsFormTable...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_MILSTRIPsFormTable = new Control("ProjectProductBillingLines_MILSTRIPsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODMILSTRIPPRO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_MILSTRIPsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ProjectProductBillingLines_MILSTRIPs_Ok...", Logger.MessageType.INF);
			Control BLMPJPRD_ProjectProductBillingLines_MILSTRIPs_Ok = new Control("ProjectProductBillingLines_MILSTRIPs_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODMILSTRIPPRO_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPJPRD_ProjectProductBillingLines_MILSTRIPs_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ProjectProductBillingLines_MILSTRIPsForm);
formBttn = BLMPJPRD_ProjectProductBillingLines_MILSTRIPsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("STANDARD TEXT");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on StandardTextLink...", Logger.MessageType.INF);
			Control BLMPJPRD_StandardTextLink = new Control("StandardTextLink", "ID", "lnk_1003388_BLMPJPRD_PROJPRODINVCHDR");
			CPCommon.AssertEqual(true,BLMPJPRD_StandardTextLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_StandardTextLink);
BLMPJPRD_StandardTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on StandardTextForm...", Logger.MessageType.INF);
			Control BLMPJPRD_StandardTextForm = new Control("StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODINVCTXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_StandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control BLMPJPRD_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODINVCTXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPJPRD_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on StandardText_Ok...", Logger.MessageType.INF);
			Control BLMPJPRD_StandardText_Ok = new Control("StandardText_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPJPRD_PROJPRODINVCTXT_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLMPJPRD_StandardText_Ok.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_StandardTextForm);
formBttn = BLMPJPRD_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("EXCHANGE RATES");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMPJPRD_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1003406_BLMPJPRD_PROJPRODINVCHDR");
			CPCommon.AssertEqual(true,BLMPJPRD_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ExchangeRatesLink);
BLMPJPRD_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMPJPRD_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control BLMPJPRD_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMPJPRD_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_ExchangeRatesForm);
formBttn = BLMPJPRD_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("INVOICE TOTALS");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on InvoiceTotalsLink...", Logger.MessageType.INF);
			Control BLMPJPRD_InvoiceTotalsLink = new Control("InvoiceTotalsLink", "ID", "lnk_1003391_BLMPJPRD_PROJPRODINVCHDR");
			CPCommon.AssertEqual(true,BLMPJPRD_InvoiceTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_InvoiceTotalsLink);
BLMPJPRD_InvoiceTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on InvoiceTotalsForm...", Logger.MessageType.INF);
			Control BLMPJPRD_InvoiceTotalsForm = new Control("InvoiceTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPBLINVT_INVCTOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMPJPRD_InvoiceTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPJPRD] Perfoming VerifyExists on InvoiceTotals_ExchangeRates_BillingToFunctional...", Logger.MessageType.INF);
			Control BLMPJPRD_InvoiceTotals_ExchangeRates_BillingToFunctional = new Control("InvoiceTotals_ExchangeRates_BillingToFunctional", "xpath", "//div[translate(@id,'0123456789','')='pr__CPBLINVT_INVCTOTALS_']/ancestor::form[1]/descendant::*[@id='TRANS_TO_FUNC_ER']");
			CPCommon.AssertEqual(true,BLMPJPRD_InvoiceTotals_ExchangeRates_BillingToFunctional.Exists());

												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_InvoiceTotalsForm);
formBttn = BLMPJPRD_InvoiceTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_MainForm);
formBttn = BLMPJPRD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? BLMPJPRD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
BLMPJPRD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "BLMPJPRD";
							CPCommon.WaitControlDisplayed(BLMPJPRD_MainForm);
formBttn = BLMPJPRD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

