 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMEICN_SMOKE : TestScript
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
new Control("Manage End Item Configurations", "xpath","//div[@class='navItem'][.='Manage End Item Configurations']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMMEICN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMMEICN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control BMMEICN_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFSPARTID']");
			CPCommon.AssertEqual(true,BMMEICN_Part.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
							CPCommon.WaitControlDisplayed(BMMEICN_MainForm);
IWebElement formBttn = BMMEICN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMEICN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMEICN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMMEICN_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEICN_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Configuration");


												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExists on ConfigurationForm...", Logger.MessageType.INF);
			Control BMMEICN_ConfigurationForm = new Control("ConfigurationForm", "xpath", "//div[starts-with(@id,'pr__BMMEICN_ENDPARTCONFIG_SIB1_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMEICN_ConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExist on ConfigurationTable...", Logger.MessageType.INF);
			Control BMMEICN_ConfigurationTable = new Control("ConfigurationTable", "xpath", "//div[starts-with(@id,'pr__BMMEICN_ENDPARTCONFIG_SIB1_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEICN_ConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
							CPCommon.WaitControlDisplayed(BMMEICN_ConfigurationForm);
formBttn = BMMEICN_ConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEICN_ConfigurationForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEICN_ConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExists on Configuration_BOMConfiguration...", Logger.MessageType.INF);
			Control BMMEICN_Configuration_BOMConfiguration = new Control("Configuration_BOMConfiguration", "xpath", "//div[starts-with(@id,'pr__BMMEICN_ENDPARTCONFIG_SIB1_')]/ancestor::form[1]/descendant::*[@id='BOM_CONFIG_ID']");
			CPCommon.AssertEqual(true,BMMEICN_Configuration_BOMConfiguration.Exists());

											Driver.SessionLogger.WriteLine("Project");


												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming Click on Configuration_ProjectLink...", Logger.MessageType.INF);
			Control BMMEICN_Configuration_ProjectLink = new Control("Configuration_ProjectLink", "ID", "lnk_1001623_BMMEICN_ENDPARTCONFIG_SIB1");
			CPCommon.WaitControlDisplayed(BMMEICN_Configuration_ProjectLink);
BMMEICN_Configuration_ProjectLink.Click(1.5);


												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExists on Configuration_ProjectForm...", Logger.MessageType.INF);
			Control BMMEICN_Configuration_ProjectForm = new Control("Configuration_ProjectForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEICN_PROJ_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMEICN_Configuration_ProjectForm.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExist on Configuration_ProjectTable...", Logger.MessageType.INF);
			Control BMMEICN_Configuration_ProjectTable = new Control("Configuration_ProjectTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEICN_PROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMEICN_Configuration_ProjectTable.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
							CPCommon.WaitControlDisplayed(BMMEICN_Configuration_ProjectForm);
formBttn = BMMEICN_Configuration_ProjectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMEICN_Configuration_ProjectForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMEICN_Configuration_ProjectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMEICN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMEICN] Perfoming VerifyExists on Configuration_Project_Project...", Logger.MessageType.INF);
			Control BMMEICN_Configuration_Project_Project = new Control("Configuration_Project_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMEICN_PROJ_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BMMEICN_Configuration_Project_Project.Exists());

												
				CPCommon.CurrentComponent = "BMMEICN";
							CPCommon.WaitControlDisplayed(BMMEICN_Configuration_ProjectForm);
formBttn = BMMEICN_Configuration_ProjectForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "BMMEICN";
							CPCommon.WaitControlDisplayed(BMMEICN_MainForm);
formBttn = BMMEICN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

