 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMCOBNK_SMOKE : TestScript
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
new Control("Manage Company Bank Accounts (US Banks)", "xpath","//div[@class='navItem'][.='Manage Company Bank Accounts (US Banks)']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on BankAbbrev...", Logger.MessageType.INF);
			Control GLMCOBNK_BankAbbrev = new Control("BankAbbrev", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ACCT_ABBRV']");
			CPCommon.AssertEqual(true,GLMCOBNK_BankAbbrev.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control GLMCOBNK_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(GLMCOBNK_MainFormTab);
IWebElement mTab = GLMCOBNK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on Details_BankInfo_USBankIDABA...", Logger.MessageType.INF);
			Control GLMCOBNK_Details_BankInfo_USBankIDABA = new Control("Details_BankInfo_USBankIDABA", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ABA_NO']");
			CPCommon.AssertEqual(true,GLMCOBNK_Details_BankInfo_USBankIDABA.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
							CPCommon.WaitControlDisplayed(GLMCOBNK_MainFormTab);
mTab = GLMCOBNK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Upload Bank Settings").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on UploadBankSettings_FileLayoutHeadingInfo_Code_AccountNo...", Logger.MessageType.INF);
			Control GLMCOBNK_UploadBankSettings_FileLayoutHeadingInfo_Code_AccountNo = new Control("UploadBankSettings_FileLayoutHeadingInfo_Code_AccountNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_CD']");
			CPCommon.AssertEqual(true,GLMCOBNK_UploadBankSettings_FileLayoutHeadingInfo_Code_AccountNo.Exists());

											Driver.SessionLogger.WriteLine("Approved Internediary Banks");


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Click on ApprovedIntermediaryBanksLink...", Logger.MessageType.INF);
			Control GLMCOBNK_ApprovedIntermediaryBanksLink = new Control("ApprovedIntermediaryBanksLink", "ID", "lnk_16612_CPMCOBNK_BANKACCT_HDR");
			CPCommon.WaitControlDisplayed(GLMCOBNK_ApprovedIntermediaryBanksLink);
GLMCOBNK_ApprovedIntermediaryBanksLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExist on ApprovedIntermediaryBanksFormTable...", Logger.MessageType.INF);
			Control GLMCOBNK_ApprovedIntermediaryBanksFormTable = new Control("ApprovedIntermediaryBanksFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_BANKACCTIBLINK_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCOBNK_ApprovedIntermediaryBanksFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Close on ApprovedIntermediaryBanksForm...", Logger.MessageType.INF);
			Control GLMCOBNK_ApprovedIntermediaryBanksForm = new Control("ApprovedIntermediaryBanksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMCOBNK_BANKACCTIBLINK_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMCOBNK_ApprovedIntermediaryBanksForm);
IWebElement formBttn = GLMCOBNK_ApprovedIntermediaryBanksForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("EFT Bank Info");


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Click on EFTBankInfoLink...", Logger.MessageType.INF);
			Control GLMCOBNK_EFTBankInfoLink = new Control("EFTBankInfoLink", "ID", "lnk_16613_CPMCOBNK_BANKACCT_HDR");
			CPCommon.WaitControlDisplayed(GLMCOBNK_EFTBankInfoLink);
GLMCOBNK_EFTBankInfoLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on EFTBankInfoForm...", Logger.MessageType.INF);
			Control GLMCOBNK_EFTBankInfoForm = new Control("EFTBankInfoForm", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_EFTINFO_CTW2_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMCOBNK_EFTBankInfoForm.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Select on EFTBankInfo_EFTBankInfoTab...", Logger.MessageType.INF);
			Control GLMCOBNK_EFTBankInfo_EFTBankInfoTab = new Control("EFTBankInfo_EFTBankInfoTab", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_EFTINFO_CTW2_')]/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(GLMCOBNK_EFTBankInfo_EFTBankInfoTab);
mTab = GLMCOBNK_EFTBankInfo_EFTBankInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Bank Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "GLMCOBNK";
							CPCommon.WaitControlDisplayed(GLMCOBNK_EFTBankInfo_EFTBankInfoTab);
mTab = GLMCOBNK_EFTBankInfo_EFTBankInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "ACH Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on EFTBankInfo_ACHInfo_AddtionalHeaderRecords_AdditionalHeaderRecords1...", Logger.MessageType.INF);
			Control GLMCOBNK_EFTBankInfo_ACHInfo_AddtionalHeaderRecords_AdditionalHeaderRecords1 = new Control("EFTBankInfo_ACHInfo_AddtionalHeaderRecords_AdditionalHeaderRecords1", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_EFTINFO_CTW2_')]/ancestor::form[1]/descendant::*[@id='HDR_REC1_S']");
			CPCommon.AssertEqual(true,GLMCOBNK_EFTBankInfo_ACHInfo_AddtionalHeaderRecords_AdditionalHeaderRecords1.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
							CPCommon.WaitControlDisplayed(GLMCOBNK_EFTBankInfo_EFTBankInfoTab);
mTab = GLMCOBNK_EFTBankInfo_EFTBankInfoTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "EDI Addenda/820 Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on EFTBankInfo_EDIAddenda820Info_EDIAddenda820Info_EDISenderID...", Logger.MessageType.INF);
			Control GLMCOBNK_EFTBankInfo_EDIAddenda820Info_EDIAddenda820Info_EDISenderID = new Control("EFTBankInfo_EDIAddenda820Info_EDIAddenda820Info_EDISenderID", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_EFTINFO_CTW2_')]/ancestor::form[1]/descendant::*[@id='EDI_ISA06_ID']");
			CPCommon.AssertEqual(true,GLMCOBNK_EFTBankInfo_EDIAddenda820Info_EDIAddenda820Info_EDISenderID.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
							CPCommon.WaitControlDisplayed(GLMCOBNK_EFTBankInfoForm);
formBttn = GLMCOBNK_EFTBankInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("View Cash Accounts");


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Click on ViewCashAccountsLink...", Logger.MessageType.INF);
			Control GLMCOBNK_ViewCashAccountsLink = new Control("ViewCashAccountsLink", "ID", "lnk_16614_CPMCOBNK_BANKACCT_HDR");
			CPCommon.WaitControlDisplayed(GLMCOBNK_ViewCashAccountsLink);
GLMCOBNK_ViewCashAccountsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExist on ViewCashAccountsFormTable...", Logger.MessageType.INF);
			Control GLMCOBNK_ViewCashAccountsFormTable = new Control("ViewCashAccountsFormTable", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_ACCTS_CTW1_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCOBNK_ViewCashAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming ClickButton on ViewCashAccountsForm...", Logger.MessageType.INF);
			Control GLMCOBNK_ViewCashAccountsForm = new Control("ViewCashAccountsForm", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_ACCTS_CTW1_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMCOBNK_ViewCashAccountsForm);
formBttn = GLMCOBNK_ViewCashAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMCOBNK_ViewCashAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMCOBNK_ViewCashAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming VerifyExists on ViewCashAccounts_AP...", Logger.MessageType.INF);
			Control GLMCOBNK_ViewCashAccounts_AP = new Control("ViewCashAccounts_AP", "xpath", "//div[starts-with(@id,'pr__CPMCOBNK_ACCTS_CTW1_')]/ancestor::form[1]/descendant::*[@id='AP_SRCE_FL']");
			CPCommon.AssertEqual(true,GLMCOBNK_ViewCashAccounts_AP.Exists());

												
				CPCommon.CurrentComponent = "GLMCOBNK";
							CPCommon.WaitControlDisplayed(GLMCOBNK_ViewCashAccountsForm);
formBttn = GLMCOBNK_ViewCashAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "GLMCOBNK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCOBNK] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control GLMCOBNK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMCOBNK_MainForm);
formBttn = GLMCOBNK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

