 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPPTRNE_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Basic Employee Information", "xpath","//div[@class='navItem'][.='Basic Employee Information']").Click();
new Control("Update Employee Training", "xpath","//div[@class='navItem'][.='Update Employee Training']").Click();


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "HPPTRNE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPPTRNE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPPTRNE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPPTRNE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPPTRNE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPPTRNE] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HPPTRNE_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HPPTRNE_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("Table");


												
				CPCommon.CurrentComponent = "HPPTRNE";
							CPCommon.WaitControlDisplayed(HPPTRNE_MainForm);
IWebElement formBttn = HPPTRNE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPPTRNE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPPTRNE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPPTRNE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPPTRNE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPPTRNE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPPTRNE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Employee Non- Contiguous Ranges");


												
				CPCommon.CurrentComponent = "HPPTRNE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPPTRNE] Perfoming VerifyExists on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control HPPTRNE_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_1004630_HPPTRNE_PARAM");
			CPCommon.AssertEqual(true,HPPTRNE_EmployeeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "HPPTRNE";
							CPCommon.WaitControlDisplayed(HPPTRNE_EmployeeNonContiguousRangesLink);
HPPTRNE_EmployeeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPPTRNE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPPTRNE] Perfoming VerifyExist on EmployeeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control HPPTRNE_EmployeeNonContiguousRangesFormTable = new Control("EmployeeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPPTRNE_EmployeeNonContiguousRangesFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "HPPTRNE";
							CPCommon.WaitControlDisplayed(HPPTRNE_MainForm);
formBttn = HPPTRNE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

