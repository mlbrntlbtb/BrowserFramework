 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARPPADJ_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Cash Receipts Processing", "xpath","//div[@class='navItem'][.='Cash Receipts Processing']").Click();
new Control("Edit/Manage A/R Underpayment Amounts", "xpath","//div[@class='navItem'][.='Edit/Manage A/R Underpayment Amounts']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARPPADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPADJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARPPADJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARPPADJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARPPADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPADJ] Perfoming VerifyExists on CustomerAccount...", Logger.MessageType.INF);
			Control ARPPADJ_CustomerAccount = new Control("CustomerAccount", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ACCT']");
			CPCommon.AssertEqual(true,ARPPADJ_CustomerAccount.Exists());

												
				CPCommon.CurrentComponent = "ARPPADJ";
							ARPPADJ_CustomerAccount.Click();
ARPPADJ_CustomerAccount.SendKeys("1", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
ARPPADJ_CustomerAccount.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "ARPPADJ";
							CPCommon.AssertEqual("1",ARPPADJ_CustomerAccount.GetAttributeValue("value"));


													
				CPCommon.CurrentComponent = "ARPPADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPADJ] Perfoming VerifyExist on AdjustmentInfoFormTable...", Logger.MessageType.INF);
			Control ARPPADJ_AdjustmentInfoFormTable = new Control("AdjustmentInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ARPPADJ_TABLE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARPPADJ_AdjustmentInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARPPADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPADJ] Perfoming ClickButton on AdjustmentInfoForm...", Logger.MessageType.INF);
			Control ARPPADJ_AdjustmentInfoForm = new Control("AdjustmentInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ARPPADJ_TABLE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARPPADJ_AdjustmentInfoForm);
IWebElement formBttn = ARPPADJ_AdjustmentInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARPPADJ_AdjustmentInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARPPADJ_AdjustmentInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARPPADJ";
							CPCommon.AssertEqual(true,ARPPADJ_AdjustmentInfoForm.Exists());

													
				CPCommon.CurrentComponent = "ARPPADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPADJ] Perfoming VerifyExists on AdjustmentInfo_InvoiceNumber...", Logger.MessageType.INF);
			Control ARPPADJ_AdjustmentInfo_InvoiceNumber = new Control("AdjustmentInfo_InvoiceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__ARPPADJ_TABLE_']/ancestor::form[1]/descendant::*[@id='INVC_ID']");
			CPCommon.AssertEqual(true,ARPPADJ_AdjustmentInfo_InvoiceNumber.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARPPADJ";
							CPCommon.WaitControlDisplayed(ARPPADJ_MainForm);
formBttn = ARPPADJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

