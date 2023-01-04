 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APROPEN_SMOKE : TestScript
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
new Control("Accounts Payable Reports/Inquiries", "xpath","//div[@class='navItem'][.='Accounts Payable Reports/Inquiries']").Click();
new Control("Print Open Accounts Payable Report", "xpath","//div[@class='navItem'][.='Print Open Accounts Payable Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "APROPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APROPEN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APROPEN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APROPEN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APROPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APROPEN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APROPEN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APROPEN_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APROPEN";
							CPCommon.WaitControlDisplayed(APROPEN_MainForm);
IWebElement formBttn = APROPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APROPEN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APROPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APROPEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APROPEN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APROPEN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APROPEN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "APROPEN";
							CPCommon.WaitControlDisplayed(APROPEN_MainForm);
formBttn = APROPEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

