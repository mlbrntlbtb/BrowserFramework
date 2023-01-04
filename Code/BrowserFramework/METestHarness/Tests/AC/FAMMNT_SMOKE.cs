 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMMNT_SMOKE : TestScript
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
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Maintenance Records", "xpath","//div[@class='navItem'][.='Maintenance Records']").Click();
new Control("Manage Asset Maintenance Information", "xpath","//div[@class='navItem'][.='Manage Asset Maintenance Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMMNT] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMMNT_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMMNT_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMMNT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMMNT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMMNT_MainForm);
IWebElement formBttn = FAMMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "FAMMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMMNT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMMNT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMMNT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "FAMMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMMNT] Perfoming VerifyExist on MaintainDetailsFormTable...", Logger.MessageType.INF);
			Control FAMMNT_MaintainDetailsFormTable = new Control("MaintainDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMMNT_FATRACKING_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMMNT_MaintainDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMMNT] Perfoming ClickButton on MaintainDetailsForm...", Logger.MessageType.INF);
			Control FAMMNT_MaintainDetailsForm = new Control("MaintainDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMMNT_FATRACKING_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(FAMMNT_MaintainDetailsForm);
formBttn = FAMMNT_MaintainDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMMNT_MaintainDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMMNT_MaintainDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMMNT] Perfoming VerifyExists on MaintainDetails_Date...", Logger.MessageType.INF);
			Control FAMMNT_MaintainDetails_Date = new Control("MaintainDetails_Date", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMMNT_FATRACKING_DTL_']/ancestor::form[1]/descendant::*[@id='TRN_DT']");
			CPCommon.AssertEqual(true,FAMMNT_MaintainDetails_Date.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMMNT";
							CPCommon.WaitControlDisplayed(FAMMNT_MainForm);
formBttn = FAMMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

