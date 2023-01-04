 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPYTM_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project History", "xpath","//div[@class='navItem'][.='Project History']").Click();
new Control("Manage Prior Year Time and Materials Revenue", "xpath","//div[@class='navItem'][.='Manage Prior Year Time and Materials Revenue']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPYTM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYTM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPYTM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPYTM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPYTM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYTM] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control PJMPYTM_FiscalYear = new Control("FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,PJMPYTM_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "PJMPYTM";
							CPCommon.WaitControlDisplayed(PJMPYTM_MainForm);
IWebElement formBttn = PJMPYTM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPYTM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPYTM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPYTM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYTM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPYTM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYTM_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DETAILS");


												
				CPCommon.CurrentComponent = "PJMPYTM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYTM] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control PJMPYTM_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYTM_PYPROJLABHS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYTM_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPYTM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYTM] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control PJMPYTM_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYTM_PYPROJLABHS_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPYTM_DetailsForm);
formBttn = PJMPYTM_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPYTM_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPYTM_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPYTM";
							CPCommon.AssertEqual(true,PJMPYTM_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPYTM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYTM] Perfoming VerifyExists on Details_Account...", Logger.MessageType.INF);
			Control PJMPYTM_Details_Account = new Control("Details_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYTM_PYPROJLABHS_CHILD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PJMPYTM_Details_Account.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMPYTM";
							CPCommon.WaitControlDisplayed(PJMPYTM_MainForm);
formBttn = PJMPYTM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

