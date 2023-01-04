 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDRAEDIT_SMOKE : TestScript
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
new Control("Timesheet Reporting", "xpath","//div[@class='navItem'][.='Timesheet Reporting']").Click();
new Control("Print Timesheet Information Report by Account", "xpath","//div[@class='navItem'][.='Print Timesheet Information Report by Account']").Click();


												
				CPCommon.CurrentComponent = "LDRAEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRAEDIT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDRAEDIT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDRAEDIT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDRAEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRAEDIT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDRAEDIT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDRAEDIT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDRAEDIT";
							CPCommon.WaitControlDisplayed(LDRAEDIT_MainForm);
IWebElement formBttn = LDRAEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDRAEDIT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDRAEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDRAEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRAEDIT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDRAEDIT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDRAEDIT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDRAEDIT";
							CPCommon.WaitControlDisplayed(LDRAEDIT_MainForm);
formBttn = LDRAEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

