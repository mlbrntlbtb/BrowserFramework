 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPMICDN_SMOKE : TestScript
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
new Control("Cost and Revenue Processing Interfaces", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Interfaces']").Click();
new Control("Create Microframe Transfer Files", "xpath","//div[@class='navItem'][.='Create Microframe Transfer Files']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "AOPMICDN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPMICDN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPMICDN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPMICDN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPMICDN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPMICDN] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPMICDN_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPMICDN_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPMICDN";
							CPCommon.WaitControlDisplayed(AOPMICDN_MainForm);
IWebElement formBttn = AOPMICDN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPMICDN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPMICDN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPMICDN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPMICDN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPMICDN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPMICDN_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "AOPMICDN";
							CPCommon.WaitControlDisplayed(AOPMICDN_MainForm);
formBttn = AOPMICDN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

