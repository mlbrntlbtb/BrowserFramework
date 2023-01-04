 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMGOVT_SMOKE : TestScript
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
new Control("Manage Government Contract Information", "xpath","//div[@class='navItem'][.='Manage Government Contract Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMGOVT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMGOVT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMGOVT_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMGOVT_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control PJMGOVT_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJMGOVT_MainTab);
IWebElement mTab = PJMGOVT_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Contract Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming VerifyExists on ContractDetails_ContractOfficer...", Logger.MessageType.INF);
			Control PJMGOVT_ContractDetails_ContractOfficer = new Control("ContractDetails_ContractOfficer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COTR_NAME']");
			CPCommon.AssertEqual(true,PJMGOVT_ContractDetails_ContractOfficer.Exists());

												
				CPCommon.CurrentComponent = "PJMGOVT";
							CPCommon.WaitControlDisplayed(PJMGOVT_MainTab);
mTab = PJMGOVT_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Telephone Numbers").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming VerifyExists on TelephoneNumbers_AdministrativeContractingOfficer_Phone...", Logger.MessageType.INF);
			Control PJMGOVT_TelephoneNumbers_AdministrativeContractingOfficer_Phone = new Control("TelephoneNumbers_AdministrativeContractingOfficer_Phone", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACO_PHONE_ID']");
			CPCommon.AssertEqual(true,PJMGOVT_TelephoneNumbers_AdministrativeContractingOfficer_Phone.Exists());

												
				CPCommon.CurrentComponent = "PJMGOVT";
							CPCommon.WaitControlDisplayed(PJMGOVT_MainTab);
mTab = PJMGOVT_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Statement of Work").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming VerifyExists on StatementOfWork_StatementOfWork...", Logger.MessageType.INF);
			Control PJMGOVT_StatementOfWork_StatementOfWork = new Control("StatementOfWork_StatementOfWork", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='STMT_OF_WORK_TX']");
			CPCommon.AssertEqual(true,PJMGOVT_StatementOfWork_StatementOfWork.Exists());

												
				CPCommon.CurrentComponent = "PJMGOVT";
							CPCommon.WaitControlDisplayed(PJMGOVT_MainForm);
IWebElement formBttn = PJMGOVT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMGOVT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMGOVT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMGOVT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMGOVT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMGOVT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMGOVT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMGOVT";
							CPCommon.WaitControlDisplayed(PJMGOVT_MainForm);
formBttn = PJMGOVT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

