 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APREFTED_SMOKE : TestScript
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
new Control("Accounts Payable", "xpath","//div[@class='deptItem'][.='Accounts Payable']").Click();
new Control("Payment Processing", "xpath","//div[@class='navItem'][.='Payment Processing']").Click();
new Control("Print EFT Edit Report", "xpath","//div[@class='navItem'][.='Print EFT Edit Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "APREFTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APREFTED] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APREFTED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APREFTED_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APREFTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APREFTED] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control APREFTED_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,APREFTED_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "APREFTED";
							CPCommon.WaitControlDisplayed(APREFTED_MainForm);
IWebElement formBttn = APREFTED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APREFTED_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APREFTED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APREFTED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APREFTED] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APREFTED_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APREFTED_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APREFTED";
							CPCommon.WaitControlDisplayed(APREFTED_MainForm);
formBttn = APREFTED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

