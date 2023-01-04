 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLPTOOL1_SMOKE : TestScript
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
new Control("Cash Management", "xpath","//div[@class='deptItem'][.='Cash Management']").Click();
new Control("Cash Management Utilities", "xpath","//div[@class='navItem'][.='Cash Management Utilities']").Click();
new Control("Update Beginning Bank Statement Balances", "xpath","//div[@class='navItem'][.='Update Beginning Bank Statement Balances']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "GLPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPTOOL1] Perfoming VerifyExists on Option_BankAbbreviation...", Logger.MessageType.INF);
			Control GLPTOOL1_Option_BankAbbreviation = new Control("Option_BankAbbreviation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BNK_ABREV']");
			CPCommon.AssertEqual(true,GLPTOOL1_Option_BankAbbreviation.Exists());

											Driver.SessionLogger.WriteLine("Bank Abbreviation");


												
				CPCommon.CurrentComponent = "GLPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPTOOL1] Perfoming Click on BankAbbreviationNonContiguousRangesLink...", Logger.MessageType.INF);
			Control GLPTOOL1_BankAbbreviationNonContiguousRangesLink = new Control("BankAbbreviationNonContiguousRangesLink", "ID", "lnk_3506_GLPTOOL_UPD_BNK_BEG_BAL");
			CPCommon.WaitControlDisplayed(GLPTOOL1_BankAbbreviationNonContiguousRangesLink);
GLPTOOL1_BankAbbreviationNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "GLPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPTOOL1] Perfoming VerifyExist on BankAbbreviationNonContiguousFormTable...", Logger.MessageType.INF);
			Control GLPTOOL1_BankAbbreviationNonContiguousFormTable = new Control("BankAbbreviationNonContiguousFormTable", "xpath", "//div[starts-with(@id,'pr__GLPTOOL1_BANKACCTABBR_NCR_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLPTOOL1_BankAbbreviationNonContiguousFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPTOOL1] Perfoming Close on BankAbbreviationNonContiguousForm...", Logger.MessageType.INF);
			Control GLPTOOL1_BankAbbreviationNonContiguousForm = new Control("BankAbbreviationNonContiguousForm", "xpath", "//div[starts-with(@id,'pr__GLPTOOL1_BANKACCTABBR_NCR_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLPTOOL1_BankAbbreviationNonContiguousForm);
IWebElement formBttn = GLPTOOL1_BankAbbreviationNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Close Form");


												
				CPCommon.CurrentComponent = "GLPTOOL1";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLPTOOL1] Perfoming Close on MainForm...", Logger.MessageType.INF);
			Control GLPTOOL1_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLPTOOL1_MainForm);
formBttn = GLPTOOL1_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

