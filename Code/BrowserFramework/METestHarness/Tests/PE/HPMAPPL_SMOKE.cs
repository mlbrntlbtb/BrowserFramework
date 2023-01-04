 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMAPPL_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Affirmative Action", "xpath","//div[@class='deptItem'][.='Affirmative Action']").Click();
new Control("Affirmative Action Plan Information", "xpath","//div[@class='navItem'][.='Affirmative Action Plan Information']").Click();
new Control("Manage Applicant Data", "xpath","//div[@class='navItem'][.='Manage Applicant Data']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPMAPPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAPPL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMAPPL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMAPPL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMAPPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAPPL] Perfoming VerifyExists on ApplicantID...", Logger.MessageType.INF);
			Control HPMAPPL_ApplicantID = new Control("ApplicantID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='APPL_ID']");
			CPCommon.AssertEqual(true,HPMAPPL_ApplicantID.Exists());

												
				CPCommon.CurrentComponent = "HPMAPPL";
							CPCommon.WaitControlDisplayed(HPMAPPL_MainForm);
IWebElement formBttn = HPMAPPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMAPPL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMAPPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMAPPL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAPPL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMAPPL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAPPL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPMAPPL";
							CPCommon.WaitControlDisplayed(HPMAPPL_MainForm);
formBttn = HPMAPPL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

