 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMNBPAY_SMOKE : TestScript
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
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Compensation Budgeting", "xpath","//div[@class='navItem'][.='Compensation Budgeting']").Click();
new Control("Manage Non-Budgeted Compensation Pay Types", "xpath","//div[@class='navItem'][.='Manage Non-Budgeted Compensation Pay Types']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMNBPAY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMNBPAY] Perfoming VerifyExists on PayTypesForm...", Logger.MessageType.INF);
			Control HSMNBPAY_PayTypesForm = new Control("PayTypesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMNBPAY_PAYTYPE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HSMNBPAY_PayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "HSMNBPAY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMNBPAY] Perfoming VerifyExist on PayTypesFormTable...", Logger.MessageType.INF);
			Control HSMNBPAY_PayTypesFormTable = new Control("PayTypesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HSMNBPAY_PAYTYPE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMNBPAY_PayTypesFormTable.Exists());

												
				CPCommon.CurrentComponent = "HSMNBPAY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMNBPAY] Perfoming VerifyExists on NonBudgetedCompensationPayTypesForm...", Logger.MessageType.INF);
			Control HSMNBPAY_NonBudgetedCompensationPayTypesForm = new Control("NonBudgetedCompensationPayTypesForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMNBPAY_NonBudgetedCompensationPayTypesForm.Exists());

												
				CPCommon.CurrentComponent = "HSMNBPAY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMNBPAY] Perfoming VerifyExist on NonBudgetedCompensationPayTypesFormTable...", Logger.MessageType.INF);
			Control HSMNBPAY_NonBudgetedCompensationPayTypesFormTable = new Control("NonBudgetedCompensationPayTypesFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMNBPAY_NonBudgetedCompensationPayTypesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMNBPAY";
							CPCommon.WaitControlDisplayed(HSMNBPAY_NonBudgetedCompensationPayTypesForm);
IWebElement formBttn = HSMNBPAY_NonBudgetedCompensationPayTypesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

