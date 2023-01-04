 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMOPEN_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Standard Bill Adjustments", "xpath","//div[@class='navItem'][.='Standard Bill Adjustments']").Click();
new Control("Manage Open Billing Detail", "xpath","//div[@class='navItem'][.='Manage Open Billing Detail']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMOPEN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMOPEN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_MainForm);
IWebElement formBttn = BLMOPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMOPEN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMOPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMOPEN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMOPEN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_MainForm);
formBttn = BLMOPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMOPEN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMOPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMOPEN_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMOPEN_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control BLMOPEN_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMOPEN_MainFormTab);
IWebElement mTab = BLMOPEN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on TransactionDescription...", Logger.MessageType.INF);
			Control BLMOPEN_TransactionDescription = new Control("TransactionDescription", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TRN_DESC']");
			CPCommon.AssertEqual(true,BLMOPEN_TransactionDescription.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_MainFormTab);
mTab = BLMOPEN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Labor").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on IDType...", Logger.MessageType.INF);
			Control BLMOPEN_IDType = new Control("IDType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_ID_TYPE']");
			CPCommon.AssertEqual(true,BLMOPEN_IDType.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_MainFormTab);
mTab = BLMOPEN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Units").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on SourceProject...", Logger.MessageType.INF);
			Control BLMOPEN_SourceProject = new Control("SourceProject", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SRCE_PROJ_ID']");
			CPCommon.AssertEqual(true,BLMOPEN_SourceProject.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_MainFormTab);
mTab = BLMOPEN_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on NumberEntry_None...", Logger.MessageType.INF);
			Control BLMOPEN_NumberEntry_None = new Control("NumberEntry_None", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFSRADIO' and @value='0']");
			CPCommon.AssertEqual(true,BLMOPEN_NumberEntry_None.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates");


												
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on ExchangeRatesLink...", Logger.MessageType.INF);
			Control BLMOPEN_ExchangeRatesLink = new Control("ExchangeRatesLink", "ID", "lnk_16711_BLMOPEN_OPENBILLINGDETL_HDR");
			CPCommon.AssertEqual(true,BLMOPEN_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_ExchangeRatesLink);
BLMOPEN_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on ExchangeRatesForm...", Logger.MessageType.INF);
			Control BLMOPEN_ExchangeRatesForm = new Control("ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMOPEN_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOPEN] Perfoming VerifyExists on ExchangeRates_TransactionCurrency...", Logger.MessageType.INF);
			Control BLMOPEN_ExchangeRates_TransactionCurrency = new Control("ExchangeRates_TransactionCurrency", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_SEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,BLMOPEN_ExchangeRates_TransactionCurrency.Exists());

												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_ExchangeRatesForm);
formBttn = BLMOPEN_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLMOPEN";
							CPCommon.WaitControlDisplayed(BLMOPEN_MainForm);
formBttn = BLMOPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

