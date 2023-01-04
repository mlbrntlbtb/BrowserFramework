 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPRUNCK_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Payment Processing", "xpath","//div[@class='navItem'][.='Payment Processing']").Click();
new Control("Manage Unusable Checks", "xpath","//div[@class='navItem'][.='Manage Unusable Checks']").Click();


												
				CPCommon.CurrentComponent = "APPRUNCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPRUNCK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPRUNCK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPRUNCK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPRUNCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPRUNCK] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APPRUNCK_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APPRUNCK_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APPRUNCK";
							CPCommon.WaitControlDisplayed(APPRUNCK_MainForm);
IWebElement formBttn = APPRUNCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APPRUNCK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APPRUNCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APPRUNCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPRUNCK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APPRUNCK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPRUNCK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APPRUNCK";
							CPCommon.WaitControlDisplayed(APPRUNCK_MainForm);
formBttn = APPRUNCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

