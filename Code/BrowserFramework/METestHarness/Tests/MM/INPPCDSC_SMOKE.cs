 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INPPCDSC_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Physical Counts", "xpath","//div[@class='navItem'][.='Physical Counts']").Click();
new Control("Create Physical Count Adjustments", "xpath","//div[@class='navItem'][.='Create Physical Count Adjustments']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "INPPCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPPCDSC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INPPCDSC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INPPCDSC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INPPCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPPCDSC] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control INPPCDSC_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,INPPCDSC_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "INPPCDSC";
							CPCommon.WaitControlDisplayed(INPPCDSC_MainForm);
IWebElement formBttn = INPPCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INPPCDSC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INPPCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INPPCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPPCDSC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INPPCDSC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INPPCDSC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "INPPCDSC";
							CPCommon.WaitControlDisplayed(INPPCDSC_MainForm);
formBttn = INPPCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INPPCDSC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INPPCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Accounting Period");


												
				CPCommon.CurrentComponent = "INPPCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPPCDSC] Perfoming VerifyExists on MainForm_AccountingPeriodLink...", Logger.MessageType.INF);
			Control INPPCDSC_MainForm_AccountingPeriodLink = new Control("MainForm_AccountingPeriodLink", "ID", "lnk_1004890_INPPCDSC_PARAM");
			CPCommon.AssertEqual(true,INPPCDSC_MainForm_AccountingPeriodLink.Exists());

												
				CPCommon.CurrentComponent = "INPPCDSC";
							CPCommon.WaitControlDisplayed(INPPCDSC_MainForm_AccountingPeriodLink);
INPPCDSC_MainForm_AccountingPeriodLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INPPCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPPCDSC] Perfoming VerifyExists on AccountingPeriodForm...", Logger.MessageType.INF);
			Control INPPCDSC_AccountingPeriodForm = new Control("AccountingPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INPPCDSC_AccountingPeriodForm.Exists());

												
				CPCommon.CurrentComponent = "INPPCDSC";
							CPCommon.WaitControlDisplayed(INPPCDSC_AccountingPeriodForm);
formBttn = INPPCDSC_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? INPPCDSC_AccountingPeriodForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
INPPCDSC_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "INPPCDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INPPCDSC] Perfoming VerifyExist on AccountingPeriodFormTable...", Logger.MessageType.INF);
			Control INPPCDSC_AccountingPeriodFormTable = new Control("AccountingPeriodFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INPPCDSC_AccountingPeriodFormTable.Exists());

												
				CPCommon.CurrentComponent = "INPPCDSC";
							CPCommon.WaitControlDisplayed(INPPCDSC_AccountingPeriodForm);
formBttn = INPPCDSC_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "INPPCDSC";
							CPCommon.WaitControlDisplayed(INPPCDSC_MainForm);
formBttn = INPPCDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

