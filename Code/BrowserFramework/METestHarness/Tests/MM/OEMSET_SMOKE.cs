 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMSET_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Configure Sales Order Entry Settings", "xpath","//div[@class='navItem'][.='Configure Sales Order Entry Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSET] Perfoming VerifyExists on SalesOrders_OverallSystem...", Logger.MessageType.INF);
			Control OEMSET_SalesOrders_OverallSystem = new Control("SalesOrders_OverallSystem", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SO_OVERALL_ID_FL']");
			CPCommon.AssertEqual(true,OEMSET_SalesOrders_OverallSystem.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OEMSET";
							CPCommon.WaitControlDisplayed(OEMSET_MainForm);
IWebElement formBttn = OEMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

