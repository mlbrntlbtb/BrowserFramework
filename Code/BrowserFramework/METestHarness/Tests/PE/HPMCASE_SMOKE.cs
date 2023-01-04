 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMCASE_SMOKE : TestScript
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
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Work-Related Injuries/Illnesses", "xpath","//div[@class='navItem'][.='Work-Related Injuries/Illnesses']").Click();
new Control("Manage Accident Case History", "xpath","//div[@class='navItem'][.='Manage Accident Case History']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HPMCASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMCASE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMCASE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMCASE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMCASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMCASE] Perfoming VerifyExists on InternalCaseNumber...", Logger.MessageType.INF);
			Control HPMCASE_InternalCaseNumber = new Control("InternalCaseNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INTERNAL_CASE_NO']");
			CPCommon.AssertEqual(true,HPMCASE_InternalCaseNumber.Exists());

												
				CPCommon.CurrentComponent = "HPMCASE";
							CPCommon.WaitControlDisplayed(HPMCASE_MainForm);
IWebElement formBttn = HPMCASE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMCASE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMCASE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMCASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMCASE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMCASE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMCASE_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMCASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMCASE] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control HPMCASE_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMCASE_HEMPLCASEHSLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMCASE_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "HPMCASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMCASE] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control HPMCASE_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMCASE_HEMPLCASEHSLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMCASE_ChildFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HPMCASE";
							CPCommon.WaitControlDisplayed(HPMCASE_MainForm);
formBttn = HPMCASE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

