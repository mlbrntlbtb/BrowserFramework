 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MSPCPY_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Master Production Scheduling", "xpath","//div[@class='deptItem'][.='Master Production Scheduling']").Click();
new Control("Master Production Schedules", "xpath","//div[@class='navItem'][.='Master Production Schedules']").Click();
new Control("Copy Master Production Schedules", "xpath","//div[@class='navItem'][.='Copy Master Production Schedules']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MSPCPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPCPY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MSPCPY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MSPCPY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MSPCPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPCPY] Perfoming VerifyExists on SelectionRanges_FromForecasts_ForecastNeedDateStart...", Logger.MessageType.INF);
			Control MSPCPY_SelectionRanges_FromForecasts_ForecastNeedDateStart = new Control("SelectionRanges_FromForecasts_ForecastNeedDateStart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FROM_FORECAST']");
			CPCommon.AssertEqual(true,MSPCPY_SelectionRanges_FromForecasts_ForecastNeedDateStart.Exists());

											Driver.SessionLogger.WriteLine("Review form");


												
				CPCommon.CurrentComponent = "MSPCPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPCPY] Perfoming Click on SelectionRanges_ReviewCopiedMPSForecastsLink...", Logger.MessageType.INF);
			Control MSPCPY_SelectionRanges_ReviewCopiedMPSForecastsLink = new Control("SelectionRanges_ReviewCopiedMPSForecastsLink", "ID", "lnk_4990_MSPCPY_PARAM");
			CPCommon.WaitControlDisplayed(MSPCPY_SelectionRanges_ReviewCopiedMPSForecastsLink);
MSPCPY_SelectionRanges_ReviewCopiedMPSForecastsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "MSPCPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPCPY] Perfoming VerifyExists on ReviewCopiedMPSForecastsForm...", Logger.MessageType.INF);
			Control MSPCPY_ReviewCopiedMPSForecastsForm = new Control("ReviewCopiedMPSForecastsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MSPCPY_PREVIEW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MSPCPY_ReviewCopiedMPSForecastsForm.Exists());

												
				CPCommon.CurrentComponent = "MSPCPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPCPY] Perfoming VerifyExist on ReviewCopiedMPSForecastsFormTable...", Logger.MessageType.INF);
			Control MSPCPY_ReviewCopiedMPSForecastsFormTable = new Control("ReviewCopiedMPSForecastsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MSPCPY_PREVIEW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MSPCPY_ReviewCopiedMPSForecastsFormTable.Exists());

												
				CPCommon.CurrentComponent = "MSPCPY";
							CPCommon.WaitControlDisplayed(MSPCPY_ReviewCopiedMPSForecastsForm);
IWebElement formBttn = MSPCPY_ReviewCopiedMPSForecastsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MSPCPY_ReviewCopiedMPSForecastsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MSPCPY_ReviewCopiedMPSForecastsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MSPCPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPCPY] Perfoming VerifyExists on ReviewCopiedMPSForecasts_ForecastNeedDate...", Logger.MessageType.INF);
			Control MSPCPY_ReviewCopiedMPSForecasts_ForecastNeedDate = new Control("ReviewCopiedMPSForecasts_ForecastNeedDate", "xpath", "//div[translate(@id,'0123456789','')='pr__MSPCPY_PREVIEW_']/ancestor::form[1]/descendant::*[@id='MPS_DT']");
			CPCommon.AssertEqual(true,MSPCPY_ReviewCopiedMPSForecasts_ForecastNeedDate.Exists());

												
				CPCommon.CurrentComponent = "MSPCPY";
							CPCommon.WaitControlDisplayed(MSPCPY_ReviewCopiedMPSForecastsForm);
formBttn = MSPCPY_ReviewCopiedMPSForecastsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "MSPCPY";
							CPCommon.WaitControlDisplayed(MSPCPY_MainForm);
formBttn = MSPCPY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

