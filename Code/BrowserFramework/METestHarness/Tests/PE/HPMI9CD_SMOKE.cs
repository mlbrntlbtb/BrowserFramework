 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMI9CD_SMOKE : TestScript
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
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Personnel Controls", "xpath","//div[@class='navItem'][.='Personnel Controls']").Click();
new Control("Manage I-9 Codes", "xpath","//div[@class='navItem'][.='Manage I-9 Codes']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMI9CD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9CD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMI9CD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMI9CD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMI9CD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9CD] Perfoming VerifyExists on EffectiveDate...", Logger.MessageType.INF);
			Control HPMI9CD_EffectiveDate = new Control("EffectiveDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,HPMI9CD_EffectiveDate.Exists());

												
				CPCommon.CurrentComponent = "HPMI9CD";
							CPCommon.WaitControlDisplayed(HPMI9CD_MainForm);
IWebElement formBttn = HPMI9CD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMI9CD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMI9CD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMI9CD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9CD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMI9CD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMI9CD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMI9CD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9CD] Perfoming VerifyExist on I9CodesFormTable...", Logger.MessageType.INF);
			Control HPMI9CD_I9CodesFormTable = new Control("I9CodesFormTable", "xpath", "//div[starts-with(@id,'pr__HPMI9CD_I9CODES_DTL_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMI9CD_I9CodesFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HPMI9CD";
							CPCommon.WaitControlDisplayed(HPMI9CD_MainForm);
formBttn = HPMI9CD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

