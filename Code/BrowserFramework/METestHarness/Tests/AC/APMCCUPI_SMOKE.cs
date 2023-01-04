 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class APMCCUPI_SMOKE : TestScript
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
new Control("Accounts Payable Controls", "xpath","//div[@class='navItem'][.='Accounts Payable Controls']").Click();
new Control("Manage Credit Card Import Information", "xpath","//div[@class='navItem'][.='Manage Credit Card Import Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "APMCCUPI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCCUPI] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control APMCCUPI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,APMCCUPI_MainForm.Exists());

												
				CPCommon.CurrentComponent = "APMCCUPI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCCUPI] Perfoming VerifyExists on CreditCardType...", Logger.MessageType.INF);
			Control APMCCUPI_CreditCardType = new Control("CreditCardType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CR_CARD_TYPE']");
			CPCommon.AssertEqual(true,APMCCUPI_CreditCardType.Exists());

												
				CPCommon.CurrentComponent = "APMCCUPI";
							CPCommon.WaitControlDisplayed(APMCCUPI_MainForm);
IWebElement formBttn = APMCCUPI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? APMCCUPI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
APMCCUPI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "APMCCUPI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[APMCCUPI] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control APMCCUPI_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,APMCCUPI_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "APMCCUPI";
							CPCommon.WaitControlDisplayed(APMCCUPI_MainForm);
formBttn = APMCCUPI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

