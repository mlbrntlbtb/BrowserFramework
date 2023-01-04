 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPAVCK_SMOKE : TestScript
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
new Control("Payroll Utilities", "xpath","//div[@class='navItem'][.='Payroll Utilities']").Click();
new Control("Void/Replace Posted Paychecks", "xpath","//div[@class='navItem'][.='Void/Replace Posted Paychecks']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PRPAVCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAVCK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPAVCK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPAVCK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPAVCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAVCK] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control PRPAVCK_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPAVCK_Identification_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("Table");


												
				CPCommon.CurrentComponent = "PRPAVCK";
							CPCommon.WaitControlDisplayed(PRPAVCK_MainForm);
IWebElement formBttn = PRPAVCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPAVCK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPAVCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPAVCK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPAVCK] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control PRPAVCK_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPAVCK_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "PRPAVCK";
							CPCommon.WaitControlDisplayed(PRPAVCK_MainForm);
formBttn = PRPAVCK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

