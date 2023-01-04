 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMMJLHS_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Multi-Job Processing", "xpath","//div[@class='navItem'][.='Multi-Job Processing']").Click();
new Control("Manage Multi-Job Allocation Labor History", "xpath","//div[@class='navItem'][.='Manage Multi-Job Allocation Labor History']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMMJLHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJLHS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMMJLHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMMJLHS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMMJLHS";
							CPCommon.WaitControlDisplayed(PJMMJLHS_MainForm);
IWebElement formBttn = PJMMJLHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMJLHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMJLHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJMMJLHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJLHS] Perfoming VerifyExists on AllocationCode...", Logger.MessageType.INF);
			Control PJMMJLHS_AllocationCode = new Control("AllocationCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ALLOC_CD']");
			CPCommon.AssertEqual(true,PJMMJLHS_AllocationCode.Exists());

												
				CPCommon.CurrentComponent = "PJMMJLHS";
							CPCommon.WaitControlDisplayed(PJMMJLHS_MainForm);
formBttn = PJMMJLHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMMJLHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMMJLHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMMJLHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJLHS] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMMJLHS_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMJLHS_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMJLHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJLHS] Perfoming VerifyExist on DetailsTable...", Logger.MessageType.INF);
			Control PJMMJLHS_DetailsTable = new Control("DetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMMJLHS_PROJALLOCLABHS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMJLHS_DetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMJLHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJLHS] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control PJMMJLHS_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMMJLHS_PROJALLOCLABHS_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMMJLHS_DetailsForm);
formBttn = PJMMJLHS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMJLHS_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMJLHS_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMMJLHS";
							CPCommon.AssertEqual(true,PJMMJLHS_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMMJLHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJLHS] Perfoming VerifyExists on Details_FiscalYearPeriod_FiscalYear...", Logger.MessageType.INF);
			Control PJMMJLHS_Details_FiscalYearPeriod_FiscalYear = new Control("Details_FiscalYearPeriod_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMMJLHS_PROJALLOCLABHS_CHLD_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJMMJLHS_Details_FiscalYearPeriod_FiscalYear.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMMJLHS";
							CPCommon.WaitControlDisplayed(PJMMJLHS_MainForm);
formBttn = PJMMJLHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

