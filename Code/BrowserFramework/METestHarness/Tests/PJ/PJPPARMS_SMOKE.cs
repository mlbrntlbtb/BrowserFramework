 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPPARMS_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost and Revenue Processing Utilities", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Utilities']").Click();
new Control("Change Period Report Parameters", "xpath","//div[@class='navItem'][.='Change Period Report Parameters']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPPARMS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPPARMS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPPARMS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPPARMS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPPARMS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPPARMS] Perfoming VerifyExists on AccountingPeriodRangeOptions_SelectApplication_PrintProjectNonLaborReport...", Logger.MessageType.INF);
			Control PJPPARMS_AccountingPeriodRangeOptions_SelectApplication_PrintProjectNonLaborReport = new Control("AccountingPeriodRangeOptions_SelectApplication_PrintProjectNonLaborReport", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NON_LAB_RPT']");
			CPCommon.AssertEqual(true,PJPPARMS_AccountingPeriodRangeOptions_SelectApplication_PrintProjectNonLaborReport.Exists());

												
				CPCommon.CurrentComponent = "PJPPARMS";
							CPCommon.WaitControlDisplayed(PJPPARMS_MainForm);
IWebElement formBttn = PJPPARMS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

