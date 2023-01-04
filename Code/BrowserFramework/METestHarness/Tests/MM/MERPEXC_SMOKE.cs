 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MERPEXC_SMOKE : TestScript
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
new Control("Materials Estimating Reports/Inquiries", "xpath","//div[@class='navItem'][.='Materials Estimating Reports/Inquiries']").Click();
new Control("Print Proposal Bills of Material Exception Report", "xpath","//div[@class='navItem'][.='Print Proposal Bills of Material Exception Report']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MERPEXC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MERPEXC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MERPEXC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MERPEXC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MERPEXC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MERPEXC] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MERPEXC_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MERPEXC_MainForm_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "MERPEXC";
							CPCommon.WaitControlDisplayed(MERPEXC_MainForm);
IWebElement formBttn = MERPEXC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MERPEXC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MERPEXC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MERPEXC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MERPEXC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MERPEXC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MERPEXC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MERPEXC";
							CPCommon.WaitControlDisplayed(MERPEXC_MainForm);
formBttn = MERPEXC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

