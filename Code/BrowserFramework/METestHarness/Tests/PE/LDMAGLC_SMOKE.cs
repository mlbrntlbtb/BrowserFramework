 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMAGLC_SMOKE : TestScript
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
new Control("Assign GLCs to Employees", "xpath","//div[@class='navItem'][.='Assign GLCs to Employees']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMAGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMAGLC] Perfoming VerifyExists on SelectGeneralLaborCategoriesForm...", Logger.MessageType.INF);
			Control LDMAGLC_SelectGeneralLaborCategoriesForm = new Control("SelectGeneralLaborCategoriesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMAGLC_GENLLABCAT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMAGLC_SelectGeneralLaborCategoriesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMAGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMAGLC] Perfoming VerifyExist on SelectGeneralLaborCategoriesFormTable...", Logger.MessageType.INF);
			Control LDMAGLC_SelectGeneralLaborCategoriesFormTable = new Control("SelectGeneralLaborCategoriesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMAGLC_GENLLABCAT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMAGLC_SelectGeneralLaborCategoriesFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMAGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMAGLC] Perfoming VerifyExists on SelectEmployeesForm...", Logger.MessageType.INF);
			Control LDMAGLC_SelectEmployeesForm = new Control("SelectEmployeesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMAGLC_EMPL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMAGLC_SelectEmployeesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMAGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMAGLC] Perfoming VerifyExist on SelectEmployeesFormTable...", Logger.MessageType.INF);
			Control LDMAGLC_SelectEmployeesFormTable = new Control("SelectEmployeesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMAGLC_EMPL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMAGLC_SelectEmployeesFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMAGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMAGLC] Perfoming VerifyExists on AssignGLCsToEmployeesForm...", Logger.MessageType.INF);
			Control LDMAGLC_AssignGLCsToEmployeesForm = new Control("AssignGLCsToEmployeesForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMAGLC_AssignGLCsToEmployeesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMAGLC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMAGLC] Perfoming VerifyExist on AssignGLCsToEmployeesFormTable...", Logger.MessageType.INF);
			Control LDMAGLC_AssignGLCsToEmployeesFormTable = new Control("AssignGLCsToEmployeesFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMAGLC_AssignGLCsToEmployeesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "LDMAGLC";
							CPCommon.WaitControlDisplayed(LDMAGLC_SelectEmployeesForm);
IWebElement formBttn = LDMAGLC_SelectEmployeesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

