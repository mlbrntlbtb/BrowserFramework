 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPWFRT_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Labor", "xpath","//div[@class='navItem'][.='Project Labor']").Click();
new Control("Link PLC Rates to Employee/Vendor", "xpath","//div[@class='navItem'][.='Link PLC Rates to Employee/Vendor']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPWFRT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPWFRT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control PJMPWFRT_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPWFRT_Identification_Project.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming VerifyExist on EmployeeBillingRatesTable...", Logger.MessageType.INF);
			Control PJMPWFRT_EmployeeBillingRatesTable = new Control("EmployeeBillingRatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPWFRT_PROJEMPLRTSCH_PWFRT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPWFRT_EmployeeBillingRatesTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming ClickButton on EmployeeBillingRatesForm...", Logger.MessageType.INF);
			Control PJMPWFRT_EmployeeBillingRatesForm = new Control("EmployeeBillingRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPWFRT_PROJEMPLRTSCH_PWFRT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPWFRT_EmployeeBillingRatesForm);
IWebElement formBttn = PJMPWFRT_EmployeeBillingRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPWFRT_EmployeeBillingRatesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPWFRT_EmployeeBillingRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPWFRT";
							CPCommon.AssertEqual(true,PJMPWFRT_EmployeeBillingRatesForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming VerifyExists on EmployeeBillingRates_EMPLOYEEID...", Logger.MessageType.INF);
			Control PJMPWFRT_EmployeeBillingRates_EMPLOYEEID = new Control("EmployeeBillingRates_EMPLOYEEID", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPWFRT_PROJEMPLRTSCH_PWFRT_']/ancestor::form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PJMPWFRT_EmployeeBillingRates_EMPLOYEEID.Exists());

												
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming VerifyExist on VendorBillingRatesTable...", Logger.MessageType.INF);
			Control PJMPWFRT_VendorBillingRatesTable = new Control("VendorBillingRatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPWFRT_PROJVENDRTSCH_PWFRT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPWFRT_VendorBillingRatesTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming ClickButton on VendorBillingRatesForm...", Logger.MessageType.INF);
			Control PJMPWFRT_VendorBillingRatesForm = new Control("VendorBillingRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPWFRT_PROJVENDRTSCH_PWFRT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPWFRT_VendorBillingRatesForm);
formBttn = PJMPWFRT_VendorBillingRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPWFRT_VendorBillingRatesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPWFRT_VendorBillingRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPWFRT";
							CPCommon.AssertEqual(true,PJMPWFRT_VendorBillingRatesForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPWFRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPWFRT] Perfoming VerifyExists on VendorBillingRates_VendorEmployee...", Logger.MessageType.INF);
			Control PJMPWFRT_VendorBillingRates_VendorEmployee = new Control("VendorBillingRates_VendorEmployee", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPWFRT_PROJVENDRTSCH_PWFRT_']/ancestor::form[1]/descendant::*[@id='VEND_EMPL_ID']");
			CPCommon.AssertEqual(true,PJMPWFRT_VendorBillingRates_VendorEmployee.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMPWFRT";
							CPCommon.WaitControlDisplayed(PJMPWFRT_MainForm);
formBttn = PJMPWFRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

