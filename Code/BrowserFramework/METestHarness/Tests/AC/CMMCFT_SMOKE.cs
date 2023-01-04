 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CMMCFT_SMOKE : TestScript
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
new Control("Cash Management Controls", "xpath","//div[@class='navItem'][.='Cash Management Controls']").Click();
new Control("Manage Cash Forecast Templates", "xpath","//div[@class='navItem'][.='Manage Cash Forecast Templates']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "CMMCFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control CMMCFT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,CMMCFT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "CMMCFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFT] Perfoming VerifyExists on TemplateCode...", Logger.MessageType.INF);
			Control CMMCFT_TemplateCode = new Control("TemplateCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TMPLT_CD']");
			CPCommon.AssertEqual(true,CMMCFT_TemplateCode.Exists());

												
				CPCommon.CurrentComponent = "CMMCFT";
							CPCommon.WaitControlDisplayed(CMMCFT_MainForm);
IWebElement formBttn = CMMCFT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? CMMCFT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
CMMCFT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "CMMCFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control CMMCFT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCFT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control CMMCFT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CMMCFT_CFTTMPLT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCFT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFT] Perfoming VerifyExist on AccountsFormTable...", Logger.MessageType.INF);
			Control CMMCFT_AccountsFormTable = new Control("AccountsFormTable", "xpath", "//div[starts-with(@id,'pr__CMMCFT_ACCT_CTW2_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCFT_AccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMCFT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMCFT] Perfoming VerifyExist on SelectedAccountsFormTable...", Logger.MessageType.INF);
			Control CMMCFT_SelectedAccountsFormTable = new Control("SelectedAccountsFormTable", "xpath", "//div[starts-with(@id,'pr__CMMCFT_ACCT_CTW3_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMCFT_SelectedAccountsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "CMMCFT";
							CPCommon.WaitControlDisplayed(CMMCFT_MainForm);
formBttn = CMMCFT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

