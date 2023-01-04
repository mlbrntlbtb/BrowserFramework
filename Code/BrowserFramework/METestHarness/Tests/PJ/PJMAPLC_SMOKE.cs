 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMAPLC_SMOKE : TestScript
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
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Budget Controls", "xpath","//div[@class='navItem'][.='Budget Controls']").Click();
new Control("Manage Average PLC Rates", "xpath","//div[@class='navItem'][.='Manage Average PLC Rates']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "PJMAPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMAPLC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMAPLC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMAPLC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMAPLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMAPLC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMAPLC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMAPLC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMAPLC";
							CPCommon.WaitControlDisplayed(PJMAPLC_MainForm);
IWebElement formBttn = PJMAPLC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

