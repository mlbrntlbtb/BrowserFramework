 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMFRG_02_SMOKE_POSTREQ : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Labor Controls", "xpath","//div[@class='navItem'][.='Labor Controls']").Click();
new Control("Configure Labor Settings", "xpath","//div[@class='navItem'][.='Configure Labor Settings']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMLABOR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMLABOR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control LDMLABOR_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,LDMLABOR_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "LDMLABOR";
							CPCommon.WaitControlDisplayed(LDMLABOR_MainFormTab);
IWebElement mTab = LDMLABOR_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Timesheet Options").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming VerifyExists on TimesheetOptions_EnableUnionFunctionality...", Logger.MessageType.INF);
			Control LDMLABOR_TimesheetOptions_EnableUnionFunctionality = new Control("TimesheetOptions_EnableUnionFunctionality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UNION_PR_FL']");
			CPCommon.AssertEqual(true,LDMLABOR_TimesheetOptions_EnableUnionFunctionality.Exists());

												
				CPCommon.CurrentComponent = "LDMLABOR";
							CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableUnionFunctionality);
LDMLABOR_TimesheetOptions_EnableUnionFunctionality.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableUnionFunctionality.GetAttributeValue("checked")) != true) LDMLABOR_TimesheetOptions_EnableUnionFunctionality.Click(4.3);


													
				CPCommon.CurrentComponent = "LDMLABOR";
							CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableUnionFunctionality);
LDMLABOR_TimesheetOptions_EnableUnionFunctionality.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableUnionFunctionality.GetAttributeValue("checked")) != false) LDMLABOR_TimesheetOptions_EnableUnionFunctionality.Click(4.3);


													
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming Set on TimesheetOptions_EnableMultiCurrencyFunctionality...", Logger.MessageType.INF);
			Control LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality = new Control("TimesheetOptions_EnableMultiCurrencyFunctionality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MULTICURR_FL']");
			CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality);
LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality.GetAttributeValue("checked")) != true) LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality.Click(4.3);


												
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming Set on TimesheetOptions_EnableWageDetermination...", Logger.MessageType.INF);
			Control LDMLABOR_TimesheetOptions_EnableWageDetermination = new Control("TimesheetOptions_EnableWageDetermination", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_WAGE_OVRIDE_CD']");
			CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableWageDetermination);
LDMLABOR_TimesheetOptions_EnableWageDetermination.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableWageDetermination.GetAttributeValue("checked")) != true) LDMLABOR_TimesheetOptions_EnableWageDetermination.Click(4.3);


											Driver.SessionLogger.WriteLine("SAVE AND CLOSE");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "LDMLABOR";
							CPCommon.WaitControlDisplayed(LDMLABOR_MainForm);
IWebElement formBttn = LDMLABOR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

