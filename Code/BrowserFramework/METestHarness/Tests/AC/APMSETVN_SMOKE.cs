 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMSETVN_SMOKE : TestScript
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
new Control("Vendor and Subcontractor Controls", "xpath","//div[@class='navItem'][.='Vendor and Subcontractor Controls']").Click();
new Control("Configure Vendor Settings", "xpath","//div[@class='navItem'][.='Configure Vendor Settings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "APMSETVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMSETVN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMSETVN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMSETVN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMSETVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMSETVN] Perfoming VerifyExists on EnableAutoAssign...", Logger.MessageType.INF);
			Control APMSETVN_EnableAutoAssign = new Control("EnableAutoAssign", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='VEND_AUTO_ASSG_FL']");
			CPCommon.AssertEqual(true,APMSETVN_EnableAutoAssign.Exists());

												
				CPCommon.CurrentComponent = "APMSETVN";
							CPCommon.WaitControlDisplayed(APMSETVN_MainForm);
IWebElement formBttn = APMSETVN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMSETVN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMSETVN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMSETVN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMSETVN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMSETVN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMSETVN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMSETVN";
							CPCommon.WaitControlDisplayed(APMSETVN_MainForm);
formBttn = APMSETVN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

