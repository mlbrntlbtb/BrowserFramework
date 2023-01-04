 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CMPCCF_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


											Driver.SessionLogger.WriteLine("Main form");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Cash Management", "xpath","//div[@class='deptItem'][.='Cash Management']").Click();
new Control("Cash Forecasting", "xpath","//div[@class='navItem'][.='Cash Forecasting']").Click();
new Control("Create Preliminary Cash Forecasts", "xpath","//div[@class='navItem'][.='Create Preliminary Cash Forecasts']").Click();


												
				CPCommon.CurrentComponent = "CMPCCF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMPCCF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control CMPCCF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,CMPCCF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "CMPCCF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMPCCF] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control CMPCCF_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,CMPCCF_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "CMPCCF";
							CPCommon.WaitControlDisplayed(CMPCCF_MainForm);
IWebElement formBttn = CMPCCF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? CMPCCF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
CMPCCF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "CMPCCF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CMPCCF] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control CMPCCF_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,CMPCCF_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "CMPCCF";
							CPCommon.WaitControlDisplayed(CMPCCF_MainForm);
formBttn = CMPCCF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

