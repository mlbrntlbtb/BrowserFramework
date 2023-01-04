 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMBANK_SMOKE : TestScript
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
new Control("Manage Banks (US)", "xpath","//div[@class='navItem'][.='Manage Banks (US)']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLMBANK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBANK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMBANK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMBANK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMBANK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBANK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMBANK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMBANK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMBANK";
							CPCommon.WaitControlDisplayed(GLMBANK_MainForm);
IWebElement formBttn = GLMBANK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMBANK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMBANK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMBANK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMBANK] Perfoming VerifyExists on BankID...", Logger.MessageType.INF);
			Control GLMBANK_BankID = new Control("BankID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BANK_ABA_NO']");
			CPCommon.AssertEqual(true,GLMBANK_BankID.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "GLMBANK";
							CPCommon.WaitControlDisplayed(GLMBANK_MainForm);
formBttn = GLMBANK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

