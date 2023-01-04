 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMMJCHS_SMOKE : TestScript
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
new Control("Manage Multi-Job Allocation Cost History", "xpath","//div[@class='navItem'][.='Manage Multi-Job Allocation Cost History']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMMJCHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJCHS] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMMJCHS_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMJCHS_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PJMMJCHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJCHS] Perfoming VerifyExist on AllocationCostDetailsTable...", Logger.MessageType.INF);
			Control PJMMJCHS_AllocationCostDetailsTable = new Control("AllocationCostDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMMJCHS_PROJALLOCHS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMMJCHS_AllocationCostDetailsTable.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJMMJCHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJCHS] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMMJCHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMMJCHS_MainForm);
IWebElement formBttn = PJMMJCHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMJCHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMJCHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMMJCHS";
							CPCommon.AssertEqual(true,PJMMJCHS_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMMJCHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJCHS] Perfoming VerifyExists on AllocationCode...", Logger.MessageType.INF);
			Control PJMMJCHS_AllocationCode = new Control("AllocationCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ALLOC_CD']");
			CPCommon.AssertEqual(true,PJMMJCHS_AllocationCode.Exists());

												
				CPCommon.CurrentComponent = "PJMMJCHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJCHS] Perfoming ClickButton on AllocationCostDetailsForm...", Logger.MessageType.INF);
			Control PJMMJCHS_AllocationCostDetailsForm = new Control("AllocationCostDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMMJCHS_PROJALLOCHS_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMMJCHS_AllocationCostDetailsForm);
formBttn = PJMMJCHS_AllocationCostDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMMJCHS_AllocationCostDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMMJCHS_AllocationCostDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMMJCHS";
							CPCommon.AssertEqual(true,PJMMJCHS_AllocationCostDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMMJCHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMMJCHS] Perfoming VerifyExists on AllocationCostDetails_POA_Project...", Logger.MessageType.INF);
			Control PJMMJCHS_AllocationCostDetails_POA_Project = new Control("AllocationCostDetails_POA_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMMJCHS_PROJALLOCHS_CHLD_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMMJCHS_AllocationCostDetails_POA_Project.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMMJCHS";
							CPCommon.WaitControlDisplayed(PJMMJCHS_MainForm);
formBttn = PJMMJCHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

