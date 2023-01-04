 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMLSD_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Local Taxes", "xpath","//div[@class='navItem'][.='Local Taxes']").Click();
new Control("Manage Local Standard Deductions", "xpath","//div[@class='navItem'][.='Manage Local Standard Deductions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMLSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLSD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMLSD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMLSD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLSD] Perfoming VerifyExists on Locality...", Logger.MessageType.INF);
			Control PRMLSD_Locality = new Control("Locality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LOCAL_CD']");
			CPCommon.AssertEqual(true,PRMLSD_Locality.Exists());

												
				CPCommon.CurrentComponent = "PRMLSD";
							CPCommon.WaitControlDisplayed(PRMLSD_MainForm);
IWebElement formBttn = PRMLSD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMLSD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMLSD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMLSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLSD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMLSD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMLSD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "PRMLSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLSD] Perfoming VerifyExists on LocalStandardDeductionsForm...", Logger.MessageType.INF);
			Control PRMLSD_LocalStandardDeductionsForm = new Control("LocalStandardDeductionsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLSD_LOCSTDDED_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMLSD_LocalStandardDeductionsForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLSD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLSD] Perfoming VerifyExist on LocalStandardDeductionsFormTable...", Logger.MessageType.INF);
			Control PRMLSD_LocalStandardDeductionsFormTable = new Control("LocalStandardDeductionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLSD_LOCSTDDED_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMLSD_LocalStandardDeductionsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMLSD";
							CPCommon.WaitControlDisplayed(PRMLSD_MainForm);
formBttn = PRMLSD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

