 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MSPFPO_SMOKE : TestScript
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
new Control("Master Production Scheduling", "xpath","//div[@class='deptItem'][.='Master Production Scheduling']").Click();
new Control("Master Production Schedules", "xpath","//div[@class='navItem'][.='Master Production Schedules']").Click();
new Control("Firm Master Production Schedule Planned Orders", "xpath","//div[@class='navItem'][.='Firm Master Production Schedule Planned Orders']").Click();


												
				CPCommon.CurrentComponent = "MSPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPFPO] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MSPFPO_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MSPFPO_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MSPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPFPO] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control MSPFPO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(MSPFPO_MainForm);
IWebElement formBttn = MSPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MSPFPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MSPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "MSPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MSPFPO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MSPFPO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MSPFPO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MSPFPO";
							CPCommon.WaitControlDisplayed(MSPFPO_MainForm);
formBttn = MSPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MSPFPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MSPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MSPFPO";
							CPCommon.WaitControlDisplayed(MSPFPO_MainForm);
formBttn = MSPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

