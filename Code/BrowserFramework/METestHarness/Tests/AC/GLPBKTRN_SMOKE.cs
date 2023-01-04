 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPBKTRN_SMOKE : TestScript
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
new Control("Cash Management", "xpath","//div[@class='deptItem'][.='Cash Management']").Click();
new Control("Bank Account Management", "xpath","//div[@class='navItem'][.='Bank Account Management']").Click();
new Control("Create Bank Transactions History", "xpath","//div[@class='navItem'][.='Create Bank Transactions History']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLPBKTRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBKTRN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLPBKTRN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLPBKTRN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLPBKTRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBKTRN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control GLPBKTRN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,GLPBKTRN_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "GLPBKTRN";
							CPCommon.WaitControlDisplayed(GLPBKTRN_MainForm);
IWebElement formBttn = GLPBKTRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLPBKTRN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLPBKTRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLPBKTRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBKTRN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLPBKTRN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPBKTRN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates Form");


												
				CPCommon.CurrentComponent = "GLPBKTRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBKTRN] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control GLPBKTRN_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_1004391_GLPBKTRN_PARAM");
			CPCommon.AssertEqual(true,GLPBKTRN_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "GLPBKTRN";
							CPCommon.WaitControlDisplayed(GLPBKTRN_ExchangeRatesLink);
GLPBKTRN_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLPBKTRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBKTRN] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control GLPBKTRN_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPBKTRN_EXCHANGE_RATES_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLPBKTRN_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "GLPBKTRN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPBKTRN] Perfoming VerifyExists on ExchangeRates_RateForAP_RateGroup...", Logger.MessageType.INF);
			Control GLPBKTRN_ExchangeRates_RateForAP_RateGroup = new Control("ExchangeRates_RateForAP_RateGroup", "xpath", "//div[translate(@id,'0123456789','')='pr__GLPBKTRN_EXCHANGE_RATES_']/ancestor::form[1]/descendant::*[@id='AP_RATE_GRP_OPT']");
			CPCommon.AssertEqual(true,GLPBKTRN_ExchangeRates_RateForAP_RateGroup.Exists());

												
				CPCommon.CurrentComponent = "GLPBKTRN";
							CPCommon.WaitControlDisplayed(GLPBKTRN_ExchangeRatesForm);
formBttn = GLPBKTRN_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLPBKTRN";
							CPCommon.WaitControlDisplayed(GLPBKTRN_MainForm);
formBttn = GLPBKTRN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

