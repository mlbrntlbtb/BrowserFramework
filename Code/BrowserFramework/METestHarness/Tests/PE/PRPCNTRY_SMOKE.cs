 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPCNTRY_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Year-End Processing", "xpath","//div[@class='navItem'][.='Year-End Processing']").Click();
new Control("Update Tax File Country Code", "xpath","//div[@class='navItem'][.='Update Tax File Country Code']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PRPCNTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCNTRY] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPCNTRY_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPCNTRY_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPCNTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCNTRY] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRPCNTRY_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPCNTRY_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRPCNTRY";
							CPCommon.WaitControlDisplayed(PRPCNTRY_MainForm);
IWebElement formBttn = PRPCNTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPCNTRY_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPCNTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPCNTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCNTRY] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRPCNTRY_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPCNTRY_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("User-Defined");


												
				CPCommon.CurrentComponent = "PRPCNTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCNTRY] Perfoming VerifyExists on UserDefinedMagneticMediaCodesForm...", Logger.MessageType.INF);
			Control PRPCNTRY_UserDefinedMagneticMediaCodesForm = new Control("UserDefinedMagneticMediaCodesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPCNTRY_MAGMEDIA_CNTRYDTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRPCNTRY_UserDefinedMagneticMediaCodesForm.Exists());

												
				CPCommon.CurrentComponent = "PRPCNTRY";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPCNTRY] Perfoming VerifyExist on UserDefinedMagneticMediaCodesFormTable...", Logger.MessageType.INF);
			Control PRPCNTRY_UserDefinedMagneticMediaCodesFormTable = new Control("UserDefinedMagneticMediaCodesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRPCNTRY_MAGMEDIA_CNTRYDTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPCNTRY_UserDefinedMagneticMediaCodesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRPCNTRY";
							CPCommon.WaitControlDisplayed(PRPCNTRY_MainForm);
formBttn = PRPCNTRY_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

