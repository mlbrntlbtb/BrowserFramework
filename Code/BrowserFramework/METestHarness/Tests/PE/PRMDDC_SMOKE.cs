 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMDDC_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Controls", "xpath","//div[@class='navItem'][.='Payroll Controls']").Click();
new Control("Configure Direct Deposit Settings", "xpath","//div[@class='navItem'][.='Configure Direct Deposit Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMDDC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMDDC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMDDC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMDDC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDC] Perfoming VerifyExists on PayCycle...", Logger.MessageType.INF);
			Control PRMDDC_PayCycle = new Control("PayCycle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_PD_CD']");
			CPCommon.AssertEqual(true,PRMDDC_PayCycle.Exists());

												
				CPCommon.CurrentComponent = "PRMDDC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDC] Perfoming VerifyExists on MainTab...", Logger.MessageType.INF);
			Control PRMDDC_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PRMDDC_MainTab.Exists());

												
				CPCommon.CurrentComponent = "PRMDDC";
							CPCommon.WaitControlDisplayed(PRMDDC_MainTab);
IWebElement mTab = PRMDDC_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "ABA/File Setup").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRMDDC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDC] Perfoming VerifyExists on ABAFileSetup_ABAFileSetup_ABAIdentification_BankAbbreviation...", Logger.MessageType.INF);
			Control PRMDDC_ABAFileSetup_ABAFileSetup_ABAIdentification_BankAbbreviation = new Control("ABAFileSetup_ABAFileSetup_ABAIdentification_BankAbbreviation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ACCT_ABBRV']");
			CPCommon.AssertEqual(true,PRMDDC_ABAFileSetup_ABAFileSetup_ABAIdentification_BankAbbreviation.Exists());

												
				CPCommon.CurrentComponent = "PRMDDC";
							CPCommon.WaitControlDisplayed(PRMDDC_MainTab);
mTab = PRMDDC_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Posting/Advice Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRMDDC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDC] Perfoming VerifyExists on PostingAdviceInformation_PostingAdviceOptions_Posting_CashAccountx...", Logger.MessageType.INF);
			Control PRMDDC_PostingAdviceInformation_PostingAdviceOptions_Posting_CashAccountx = new Control("PostingAdviceInformation_PostingAdviceOptions_Posting_CashAccountx", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CASH_ACCT_ID']");
			CPCommon.AssertEqual(true,PRMDDC_PostingAdviceInformation_PostingAdviceOptions_Posting_CashAccountx.Exists());

												
				CPCommon.CurrentComponent = "PRMDDC";
							CPCommon.WaitControlDisplayed(PRMDDC_MainForm);
IWebElement formBttn = PRMDDC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMDDC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMDDC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMDDC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDC] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMDDC_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMDDC_MainTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMDDC";
							CPCommon.WaitControlDisplayed(PRMDDC_MainForm);
formBttn = PRMDDC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

