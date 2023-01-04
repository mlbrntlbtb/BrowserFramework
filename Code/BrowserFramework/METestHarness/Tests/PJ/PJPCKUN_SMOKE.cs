 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCKUN_SMOKE : TestScript
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
new Control("Check for Unposted Journals", "xpath","//div[@class='navItem'][.='Check for Unposted Journals']").Click();


											Driver.SessionLogger.WriteLine("Verify MainForm");


												
				CPCommon.CurrentComponent = "PJPCKUN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKUN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPCKUN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCKUN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCKUN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKUN] Perfoming VerifyExists on SelectSubperiod_AccountingPeriod...", Logger.MessageType.INF);
			Control PJPCKUN_SelectSubperiod_AccountingPeriod = new Control("SelectSubperiod_AccountingPeriod", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_RANGE']");
			CPCommon.AssertEqual(true,PJPCKUN_SelectSubperiod_AccountingPeriod.Exists());

											Driver.SessionLogger.WriteLine("Set Fiscalyear (for DEFECT #471661)");


												
				CPCommon.CurrentComponent = "PJPCKUN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKUN] Perfoming Set on SelectSubperiod_FiscalYear_FiscalYear...", Logger.MessageType.INF);
			Control PJPCKUN_SelectSubperiod_FiscalYear_FiscalYear = new Control("SelectSubperiod_FiscalYear_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			PJPCKUN_SelectSubperiod_FiscalYear_FiscalYear.Click();
PJPCKUN_SelectSubperiod_FiscalYear_FiscalYear.SendKeys("S2084", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJPCKUN_SelectSubperiod_FiscalYear_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "PJPCKUN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKUN] Perfoming Set on SelectSubperiod_Period_Period...", Logger.MessageType.INF);
			Control PJPCKUN_SelectSubperiod_Period_Period = new Control("SelectSubperiod_Period_Period", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PD_NO']");
			PJPCKUN_SelectSubperiod_Period_Period.Click();
PJPCKUN_SelectSubperiod_Period_Period.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJPCKUN_SelectSubperiod_Period_Period.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "PJPCKUN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCKUN] Perfoming Set on SelectSubperiod_Subpd_Subpd...", Logger.MessageType.INF);
			Control PJPCKUN_SelectSubperiod_Subpd_Subpd = new Control("SelectSubperiod_Subpd_Subpd", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SUB_PD_NO']");
			PJPCKUN_SelectSubperiod_Subpd_Subpd.Click();
PJPCKUN_SelectSubperiod_Subpd_Subpd.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJPCKUN_SelectSubperiod_Subpd_Subpd.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											Driver.SessionLogger.WriteLine("Check for Open Journal (Action Button)");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Check for Open Journals')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Check for Open Journals.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Verify record");


												
				CPCommon.CurrentComponent = "PJPCKUN";
							CPCommon.AssertEqual("S2084",PJPCKUN_SelectSubperiod_FiscalYear_FiscalYear.GetAttributeValue("value"));


													
				CPCommon.CurrentComponent = "PJPCKUN";
							CPCommon.AssertEqual("1",PJPCKUN_SelectSubperiod_Period_Period.GetAttributeValue("value"));


													
				CPCommon.CurrentComponent = "PJPCKUN";
							CPCommon.AssertEqual("1",PJPCKUN_SelectSubperiod_Subpd_Subpd.GetAttributeValue("value"));


												Driver.SessionLogger.WriteLine("Close the app");


												
				CPCommon.CurrentComponent = "PJPCKUN";
							CPCommon.WaitControlDisplayed(PJPCKUN_MainForm);
IWebElement formBttn = PJPCKUN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

