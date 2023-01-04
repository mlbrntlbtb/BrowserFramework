 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJM533PJ_SMOKE : TestScript
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
new Control("NASA 533s", "xpath","//div[@class='deptItem'][.='NASA 533s']").Click();
new Control("NASA 533 Processing", "xpath","//div[@class='navItem'][.='NASA 533 Processing']").Click();
new Control("Manage NASA 533 Project Information", "xpath","//div[@class='navItem'][.='Manage NASA 533 Project Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJM533PJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533PJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJM533PJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJM533PJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJM533PJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533PJ] Perfoming VerifyExists on ProjectID...", Logger.MessageType.INF);
			Control PJM533PJ_ProjectID = new Control("ProjectID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJM533PJ_ProjectID.Exists());

											Driver.SessionLogger.WriteLine("MAINFORMTABLE");


												
				CPCommon.CurrentComponent = "PJM533PJ";
							CPCommon.WaitControlDisplayed(PJM533PJ_MainForm);
IWebElement formBttn = PJM533PJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJM533PJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJM533PJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJM533PJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533PJ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJM533PJ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJM533PJ_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJM533PJ";
							CPCommon.WaitControlDisplayed(PJM533PJ_MainForm);
formBttn = PJM533PJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

