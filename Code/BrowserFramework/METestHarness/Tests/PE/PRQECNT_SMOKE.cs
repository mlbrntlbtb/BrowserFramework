 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQECNT_SMOKE : TestScript
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
new Control("View Contributions", "xpath","//div[@class='navItem'][.='View Contributions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRQECNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQECNT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQECNT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQECNT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQECNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQECNT] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQECNT_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PRQECNT_Identification_Employee.Exists());

											Driver.SessionLogger.WriteLine("Inquiry Details Form");


												
				CPCommon.CurrentComponent = "PRQECNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQECNT] Perfoming VerifyExists on InquiryDetailsForm...", Logger.MessageType.INF);
			Control PRQECNT_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQECNT_EMPLCNTRBADT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQECNT_InquiryDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQECNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQECNT] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control PRQECNT_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQECNT_EMPLCNTRBADT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQECNT_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQECNT";
							CPCommon.WaitControlDisplayed(PRQECNT_InquiryDetailsForm);
IWebElement formBttn = PRQECNT_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQECNT_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQECNT_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQECNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQECNT] Perfoming VerifyExists on InquiryDetails_Transaction_TransactionType...", Logger.MessageType.INF);
			Control PRQECNT_InquiryDetails_Transaction_TransactionType = new Control("InquiryDetails_Transaction_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQECNT_EMPLCNTRBADT_CHLD_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQECNT_InquiryDetails_Transaction_TransactionType.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQECNT";
							CPCommon.WaitControlDisplayed(PRQECNT_MainForm);
formBttn = PRQECNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

