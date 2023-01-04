 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPRPJSUB_SMOKE : TestScript
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
new Control("Advanced Project Budgeting", "xpath","//div[@class='deptItem'][.='Advanced Project Budgeting']").Click();
new Control("Project Budget Reports/Inquiries", "xpath","//div[@class='navItem'][.='Project Budget Reports/Inquiries']").Click();
new Control("Print Project Budget Detail Report", "xpath","//div[@class='navItem'][.='Print Project Budget Detail Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BPRPJSUB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJSUB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPRPJSUB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPRPJSUB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPRPJSUB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJSUB] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control BPRPJSUB_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BPRPJSUB_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BPRPJSUB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJSUB] Perfoming Select on MainForm_Tab...", Logger.MessageType.INF);
			Control BPRPJSUB_MainForm_Tab = new Control("MainForm_Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BPRPJSUB_MainForm_Tab);
IWebElement mTab = BPRPJSUB_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Previous").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BPRPJSUB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJSUB] Perfoming VerifyExists on Identification_Previous_SelectionRanges_ProjectsStart...", Logger.MessageType.INF);
			Control BPRPJSUB_Identification_Previous_SelectionRanges_ProjectsStart = new Control("Identification_Previous_SelectionRanges_ProjectsStart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID_FR']");
			CPCommon.AssertEqual(true,BPRPJSUB_Identification_Previous_SelectionRanges_ProjectsStart.Exists());

												
				CPCommon.CurrentComponent = "BPRPJSUB";
							CPCommon.WaitControlDisplayed(BPRPJSUB_MainForm_Tab);
mTab = BPRPJSUB_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Next").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BPRPJSUB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJSUB] Perfoming VerifyExists on Identification_Next_SelectionRanges_FiscalYearStart...", Logger.MessageType.INF);
			Control BPRPJSUB_Identification_Next_SelectionRanges_FiscalYearStart = new Control("Identification_Next_SelectionRanges_FiscalYearStart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FY_FROM']");
			CPCommon.AssertEqual(true,BPRPJSUB_Identification_Next_SelectionRanges_FiscalYearStart.Exists());

												
				CPCommon.CurrentComponent = "BPRPJSUB";
							CPCommon.WaitControlDisplayed(BPRPJSUB_MainForm);
IWebElement formBttn = BPRPJSUB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BPRPJSUB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BPRPJSUB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BPRPJSUB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJSUB] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BPRPJSUB_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPRPJSUB_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BPRPJSUB";
							CPCommon.WaitControlDisplayed(BPRPJSUB_MainForm);
formBttn = BPRPJSUB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

