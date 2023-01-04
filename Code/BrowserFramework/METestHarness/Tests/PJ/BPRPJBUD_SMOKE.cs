 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPRPJBUD_SMOKE : TestScript
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
new Control("Print Project Budget Summary Report", "xpath","//div[@class='navItem'][.='Print Project Budget Summary Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BPRPJBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJBUD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPRPJBUD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPRPJBUD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPRPJBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJBUD] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control BPRPJBUD_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BPRPJBUD_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BPRPJBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJBUD] Perfoming Select on MainForm_Tab...", Logger.MessageType.INF);
			Control BPRPJBUD_MainForm_Tab = new Control("MainForm_Tab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BPRPJBUD_MainForm_Tab);
IWebElement mTab = BPRPJBUD_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Previous").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BPRPJBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJBUD] Perfoming VerifyExists on Identification_Previous_SelectionRanges_Identification_AccountsEnd...", Logger.MessageType.INF);
			Control BPRPJBUD_Identification_Previous_SelectionRanges_Identification_AccountsEnd = new Control("Identification_Previous_SelectionRanges_Identification_AccountsEnd", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_ID_TO']");
			CPCommon.AssertEqual(true,BPRPJBUD_Identification_Previous_SelectionRanges_Identification_AccountsEnd.Exists());

												
				CPCommon.CurrentComponent = "BPRPJBUD";
							CPCommon.WaitControlDisplayed(BPRPJBUD_MainForm_Tab);
mTab = BPRPJBUD_MainForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Next").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BPRPJBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJBUD] Perfoming VerifyExists on Identification_Next_BaselineOrWorkplanOptions_Column1...", Logger.MessageType.INF);
			Control BPRPJBUD_Identification_Next_BaselineOrWorkplanOptions_Column1 = new Control("Identification_Next_BaselineOrWorkplanOptions_Column1", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COLUMN_1']");
			CPCommon.AssertEqual(true,BPRPJBUD_Identification_Next_BaselineOrWorkplanOptions_Column1.Exists());

												
				CPCommon.CurrentComponent = "BPRPJBUD";
							CPCommon.WaitControlDisplayed(BPRPJBUD_MainForm);
IWebElement formBttn = BPRPJBUD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BPRPJBUD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BPRPJBUD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BPRPJBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPRPJBUD] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BPRPJBUD_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPRPJBUD_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BPRPJBUD";
							CPCommon.WaitControlDisplayed(BPRPJBUD_MainForm);
formBttn = BPRPJBUD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

