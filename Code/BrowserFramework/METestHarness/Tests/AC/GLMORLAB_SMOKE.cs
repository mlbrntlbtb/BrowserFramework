 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMORLAB_SMOKE : TestScript
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
new Control("Organizations", "xpath","//div[@class='navItem'][.='Organizations']").Click();
new Control("Manage Organization User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Organization User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMORLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORLAB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMORLAB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMORLAB_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMORLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORLAB] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control GLMORLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMORLAB_MainForm);
IWebElement formBttn = GLMORLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMORLAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMORLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMORLAB";
							CPCommon.AssertEqual(true,GLMORLAB_MainForm.Exists());

													
				CPCommon.CurrentComponent = "GLMORLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORLAB] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control GLMORLAB_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,GLMORLAB_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "GLMORLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORLAB] Perfoming VerifyExists on ValidatedTextLink...", Logger.MessageType.INF);
			Control GLMORLAB_ValidatedTextLink = new Control("ValidatedTextLink", "ID", "lnk_1002827_CPMUDLAB_UDEFLBL_USERDEFLABELS");
			CPCommon.AssertEqual(true,GLMORLAB_ValidatedTextLink.Exists());

												
				CPCommon.CurrentComponent = "GLMORLAB";
							CPCommon.WaitControlDisplayed(GLMORLAB_ValidatedTextLink);
GLMORLAB_ValidatedTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMORLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORLAB] Perfoming VerifyExist on ValidatedTextFormTable...", Logger.MessageType.INF);
			Control GLMORLAB_ValidatedTextFormTable = new Control("ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMORLAB_ValidatedTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMORLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORLAB] Perfoming VerifyExists on ValidatedTextForm...", Logger.MessageType.INF);
			Control GLMORLAB_ValidatedTextForm = new Control("ValidatedTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMORLAB_ValidatedTextForm.Exists());

												
				CPCommon.CurrentComponent = "GLMORLAB";
							CPCommon.WaitControlDisplayed(GLMORLAB_MainForm);
formBttn = GLMORLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

