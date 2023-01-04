 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMWACCT_SMOKE : TestScript
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
new Control("Configure Default Project Inventory Accounts", "xpath","//div[@class='navItem'][.='Configure Default Project Inventory Accounts']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "INMWACCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWACCT] Perfoming VerifyExists on AssetInventoryAbbreviations...", Logger.MessageType.INF);
			Control INMWACCT_AssetInventoryAbbreviations = new Control("AssetInventoryAbbreviations", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMWACCT_AssetInventoryAbbreviations.Exists());

												
				CPCommon.CurrentComponent = "INMWACCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMWACCT] Perfoming VerifyExists on AssetInventoryAbbreviations_RMVarAcctID...", Logger.MessageType.INF);
			Control INMWACCT_AssetInventoryAbbreviations_RMVarAcctID = new Control("AssetInventoryAbbreviations_RMVarAcctID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RM_VAR_ACCT_ID']");
			CPCommon.AssertEqual(true,INMWACCT_AssetInventoryAbbreviations_RMVarAcctID.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMWACCT";
							CPCommon.WaitControlDisplayed(INMWACCT_AssetInventoryAbbreviations);
IWebElement formBttn = INMWACCT_AssetInventoryAbbreviations.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

