 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUMPRATE_SMOKE : TestScript
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
new Control("Multicurrency", "xpath","//div[@class='deptItem'][.='Multicurrency']").Click();
new Control("Exchange Rates", "xpath","//div[@class='navItem'][.='Exchange Rates']").Click();
new Control("Manage Period Exchange Rates", "xpath","//div[@class='navItem'][.='Manage Period Exchange Rates']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUMPRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMPRATE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUMPRATE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUMPRATE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUMPRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMPRATE] Perfoming VerifyExists on RateGroupID...", Logger.MessageType.INF);
			Control MUMPRATE_RateGroupID = new Control("RateGroupID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RT_GRP_ID']");
			CPCommon.AssertEqual(true,MUMPRATE_RateGroupID.Exists());

												
				CPCommon.CurrentComponent = "MUMPRATE";
							CPCommon.WaitControlDisplayed(MUMPRATE_MainForm);
IWebElement formBttn = MUMPRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MUMPRATE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MUMPRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MUMPRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMPRATE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MUMPRATE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMPRATE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Period Exchange Rates Form");


												
				CPCommon.CurrentComponent = "MUMPRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMPRATE] Perfoming VerifyExists on PeriodExchangeRatesForm...", Logger.MessageType.INF);
			Control MUMPRATE_PeriodExchangeRatesForm = new Control("PeriodExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMPRATE_RTBYPD_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MUMPRATE_PeriodExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "MUMPRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMPRATE] Perfoming VerifyExist on PeriodExchangeRatesFormTable...", Logger.MessageType.INF);
			Control MUMPRATE_PeriodExchangeRatesFormTable = new Control("PeriodExchangeRatesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMPRATE_RTBYPD_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMPRATE_PeriodExchangeRatesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMPRATE";
							CPCommon.WaitControlDisplayed(MUMPRATE_PeriodExchangeRatesForm);
formBttn = MUMPRATE_PeriodExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMPRATE_PeriodExchangeRatesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMPRATE_PeriodExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMPRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMPRATE] Perfoming VerifyExists on PeriodExchangeRates_CurrencyFrom...", Logger.MessageType.INF);
			Control MUMPRATE_PeriodExchangeRates_CurrencyFrom = new Control("PeriodExchangeRates_CurrencyFrom", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMPRATE_RTBYPD_CHILD_']/ancestor::form[1]/descendant::*[@id='FR_S_CRNCY_CD']");
			CPCommon.AssertEqual(true,MUMPRATE_PeriodExchangeRates_CurrencyFrom.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUMPRATE";
							CPCommon.WaitControlDisplayed(MUMPRATE_MainForm);
formBttn = MUMPRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

