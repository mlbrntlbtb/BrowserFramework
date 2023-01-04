 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLPAOBD_SMOKE : TestScript
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
new Control("Standard Bill Adjustments", "xpath","//div[@class='navItem'][.='Standard Bill Adjustments']").Click();
new Control("Adjust Open Billing Detail Records", "xpath","//div[@class='navItem'][.='Adjust Open Billing Detail Records']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming Set on Identification_Project...", Logger.MessageType.INF);
			Control BLPAOBD_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			BLPAOBD_Identification_Project.Click();
BLPAOBD_Identification_Project.SendKeys("1003", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
BLPAOBD_Identification_Project.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming Set on Identification_Identification_SelectSubperiodForAdjustment_FiscalYear...", Logger.MessageType.INF);
			Control BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_FiscalYear = new Control("Identification_Identification_SelectSubperiodForAdjustment_FiscalYear", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_CD']");
			BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_FiscalYear.Click();
BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_FiscalYear.SendKeys("2040", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_FiscalYear.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming Set on Identification_Identification_SelectSubperiodForAdjustment_Subperiod...", Logger.MessageType.INF);
			Control BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Subperiod = new Control("Identification_Identification_SelectSubperiodForAdjustment_Subperiod", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SUB_PD']");
			BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Subperiod.Click();
BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Subperiod.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Subperiod.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming Set on Identification_Identification_SelectSubperiodForAdjustment_Period...", Logger.MessageType.INF);
			Control BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Period = new Control("Identification_Identification_SelectSubperiodForAdjustment_Period", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PD_NO']");
			BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Period.Click();
BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Period.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
BLPAOBD_Identification_Identification_SelectSubperiodForAdjustment_Period.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLPAOBD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLPAOBD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLPAOBD";
							CPCommon.AssertEqual(true,BLPAOBD_Identification_Project.Exists());

												Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control BLPAOBD_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPAOBD_OPENBILLINGDETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPAOBD_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control BLPAOBD_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPAOBD_OPENBILLINGDETL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLPAOBD_ChildForm);
IWebElement formBttn = BLPAOBD_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLPAOBD_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLPAOBD_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLPAOBD";
							CPCommon.AssertEqual(true,BLPAOBD_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming VerifyExists on ChildForm_TransactionDetails_FiscalYear...", Logger.MessageType.INF);
			Control BLPAOBD_ChildForm_TransactionDetails_FiscalYear = new Control("ChildForm_TransactionDetails_FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPAOBD_OPENBILLINGDETL_']/ancestor::form[1]/descendant::*[@id='FY_CD']");
			CPCommon.AssertEqual(true,BLPAOBD_ChildForm_TransactionDetails_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming Select on ChildFormTab...", Logger.MessageType.INF);
			Control BLPAOBD_ChildFormTab = new Control("ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPAOBD_OPENBILLINGDETL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BLPAOBD_ChildFormTab);
IWebElement mTab = BLPAOBD_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Adjustment Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BLPAOBD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPAOBD] Perfoming VerifyExists on ChildForm_AdjustmentDetails_AdjustmentProject...", Logger.MessageType.INF);
			Control BLPAOBD_ChildForm_AdjustmentDetails_AdjustmentProject = new Control("ChildForm_AdjustmentDetails_AdjustmentProject", "xpath", "//div[translate(@id,'0123456789','')='pr__BLPAOBD_OPENBILLINGDETL_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLPAOBD_ChildForm_AdjustmentDetails_AdjustmentProject.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLPAOBD";
							CPCommon.WaitControlDisplayed(BLPAOBD_MainForm);
formBttn = BLPAOBD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

