 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMQWBS_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Master", "xpath","//div[@class='navItem'][.='Project Master']").Click();
new Control("Create Projects from Templates", "xpath","//div[@class='navItem'][.='Create Projects from Templates']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMQWBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQWBS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMQWBS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMQWBS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMQWBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQWBS] Perfoming VerifyExists on TemplateID...", Logger.MessageType.INF);
			Control PJMQWBS_TemplateID = new Control("TemplateID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TEMPLATE_ID']");
			CPCommon.AssertEqual(true,PJMQWBS_TemplateID.Exists());

											Driver.SessionLogger.WriteLine("Project Levels Setup");


												
				CPCommon.CurrentComponent = "PJMQWBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQWBS] Perfoming VerifyExist on ProjectLevelsSetupFormTable...", Logger.MessageType.INF);
			Control PJMQWBS_ProjectLevelsSetupFormTable = new Control("ProjectLevelsSetupFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMQWBS_LVLS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMQWBS_ProjectLevelsSetupFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMQWBS";
							CPCommon.WaitControlDisplayed(PJMQWBS_MainForm);
IWebElement formBttn = PJMQWBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

