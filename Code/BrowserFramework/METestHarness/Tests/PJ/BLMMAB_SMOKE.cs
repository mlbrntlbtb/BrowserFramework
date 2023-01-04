 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMMAB_SMOKE : TestScript
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
new Control("Manage ACRN Bills", "xpath","//div[@class='navItem'][.='Manage ACRN Bills']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMMAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMMAB_MainForm);
IWebElement formBttn = BLMMAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? BLMMAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
BLMMAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BLMMAB_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMAB_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMMAB";
							CPCommon.WaitControlDisplayed(BLMMAB_MainForm);
formBttn = BLMMAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMMAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMMAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BLMMAB";
							CPCommon.AssertEqual(true,BLMMAB_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control BLMMAB_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMMAB_Identification_Project.Exists());

											Driver.SessionLogger.WriteLine("ACRN Details");


												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExist on ACRNDetails_Table...", Logger.MessageType.INF);
			Control BLMMAB_ACRNDetails_Table = new Control("ACRNDetails_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNDETL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMAB_ACRNDetails_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming ClickButton on ProjectACRNDetailsForm...", Logger.MessageType.INF);
			Control BLMMAB_ProjectACRNDetailsForm = new Control("ProjectACRNDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNDETL_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMMAB_ProjectACRNDetailsForm);
formBttn = BLMMAB_ProjectACRNDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMMAB_ProjectACRNDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMMAB_ProjectACRNDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMMAB";
							CPCommon.AssertEqual(true,BLMMAB_ProjectACRNDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExists on ProjectACRNDetails_SequenceNumber...", Logger.MessageType.INF);
			Control BLMMAB_ProjectACRNDetails_SequenceNumber = new Control("ProjectACRNDetails_SequenceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNDETL_CHLD_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,BLMMAB_ProjectACRNDetails_SequenceNumber.Exists());

											Driver.SessionLogger.WriteLine("Acconts");


												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExists on ProjectACRNDetails_AccountsLink...", Logger.MessageType.INF);
			Control BLMMAB_ProjectACRNDetails_AccountsLink = new Control("ProjectACRNDetails_AccountsLink", "ID", "lnk_16420_PBM_ACRNBILL_PROJACRNDETL_CHLD");
			CPCommon.AssertEqual(true,BLMMAB_ProjectACRNDetails_AccountsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMAB";
							CPCommon.WaitControlDisplayed(BLMMAB_ProjectACRNDetails_AccountsLink);
BLMMAB_ProjectACRNDetails_AccountsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExist on Accounts_Table...", Logger.MessageType.INF);
			Control BLMMAB_Accounts_Table = new Control("Accounts_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNACCT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMAB_Accounts_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming Close on AccountsForm...", Logger.MessageType.INF);
			Control BLMMAB_AccountsForm = new Control("AccountsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNACCT_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMMAB_AccountsForm);
formBttn = BLMMAB_AccountsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("PLC Mapping");


												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExists on ProjectACRNDetails_PLCMappingLink...", Logger.MessageType.INF);
			Control BLMMAB_ProjectACRNDetails_PLCMappingLink = new Control("ProjectACRNDetails_PLCMappingLink", "ID", "lnk_16421_PBM_ACRNBILL_PROJACRNDETL_CHLD");
			CPCommon.AssertEqual(true,BLMMAB_ProjectACRNDetails_PLCMappingLink.Exists());

												
				CPCommon.CurrentComponent = "BLMMAB";
							CPCommon.WaitControlDisplayed(BLMMAB_ProjectACRNDetails_PLCMappingLink);
BLMMAB_ProjectACRNDetails_PLCMappingLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming VerifyExist on PLCMapping_Table...", Logger.MessageType.INF);
			Control BLMMAB_PLCMapping_Table = new Control("PLCMapping_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNPLC_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMMAB_PLCMapping_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMMAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMMAB] Perfoming Close on PLCMappingForm...", Logger.MessageType.INF);
			Control BLMMAB_PLCMappingForm = new Control("PLCMappingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PBM_ACRNBILL_PROJACRNPLC_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMMAB_PLCMappingForm);
formBttn = BLMMAB_PLCMappingForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLMMAB";
							CPCommon.WaitControlDisplayed(BLMMAB_MainForm);
formBttn = BLMMAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

