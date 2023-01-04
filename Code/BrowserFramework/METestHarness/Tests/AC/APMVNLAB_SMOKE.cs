 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMVNLAB_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Vendor and Subcontractor Controls", "xpath","//div[@class='navItem'][.='Vendor and Subcontractor Controls']").Click();
new Control("Manage Vendor User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Vendor User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "APMVNLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVNLAB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMVNLAB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVNLAB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVNLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVNLAB] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control APMVNLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(APMVNLAB_MainForm);
IWebElement formBttn = APMVNLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? APMVNLAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
APMVNLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "APMVNLAB";
							CPCommon.AssertEqual(true,APMVNLAB_MainForm.Exists());

													
				CPCommon.CurrentComponent = "APMVNLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVNLAB] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control APMVNLAB_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,APMVNLAB_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "APMVNLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVNLAB] Perfoming VerifyExists on ValidatedTextLink...", Logger.MessageType.INF);
			Control APMVNLAB_ValidatedTextLink = new Control("ValidatedTextLink", "ID", "lnk_1002827_CPMUDLAB_UDEFLBL_USERDEFLABELS");
			CPCommon.AssertEqual(true,APMVNLAB_ValidatedTextLink.Exists());

												
				CPCommon.CurrentComponent = "APMVNLAB";
							CPCommon.WaitControlDisplayed(APMVNLAB_ValidatedTextLink);
APMVNLAB_ValidatedTextLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Validated Text");


												
				CPCommon.CurrentComponent = "APMVNLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMVNLAB] Perfoming VerifyExist on ValidatedTextFormTable...", Logger.MessageType.INF);
			Control APMVNLAB_ValidatedTextFormTable = new Control("ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMVNLAB_ValidatedTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "APMVNLAB";
							CPCommon.WaitControlDisplayed(APMVNLAB_MainForm);
formBttn = APMVNLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

