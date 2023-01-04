 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MSMPLRT_SMOKE : TestScript
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
new Control("Planning Routings", "xpath","//div[@class='navItem'][.='Planning Routings']").Click();
new Control("Manage Planning Routings", "xpath","//div[@class='navItem'][.='Manage Planning Routings']").Click();


												
				CPCommon.CurrentComponent = "MSMPLRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMPLRT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MSMPLRT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MSMPLRT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MSMPLRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMPLRT] Perfoming VerifyExists on MainForm_Part...", Logger.MessageType.INF);
			Control MSMPLRT_MainForm_Part = new Control("MainForm_Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,MSMPLRT_MainForm_Part.Exists());

												
				CPCommon.CurrentComponent = "MSMPLRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMPLRT] Perfoming VerifyExist on PlanningRoutingsFormTable...", Logger.MessageType.INF);
			Control MSMPLRT_PlanningRoutingsFormTable = new Control("PlanningRoutingsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MSMPLRT_PARTPLNROUTING_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MSMPLRT_PlanningRoutingsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MSMPLRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMPLRT] Perfoming ClickButton on PlanningRoutingsForm...", Logger.MessageType.INF);
			Control MSMPLRT_PlanningRoutingsForm = new Control("PlanningRoutingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MSMPLRT_PARTPLNROUTING_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MSMPLRT_PlanningRoutingsForm);
IWebElement formBttn = MSMPLRT_PlanningRoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MSMPLRT_PlanningRoutingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MSMPLRT_PlanningRoutingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MSMPLRT";
							CPCommon.AssertEqual(true,MSMPLRT_PlanningRoutingsForm.Exists());

													
				CPCommon.CurrentComponent = "MSMPLRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSMPLRT] Perfoming VerifyExists on PlanningRoutings_Part...", Logger.MessageType.INF);
			Control MSMPLRT_PlanningRoutings_Part = new Control("PlanningRoutings_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__MSMPLRT_PARTPLNROUTING_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,MSMPLRT_PlanningRoutings_Part.Exists());

												
				CPCommon.CurrentComponent = "MSMPLRT";
							CPCommon.WaitControlDisplayed(MSMPLRT_MainForm);
formBttn = MSMPLRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

