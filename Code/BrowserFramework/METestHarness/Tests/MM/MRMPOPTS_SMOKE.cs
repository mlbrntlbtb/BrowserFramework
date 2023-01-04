 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRMPOPTS_SMOKE : TestScript
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
new Control("Material Requirements Planning", "xpath","//div[@class='deptItem'][.='Material Requirements Planning']").Click();
new Control("MRP Controls", "xpath","//div[@class='navItem'][.='MRP Controls']").Click();
new Control("Configure MRP Report Print Options", "xpath","//div[@class='navItem'][.='Configure MRP Report Print Options']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "MRMPOPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMPOPTS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MRMPOPTS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRMPOPTS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MRMPOPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMPOPTS] Perfoming VerifyExists on SummaryReportInquiryOptions_Weekly...", Logger.MessageType.INF);
			Control MRMPOPTS_SummaryReportInquiryOptions_Weekly = new Control("SummaryReportInquiryOptions_Weekly", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFNSUMWKSNO']");
			CPCommon.AssertEqual(true,MRMPOPTS_SummaryReportInquiryOptions_Weekly.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "MRMPOPTS";
							CPCommon.WaitControlDisplayed(MRMPOPTS_MainForm);
IWebElement formBttn = MRMPOPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

