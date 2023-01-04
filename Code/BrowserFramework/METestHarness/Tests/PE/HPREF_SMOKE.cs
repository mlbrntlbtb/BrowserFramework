 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPREF_SMOKE : TestScript
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
new Control("Termination Processing", "xpath","//div[@class='navItem'][.='Termination Processing']").Click();
new Control("Print Completed Exit Interview Form", "xpath","//div[@class='navItem'][.='Print Completed Exit Interview Form']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPREF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPREF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPREF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPREF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREF] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HPREF_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HPREF_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HPREF";
							CPCommon.WaitControlDisplayed(HPREF_MainForm);
IWebElement formBttn = HPREF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPREF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPREF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPREF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPREF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPREF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("EMPLOYEE NON-CONTIGUOUS RANGES FORM");


												
				CPCommon.CurrentComponent = "HPREF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREF] Perfoming Click on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control HPREF_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_5358_HPREF_PARAM");
			CPCommon.WaitControlDisplayed(HPREF_EmployeeNonContiguousRangesLink);
HPREF_EmployeeNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HPREF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREF] Perfoming VerifyExist on EmployeeNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control HPREF_EmployeeNonContiguousRangesFormTable = new Control("EmployeeNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPREF_EmployeeNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPREF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPREF] Perfoming Click on EmployeeNonContiguousRanges_Ok...", Logger.MessageType.INF);
			Control HPREF_EmployeeNonContiguousRanges_Ok = new Control("EmployeeNonContiguousRanges_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(HPREF_EmployeeNonContiguousRanges_Ok);
if (HPREF_EmployeeNonContiguousRanges_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
HPREF_EmployeeNonContiguousRanges_Ok.Click(5,5);
else HPREF_EmployeeNonContiguousRanges_Ok.Click(4.5);


											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPREF";
							CPCommon.WaitControlDisplayed(HPREF_MainForm);
formBttn = HPREF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

