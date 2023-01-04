 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSAL_SMOKE : TestScript
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

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Set on SearchApplications...", Logger.MessageType.INF);
			Control CP7Main_SearchApplications = new Control("SearchApplications", "ID", "appFltrFld");
			CP7Main_SearchApplications.Click();
CP7Main_SearchApplications.SendKeys("AOMESSAL", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOMESSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSAL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSAL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSAL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSAL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control AOMESSAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(AOMESSAL_MainForm);
IWebElement formBttn = AOMESSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMESSAL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMESSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMESSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSAL] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control AOMESSAL_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,AOMESSAL_Employee.Exists());

												
				CPCommon.CurrentComponent = "AOMESSAL";
							CPCommon.AssertEqual(true,AOMESSAL_MainForm.Exists());

												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMESSAL";
							CPCommon.WaitControlDisplayed(AOMESSAL_MainForm);
formBttn = AOMESSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

