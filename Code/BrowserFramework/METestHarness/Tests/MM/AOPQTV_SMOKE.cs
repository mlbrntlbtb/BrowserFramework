 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPQTV_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Procurement Planning Interfaces", "xpath","//div[@class='navItem'][.='Procurement Planning Interfaces']").Click();
new Control("Import Vendor Quotes", "xpath","//div[@class='navItem'][.='Import Vendor Quotes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "AOPQTV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPQTV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPQTV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPQTV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPQTV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPQTV] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPQTV_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPQTV_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPQTV";
							CPCommon.WaitControlDisplayed(AOPQTV_MainForm);
IWebElement formBttn = AOPQTV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPQTV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPQTV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPQTV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPQTV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPQTV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPQTV_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPQTV";
							CPCommon.WaitControlDisplayed(AOPQTV_MainForm);
formBttn = AOPQTV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

