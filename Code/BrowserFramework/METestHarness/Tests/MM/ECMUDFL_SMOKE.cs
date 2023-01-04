 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMUDFL_SMOKE : TestScript
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
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("Engineering Change Controls", "xpath","//div[@class='navItem'][.='Engineering Change Controls']").Click();
new Control("Configure Engineering Change User-Defined Labels", "xpath","//div[@class='navItem'][.='Configure Engineering Change User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "ECMUDFL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMUDFL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMUDFL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMUDFL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMUDFL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMUDFL] Perfoming VerifyExist on LabelInfo_LabelInfoTable...", Logger.MessageType.INF);
			Control ECMUDFL_LabelInfo_LabelInfoTable = new Control("LabelInfo_LabelInfoTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMUDFL_LabelInfo_LabelInfoTable.Exists());

												
				CPCommon.CurrentComponent = "ECMUDFL";
							CPCommon.WaitControlDisplayed(ECMUDFL_MainForm);
IWebElement formBttn = ECMUDFL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMUDFL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMUDFL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "ECMUDFL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMUDFL] Perfoming VerifyExists on LabelInfo_SequenceNumber...", Logger.MessageType.INF);
			Control ECMUDFL_LabelInfo_SequenceNumber = new Control("LabelInfo_SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,ECMUDFL_LabelInfo_SequenceNumber.Exists());

											Driver.SessionLogger.WriteLine("validated text");


												
				CPCommon.CurrentComponent = "ECMUDFL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMUDFL] Perfoming VerifyExists on ValidatedTextForm...", Logger.MessageType.INF);
			Control ECMUDFL_ValidatedTextForm = new Control("ValidatedTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ECMUDFL_ValidatedTextForm.Exists());

												
				CPCommon.CurrentComponent = "ECMUDFL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMUDFL] Perfoming VerifyExist on ValidatedTextTable...", Logger.MessageType.INF);
			Control ECMUDFL_ValidatedTextTable = new Control("ValidatedTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMUDFL_ValidatedTextTable.Exists());

												
				CPCommon.CurrentComponent = "ECMUDFL";
							CPCommon.WaitControlDisplayed(ECMUDFL_ValidatedTextForm);
formBttn = ECMUDFL_ValidatedTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "ECMUDFL";
							CPCommon.WaitControlDisplayed(ECMUDFL_MainForm);
formBttn = ECMUDFL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

