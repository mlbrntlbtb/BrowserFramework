 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMSALES_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Manage Sales Group Abbreviations", "xpath","//div[@class='navItem'][.='Manage Sales Group Abbreviations']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMSALES_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMSALES_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming VerifyExists on SalesGroupAbbreviationCode...", Logger.MessageType.INF);
			Control PDMSALES_SalesGroupAbbreviationCode = new Control("SalesGroupAbbreviationCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SALES_ABBRV_CD']");
			CPCommon.AssertEqual(true,PDMSALES_SalesGroupAbbreviationCode.Exists());

												
				CPCommon.CurrentComponent = "PDMSALES";
							CPCommon.WaitControlDisplayed(PDMSALES_MainForm);
IWebElement formBttn = PDMSALES_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMSALES_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMSALES_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMSALES_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSALES_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("TRANSACTIONTYPE");


												
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming VerifyExist on TransactionTypeFormTable...", Logger.MessageType.INF);
			Control PDMSALES_TransactionTypeFormTable = new Control("TransactionTypeFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSALES_SPRODTRNTYPE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSALES_TransactionTypeFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming VerifyExist on SettingsFormTable...", Logger.MessageType.INF);
			Control PDMSALES_SettingsFormTable = new Control("SettingsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSALES_SALESGROUPACCTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMSALES_SettingsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming ClickButton on SettingsForm...", Logger.MessageType.INF);
			Control PDMSALES_SettingsForm = new Control("SettingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSALES_SALESGROUPACCTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMSALES_SettingsForm);
formBttn = PDMSALES_SettingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMSALES_SettingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMSALES_SettingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMSALES";
							CPCommon.AssertEqual(true,PDMSALES_SettingsForm.Exists());

													
				CPCommon.CurrentComponent = "PDMSALES";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMSALES] Perfoming VerifyExists on Settings_TransactionType...", Logger.MessageType.INF);
			Control PDMSALES_Settings_TransactionType = new Control("Settings_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMSALES_SALESGROUPACCTS_']/ancestor::form[1]/descendant::*[@id='PROD_TRN_TYPE_DESC']");
			CPCommon.AssertEqual(true,PDMSALES_Settings_TransactionType.Exists());

											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMSALES";
							CPCommon.WaitControlDisplayed(PDMSALES_MainForm);
formBttn = PDMSALES_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

