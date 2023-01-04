 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HKMDED_SMOKE : TestScript
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
new Control("Deferred Compensation Admin", "xpath","//div[@class='deptItem'][.='Deferred Compensation Admin']").Click();
new Control("Deferred Compensation Plan Controls", "xpath","//div[@class='navItem'][.='Deferred Compensation Plan Controls']").Click();
new Control("Manage Deferred Compensation Deductions", "xpath","//div[@class='navItem'][.='Manage Deferred Compensation Deductions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HKMDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMDED] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HKMDED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HKMDED_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HKMDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMDED] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HKMDED_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HKMDED_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HKMDED";
							CPCommon.WaitControlDisplayed(HKMDED_MainForm);
IWebElement formBttn = HKMDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HKMDED_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HKMDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HKMDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HKMDED] Perfoming VerifyExists on Deduction...", Logger.MessageType.INF);
			Control HKMDED_Deduction = new Control("Deduction", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DED_CD']");
			CPCommon.AssertEqual(true,HKMDED_Deduction.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HKMDED";
							CPCommon.WaitControlDisplayed(HKMDED_MainForm);
formBttn = HKMDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

