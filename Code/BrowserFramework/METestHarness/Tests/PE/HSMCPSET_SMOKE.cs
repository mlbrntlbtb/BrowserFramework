 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HSMCPSET_SMOKE : TestScript
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
new Control("Compensation", "xpath","//div[@class='deptItem'][.='Compensation']").Click();
new Control("Compensation Plans", "xpath","//div[@class='navItem'][.='Compensation Plans']").Click();
new Control("Manage Compensation Plans", "xpath","//div[@class='navItem'][.='Manage Compensation Plans']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HSMCPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HSMCPSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HSMCPSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSET] Perfoming VerifyExists on CompensationPlanCode...", Logger.MessageType.INF);
			Control HSMCPSET_CompensationPlanCode = new Control("CompensationPlanCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMP_PLAN_CD']");
			CPCommon.AssertEqual(true,HSMCPSET_CompensationPlanCode.Exists());

												
				CPCommon.CurrentComponent = "HSMCPSET";
							CPCommon.WaitControlDisplayed(HSMCPSET_MainForm);
IWebElement formBttn = HSMCPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HSMCPSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HSMCPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HSMCPSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HSMCPSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HSMCPSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HSMCPSET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "HSMCPSET";
							CPCommon.WaitControlDisplayed(HSMCPSET_MainForm);
formBttn = HSMCPSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

