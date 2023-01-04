 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMEADD_SMOKE : TestScript
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
new Control("Manage Employee Allowances", "xpath","//div[@class='navItem'][.='Manage Employee Allowances']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMEADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEADD] Perfoming VerifyExists on AllowanceDetailsForm...", Logger.MessageType.INF);
			Control LDMEADD_AllowanceDetailsForm = new Control("AllowanceDetailsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMEADD_AllowanceDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMEADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEADD] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control LDMEADD_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,LDMEADD_Employee.Exists());

												
				CPCommon.CurrentComponent = "LDMEADD";
							CPCommon.WaitControlDisplayed(LDMEADD_AllowanceDetailsForm);
IWebElement formBttn = LDMEADD_AllowanceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMEADD_AllowanceDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMEADD_AllowanceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMEADD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEADD] Perfoming VerifyExist on AllowanceDetailsTable...", Logger.MessageType.INF);
			Control LDMEADD_AllowanceDetailsTable = new Control("AllowanceDetailsTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMEADD_AllowanceDetailsTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "LDMEADD";
							CPCommon.WaitControlDisplayed(LDMEADD_AllowanceDetailsForm);
formBttn = LDMEADD_AllowanceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

