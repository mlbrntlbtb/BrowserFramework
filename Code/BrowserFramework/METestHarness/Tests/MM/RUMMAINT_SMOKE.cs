 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMMAINT_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Routings Controls", "xpath","//div[@class='navItem'][.='Routings Controls']").Click();
new Control("Manage Maintenance Types", "xpath","//div[@class='navItem'][.='Manage Maintenance Types']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "RUMMAINT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMMAINT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RUMMAINT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMMAINT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMMAINT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMMAINT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control RUMMAINT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(RUMMAINT_MainForm);
IWebElement formBttn = RUMMAINT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMMAINT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMMAINT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMMAINT";
							CPCommon.AssertEqual(true,RUMMAINT_MainForm.Exists());

													
				CPCommon.CurrentComponent = "RUMMAINT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMMAINT] Perfoming VerifyExists on MaintenanceTypeCode...", Logger.MessageType.INF);
			Control RUMMAINT_MaintenanceTypeCode = new Control("MaintenanceTypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MAINT_TYPE_CD']");
			CPCommon.AssertEqual(true,RUMMAINT_MaintenanceTypeCode.Exists());

												
				CPCommon.CurrentComponent = "RUMMAINT";
							CPCommon.WaitControlDisplayed(RUMMAINT_MainForm);
formBttn = RUMMAINT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

