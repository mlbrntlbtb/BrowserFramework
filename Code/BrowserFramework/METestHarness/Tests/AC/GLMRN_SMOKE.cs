 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMRN_SMOKE : TestScript
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
new Control("Reference Numbers", "xpath","//div[@class='navItem'][.='Reference Numbers']").Click();
new Control("Manage Reference Elements", "xpath","//div[@class='navItem'][.='Manage Reference Elements']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMRN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMRN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExists on ReferenceNo...", Logger.MessageType.INF);
			Control GLMRN_ReferenceNo = new Control("ReferenceNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='REF_STRUC_ID']");
			CPCommon.AssertEqual(true,GLMRN_ReferenceNo.Exists());

												
				CPCommon.CurrentComponent = "GLMRN";
							CPCommon.WaitControlDisplayed(GLMRN_MainForm);
IWebElement formBttn = GLMRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMRN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMRN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Link to accounts");


												
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExists on LinkToAcctOrgsLink...", Logger.MessageType.INF);
			Control GLMRN_LinkToAcctOrgsLink = new Control("LinkToAcctOrgsLink", "ID", "lnk_1000787_GLMRN_REFSTRUC_HDR");
			CPCommon.AssertEqual(true,GLMRN_LinkToAcctOrgsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMRN";
							CPCommon.WaitControlDisplayed(GLMRN_LinkToAcctOrgsLink);
GLMRN_LinkToAcctOrgsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExists on LinkToAcctOrgsForm...", Logger.MessageType.INF);
			Control GLMRN_LinkToAcctOrgsForm = new Control("LinkToAcctOrgsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRN_ORGACCTREFSTRUC_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMRN_LinkToAcctOrgsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExist on LinkToAcctOrgsFormTable...", Logger.MessageType.INF);
			Control GLMRN_LinkToAcctOrgsFormTable = new Control("LinkToAcctOrgsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRN_ORGACCTREFSTRUC_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRN_LinkToAcctOrgsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRN] Perfoming VerifyExists on LinkToAcctOrgs_Ok...", Logger.MessageType.INF);
			Control GLMRN_LinkToAcctOrgs_Ok = new Control("LinkToAcctOrgs_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRN_ORGACCTREFSTRUC_TBL_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,GLMRN_LinkToAcctOrgs_Ok.Exists());

												
				CPCommon.CurrentComponent = "GLMRN";
							CPCommon.WaitControlDisplayed(GLMRN_LinkToAcctOrgsForm);
formBttn = GLMRN_LinkToAcctOrgsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "GLMRN";
							CPCommon.WaitControlDisplayed(GLMRN_MainForm);
formBttn = GLMRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

