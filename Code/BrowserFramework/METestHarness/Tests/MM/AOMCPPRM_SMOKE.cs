 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMCPPRM_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Interfaces", "xpath","//div[@class='navItem'][.='Production Control Interfaces']").Click();
new Control("Configure Advanced Planning and Scheduling Parameters", "xpath","//div[@class='navItem'][.='Configure Advanced Planning and Scheduling Parameters']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOMCPPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCPPRM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMCPPRM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMCPPRM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMCPPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCPPRM] Perfoming VerifyExists on ScheduledReleaseDate_MOPlannedReleaseDate...", Logger.MessageType.INF);
			Control AOMCPPRM_ScheduledReleaseDate_MOPlannedReleaseDate = new Control("ScheduledReleaseDate_MOPlannedReleaseDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_REL_DT_CD' and @value='R']");
			CPCommon.AssertEqual(true,AOMCPPRM_ScheduledReleaseDate_MOPlannedReleaseDate.Exists());

												
				CPCommon.CurrentComponent = "AOMCPPRM";
							CPCommon.WaitControlDisplayed(AOMCPPRM_MainForm);
IWebElement formBttn = AOMCPPRM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

