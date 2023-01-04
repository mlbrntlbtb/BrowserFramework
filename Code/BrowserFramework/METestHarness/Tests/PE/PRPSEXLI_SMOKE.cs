 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPSEXLI_SMOKE : TestScript
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
new Control("Update Excess Life Deductions", "xpath","//div[@class='navItem'][.='Update Excess Life Deductions']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRPSEXLI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPSEXLI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPSEXLI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPSEXLI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPSEXLI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPSEXLI] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRPSEXLI_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPSEXLI_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRPSEXLI";
							CPCommon.WaitControlDisplayed(PRPSEXLI_MainForm);
IWebElement formBttn = PRPSEXLI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPSEXLI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPSEXLI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPSEXLI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPSEXLI] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRPSEXLI_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPSEXLI_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRPSEXLI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPSEXLI] Perfoming VerifyExists on FactorsInsuranceAmountsBySalaryRangeForm...", Logger.MessageType.INF);
			Control PRPSEXLI_FactorsInsuranceAmountsBySalaryRangeForm = new Control("FactorsInsuranceAmountsBySalaryRangeForm", "xpath", "//div[starts-with(@id,'pr__PRPSEXLI_PARAM_2_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPSEXLI_FactorsInsuranceAmountsBySalaryRangeForm.Exists());

												
				CPCommon.CurrentComponent = "PRPSEXLI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPSEXLI] Perfoming VerifyExist on FactorsInsuranceAmountsBySalaryRangeFormTable...", Logger.MessageType.INF);
			Control PRPSEXLI_FactorsInsuranceAmountsBySalaryRangeFormTable = new Control("FactorsInsuranceAmountsBySalaryRangeFormTable", "xpath", "//div[starts-with(@id,'pr__PRPSEXLI_PARAM_2_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPSEXLI_FactorsInsuranceAmountsBySalaryRangeFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRPSEXLI";
							CPCommon.WaitControlDisplayed(PRPSEXLI_MainForm);
formBttn = PRPSEXLI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

