 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPUPPWP_SMOKE : TestScript
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
new Control("Update Pay When Paid Voucher History", "xpath","//div[@class='navItem'][.='Update Pay When Paid Voucher History']").Click();


												
				CPCommon.CurrentComponent = "APPUPPWP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPUPPWP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPUPPWP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPUPPWP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPUPPWP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPUPPWP] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APPUPPWP_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APPUPPWP_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APPUPPWP";
							CPCommon.WaitControlDisplayed(APPUPPWP_MainForm);
IWebElement formBttn = APPUPPWP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APPUPPWP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APPUPPWP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APPUPPWP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPUPPWP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APPUPPWP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPUPPWP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APPUPPWP";
							CPCommon.WaitControlDisplayed(APPUPPWP_MainForm);
formBttn = APPUPPWP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

