 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUMRDT_SMOKE : TestScript
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
new Control("Manage Exchange Rates by Date", "xpath","//div[@class='navItem'][.='Manage Exchange Rates by Date']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUMRDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRDT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUMRDT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUMRDT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUMRDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRDT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MUMRDT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMRDT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMRDT";
							CPCommon.WaitControlDisplayed(MUMRDT_MainForm);
IWebElement formBttn = MUMRDT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMRDT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMRDT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMRDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRDT] Perfoming VerifyExists on RateGroup...", Logger.MessageType.INF);
			Control MUMRDT_RateGroup = new Control("RateGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RT_GRP_ID']");
			CPCommon.AssertEqual(true,MUMRDT_RateGroup.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rate By Date Form");


												
				CPCommon.CurrentComponent = "MUMRDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRDT] Perfoming VerifyExists on ExchangeRateByDateForm...", Logger.MessageType.INF);
			Control MUMRDT_ExchangeRateByDateForm = new Control("ExchangeRateByDateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRDT_RTBYDT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MUMRDT_ExchangeRateByDateForm.Exists());

												
				CPCommon.CurrentComponent = "MUMRDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRDT] Perfoming VerifyExist on ExchangeRateByDateFormTable...", Logger.MessageType.INF);
			Control MUMRDT_ExchangeRateByDateFormTable = new Control("ExchangeRateByDateFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRDT_RTBYDT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMRDT_ExchangeRateByDateFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMRDT";
							CPCommon.WaitControlDisplayed(MUMRDT_ExchangeRateByDateForm);
formBttn = MUMRDT_ExchangeRateByDateForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMRDT_ExchangeRateByDateForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMRDT_ExchangeRateByDateForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMRDT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMRDT] Perfoming VerifyExists on ExchangeRateByDate_RateAmount...", Logger.MessageType.INF);
			Control MUMRDT_ExchangeRateByDate_RateAmount = new Control("ExchangeRateByDate_RateAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMRDT_RTBYDT_CTW_']/ancestor::form[1]/descendant::*[@id='EXCH_RT']");
			CPCommon.AssertEqual(true,MUMRDT_ExchangeRateByDate_RateAmount.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUMRDT";
							CPCommon.WaitControlDisplayed(MUMRDT_MainForm);
formBttn = MUMRDT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

