 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMMOLAB_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Utilities", "xpath","//div[@class='navItem'][.='Production Control Utilities']").Click();
new Control("Update Manufacturing Order Labor Costs", "xpath","//div[@class='navItem'][.='Update Manufacturing Order Labor Costs']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PCMMOLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMOLAB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMMOLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMMOLAB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMMOLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMOLAB] Perfoming VerifyExists on ManufacturingOrder...", Logger.MessageType.INF);
			Control PCMMOLAB_ManufacturingOrder = new Control("ManufacturingOrder", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MO_ID']");
			CPCommon.AssertEqual(true,PCMMOLAB_ManufacturingOrder.Exists());

												
				CPCommon.CurrentComponent = "PCMMOLAB";
							CPCommon.WaitControlDisplayed(PCMMOLAB_MainForm);
IWebElement formBttn = PCMMOLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCMMOLAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCMMOLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCMMOLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMMOLAB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCMMOLAB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMMOLAB_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PCMMOLAB";
							CPCommon.WaitControlDisplayed(PCMMOLAB_MainForm);
formBttn = PCMMOLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

