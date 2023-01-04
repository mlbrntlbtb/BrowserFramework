 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POPCOMM_SMOKE : TestScript
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
new Control("Project Inquiry and Reporting", "xpath","//div[@class='deptItem'][.='Project Inquiry and Reporting']").Click();
new Control("Report Processing", "xpath","//div[@class='navItem'][.='Report Processing']").Click();
new Control("Compute/Print Purchasing Commitments", "xpath","//div[@class='navItem'][.='Compute/Print Purchasing Commitments']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "POPCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPCOMM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POPCOMM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POPCOMM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POPCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPCOMM] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control POPCOMM_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,POPCOMM_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "POPCOMM";
							CPCommon.WaitControlDisplayed(POPCOMM_MainForm);
IWebElement formBttn = POPCOMM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POPCOMM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POPCOMM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POPCOMM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POPCOMM] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control POPCOMM_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POPCOMM_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "POPCOMM";
							CPCommon.WaitControlDisplayed(POPCOMM_MainForm);
formBttn = POPCOMM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

