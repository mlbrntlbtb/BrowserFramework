 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDRESET_SMOKE : TestScript
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
new Control("Employee Reports", "xpath","//div[@class='navItem'][.='Employee Reports']").Click();
new Control("Print Employee Setup Report", "xpath","//div[@class='navItem'][.='Print Employee Setup Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "LDRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRESET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDRESET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDRESET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRESET] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDRESET_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDRESET_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDRESET";
							CPCommon.WaitControlDisplayed(LDRESET_MainForm);
IWebElement formBttn = LDRESET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDRESET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDRESET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDRESET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDRESET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDRESET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDRESET_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDRESET";
							CPCommon.WaitControlDisplayed(LDRESET_MainForm);
formBttn = LDRESET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

