 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARRHIST_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Accounts Receivable Reports/Inquiries", "xpath","//div[@class='navItem'][.='Accounts Receivable Reports/Inquiries']").Click();
new Control("Print Accounts Receivable History Report", "xpath","//div[@class='navItem'][.='Print Accounts Receivable History Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARRHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRHIST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARRHIST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARRHIST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARRHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRHIST] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control ARRHIST_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,ARRHIST_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "ARRHIST";
							CPCommon.WaitControlDisplayed(ARRHIST_MainForm);
IWebElement formBttn = ARRHIST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ARRHIST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ARRHIST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ARRHIST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRHIST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ARRHIST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRHIST_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARRHIST";
							CPCommon.WaitControlDisplayed(ARRHIST_MainForm);
formBttn = ARRHIST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

