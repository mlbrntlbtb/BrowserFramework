 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMELSET_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Defaults", "xpath","//div[@class='navItem'][.='Timesheet Defaults']").Click();
new Control("Manage Employee Timesheet Line Type Defaults", "xpath","//div[@class='navItem'][.='Manage Employee Timesheet Line Type Defaults']").Click();


												
				CPCommon.CurrentComponent = "LDMELSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMELSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMELSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMELSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMELSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMELSET] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control LDMELSET_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,LDMELSET_Employee.Exists());

												
				CPCommon.CurrentComponent = "LDMELSET";
							CPCommon.WaitControlDisplayed(LDMELSET_MainForm);
IWebElement formBttn = LDMELSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMELSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMELSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMELSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMELSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMELSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMELSET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMELSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMELSET] Perfoming VerifyExists on DetailsForm...", Logger.MessageType.INF);
			Control LDMELSET_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMELSET_EMPLTSLNTYPE_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMELSET_DetailsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMELSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMELSET] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control LDMELSET_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMELSET_EMPLTSLNTYPE_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMELSET_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMELSET";
							CPCommon.WaitControlDisplayed(LDMELSET_DetailsForm);
formBttn = LDMELSET_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMELSET_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMELSET_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMELSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMELSET] Perfoming VerifyExists on Details_PayType...", Logger.MessageType.INF);
			Control LDMELSET_Details_PayType = new Control("Details_PayType", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMELSET_EMPLTSLNTYPE_CHLD_']/ancestor::form[1]/descendant::*[@id='DFLT_PAY_TYPE']");
			CPCommon.AssertEqual(true,LDMELSET_Details_PayType.Exists());

												
				CPCommon.CurrentComponent = "LDMELSET";
							CPCommon.WaitControlDisplayed(LDMELSET_DetailsForm);
formBttn = LDMELSET_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

