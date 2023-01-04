 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAPTOOL4_SMOKE : TestScript
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
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("Update the JE Required Flag in the Asset Audit Log", "xpath","//div[@class='navItem'][.='Update the JE Required Flag in the Asset Audit Log']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL4] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAPTOOL4_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAPTOOL4_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAPTOOL4";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAPTOOL4] Perfoming VerifyExists on Start_TimePeriods_FiscalYear...", Logger.MessageType.INF);
			Control FAPTOOL4_Start_TimePeriods_FiscalYear = new Control("Start_TimePeriods_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD_FR']");
			CPCommon.AssertEqual(true,FAPTOOL4_Start_TimePeriods_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "FAPTOOL4";
							CPCommon.WaitControlDisplayed(FAPTOOL4_MainForm);
IWebElement formBttn = FAPTOOL4_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

