 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMLAD_00_SMOKE_PREREQ : TestScript
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

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control LDMLABOR_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(LDMLABOR_MainFormTab);
IWebElement mTab = LDMLABOR_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Timesheet Options").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming Set on TimesheetOptions_EnableUnionFunctionality...", Logger.MessageType.INF);
			Control LDMLABOR_TimesheetOptions_EnableUnionFunctionality = new Control("TimesheetOptions_EnableUnionFunctionality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UNION_PR_FL']");
			CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableUnionFunctionality);
LDMLABOR_TimesheetOptions_EnableUnionFunctionality.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableUnionFunctionality.GetAttributeValue("checked")) != true) LDMLABOR_TimesheetOptions_EnableUnionFunctionality.Click(4.3);


												
				CPCommon.CurrentComponent = "LDMLABOR";
							CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableUnionFunctionality);
LDMLABOR_TimesheetOptions_EnableUnionFunctionality.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableUnionFunctionality.GetAttributeValue("checked")) != false) LDMLABOR_TimesheetOptions_EnableUnionFunctionality.Click(4.3);


													
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming Set on TimesheetOptions_EnableBatch...", Logger.MessageType.INF);
			Control LDMLABOR_TimesheetOptions_EnableBatch = new Control("TimesheetOptions_EnableBatch", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BATCH_CNTL_FL']");
			CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableBatch);
LDMLABOR_TimesheetOptions_EnableBatch.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableBatch.GetAttributeValue("checked")) != false) LDMLABOR_TimesheetOptions_EnableBatch.Click(4.3);


												
				CPCommon.CurrentComponent = "LDMLABOR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLABOR] Perfoming Set on TimesheetOptions_EnableMultiCurrencyFunctionality...", Logger.MessageType.INF);
			Control LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality = new Control("TimesheetOptions_EnableMultiCurrencyFunctionality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MULTICURR_FL']");
			CPCommon.WaitControlDisplayed(LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality);
LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality.ScrollIntoViewUsingJavaScript();
if (Convert.ToBoolean(LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality.GetAttributeValue("checked")) != true) LDMLABOR_TimesheetOptions_EnableMultiCurrencyFunctionality.Click(4.3);


											Driver.SessionLogger.WriteLine("SAVE AND CLOSE");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("SYPSTNG");


												
				CPCommon.CurrentComponent = "CP7Main";
							if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Admin", "xpath","//div[@class='busItem'][.='Admin']").Click();
new Control("System Administration", "xpath","//div[@class='deptItem'][.='System Administration']").Click();
new Control("System Administration Utilities", "xpath","//div[@class='navItem'][.='System Administration Utilities']").Click();
new Control("Rebuild Global Settings", "xpath","//div[@class='navItem'][.='Rebuild Global Settings']").Click();


													
				CPCommon.CurrentComponent = "CP7Main";
							CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Action Menu')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Action Menu~Reload Settings.");
tlbrBtn.Click();
CP7Main_MainToolBar.mElement.FindElements(By.XPath("//*[@class = 'tlbrDDItem' and contains(text(),'Reload Settings')]")).FirstOrDefault().Click();


													
				CPCommon.CurrentComponent = "ProcessProgress";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ProcessProgress] Perfoming Click on OK...", Logger.MessageType.INF);
			Control ProcessProgress_OK = new Control("OK", "ID", "progMtrBtn");
			CPCommon.WaitControlDisplayed(ProcessProgress_OK);
if (ProcessProgress_OK.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
ProcessProgress_OK.Click(5,5);
else ProcessProgress_OK.Click(4.5);


												
				CPCommon.CurrentComponent = "SYPSTNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[SYPSTNG] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control SYPSTNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(SYPSTNG_MainForm);
IWebElement formBttn = SYPSTNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

