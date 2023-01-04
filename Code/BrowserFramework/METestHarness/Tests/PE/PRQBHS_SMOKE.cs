 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQBHS_SMOKE : TestScript
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
new Control("U.S. Treasury Savings Bonds", "xpath","//div[@class='navItem'][.='U.S. Treasury Savings Bonds']").Click();
new Control("View Savings Bond File History", "xpath","//div[@class='navItem'][.='View Savings Bond File History']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRQBHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBHS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQBHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQBHS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQBHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBHS] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control PRQBHS_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPLOYEE']");
			CPCommon.AssertEqual(true,PRQBHS_Employee.Exists());

												
				CPCommon.CurrentComponent = "PRQBHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBHS] Perfoming VerifyExists on InquiryDetailsForm...", Logger.MessageType.INF);
			Control PRQBHS_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBHS_SAVINGBONDHISTORY_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQBHS_InquiryDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQBHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBHS] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control PRQBHS_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBHS_SAVINGBONDHISTORY_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQBHS_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQBHS";
							CPCommon.WaitControlDisplayed(PRQBHS_InquiryDetailsForm);
IWebElement formBttn = PRQBHS_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQBHS_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQBHS_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQBHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQBHS] Perfoming VerifyExists on InquiryDetails_Employee...", Logger.MessageType.INF);
			Control PRQBHS_InquiryDetails_Employee = new Control("InquiryDetails_Employee", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQBHS_SAVINGBONDHISTORY_CHILD_']/ancestor::form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQBHS_InquiryDetails_Employee.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRQBHS";
							CPCommon.WaitControlDisplayed(PRQBHS_MainForm);
formBttn = PRQBHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

