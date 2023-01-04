 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPAIPRJ_SMOKE : TestScript
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
new Control("Project Setup Utilities", "xpath","//div[@class='navItem'][.='Project Setup Utilities']").Click();
new Control("Activate/Inactivate Projects", "xpath","//div[@class='navItem'][.='Activate/Inactivate Projects']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPAIPRJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPAIPRJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPAIPRJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPAIPRJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPAIPRJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPAIPRJ] Perfoming VerifyExists on Option...", Logger.MessageType.INF);
			Control PJPAIPRJ_Option = new Control("Option", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_RANGE_CD']");
			CPCommon.AssertEqual(true,PJPAIPRJ_Option.Exists());

												
				CPCommon.CurrentComponent = "PJPAIPRJ";
							CPCommon.WaitControlDisplayed(PJPAIPRJ_MainForm);
IWebElement formBttn = PJPAIPRJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

