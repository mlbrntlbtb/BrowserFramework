 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMATCS_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("Configure Asset/Template Change Settings", "xpath","//div[@class='navItem'][.='Configure Asset/Template Change Settings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMATCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMATCS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMATCS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMATCS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMATCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMATCS] Perfoming VerifyExists on WriteChangeDetailsToAuditLogFor_AssetMasterRecords...", Logger.MessageType.INF);
			Control FAMATCS_WriteChangeDetailsToAuditLogFor_AssetMasterRecords = new Control("WriteChangeDetailsToAuditLogFor_AssetMasterRecords", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_AUDIT_LOG_FL']");
			CPCommon.AssertEqual(true,FAMATCS_WriteChangeDetailsToAuditLogFor_AssetMasterRecords.Exists());

											Driver.SessionLogger.WriteLine("Asset/Templaye Field/Column Names");


												
				CPCommon.CurrentComponent = "FAMATCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMATCS] Perfoming Click on AssetTemplateFieldColumnNamesLink...", Logger.MessageType.INF);
			Control FAMATCS_AssetTemplateFieldColumnNamesLink = new Control("AssetTemplateFieldColumnNamesLink", "ID", "lnk_1002100_FAMATCS_SFAAUDITSETTING");
			CPCommon.WaitControlDisplayed(FAMATCS_AssetTemplateFieldColumnNamesLink);
FAMATCS_AssetTemplateFieldColumnNamesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "FAMATCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMATCS] Perfoming VerifyExist on AssetTemplateFieldColumnNamesFormTable...", Logger.MessageType.INF);
			Control FAMATCS_AssetTemplateFieldColumnNamesFormTable = new Control("AssetTemplateFieldColumnNamesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMATCS_SFADBCOLDEF_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMATCS_AssetTemplateFieldColumnNamesFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMATCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMATCS] Perfoming ClickButton on AssetTemplateFieldColumnNamesForm...", Logger.MessageType.INF);
			Control FAMATCS_AssetTemplateFieldColumnNamesForm = new Control("AssetTemplateFieldColumnNamesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMATCS_SFADBCOLDEF_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMATCS_AssetTemplateFieldColumnNamesForm);
IWebElement formBttn = FAMATCS_AssetTemplateFieldColumnNamesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMATCS_AssetTemplateFieldColumnNamesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMATCS_AssetTemplateFieldColumnNamesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMATCS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMATCS] Perfoming VerifyExists on AssetTemplateFieldColumnNames_EnglishFieldColumnName...", Logger.MessageType.INF);
			Control FAMATCS_AssetTemplateFieldColumnNames_EnglishFieldColumnName = new Control("AssetTemplateFieldColumnNames_EnglishFieldColumnName", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMATCS_SFADBCOLDEF_']/ancestor::form[1]/descendant::*[@id='ENGL_COL_NAME']");
			CPCommon.AssertEqual(true,FAMATCS_AssetTemplateFieldColumnNames_EnglishFieldColumnName.Exists());

												
				CPCommon.CurrentComponent = "FAMATCS";
							CPCommon.WaitControlDisplayed(FAMATCS_AssetTemplateFieldColumnNamesForm);
formBttn = FAMATCS_AssetTemplateFieldColumnNamesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMATCS";
							CPCommon.WaitControlDisplayed(FAMATCS_MainForm);
formBttn = FAMATCS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

