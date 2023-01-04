 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMDEACT_SMOKE : TestScript
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
new Control("Fixed Assets Controls", "xpath","//div[@class='navItem'][.='Fixed Assets Controls']").Click();
new Control("Manage Depreciation Expense Acct Allocation Codes", "xpath","//div[@class='navItem'][.='Manage Depreciation Expense Acct Allocation Codes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMDEACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEACT] Perfoming VerifyExists on Code...", Logger.MessageType.INF);
			Control FAMDEACT_Code = new Control("Code", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DEPR_EXP_ALC_CD']");
			CPCommon.AssertEqual(true,FAMDEACT_Code.Exists());

												
				CPCommon.CurrentComponent = "FAMDEACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEACT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMDEACT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMDEACT_MainForm);
IWebElement formBttn = FAMDEACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMDEACT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMDEACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "FAMDEACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEACT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMDEACT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMDEACT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "FAMDEACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEACT] Perfoming VerifyExist on AllocationDetailsFormTable...", Logger.MessageType.INF);
			Control FAMDEACT_AllocationDetailsFormTable = new Control("AllocationDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDEACT_DEPREXPALCACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMDEACT_AllocationDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMDEACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEACT] Perfoming ClickButton on AllocationDetailsForm...", Logger.MessageType.INF);
			Control FAMDEACT_AllocationDetailsForm = new Control("AllocationDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDEACT_DEPREXPALCACCT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMDEACT_AllocationDetailsForm);
formBttn = FAMDEACT_AllocationDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMDEACT_AllocationDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMDEACT_AllocationDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMDEACT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEACT] Perfoming VerifyExists on AllocationDetails_Account...", Logger.MessageType.INF);
			Control FAMDEACT_AllocationDetails_Account = new Control("AllocationDetails_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDEACT_DEPREXPALCACCT_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,FAMDEACT_AllocationDetails_Account.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMDEACT";
							CPCommon.WaitControlDisplayed(FAMDEACT_MainForm);
formBttn = FAMDEACT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

