 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPREVTS_SMOKE : TestScript
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
new Control("Timesheet Entry/Creation", "xpath","//div[@class='navItem'][.='Timesheet Entry/Creation']").Click();
new Control("Create Reversing Timesheets", "xpath","//div[@class='navItem'][.='Create Reversing Timesheets']").Click();


												
				CPCommon.CurrentComponent = "LDPREVTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPREVTS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPREVTS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPREVTS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPREVTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPREVTS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPREVTS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPREVTS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPREVTS";
							CPCommon.WaitControlDisplayed(LDPREVTS_MainForm);
IWebElement formBttn = LDPREVTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPREVTS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPREVTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPREVTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPREVTS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDPREVTS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPREVTS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDPREVTS";
							CPCommon.WaitControlDisplayed(LDPREVTS_MainForm);
formBttn = LDPREVTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

