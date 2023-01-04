 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMBDORG_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Budgets", "xpath","//div[@class='navItem'][.='General Ledger Budgets']").Click();
new Control("Manage Organization/Account Budgets", "xpath","//div[@class='navItem'][.='Manage Organization/Account Budgets']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMBDORG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMBDORG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_MainForm);
IWebElement formBttn = GLMBDORG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMBDORG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMBDORG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on Organization...", Logger.MessageType.INF);
			Control GLMBDORG_Organization = new Control("Organization", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,GLMBDORG_Organization.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_MainForm);
formBttn = GLMBDORG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMBDORG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMBDORG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMBDORG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBDORG_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExist on BudgetsByPeriodFormTable...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriodFormTable = new Control("BudgetsByPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming ClickButtonIfExists on BudgetsByPeriodForm...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriodForm = new Control("BudgetsByPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriodForm);
formBttn = GLMBDORG_BudgetsByPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMBDORG_BudgetsByPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMBDORG_BudgetsByPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriodForm.Exists());

													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_Account...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_Account = new Control("BudgetsByPeriod_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_CTW_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_Account.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_AdjustActualsLink...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_AdjustActualsLink = new Control("BudgetsByPeriod_AdjustActualsLink", "ID", "lnk_1000923_GLMBDORG_ORGACCTBUD_CTW");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_AdjustActualsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_EscalateLink...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_EscalateLink = new Control("BudgetsByPeriod_EscalateLink", "ID", "lnk_1000754_GLMBDORG_ORGACCTBUD_CTW");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_EscalateLink.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_FillLink...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_FillLink = new Control("BudgetsByPeriod_FillLink", "ID", "lnk_1000756_GLMBDORG_ORGACCTBUD_CTW");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_FillLink.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_MoveLink...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_MoveLink = new Control("BudgetsByPeriod_MoveLink", "ID", "lnk_1000755_GLMBDORG_ORGACCTBUD_CTW");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_MoveLink.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_SpreadLink...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_SpreadLink = new Control("BudgetsByPeriod_SpreadLink", "ID", "lnk_1000757_GLMBDORG_ORGACCTBUD_CTW");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_SpreadLink.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_AdjustActualsLink);
GLMBDORG_BudgetsByPeriod_AdjustActualsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_AdjustActuals_AdjustToActualsPeriodRange_From...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_AdjustActuals_AdjustToActualsPeriodRange_From = new Control("BudgetsByPeriod_AdjustActuals_AdjustToActualsPeriodRange_From", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_ACTUALS_']/ancestor::form[1]/descendant::*[@id='PD_FR']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_AdjustActuals_AdjustToActualsPeriodRange_From.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming Close on BudgetsByPeriod_AdjustActualsForm...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_AdjustActualsForm = new Control("BudgetsByPeriod_AdjustActualsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_ACTUALS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_AdjustActualsForm);
formBttn = GLMBDORG_BudgetsByPeriod_AdjustActualsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_SpreadLink);
GLMBDORG_BudgetsByPeriod_SpreadLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_Spread_SelectAccounts_From...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_Spread_SelectAccounts_From = new Control("BudgetsByPeriod_Spread_SelectAccounts_From", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_SPREAD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID_FR']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_Spread_SelectAccounts_From.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming Close on BudgetsByPeriod_SpreadForm...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_SpreadForm = new Control("BudgetsByPeriod_SpreadForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_SPREAD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_SpreadForm);
formBttn = GLMBDORG_BudgetsByPeriod_SpreadForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_FillLink);
GLMBDORG_BudgetsByPeriod_FillLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_Fill_Fill_Amount...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_Fill_Fill_Amount = new Control("BudgetsByPeriod_Fill_Fill_Amount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_FILL_']/ancestor::form[1]/descendant::*[@id='FILL_AMT']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_Fill_Fill_Amount.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming Close on BudgetsByPeriod_FillForm...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_FillForm = new Control("BudgetsByPeriod_FillForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_FILL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_FillForm);
formBttn = GLMBDORG_BudgetsByPeriod_FillForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_MoveLink);
GLMBDORG_BudgetsByPeriod_MoveLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_Move_SelectAccounts_From...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_Move_SelectAccounts_From = new Control("BudgetsByPeriod_Move_SelectAccounts_From", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_MOVE_']/ancestor::form[1]/descendant::*[@id='ACCT_ID_FR']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_Move_SelectAccounts_From.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming Close on BudgetsByPeriod_MoveForm...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_MoveForm = new Control("BudgetsByPeriod_MoveForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_MOVE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_MoveForm);
formBttn = GLMBDORG_BudgetsByPeriod_MoveForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_EscalateLink);
GLMBDORG_BudgetsByPeriod_EscalateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming VerifyExists on BudgetsByPeriod_Escalate_Escalation_Amount...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_Escalate_Escalation_Amount = new Control("BudgetsByPeriod_Escalate_Escalation_Amount", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_ESCALATE_']/ancestor::form[1]/descendant::*[@id='ESC_PCT_AMT']");
			CPCommon.AssertEqual(true,GLMBDORG_BudgetsByPeriod_Escalate_Escalation_Amount.Exists());

												
				CPCommon.CurrentComponent = "GLMBDORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBDORG] Perfoming Close on BudgetsByPeriod_EscalateForm...", Logger.MessageType.INF);
			Control GLMBDORG_BudgetsByPeriod_EscalateForm = new Control("BudgetsByPeriod_EscalateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMBDORG_ORGACCTBUD_ESCALATE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMBDORG_BudgetsByPeriod_EscalateForm);
formBttn = GLMBDORG_BudgetsByPeriod_EscalateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "GLMBDORG";
							CPCommon.WaitControlDisplayed(GLMBDORG_MainForm);
formBttn = GLMBDORG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

