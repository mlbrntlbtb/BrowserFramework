 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRMIAPEG_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Manage Inventory Abbreviation Peggings", "xpath","//div[@class='navItem'][.='Manage Inventory Abbreviation Peggings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MRMIAPEG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRMIAPEG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control MRMIAPEG_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,MRMIAPEG_Part.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
							CPCommon.WaitControlDisplayed(MRMIAPEG_MainForm);
IWebElement formBttn = MRMIAPEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MRMIAPEG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MRMIAPEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MRMIAPEG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MRMIAPEG_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
							CPCommon.WaitControlDisplayed(MRMIAPEG_MainForm);
formBttn = MRMIAPEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MRMIAPEG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MRMIAPEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("SELECT END ITEM CONFIGURATION");


												
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExists on SelectEndItemConfigurationLink...", Logger.MessageType.INF);
			Control MRMIAPEG_SelectEndItemConfigurationLink = new Control("SelectEndItemConfigurationLink", "ID", "lnk_4411_MMMIAPEG_INVTABBRVPEGGING");
			CPCommon.AssertEqual(true,MRMIAPEG_SelectEndItemConfigurationLink.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
							CPCommon.WaitControlDisplayed(MRMIAPEG_SelectEndItemConfigurationLink);
MRMIAPEG_SelectEndItemConfigurationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExists on SelectEndItemConfigurationForm...", Logger.MessageType.INF);
			Control MRMIAPEG_SelectEndItemConfigurationForm = new Control("SelectEndItemConfigurationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,MRMIAPEG_SelectEndItemConfigurationForm.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExist on SelectEndItemConfigurationTable...", Logger.MessageType.INF);
			Control MRMIAPEG_SelectEndItemConfigurationTable = new Control("SelectEndItemConfigurationTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MM_ENDPARTCONFIG_LOAD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MRMIAPEG_SelectEndItemConfigurationTable.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
							CPCommon.WaitControlDisplayed(MRMIAPEG_SelectEndItemConfigurationForm);
formBttn = MRMIAPEG_SelectEndItemConfigurationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("COMPONENT LINES");


												
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExist on ComponentLinesFormTable...", Logger.MessageType.INF);
			Control MRMIAPEG_ComponentLinesFormTable = new Control("ComponentLinesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIAPEG_PEGGING_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MRMIAPEG_ComponentLinesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming ClickButton on ComponentLinesForm...", Logger.MessageType.INF);
			Control MRMIAPEG_ComponentLinesForm = new Control("ComponentLinesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIAPEG_PEGGING_DETAIL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MRMIAPEG_ComponentLinesForm);
formBttn = MRMIAPEG_ComponentLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MRMIAPEG_ComponentLinesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MRMIAPEG_ComponentLinesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "MRMIAPEG";
							CPCommon.AssertEqual(true,MRMIAPEG_ComponentLinesForm.Exists());

													
				CPCommon.CurrentComponent = "MRMIAPEG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRMIAPEG] Perfoming VerifyExists on ComponentLines_Part...", Logger.MessageType.INF);
			Control MRMIAPEG_ComponentLines_Part = new Control("ComponentLines_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIAPEG_PEGGING_DETAIL_']/ancestor::form[1]/descendant::*[@id='COMP_PART_ID']");
			CPCommon.AssertEqual(true,MRMIAPEG_ComponentLines_Part.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APPLICATION");


												
				CPCommon.CurrentComponent = "MRMIAPEG";
							CPCommon.WaitControlDisplayed(MRMIAPEG_MainForm);
formBttn = MRMIAPEG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

