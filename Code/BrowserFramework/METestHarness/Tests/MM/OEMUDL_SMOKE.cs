 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMUDL_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Controls", "xpath","//div[@class='navItem'][.='Sales Order Entry Controls']").Click();
new Control("Manage Order User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Order User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "OEMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMUDL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMUDL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMUDL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMUDL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control OEMUDL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(OEMUDL_MainForm);
IWebElement formBttn = OEMUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMUDL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMUDL";
							CPCommon.AssertEqual(true,OEMUDL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "OEMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMUDL] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control OEMUDL_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,OEMUDL_SequenceNumber.Exists());

											Driver.SessionLogger.WriteLine("VALIDATEDTEXT");


												
				CPCommon.CurrentComponent = "OEMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMUDL] Perfoming VerifyExist on ValidatedTextFormTable...", Logger.MessageType.INF);
			Control OEMUDL_ValidatedTextFormTable = new Control("ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMUDL_ValidatedTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMUDL] Perfoming Close on ValidatedTextForm...", Logger.MessageType.INF);
			Control OEMUDL_ValidatedTextForm = new Control("ValidatedTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(OEMUDL_ValidatedTextForm);
formBttn = OEMUDL_ValidatedTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "OEMUDL";
							CPCommon.WaitControlDisplayed(OEMUDL_MainForm);
formBttn = OEMUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

