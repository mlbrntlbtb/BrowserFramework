 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPCPR_SMOKE : TestScript
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
new Control("Compute Payroll", "xpath","//div[@class='navItem'][.='Compute Payroll']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRPCPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCPR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPCPR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPCPR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPCPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCPR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRPCPR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPCPR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRPCPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCPR] Perfoming VerifyExists on EmployeeNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PRPCPR_EmployeeNonContiguousRangesLink = new Control("EmployeeNonContiguousRangesLink", "ID", "lnk_15467_PRPCPR_PROCESS");
			CPCommon.AssertEqual(true,PRPCPR_EmployeeNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRPCPR";
							CPCommon.WaitControlDisplayed(PRPCPR_EmployeeNonContiguousRangesLink);
PRPCPR_EmployeeNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRPCPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCPR] Perfoming VerifyExists on EmployeeNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PRPCPR_EmployeeNonContiguousRangesForm = new Control("EmployeeNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPCPR_EmployeeNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRPCPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCPR] Perfoming VerifyExist on EmployeeNonContiguousRangesTable...", Logger.MessageType.INF);
			Control PRPCPR_EmployeeNonContiguousRangesTable = new Control("EmployeeNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPCPR_EmployeeNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRPCPR";
							CPCommon.WaitControlDisplayed(PRPCPR_EmployeeNonContiguousRangesForm);
IWebElement formBttn = PRPCPR_EmployeeNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PRPCPR";
							CPCommon.WaitControlDisplayed(PRPCPR_MainForm);
formBttn = PRPCPR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPCPR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPCPR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPCPR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCPR] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRPCPR_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPCPR_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRPCPR";
							CPCommon.WaitControlDisplayed(PRPCPR_MainForm);
formBttn = PRPCPR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

