 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMMGMR_SMOKE : TestScript
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
new Control("Personnel Controls", "xpath","//div[@class='navItem'][.='Personnel Controls']").Click();
new Control("Manage Managers/HR Reps by Organization", "xpath","//div[@class='navItem'][.='Manage Managers/HR Reps by Organization']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMMGMR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMMGMR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMMGMR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMMGMR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMMGMR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMMGMR] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control HSMMGMR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(HSMMGMR_MainForm);
IWebElement formBttn = HSMMGMR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HSMMGMR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HSMMGMR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HSMMGMR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMMGMR] Perfoming VerifyExists on Organization...", Logger.MessageType.INF);
			Control HSMMGMR_Organization = new Control("Organization", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='HOME_ORG_ID']");
			CPCommon.AssertEqual(true,HSMMGMR_Organization.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMMGMR";
							CPCommon.WaitControlDisplayed(HSMMGMR_MainForm);
formBttn = HSMMGMR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

