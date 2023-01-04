 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMQPJAP_SMOKE : TestScript
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
new Control("Approve Projects from Templates", "xpath","//div[@class='navItem'][.='Approve Projects from Templates']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMQPJAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPJAP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMQPJAP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMQPJAP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMQPJAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPJAP] Perfoming VerifyExists on Approve...", Logger.MessageType.INF);
			Control PJMQPJAP_Approve = new Control("Approve", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'PBAPPROVESEL') and contains(@style,'visible')]");
			CPCommon.AssertEqual(true,PJMQPJAP_Approve.Exists());

												
				CPCommon.CurrentComponent = "PJMQPJAP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPJAP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMQPJAP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMQPJAP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMQPJAP";
							CPCommon.WaitControlDisplayed(PJMQPJAP_MainForm);
IWebElement formBttn = PJMQPJAP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

