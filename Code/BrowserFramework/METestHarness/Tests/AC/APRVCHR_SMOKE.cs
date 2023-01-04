 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APRVCHR_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Voucher Processing", "xpath","//div[@class='navItem'][.='Voucher Processing']").Click();
new Control("Print Voucher Edit Report", "xpath","//div[@class='navItem'][.='Print Voucher Edit Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "APRVCHR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRVCHR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APRVCHR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APRVCHR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APRVCHR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRVCHR] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APRVCHR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APRVCHR_MainForm);
IWebElement formBttn = APRVCHR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APRVCHR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APRVCHR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "APRVCHR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APRVCHR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APRVCHR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APRVCHR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "APRVCHR";
							CPCommon.WaitControlDisplayed(APRVCHR_MainForm);
formBttn = APRVCHR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

