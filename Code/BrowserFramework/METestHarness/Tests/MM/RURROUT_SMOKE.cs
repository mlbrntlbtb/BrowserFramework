 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RURROUT_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Routings Reports/Inquiries", "xpath","//div[@class='navItem'][.='Routings Reports/Inquiries']").Click();
new Control("Print Routings Report", "xpath","//div[@class='navItem'][.='Print Routings Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "RURROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RURROUT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RURROUT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RURROUT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RURROUT";
							CPCommon.WaitControlDisplayed(RURROUT_MainForm);
IWebElement formBttn = RURROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RURROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RURROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RURROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RURROUT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RURROUT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RURROUT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RURROUT";
							CPCommon.WaitControlDisplayed(RURROUT_MainForm);
formBttn = RURROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RURROUT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RURROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RURROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RURROUT] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control RURROUT_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,RURROUT_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "RURROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RURROUT] Perfoming VerifyExists on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control RURROUT_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_5561_RURROUT_PARAM");
			CPCommon.AssertEqual(true,RURROUT_SelectEndItemConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "RURROUT";
							CPCommon.WaitControlDisplayed(RURROUT_SelectEndItemConfigurationLink);
RURROUT_SelectEndItemConfigurationLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Select End Item Configuration");


												
				CPCommon.CurrentComponent = "RURROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RURROUT] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control RURROUT_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RURROUT_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "RURROUT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RURROUT] Perfoming VerifyExist on SelectEndItemConfigurationTable...", Logger.MessageType.INF);
			Control RURROUT_SelectEndItemConfigurationTable = new Control("SelectEndItemConfigurationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RURROUT_SelectEndItemConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "RURROUT";
							CPCommon.WaitControlDisplayed(RURROUT_SelectEndItemConfigurationForm);
formBttn = RURROUT_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "RURROUT";
							CPCommon.WaitControlDisplayed(RURROUT_MainForm);
formBttn = RURROUT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

