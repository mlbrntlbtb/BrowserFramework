 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDQLHF_SMOKE : TestScript
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
new Control("Leave", "xpath","//div[@class='deptItem'][.='Leave']").Click();
new Control("Leave Inquiries", "xpath","//div[@class='navItem'][.='Leave Inquiries']").Click();
new Control("View Leave History", "xpath","//div[@class='navItem'][.='View Leave History']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "LDQLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQLHF] Perfoming ClickButtonIfExists on SelectionCriteriaForm...", Logger.MessageType.INF);
			Control LDQLHF_SelectionCriteriaForm = new Control("SelectionCriteriaForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(LDQLHF_SelectionCriteriaForm);
IWebElement formBttn = LDQLHF_SelectionCriteriaForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDQLHF_SelectionCriteriaForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDQLHF_SelectionCriteriaForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "LDQLHF";
							CPCommon.AssertEqual(true,LDQLHF_SelectionCriteriaForm.Exists());

													
				CPCommon.CurrentComponent = "LDQLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQLHF] Perfoming VerifyExists on SelectionCriteria_Employee...", Logger.MessageType.INF);
			Control LDQLHF_SelectionCriteria_Employee = new Control("SelectionCriteria_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,LDQLHF_SelectionCriteria_Employee.Exists());

												
				CPCommon.CurrentComponent = "LDQLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQLHF] Perfoming VerifyExists on InquiryDetailsForm...", Logger.MessageType.INF);
			Control LDQLHF_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDQLHF_InquiryDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "LDQLHF";
							CPCommon.WaitControlDisplayed(LDQLHF_InquiryDetailsForm);
formBttn = LDQLHF_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDQLHF_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDQLHF_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "LDQLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQLHF] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control LDQLHF_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDQLHF_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDQLHF";
							CPCommon.WaitControlDisplayed(LDQLHF_InquiryDetailsForm);
formBttn = LDQLHF_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDQLHF_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDQLHF_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDQLHF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDQLHF] Perfoming VerifyExists on InquiryDetails_Leave_LeaveType...", Logger.MessageType.INF);
			Control LDQLHF_InquiryDetails_Leave_LeaveType = new Control("InquiryDetails_Leave_LeaveType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LV_TYPE_CD']");
			CPCommon.AssertEqual(true,LDQLHF_InquiryDetails_Leave_LeaveType.Exists());

												
				CPCommon.CurrentComponent = "LDQLHF";
							CPCommon.WaitControlDisplayed(LDQLHF_InquiryDetailsForm);
formBttn = LDQLHF_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

