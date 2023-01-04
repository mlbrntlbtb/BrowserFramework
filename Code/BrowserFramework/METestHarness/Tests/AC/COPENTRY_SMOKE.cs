 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COPENTRY_SMOKE : TestScript
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
new Control("Create Consolidation Entries", "xpath","//div[@class='navItem'][.='Create Consolidation Entries']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "COPENTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPENTRY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control COPENTRY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,COPENTRY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "COPENTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPENTRY] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control COPENTRY_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,COPENTRY_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "COPENTRY";
							CPCommon.WaitControlDisplayed(COPENTRY_MainForm);
IWebElement formBttn = COPENTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? COPENTRY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
COPENTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "COPENTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPENTRY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control COPENTRY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COPENTRY_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "COPENTRY";
							CPCommon.WaitControlDisplayed(COPENTRY_MainForm);
formBttn = COPENTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? COPENTRY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
COPENTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "COPENTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPENTRY] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control COPENTRY_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_5235_COPENTRY_WFUNCPARMCATLG_PARAM");
			CPCommon.AssertEqual(true,COPENTRY_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "COPENTRY";
							CPCommon.WaitControlDisplayed(COPENTRY_ExchangeRatesLink);
COPENTRY_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "COPENTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPENTRY] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control COPENTRY_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_PDREXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COPENTRY_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "COPENTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COPENTRY] Perfoming VerifyExists on ExchangeRates_PayCurrency...", Logger.MessageType.INF);
			Control COPENTRY_ExchangeRates_PayCurrency = new Control("ExchangeRates_PayCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_PDREXR_']/ancestor::form[1]/descendant::*[@id='PAY_CRNCY_CD']");
			CPCommon.AssertEqual(true,COPENTRY_ExchangeRates_PayCurrency.Exists());

												
				CPCommon.CurrentComponent = "COPENTRY";
							CPCommon.WaitControlDisplayed(COPENTRY_ExchangeRatesForm);
formBttn = COPENTRY_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "COPENTRY";
							CPCommon.WaitControlDisplayed(COPENTRY_MainForm);
formBttn = COPENTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

