 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMACCT_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Controls", "xpath","//div[@class='navItem'][.='Inventory Controls']").Click();
new Control("Configure Inventory Accounts", "xpath","//div[@class='navItem'][.='Configure Inventory Accounts']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "INMACCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMACCT] Perfoming VerifyExists on AdjustmentAccountOrgsForm...", Logger.MessageType.INF);
			Control INMACCT_AdjustmentAccountOrgsForm = new Control("AdjustmentAccountOrgsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMACCT_AdjustmentAccountOrgsForm.Exists());

												
				CPCommon.CurrentComponent = "INMACCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMACCT] Perfoming VerifyExists on AdjustmentAccountOrgs_AllocatedInventory_Account...", Logger.MessageType.INF);
			Control INMACCT_AdjustmentAccountOrgs_AllocatedInventory_Account = new Control("AdjustmentAccountOrgs_AllocatedInventory_Account", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VAR_PROJ_ACCT_ID']");
			CPCommon.AssertEqual(true,INMACCT_AdjustmentAccountOrgs_AllocatedInventory_Account.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMACCT";
							CPCommon.WaitControlDisplayed(INMACCT_AdjustmentAccountOrgsForm);
IWebElement formBttn = INMACCT_AdjustmentAccountOrgsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

