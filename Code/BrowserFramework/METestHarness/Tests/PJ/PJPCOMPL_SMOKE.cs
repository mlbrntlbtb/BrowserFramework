 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCOMPL_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost and Revenue Processing Utilities", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Utilities']").Click();
new Control("Compare Project Ledger to General Ledger", "xpath","//div[@class='navItem'][.='Compare Project Ledger to General Ledger']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPCOMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPCOMPL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCOMPL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCOMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPL] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJPCOMPL_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJPCOMPL_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJPCOMPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOMPL] Perfoming VerifyExist on ChildForm...", Logger.MessageType.INF);
			Control PJPCOMPL_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPCOMPL_ZPJPCOMPLERRORS_CH_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCOMPL_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCOMPL";
							CPCommon.WaitControlDisplayed(PJPCOMPL_MainForm);
IWebElement formBttn = PJPCOMPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

