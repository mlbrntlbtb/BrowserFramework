 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMRSUMBM_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='deptItem'][.='Bills of Material']").Click();
new Control("Bills of Material Reports/Inquiries", "xpath","//div[@class='navItem'][.='Bills of Material Reports/Inquiries']").Click();
new Control("Print Summarized Bills of Material Report", "xpath","//div[@class='navItem'][.='Print Summarized Bills of Material Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "BMRSUMBM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSUMBM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMRSUMBM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMRSUMBM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMRSUMBM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSUMBM] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BMRSUMBM_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BMRSUMBM_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BMRSUMBM";
							CPCommon.WaitControlDisplayed(BMRSUMBM_MainForm);
IWebElement formBttn = BMRSUMBM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMRSUMBM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMRSUMBM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMRSUMBM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSUMBM] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMRSUMBM_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMRSUMBM_MainTable.Exists());

											Driver.SessionLogger.WriteLine("SelectEndItemConfigurationLink");


												
				CPCommon.CurrentComponent = "BMRSUMBM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSUMBM] Perfoming VerifyExists on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control BMRSUMBM_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_4132_BMRSUMBM_PARAM");
			CPCommon.AssertEqual(true,BMRSUMBM_SelectEndItemConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "BMRSUMBM";
							CPCommon.WaitControlDisplayed(BMRSUMBM_SelectEndItemConfigurationLink);
BMRSUMBM_SelectEndItemConfigurationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMRSUMBM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSUMBM] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control BMRSUMBM_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMRSUMBM_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "BMRSUMBM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSUMBM] Perfoming VerifyExist on SelectEndItemConfigurationTable...", Logger.MessageType.INF);
			Control BMRSUMBM_SelectEndItemConfigurationTable = new Control("SelectEndItemConfigurationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMRSUMBM_SelectEndItemConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "BMRSUMBM";
							CPCommon.WaitControlDisplayed(BMRSUMBM_SelectEndItemConfigurationForm);
formBttn = BMRSUMBM_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "BMRSUMBM";
							CPCommon.WaitControlDisplayed(BMRSUMBM_MainForm);
formBttn = BMRSUMBM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

