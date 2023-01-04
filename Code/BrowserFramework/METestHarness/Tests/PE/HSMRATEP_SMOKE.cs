 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMRATEP_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Performance Reviews", "xpath","//div[@class='navItem'][.='Performance Reviews']").Click();
new Control("Manage Performance Ratings by Plan", "xpath","//div[@class='navItem'][.='Manage Performance Ratings by Plan']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMRATEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMRATEP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMRATEP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMRATEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEP] Perfoming VerifyExists on Options_CompensationPlan...", Logger.MessageType.INF);
			Control HSMRATEP_Options_CompensationPlan = new Control("Options_CompensationPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFSCOMPPLAN']");
			CPCommon.AssertEqual(true,HSMRATEP_Options_CompensationPlan.Exists());

												
				CPCommon.CurrentComponent = "HSMRATEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control HSMRATEP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMRATEP_RATINGBYPLAN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMRATEP_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMRATEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEP] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control HSMRATEP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMRATEP_RATINGBYPLAN_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HSMRATEP_ChildForm);
IWebElement formBttn = HSMRATEP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMRATEP_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMRATEP_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMRATEP";
							CPCommon.AssertEqual(true,HSMRATEP_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "HSMRATEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEP] Perfoming VerifyExists on CompensationPlanInformation_CompensationPlan...", Logger.MessageType.INF);
			Control HSMRATEP_CompensationPlanInformation_CompensationPlan = new Control("CompensationPlanInformation_CompensationPlan", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMRATEP_RATINGBYPLAN_CHLD_']/ancestor::form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,HSMRATEP_CompensationPlanInformation_CompensationPlan.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMRATEP";
							CPCommon.WaitControlDisplayed(HSMRATEP_MainForm);
formBttn = HSMRATEP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

