 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRRVCHR_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Closing", "xpath","//div[@class='navItem'][.='Payroll Closing']").Click();
new Control("Print Accounts Payable Voucher Status Report", "xpath","//div[@class='navItem'][.='Print Accounts Payable Voucher Status Report']").Click();


												
				CPCommon.CurrentComponent = "PRRVCHR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRVCHR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRRVCHR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRRVCHR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRRVCHR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRVCHR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRRVCHR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRRVCHR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRRVCHR";
							CPCommon.WaitControlDisplayed(PRRVCHR_MainForm);
IWebElement formBttn = PRRVCHR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRRVCHR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRRVCHR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRRVCHR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRRVCHR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRRVCHR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRRVCHR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRRVCHR";
							CPCommon.WaitControlDisplayed(PRRVCHR_MainForm);
formBttn = PRRVCHR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

