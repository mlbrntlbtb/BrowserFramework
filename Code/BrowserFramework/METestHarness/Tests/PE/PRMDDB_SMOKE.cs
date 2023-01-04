 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMDDB_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Controls", "xpath","//div[@class='navItem'][.='Payroll Controls']").Click();
new Control("Manage Bank Routings", "xpath","//div[@class='navItem'][.='Manage Bank Routings']").Click();


												
				CPCommon.CurrentComponent = "PRMDDB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMDDB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMDDB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMDDB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMDDB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMDDB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMDDB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMDDB";
							CPCommon.WaitControlDisplayed(PRMDDB_MainForm);
IWebElement formBttn = PRMDDB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

