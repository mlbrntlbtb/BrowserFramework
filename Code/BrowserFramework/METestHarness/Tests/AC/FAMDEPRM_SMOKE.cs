 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMDEPRM_SMOKE : TestScript
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
new Control("Fixed Assets Controls", "xpath","//div[@class='navItem'][.='Fixed Assets Controls']").Click();
new Control("Manage Depreciation Methods", "xpath","//div[@class='navItem'][.='Manage Depreciation Methods']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming VerifyExists on DepreciationMethodCode...", Logger.MessageType.INF);
			Control FAMDEPRM_DepreciationMethodCode = new Control("DepreciationMethodCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DEPR_MTHD_CD']");
			CPCommon.AssertEqual(true,FAMDEPRM_DepreciationMethodCode.Exists());

												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMDEPRM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMDEPRM_MainForm);
IWebElement formBttn = FAMDEPRM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? FAMDEPRM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
FAMDEPRM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMDEPRM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMDEPRM_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Auto Calc");


												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming Click on AutoCalc...", Logger.MessageType.INF);
			Control FAMDEPRM_AutoCalc = new Control("AutoCalc", "ID", "lnk_1000952_FAMDEPRM_DEPRMTHD_HDR");
			CPCommon.WaitControlDisplayed(FAMDEPRM_AutoCalc);
FAMDEPRM_AutoCalc.Click(1.5);


												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming VerifyExists on AutoCalcForm...", Logger.MessageType.INF);
			Control FAMDEPRM_AutoCalcForm = new Control("AutoCalcForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDEPRM_AUTOCALC_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMDEPRM_AutoCalcForm.Exists());

												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming VerifyExists on AutoCalc_DepreciationMethodOptions_StraightLine...", Logger.MessageType.INF);
			Control FAMDEPRM_AutoCalc_DepreciationMethodOptions_StraightLine = new Control("AutoCalc_DepreciationMethodOptions_StraightLine", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDEPRM_AUTOCALC_']/ancestor::form[1]/descendant::*[@id='DEPR_METHOD_OPT' and @value='SL']");
			CPCommon.AssertEqual(true,FAMDEPRM_AutoCalc_DepreciationMethodOptions_StraightLine.Exists());

												
				CPCommon.CurrentComponent = "FAMDEPRM";
							CPCommon.WaitControlDisplayed(FAMDEPRM_AutoCalcForm);
formBttn = FAMDEPRM_AutoCalcForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "FAMDEPRM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMDEPRM] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control FAMDEPRM_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAMDEPRM_DEPRMTHDYRS_DEPR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAMDEPRM_ChildForm.Exists());

											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "FAMDEPRM";
							CPCommon.WaitControlDisplayed(FAMDEPRM_MainForm);
formBttn = FAMDEPRM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

