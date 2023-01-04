 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HKME401K_SMOKE : TestScript
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
new Control("Employee Deferred Compensation Information", "xpath","//div[@class='navItem'][.='Employee Deferred Compensation Information']").Click();
new Control("Manage Employee Deferred Compensation", "xpath","//div[@class='navItem'][.='Manage Employee Deferred Compensation']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HKME401K";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKME401K] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HKME401K_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HKME401K_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HKME401K";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKME401K] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HKME401K_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HKME401K_Employee.Exists());

												
				CPCommon.CurrentComponent = "HKME401K";
							CPCommon.WaitControlDisplayed(HKME401K_MainForm);
IWebElement formBttn = HKME401K_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HKME401K_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HKME401K_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HKME401K";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKME401K] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HKME401K_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HKME401K_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Deferred Compensation Loans Form");


												
				CPCommon.CurrentComponent = "HKME401K";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKME401K] Perfoming VerifyExists on DeferredCompensationLoansForm...", Logger.MessageType.INF);
			Control HKME401K_DeferredCompensationLoansForm = new Control("DeferredCompensationLoansForm", "xpath", "//div[starts-with(@id,'pr__HKME401K_HCODAEMPLLOAN_DETAIL_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,HKME401K_DeferredCompensationLoansForm.Exists());

												
				CPCommon.CurrentComponent = "HKME401K";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKME401K] Perfoming VerifyExist on DeferredCompensationLoansFormTable...", Logger.MessageType.INF);
			Control HKME401K_DeferredCompensationLoansFormTable = new Control("DeferredCompensationLoansFormTable", "xpath", "//div[starts-with(@id,'pr__HKME401K_HCODAEMPLLOAN_DETAIL_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HKME401K_DeferredCompensationLoansFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HKME401K";
							CPCommon.WaitControlDisplayed(HKME401K_MainForm);
formBttn = HKME401K_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

