 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COPEXPPJ_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Consolidations", "xpath","//div[@class='deptItem'][.='Consolidations']").Click();
new Control("Consolidations Processing", "xpath","//div[@class='navItem'][.='Consolidations Processing']").Click();
new Control("Create Project Summary Balances", "xpath","//div[@class='navItem'][.='Create Project Summary Balances']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "COPEXPPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPEXPPJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control COPEXPPJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,COPEXPPJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "COPEXPPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPEXPPJ] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control COPEXPPJ_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,COPEXPPJ_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "COPEXPPJ";
							CPCommon.WaitControlDisplayed(COPEXPPJ_MainForm);
IWebElement formBttn = COPEXPPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? COPEXPPJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
COPEXPPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "COPEXPPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPEXPPJ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control COPEXPPJ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COPEXPPJ_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "COPEXPPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPEXPPJ] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control COPEXPPJ_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_3727_COPEXPPJ_WFUNCPARMCATLG_PARAM");
			CPCommon.AssertEqual(true,COPEXPPJ_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "COPEXPPJ";
							CPCommon.WaitControlDisplayed(COPEXPPJ_ExchangeRatesLink);
COPEXPPJ_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "COPEXPPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPEXPPJ] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control COPEXPPJ_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_PDREXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COPEXPPJ_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "COPEXPPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPEXPPJ] Perfoming VerifyExists on ExchangeRates_PayCurrency...", Logger.MessageType.INF);
			Control COPEXPPJ_ExchangeRates_PayCurrency = new Control("ExchangeRates_PayCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_PDREXR_']/ancestor::form[1]/descendant::*[@id='PAY_CRNCY_CD']");
			CPCommon.AssertEqual(true,COPEXPPJ_ExchangeRates_PayCurrency.Exists());

												
				CPCommon.CurrentComponent = "COPEXPPJ";
							CPCommon.WaitControlDisplayed(COPEXPPJ_ExchangeRatesForm);
formBttn = COPEXPPJ_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "COPEXPPJ";
							CPCommon.WaitControlDisplayed(COPEXPPJ_MainForm);
formBttn = COPEXPPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

