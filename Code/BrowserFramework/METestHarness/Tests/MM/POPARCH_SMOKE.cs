 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POPARCH_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchasing Utilities", "xpath","//div[@class='navItem'][.='Purchasing Utilities']").Click();
new Control("Archive Purchase Orders", "xpath","//div[@class='navItem'][.='Archive Purchase Orders']").Click();


												
				CPCommon.CurrentComponent = "POPARCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPARCH] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POPARCH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POPARCH_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POPARCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPARCH] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control POPARCH_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,POPARCH_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "POPARCH";
							CPCommon.WaitControlDisplayed(POPARCH_MainForm);
IWebElement formBttn = POPARCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POPARCH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POPARCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POPARCH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPARCH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POPARCH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPARCH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POPARCH";
							CPCommon.WaitControlDisplayed(POPARCH_MainForm);
formBttn = POPARCH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

