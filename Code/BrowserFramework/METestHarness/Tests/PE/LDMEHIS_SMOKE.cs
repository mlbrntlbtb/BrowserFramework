 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMEHIS_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Basic Employee Information", "xpath","//div[@class='navItem'][.='Basic Employee Information']").Click();
new Control("Manage Employee Salary Information", "xpath","//div[@class='navItem'][.='Manage Employee Salary Information']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMEHIS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMEHIS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control LDMEHIS_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,LDMEHIS_Employee.Exists());

												
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExists on Identification_Tab...", Logger.MessageType.INF);
			Control LDMEHIS_Identification_Tab = new Control("Identification_Tab", "XPATH", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,LDMEHIS_Identification_Tab.Exists());

												
				CPCommon.CurrentComponent = "LDMEHIS";
							CPCommon.WaitControlDisplayed(LDMEHIS_Identification_Tab);
IWebElement mTab = LDMEHIS_Identification_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Salary Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExists on SalaryDetails_SalaryInfo_Dates_EffectiveDate...", Logger.MessageType.INF);
			Control LDMEHIS_SalaryDetails_SalaryInfo_Dates_EffectiveDate = new Control("SalaryDetails_SalaryInfo_Dates_EffectiveDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,LDMEHIS_SalaryDetails_SalaryInfo_Dates_EffectiveDate.Exists());

												
				CPCommon.CurrentComponent = "LDMEHIS";
							CPCommon.WaitControlDisplayed(LDMEHIS_Identification_Tab);
mTab = LDMEHIS_Identification_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "HR Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExists on SalaryDetails_HRInfo_CompensationData_CompensationPlan...", Logger.MessageType.INF);
			Control LDMEHIS_SalaryDetails_HRInfo_CompensationData_CompensationPlan = new Control("SalaryDetails_HRInfo_CompensationData_CompensationPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,LDMEHIS_SalaryDetails_HRInfo_CompensationData_CompensationPlan.Exists());

												
				CPCommon.CurrentComponent = "LDMEHIS";
							CPCommon.WaitControlDisplayed(LDMEHIS_Identification_Tab);
mTab = LDMEHIS_Identification_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExists on SalaryDetails_RefNoComments_Comments...", Logger.MessageType.INF);
			Control LDMEHIS_SalaryDetails_RefNoComments_Comments = new Control("SalaryDetails_RefNoComments_Comments", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMMENTS']");
			CPCommon.AssertEqual(true,LDMEHIS_SalaryDetails_RefNoComments_Comments.Exists());

												
				CPCommon.CurrentComponent = "LDMEHIS";
							CPCommon.WaitControlDisplayed(LDMEHIS_MainForm);
IWebElement formBttn = LDMEHIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMEHIS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMEHIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMEHIS] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control LDMEHIS_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMEHIS_MainTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDMEHIS";
							CPCommon.WaitControlDisplayed(LDMEHIS_MainForm);
formBttn = LDMEHIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

