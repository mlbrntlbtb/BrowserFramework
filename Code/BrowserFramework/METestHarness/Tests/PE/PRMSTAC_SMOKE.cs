 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMSTAC_SMOKE : TestScript
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
new Control("State Taxes", "xpath","//div[@class='navItem'][.='State Taxes']").Click();
new Control("Manage State Tax Withholding Adjustments", "xpath","//div[@class='navItem'][.='Manage State Tax Withholding Adjustments']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMSTAC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTAC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMSTAC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMSTAC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSTAC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTAC] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control PRMSTAC_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,PRMSTAC_State.Exists());

												
				CPCommon.CurrentComponent = "PRMSTAC";
							CPCommon.WaitControlDisplayed(PRMSTAC_MainForm);
IWebElement formBttn = PRMSTAC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMSTAC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMSTAC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMSTAC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTAC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMSTAC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTAC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("TaxAllowancesAndCredits Form");


												
				CPCommon.CurrentComponent = "PRMSTAC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTAC] Perfoming VerifyExists on TaxAllowancesAndCreditsForm...", Logger.MessageType.INF);
			Control PRMSTAC_TaxAllowancesAndCreditsForm = new Control("TaxAllowancesAndCreditsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTAC_STATETAXALLCR_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMSTAC_TaxAllowancesAndCreditsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSTAC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTAC] Perfoming VerifyExist on TaxAllowancesAndCreditsTable...", Logger.MessageType.INF);
			Control PRMSTAC_TaxAllowancesAndCreditsTable = new Control("TaxAllowancesAndCreditsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTAC_STATETAXALLCR_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTAC_TaxAllowancesAndCreditsTable.Exists());

												
				CPCommon.CurrentComponent = "PRMSTAC";
							CPCommon.WaitControlDisplayed(PRMSTAC_TaxAllowancesAndCreditsForm);
formBttn = PRMSTAC_TaxAllowancesAndCreditsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRMSTAC_TaxAllowancesAndCreditsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRMSTAC_TaxAllowancesAndCreditsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRMSTAC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTAC] Perfoming VerifyExists on TaxAllowancesAndCredits_ForAnnualizedWageOver...", Logger.MessageType.INF);
			Control PRMSTAC_TaxAllowancesAndCredits_ForAnnualizedWageOver = new Control("TaxAllowancesAndCredits_ForAnnualizedWageOver", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTAC_STATETAXALLCR_CTW_']/ancestor::form[1]/descendant::*[@id='GROSS_WGE_AMT']");
			CPCommon.AssertEqual(true,PRMSTAC_TaxAllowancesAndCredits_ForAnnualizedWageOver.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMSTAC";
							CPCommon.WaitControlDisplayed(PRMSTAC_MainForm);
formBttn = PRMSTAC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

