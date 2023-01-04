 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMFCEIL_01_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Union Information Controls", "xpath","//div[@class='navItem'][.='Union Information Controls']").Click();
new Control("Manage Fringe Ceilings by Local", "xpath","//div[@class='navItem'][.='Manage Fringe Ceilings by Local']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMFCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMFCEIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMFCEIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMFCEIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMFCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMFCEIL] Perfoming VerifyExists on Local...", Logger.MessageType.INF);
			Control LDMFCEIL_Local = new Control("Local", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LOCAL_ID']");
			CPCommon.AssertEqual(true,LDMFCEIL_Local.Exists());

												
				CPCommon.CurrentComponent = "LDMFCEIL";
							CPCommon.WaitControlDisplayed(LDMFCEIL_MainForm);
IWebElement formBttn = LDMFCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMFCEIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMFCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMFCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMFCEIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMFCEIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMFCEIL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMFCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMFCEIL] Perfoming VerifyExists on LocalFringeCeilingsForm...", Logger.MessageType.INF);
			Control LDMFCEIL_LocalFringeCeilingsForm = new Control("LocalFringeCeilingsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMFCEIL_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMFCEIL_LocalFringeCeilingsForm.Exists());

												
				CPCommon.CurrentComponent = "LDMFCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMFCEIL] Perfoming VerifyExist on LocalFringeCeilingsTable...", Logger.MessageType.INF);
			Control LDMFCEIL_LocalFringeCeilingsTable = new Control("LocalFringeCeilingsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMFCEIL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMFCEIL_LocalFringeCeilingsTable.Exists());

												
				CPCommon.CurrentComponent = "LDMFCEIL";
							CPCommon.WaitControlDisplayed(LDMFCEIL_LocalFringeCeilingsForm);
formBttn = LDMFCEIL_LocalFringeCeilingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMFCEIL_LocalFringeCeilingsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMFCEIL_LocalFringeCeilingsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMFCEIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMFCEIL] Perfoming VerifyExists on LocalFringeCeilingsForm_StartDate...", Logger.MessageType.INF);
			Control LDMFCEIL_LocalFringeCeilingsForm_StartDate = new Control("LocalFringeCeilingsForm_StartDate", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMFCEIL_CHILD_']/ancestor::form[1]/descendant::*[@id='START_DT']");
			CPCommon.AssertEqual(true,LDMFCEIL_LocalFringeCeilingsForm_StartDate.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDMFCEIL";
							CPCommon.WaitControlDisplayed(LDMFCEIL_MainForm);
formBttn = LDMFCEIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

