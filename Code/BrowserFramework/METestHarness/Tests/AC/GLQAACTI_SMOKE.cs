 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLQAACTI_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Reports/Inquiries", "xpath","//div[@class='navItem'][.='General Ledger Reports/Inquiries']").Click();
new Control("View Account Activity", "xpath","//div[@class='navItem'][.='View Account Activity']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on Account_Account...", Logger.MessageType.INF);
			Control GLQAACTI_Account_Account = new Control("Account_Account", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLQAACTI_Account_Account.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on Account_Level...", Logger.MessageType.INF);
			Control GLQAACTI_Account_Level = new Control("Account_Level", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_LVL_NO']");
			CPCommon.AssertEqual(true,GLQAACTI_Account_Level.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control GLQAACTI_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm);
IWebElement formBttn = GLQAACTI_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQAACTI_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQAACTI_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_Subperiod...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_Subperiod = new Control("ChildForm_Subperiod", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_CTW_']/ancestor::form[1]/descendant::*[@id='SUB_PD_NO']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_Subperiod.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							GLQAACTI_Account_Account.Click();
GLQAACTI_Account_Account.SendKeys("05000-010", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQAACTI_Account_Account.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "GLQAACTI";
							GLQAACTI_Account_Level.Click();
GLQAACTI_Account_Level.SendKeys("2", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQAACTI_Account_Level.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming Set on PeriodsToDisplay_FiscalYear...", Logger.MessageType.INF);
			Control GLQAACTI_PeriodsToDisplay_FiscalYear = new Control("PeriodsToDisplay_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			GLQAACTI_PeriodsToDisplay_FiscalYear.Click();
GLQAACTI_PeriodsToDisplay_FiscalYear.SendKeys("2014", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQAACTI_PeriodsToDisplay_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming Set on PeriodsToDisplay_Period...", Logger.MessageType.INF);
			Control GLQAACTI_PeriodsToDisplay_Period = new Control("PeriodsToDisplay_Period", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PD_NO']");
			GLQAACTI_PeriodsToDisplay_Period.Click();
GLQAACTI_PeriodsToDisplay_Period.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
GLQAACTI_PeriodsToDisplay_Period.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetailLink...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetailLink = new Control("ChildForm_PostingDetailLink", "ID", "lnk_1007301_GLQAACTI_GLDETL_CTW");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetailLink.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm_PostingDetailLink);
GLQAACTI_ChildForm_PostingDetailLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Posting Detail Form");


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetailForm...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetailForm = new Control("ChildForm_PostingDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_JNLDTL_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetailForm.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetail_TotalDebit...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_TotalDebit = new Control("ChildForm_PostingDetail_TotalDebit", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_JNLDTL_HDR_']/ancestor::form[1]/descendant::*[@id='TOTAL_DB']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_TotalDebit.Exists());

											Driver.SessionLogger.WriteLine("Journal Detail Form");


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetail_JournalDetailForm...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_JournalDetailForm = new Control("ChildForm_PostingDetail_JournalDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_JNLDETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_JournalDetailForm.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExist on ChildForm_PostingDetail_JournalDetailFormTable...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_JournalDetailFormTable = new Control("ChildForm_PostingDetail_JournalDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_JNLDETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_JournalDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm_PostingDetail_JournalDetailForm);
formBttn = GLQAACTI_ChildForm_PostingDetail_JournalDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLQAACTI_ChildForm_PostingDetail_JournalDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLQAACTI_ChildForm_PostingDetail_JournalDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetail_JournalDetail_Tab...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab = new Control("ChildForm_PostingDetail_JournalDetail_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_JNLDETL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab);
IWebElement mTab = GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetail_JournalDetail_Details_TransactionDescription...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_JournalDetail_Details_TransactionDescription = new Control("ChildForm_PostingDetail_JournalDetail_Details_TransactionDescription", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_JNLDETL_']/ancestor::form[1]/descendant::*[@id='TRN_DESC']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_JournalDetail_Details_TransactionDescription.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab);
mTab = GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Amount/Other Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetail_JournalDetail_AmountOtherDetails_TransDebit...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_JournalDetail_AmountOtherDetails_TransDebit = new Control("ChildForm_PostingDetail_JournalDetail_AmountOtherDetails_TransDebit", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_JNLDETL_']/ancestor::form[1]/descendant::*[@id='TRN_DB_AMT']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_JournalDetail_AmountOtherDetails_TransDebit.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab);
mTab = GLQAACTI_ChildForm_PostingDetail_JournalDetail_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming VerifyExists on ChildForm_PostingDetail_JournalDetail_Comments_TextArea...", Logger.MessageType.INF);
			Control GLQAACTI_ChildForm_PostingDetail_JournalDetail_Comments_TextArea = new Control("ChildForm_PostingDetail_JournalDetail_Comments_TextArea", "xpath", "//div[translate(@id,'0123456789','')='pr__GLQAACTI_GLDETL_JNLDETL_']/ancestor::form[1]/descendant::*[@id='COMMENTS_NT']");
			CPCommon.AssertEqual(true,GLQAACTI_ChildForm_PostingDetail_JournalDetail_Comments_TextArea.Exists());

												
				CPCommon.CurrentComponent = "GLQAACTI";
							CPCommon.WaitControlDisplayed(GLQAACTI_ChildForm_PostingDetailForm);
formBttn = GLQAACTI_ChildForm_PostingDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLQAACTI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLQAACTI] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control GLQAACTI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLQAACTI_MainForm);
formBttn = GLQAACTI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

