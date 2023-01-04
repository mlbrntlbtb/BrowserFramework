 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CMMIB_SMOKE : TestScript
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
new Control("Cash Management", "xpath","//div[@class='deptItem'][.='Cash Management']").Click();
new Control("Cash Management Controls", "xpath","//div[@class='navItem'][.='Cash Management Controls']").Click();
new Control("Manage Intermediary Banks", "xpath","//div[@class='navItem'][.='Manage Intermediary Banks']").Click();


											Driver.SessionLogger.WriteLine("Main form");


												
				CPCommon.CurrentComponent = "CMMIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMIB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control CMMIB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,CMMIB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "CMMIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMIB] Perfoming VerifyExists on IntermediaryBankID...", Logger.MessageType.INF);
			Control CMMIB_IntermediaryBankID = new Control("IntermediaryBankID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='IB_BANK_ID']");
			CPCommon.AssertEqual(true,CMMIB_IntermediaryBankID.Exists());

												
				CPCommon.CurrentComponent = "CMMIB";
							CPCommon.WaitControlDisplayed(CMMIB_MainForm);
IWebElement formBttn = CMMIB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? CMMIB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
CMMIB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "CMMIB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMMIB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control CMMIB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMMIB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMMIB";
							CPCommon.WaitControlDisplayed(CMMIB_MainForm);
formBttn = CMMIB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

