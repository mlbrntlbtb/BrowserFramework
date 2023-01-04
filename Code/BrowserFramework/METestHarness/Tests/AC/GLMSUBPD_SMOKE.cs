 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMSUBPD_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Company Calendar", "xpath","//div[@class='navItem'][.='Company Calendar']").Click();
new Control("Manage Subperiods", "xpath","//div[@class='navItem'][.='Manage Subperiods']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMSUBPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSUBPD] Perfoming ClickButtonIfExists on MainForm...", Logger.MessageType.INF);
			Control GLMSUBPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMSUBPD_MainForm);
IWebElement formBttn = GLMSUBPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? GLMSUBPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
GLMSUBPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Query] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "GLMSUBPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSUBPD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMSUBPD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSUBPD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMSUBPD";
							CPCommon.WaitControlDisplayed(GLMSUBPD_MainForm);
formBttn = GLMSUBPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMSUBPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMSUBPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMSUBPD";
							CPCommon.AssertEqual(true,GLMSUBPD_MainForm.Exists());

													
				CPCommon.CurrentComponent = "GLMSUBPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSUBPD] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control GLMSUBPD_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,GLMSUBPD_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("Child");


												
				CPCommon.CurrentComponent = "GLMSUBPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSUBPD] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLMSUBPD_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSUBPD_SUBPDJNLSTATUS_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMSUBPD_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLMSUBPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMSUBPD] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLMSUBPD_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMSUBPD_SUBPDJNLSTATUS_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMSUBPD_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "GLMSUBPD";
							CPCommon.WaitControlDisplayed(GLMSUBPD_MainForm);
formBttn = GLMSUBPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

