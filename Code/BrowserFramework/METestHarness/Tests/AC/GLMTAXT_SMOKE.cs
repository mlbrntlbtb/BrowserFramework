 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMTAXT_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Sales and Value Added Tax Processing", "xpath","//div[@class='navItem'][.='Sales and Value Added Tax Processing']").Click();
new Control("View Tax Reporting Status by Transaction", "xpath","//div[@class='navItem'][.='View Tax Reporting Status by Transaction']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExists on TransactiontypeForm...", Logger.MessageType.INF);
			Control GLMTAXT_TransactiontypeForm = new Control("TransactiontypeForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMTAXT_TransactiontypeForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExists on Output_InvoiceDate_Ending...", Logger.MessageType.INF);
			Control GLMTAXT_Output_InvoiceDate_Ending = new Control("Output_InvoiceDate_Ending", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='I_END_DATE']");
			CPCommon.AssertEqual(true,GLMTAXT_Output_InvoiceDate_Ending.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExists on Transactiontype_ChildOneForm...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildOneForm = new Control("Transactiontype_ChildOneForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAXT_RPTTAXHDR_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildOneForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExist on Transactiontype_ChildOneFormTable...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildOneFormTable = new Control("Transactiontype_ChildOneFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAXT_RPTTAXHDR_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildOneFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExists on Transactiontype_ChildTwoForm...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildTwoForm = new Control("Transactiontype_ChildTwoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLN_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildTwoForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExist on Transactiontype_ChildTwoFormTable...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildTwoFormTable = new Control("Transactiontype_ChildTwoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLN_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildTwoFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExists on Transactiontype_ChildTwo_TaxAccountsLink...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildTwo_TaxAccountsLink = new Control("Transactiontype_ChildTwo_TaxAccountsLink", "ID", "lnk_1001788_GLMTAX_RPTTAXLN_HDR");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildTwo_TaxAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
							CPCommon.WaitControlDisplayed(GLMTAXT_Transactiontype_ChildTwo_TaxAccountsLink);
GLMTAXT_Transactiontype_ChildTwo_TaxAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExists on Transactiontype_ChildTwo_TaxAccountsForm...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildTwo_TaxAccountsForm = new Control("Transactiontype_ChildTwo_TaxAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLNACCT_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildTwo_TaxAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXT] Perfoming VerifyExist on Transactiontype_ChildTwo_TaxAccountsFormTable...", Logger.MessageType.INF);
			Control GLMTAXT_Transactiontype_ChildTwo_TaxAccountsFormTable = new Control("Transactiontype_ChildTwo_TaxAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLNACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMTAXT_Transactiontype_ChildTwo_TaxAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXT";
							CPCommon.WaitControlDisplayed(GLMTAXT_Transactiontype_ChildTwo_TaxAccountsForm);
IWebElement formBttn = GLMTAXT_Transactiontype_ChildTwo_TaxAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "GLMTAXT";
							CPCommon.WaitControlDisplayed(GLMTAXT_TransactiontypeForm);
formBttn = GLMTAXT_TransactiontypeForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

