 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMEDEP_SMOKE : TestScript
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
new Control("Employee Benefit Information", "xpath","//div[@class='navItem'][.='Employee Benefit Information']").Click();
new Control("Manage Employee Dependents/Beneficiaries", "xpath","//div[@class='navItem'][.='Manage Employee Dependents/Beneficiaries']").Click();


											Driver.SessionLogger.WriteLine("Checking the App");


												
				CPCommon.CurrentComponent = "HBMEDEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDEP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMEDEP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMEDEP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEDEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDEP] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HBMEDEP_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HBMEDEP_Employee.Exists());

												
				CPCommon.CurrentComponent = "HBMEDEP";
							CPCommon.WaitControlDisplayed(HBMEDEP_MainForm);
IWebElement formBttn = HBMEDEP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMEDEP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMEDEP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMEDEP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEDEP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMEDEP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEDEP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing the App");


												
				CPCommon.CurrentComponent = "HBMEDEP";
							CPCommon.WaitControlDisplayed(HBMEDEP_MainForm);
formBttn = HBMEDEP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

