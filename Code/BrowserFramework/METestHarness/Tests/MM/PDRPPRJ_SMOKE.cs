 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDRPPRJ_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Product Definition Reports/Inquiries", "xpath","//div[@class='navItem'][.='Product Definition Reports/Inquiries']").Click();
new Control("Print Part Project Report", "xpath","//div[@class='navItem'][.='Print Part Project Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PDRPPRJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDRPPRJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDRPPRJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDRPPRJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDRPPRJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDRPPRJ] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PDRPPRJ_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PDRPPRJ_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PDRPPRJ";
							CPCommon.WaitControlDisplayed(PDRPPRJ_MainForm);
IWebElement formBttn = PDRPPRJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDRPPRJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDRPPRJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDRPPRJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDRPPRJ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDRPPRJ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDRPPRJ_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDRPPRJ";
							CPCommon.WaitControlDisplayed(PDRPPRJ_MainForm);
formBttn = PDRPPRJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

