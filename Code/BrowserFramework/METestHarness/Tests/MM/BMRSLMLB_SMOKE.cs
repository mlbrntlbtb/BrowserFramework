 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMRSLMLB_SMOKE : TestScript
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
new Control("Print Indented Bills of Material Report", "xpath","//div[@class='navItem'][.='Print Indented Bills of Material Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "BMRSLMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSLMLB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMRSLMLB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMRSLMLB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMRSLMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSLMLB] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BMRSLMLB_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BMRSLMLB_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BMRSLMLB";
							CPCommon.WaitControlDisplayed(BMRSLMLB_MainForm);
IWebElement formBttn = BMRSLMLB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMRSLMLB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMRSLMLB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMRSLMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSLMLB] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMRSLMLB_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMRSLMLB_MainTable.Exists());

											Driver.SessionLogger.WriteLine("SelectEndItemConfigurationLink");


												
				CPCommon.CurrentComponent = "BMRSLMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSLMLB] Perfoming VerifyExists on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control BMRSLMLB_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_4156_BMRSLMLB_PARAM");
			CPCommon.AssertEqual(true,BMRSLMLB_SelectEndItemConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "BMRSLMLB";
							CPCommon.WaitControlDisplayed(BMRSLMLB_SelectEndItemConfigurationLink);
BMRSLMLB_SelectEndItemConfigurationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMRSLMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSLMLB] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control BMRSLMLB_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMRSLMLB_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "BMRSLMLB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRSLMLB] Perfoming VerifyExist on SelectEndItemConfigurationTable...", Logger.MessageType.INF);
			Control BMRSLMLB_SelectEndItemConfigurationTable = new Control("SelectEndItemConfigurationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMRSLMLB_SelectEndItemConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "BMRSLMLB";
							CPCommon.WaitControlDisplayed(BMRSLMLB_SelectEndItemConfigurationForm);
formBttn = BMRSLMLB_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "BMRSLMLB";
							CPCommon.WaitControlDisplayed(BMRSLMLB_MainForm);
formBttn = BMRSLMLB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

