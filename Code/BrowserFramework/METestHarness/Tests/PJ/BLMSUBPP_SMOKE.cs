 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMSUBPP_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing History", "xpath","//div[@class='navItem'][.='Billing History']").Click();
new Control("Manage Subcontractor Progress Payments", "xpath","//div[@class='navItem'][.='Manage Subcontractor Progress Payments']").Click();


											Driver.SessionLogger.WriteLine("QUERY");


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMSUBPP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMSUBPP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BLMSUBPP_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMSUBPP_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMSUBPP";
							CPCommon.WaitControlDisplayed(BLMSUBPP_MainForm);
IWebElement formBttn = BLMSUBPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMSUBPP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMSUBPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMSUBPP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMSUBPP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExist on InvoiceDetailsFormTable...", Logger.MessageType.INF);
			Control BLMSUBPP_InvoiceDetailsFormTable = new Control("InvoiceDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMSUBPP_SUBCTRPRGPMT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMSUBPP_InvoiceDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming ClickButton on InvoiceDetailsForm...", Logger.MessageType.INF);
			Control BLMSUBPP_InvoiceDetailsForm = new Control("InvoiceDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMSUBPP_SUBCTRPRGPMT_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMSUBPP_InvoiceDetailsForm);
formBttn = BLMSUBPP_InvoiceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMSUBPP_InvoiceDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMSUBPP_InvoiceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExists on InvoiceDetails_VoucherNumber...", Logger.MessageType.INF);
			Control BLMSUBPP_InvoiceDetails_VoucherNumber = new Control("InvoiceDetails_VoucherNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMSUBPP_SUBCTRPRGPMT_CHLD_']/ancestor::form[1]/descendant::*[@id='VCHR_NO']");
			CPCommon.AssertEqual(true,BLMSUBPP_InvoiceDetails_VoucherNumber.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORM LINK");


												
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExists on InvoiceDetails_PaymentsLink...", Logger.MessageType.INF);
			Control BLMSUBPP_InvoiceDetails_PaymentsLink = new Control("InvoiceDetails_PaymentsLink", "ID", "lnk_1002481_BLMSUBPP_SUBCTRPRGPMT_CHLD");
			CPCommon.AssertEqual(true,BLMSUBPP_InvoiceDetails_PaymentsLink.Exists());

												
				CPCommon.CurrentComponent = "BLMSUBPP";
							CPCommon.WaitControlDisplayed(BLMSUBPP_InvoiceDetails_PaymentsLink);
BLMSUBPP_InvoiceDetails_PaymentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMSUBPP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMSUBPP] Perfoming VerifyExist on PaymentsFormTable...", Logger.MessageType.INF);
			Control BLMSUBPP_PaymentsFormTable = new Control("PaymentsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMSUBPP_SUBCTRPRGPMTCHK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMSUBPP_PaymentsFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMSUBPP";
							CPCommon.WaitControlDisplayed(BLMSUBPP_MainForm);
formBttn = BLMSUBPP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

