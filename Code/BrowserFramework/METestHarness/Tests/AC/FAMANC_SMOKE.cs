 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMANC_SMOKE : TestScript
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
new Control("Configure Default Autocreation Asset Numbering", "xpath","//div[@class='navItem'][.='Configure Default Autocreation Asset Numbering']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMANC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMANC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMANC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMANC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMANC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMANC] Perfoming VerifyExists on GenerateNewIncrementalAssetNumbersEachWithAnItemNumberOf00001ForEachAcceptedUnitInReceivingFromTheSamePO...", Logger.MessageType.INF);
			Control FAMANC_GenerateNewIncrementalAssetNumbersEachWithAnItemNumberOf00001ForEachAcceptedUnitInReceivingFromTheSamePO = new Control("GenerateNewIncrementalAssetNumbersEachWithAnItemNumberOf00001ForEachAcceptedUnitInReceivingFromTheSamePO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_RCV_ASSET_NO_CD' and @value='IAU']");
			CPCommon.AssertEqual(true,FAMANC_GenerateNewIncrementalAssetNumbersEachWithAnItemNumberOf00001ForEachAcceptedUnitInReceivingFromTheSamePO.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMANC";
							CPCommon.WaitControlDisplayed(FAMANC_MainForm);
IWebElement formBttn = FAMANC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

