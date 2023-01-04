 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMFLVY_SMOKE : TestScript
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
new Control("Manage Federal Tax Levy Exemptions", "xpath","//div[@class='navItem'][.='Manage Federal Tax Levy Exemptions']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PRMFLVY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFLVY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMFLVY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMFLVY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMFLVY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFLVY] Perfoming VerifyExists on FilingStatus...", Logger.MessageType.INF);
			Control PRMFLVY_FilingStatus = new Control("FilingStatus", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_LVY_FIL_ST_DESC']");
			CPCommon.AssertEqual(true,PRMFLVY_FilingStatus.Exists());

												
				CPCommon.CurrentComponent = "PRMFLVY";
							CPCommon.WaitControlDisplayed(PRMFLVY_MainForm);
IWebElement formBttn = PRMFLVY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMFLVY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMFLVY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMFLVY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFLVY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMFLVY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMFLVY_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMFLVY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFLVY] Perfoming VerifyExists on WeeklyExemptionAmountsForm...", Logger.MessageType.INF);
			Control PRMFLVY_WeeklyExemptionAmountsForm = new Control("WeeklyExemptionAmountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFLVY_FEDTAXLVYEXEMPT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMFLVY_WeeklyExemptionAmountsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMFLVY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFLVY] Perfoming VerifyExist on WeeklyExemptionAmountsFormTable...", Logger.MessageType.INF);
			Control PRMFLVY_WeeklyExemptionAmountsFormTable = new Control("WeeklyExemptionAmountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFLVY_FEDTAXLVYEXEMPT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMFLVY_WeeklyExemptionAmountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMFLVY";
							CPCommon.WaitControlDisplayed(PRMFLVY_WeeklyExemptionAmountsForm);
formBttn = PRMFLVY_WeeklyExemptionAmountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRMFLVY_WeeklyExemptionAmountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRMFLVY_WeeklyExemptionAmountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRMFLVY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFLVY] Perfoming VerifyExists on WeeklyExemptionAmounts_EffectiveDate...", Logger.MessageType.INF);
			Control PRMFLVY_WeeklyExemptionAmounts_EffectiveDate = new Control("WeeklyExemptionAmounts_EffectiveDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFLVY_FEDTAXLVYEXEMPT_CTW_']/ancestor::form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,PRMFLVY_WeeklyExemptionAmounts_EffectiveDate.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "PRMFLVY";
							CPCommon.WaitControlDisplayed(PRMFLVY_MainForm);
formBttn = PRMFLVY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

