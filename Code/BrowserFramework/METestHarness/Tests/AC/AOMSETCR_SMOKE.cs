 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMSETCR_SMOKE : TestScript
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
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Accounts Receivable Interfaces", "xpath","//div[@class='navItem'][.='Accounts Receivable Interfaces']").Click();
new Control("Manage Cash Receipts Preprocessor Settings", "xpath","//div[@class='navItem'][.='Manage Cash Receipts Preprocessor Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "AOMSETCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSETCR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMSETCR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMSETCR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMSETCR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMSETCR] Perfoming VerifyExists on EDIDelimiters_Segment...", Logger.MessageType.INF);
			Control AOMSETCR_EDIDelimiters_Segment = new Control("EDIDelimiters_Segment", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEGMENT_DELIMITER_CD']");
			CPCommon.AssertEqual(true,AOMSETCR_EDIDelimiters_Segment.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "AOMSETCR";
							CPCommon.WaitControlDisplayed(AOMSETCR_MainForm);
IWebElement formBttn = AOMSETCR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

