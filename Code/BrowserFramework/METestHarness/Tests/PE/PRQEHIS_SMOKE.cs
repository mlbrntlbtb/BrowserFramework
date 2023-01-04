 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQEHIS_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Reports/Inquiries", "xpath","//div[@class='navItem'][.='Payroll Reports/Inquiries']").Click();
new Control("View Salary Information and History", "xpath","//div[@class='navItem'][.='View Salary Information and History']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQEHIS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQEHIS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQEHIS_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQEHIS_Identification_Employee.Exists());

												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Inquiry Details Form");


												
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on InquiryDetailsForm...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
							CPCommon.WaitControlDisplayed(PRQEHIS_InquiryDetailsForm);
IWebElement formBttn = PRQEHIS_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQEHIS_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQEHIS_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on InquiryDetails_TransactionType...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetails_TransactionType = new Control("InquiryDetails_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetails_TransactionType.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on InquiryDetails_InquiryDetailsTab...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetails_InquiryDetailsTab = new Control("InquiryDetails_InquiryDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetails_InquiryDetailsTab.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
							CPCommon.WaitControlDisplayed(PRQEHIS_InquiryDetails_InquiryDetailsTab);
IWebElement mTab = PRQEHIS_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Salary Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on InquiryDetails_SalaryInformation_EffectiveDate...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetails_SalaryInformation_EffectiveDate = new Control("InquiryDetails_SalaryInformation_EffectiveDate", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetails_SalaryInformation_EffectiveDate.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
							CPCommon.WaitControlDisplayed(PRQEHIS_InquiryDetails_InquiryDetailsTab);
mTab = PRQEHIS_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "HR Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on InquiryDetails_HRInformation_CompensationData_CompensationPlan...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetails_HRInformation_CompensationData_CompensationPlan = new Control("InquiryDetails_HRInformation_CompensationData_CompensationPlan", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetails_HRInformation_CompensationData_CompensationPlan.Exists());

												
				CPCommon.CurrentComponent = "PRQEHIS";
							CPCommon.WaitControlDisplayed(PRQEHIS_InquiryDetails_InquiryDetailsTab);
mTab = PRQEHIS_InquiryDetails_InquiryDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Comments").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PRQEHIS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEHIS] Perfoming VerifyExists on InquiryDetails_Comments_Comments_Comments...", Logger.MessageType.INF);
			Control PRQEHIS_InquiryDetails_Comments_Comments_Comments = new Control("InquiryDetails_Comments_Comments_Comments", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEHIS_EMPLLABINFOADT_CHILD_']/ancestor::form[1]/descendant::*[@id='COMMENTS']");
			CPCommon.AssertEqual(true,PRQEHIS_InquiryDetails_Comments_Comments_Comments.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQEHIS";
							CPCommon.WaitControlDisplayed(PRQEHIS_MainForm);
formBttn = PRQEHIS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

