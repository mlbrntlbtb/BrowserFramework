 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMPCOST_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='navItem'][.='Bills of Material']").Click();
new Control("Compute Costed Bills of Material", "xpath","//div[@class='navItem'][.='Compute Costed Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("MAINF FORM");


												
				CPCommon.CurrentComponent = "BMPCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPCOST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMPCOST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMPCOST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMPCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPCOST] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BMPCOST_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BMPCOST_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BMPCOST";
							CPCommon.WaitControlDisplayed(BMPCOST_MainForm);
IWebElement formBttn = BMPCOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMPCOST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMPCOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMPCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPCOST] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMPCOST_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMPCOST_MainTable.Exists());

											Driver.SessionLogger.WriteLine("SELECT END ITEM CONFIGURATION");


												
				CPCommon.CurrentComponent = "BMPCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPCOST] Perfoming VerifyExists on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control BMPCOST_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_4123_BMPCOST_PARAM");
			CPCommon.AssertEqual(true,BMPCOST_SelectEndItemConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "BMPCOST";
							CPCommon.WaitControlDisplayed(BMPCOST_SelectEndItemConfigurationLink);
BMPCOST_SelectEndItemConfigurationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMPCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPCOST] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control BMPCOST_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMPCOST_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "BMPCOST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMPCOST] Perfoming VerifyExist on SelectEndItemConfigurationTable...", Logger.MessageType.INF);
			Control BMPCOST_SelectEndItemConfigurationTable = new Control("SelectEndItemConfigurationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMPCOST_SelectEndItemConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "BMPCOST";
							CPCommon.WaitControlDisplayed(BMPCOST_SelectEndItemConfigurationForm);
formBttn = BMPCOST_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APPLICATION");


												
				CPCommon.CurrentComponent = "BMPCOST";
							CPCommon.WaitControlDisplayed(BMPCOST_MainForm);
formBttn = BMPCOST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

