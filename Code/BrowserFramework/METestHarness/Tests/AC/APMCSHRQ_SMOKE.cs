 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMCSHRQ_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Accounts Payable Controls", "xpath","//div[@class='navItem'][.='Accounts Payable Controls']").Click();
new Control("Manage Cash Requirements Rpt Supplemental Amounts", "xpath","//div[@class='navItem'][.='Manage Cash Requirements Rpt Supplemental Amounts']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExists on SupplementalAmountsScheduleForm...", Logger.MessageType.INF);
			Control APMCSHRQ_SupplementalAmountsScheduleForm = new Control("SupplementalAmountsScheduleForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMCSHRQ_SupplementalAmountsScheduleForm.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExists on Schedule...", Logger.MessageType.INF);
			Control APMCSHRQ_Schedule = new Control("Schedule", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CREQ_SCH_CD']");
			CPCommon.AssertEqual(true,APMCSHRQ_Schedule.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
							CPCommon.WaitControlDisplayed(APMCSHRQ_SupplementalAmountsScheduleForm);
IWebElement formBttn = APMCSHRQ_SupplementalAmountsScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMCSHRQ_SupplementalAmountsScheduleForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMCSHRQ_SupplementalAmountsScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExist on SupplementalAmountsScheduleFormTable...", Logger.MessageType.INF);
			Control APMCSHRQ_SupplementalAmountsScheduleFormTable = new Control("SupplementalAmountsScheduleFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMCSHRQ_SupplementalAmountsScheduleFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExist on CashRequirementsDetailsFormTable...", Logger.MessageType.INF);
			Control APMCSHRQ_CashRequirementsDetailsFormTable = new Control("CashRequirementsDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCSHRQ_SUPPLCASHREQSCH_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMCSHRQ_CashRequirementsDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming ClickButton on CashRequirementsDetailsForm...", Logger.MessageType.INF);
			Control APMCSHRQ_CashRequirementsDetailsForm = new Control("CashRequirementsDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCSHRQ_SUPPLCASHREQSCH_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(APMCSHRQ_CashRequirementsDetailsForm);
formBttn = APMCSHRQ_CashRequirementsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMCSHRQ_CashRequirementsDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMCSHRQ_CashRequirementsDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMCSHRQ";
							CPCommon.AssertEqual(true,APMCSHRQ_CashRequirementsDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExists on CashRequirementsDetails_Description...", Logger.MessageType.INF);
			Control APMCSHRQ_CashRequirementsDetails_Description = new Control("CashRequirementsDetails_Description", "xpath", "//div[translate(@id,'0123456789','')='pr__APMCSHRQ_SUPPLCASHREQSCH_']/ancestor::form[1]/descendant::*[@id='SUPPL_AMT_DESC']");
			CPCommon.AssertEqual(true,APMCSHRQ_CashRequirementsDetails_Description.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORM LINK");


												
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExists on CashRequirementsDetails_ExchangeRatesLink...", Logger.MessageType.INF);
			Control APMCSHRQ_CashRequirementsDetails_ExchangeRatesLink = new Control("CashRequirementsDetails_ExchangeRatesLink", "ID", "lnk_1003707_APMCSHRQ_SUPPLCASHREQSCH");
			CPCommon.AssertEqual(true,APMCSHRQ_CashRequirementsDetails_ExchangeRatesLink.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
							CPCommon.WaitControlDisplayed(APMCSHRQ_CashRequirementsDetails_ExchangeRatesLink);
APMCSHRQ_CashRequirementsDetails_ExchangeRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExists on CashRequirementsDetails_ExchangeRatesForm...", Logger.MessageType.INF);
			Control APMCSHRQ_CashRequirementsDetails_ExchangeRatesForm = new Control("CashRequirementsDetails_ExchangeRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,APMCSHRQ_CashRequirementsDetails_ExchangeRatesForm.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCSHRQ] Perfoming VerifyExists on CashRequirementsDetails_ExchangeRates_Currencies_Transaction...", Logger.MessageType.INF);
			Control APMCSHRQ_CashRequirementsDetails_ExchangeRates_Currencies_Transaction = new Control("CashRequirementsDetails_ExchangeRates_Currencies_Transaction", "xpath", "//div[translate(@id,'0123456789','')='pr__CPM_MEXR_']/ancestor::form[1]/descendant::*[@id='TRN_CRNCY_CD']");
			CPCommon.AssertEqual(true,APMCSHRQ_CashRequirementsDetails_ExchangeRates_Currencies_Transaction.Exists());

												
				CPCommon.CurrentComponent = "APMCSHRQ";
							CPCommon.WaitControlDisplayed(APMCSHRQ_CashRequirementsDetails_ExchangeRatesForm);
formBttn = APMCSHRQ_CashRequirementsDetails_ExchangeRatesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APMCSHRQ";
							CPCommon.WaitControlDisplayed(APMCSHRQ_SupplementalAmountsScheduleForm);
formBttn = APMCSHRQ_SupplementalAmountsScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

