 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMLEDIT_SMOKE : TestScript
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
new Control("Leave", "xpath","//div[@class='deptItem'][.='Leave']").Click();
new Control("Leave Processing", "xpath","//div[@class='navItem'][.='Leave Processing']").Click();
new Control("Manage Leave Edit Table", "xpath","//div[@class='navItem'][.='Manage Leave Edit Table']").Click();


											Driver.SessionLogger.WriteLine("Query an existing record");


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEDIT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMLEDIT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMLEDIT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLEDIT";
							CPCommon.WaitControlDisplayed(LDMLEDIT_MainForm);
IWebElement formBttn = LDMLEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMLEDIT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMLEDIT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "LDMLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEDIT] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control LDMLEDIT_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLEDIT_MainTable.Exists());

												
				CPCommon.CurrentComponent = "LDMLEDIT";
							CPCommon.WaitControlDisplayed(LDMLEDIT_MainForm);
formBttn = LDMLEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMLEDIT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMLEDIT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "LDMLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEDIT] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control LDMLEDIT_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,LDMLEDIT_Employee.Exists());

											Driver.SessionLogger.WriteLine("Leave Details Form");


												
				CPCommon.CurrentComponent = "LDMLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEDIT] Perfoming VerifyExists on LeaveDetailsForm...", Logger.MessageType.INF);
			Control LDMLEDIT_LeaveDetailsForm = new Control("LeaveDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLEDIT_EMPLLVJNL_TBL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMLEDIT_LeaveDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLEDIT";
							CPCommon.AssertEqual(true,LDMLEDIT_Employee.Exists());

													
				CPCommon.CurrentComponent = "LDMLEDIT";
							CPCommon.WaitControlDisplayed(LDMLEDIT_LeaveDetailsForm);
formBttn = LDMLEDIT_LeaveDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMLEDIT_LeaveDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMLEDIT_LeaveDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "LDMLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLEDIT] Perfoming VerifyExist on LeaveDetailsTable...", Logger.MessageType.INF);
			Control LDMLEDIT_LeaveDetailsTable = new Control("LeaveDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLEDIT_EMPLLVJNL_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLEDIT_LeaveDetailsTable.Exists());

											Driver.SessionLogger.WriteLine("Exchange Rates Form");


											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMLEDIT";
							CPCommon.WaitControlDisplayed(LDMLEDIT_MainForm);
formBttn = LDMLEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

