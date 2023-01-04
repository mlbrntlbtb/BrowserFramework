 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMTAXA_SMOKE : TestScript
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
new Control("View Tax Reporting Status by Tax Account", "xpath","//div[@class='navItem'][.='View Tax Reporting Status by Tax Account']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExists on TransactionEntitiesForm...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntitiesForm = new Control("TransactionEntitiesForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntitiesForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExists on Account...", Logger.MessageType.INF);
			Control GLMTAXA_Account = new Control("Account", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMTAXA_Account.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExists on TransactionEntities_ChildOneForm...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildOneForm = new Control("TransactionEntities_ChildOneForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAXA_RPTTAXLNACCT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildOneForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExist on TransactionEntities_ChildOneFormTable...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildOneFormTable = new Control("TransactionEntities_ChildOneFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAXA_RPTTAXLNACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildOneFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExists on TransactionEntities_ChildTwoForm...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildTwoForm = new Control("TransactionEntities_ChildTwoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLN_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildTwoForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExist on TransactionEntities_ChildTwoFormTable...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildTwoFormTable = new Control("TransactionEntities_ChildTwoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLN_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildTwoFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExists on TransactionEntities_ChildTwo_TaxAccountsLink...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsLink = new Control("TransactionEntities_ChildTwo_TaxAccountsLink", "ID", "lnk_1003026_GLMTAX_RPTTAXLN_HDR");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
							CPCommon.WaitControlDisplayed(GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsLink);
GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExists on TransactionEntities_ChildTwo_TaxAccountsForm...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsForm = new Control("TransactionEntities_ChildTwo_TaxAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLNACCT_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMTAXA] Perfoming VerifyExist on TransactionEntities_ChildTwo_TaxAccountsFormTable...", Logger.MessageType.INF);
			Control GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsFormTable = new Control("TransactionEntities_ChildTwo_TaxAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMTAX_RPTTAXLNACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMTAXA";
							CPCommon.WaitControlDisplayed(GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsForm);
IWebElement formBttn = GLMTAXA_TransactionEntities_ChildTwo_TaxAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "GLMTAXA";
							CPCommon.WaitControlDisplayed(GLMTAXA_TransactionEntitiesForm);
formBttn = GLMTAXA_TransactionEntitiesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

