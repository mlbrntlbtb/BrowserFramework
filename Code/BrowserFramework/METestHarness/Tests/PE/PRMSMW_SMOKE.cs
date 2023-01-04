 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMSMW_SMOKE : TestScript
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
new Control("Garnishments", "xpath","//div[@class='navItem'][.='Garnishments']").Click();
new Control("Manage Minimum Wage", "xpath","//div[@class='navItem'][.='Manage Minimum Wage']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PRMSMW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSMW] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMSMW_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMSMW_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSMW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSMW] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control PRMSMW_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,PRMSMW_State.Exists());

												
				CPCommon.CurrentComponent = "PRMSMW";
							CPCommon.WaitControlDisplayed(PRMSMW_MainForm);
IWebElement formBttn = PRMSMW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMSMW_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMSMW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMSMW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSMW] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMSMW_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSMW_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("MinimumWageRates");


												
				CPCommon.CurrentComponent = "PRMSMW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSMW] Perfoming VerifyExists on MinimumWageRatesForm...", Logger.MessageType.INF);
			Control PRMSMW_MinimumWageRatesForm = new Control("MinimumWageRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSMW_ST_MIN_WAGE_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMSMW_MinimumWageRatesForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSMW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSMW] Perfoming VerifyExist on MinimumWageRatesFormTable...", Logger.MessageType.INF);
			Control PRMSMW_MinimumWageRatesFormTable = new Control("MinimumWageRatesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSMW_ST_MIN_WAGE_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSMW_MinimumWageRatesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMSMW";
							CPCommon.WaitControlDisplayed(PRMSMW_MainForm);
formBttn = PRMSMW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

