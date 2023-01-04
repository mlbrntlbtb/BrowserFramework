 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CMMCFA_SMOKE : TestScript
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
new Control("Cash Management Controls", "xpath","//div[@class='navItem'][.='Cash Management Controls']").Click();
new Control("Manage Cash Forecast Accounts", "xpath","//div[@class='navItem'][.='Manage Cash Forecast Accounts']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "CMMCFA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control CMMCFA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,CMMCFA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCFA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFA] Perfoming VerifyExists on LinkCashForecastTemplate...", Logger.MessageType.INF);
			Control CMMCFA_LinkCashForecastTemplate = new Control("LinkCashForecastTemplate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TMPLT_CD']");
			CPCommon.AssertEqual(true,CMMCFA_LinkCashForecastTemplate.Exists());

												
				CPCommon.CurrentComponent = "CMMCFA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFA] Perfoming VerifyExists on CashForecastAccountsLink...", Logger.MessageType.INF);
			Control CMMCFA_CashForecastAccountsLink = new Control("CashForecastAccountsLink", "ID", "lnk_3630_CMMCFA_CFTTMPLT_HDR");
			CPCommon.AssertEqual(true,CMMCFA_CashForecastAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "CMMCFA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFA] Perfoming VerifyExists on CashForecastAccountsForm...", Logger.MessageType.INF);
			Control CMMCFA_CashForecastAccountsForm = new Control("CashForecastAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMCFA_CFTCASHACCTS_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,CMMCFA_CashForecastAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCFA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFA] Perfoming VerifyExist on CashForecastAccountsFormTable...", Logger.MessageType.INF);
			Control CMMCFA_CashForecastAccountsFormTable = new Control("CashForecastAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMCFA_CFTCASHACCTS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCFA_CashForecastAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCFA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFA] Perfoming VerifyExists on CashForecastAccounts_Ok...", Logger.MessageType.INF);
			Control CMMCFA_CashForecastAccounts_Ok = new Control("CashForecastAccounts_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMCFA_CFTCASHACCTS_CHILD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,CMMCFA_CashForecastAccounts_Ok.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "CMMCFA";
							CPCommon.WaitControlDisplayed(CMMCFA_MainForm);
IWebElement formBttn = CMMCFA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

