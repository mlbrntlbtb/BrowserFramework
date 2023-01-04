 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class IWMEXPMP_SMOKE : TestScript
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
new Control("Inter-Company Work Orders", "xpath","//div[@class='deptItem'][.='Inter-Company Work Orders']").Click();
new Control("Inter-Company Work Orders Processing", "xpath","//div[@class='navItem'][.='Inter-Company Work Orders Processing']").Click();
new Control("Manage IWO Expense Mappings", "xpath","//div[@class='navItem'][.='Manage IWO Expense Mappings']").Click();


												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control IWMEXPMP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(IWMEXPMP_MainForm);
IWebElement formBttn = IWMEXPMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? IWMEXPMP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
IWMEXPMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage IWO Expense Mappings", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control IWMEXPMP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMEXPMP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
							CPCommon.WaitControlDisplayed(IWMEXPMP_MainForm);
formBttn = IWMEXPMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWMEXPMP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWMEXPMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "IWMEXPMP";
							CPCommon.AssertEqual(true,IWMEXPMP_MainForm.Exists());

													
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExists on ExpenseMappingCode...", Logger.MessageType.INF);
			Control IWMEXPMP_ExpenseMappingCode = new Control("ExpenseMappingCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='IWO_EXP_MAP_CD']");
			CPCommon.AssertEqual(true,IWMEXPMP_ExpenseMappingCode.Exists());

											Driver.SessionLogger.WriteLine("POSTING ACCOUNT");


												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExists on PostingAccountLink...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccountLink = new Control("PostingAccountLink", "ID", "lnk_1002328_IWMEXPMP_IWOEXPMAPPING_HDR");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccountLink.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExists on PostingAccountForm...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccountForm = new Control("PostingAccountForm", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMEXPMP_IWOEXPACCT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccountForm.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExists on PostingAccount_Ok...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccount_Ok = new Control("PostingAccount_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMEXPMP_IWOEXPACCT_CHLD_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccount_Ok.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExist on PostingAccountFormTable...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccountFormTable = new Control("PostingAccountFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMEXPMP_IWOEXPACCT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccountFormTable.Exists());

											Driver.SessionLogger.WriteLine("LINK EXPENSES");


												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExists on PostingAccount_LinkExpensesLink...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccount_LinkExpensesLink = new Control("PostingAccount_LinkExpensesLink", "ID", "lnk_3757_IWMEXPMP_IWOEXPACCT_CHLD");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccount_LinkExpensesLink.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
							CPCommon.WaitControlDisplayed(IWMEXPMP_PostingAccount_LinkExpensesLink);
IWMEXPMP_PostingAccount_LinkExpensesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExist on PostingAccount_LinkExpensesFormTable...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccount_LinkExpensesFormTable = new Control("PostingAccount_LinkExpensesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMEXPMP_IWOEXPACCTLN_EXPENSE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccount_LinkExpensesFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming ClickButton on PostingAccount_LinkExpensesForm...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccount_LinkExpensesForm = new Control("PostingAccount_LinkExpensesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMEXPMP_IWOEXPACCTLN_EXPENSE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(IWMEXPMP_PostingAccount_LinkExpensesForm);
formBttn = IWMEXPMP_PostingAccount_LinkExpensesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWMEXPMP_PostingAccount_LinkExpensesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWMEXPMP_PostingAccount_LinkExpensesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "IWMEXPMP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMEXPMP] Perfoming VerifyExists on PostingAccount_LinkExpenses_Account...", Logger.MessageType.INF);
			Control IWMEXPMP_PostingAccount_LinkExpenses_Account = new Control("PostingAccount_LinkExpenses_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMEXPMP_IWOEXPACCTLN_EXPENSE_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,IWMEXPMP_PostingAccount_LinkExpenses_Account.Exists());

												
				CPCommon.CurrentComponent = "IWMEXPMP";
							CPCommon.AssertEqual(true,IWMEXPMP_PostingAccount_LinkExpensesForm.Exists());

													
				CPCommon.CurrentComponent = "IWMEXPMP";
							CPCommon.WaitControlDisplayed(IWMEXPMP_PostingAccount_LinkExpensesForm);
formBttn = IWMEXPMP_PostingAccount_LinkExpensesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "IWMEXPMP";
							CPCommon.WaitControlDisplayed(IWMEXPMP_MainForm);
formBttn = IWMEXPMP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

