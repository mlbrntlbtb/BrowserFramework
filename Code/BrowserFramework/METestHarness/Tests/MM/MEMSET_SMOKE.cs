 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEMSET_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Materials Estimating Controls", "xpath","//div[@class='navItem'][.='Materials Estimating Controls']").Click();
new Control("Configure Materials Estimating Settings", "xpath","//div[@class='navItem'][.='Configure Materials Estimating Settings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MEMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEMSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEMSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEMSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEMSET] Perfoming VerifyExists on MainForm_PBOMExplosions_IncludeToolingParts...", Logger.MessageType.INF);
			Control MEMSET_MainForm_PBOMExplosions_IncludeToolingParts = new Control("MainForm_PBOMExplosions_IncludeToolingParts", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INCL_TOOL_PART_FL']");
			CPCommon.AssertEqual(true,MEMSET_MainForm_PBOMExplosions_IncludeToolingParts.Exists());

												
				CPCommon.CurrentComponent = "MEMSET";
							CPCommon.WaitControlDisplayed(MEMSET_MainForm);
IWebElement formBttn = MEMSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

