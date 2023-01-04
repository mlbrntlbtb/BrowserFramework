 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARMUSRDF_SMOKE : TestScript
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
new Control("Accounts Receivable Controls", "xpath","//div[@class='navItem'][.='Accounts Receivable Controls']").Click();
new Control("Manage Customer User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Customer User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARMUSRDF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMUSRDF] Perfoming VerifyExist on LabelInfoFormTable...", Logger.MessageType.INF);
			Control ARMUSRDF_LabelInfoFormTable = new Control("LabelInfoFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMUSRDF_LabelInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARMUSRDF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMUSRDF] Perfoming ClickButton on LabelInfoForm...", Logger.MessageType.INF);
			Control ARMUSRDF_LabelInfoForm = new Control("LabelInfoForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ARMUSRDF_LabelInfoForm);
IWebElement formBttn = ARMUSRDF_LabelInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ARMUSRDF_LabelInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ARMUSRDF_LabelInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ARMUSRDF";
							CPCommon.AssertEqual(true,ARMUSRDF_LabelInfoForm.Exists());

													
				CPCommon.CurrentComponent = "ARMUSRDF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMUSRDF] Perfoming VerifyExists on LabelInfo_SequenceNumber...", Logger.MessageType.INF);
			Control ARMUSRDF_LabelInfo_SequenceNumber = new Control("LabelInfo_SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,ARMUSRDF_LabelInfo_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "ARMUSRDF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARMUSRDF] Perfoming VerifyExist on ValidatedTextFormTable...", Logger.MessageType.INF);
			Control ARMUSRDF_ValidatedTextFormTable = new Control("ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARMUSRDF_ValidatedTextFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARMUSRDF";
							CPCommon.WaitControlDisplayed(ARMUSRDF_LabelInfoForm);
formBttn = ARMUSRDF_LabelInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

