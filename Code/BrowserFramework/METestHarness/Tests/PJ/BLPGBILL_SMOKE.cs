 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLPGBILL_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Standard Bills Processing", "xpath","//div[@class='navItem'][.='Standard Bills Processing']").Click();
new Control("Calculate Standard Bills", "xpath","//div[@class='navItem'][.='Calculate Standard Bills']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLPGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPGBILL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLPGBILL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLPGBILL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLPGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPGBILL] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLPGBILL_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLPGBILL_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLPGBILL";
							CPCommon.WaitControlDisplayed(BLPGBILL_MainForm);
IWebElement formBttn = BLPGBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLPGBILL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLPGBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLPGBILL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPGBILL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLPGBILL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPGBILL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLPGBILL";
							CPCommon.WaitControlDisplayed(BLPGBILL_MainForm);
formBttn = BLPGBILL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

