 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMSUPV_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Manage Supervisors", "xpath","//div[@class='navItem'][.='Manage Supervisors']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PCMSUPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSUPV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCMSUPV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMSUPV_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PCMSUPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSUPV] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PCMSUPV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PCMSUPV_MainForm);
IWebElement formBttn = PCMSUPV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMSUPV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMSUPV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PCMSUPV";
							CPCommon.AssertEqual(true,PCMSUPV_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PCMSUPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSUPV] Perfoming VerifyExists on SupervisorID...", Logger.MessageType.INF);
			Control PCMSUPV_SupervisorID = new Control("SupervisorID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PC_SPRVR_ID']");
			CPCommon.AssertEqual(true,PCMSUPV_SupervisorID.Exists());

											Driver.SessionLogger.WriteLine("MainForm Close");


												
				CPCommon.CurrentComponent = "PCMSUPV";
							CPCommon.WaitControlDisplayed(PCMSUPV_MainForm);
formBttn = PCMSUPV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

