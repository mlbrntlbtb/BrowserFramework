 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBDPER_SMOKE : TestScript
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
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Period Budgets", "xpath","//div[@class='navItem'][.='Period Budgets']").Click();
new Control("Manage Project Budgets By Period", "xpath","//div[@class='navItem'][.='Manage Project Budgets By Period']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBDPER_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMBDPER_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMBDPER_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_MainForm);
IWebElement formBttn = PJMBDPER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMBDPER_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMBDPER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMBDPER_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDPER_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExist on DirectFormTable...", Logger.MessageType.INF);
			Control PJMBDPER_DirectFormTable = new Control("DirectFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_DIRECT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDPER_DirectFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming ClickButton on DirectForm...", Logger.MessageType.INF);
			Control PJMBDPER_DirectForm = new Control("DirectForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_DIRECT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBDPER_DirectForm);
formBttn = PJMBDPER_DirectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBDPER_DirectForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBDPER_DirectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.AssertEqual(true,PJMBDPER_DirectForm.Exists());

													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_AccountInfo_Account...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_AccountInfo_Account = new Control("Direct_AccountInfo_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_DIRECT_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_AccountInfo_Account.Exists());

											Driver.SessionLogger.WriteLine("Indirect");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_IndirectLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_IndirectLink = new Control("Direct_IndirectLink", "ID", "lnk_1001914_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_IndirectLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_IndirectLink);
PJMBDPER_Direct_IndirectLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExist on Direct_IndirectFormTable...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_IndirectFormTable = new Control("Direct_IndirectFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDIND_INDIRECT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_IndirectFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming ClickButton on Direct_IndirectForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_IndirectForm = new Control("Direct_IndirectForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDIND_INDIRECT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBDPER_Direct_IndirectForm);
formBttn = PJMBDPER_Direct_IndirectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBDPER_Direct_IndirectForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBDPER_Direct_IndirectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.AssertEqual(true,PJMBDPER_Direct_IndirectForm.Exists());

													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_Indirect_Identification_PoolNumber...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_Indirect_Identification_PoolNumber = new Control("Direct_Indirect_Identification_PoolNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDIND_INDIRECT_']/ancestor::form[1]/descendant::*[@id='POOL_NO']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_Indirect_Identification_PoolNumber.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_IndirectForm);
formBttn = PJMBDPER_Direct_IndirectForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Total Budget");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_TotalBudgetLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_TotalBudgetLink = new Control("Direct_TotalBudgetLink", "ID", "lnk_1001915_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_TotalBudgetLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_TotalBudgetLink);
PJMBDPER_Direct_TotalBudgetLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_TotalBudgetForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_TotalBudgetForm = new Control("Direct_TotalBudgetForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDFEE_TOTALBUDG_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_TotalBudgetForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_TotalBudget_ManualFeeOverride_Fee...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_TotalBudget_ManualFeeOverride_Fee = new Control("Direct_TotalBudget_ManualFeeOverride_Fee", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDFEE_TOTALBUDG_']/ancestor::form[1]/descendant::*[@id='DFLT_FEE_RT']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_TotalBudget_ManualFeeOverride_Fee.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_TotalBudgetForm);
formBttn = PJMBDPER_Direct_TotalBudgetForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Adjust Actuals");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_AdjustActualsLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_AdjustActualsLink = new Control("Direct_AdjustActualsLink", "ID", "lnk_1001920_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_AdjustActualsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_AdjustActualsLink);
PJMBDPER_Direct_AdjustActualsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_AdjustActualsForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_AdjustActualsForm = new Control("Direct_AdjustActualsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_ACTUALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_AdjustActualsForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_AdjustActuals_SelectAccounts_RangeOption...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_AdjustActuals_SelectAccounts_RangeOption = new Control("Direct_AdjustActuals_SelectAccounts_RangeOption", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_ACTUALS_']/ancestor::form[1]/descendant::*[@id='ACCT_RANGE_CD']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_AdjustActuals_SelectAccounts_RangeOption.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_AdjustActualsForm);
formBttn = PJMBDPER_Direct_AdjustActualsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Spread");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_SpreadLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_SpreadLink = new Control("Direct_SpreadLink", "ID", "lnk_1001921_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_SpreadLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_SpreadLink);
PJMBDPER_Direct_SpreadLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_SpreadForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_SpreadForm = new Control("Direct_SpreadForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_SPREAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_SpreadForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_Spread_SelectAccounts_RangeOption...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_Spread_SelectAccounts_RangeOption = new Control("Direct_Spread_SelectAccounts_RangeOption", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_SPREAD_']/ancestor::form[1]/descendant::*[@id='ACCT_RANGE_CD']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_Spread_SelectAccounts_RangeOption.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_SpreadForm);
formBttn = PJMBDPER_Direct_SpreadForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Fill");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_FillLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_FillLink = new Control("Direct_FillLink", "ID", "lnk_1001922_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_FillLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_FillLink);
PJMBDPER_Direct_FillLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_FillForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_FillForm = new Control("Direct_FillForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_FILL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_FillForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_Fill_SelectAccounts_RangeOption...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_Fill_SelectAccounts_RangeOption = new Control("Direct_Fill_SelectAccounts_RangeOption", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_FILL_']/ancestor::form[1]/descendant::*[@id='ACCT_RANGE_CD']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_Fill_SelectAccounts_RangeOption.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_FillForm);
formBttn = PJMBDPER_Direct_FillForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Move");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_MoveLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_MoveLink = new Control("Direct_MoveLink", "ID", "lnk_1001923_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_MoveLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_MoveLink);
PJMBDPER_Direct_MoveLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_MoveForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_MoveForm = new Control("Direct_MoveForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_MOVE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_MoveForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_Move_SelectAccounts_RangeOption...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_Move_SelectAccounts_RangeOption = new Control("Direct_Move_SelectAccounts_RangeOption", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_MOVE_']/ancestor::form[1]/descendant::*[@id='ACCT_RANGE_CD']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_Move_SelectAccounts_RangeOption.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_MoveForm);
formBttn = PJMBDPER_Direct_MoveForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Escalate");


												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_EscalateLink...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_EscalateLink = new Control("Direct_EscalateLink", "ID", "lnk_1001924_PJMBDPER_PROJBUDDIR_DIRECT");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_EscalateLink.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_EscalateLink);
PJMBDPER_Direct_EscalateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_EscalateForm...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_EscalateForm = new Control("Direct_EscalateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_ESCALATE_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_EscalateForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBDPER] Perfoming VerifyExists on Direct_Escalate_SelectAccounts_RangeOption...", Logger.MessageType.INF);
			Control PJMBDPER_Direct_Escalate_SelectAccounts_RangeOption = new Control("Direct_Escalate_SelectAccounts_RangeOption", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBDPER_PROJBUDDIR_ESCALATE_']/ancestor::form[1]/descendant::*[@id='ACCT_RANGE_CD']");
			CPCommon.AssertEqual(true,PJMBDPER_Direct_Escalate_SelectAccounts_RangeOption.Exists());

												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_Direct_EscalateForm);
formBttn = PJMBDPER_Direct_EscalateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMBDPER";
							CPCommon.WaitControlDisplayed(PJMBDPER_MainForm);
formBttn = PJMBDPER_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

