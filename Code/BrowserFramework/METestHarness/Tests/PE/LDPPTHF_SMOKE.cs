 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPPTHF_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Administrative Utilities", "xpath","//div[@class='navItem'][.='Administrative Utilities']").Click();
new Control("Purge Timesheet History", "xpath","//div[@class='navItem'][.='Purge Timesheet History']").Click();


												
				CPCommon.CurrentComponent = "LDPPTHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPPTHF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPPTHF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPPTHF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPPTHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPPTHF] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPPTHF_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPPTHF_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPPTHF";
							CPCommon.WaitControlDisplayed(LDPPTHF_MainForm);
IWebElement formBttn = LDPPTHF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPPTHF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPPTHF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPPTHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPPTHF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDPPTHF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPPTHF_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDPPTHF";
							CPCommon.WaitControlDisplayed(LDPPTHF_MainForm);
formBttn = LDPPTHF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

