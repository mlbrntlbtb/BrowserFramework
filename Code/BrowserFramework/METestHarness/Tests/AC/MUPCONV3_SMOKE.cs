 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUPCONV3_SMOKE : TestScript
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
new Control("Multicurrency", "xpath","//div[@class='deptItem'][.='Multicurrency']").Click();
new Control("Multicurrency Utilities", "xpath","//div[@class='navItem'][.='Multicurrency Utilities']").Click();
new Control("Compute Future Value of Money", "xpath","//div[@class='navItem'][.='Compute Future Value of Money']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUPCONV3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPCONV3] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUPCONV3_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUPCONV3_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUPCONV3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPCONV3] Perfoming VerifyExists on Input_InitialValueAmount...", Logger.MessageType.INF);
			Control MUPCONV3_Input_InitialValueAmount = new Control("Input_InitialValueAmount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFN_INITIAL_VAL_AMT']");
			CPCommon.AssertEqual(true,MUPCONV3_Input_InitialValueAmount.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "MUPCONV3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPCONV3] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control MUPCONV3_ChildForm = new Control("ChildForm", "xpath", "//div[starts-with(@id,'pr__MUPCONV3_FVM_CTW_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,MUPCONV3_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "MUPCONV3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPCONV3] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control MUPCONV3_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[starts-with(@id,'pr__MUPCONV3_FVM_CTW_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUPCONV3_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUPCONV3";
							CPCommon.WaitControlDisplayed(MUPCONV3_MainForm);
IWebElement formBttn = MUPCONV3_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

