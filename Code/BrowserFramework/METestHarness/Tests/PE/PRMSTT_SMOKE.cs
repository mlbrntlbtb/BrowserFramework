 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMSTT_SMOKE : TestScript
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
new Control("State Taxes", "xpath","//div[@class='navItem'][.='State Taxes']").Click();
new Control("Manage State Tax Tables", "xpath","//div[@class='navItem'][.='Manage State Tax Tables']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMSTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMSTT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMSTT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMSTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTT] Perfoming VerifyExists on State...", Logger.MessageType.INF);
			Control PRMSTT_State = new Control("State", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STATE_CD']");
			CPCommon.AssertEqual(true,PRMSTT_State.Exists());

												
				CPCommon.CurrentComponent = "PRMSTT";
							CPCommon.WaitControlDisplayed(PRMSTT_MainForm);
IWebElement formBttn = PRMSTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMSTT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMSTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMSTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMSTT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("TaxTable Form");


												
				CPCommon.CurrentComponent = "PRMSTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTT] Perfoming ClickButton on TaxTableForm...", Logger.MessageType.INF);
			Control PRMSTT_TaxTableForm = new Control("TaxTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTT_STATETAXTBL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PRMSTT_TaxTableForm);
formBttn = PRMSTT_TaxTableForm.mElement.FindElements(By.CssSelector("*[title*='New']")).Count <= 0 ? PRMSTT_TaxTableForm.mElement.FindElements(By.XPath(".//*[contains(text(),'New')]")).FirstOrDefault() :
PRMSTT_TaxTableForm.mElement.FindElements(By.CssSelector("*[title*='New']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" New not found ");


												
				CPCommon.CurrentComponent = "PRMSTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMSTT] Perfoming VerifyExist on TaxTableTable...", Logger.MessageType.INF);
			Control PRMSTT_TaxTableTable = new Control("TaxTableTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMSTT_STATETAXTBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMSTT_TaxTableTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMSTT";
							CPCommon.WaitControlDisplayed(PRMSTT_MainForm);
formBttn = PRMSTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

