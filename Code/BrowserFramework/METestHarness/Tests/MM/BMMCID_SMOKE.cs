 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMCID_SMOKE : TestScript
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
new Control("Bills of Material Controls", "xpath","//div[@class='navItem'][.='Bills of Material Controls']").Click();
new Control("Manage Configuration Identifiers", "xpath","//div[@class='navItem'][.='Manage Configuration Identifiers']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BMMCID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMCID] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BMMCID_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMCID_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMCID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMCID] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BMMCID_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BMMCID_MainForm);
IWebElement formBttn = BMMCID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMCID_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMCID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMCID";
							CPCommon.AssertEqual(true,BMMCID_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BMMCID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMCID] Perfoming VerifyExists on ConfigurationID...", Logger.MessageType.INF);
			Control BMMCID_ConfigurationID = new Control("ConfigurationID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BOM_CONFIG_ID']");
			CPCommon.AssertEqual(true,BMMCID_ConfigurationID.Exists());

											Driver.SessionLogger.WriteLine("Link Projects");


												
				CPCommon.CurrentComponent = "BMMCID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMCID] Perfoming VerifyExist on LinkProjectsFormTable...", Logger.MessageType.INF);
			Control BMMCID_LinkProjectsFormTable = new Control("LinkProjectsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMCID_BOMCONFIGPROJ_LINKPROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMCID_LinkProjectsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMCID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMCID] Perfoming ClickButton on LinkProjectsForm...", Logger.MessageType.INF);
			Control BMMCID_LinkProjectsForm = new Control("LinkProjectsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMCID_BOMCONFIGPROJ_LINKPROJ_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMCID_LinkProjectsForm);
formBttn = BMMCID_LinkProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMCID_LinkProjectsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMCID_LinkProjectsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMCID";
							CPCommon.AssertEqual(true,BMMCID_LinkProjectsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMCID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMCID] Perfoming VerifyExists on LinkProjects_Project...", Logger.MessageType.INF);
			Control BMMCID_LinkProjects_Project = new Control("LinkProjects_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMCID_BOMCONFIGPROJ_LINKPROJ_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BMMCID_LinkProjects_Project.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BMMCID";
							CPCommon.WaitControlDisplayed(BMMCID_MainForm);
formBttn = BMMCID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

