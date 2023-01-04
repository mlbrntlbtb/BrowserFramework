 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBPAPDED_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Processing", "xpath","//div[@class='navItem'][.='Payroll Processing']").Click();
new Control("Update Employee Package Deductions", "xpath","//div[@class='navItem'][.='Update Employee Package Deductions']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HBPAPDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBPAPDED] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBPAPDED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBPAPDED_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBPAPDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBPAPDED] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HBPAPDED_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HBPAPDED_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HBPAPDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBPAPDED] Perfoming VerifyExists on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control HBPAPDED_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_15284_HBPAPDED_PARAM");
			CPCommon.AssertEqual(true,HBPAPDED_EmployeeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "HBPAPDED";
							CPCommon.WaitControlDisplayed(HBPAPDED_EmployeeNonContiguousRangesLink);
HBPAPDED_EmployeeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HBPAPDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBPAPDED] Perfoming VerifyExists on EmployeeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control HBPAPDED_EmployeeNonContiguousRangesForm = new Control("EmployeeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBPAPDED_EmployeeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "HBPAPDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBPAPDED] Perfoming VerifyExist on EmployeeNonContiguousRangesTable...", Logger.MessageType.INF);
			Control HBPAPDED_EmployeeNonContiguousRangesTable = new Control("EmployeeNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBPAPDED_EmployeeNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "HBPAPDED";
							CPCommon.WaitControlDisplayed(HBPAPDED_EmployeeNonContiguousRangesForm);
IWebElement formBttn = HBPAPDED_EmployeeNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "HBPAPDED";
							CPCommon.WaitControlDisplayed(HBPAPDED_MainForm);
formBttn = HBPAPDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBPAPDED_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBPAPDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBPAPDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBPAPDED] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBPAPDED_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBPAPDED_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "HBPAPDED";
							CPCommon.WaitControlDisplayed(HBPAPDED_MainForm);
formBttn = HBPAPDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

