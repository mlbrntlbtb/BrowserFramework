 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMTRNPS_SMOKE : TestScript
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
new Control("Employee Controls", "xpath","//div[@class='navItem'][.='Employee Controls']").Click();
new Control("Manage Training Program Schedules", "xpath","//div[@class='navItem'][.='Manage Training Program Schedules']").Click();


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPMTRNPS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMTRNPS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMTRNPS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMTRNPS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMTRNPS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMTRNPS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMTRNPS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMTRNPS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMTRNPS";
							CPCommon.WaitControlDisplayed(HPMTRNPS_MainForm);
IWebElement formBttn = HPMTRNPS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMTRNPS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMTRNPS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMTRNPS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMTRNPS] Perfoming VerifyExists on Training...", Logger.MessageType.INF);
			Control HPMTRNPS_Training = new Control("Training", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TRAIN_ID']");
			CPCommon.AssertEqual(true,HPMTRNPS_Training.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPMTRNPS";
							CPCommon.WaitControlDisplayed(HPMTRNPS_MainForm);
formBttn = HPMTRNPS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

