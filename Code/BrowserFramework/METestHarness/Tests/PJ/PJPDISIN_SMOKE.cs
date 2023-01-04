 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPDISIN_SMOKE : TestScript
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
new Control("View Invalid Pools in Project Ledger", "xpath","//div[@class='navItem'][.='View Invalid Pools in Project Ledger']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJPDISIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPDISIN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPDISIN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPDISIN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPDISIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPDISIN] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJPDISIN_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJPDISIN_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJPDISIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPDISIN] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PJPDISIN_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPDISIN_ZPJPDISINERRORS_CH_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJPDISIN_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PJPDISIN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPDISIN] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PJPDISIN_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJPDISIN_ZPJPDISINERRORS_CH_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPDISIN_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJPDISIN";
							CPCommon.WaitControlDisplayed(PJPDISIN_MainForm);
IWebElement formBttn = PJPDISIN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

