 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMACLAB_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("Accounts", "xpath","//div[@class='navItem'][.='Accounts']").Click();
new Control("Manage Account User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Account User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMACLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACLAB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMACLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMACLAB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMACLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACLAB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMACLAB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMACLAB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMACLAB";
							CPCommon.WaitControlDisplayed(GLMACLAB_MainForm);
IWebElement formBttn = GLMACLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMACLAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMACLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "GLMACLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACLAB] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control GLMACLAB_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,GLMACLAB_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "GLMACLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACLAB] Perfoming VerifyExists on ValidatedTextLink...", Logger.MessageType.INF);
			Control GLMACLAB_ValidatedTextLink = new Control("ValidatedTextLink", "ID", "lnk_1002827_CPMUDLAB_UDEFLBL_USERDEFLABELS");
			CPCommon.AssertEqual(true,GLMACLAB_ValidatedTextLink.Exists());

												
				CPCommon.CurrentComponent = "GLMACLAB";
							CPCommon.WaitControlDisplayed(GLMACLAB_ValidatedTextLink);
GLMACLAB_ValidatedTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMACLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACLAB] Perfoming VerifyExists on ValidatedTextForm...", Logger.MessageType.INF);
			Control GLMACLAB_ValidatedTextForm = new Control("ValidatedTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMACLAB_ValidatedTextForm.Exists());

												
				CPCommon.CurrentComponent = "GLMACLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMACLAB] Perfoming VerifyExist on ValidatedTextFormTable...", Logger.MessageType.INF);
			Control GLMACLAB_ValidatedTextFormTable = new Control("ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMACLAB_ValidatedTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMACLAB";
							CPCommon.WaitControlDisplayed(GLMACLAB_MainForm);
formBttn = GLMACLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

