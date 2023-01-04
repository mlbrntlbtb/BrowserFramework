 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMUDLAB_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Setup Controls", "xpath","//div[@class='navItem'][.='Project Setup Controls']").Click();
new Control("Manage User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMUDLAB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMUDLAB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMUDLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMUDLAB_MainForm);
IWebElement formBttn = PJMUDLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMUDLAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMUDLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMUDLAB";
							CPCommon.AssertEqual(true,PJMUDLAB_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control PJMUDLAB_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,PJMUDLAB_SequenceNumber.Exists());

											Driver.SessionLogger.WriteLine("VALIDATED TEXT");


												
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming VerifyExists on ValidatedTextLink...", Logger.MessageType.INF);
			Control PJMUDLAB_ValidatedTextLink = new Control("ValidatedTextLink", "ID", "lnk_1002827_CPMUDLAB_UDEFLBL_USERDEFLABELS");
			CPCommon.AssertEqual(true,PJMUDLAB_ValidatedTextLink.Exists());

												
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming VerifyExist on ValidatedTextFormTable...", Logger.MessageType.INF);
			Control PJMUDLAB_ValidatedTextFormTable = new Control("ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMUDLAB_ValidatedTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming VerifyExists on ValidatedTextForm...", Logger.MessageType.INF);
			Control PJMUDLAB_ValidatedTextForm = new Control("ValidatedTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMUDLAB_ValidatedTextForm.Exists());

												
				CPCommon.CurrentComponent = "PJMUDLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMUDLAB] Perfoming VerifyExists on ValidatedText_Ok...", Logger.MessageType.INF);
			Control PJMUDLAB_ValidatedText_Ok = new Control("ValidatedText_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,PJMUDLAB_ValidatedText_Ok.Exists());

												
				CPCommon.CurrentComponent = "PJMUDLAB";
							CPCommon.WaitControlDisplayed(PJMUDLAB_ValidatedTextForm);
formBttn = PJMUDLAB_ValidatedTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMUDLAB";
							CPCommon.WaitControlDisplayed(PJMUDLAB_MainForm);
formBttn = PJMUDLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

