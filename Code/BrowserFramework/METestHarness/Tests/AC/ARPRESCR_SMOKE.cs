 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARPRESCR_SMOKE : TestScript
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
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Cash Receipts Processing", "xpath","//div[@class='navItem'][.='Cash Receipts Processing']").Click();
new Control("Copy/Reverse Cash Receipts", "xpath","//div[@class='navItem'][.='Copy/Reverse Cash Receipts']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARPRESCR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARPRESCR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control ARPRESCR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,ARPRESCR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "ARPRESCR";
							CPCommon.WaitControlDisplayed(ARPRESCR_MainForm);
IWebElement formBttn = ARPRESCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ARPRESCR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ARPRESCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ARPRESCR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARPRESCR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CASH RECEIPTS NON CONTIGUOUS RANGES");


												
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExists on CashReceiptsNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARPRESCR_CashReceiptsNonContiguousRangesLink = new Control("CashReceiptsNonContiguousRangesLink", "ID", "lnk_1004744_ARPRESCR_PARAM");
			CPCommon.AssertEqual(true,ARPRESCR_CashReceiptsNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "ARPRESCR";
							CPCommon.WaitControlDisplayed(ARPRESCR_CashReceiptsNonContiguousRangesLink);
ARPRESCR_CashReceiptsNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExists on CashReceiptsNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARPRESCR_CashReceiptsNonContiguousRangesForm = new Control("CashReceiptsNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCASHRECPTNO_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ARPRESCR_CashReceiptsNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExist on CashReceiptsNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control ARPRESCR_CashReceiptsNonContiguousRangesFormTable = new Control("CashReceiptsNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCASHRECPTNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARPRESCR_CashReceiptsNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARPRESCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPRESCR] Perfoming VerifyExists on CashReceiptsNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control ARPRESCR_CashReceiptsNonContiguousRanges_Ok = new Control("CashReceiptsNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCASHRECPTNO_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,ARPRESCR_CashReceiptsNonContiguousRanges_Ok.Exists());

												
				CPCommon.CurrentComponent = "ARPRESCR";
							CPCommon.WaitControlDisplayed(ARPRESCR_CashReceiptsNonContiguousRangesForm);
formBttn = ARPRESCR_CashReceiptsNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "ARPRESCR";
							CPCommon.WaitControlDisplayed(ARPRESCR_MainForm);
formBttn = ARPRESCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

