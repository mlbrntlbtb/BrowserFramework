 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMSURCP_SMOKE : TestScript
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
new Control("Salary Surveys", "xpath","//div[@class='navItem'][.='Salary Surveys']").Click();
new Control("Manage Survey Region Mappings", "xpath","//div[@class='navItem'][.='Manage Survey Region Mappings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMSURCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSURCP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMSURCP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMSURCP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSURCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSURCP] Perfoming VerifyExists on SurveyCompany...", Logger.MessageType.INF);
			Control HSMSURCP_SurveyCompany = new Control("SurveyCompany", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SURVEY_CO_ID']");
			CPCommon.AssertEqual(true,HSMSURCP_SurveyCompany.Exists());

												
				CPCommon.CurrentComponent = "HSMSURCP";
							CPCommon.WaitControlDisplayed(HSMSURCP_MainForm);
IWebElement formBttn = HSMSURCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSMSURCP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSMSURCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSMSURCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSURCP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMSURCP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMSURCP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMSURCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSURCP] Perfoming VerifyExists on SurveyRegionCompensationPlanForm...", Logger.MessageType.INF);
			Control HSMSURCP_SurveyRegionCompensationPlanForm = new Control("SurveyRegionCompensationPlanForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMSURCP_SURVEYCOMPPLANCD_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMSURCP_SurveyRegionCompensationPlanForm.Exists());

												
				CPCommon.CurrentComponent = "HSMSURCP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMSURCP] Perfoming VerifyExist on SurveyRegionCompensationPlanFormTable...", Logger.MessageType.INF);
			Control HSMSURCP_SurveyRegionCompensationPlanFormTable = new Control("SurveyRegionCompensationPlanFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMSURCP_SURVEYCOMPPLANCD_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMSURCP_SurveyRegionCompensationPlanFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "HSMSURCP";
							CPCommon.WaitControlDisplayed(HSMSURCP_MainForm);
formBttn = HSMSURCP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

