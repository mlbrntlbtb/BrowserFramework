 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECRIMPCT_SMOKE : TestScript
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
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("ECN Reports/Inquiries", "xpath","//div[@class='navItem'][.='ECN Reports/Inquiries']").Click();
new Control("Print Engineering Change Notice Impact Report", "xpath","//div[@class='navItem'][.='Print Engineering Change Notice Impact Report']").Click();


												
				CPCommon.CurrentComponent = "ECRIMPCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECRIMPCT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECRIMPCT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECRIMPCT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECRIMPCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECRIMPCT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control ECRIMPCT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,ECRIMPCT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "ECRIMPCT";
							CPCommon.WaitControlDisplayed(ECRIMPCT_MainForm);
IWebElement formBttn = ECRIMPCT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ECRIMPCT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ECRIMPCT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ECRIMPCT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECRIMPCT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECRIMPCT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECRIMPCT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECRIMPCT";
							CPCommon.WaitControlDisplayed(ECRIMPCT_MainForm);
formBttn = ECRIMPCT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

