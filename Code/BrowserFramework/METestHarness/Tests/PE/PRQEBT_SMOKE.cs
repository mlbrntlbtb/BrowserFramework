 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQEBT_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Reports/Inquiries", "xpath","//div[@class='navItem'][.='Payroll Reports/Inquiries']").Click();
new Control("View Bank Information", "xpath","//div[@class='navItem'][.='View Bank Information']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQEBT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQEBT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQEBT_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQEBT_Identification_Employee.Exists());

											Driver.SessionLogger.WriteLine("Employee Bank Header Form");


												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeaderForm...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeaderForm = new Control("EmployeeBankHeaderForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKHDRADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeaderForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExist on EmployeeBankHeaderFormTable...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeaderFormTable = new Control("EmployeeBankHeaderFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKHDRADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeaderFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeaderForm);
IWebElement formBttn = PRQEBT_EmployeeBankHeaderForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQEBT_EmployeeBankHeaderForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQEBT_EmployeeBankHeaderForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_Transaction_TransactionType...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_Transaction_TransactionType = new Control("EmployeeBankHeader_Transaction_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKHDRADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_Transaction_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Active Bank Acct Form");


												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_ActiveBankAcctLink...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_ActiveBankAcctLink = new Control("EmployeeBankHeader_ActiveBankAcctLink", "ID", "lnk_5472_PRQEBT_EMPLBANKHDRADT_CTW");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_ActiveBankAcctLink.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeader_ActiveBankAcctLink);
PRQEBT_EmployeeBankHeader_ActiveBankAcctLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_ActiveBankAccountsForm...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm = new Control("EmployeeBankHeader_ActiveBankAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKLNADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExist on EmployeeBankHeader_ActiveBankAccountsFormTable...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_ActiveBankAccountsFormTable = new Control("EmployeeBankHeader_ActiveBankAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKLNADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_ActiveBankAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm);
formBttn = PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_ActiveBankAccounts_Transaction_TransactionType...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_ActiveBankAccounts_Transaction_TransactionType = new Control("EmployeeBankHeader_ActiveBankAccounts_Transaction_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKLNADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_ActiveBankAccounts_Transaction_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm);
formBttn = PRQEBT_EmployeeBankHeader_ActiveBankAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Pending Bank Acct Form");


												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_PendingBankAcctLink...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_PendingBankAcctLink = new Control("EmployeeBankHeader_PendingBankAcctLink", "ID", "lnk_5473_PRQEBT_EMPLBANKHDRADT_CTW");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_PendingBankAcctLink.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeader_PendingBankAcctLink);
PRQEBT_EmployeeBankHeader_PendingBankAcctLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_PendingBankAccountsForm...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_PendingBankAccountsForm = new Control("EmployeeBankHeader_PendingBankAccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKPENDADT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_PendingBankAccountsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExist on EmployeeBankHeader_PendingBankAccountsFormTable...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_PendingBankAccountsFormTable = new Control("EmployeeBankHeader_PendingBankAccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKPENDADT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_PendingBankAccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeader_PendingBankAccountsForm);
formBttn = PRQEBT_EmployeeBankHeader_PendingBankAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQEBT_EmployeeBankHeader_PendingBankAccountsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQEBT_EmployeeBankHeader_PendingBankAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQEBT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEBT] Perfoming VerifyExists on EmployeeBankHeader_PendingBankAccounts_Transaction_TransactionType...", Logger.MessageType.INF);
			Control PRQEBT_EmployeeBankHeader_PendingBankAccounts_Transaction_TransactionType = new Control("EmployeeBankHeader_PendingBankAccounts_Transaction_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEBT_EMPLBANKPENDADT_CTW_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQEBT_EmployeeBankHeader_PendingBankAccounts_Transaction_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_EmployeeBankHeader_PendingBankAccountsForm);
formBttn = PRQEBT_EmployeeBankHeader_PendingBankAccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQEBT";
							CPCommon.WaitControlDisplayed(PRQEBT_MainForm);
formBttn = PRQEBT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

