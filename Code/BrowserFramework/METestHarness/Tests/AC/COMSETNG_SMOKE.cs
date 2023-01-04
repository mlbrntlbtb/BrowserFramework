 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COMSETNG_SMOKE : TestScript
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
new Control("Consolidations", "xpath","//div[@class='deptItem'][.='Consolidations']").Click();
new Control("Consolidations Controls", "xpath","//div[@class='navItem'][.='Consolidations Controls']").Click();
new Control("Configure Consolidation Settings", "xpath","//div[@class='navItem'][.='Configure Consolidation Settings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "COMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSETNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control COMSETNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,COMSETNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "COMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSETNG] Perfoming VerifyExists on ConsolidationCalculationMethod_YearToDate...", Logger.MessageType.INF);
			Control COMSETNG_ConsolidationCalculationMethod_YearToDate = new Control("ConsolidationCalculationMethod_YearToDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CONS_CALC_CD' and @value='Y']");
			CPCommon.AssertEqual(true,COMSETNG_ConsolidationCalculationMethod_YearToDate.Exists());

												
				CPCommon.CurrentComponent = "COMSETNG";
							CPCommon.WaitControlDisplayed(COMSETNG_MainForm);
IWebElement formBttn = COMSETNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

