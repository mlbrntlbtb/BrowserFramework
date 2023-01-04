 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPDEFER_SMOKE : TestScript
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
new Control("General Ledger Utilities", "xpath","//div[@class='navItem'][.='General Ledger Utilities']").Click();
new Control("Defer Unposted Transactions", "xpath","//div[@class='navItem'][.='Defer Unposted Transactions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLPDEFER_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLPDEFER_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control GLPDEFER_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,GLPDEFER_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_MainForm);
IWebElement formBttn = GLPDEFER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLPDEFER_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLPDEFER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLPDEFER_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPDEFER_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("AP Voucher  Number  Non-Contiguous Form");


												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on APVoucherNumberNonContiguousLink...", Logger.MessageType.INF);
			Control GLPDEFER_APVoucherNumberNonContiguousLink = new Control("APVoucherNumberNonContiguousLink", "ID", "lnk_3784_GLPDEFER_PARAM");
			CPCommon.AssertEqual(true,GLPDEFER_APVoucherNumberNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_APVoucherNumberNonContiguousLink);
GLPDEFER_APVoucherNumberNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on APVoucherNumberNonContiguousForm...", Logger.MessageType.INF);
			Control GLPDEFER_APVoucherNumberNonContiguousForm = new Control("APVoucherNumberNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRAPVCHRNO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLPDEFER_APVoucherNumberNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExist on APVoucherNumberNonContiguousFormTable...", Logger.MessageType.INF);
			Control GLPDEFER_APVoucherNumberNonContiguousFormTable = new Control("APVoucherNumberNonContiguousFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRAPVCHRNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPDEFER_APVoucherNumberNonContiguousFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_APVoucherNumberNonContiguousForm);
formBttn = GLPDEFER_APVoucherNumberNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PO Voucher Number Non-Contiguous Form");


												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on POVoucherNumberNonContiguousLink...", Logger.MessageType.INF);
			Control GLPDEFER_POVoucherNumberNonContiguousLink = new Control("POVoucherNumberNonContiguousLink", "ID", "lnk_3785_GLPDEFER_PARAM");
			CPCommon.AssertEqual(true,GLPDEFER_POVoucherNumberNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_POVoucherNumberNonContiguousLink);
GLPDEFER_POVoucherNumberNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on POVoucherNumberNonContiguousForm...", Logger.MessageType.INF);
			Control GLPDEFER_POVoucherNumberNonContiguousForm = new Control("POVoucherNumberNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRPOVCHRNO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLPDEFER_POVoucherNumberNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExist on POVoucherNumberNonContiguousFormTable...", Logger.MessageType.INF);
			Control GLPDEFER_POVoucherNumberNonContiguousFormTable = new Control("POVoucherNumberNonContiguousFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRPOVCHRNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPDEFER_POVoucherNumberNonContiguousFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_POVoucherNumberNonContiguousForm);
formBttn = GLPDEFER_POVoucherNumberNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("JE Number Non-Contiguous Form");


												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on JENumberNonContiguousLink...", Logger.MessageType.INF);
			Control GLPDEFER_JENumberNonContiguousLink = new Control("JENumberNonContiguousLink", "ID", "lnk_3786_GLPDEFER_PARAM");
			CPCommon.AssertEqual(true,GLPDEFER_JENumberNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_JENumberNonContiguousLink);
GLPDEFER_JENumberNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on JEVoucherNumberNonContiguousForm...", Logger.MessageType.INF);
			Control GLPDEFER_JEVoucherNumberNonContiguousForm = new Control("JEVoucherNumberNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRJENO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLPDEFER_JEVoucherNumberNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExist on JEVoucherNumberNonContiguousFormTable...", Logger.MessageType.INF);
			Control GLPDEFER_JEVoucherNumberNonContiguousFormTable = new Control("JEVoucherNumberNonContiguousFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRJENO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPDEFER_JEVoucherNumberNonContiguousFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_JEVoucherNumberNonContiguousForm);
formBttn = GLPDEFER_JEVoucherNumberNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Timesheet Date Non-Contiguous Form");


												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on TimesheetDateNonContiguousLink...", Logger.MessageType.INF);
			Control GLPDEFER_TimesheetDateNonContiguousLink = new Control("TimesheetDateNonContiguousLink", "ID", "lnk_3787_GLPDEFER_PARAM");
			CPCommon.AssertEqual(true,GLPDEFER_TimesheetDateNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_TimesheetDateNonContiguousLink);
GLPDEFER_TimesheetDateNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExists on TimesheetDateNonContiguousForm...", Logger.MessageType.INF);
			Control GLPDEFER_TimesheetDateNonContiguousForm = new Control("TimesheetDateNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRTSDT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLPDEFER_TimesheetDateNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPDEFER] Perfoming VerifyExist on TimesheetDateNonContiguousFormTable...", Logger.MessageType.INF);
			Control GLPDEFER_TimesheetDateNonContiguousFormTable = new Control("TimesheetDateNonContiguousFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPDEFER_NCRTSDT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPDEFER_TimesheetDateNonContiguousFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_TimesheetDateNonContiguousForm);
formBttn = GLPDEFER_TimesheetDateNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLPDEFER";
							CPCommon.WaitControlDisplayed(GLPDEFER_MainForm);
formBttn = GLPDEFER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

