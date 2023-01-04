 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDQPMESH_SMOKE : TestScript
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
new Control("Admin", "xpath","//div[@class='busItem'][.='Admin']").Click();
new Control("System Administration", "xpath","//div[@class='deptItem'][.='System Administration']").Click();
new Control("System Administration Reports/Inquiries", "xpath","//div[@class='navItem'][.='System Administration Reports/Inquiries']").Click();
new Control("View Interface Execution Status History", "xpath","//div[@class='navItem'][.='View Interface Execution Status History']").Click();


											Driver.SessionLogger.WriteLine("Verify and Close");


												
				CPCommon.CurrentComponent = "LDQPMESH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQPMESH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDQPMESH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDQPMESH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDQPMESH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQPMESH] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control LDQPMESH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(LDQPMESH_MainForm);
IWebElement formBttn = LDQPMESH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

