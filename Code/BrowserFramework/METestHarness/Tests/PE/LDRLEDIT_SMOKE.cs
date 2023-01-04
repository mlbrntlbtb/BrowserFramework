 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDRLEDIT_SMOKE : TestScript
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
new Control("Print Leave Edit Report", "xpath","//div[@class='navItem'][.='Print Leave Edit Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDRLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRLEDIT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDRLEDIT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDRLEDIT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDRLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRLEDIT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDRLEDIT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDRLEDIT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDRLEDIT";
							CPCommon.WaitControlDisplayed(LDRLEDIT_MainForm);
IWebElement formBttn = LDRLEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDRLEDIT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDRLEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDRLEDIT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRLEDIT] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control LDRLEDIT_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDRLEDIT_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDRLEDIT";
							CPCommon.WaitControlDisplayed(LDRLEDIT_MainForm);
formBttn = LDRLEDIT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

