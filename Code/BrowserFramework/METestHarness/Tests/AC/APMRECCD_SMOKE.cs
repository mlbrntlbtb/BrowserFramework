 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMRECCD_SMOKE : TestScript
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
new Control("Accounts Payable Controls", "xpath","//div[@class='navItem'][.='Accounts Payable Controls']").Click();
new Control("Manage Recurring A/P Voucher Codes", "xpath","//div[@class='navItem'][.='Manage Recurring A/P Voucher Codes']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "APMRECCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMRECCD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMRECCD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMRECCD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMRECCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMRECCD] Perfoming VerifyExists on RecurringCode...", Logger.MessageType.INF);
			Control APMRECCD_RecurringCode = new Control("RecurringCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RECUR_VCHR_DC']");
			CPCommon.AssertEqual(true,APMRECCD_RecurringCode.Exists());

												
				CPCommon.CurrentComponent = "APMRECCD";
							CPCommon.WaitControlDisplayed(APMRECCD_MainForm);
IWebElement formBttn = APMRECCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMRECCD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMRECCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMRECCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMRECCD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMRECCD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMRECCD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMRECCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMRECCD] Perfoming VerifyExist on PeriodFormTable...", Logger.MessageType.INF);
			Control APMRECCD_PeriodFormTable = new Control("PeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMRECCD_SUBPD_ACCTINGPD_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMRECCD_PeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMRECCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMRECCD] Perfoming VerifyExist on SelectedPeriodFormTable...", Logger.MessageType.INF);
			Control APMRECCD_SelectedPeriodFormTable = new Control("SelectedPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMRECCD_RECURVCHRPDS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMRECCD_SelectedPeriodFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APMRECCD";
							CPCommon.WaitControlDisplayed(APMRECCD_MainForm);
formBttn = APMRECCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

