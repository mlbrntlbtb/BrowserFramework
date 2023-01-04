 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMEMED_SMOKE : TestScript
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
new Control("Manage Employee Medical Surveillance Log", "xpath","//div[@class='navItem'][.='Manage Employee Medical Surveillance Log']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HBMEMED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEMED] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMEMED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMEMED_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEMED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEMED] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HBMEMED_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HBMEMED_Employee.Exists());

												
				CPCommon.CurrentComponent = "HBMEMED";
							CPCommon.WaitControlDisplayed(HBMEMED_MainForm);
IWebElement formBttn = HBMEMED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMEMED_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMEMED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMEMED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEMED] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMEMED_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEMED_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DETAILS");


												
				CPCommon.CurrentComponent = "HBMEMED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEMED] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control HBMEMED_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEMED_HBEMPLMEDTEST_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEMED_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMEMED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEMED] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control HBMEMED_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEMED_HBEMPLMEDTEST_DETAIL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HBMEMED_DetailsForm);
formBttn = HBMEMED_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMEMED_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMEMED_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HBMEMED";
							CPCommon.AssertEqual(true,HBMEMED_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "HBMEMED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEMED] Perfoming VerifyExists on Details_NextDueDate...", Logger.MessageType.INF);
			Control HBMEMED_Details_NextDueDate = new Control("Details_NextDueDate", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEMED_HBEMPLMEDTEST_DETAIL_']/ancestor::form[1]/descendant::*[@id='TEST_DUE_DT']");
			CPCommon.AssertEqual(true,HBMEMED_Details_NextDueDate.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HBMEMED";
							CPCommon.WaitControlDisplayed(HBMEMED_MainForm);
formBttn = HBMEMED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

