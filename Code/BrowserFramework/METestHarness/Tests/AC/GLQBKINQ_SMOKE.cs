 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLQBKINQ_SMOKE : TestScript
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
new Control("Cash Management Reports/Inquiries", "xpath","//div[@class='navItem'][.='Cash Management Reports/Inquiries']").Click();
new Control("View Bank Reconciliation Summary", "xpath","//div[@class='navItem'][.='View Bank Reconciliation Summary']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLQBKINQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on SelectBankAccount_BankAbbr...", Logger.MessageType.INF);
			Control GLQBKINQ_SelectBankAccount_BankAbbr = new Control("SelectBankAccount_BankAbbr", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ACCT_ABBRV']");
			CPCommon.AssertEqual(true,GLQBKINQ_SelectBankAccount_BankAbbr.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							GLQBKINQ_SelectBankAccount_BankAbbr.Click();
GLQBKINQ_SelectBankAccount_BankAbbr.SendKeys("TEST", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQBKINQ_SelectBankAccount_BankAbbr.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming Set on SelectTiming_FiscalYear...", Logger.MessageType.INF);
			Control GLQBKINQ_SelectTiming_FiscalYear = new Control("SelectTiming_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			GLQBKINQ_SelectTiming_FiscalYear.Click();
GLQBKINQ_SelectTiming_FiscalYear.SendKeys("1961", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQBKINQ_SelectTiming_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming Set on SelectTiming_Period...", Logger.MessageType.INF);
			Control GLQBKINQ_SelectTiming_Period = new Control("SelectTiming_Period", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PD_NO']");
			GLQBKINQ_SelectTiming_Period.Click();
GLQBKINQ_SelectTiming_Period.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQBKINQ_SelectTiming_Period.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_GLPeriodEndingDate...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_GLPeriodEndingDate = new Control("ChildForm_GLPeriodEndingDate", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_HDR_']/ancestor::form[1]/descendant::*[@id='PD_END_DT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_GLPeriodEndingDate.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm);
IWebElement formBttn = GLQBKINQ_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLQBKINQ_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLQBKINQ_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("C/R PER BOOKS");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CRPerBooksLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CRPerBooksLink = new Control("ChildForm_CRPerBooksLink", "ID", "lnk_1005612_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CRPerBooksLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_CRPerBooksLink);
GLQBKINQ_ChildForm_CRPerBooksLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CRPerBooksForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CRPerBooksForm = new Control("ChildForm_CRPerBooksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_CASHRECPTS_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CRPerBooksForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CRPerBooks_TransTotalGLAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CRPerBooks_TransTotalGLAmount = new Control("ChildForm_CRPerBooks_TransTotalGLAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_CASHRECPTS_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_TRN_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CRPerBooks_TransTotalGLAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_CRPerBooks_CashReceiptsPerBooksFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksFormTable = new Control("ChildForm_CRPerBooks_CashReceiptsPerBooksFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_CASHRECPTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_CRPerBooks_CashReceiptsPerBooksForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksForm = new Control("ChildForm_CRPerBooks_CashReceiptsPerBooksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_CASHRECPTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksForm);
formBttn = GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooksForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CRPerBooks_CashReceiptsPerBooks_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooks_TransType = new Control("ChildForm_CRPerBooks_CashReceiptsPerBooks_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_CASHRECPTS_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CRPerBooks_CashReceiptsPerBooks_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_CRPerBooksForm);
formBttn = GLQBKINQ_ChildForm_CRPerBooksForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("C/R PER BOOKS");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CDPerBooksLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CDPerBooksLink = new Control("ChildForm_CDPerBooksLink", "ID", "lnk_1005614_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CDPerBooksLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_CDPerBooksLink);
GLQBKINQ_ChildForm_CDPerBooksLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CDPerBooksForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CDPerBooksForm = new Control("ChildForm_CDPerBooksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_CASHDISB_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CDPerBooksForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CDPerBooks_TransTotalGLAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CDPerBooks_TransTotalGLAmount = new Control("ChildForm_CDPerBooks_TransTotalGLAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_CASHDISB_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_TRN_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CDPerBooks_TransTotalGLAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_CDPerBooks_CashDisbursementsPerBooksFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksFormTable = new Control("ChildForm_CDPerBooks_CashDisbursementsPerBooksFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_CASHDISB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_CDPerBooks_CashDisbursementsPerBooksForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksForm = new Control("ChildForm_CDPerBooks_CashDisbursementsPerBooksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_CASHDISB_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksForm);
formBttn = GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooksForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_CDPerBooks_CashDisbursementsPerBooks_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooks_TransType = new Control("ChildForm_CDPerBooks_CashDisbursementsPerBooks_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_CASHDISB_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_CDPerBooks_CashDisbursementsPerBooks_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_CDPerBooksForm);
formBttn = GLQBKINQ_ChildForm_CDPerBooksForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("MANUAL ADJ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_ManualAdjLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_ManualAdjLink = new Control("ChildForm_ManualAdjLink", "ID", "lnk_1005616_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_ManualAdjLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_ManualAdjLink);
GLQBKINQ_ChildForm_ManualAdjLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_ManualAdjForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_ManualAdjForm = new Control("ChildForm_ManualAdjForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_MANADJ_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_ManualAdjForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_ManualAdj_TransTotalGLAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_ManualAdj_TransTotalGLAmount = new Control("ChildForm_ManualAdj_TransTotalGLAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_MANADJ_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_TRN_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_ManualAdj_TransTotalGLAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_ManualAdj_ManualAdjustmentsFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsFormTable = new Control("ChildForm_ManualAdj_ManualAdjustmentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_MANADJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_ManualAdj_ManualAdjustmentsForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsForm = new Control("ChildForm_ManualAdj_ManualAdjustmentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_MANADJ_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsForm);
formBttn = GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_ManualAdj_ManualAdjustmentsForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_ManualAdj_ManualAdjustments_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_ManualAdj_ManualAdjustments_TransType = new Control("ChildForm_ManualAdj_ManualAdjustments_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_MANADJ_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_ManualAdj_ManualAdjustments_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_ManualAdjForm);
formBttn = GLQBKINQ_ChildForm_ManualAdjForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DEPOSITS IN TRANSIT");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_DepositsInTransitLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_DepositsInTransitLink = new Control("ChildForm_DepositsInTransitLink", "ID", "lnk_1005619_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_DepositsInTransitLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_DepositsInTransitLink);
GLQBKINQ_ChildForm_DepositsInTransitLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_DepositsInTransitForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_DepositsInTransitForm = new Control("ChildForm_DepositsInTransitForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_TRNDEP_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_DepositsInTransitForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_DepositsInTransit_FuncTotalReceiptAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_DepositsInTransit_FuncTotalReceiptAmount = new Control("ChildForm_DepositsInTransit_FuncTotalReceiptAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_TRNDEP_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_FUNC_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_DepositsInTransit_FuncTotalReceiptAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_DepositsInTransit_DepositsInTransitFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitFormTable = new Control("ChildForm_DepositsInTransit_DepositsInTransitFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_TRNDEP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_DepositsInTransit_DepositsInTransitForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitForm = new Control("ChildForm_DepositsInTransit_DepositsInTransitForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_TRNDEP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitForm);
formBttn = GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransitForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_DepositsInTransit_DepositsInTransit_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransit_TransType = new Control("ChildForm_DepositsInTransit_DepositsInTransit_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_TRNDEP_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_DepositsInTransit_DepositsInTransit_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_DepositsInTransitForm);
formBttn = GLQBKINQ_ChildForm_DepositsInTransitForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("OUTSTANDING CHKS");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingChksLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingChksLink = new Control("ChildForm_OutstandingChksLink", "ID", "lnk_1005621_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingChksLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_OutstandingChksLink);
GLQBKINQ_ChildForm_OutstandingChksLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingChksForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingChksForm = new Control("ChildForm_OutstandingChksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_OUTCHK_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingChksForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingChks_FuncTotalCheckAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingChks_FuncTotalCheckAmount = new Control("ChildForm_OutstandingChks_FuncTotalCheckAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_OUTCHK_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_FUNC_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingChks_FuncTotalCheckAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_OutstandingChks_OutstandingChecksFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksFormTable = new Control("ChildForm_OutstandingChks_OutstandingChecksFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_OUTCHK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_OutstandingChks_OutstandingChecksForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksForm = new Control("ChildForm_OutstandingChks_OutstandingChecksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_OUTCHK_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksForm);
formBttn = GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecksForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingChks_OutstandingChecks_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecks_TransType = new Control("ChildForm_OutstandingChks_OutstandingChecks_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_OUTCHK_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingChks_OutstandingChecks_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_OutstandingChksForm);
formBttn = GLQBKINQ_ChildForm_OutstandingChksForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("OUTSTANDING ADJ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingAdjLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingAdjLink = new Control("ChildForm_OutstandingAdjLink", "ID", "lnk_1005623_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingAdjLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_OutstandingAdjLink);
GLQBKINQ_ChildForm_OutstandingAdjLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingAdjForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingAdjForm = new Control("ChildForm_OutstandingAdjForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_OUTADJ_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingAdjForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingAdj_FuncTotalAdjustedAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingAdj_FuncTotalAdjustedAmount = new Control("ChildForm_OutstandingAdj_FuncTotalAdjustedAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_OUTADJ_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_FUNC_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingAdj_FuncTotalAdjustedAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_OutstandingAdj_OutstandingManualAdjustmentsFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsFormTable = new Control("ChildForm_OutstandingAdj_OutstandingManualAdjustmentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_OUTADJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm = new Control("ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_OUTADJ_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm);
formBttn = GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustmentsForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_OutstandingAdj_OutstandingManualAdjustments_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustments_TransType = new Control("ChildForm_OutstandingAdj_OutstandingManualAdjustments_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_OUTADJ_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_OutstandingAdj_OutstandingManualAdjustments_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_OutstandingAdjForm);
formBttn = GLQBKINQ_ChildForm_OutstandingAdjForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("BANK STMT O/I");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_BankStmtOILink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_BankStmtOILink = new Control("ChildForm_BankStmtOILink", "ID", "lnk_1005625_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_BankStmtOILink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_BankStmtOILink);
GLQBKINQ_ChildForm_BankStmtOILink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_BankStmtOIForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_BankStmtOIForm = new Control("ChildForm_BankStmtOIForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BKSMT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_BankStmtOIForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_BankStmtOI_TotalDepositAmt...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_BankStmtOI_TotalDepositAmt = new Control("ChildForm_BankStmtOI_TotalDepositAmt", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BKSMT_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_DEPT_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_BankStmtOI_TotalDepositAmt.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_BankStmtOI_BankStatementOutstandingItemsFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsFormTable = new Control("ChildForm_BankStmtOI_BankStatementOutstandingItemsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKACCTSTMTDTL_BKSMT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_BankStmtOI_BankStatementOutstandingItemsForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsForm = new Control("ChildForm_BankStmtOI_BankStatementOutstandingItemsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKACCTSTMTDTL_BKSMT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsForm);
formBttn = GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItemsForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_BankStmtOI_BankStatementOutstandingItems_TransDate...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItems_TransDate = new Control("ChildForm_BankStmtOI_BankStatementOutstandingItems_TransDate", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKACCTSTMTDTL_BKSMT_']/ancestor::form[1]/descendant::*[@id='TRN_DT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_BankStmtOI_BankStatementOutstandingItems_TransDate.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_BankStmtOIForm);
formBttn = GLQBKINQ_ChildForm_BankStmtOIForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("UNMATCHED CR");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCRLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCRLink = new Control("ChildForm_UnmatchedCRLink", "ID", "lnk_1005627_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCRLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedCRLink);
GLQBKINQ_ChildForm_UnmatchedCRLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCRForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCRForm = new Control("ChildForm_UnmatchedCRForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_UNMREC_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCRForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCR_FuncTotalGLAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCR_FuncTotalGLAmount = new Control("ChildForm_UnmatchedCR_FuncTotalGLAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_UNMREC_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_FUNC_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCR_FuncTotalGLAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_UnmatchedCR_UnmatchedCashReceiptsFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsFormTable = new Control("ChildForm_UnmatchedCR_UnmatchedCashReceiptsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMREC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm = new Control("ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMREC_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm);
formBttn = GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceiptsForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCR_UnmatchedCashReceipts_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceipts_TransType = new Control("ChildForm_UnmatchedCR_UnmatchedCashReceipts_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMREC_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCR_UnmatchedCashReceipts_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedCRForm);
formBttn = GLQBKINQ_ChildForm_UnmatchedCRForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("UNMATCHED CD");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCDLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCDLink = new Control("ChildForm_UnmatchedCDLink", "ID", "lnk_1005629_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCDLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedCDLink);
GLQBKINQ_ChildForm_UnmatchedCDLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCDForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCDForm = new Control("ChildForm_UnmatchedCDForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_UNMDISP_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCDForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCD_TransTotalGLAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCD_TransTotalGLAmount = new Control("ChildForm_UnmatchedCD_TransTotalGLAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_UNMDISP_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_TRN_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCD_TransTotalGLAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_UnmatchedCD_UnmatchedCashDisbursementsFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsFormTable = new Control("ChildForm_UnmatchedCD_UnmatchedCashDisbursementsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMDISP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm = new Control("ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMDISP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm);
formBttn = GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursementsForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedCD_UnmatchedCashDisbursements_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursements_TransType = new Control("ChildForm_UnmatchedCD_UnmatchedCashDisbursements_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMDISP_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedCD_UnmatchedCashDisbursements_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedCDForm);
formBttn = GLQBKINQ_ChildForm_UnmatchedCDForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("UNMATCHED ADJ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedAdjLink...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedAdjLink = new Control("ChildForm_UnmatchedAdjLink", "ID", "lnk_1005631_GLQBKINQ_BANKTRNHS_HDR");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedAdjLink.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedAdjLink);
GLQBKINQ_ChildForm_UnmatchedAdjLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedAdjForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedAdjForm = new Control("ChildForm_UnmatchedAdjForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_UNMMANADJ_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedAdjForm.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedAdj_TransTotalGLAmount...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedAdj_TransTotalGLAmount = new Control("ChildForm_UnmatchedAdj_TransTotalGLAmount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_UNMMANADJ_CHLD_']/ancestor::form[1]/descendant::*[@id='TOT_TRN_AMT']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedAdj_TransTotalGLAmount.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExist on ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsFormTable...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsFormTable = new Control("ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMMANADJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming ClickButton on ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm = new Control("ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMMANADJ_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm);
formBttn = GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustmentsForm.Exists());

													
				CPCommon.CurrentComponent = "GLQBKINQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQBKINQ] Perfoming VerifyExists on ChildForm_UnmatchedAdj_UnmatchedManualAdjustments_TransType...", Logger.MessageType.INF);
			Control GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustments_TransType = new Control("ChildForm_UnmatchedAdj_UnmatchedManualAdjustments_TransType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQBKINQ_BANKTRNHS_UNMMANADJ_']/ancestor::form[1]/descendant::*[@id='S_BANK_TRN_TYPE']");
			CPCommon.AssertEqual(true,GLQBKINQ_ChildForm_UnmatchedAdj_UnmatchedManualAdjustments_TransType.Exists());

												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_ChildForm_UnmatchedAdjForm);
formBttn = GLQBKINQ_ChildForm_UnmatchedAdjForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLQBKINQ";
							CPCommon.WaitControlDisplayed(GLQBKINQ_MainForm);
formBttn = GLQBKINQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

