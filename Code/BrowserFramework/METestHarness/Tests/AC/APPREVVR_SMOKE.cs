 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPREVVR_SMOKE : TestScript
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
new Control("Voucher Processing", "xpath","//div[@class='navItem'][.='Voucher Processing']").Click();
new Control("Reverse Posted Vouchers", "xpath","//div[@class='navItem'][.='Reverse Posted Vouchers']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APPREVVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPREVVR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPREVVR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPREVVR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPREVVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPREVVR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APPREVVR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APPREVVR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APPREVVR";
							CPCommon.WaitControlDisplayed(APPREVVR_MainForm);
IWebElement formBttn = APPREVVR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APPREVVR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APPREVVR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APPREVVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPREVVR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APPREVVR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPREVVR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("VOUCHER NUMBER NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "APPREVVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPREVVR] Perfoming VerifyExists on VoucherNumberNonContiguousRangesLink...", Logger.MessageType.INF);
			Control APPREVVR_VoucherNumberNonContiguousRangesLink = new Control("VoucherNumberNonContiguousRangesLink", "ID", "lnk_1004740_APPREVVR_PARAM");
			CPCommon.AssertEqual(true,APPREVVR_VoucherNumberNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "APPREVVR";
							CPCommon.WaitControlDisplayed(APPREVVR_VoucherNumberNonContiguousRangesLink);
APPREVVR_VoucherNumberNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APPREVVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPREVVR] Perfoming VerifyExists on VoucherNumberNonContiguousRangesForm...", Logger.MessageType.INF);
			Control APPREVVR_VoucherNumberNonContiguousRangesForm = new Control("VoucherNumberNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRVCHRNO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APPREVVR_VoucherNumberNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "APPREVVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPREVVR] Perfoming VerifyExist on VoucherNumberNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control APPREVVR_VoucherNumberNonContiguousRangesFormTable = new Control("VoucherNumberNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRVCHRNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPREVVR_VoucherNumberNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "APPREVVR";
							CPCommon.WaitControlDisplayed(APPREVVR_VoucherNumberNonContiguousRangesForm);
formBttn = APPREVVR_VoucherNumberNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APPREVVR";
							CPCommon.WaitControlDisplayed(APPREVVR_MainForm);
formBttn = APPREVVR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

