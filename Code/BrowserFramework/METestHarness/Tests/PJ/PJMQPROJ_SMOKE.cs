 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMQPROJ_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Master", "xpath","//div[@class='navItem'][.='Project Master']").Click();
new Control("Manage Project Master Data from Templates", "xpath","//div[@class='navItem'][.='Manage Project Master Data from Templates']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMQPROJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMQPROJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on TemplateID...", Logger.MessageType.INF);
			Control PJMQPROJ_TemplateID = new Control("TemplateID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TEMPLATE_ID']");
			CPCommon.AssertEqual(true,PJMQPROJ_TemplateID.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainForm);
IWebElement formBttn = PJMQPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMQPROJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMQPROJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMQPROJ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMQPROJ_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainForm);
formBttn = PJMQPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMQPROJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMQPROJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PJMQPROJ_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJMQPROJ_MainFormTab);
IWebElement mTab = PJMQPROJ_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on BasicInfo_ProjectManager...", Logger.MessageType.INF);
			Control PJMQPROJ_BasicInfo_ProjectManager = new Control("BasicInfo_ProjectManager", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PJMQPROJ_BasicInfo_ProjectManager.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainFormTab);
mTab = PJMQPROJ_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Revenue Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on RevenueInfo_RevenueFormula...", Logger.MessageType.INF);
			Control PJMQPROJ_RevenueInfo_RevenueFormula = new Control("RevenueInfo_RevenueFormula", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_REV_FORMULA_CD']");
			CPCommon.AssertEqual(true,PJMQPROJ_RevenueInfo_RevenueFormula.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainFormTab);
mTab = PJMQPROJ_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on BillingInfo_GenericBillingFormat...", Logger.MessageType.INF);
			Control PJMQPROJ_BillingInfo_GenericBillingFormat = new Control("BillingInfo_GenericBillingFormat", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_FRMT_CD']");
			CPCommon.AssertEqual(true,PJMQPROJ_BillingInfo_GenericBillingFormat.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainFormTab);
mTab = PJMQPROJ_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Modifications").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on Modifications_ModificationID...", Logger.MessageType.INF);
			Control PJMQPROJ_Modifications_ModificationID = new Control("Modifications_ModificationID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_MOD_ID']");
			CPCommon.AssertEqual(true,PJMQPROJ_Modifications_ModificationID.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainFormTab);
mTab = PJMQPROJ_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQPROJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQPROJ] Perfoming VerifyExists on Notes_Notes...", Logger.MessageType.INF);
			Control PJMQPROJ_Notes_Notes = new Control("Notes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES']");
			CPCommon.AssertEqual(true,PJMQPROJ_Notes_Notes.Exists());

												
				CPCommon.CurrentComponent = "PJMQPROJ";
							CPCommon.WaitControlDisplayed(PJMQPROJ_MainForm);
formBttn = PJMQPROJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

