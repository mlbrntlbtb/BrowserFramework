 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMPAYTP_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Labor Rate Controls", "xpath","//div[@class='navItem'][.='Labor Rate Controls']").Click();
new Control("Manage Pay Types", "xpath","//div[@class='navItem'][.='Manage Pay Types']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMPAYTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMPAYTP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMPAYTP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTP] Perfoming VerifyExists on PayTypeCode...", Logger.MessageType.INF);
			Control LDMPAYTP_PayTypeCode = new Control("PayTypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_TYPE']");
			CPCommon.AssertEqual(true,LDMPAYTP_PayTypeCode.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTP";
							CPCommon.WaitControlDisplayed(LDMPAYTP_MainForm);
IWebElement formBttn = LDMPAYTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMPAYTP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMPAYTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMPAYTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTP] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control LDMPAYTP_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMPAYTP_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Transaction Currency Overrides Form");


												
				CPCommon.CurrentComponent = "LDMPAYTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTP] Perfoming VerifyExists on TransactionCurrencyOverridesLink...", Logger.MessageType.INF);
			Control LDMPAYTP_TransactionCurrencyOverridesLink = new Control("TransactionCurrencyOverridesLink", "ID", "lnk_16739_LDMPAYTP_PAYTYPE");
			CPCommon.AssertEqual(true,LDMPAYTP_TransactionCurrencyOverridesLink.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTP";
							CPCommon.WaitControlDisplayed(LDMPAYTP_TransactionCurrencyOverridesLink);
LDMPAYTP_TransactionCurrencyOverridesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMPAYTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTP] Perfoming VerifyExists on TransactionCurrencyOverridesForm...", Logger.MessageType.INF);
			Control LDMPAYTP_TransactionCurrencyOverridesForm = new Control("TransactionCurrencyOverridesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMPAYTP_PAYTYPE_CURR_OVRD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMPAYTP_TransactionCurrencyOverridesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMPAYTP] Perfoming VerifyExist on TransactionCurrencyOverridesFormTable...", Logger.MessageType.INF);
			Control LDMPAYTP_TransactionCurrencyOverridesFormTable = new Control("TransactionCurrencyOverridesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMPAYTP_PAYTYPE_CURR_OVRD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMPAYTP_TransactionCurrencyOverridesFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMPAYTP";
							CPCommon.WaitControlDisplayed(LDMPAYTP_TransactionCurrencyOverridesForm);
formBttn = LDMPAYTP_TransactionCurrencyOverridesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMPAYTP";
							CPCommon.WaitControlDisplayed(LDMPAYTP_MainForm);
formBttn = LDMPAYTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

