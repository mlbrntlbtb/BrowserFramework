 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSRB_SMOKE : TestScript
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
CP7Main_SearchApplications.SendKeys("AOMESSRB", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOMESSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSRB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMESSRB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMESSRB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSRB] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control AOMESSRB_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,AOMESSRB_Employee.Exists());

												
				CPCommon.CurrentComponent = "AOMESSRB";
							CPCommon.WaitControlDisplayed(AOMESSRB_MainForm);
IWebElement formBttn = AOMESSRB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSRB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSRB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSRB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSRB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSRB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSRB_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMESSRB";
							CPCommon.WaitControlDisplayed(AOMESSRB_MainForm);
formBttn = AOMESSRB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

