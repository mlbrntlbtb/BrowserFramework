 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPMRVSN_SMOKE : TestScript
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
new Control("Advanced Project Budgeting", "xpath","//div[@class='deptItem'][.='Advanced Project Budgeting']").Click();
new Control("Project Budget and Estimate To Complete", "xpath","//div[@class='navItem'][.='Project Budget and Estimate To Complete']").Click();
new Control("Manage Project Budget Revisions", "xpath","//div[@class='navItem'][.='Manage Project Budget Revisions']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BPMRVSN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMRVSN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPMRVSN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPMRVSN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPMRVSN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMRVSN] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BPMRVSN_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMRVSN_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BPMRVSN";
							CPCommon.WaitControlDisplayed(BPMRVSN_MainForm);
IWebElement formBttn = BPMRVSN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

