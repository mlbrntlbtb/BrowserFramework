 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUMDRATE_SMOKE : TestScript
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
new Control("Manage Daily Exchange Rates", "xpath","//div[@class='navItem'][.='Manage Daily Exchange Rates']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUMDRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMDRATE] Perfoming ClickButtonIfExists on MainForm...", Logger.MessageType.INF);
			Control MUMDRATE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(MUMDRATE_MainForm);
IWebElement formBttn = MUMDRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MUMDRATE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MUMDRATE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "MUMDRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMDRATE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MUMDRATE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMDRATE_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMDRATE";
							CPCommon.AssertEqual(true,MUMDRATE_MainForm.Exists());

													
				CPCommon.CurrentComponent = "MUMDRATE";
							CPCommon.WaitControlDisplayed(MUMDRATE_MainForm);
formBttn = MUMDRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMDRATE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMDRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMDRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMDRATE] Perfoming VerifyExists on RateGroup...", Logger.MessageType.INF);
			Control MUMDRATE_RateGroup = new Control("RateGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RT_GRP_ID']");
			CPCommon.AssertEqual(true,MUMDRATE_RateGroup.Exists());

											Driver.SessionLogger.WriteLine("Currency Rate Form");


												
				CPCommon.CurrentComponent = "MUMDRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMDRATE] Perfoming VerifyExists on CurrencyRateForm...", Logger.MessageType.INF);
			Control MUMDRATE_CurrencyRateForm = new Control("CurrencyRateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMDRATE_RTBYDT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MUMDRATE_CurrencyRateForm.Exists());

												
				CPCommon.CurrentComponent = "MUMDRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMDRATE] Perfoming VerifyExist on CurrencyRateFormTable...", Logger.MessageType.INF);
			Control MUMDRATE_CurrencyRateFormTable = new Control("CurrencyRateFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMDRATE_RTBYDT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUMDRATE_CurrencyRateFormTable.Exists());

												
				CPCommon.CurrentComponent = "MUMDRATE";
							CPCommon.WaitControlDisplayed(MUMDRATE_CurrencyRateForm);
formBttn = MUMDRATE_CurrencyRateForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MUMDRATE_CurrencyRateForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MUMDRATE_CurrencyRateForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MUMDRATE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMDRATE] Perfoming VerifyExists on CurrencyRate_EndingDate...", Logger.MessageType.INF);
			Control MUMDRATE_CurrencyRate_EndingDate = new Control("CurrencyRate_EndingDate", "xpath", "//div[translate(@id,'0123456789','')='pr__MUMDRATE_RTBYDT_CTW_']/ancestor::form[1]/descendant::*[@id='END_DT']");
			CPCommon.AssertEqual(true,MUMDRATE_CurrencyRate_EndingDate.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUMDRATE";
							CPCommon.WaitControlDisplayed(MUMDRATE_MainForm);
formBttn = MUMDRATE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

