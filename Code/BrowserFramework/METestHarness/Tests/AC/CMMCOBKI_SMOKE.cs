 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CMMCOBKI_SMOKE : TestScript
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
new Control("Cash Management", "xpath","//div[@class='deptItem'][.='Cash Management']").Click();
new Control("Bank Account Management", "xpath","//div[@class='navItem'][.='Bank Account Management']").Click();
new Control("Manage Company Bank Accounts (Non-US Banks)", "xpath","//div[@class='navItem'][.='Manage Company Bank Accounts (Non-US Banks)']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control CMMCOBKI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,CMMCOBKI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on BankAbbrev...", Logger.MessageType.INF);
			Control CMMCOBKI_BankAbbrev = new Control("BankAbbrev", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ACCT_ABBRV']");
			CPCommon.AssertEqual(true,CMMCOBKI_BankAbbrev.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control CMMCOBKI_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,CMMCOBKI_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_MainFormTab);
IWebElement mTab = CMMCOBKI_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on Details_BankInfo_NonUSBankID...", Logger.MessageType.INF);
			Control CMMCOBKI_Details_BankInfo_NonUSBankID = new Control("Details_BankInfo_NonUSBankID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NON_US_BANK_ID']");
			CPCommon.AssertEqual(true,CMMCOBKI_Details_BankInfo_NonUSBankID.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_MainFormTab);
mTab = CMMCOBKI_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Upload Bank Settings").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on UploadBankSettings_FileLayoutHeadingInfo_FieldNo_Code...", Logger.MessageType.INF);
			Control CMMCOBKI_UploadBankSettings_FileLayoutHeadingInfo_FieldNo_Code = new Control("UploadBankSettings_FileLayoutHeadingInfo_FieldNo_Code", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CODE_FLD_NO']");
			CPCommon.AssertEqual(true,CMMCOBKI_UploadBankSettings_FileLayoutHeadingInfo_FieldNo_Code.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_MainForm);
IWebElement formBttn = CMMCOBKI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? CMMCOBKI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
CMMCOBKI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control CMMCOBKI_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCOBKI_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on ApprovedIntermediaryBanksLink...", Logger.MessageType.INF);
			Control CMMCOBKI_ApprovedIntermediaryBanksLink = new Control("ApprovedIntermediaryBanksLink", "ID", "lnk_16630_CMMCOBKI_BANKACCT_HDR");
			CPCommon.AssertEqual(true,CMMCOBKI_ApprovedIntermediaryBanksLink.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_ApprovedIntermediaryBanksLink);
CMMCOBKI_ApprovedIntermediaryBanksLink.Click(1.5);


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on ApprovedIntermediaryBanksForm...", Logger.MessageType.INF);
			Control CMMCOBKI_ApprovedIntermediaryBanksForm = new Control("ApprovedIntermediaryBanksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_BANKACCTIBLINK_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,CMMCOBKI_ApprovedIntermediaryBanksForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExist on ApprovedIntermediaryBanksFormTable...", Logger.MessageType.INF);
			Control CMMCOBKI_ApprovedIntermediaryBanksFormTable = new Control("ApprovedIntermediaryBanksFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_BANKACCTIBLINK_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCOBKI_ApprovedIntermediaryBanksFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_ApprovedIntermediaryBanksForm);
formBttn = CMMCOBKI_ApprovedIntermediaryBanksForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on EFTBankInfoLink...", Logger.MessageType.INF);
			Control CMMCOBKI_EFTBankInfoLink = new Control("EFTBankInfoLink", "ID", "lnk_16631_CMMCOBKI_BANKACCT_HDR");
			CPCommon.AssertEqual(true,CMMCOBKI_EFTBankInfoLink.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_EFTBankInfoLink);
CMMCOBKI_EFTBankInfoLink.Click(1.5);


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on EFTBankInfoForm...", Logger.MessageType.INF);
			Control CMMCOBKI_EFTBankInfoForm = new Control("EFTBankInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_NONUS_EFTINFO_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,CMMCOBKI_EFTBankInfoForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on EFTBankInfo_BankInfo_NonUSDebitBankID...", Logger.MessageType.INF);
			Control CMMCOBKI_EFTBankInfo_BankInfo_NonUSDebitBankID = new Control("EFTBankInfo_BankInfo_NonUSDebitBankID", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_NONUS_EFTINFO_CTW_']/ancestor::form[1]/descendant::*[@id='NON_US_DBT_BANK_ID']");
			CPCommon.AssertEqual(true,CMMCOBKI_EFTBankInfo_BankInfo_NonUSDebitBankID.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_EFTBankInfoForm);
formBttn = CMMCOBKI_EFTBankInfoForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? CMMCOBKI_EFTBankInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
CMMCOBKI_EFTBankInfoForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExist on EFTBankInfoFormTable...", Logger.MessageType.INF);
			Control CMMCOBKI_EFTBankInfoFormTable = new Control("EFTBankInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_NONUS_EFTINFO_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCOBKI_EFTBankInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_EFTBankInfoForm);
formBttn = CMMCOBKI_EFTBankInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on ViewCashAccountsLink...", Logger.MessageType.INF);
			Control CMMCOBKI_ViewCashAccountsLink = new Control("ViewCashAccountsLink", "ID", "lnk_16633_CMMCOBKI_BANKACCT_HDR");
			CPCommon.AssertEqual(true,CMMCOBKI_ViewCashAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_ViewCashAccountsLink);
CMMCOBKI_ViewCashAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on ViewCashAccountsForm...", Logger.MessageType.INF);
			Control CMMCOBKI_ViewCashAccountsForm = new Control("ViewCashAccountsForm", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_ACCTS_CTW1_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,CMMCOBKI_ViewCashAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExist on ViewCashAccountsFormTable...", Logger.MessageType.INF);
			Control CMMCOBKI_ViewCashAccountsFormTable = new Control("ViewCashAccountsFormTable", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_ACCTS_CTW1_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCOBKI_ViewCashAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_ViewCashAccountsForm);
formBttn = CMMCOBKI_ViewCashAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? CMMCOBKI_ViewCashAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
CMMCOBKI_ViewCashAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "CMMCOBKI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCOBKI] Perfoming VerifyExists on ViewCashAccounts_Account...", Logger.MessageType.INF);
			Control CMMCOBKI_ViewCashAccounts_Account = new Control("ViewCashAccounts_Account", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_ACCTS_CTW1_')]/ancestor::form[1]/descendant::*[@id='CASH_ACCT_ID']");
			CPCommon.AssertEqual(true,CMMCOBKI_ViewCashAccounts_Account.Exists());

												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_ViewCashAccountsForm);
formBttn = CMMCOBKI_ViewCashAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "CMMCOBKI";
							CPCommon.WaitControlDisplayed(CMMCOBKI_MainForm);
formBttn = CMMCOBKI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

