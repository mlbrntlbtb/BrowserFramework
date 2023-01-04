 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUMRGRP_SMOKE : TestScript
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
new Control("Manage Exchange Rate Groups", "xpath","//div[@class='navItem'][.='Manage Exchange Rate Groups']").Click();


											Driver.SessionLogger.WriteLine("MAIN form");


												
				CPCommon.CurrentComponent = "MUMRGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRGRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUMRGRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUMRGRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUMRGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRGRP] Perfoming VerifyExists on RateGroup...", Logger.MessageType.INF);
			Control MUMRGRP_RateGroup = new Control("RateGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RT_GRP_ID']");
			CPCommon.AssertEqual(true,MUMRGRP_RateGroup.Exists());

												
				CPCommon.CurrentComponent = "MUMRGRP";
							CPCommon.WaitControlDisplayed(MUMRGRP_MainForm);
IWebElement formBttn = MUMRGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MUMRGRP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MUMRGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MUMRGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRGRP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MUMRGRP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMRGRP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMRGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRGRP] Perfoming VerifyExist on CurrencyExchangeRateToleranceFormTable...", Logger.MessageType.INF);
			Control MUMRGRP_CurrencyExchangeRateToleranceFormTable = new Control("CurrencyExchangeRateToleranceFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRGRP_RTGRPCRNCY_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMRGRP_CurrencyExchangeRateToleranceFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMRGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRGRP] Perfoming ClickButton on CurrencyExchangeRateToleranceForm...", Logger.MessageType.INF);
			Control MUMRGRP_CurrencyExchangeRateToleranceForm = new Control("CurrencyExchangeRateToleranceForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRGRP_RTGRPCRNCY_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MUMRGRP_CurrencyExchangeRateToleranceForm);
formBttn = MUMRGRP_CurrencyExchangeRateToleranceForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMRGRP_CurrencyExchangeRateToleranceForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMRGRP_CurrencyExchangeRateToleranceForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MUMRGRP";
							CPCommon.AssertEqual(true,MUMRGRP_CurrencyExchangeRateToleranceForm.Exists());

													
				CPCommon.CurrentComponent = "MUMRGRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRGRP] Perfoming VerifyExists on CurrencyExchangeRateTolerance_CurrencyFrom...", Logger.MessageType.INF);
			Control MUMRGRP_CurrencyExchangeRateTolerance_CurrencyFrom = new Control("CurrencyExchangeRateTolerance_CurrencyFrom", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRGRP_RTGRPCRNCY_CTW_']/ancestor::form[1]/descendant::*[@id='FR_S_CRNCY_CD']");
			CPCommon.AssertEqual(true,MUMRGRP_CurrencyExchangeRateTolerance_CurrencyFrom.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "MUMRGRP";
							CPCommon.WaitControlDisplayed(MUMRGRP_MainForm);
formBttn = MUMRGRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

