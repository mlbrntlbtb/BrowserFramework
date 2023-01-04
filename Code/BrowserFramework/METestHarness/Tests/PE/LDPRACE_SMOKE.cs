 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPRACE_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee Utilities", "xpath","//div[@class='navItem'][.='Employee Utilities']").Click();
new Control("Convert Race/Ethnicity Codes to New Race Codes", "xpath","//div[@class='navItem'][.='Convert Race/Ethnicity Codes to New Race Codes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDPRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRACE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPRACE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPRACE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRACE] Perfoming VerifyExists on ConversionOptions_ApplicantRaceCodes...", Logger.MessageType.INF);
			Control LDPRACE_ConversionOptions_ApplicantRaceCodes = new Control("ConversionOptions_ApplicantRaceCodes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='APPL_FL']");
			CPCommon.AssertEqual(true,LDPRACE_ConversionOptions_ApplicantRaceCodes.Exists());

												
				CPCommon.CurrentComponent = "LDPRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRACE] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control LDPRACE_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPRACE_CONVERT_RACE_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDPRACE_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "LDPRACE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPRACE] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control LDPRACE_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDPRACE_CONVERT_RACE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPRACE_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDPRACE";
							CPCommon.WaitControlDisplayed(LDPRACE_MainForm);
IWebElement formBttn = LDPRACE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

