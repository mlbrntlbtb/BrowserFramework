 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HARNHIRE_SMOKE : TestScript
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
new Control("Affirmative Action Reports", "xpath","//div[@class='navItem'][.='Affirmative Action Reports']").Click();
new Control("Print New Hire Analysis Report", "xpath","//div[@class='navItem'][.='Print New Hire Analysis Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HARNHIRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARNHIRE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HARNHIRE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HARNHIRE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HARNHIRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARNHIRE] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control HARNHIRE_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,HARNHIRE_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "HARNHIRE";
							CPCommon.WaitControlDisplayed(HARNHIRE_MainForm);
IWebElement formBttn = HARNHIRE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HARNHIRE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HARNHIRE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HARNHIRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HARNHIRE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HARNHIRE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HARNHIRE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HARNHIRE";
							CPCommon.WaitControlDisplayed(HARNHIRE_MainForm);
formBttn = HARNHIRE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

