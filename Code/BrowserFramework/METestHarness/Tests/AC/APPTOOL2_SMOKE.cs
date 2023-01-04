 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APPTOOL2_SMOKE : TestScript
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
new Control("Accounts Payable Utilities", "xpath","//div[@class='navItem'][.='Accounts Payable Utilities']").Click();
new Control("Update Vendor Address Line Information", "xpath","//div[@class='navItem'][.='Update Vendor Address Line Information']").Click();


												
				CPCommon.CurrentComponent = "APPTOOL2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL2] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APPTOOL2_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APPTOOL2_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL2] Perfoming VerifyExists on Option_Vendor...", Logger.MessageType.INF);
			Control APPTOOL2_Option_Vendor = new Control("Option_Vendor", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VENDOR_RANGE']");
			CPCommon.AssertEqual(true,APPTOOL2_Option_Vendor.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL2";
							CPCommon.WaitControlDisplayed(APPTOOL2_MainForm);
IWebElement formBttn = APPTOOL2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APPTOOL2_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APPTOOL2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APPTOOL2";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APPTOOL2] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APPTOOL2_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APPTOOL2_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APPTOOL2";
							CPCommon.WaitControlDisplayed(APPTOOL2_MainForm);
formBttn = APPTOOL2_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

