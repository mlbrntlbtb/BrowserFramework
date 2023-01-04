 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSUF_SMOKE : TestScript
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
CP7Main_SearchApplications.SendKeys("AOMESSUF", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("QUERY");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "AOMESSUF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSUF] Perfoming VerifyExist on UserFlowStatusFormTable...", Logger.MessageType.INF);
			Control AOMESSUF_UserFlowStatusFormTable = new Control("UserFlowStatusFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSUF_ESSUFLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSUF_UserFlowStatusFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSUF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSUF] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control AOMESSUF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(AOMESSUF_MainForm);
IWebElement formBttn = AOMESSUF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMESSUF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMESSUF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMESSUF";
							CPCommon.AssertEqual(true,AOMESSUF_MainForm.Exists());

													
				CPCommon.CurrentComponent = "AOMESSUF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSUF] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control AOMESSUF_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,AOMESSUF_Employee.Exists());

											Driver.SessionLogger.WriteLine("USER FLOW STATUS FORM");


												
				CPCommon.CurrentComponent = "AOMESSUF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSUF] Perfoming VerifyExists on UserFlowStatusForm...", Logger.MessageType.INF);
			Control AOMESSUF_UserFlowStatusForm = new Control("UserFlowStatusForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSUF_ESSUFLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMESSUF_UserFlowStatusForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSUF";
							CPCommon.AssertEqual(true,AOMESSUF_UserFlowStatusFormTable.Exists());

												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMESSUF";
							CPCommon.WaitControlDisplayed(AOMESSUF_MainForm);
formBttn = AOMESSUF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

