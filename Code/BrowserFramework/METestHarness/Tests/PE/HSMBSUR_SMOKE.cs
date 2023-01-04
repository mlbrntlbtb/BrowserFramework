 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMBSUR_SMOKE : TestScript
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
new Control("Manage Survey Companies", "xpath","//div[@class='navItem'][.='Manage Survey Companies']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMBSUR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMBSUR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMBSUR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMBSUR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMBSUR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMBSUR] Perfoming VerifyExists on SurveyCompanyID...", Logger.MessageType.INF);
			Control HSMBSUR_SurveyCompanyID = new Control("SurveyCompanyID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SURVEY_CO_ID']");
			CPCommon.AssertEqual(true,HSMBSUR_SurveyCompanyID.Exists());

												
				CPCommon.CurrentComponent = "HSMBSUR";
							CPCommon.WaitControlDisplayed(HSMBSUR_MainForm);
IWebElement formBttn = HSMBSUR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSMBSUR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSMBSUR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSMBSUR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMBSUR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMBSUR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMBSUR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMBSUR";
							CPCommon.WaitControlDisplayed(HSMBSUR_MainForm);
formBttn = HSMBSUR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

