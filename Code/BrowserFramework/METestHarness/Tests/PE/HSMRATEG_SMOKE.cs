 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMRATEG_SMOKE : TestScript
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
new Control("Manage Performance Ratings by Plan and Grade", "xpath","//div[@class='navItem'][.='Manage Performance Ratings by Plan and Grade']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMRATEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMRATEG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMRATEG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMRATEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEG] Perfoming VerifyExists on CompensationPlan...", Logger.MessageType.INF);
			Control HSMRATEG_CompensationPlan = new Control("CompensationPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFSCOMPPLAN']");
			CPCommon.AssertEqual(true,HSMRATEG_CompensationPlan.Exists());

												
				CPCommon.CurrentComponent = "HSMRATEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEG] Perfoming VerifyExist on CompensationPlanInformationFormTable...", Logger.MessageType.INF);
			Control HSMRATEG_CompensationPlanInformationFormTable = new Control("CompensationPlanInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMRATEG_RATINGBYGRADE_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMRATEG_CompensationPlanInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMRATEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEG] Perfoming ClickButton on CompensationPlanInformationForm...", Logger.MessageType.INF);
			Control HSMRATEG_CompensationPlanInformationForm = new Control("CompensationPlanInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMRATEG_RATINGBYGRADE_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HSMRATEG_CompensationPlanInformationForm);
IWebElement formBttn = HSMRATEG_CompensationPlanInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMRATEG_CompensationPlanInformationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMRATEG_CompensationPlanInformationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMRATEG";
							CPCommon.AssertEqual(true,HSMRATEG_CompensationPlanInformationForm.Exists());

													
				CPCommon.CurrentComponent = "HSMRATEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMRATEG] Perfoming VerifyExists on CompensationPlanInformation_CompensationPlan...", Logger.MessageType.INF);
			Control HSMRATEG_CompensationPlanInformation_CompensationPlan = new Control("CompensationPlanInformation_CompensationPlan", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMRATEG_RATINGBYGRADE_CHLD_']/ancestor::form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,HSMRATEG_CompensationPlanInformation_CompensationPlan.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMRATEG";
							CPCommon.WaitControlDisplayed(HSMRATEG_MainForm);
formBttn = HSMRATEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

