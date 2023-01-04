 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRQEDED_SMOKE : TestScript
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
new Control("View Deductions", "xpath","//div[@class='navItem'][.='View Deductions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRQEDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEDED] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRQEDED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRQEDED_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEDED] Perfoming VerifyExists on Identification_Employee...", Logger.MessageType.INF);
			Control PRQEDED_Identification_Employee = new Control("Identification_Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMP_ID']");
			CPCommon.AssertEqual(true,PRQEDED_Identification_Employee.Exists());

											Driver.SessionLogger.WriteLine("Inquiry Details Form");


												
				CPCommon.CurrentComponent = "PRQEDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEDED] Perfoming VerifyExists on InquiryDetailsForm...", Logger.MessageType.INF);
			Control PRQEDED_InquiryDetailsForm = new Control("InquiryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEDED_EMPLDEDADT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRQEDED_InquiryDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PRQEDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEDED] Perfoming VerifyExist on InquiryDetailsFormTable...", Logger.MessageType.INF);
			Control PRQEDED_InquiryDetailsFormTable = new Control("InquiryDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEDED_EMPLDEDADT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRQEDED_InquiryDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRQEDED";
							CPCommon.WaitControlDisplayed(PRQEDED_InquiryDetailsForm);
IWebElement formBttn = PRQEDED_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PRQEDED_InquiryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PRQEDED_InquiryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PRQEDED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRQEDED] Perfoming VerifyExists on InquiryDetails_TransactionType...", Logger.MessageType.INF);
			Control PRQEDED_InquiryDetails_TransactionType = new Control("InquiryDetails_TransactionType", "xpath", "//div[translate(@id,'0123456789','')='pr__PRQEDED_EMPLDEDADT_CHLD_']/ancestor::form[1]/descendant::*[@id='S_MOD_TYPE_CD']");
			CPCommon.AssertEqual(true,PRQEDED_InquiryDetails_TransactionType.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRQEDED";
							CPCommon.WaitControlDisplayed(PRQEDED_MainForm);
formBttn = PRQEDED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

