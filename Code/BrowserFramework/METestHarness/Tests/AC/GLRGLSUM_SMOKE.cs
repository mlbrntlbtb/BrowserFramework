 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLRGLSUM_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Reports/Inquiries", "xpath","//div[@class='navItem'][.='General Ledger Reports/Inquiries']").Click();
new Control("Print General Ledger Posting Summary report", "xpath","//div[@class='navItem'][.='Print General Ledger Posting Summary report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLRGLSUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRGLSUM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLRGLSUM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLRGLSUM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLRGLSUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRGLSUM] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control GLRGLSUM_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,GLRGLSUM_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "GLRGLSUM";
							CPCommon.WaitControlDisplayed(GLRGLSUM_MainForm);
IWebElement formBttn = GLRGLSUM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLRGLSUM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLRGLSUM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLRGLSUM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLRGLSUM] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control GLRGLSUM_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLRGLSUM_MainTable.Exists());

												
				CPCommon.CurrentComponent = "GLRGLSUM";
							CPCommon.WaitControlDisplayed(GLRGLSUM_MainForm);
formBttn = GLRGLSUM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

