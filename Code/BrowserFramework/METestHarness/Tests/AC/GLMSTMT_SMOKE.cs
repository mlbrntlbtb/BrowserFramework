 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMSTMT_SMOKE : TestScript
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
new Control("Manage Bank Statement Information", "xpath","//div[@class='navItem'][.='Manage Bank Statement Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExists on BankAbbr...", Logger.MessageType.INF);
			Control GLMSTMT_BankAbbr = new Control("BankAbbr", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ACCT_ABBRV']");
			CPCommon.AssertEqual(true,GLMSTMT_BankAbbr.Exists());

												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control GLMSTMT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMSTMT_MainForm);
IWebElement formBttn = GLMSTMT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMSTMT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMSTMT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMSTMT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSTMT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Statement and Timing Info");


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExist on StatementAndTimingInfoFormTable...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfoFormTable = new Control("StatementAndTimingInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSTMT_StatementAndTimingInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming ClickButton on StatementAndTimingInfoForm...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfoForm = new Control("StatementAndTimingInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_DETL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMSTMT_StatementAndTimingInfoForm);
formBttn = GLMSTMT_StatementAndTimingInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMSTMT_StatementAndTimingInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMSTMT_StatementAndTimingInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExists on StatementAndTimingInfo_FiscalYear...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_FiscalYear = new Control("StatementAndTimingInfo_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_DETL_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLMSTMT_StatementAndTimingInfo_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Bank Upload");


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming Click on StatementAndTimingInfo_BankStatementDetailsLink...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_BankStatementDetailsLink = new Control("StatementAndTimingInfo_BankStatementDetailsLink", "ID", "lnk_4197_GLMSTMT_DETL");
			CPCommon.WaitControlDisplayed(GLMSTMT_StatementAndTimingInfo_BankStatementDetailsLink);
GLMSTMT_StatementAndTimingInfo_BankStatementDetailsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExists on StatementAndTimingInfo_BankUploadTotals_BeginningBalance...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BeginningBalance = new Control("StatementAndTimingInfo_BankUploadTotals_BeginningBalance", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_BANK_TOTALS_']/ancestor::form[1]/descendant::*[@id='BEGINING_BAL']");
			CPCommon.AssertEqual(true,GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BeginningBalance.Exists());

												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExist on StatementAndTimingInfo_BankUploadTotals_BankStatementDetailFormTable...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailFormTable = new Control("StatementAndTimingInfo_BankUploadTotals_BankStatementDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_BANK_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming ClickButton on StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm = new Control("StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_BANK_DETL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm);
formBttn = GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming VerifyExists on StatementAndTimingInfo_BankUploadTotals_BankStatementDetail_TransDate...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetail_TransDate = new Control("StatementAndTimingInfo_BankUploadTotals_BankStatementDetail_TransDate", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_BANK_DETL_']/ancestor::form[1]/descendant::*[@id='TRN_DT']");
			CPCommon.AssertEqual(true,GLMSTMT_StatementAndTimingInfo_BankUploadTotals_BankStatementDetail_TransDate.Exists());

												
				CPCommon.CurrentComponent = "GLMSTMT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSTMT] Perfoming Close on StatementAndTimingInfo_BankUploadTotalsForm...", Logger.MessageType.INF);
			Control GLMSTMT_StatementAndTimingInfo_BankUploadTotalsForm = new Control("StatementAndTimingInfo_BankUploadTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSTMT_BANK_TOTALS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMSTMT_StatementAndTimingInfo_BankUploadTotalsForm);
formBttn = GLMSTMT_StatementAndTimingInfo_BankUploadTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "GLMSTMT";
							CPCommon.WaitControlDisplayed(GLMSTMT_MainForm);
formBttn = GLMSTMT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

