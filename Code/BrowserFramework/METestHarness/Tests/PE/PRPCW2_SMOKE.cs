 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPCW2_SMOKE : TestScript
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
new Control("Year-End Processing", "xpath","//div[@class='navItem'][.='Year-End Processing']").Click();
new Control("Create W-2 Table", "xpath","//div[@class='navItem'][.='Create W-2 Table']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRPCW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCW2] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPCW2_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPCW2_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPCW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCW2] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRPCW2_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPCW2_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRPCW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCW2] Perfoming VerifyExists on EmployeeNonContigousRangesLink...", Logger.MessageType.INF);
			Control PRPCW2_EmployeeNonContigousRangesLink = new Control("EmployeeNonContigousRangesLink", "ID", "lnk_5584_PRPCW2_PARAM");
			CPCommon.AssertEqual(true,PRPCW2_EmployeeNonContigousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PRPCW2";
							CPCommon.WaitControlDisplayed(PRPCW2_EmployeeNonContigousRangesLink);
PRPCW2_EmployeeNonContigousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRPCW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCW2] Perfoming VerifyExists on EmployeeNonContigousRangesForm...", Logger.MessageType.INF);
			Control PRPCW2_EmployeeNonContigousRangesForm = new Control("EmployeeNonContigousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPCW2_EmployeeNonContigousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "PRPCW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCW2] Perfoming VerifyExist on EmployeeNonContigousRangesTable...", Logger.MessageType.INF);
			Control PRPCW2_EmployeeNonContigousRangesTable = new Control("EmployeeNonContigousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCREMPLID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPCW2_EmployeeNonContigousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "PRPCW2";
							CPCommon.WaitControlDisplayed(PRPCW2_EmployeeNonContigousRangesForm);
IWebElement formBttn = PRPCW2_EmployeeNonContigousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PRPCW2";
							CPCommon.WaitControlDisplayed(PRPCW2_MainForm);
formBttn = PRPCW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPCW2_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPCW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPCW2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCW2] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRPCW2_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPCW2_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRPCW2";
							CPCommon.WaitControlDisplayed(PRPCW2_MainForm);
formBttn = PRPCW2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

