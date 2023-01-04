 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMPROP_SMOKE : TestScript
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
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Security and Company Property", "xpath","//div[@class='navItem'][.='Security and Company Property']").Click();
new Control("Manage Company Property", "xpath","//div[@class='navItem'][.='Manage Company Property']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMPROP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMPROP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMPROP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMPROP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMPROP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMPROP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMPROP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMPROP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "HPMPROP";
							CPCommon.WaitControlDisplayed(HPMPROP_MainForm);
IWebElement formBttn = HPMPROP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

