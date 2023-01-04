 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJPCOPY_SMOKE : TestScript
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
new Control("Cost and Revenue Processing Utilities", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Utilities']").Click();
new Control("Copy Pools", "xpath","//div[@class='navItem'][.='Copy Pools']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJPCOPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOPY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJPCOPY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJPCOPY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJPCOPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOPY] Perfoming VerifyExists on Option_AllocationGroup...", Logger.MessageType.INF);
			Control PJPCOPY_Option_AllocationGroup = new Control("Option_AllocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ONE__ALL_GRP']");
			CPCommon.AssertEqual(true,PJPCOPY_Option_AllocationGroup.Exists());

												
				CPCommon.CurrentComponent = "PJPCOPY";
							CPCommon.WaitControlDisplayed(PJPCOPY_MainForm);
IWebElement formBttn = PJPCOPY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJPCOPY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJPCOPY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJPCOPY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJPCOPY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJPCOPY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJPCOPY_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJPCOPY";
							CPCommon.WaitControlDisplayed(PJPCOPY_MainForm);
formBttn = PJPCOPY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

