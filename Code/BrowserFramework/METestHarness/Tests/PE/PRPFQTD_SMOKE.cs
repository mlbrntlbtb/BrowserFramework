 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRPFQTD_SMOKE : TestScript
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
new Control("Tax Reporting", "xpath","//div[@class='navItem'][.='Tax Reporting']").Click();
new Control("Create Quarterly EFTPS FUTA Tax File", "xpath","//div[@class='navItem'][.='Create Quarterly EFTPS FUTA Tax File']").Click();


											Driver.SessionLogger.WriteLine("Checking the App");


												
				CPCommon.CurrentComponent = "PRPFQTD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPFQTD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRPFQTD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRPFQTD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRPFQTD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPFQTD] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PRPFQTD_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PRPFQTD_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PRPFQTD";
							CPCommon.WaitControlDisplayed(PRPFQTD_MainForm);
IWebElement formBttn = PRPFQTD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRPFQTD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRPFQTD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRPFQTD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRPFQTD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRPFQTD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRPFQTD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Closing the App");


												
				CPCommon.CurrentComponent = "PRPFQTD";
							CPCommon.WaitControlDisplayed(PRPFQTD_MainForm);
formBttn = PRPFQTD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

