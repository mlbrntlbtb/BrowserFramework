 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMPDSC_SMOKE : TestScript
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
new Control("Billing Master", "xpath","//div[@class='navItem'][.='Billing Master']").Click();
new Control("Manage Project Volume Discounts", "xpath","//div[@class='navItem'][.='Manage Project Volume Discounts']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMPDSC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPDSC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMPDSC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMPDSC_MainForm);
IWebElement formBttn = BLMPDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMPDSC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMPDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMPDSC";
							CPCommon.AssertEqual(true,BLMPDSC_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMPDSC_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMPDSC_Project.Exists());

											Driver.SessionLogger.WriteLine("Accounts");


												
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming VerifyExists on AccountsLink...", Logger.MessageType.INF);
			Control BLMPDSC_AccountsLink = new Control("AccountsLink", "ID", "lnk_1007591_BLMPDSC_PROJVOLUME_HDR");
			CPCommon.AssertEqual(true,BLMPDSC_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMPDSC";
							CPCommon.WaitControlDisplayed(BLMPDSC_AccountsLink);
BLMPDSC_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming VerifyExist on AccountsFormTable...", Logger.MessageType.INF);
			Control BLMPDSC_AccountsFormTable = new Control("AccountsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPDSC_PROJACCT_CHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPDSC_AccountsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming Close on AccountsForm...", Logger.MessageType.INF);
			Control BLMPDSC_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPDSC_PROJACCT_CHD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMPDSC_AccountsForm);
formBttn = BLMPDSC_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Project Volume Details");


												
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming VerifyExist on ProjectVolumeDetailsFormTable...", Logger.MessageType.INF);
			Control BLMPDSC_ProjectVolumeDetailsFormTable = new Control("ProjectVolumeDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPDSC_PROJVOLUME_CHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMPDSC_ProjectVolumeDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming ClickButton on ProjectVolumeDetailsForm...", Logger.MessageType.INF);
			Control BLMPDSC_ProjectVolumeDetailsForm = new Control("ProjectVolumeDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPDSC_PROJVOLUME_CHD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMPDSC_ProjectVolumeDetailsForm);
formBttn = BLMPDSC_ProjectVolumeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMPDSC_ProjectVolumeDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMPDSC_ProjectVolumeDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMPDSC";
							CPCommon.AssertEqual(true,BLMPDSC_ProjectVolumeDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMPDSC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMPDSC] Perfoming VerifyExists on ProjectVolumeDetails_StartingSalesVolume...", Logger.MessageType.INF);
			Control BLMPDSC_ProjectVolumeDetails_StartingSalesVolume = new Control("ProjectVolumeDetails_StartingSalesVolume", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMPDSC_PROJVOLUME_CHD_']/ancestor::form[1]/descendant::*[@id='FROM_QTY']");
			CPCommon.AssertEqual(true,BLMPDSC_ProjectVolumeDetails_StartingSalesVolume.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLMPDSC";
							CPCommon.WaitControlDisplayed(BLMPDSC_MainForm);
formBttn = BLMPDSC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

