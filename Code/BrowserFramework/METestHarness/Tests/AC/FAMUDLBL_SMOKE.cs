 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMUDLBL_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Controls", "xpath","//div[@class='navItem'][.='Fixed Assets Controls']").Click();
new Control("Manage Asset Master User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Asset Master User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAMUDLBL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDLBL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMUDLBL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMUDLBL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMUDLBL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDLBL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMUDLBL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMUDLBL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMUDLBL";
							CPCommon.WaitControlDisplayed(FAMUDLBL_MainForm);
IWebElement formBttn = FAMUDLBL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMUDLBL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMUDLBL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAMUDLBL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDLBL] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control FAMUDLBL_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,FAMUDLBL_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "FAMUDLBL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDLBL] Perfoming VerifyExists on ValidatedTextLink...", Logger.MessageType.INF);
			Control FAMUDLBL_ValidatedTextLink = new Control("ValidatedTextLink", "ID", "lnk_1002827_CPMUDLAB_UDEFLBL_USERDEFLABELS");
			CPCommon.AssertEqual(true,FAMUDLBL_ValidatedTextLink.Exists());

												
				CPCommon.CurrentComponent = "FAMUDLBL";
							CPCommon.WaitControlDisplayed(FAMUDLBL_ValidatedTextLink);
FAMUDLBL_ValidatedTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "FAMUDLBL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDLBL] Perfoming VerifyExist on ValidatedTextTable...", Logger.MessageType.INF);
			Control FAMUDLBL_ValidatedTextTable = new Control("ValidatedTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMUDLBL_ValidatedTextTable.Exists());

												
				CPCommon.CurrentComponent = "FAMUDLBL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUDLBL] Perfoming VerifyExists on ValidatedTextForm...", Logger.MessageType.INF);
			Control FAMUDLBL_ValidatedTextForm = new Control("ValidatedTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMUDLBL_ValidatedTextForm.Exists());

												
				CPCommon.CurrentComponent = "FAMUDLBL";
							CPCommon.WaitControlDisplayed(FAMUDLBL_MainForm);
formBttn = FAMUDLBL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

