 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPREALL_SMOKE : TestScript
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
new Control("Revenue Processing", "xpath","//div[@class='navItem'][.='Revenue Processing']").Click();
new Control("Redistribute Revenue by Project", "xpath","//div[@class='navItem'][.='Redistribute Revenue by Project']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJPREALL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPREALL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPREALL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPREALL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPREALL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPREALL] Perfoming VerifyExists on Options_OverrideVariance_Actual...", Logger.MessageType.INF);
			Control PJPREALL_Options_OverrideVariance_Actual = new Control("Options_OverrideVariance_Actual", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACT_VARIANCE']");
			CPCommon.AssertEqual(true,PJPREALL_Options_OverrideVariance_Actual.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJPREALL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPREALL] Perfoming VerifyExists on ProjectPercentagesLink...", Logger.MessageType.INF);
			Control PJPREALL_ProjectPercentagesLink = new Control("ProjectPercentagesLink", "ID", "lnk_2144_PJPREALL_PROCESS");
			CPCommon.AssertEqual(true,PJPREALL_ProjectPercentagesLink.Exists());

												
				CPCommon.CurrentComponent = "PJPREALL";
							CPCommon.WaitControlDisplayed(PJPREALL_ProjectPercentagesLink);
PJPREALL_ProjectPercentagesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJPREALL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPREALL] Perfoming VerifyExist on ProjectPercentagesFormTable...", Logger.MessageType.INF);
			Control PJPREALL_ProjectPercentagesFormTable = new Control("ProjectPercentagesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPREALL_PCTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPREALL_ProjectPercentagesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJPREALL";
							CPCommon.WaitControlDisplayed(PJPREALL_MainForm);
IWebElement formBttn = PJPREALL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

