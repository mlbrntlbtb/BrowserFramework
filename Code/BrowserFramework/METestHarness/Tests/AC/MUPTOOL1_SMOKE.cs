 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUPTOOL1_SMOKE : TestScript
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
new Control("Multicurrency", "xpath","//div[@class='deptItem'][.='Multicurrency']").Click();
new Control("Multicurrency Utilities", "xpath","//div[@class='navItem'][.='Multicurrency Utilities']").Click();
new Control("Update Currency Codes for Current Version", "xpath","//div[@class='navItem'][.='Update Currency Codes for Current Version']").Click();


												
				CPCommon.CurrentComponent = "MUPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPTOOL1] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUPTOOL1_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUPTOOL1_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPTOOL1] Perfoming VerifyExists on TextArea...", Logger.MessageType.INF);
			Control MUPTOOL1_TextArea = new Control("TextArea", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MT1']");
			CPCommon.AssertEqual(true,MUPTOOL1_TextArea.Exists());

												
				CPCommon.CurrentComponent = "MUPTOOL1";
							CPCommon.WaitControlDisplayed(MUPTOOL1_MainForm);
IWebElement formBttn = MUPTOOL1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

