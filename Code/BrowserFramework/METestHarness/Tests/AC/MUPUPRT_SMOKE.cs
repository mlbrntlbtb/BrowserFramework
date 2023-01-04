 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MUPUPRT_SMOKE : TestScript
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
new Control("Exchange Rates", "xpath","//div[@class='navItem'][.='Exchange Rates']").Click();
new Control("Import Daily Exchange Rates", "xpath","//div[@class='navItem'][.='Import Daily Exchange Rates']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MUPUPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPUPRT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MUPUPRT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MUPUPRT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MUPUPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPUPRT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control MUPUPRT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MUPUPRT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MUPUPRT";
							CPCommon.WaitControlDisplayed(MUPUPRT_MainForm);
IWebElement formBttn = MUPUPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MUPUPRT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MUPUPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MUPUPRT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MUPUPRT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MUPUPRT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MUPUPRT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MUPUPRT";
							CPCommon.WaitControlDisplayed(MUPUPRT_MainForm);
formBttn = MUPUPRT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

