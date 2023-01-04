 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPADPTS_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("ADP Interface", "xpath","//div[@class='navItem'][.='ADP Interface']").Click();
new Control("Export Timesheets To ADP", "xpath","//div[@class='navItem'][.='Export Timesheets To ADP']").Click();


											Driver.SessionLogger.WriteLine("Checking the App");


												
				CPCommon.CurrentComponent = "AOPADPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPADPTS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPADPTS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPADPTS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPADPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPADPTS] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPADPTS_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPADPTS_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPADPTS";
							CPCommon.WaitControlDisplayed(AOPADPTS_MainForm);
IWebElement formBttn = AOPADPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPADPTS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPADPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPADPTS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPADPTS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPADPTS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPADPTS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing the App");


												
				CPCommon.CurrentComponent = "AOPADPTS";
							CPCommon.WaitControlDisplayed(AOPADPTS_MainForm);
formBttn = AOPADPTS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

