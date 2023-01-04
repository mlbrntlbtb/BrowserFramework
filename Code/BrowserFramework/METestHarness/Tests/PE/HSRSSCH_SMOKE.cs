 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSRSSCH_SMOKE : TestScript
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
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Performance Reviews", "xpath","//div[@class='navItem'][.='Performance Reviews']").Click();
new Control("Print/Send Performance Review Schedule by Manager", "xpath","//div[@class='navItem'][.='Print/Send Performance Review Schedule by Manager']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSRSSCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSRSSCH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSRSSCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSRSSCH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSRSSCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSRSSCH] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HSRSSCH_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HSRSSCH_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HSRSSCH";
							CPCommon.WaitControlDisplayed(HSRSSCH_MainForm);
IWebElement formBttn = HSRSSCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSRSSCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSRSSCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSRSSCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSRSSCH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSRSSCH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSRSSCH_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSRSSCH";
							CPCommon.WaitControlDisplayed(HSRSSCH_MainForm);
formBttn = HSRSSCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

