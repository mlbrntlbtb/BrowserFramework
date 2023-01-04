 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FARRECON_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Reports", "xpath","//div[@class='navItem'][.='Fixed Assets Reports']").Click();
new Control("Print Fixed Assets/General Ledger Reconciliation Report", "xpath","//div[@class='navItem'][.='Print Fixed Assets/General Ledger Reconciliation Report']").Click();


												
				CPCommon.CurrentComponent = "FARRECON";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARRECON] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FARRECON_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FARRECON_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FARRECON";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARRECON] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control FARRECON_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,FARRECON_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "FARRECON";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARRECON] Perfoming VerifyExists on SelectByOptions_PrintReconciliationFor_FACost...", Logger.MessageType.INF);
			Control FARRECON_SelectByOptions_PrintReconciliationFor_FACost = new Control("SelectByOptions_PrintReconciliationFor_FACost", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INCL_ASSET_ACCTS']");
			CPCommon.AssertEqual(true,FARRECON_SelectByOptions_PrintReconciliationFor_FACost.Exists());

												
				CPCommon.CurrentComponent = "FARRECON";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARRECON] Perfoming VerifyExists on SelectionRange_Option_AssetAccountSNM...", Logger.MessageType.INF);
			Control FARRECON_SelectionRange_Option_AssetAccountSNM = new Control("SelectionRange_Option_AssetAccountSNM", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_RANGE_OPT']");
			CPCommon.AssertEqual(true,FARRECON_SelectionRange_Option_AssetAccountSNM.Exists());

												
				CPCommon.CurrentComponent = "FARRECON";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FARRECON] Perfoming VerifyExists on Options_Include_FACurrentPeriod...", Logger.MessageType.INF);
			Control FARRECON_Options_Include_FACurrentPeriod = new Control("Options_Include_FACurrentPeriod", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INCL_CURPD_DEPR']");
			CPCommon.AssertEqual(true,FARRECON_Options_Include_FACurrentPeriod.Exists());

												
				CPCommon.CurrentComponent = "FARRECON";
							CPCommon.WaitControlDisplayed(FARRECON_MainForm);
IWebElement formBttn = FARRECON_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

