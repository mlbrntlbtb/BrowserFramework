 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPMADD_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Master", "xpath","//div[@class='navItem'][.='Project Master']").Click();
new Control("Mass Add Project Master Data", "xpath","//div[@class='navItem'][.='Mass Add Project Master Data']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJPMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPMADD] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJPMADD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJPMADD_MainForm);
IWebElement formBttn = PJPMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJPMADD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJPMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "PJPMADD";
							CPCommon.WaitControlDisplayed(PJPMADD_MainForm);
formBttn = PJPMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJPMADD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJPMADD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJPMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPMADD] Perfoming Click on Autoload...", Logger.MessageType.INF);
			Control PJPMADD_Autoload = new Control("Autoload", "xpath", "//div[@id='0']/form[1]/descendant::*[contains(@id,'FILL_TABLE') and contains(@style,'visible')]");
			CPCommon.WaitControlDisplayed(PJPMADD_Autoload);
if (PJPMADD_Autoload.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PJPMADD_Autoload.Click(5,5);
else PJPMADD_Autoload.Click(4.5);


												
				CPCommon.CurrentComponent = "PJPMADD";
							CPCommon.AssertEqual(true,PJPMADD_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJPMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPMADD] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJPMADD_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJPMADD_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJPMADD";
							CPCommon.WaitControlDisplayed(PJPMADD_MainForm);
formBttn = PJPMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJPMADD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJPMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJPMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPMADD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJPMADD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPMADD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DETAILS");


												
				CPCommon.CurrentComponent = "PJPMADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPMADD] Perfoming VerifyExist on DetailsTable...", Logger.MessageType.INF);
			Control PJPMADD_DetailsTable = new Control("DetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPMADD_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPMADD_DetailsTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJPMADD";
							CPCommon.WaitControlDisplayed(PJPMADD_MainForm);
formBttn = PJPMADD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

