 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MSQRCAP_SMOKE : TestScript
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
new Control("Master Production Scheduling", "xpath","//div[@class='deptItem'][.='Master Production Scheduling']").Click();
new Control("Rough-Cut Capacity Plan", "xpath","//div[@class='navItem'][.='Rough-Cut Capacity Plan']").Click();
new Control("View Rough-Cut Capacity Plan", "xpath","//div[@class='navItem'][.='View Rough-Cut Capacity Plan']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MSQRCAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSQRCAP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MSQRCAP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MSQRCAP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MSQRCAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSQRCAP] Perfoming VerifyExists on MainForm_MPSPlan...", Logger.MessageType.INF);
			Control MSQRCAP_MainForm_MPSPlan = new Control("MainForm_MPSPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MPS_PLAN']");
			CPCommon.AssertEqual(true,MSQRCAP_MainForm_MPSPlan.Exists());

											Driver.SessionLogger.WriteLine("KeyResourceLoad");


												
				CPCommon.CurrentComponent = "MSQRCAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSQRCAP] Perfoming VerifyExists on KeyResourceLoadForm...", Logger.MessageType.INF);
			Control MSQRCAP_KeyResourceLoadForm = new Control("KeyResourceLoadForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MSQRCAP_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MSQRCAP_KeyResourceLoadForm.Exists());

												
				CPCommon.CurrentComponent = "MSQRCAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSQRCAP] Perfoming VerifyExists on KeyResourceLoad_WeekEnding...", Logger.MessageType.INF);
			Control MSQRCAP_KeyResourceLoad_WeekEnding = new Control("KeyResourceLoad_WeekEnding", "xpath", "//div[translate(@id,'0123456789','')='pr__MSQRCAP_DTL_']/ancestor::form[1]/descendant::*[@id='WEEK_ENDING_DT']");
			CPCommon.AssertEqual(true,MSQRCAP_KeyResourceLoad_WeekEnding.Exists());

											Driver.SessionLogger.WriteLine("Key Resource");


												
				CPCommon.CurrentComponent = "MSQRCAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSQRCAP] Perfoming VerifyExists on KeyResourceLoad_KeyResourceLink...", Logger.MessageType.INF);
			Control MSQRCAP_KeyResourceLoad_KeyResourceLink = new Control("KeyResourceLoad_KeyResourceLink", "ID", "lnk_5121_MSQRCAP_DTL");
			CPCommon.AssertEqual(true,MSQRCAP_KeyResourceLoad_KeyResourceLink.Exists());

												
				CPCommon.CurrentComponent = "MSQRCAP";
							CPCommon.WaitControlDisplayed(MSQRCAP_KeyResourceLoad_KeyResourceLink);
MSQRCAP_KeyResourceLoad_KeyResourceLink.Click(1.5);


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You must query and select a record before performing this action.");


											Driver.SessionLogger.WriteLine("Weekly Detail");


												
				CPCommon.CurrentComponent = "MSQRCAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSQRCAP] Perfoming VerifyExists on KeyResourceLoad_WeeklyDetailLink...", Logger.MessageType.INF);
			Control MSQRCAP_KeyResourceLoad_WeeklyDetailLink = new Control("KeyResourceLoad_WeeklyDetailLink", "ID", "lnk_5129_MSQRCAP_DTL");
			CPCommon.AssertEqual(true,MSQRCAP_KeyResourceLoad_WeeklyDetailLink.Exists());

												
				CPCommon.CurrentComponent = "MSQRCAP";
							CPCommon.WaitControlDisplayed(MSQRCAP_KeyResourceLoad_WeeklyDetailLink);
MSQRCAP_KeyResourceLoad_WeeklyDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You must query and select a record before performing this action.");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "MSQRCAP";
							CPCommon.WaitControlDisplayed(MSQRCAP_MainForm);
IWebElement formBttn = MSQRCAP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

