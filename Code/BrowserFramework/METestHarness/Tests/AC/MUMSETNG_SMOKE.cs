 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUMSETNG_SMOKE : TestScript
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
new Control("Multicurrency", "xpath","//div[@class='deptItem'][.='Multicurrency']").Click();
new Control("Multicurrency Controls", "xpath","//div[@class='navItem'][.='Multicurrency Controls']").Click();
new Control("Configure Multicurrency Settings", "xpath","//div[@class='navItem'][.='Configure Multicurrency Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMSETNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUMSETNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUMSETNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUMSETNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUMSETNG] Perfoming VerifyExists on DefaultTransaction...", Logger.MessageType.INF);
			Control MUMSETNG_DefaultTransaction = new Control("DefaultTransaction", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFLT_TR_S_CRNCY_CD']");
			CPCommon.AssertEqual(true,MUMSETNG_DefaultTransaction.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUMSETNG";
							CPCommon.WaitControlDisplayed(MUMSETNG_MainForm);
IWebElement formBttn = MUMSETNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

