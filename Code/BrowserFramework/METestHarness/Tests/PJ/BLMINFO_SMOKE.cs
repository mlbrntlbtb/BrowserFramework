 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMINFO_SMOKE : TestScript
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
new Control("Billing Master", "xpath","//div[@class='navItem'][.='Billing Master']").Click();
new Control("Manage Project Billing Information", "xpath","//div[@class='navItem'][.='Manage Project Billing Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMINFO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMINFO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMINFO_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVC_PROJ_ID']");
			CPCommon.AssertEqual(true,BLMINFO_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control BLMINFO_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLMINFO_MainFormTab);
IWebElement mTab = BLMINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Setup Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on SetupInformation_BillingFormula_BillingFormulaDescription...", Logger.MessageType.INF);
			Control BLMINFO_SetupInformation_BillingFormula_BillingFormulaDescription = new Control("SetupInformation_BillingFormula_BillingFormulaDescription", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_FORMULA_DESC']");
			CPCommon.AssertEqual(true,BLMINFO_SetupInformation_BillingFormula_BillingFormulaDescription.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_MainFormTab);
mTab = BLMINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "1443 Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on 1443InfoRates_Rates_ProgressPayment...", Logger.MessageType.INF);
			Control BLMINFO_1443InfoRates_Rates_ProgressPayment = new Control("1443InfoRates_Rates_ProgressPayment", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROGRESS_PMT_RT']");
			CPCommon.AssertEqual(true,BLMINFO_1443InfoRates_Rates_ProgressPayment.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_MainFormTab);
mTab = BLMINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on OtherInfo_LimitTransactionsToPeriod...", Logger.MessageType.INF);
			Control BLMINFO_OtherInfo_LimitTransactionsToPeriod = new Control("OtherInfo_LimitTransactionsToPeriod", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='POP_FL']");
			CPCommon.AssertEqual(true,BLMINFO_OtherInfo_LimitTransactionsToPeriod.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_MainFormTab);
mTab = BLMINFO_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Global Withholding").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on GlobalWithholding_SubjectToWithholding...", Logger.MessageType.INF);
			Control BLMINFO_GlobalWithholding_SubjectToWithholding = new Control("GlobalWithholding_SubjectToWithholding", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SUBJ_BILL_WH_FL']");
			CPCommon.AssertEqual(true,BLMINFO_GlobalWithholding_SubjectToWithholding.Exists());

											Driver.SessionLogger.WriteLine("Details Levels");


												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on DetailLevelsLink...", Logger.MessageType.INF);
			Control BLMINFO_DetailLevelsLink = new Control("DetailLevelsLink", "ID", "lnk_1002237_PBM_PROJBILLINFO_HDR");
			CPCommon.AssertEqual(true,BLMINFO_DetailLevelsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_DetailLevelsLink);
BLMINFO_DetailLevelsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExist on DetailLevelsTable...", Logger.MessageType.INF);
			Control BLMINFO_DetailLevelsTable = new Control("DetailLevelsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_PROJBILLINFOSCH_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMINFO_DetailLevelsTable.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming Close on DetailLevelsForm...", Logger.MessageType.INF);
			Control BLMINFO_DetailLevelsForm = new Control("DetailLevelsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_PROJBILLINFOSCH_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMINFO_DetailLevelsForm);
IWebElement formBttn = BLMINFO_DetailLevelsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Cash Basis");


												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on CashBasisLink...", Logger.MessageType.INF);
			Control BLMINFO_CashBasisLink = new Control("CashBasisLink", "ID", "lnk_1002238_PBM_PROJBILLINFO_HDR");
			CPCommon.AssertEqual(true,BLMINFO_CashBasisLink.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_CashBasisLink);
BLMINFO_CashBasisLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExist on CashBasisTable...", Logger.MessageType.INF);
			Control BLMINFO_CashBasisTable = new Control("CashBasisTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_PROJCASHBASIS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMINFO_CashBasisTable.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming Close on CashBasisForm...", Logger.MessageType.INF);
			Control BLMINFO_CashBasisForm = new Control("CashBasisForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_PROJCASHBASIS_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMINFO_CashBasisForm);
formBttn = BLMINFO_CashBasisForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Schedule");


												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on ScheduleLink...", Logger.MessageType.INF);
			Control BLMINFO_ScheduleLink = new Control("ScheduleLink", "ID", "lnk_1002240_PBM_PROJBILLINFO_HDR");
			CPCommon.AssertEqual(true,BLMINFO_ScheduleLink.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_ScheduleLink);
BLMINFO_ScheduleLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExist on ScheduleTable...", Logger.MessageType.INF);
			Control BLMINFO_ScheduleTable = new Control("ScheduleTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_BILLSCHEDULE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMINFO_ScheduleTable.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming ClickButton on ScheduleForm...", Logger.MessageType.INF);
			Control BLMINFO_ScheduleForm = new Control("ScheduleForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_BILLSCHEDULE_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMINFO_ScheduleForm);
formBttn = BLMINFO_ScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMINFO_ScheduleForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMINFO_ScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.AssertEqual(true,BLMINFO_ScheduleForm.Exists());

													
				CPCommon.CurrentComponent = "BLMINFO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMINFO] Perfoming VerifyExists on Schedule_DateToBill...", Logger.MessageType.INF);
			Control BLMINFO_Schedule_DateToBill = new Control("Schedule_DateToBill", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_BILLSCHEDULE_CHILD_']/ancestor::form[1]/descendant::*[@id='BILL_SCHED_DT']");
			CPCommon.AssertEqual(true,BLMINFO_Schedule_DateToBill.Exists());

												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_ScheduleForm);
formBttn = BLMINFO_ScheduleForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLMINFO";
							CPCommon.WaitControlDisplayed(BLMINFO_MainForm);
formBttn = BLMINFO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

