 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPBATCH_SMOKE : TestScript
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
new Control("Process Bank Transactions Acceptances", "xpath","//div[@class='navItem'][.='Process Bank Transactions Acceptances']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLPBATCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBATCH] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control GLPBATCH_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,GLPBATCH_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "GLPBATCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBATCH] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control GLPBATCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLPBATCH_MainForm);
IWebElement formBttn = GLPBATCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLPBATCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLPBATCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "GLPBATCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBATCH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLPBATCH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPBATCH_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Transaction No Non Contigouos Ranges");


												
				CPCommon.CurrentComponent = "GLPBATCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBATCH] Perfoming Click on TransactionNoNonContiguousRangesLink...", Logger.MessageType.INF);
			Control GLPBATCH_TransactionNoNonContiguousRangesLink = new Control("TransactionNoNonContiguousRangesLink", "ID", "lnk_3743_GLPBATCH_PARAM");
			CPCommon.WaitControlDisplayed(GLPBATCH_TransactionNoNonContiguousRangesLink);
GLPBATCH_TransactionNoNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLPBATCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBATCH] Perfoming VerifyExist on TransactionNoNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control GLPBATCH_TransactionNoNonContiguousRangesFormTable = new Control("TransactionNoNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRTRNNO_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPBATCH_TransactionNoNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPBATCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBATCH] Perfoming Close on TransactionNoNonContiguousRangesForm...", Logger.MessageType.INF);
			Control GLPBATCH_TransactionNoNonContiguousRangesForm = new Control("TransactionNoNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRTRNNO_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLPBATCH_TransactionNoNonContiguousRangesForm);
formBttn = GLPBATCH_TransactionNoNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "GLPBATCH";
							CPCommon.WaitControlDisplayed(GLPBATCH_MainForm);
formBttn = GLPBATCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

