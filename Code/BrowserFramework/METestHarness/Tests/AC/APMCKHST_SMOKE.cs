 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMCKHST_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Utilities", "xpath","//div[@class='navItem'][.='Accounts Payable Utilities']").Click();
new Control("Manage Check History", "xpath","//div[@class='navItem'][.='Manage Check History']").Click();


												
				CPCommon.CurrentComponent = "APMCKHST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCKHST] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APMCKHST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APMCKHST_MainForm);
IWebElement formBttn = APMCKHST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? APMCKHST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
APMCKHST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "APMCKHST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCKHST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMCKHST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMCKHST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMCKHST";
							CPCommon.WaitControlDisplayed(APMCKHST_MainForm);
formBttn = APMCKHST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMCKHST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMCKHST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "APMCKHST";
							CPCommon.AssertEqual(true,APMCKHST_MainForm.Exists());

													
				CPCommon.CurrentComponent = "APMCKHST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCKHST] Perfoming VerifyExists on CheckNumber...", Logger.MessageType.INF);
			Control APMCKHST_CheckNumber = new Control("CheckNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CHK_NO']");
			CPCommon.AssertEqual(true,APMCKHST_CheckNumber.Exists());

												
				CPCommon.CurrentComponent = "APMCKHST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCKHST] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control APMCKHST_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCKHST_VENDCHKVCHR_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMCKHST_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APMCKHST";
							CPCommon.WaitControlDisplayed(APMCKHST_MainForm);
formBttn = APMCKHST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

