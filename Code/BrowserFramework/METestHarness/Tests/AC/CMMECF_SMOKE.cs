 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CMMECF_SMOKE : TestScript
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
new Control("Cash Forecasting", "xpath","//div[@class='navItem'][.='Cash Forecasting']").Click();
new Control("Manage Preliminary Cash Forecasts", "xpath","//div[@class='navItem'][.='Manage Preliminary Cash Forecasts']").Click();


											Driver.SessionLogger.WriteLine("Query");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "CMMECF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMECF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control CMMECF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,CMMECF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "CMMECF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMECF] Perfoming VerifyExists on TemplateCode...", Logger.MessageType.INF);
			Control CMMECF_TemplateCode = new Control("TemplateCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TMPLT_CD']");
			CPCommon.AssertEqual(true,CMMECF_TemplateCode.Exists());

												
				CPCommon.CurrentComponent = "CMMECF";
							CPCommon.WaitControlDisplayed(CMMECF_MainForm);
IWebElement formBttn = CMMECF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? CMMECF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
CMMECF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "CMMECF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMECF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control CMMECF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMECF_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "CMMECF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMECF] Perfoming VerifyExists on PerliminaryCashForecastsDetailsForm...", Logger.MessageType.INF);
			Control CMMECF_PerliminaryCashForecastsDetailsForm = new Control("PerliminaryCashForecastsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMECF_CFTRPTDETL_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,CMMECF_PerliminaryCashForecastsDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "CMMECF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMECF] Perfoming VerifyExist on PerliminaryCashForecastsDetailsFormTable...", Logger.MessageType.INF);
			Control CMMECF_PerliminaryCashForecastsDetailsFormTable = new Control("PerliminaryCashForecastsDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMECF_CFTRPTDETL_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMECF_PerliminaryCashForecastsDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMECF";
							CPCommon.WaitControlDisplayed(CMMECF_PerliminaryCashForecastsDetailsForm);
formBttn = CMMECF_PerliminaryCashForecastsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? CMMECF_PerliminaryCashForecastsDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
CMMECF_PerliminaryCashForecastsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "CMMECF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMECF] Perfoming VerifyExists on CashForecastLineTitles...", Logger.MessageType.INF);
			Control CMMECF_CashForecastLineTitles = new Control("CashForecastLineTitles", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMECF_CFTRPTDETL_DETL_']/ancestor::form[1]/descendant::*[@id='CFT_LN_DESC']");
			CPCommon.AssertEqual(true,CMMECF_CashForecastLineTitles.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "CMMECF";
							CPCommon.WaitControlDisplayed(CMMECF_MainForm);
formBttn = CMMECF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

