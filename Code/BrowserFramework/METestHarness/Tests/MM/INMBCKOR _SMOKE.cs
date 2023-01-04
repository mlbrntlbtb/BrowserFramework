 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMBCKOR__SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Controls", "xpath","//div[@class='navItem'][.='Inventory Controls']").Click();
new Control("Configure Back Order Processing Settings", "xpath","//div[@class='navItem'][.='Configure Back Order Processing Settings']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "INMBCKOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMBCKOR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMBCKOR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMBCKOR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMBCKOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMBCKOR] Perfoming VerifyExists on MainForm_DateFilter_NeedDateFilterStandardCalendarDays...", Logger.MessageType.INF);
			Control INMBCKOR_MainForm_DateFilter_NeedDateFilterStandardCalendarDays = new Control("MainForm_DateFilter_NeedDateFilterStandardCalendarDays", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NEED_DT_FILTER_NO']");
			CPCommon.AssertEqual(true,INMBCKOR_MainForm_DateFilter_NeedDateFilterStandardCalendarDays.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMBCKOR";
							CPCommon.WaitControlDisplayed(INMBCKOR_MainForm);
IWebElement formBttn = INMBCKOR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

