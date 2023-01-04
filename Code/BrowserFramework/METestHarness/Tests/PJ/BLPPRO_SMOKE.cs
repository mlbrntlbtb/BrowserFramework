 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLPPRO_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Progress Payment Bills Processing", "xpath","//div[@class='navItem'][.='Progress Payment Bills Processing']").Click();
new Control("Post Progress Payment Bills", "xpath","//div[@class='navItem'][.='Post Progress Payment Bills']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLPPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPPRO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLPPRO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLPPRO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLPPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPPRO] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLPPRO_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLPPRO_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLPPRO";
							CPCommon.WaitControlDisplayed(BLPPRO_MainForm);
IWebElement formBttn = BLPPRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLPPRO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLPPRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLPPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPPRO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLPPRO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPPRO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPPRO";
							CPCommon.WaitControlDisplayed(BLPPRO_MainForm);
formBttn = BLPPRO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

