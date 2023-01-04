 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCHORG_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Project Cost Processing", "xpath","//div[@class='navItem'][.='Project Cost Processing']").Click();
new Control("Process Organization Changes", "xpath","//div[@class='navItem'][.='Process Organization Changes']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJPCHORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCHORG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPCHORG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCHORG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCHORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCHORG] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJPCHORG_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJPCHORG_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJPCHORG";
							CPCommon.WaitControlDisplayed(PJPCHORG_MainForm);
IWebElement formBttn = PJPCHORG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJPCHORG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJPCHORG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJPCHORG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCHORG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJPCHORG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCHORG_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJPCHORG";
							CPCommon.WaitControlDisplayed(PJPCHORG_MainForm);
formBttn = PJPCHORG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

