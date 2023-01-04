 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMFS_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Financial Statement Configuration", "xpath","//div[@class='navItem'][.='Financial Statement Configuration']").Click();
new Control("Manage Financial Statements", "xpath","//div[@class='navItem'][.='Manage Financial Statements']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMFS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMFS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMFS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMFS] Perfoming VerifyExists on FinancialStatementCode...", Logger.MessageType.INF);
			Control GLMFS_FinancialStatementCode = new Control("FinancialStatementCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FS_CD']");
			CPCommon.AssertEqual(true,GLMFS_FinancialStatementCode.Exists());

												
				CPCommon.CurrentComponent = "GLMFS";
							CPCommon.WaitControlDisplayed(GLMFS_MainForm);
IWebElement formBttn = GLMFS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMFS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMFS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMFS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMFS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMFS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMFS";
							CPCommon.WaitControlDisplayed(GLMFS_MainForm);
formBttn = GLMFS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? GLMFS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
GLMFS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "GLMFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMFS] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLMFS_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMFS_FSLN_CASH_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMFS_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMFS] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLMFS_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMFS_FSLN_CASH_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMFS_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("link");


												
				CPCommon.CurrentComponent = "GLMFS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMFS] Perfoming VerifyExists on ChildForm_CashFlowAccountsLink...", Logger.MessageType.INF);
			Control GLMFS_ChildForm_CashFlowAccountsLink = new Control("ChildForm_CashFlowAccountsLink", "ID", "lnk_1001817_GLMFS_FSLN_CASH");
			CPCommon.AssertEqual(true,GLMFS_ChildForm_CashFlowAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMFS";
							CPCommon.WaitControlDisplayed(GLMFS_ChildForm_CashFlowAccountsLink);
GLMFS_ChildForm_CashFlowAccountsLink.Click(1.5);


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
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

