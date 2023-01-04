 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RCMREJCD_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Receiving", "xpath","//div[@class='deptItem'][.='Receiving']").Click();
new Control("Receiving Controls", "xpath","//div[@class='navItem'][.='Receiving Controls']").Click();
new Control("Manage Vendor Rejection Reasons", "xpath","//div[@class='navItem'][.='Manage Vendor Rejection Reasons']").Click();


												
				CPCommon.CurrentComponent = "RCMREJCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMREJCD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RCMREJCD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RCMREJCD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RCMREJCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCMREJCD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RCMREJCD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCMREJCD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RCMREJCD";
							CPCommon.WaitControlDisplayed(RCMREJCD_MainForm);
IWebElement formBttn = RCMREJCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

