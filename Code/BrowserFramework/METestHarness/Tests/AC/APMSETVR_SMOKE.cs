 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMSETVR_SMOKE : TestScript
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
new Control("Accounts Payable Controls", "xpath","//div[@class='navItem'][.='Accounts Payable Controls']").Click();
new Control("Configure Accounts Payable Voucher Settings", "xpath","//div[@class='navItem'][.='Configure Accounts Payable Voucher Settings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "APMSETVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMSETVR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMSETVR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMSETVR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMSETVR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMSETVR] Perfoming VerifyExists on System...", Logger.MessageType.INF);
			Control APMSETVR_System = new Control("System", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_VCHR_NO_MTHD_CD' and @value='S']");
			CPCommon.AssertEqual(true,APMSETVR_System.Exists());

												
				CPCommon.CurrentComponent = "APMSETVR";
							CPCommon.WaitControlDisplayed(APMSETVR_MainForm);
IWebElement formBttn = APMSETVR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

