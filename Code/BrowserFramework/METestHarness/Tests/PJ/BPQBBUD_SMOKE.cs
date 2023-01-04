 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPQBBUD_SMOKE : TestScript
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
new Control("View Project Budgets", "xpath","//div[@class='navItem'][.='View Project Budgets']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPQBBUD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPQBBUD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming VerifyExists on SelectionCriteria_Project...", Logger.MessageType.INF);
			Control BPQBBUD_SelectionCriteria_Project = new Control("SelectionCriteria_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BPQBBUD_SelectionCriteria_Project.Exists());

												
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control BPQBBUD_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BPQBBUD_PROJBUDGETSUM_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPQBBUD_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming ClickButton on InquiryDetailsForm...", Logger.MessageType.INF);
			Control BPQBBUD_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPQBBUD_PROJBUDGETSUM_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BPQBBUD_InquiryDetailsForm);
IWebElement formBttn = BPQBBUD_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BPQBBUD_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BPQBBUD_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BPQBBUD";
							CPCommon.AssertEqual(true,BPQBBUD_InquiryDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming VerifyExists on InquiryDetails_Project...", Logger.MessageType.INF);
			Control BPQBBUD_InquiryDetails_Project = new Control("InquiryDetails_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__BPQBBUD_PROJBUDGETSUM_CHILD_']/ancestor::form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BPQBBUD_InquiryDetails_Project.Exists());

												
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming Select on InquiryDetails_Tab...", Logger.MessageType.INF);
			Control BPQBBUD_InquiryDetails_Tab = new Control("InquiryDetails_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__BPQBBUD_PROJBUDGETSUM_CHILD_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BPQBBUD_InquiryDetails_Tab);
IWebElement mTab = BPQBBUD_InquiryDetails_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BPQBBUD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPQBBUD] Perfoming VerifyExists on InquiryDetails_Details_Revision...", Logger.MessageType.INF);
			Control BPQBBUD_InquiryDetails_Details_Revision = new Control("InquiryDetails_Details_Revision", "xpath", "//div[translate(@id,'0123456789','')='pr__BPQBBUD_PROJBUDGETSUM_CHILD_']/ancestor::form[1]/descendant::*[@id='REVISION_ID']");
			CPCommon.AssertEqual(true,BPQBBUD_InquiryDetails_Details_Revision.Exists());

												
				CPCommon.CurrentComponent = "BPQBBUD";
							CPCommon.WaitControlDisplayed(BPQBBUD_InquiryDetails_Tab);
mTab = BPQBBUD_InquiryDetails_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Periods").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BPQBBUD";
							CPCommon.WaitControlDisplayed(BPQBBUD_MainForm);
formBttn = BPQBBUD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

