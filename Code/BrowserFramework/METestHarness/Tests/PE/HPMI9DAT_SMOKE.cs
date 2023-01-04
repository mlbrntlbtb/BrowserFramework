 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMI9DAT_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee HR Information", "xpath","//div[@class='navItem'][.='Employee HR Information']").Click();
new Control("Manage Employee I-9 Data", "xpath","//div[@class='navItem'][.='Manage Employee I-9 Data']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "HPMI9DAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9DAT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMI9DAT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMI9DAT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMI9DAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9DAT] Perfoming VerifyExists on EmployeeName...", Logger.MessageType.INF);
			Control HPMI9DAT_EmployeeName = new Control("EmployeeName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LAST_FIRST_NAME']");
			CPCommon.AssertEqual(true,HPMI9DAT_EmployeeName.Exists());

												
				CPCommon.CurrentComponent = "HPMI9DAT";
							CPCommon.WaitControlDisplayed(HPMI9DAT_MainForm);
IWebElement formBttn = HPMI9DAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMI9DAT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMI9DAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMI9DAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9DAT] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control HPMI9DAT_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMI9DAT_MainTable.Exists());

												
				CPCommon.CurrentComponent = "HPMI9DAT";
							CPCommon.WaitControlDisplayed(HPMI9DAT_MainForm);
formBttn = HPMI9DAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMI9DAT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMI9DAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("EMPLOYEE I-9 Data Form");


												
				CPCommon.CurrentComponent = "HPMI9DAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9DAT] Perfoming VerifyExist on EMPLOYEEI9DataTable...", Logger.MessageType.INF);
			Control HPMI9DAT_EMPLOYEEI9DataTable = new Control("EMPLOYEEI9DataTable", "xpath", "//div[starts-with(@id,'pr__HPMI9DAT_HI9DATA_I9DATA_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMI9DAT_EMPLOYEEI9DataTable.Exists());

												
				CPCommon.CurrentComponent = "HPMI9DAT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMI9DAT] Perfoming ClickButton on EMPLOYEEI9DataForm...", Logger.MessageType.INF);
			Control HPMI9DAT_EMPLOYEEI9DataForm = new Control("EMPLOYEEI9DataForm", "xpath", "//div[starts-with(@id,'pr__HPMI9DAT_HI9DATA_I9DATA_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HPMI9DAT_EMPLOYEEI9DataForm);
formBttn = HPMI9DAT_EMPLOYEEI9DataForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMI9DAT_EMPLOYEEI9DataForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMI9DAT_EMPLOYEEI9DataForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HPMI9DAT";
							CPCommon.AssertEqual(true,HPMI9DAT_EMPLOYEEI9DataForm.Exists());

													
				CPCommon.CurrentComponent = "HPMI9DAT";
							CPCommon.WaitControlDisplayed(HPMI9DAT_EMPLOYEEI9DataForm);
formBttn = HPMI9DAT_EMPLOYEEI9DataForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMI9DAT_EMPLOYEEI9DataForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMI9DAT_EMPLOYEEI9DataForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												Driver.SessionLogger.WriteLine("Close Main Form");


												
				CPCommon.CurrentComponent = "HPMI9DAT";
							CPCommon.WaitControlDisplayed(HPMI9DAT_MainForm);
formBttn = HPMI9DAT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

