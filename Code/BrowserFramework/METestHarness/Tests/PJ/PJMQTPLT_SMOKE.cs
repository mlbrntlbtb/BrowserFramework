 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMQTPLT_SMOKE : TestScript
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
new Control("Manage Project Templates", "xpath","//div[@class='navItem'][.='Manage Project Templates']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMQTPLT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMQTPLT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on TemplateID...", Logger.MessageType.INF);
			Control PJMQTPLT_TemplateID = new Control("TemplateID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TEMPLATE_ID']");
			CPCommon.AssertEqual(true,PJMQTPLT_TemplateID.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PJMQTPLT_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJMQTPLT_MainFormTab);
IWebElement mTab = PJMQTPLT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Basic Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on BasicInfo_Role_Project...", Logger.MessageType.INF);
			Control PJMQTPLT_BasicInfo_Role_Project = new Control("BasicInfo_Role_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID_ROLE']");
			CPCommon.AssertEqual(true,PJMQTPLT_BasicInfo_Role_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
							CPCommon.WaitControlDisplayed(PJMQTPLT_MainFormTab);
mTab = PJMQTPLT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Revenue Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on RevenueInfo_Role_RevenueFormula...", Logger.MessageType.INF);
			Control PJMQTPLT_RevenueInfo_Role_RevenueFormula = new Control("RevenueInfo_Role_RevenueFormula", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_REV_FORMULA_ROLE']");
			CPCommon.AssertEqual(true,PJMQTPLT_RevenueInfo_Role_RevenueFormula.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
							CPCommon.WaitControlDisplayed(PJMQTPLT_MainFormTab);
mTab = PJMQTPLT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Billing Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on BillingInfo_Role_GenericBillingFormat...", Logger.MessageType.INF);
			Control PJMQTPLT_BillingInfo_Role_GenericBillingFormat = new Control("BillingInfo_Role_GenericBillingFormat", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BILL_FRMT_CD_ROLE']");
			CPCommon.AssertEqual(true,PJMQTPLT_BillingInfo_Role_GenericBillingFormat.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
							CPCommon.WaitControlDisplayed(PJMQTPLT_MainFormTab);
mTab = PJMQTPLT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Modifications").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on Modifications_Role_ModificationInformation...", Logger.MessageType.INF);
			Control PJMQTPLT_Modifications_Role_ModificationInformation = new Control("Modifications_Role_ModificationInformation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_MOD_ROLE']");
			CPCommon.AssertEqual(true,PJMQTPLT_Modifications_Role_ModificationInformation.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
							CPCommon.WaitControlDisplayed(PJMQTPLT_MainFormTab);
mTab = PJMQTPLT_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExists on Notes_Role_Notes...", Logger.MessageType.INF);
			Control PJMQTPLT_Notes_Role_Notes = new Control("Notes_Role_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NOTES_ROLE']");
			CPCommon.AssertEqual(true,PJMQTPLT_Notes_Role_Notes.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
							CPCommon.WaitControlDisplayed(PJMQTPLT_MainForm);
IWebElement formBttn = PJMQTPLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMQTPLT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMQTPLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMQTPLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMQTPLT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMQTPLT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMQTPLT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMQTPLT";
							CPCommon.WaitControlDisplayed(PJMQTPLT_MainForm);
formBttn = PJMQTPLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

