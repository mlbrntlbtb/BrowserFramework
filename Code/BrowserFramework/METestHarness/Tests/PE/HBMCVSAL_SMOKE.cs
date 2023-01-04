 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMCVSAL_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Benefits", "xpath","//div[@class='deptItem'][.='Benefits']").Click();
new Control("Benefit Controls", "xpath","//div[@class='navItem'][.='Benefit Controls']").Click();
new Control("Manage Coverage Amounts by Salary", "xpath","//div[@class='navItem'][.='Manage Coverage Amounts by Salary']").Click();


												
				CPCommon.CurrentComponent = "HBMCVSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVSAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMCVSAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMCVSAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMCVSAL";
							CPCommon.WaitControlDisplayed(HBMCVSAL_MainForm);
IWebElement formBttn = HBMCVSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMCVSAL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMCVSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMCVSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVSAL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMCVSAL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMCVSAL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMCVSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVSAL] Perfoming Click on CoverageAmountsBySalaryLink...", Logger.MessageType.INF);
			Control HBMCVSAL_CoverageAmountsBySalaryLink = new Control("CoverageAmountsBySalaryLink", "ID", "lnk_4022_HBMCVSAL_HBSALCVG_HDR");
			CPCommon.WaitControlDisplayed(HBMCVSAL_CoverageAmountsBySalaryLink);
HBMCVSAL_CoverageAmountsBySalaryLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMCVSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVSAL] Perfoming VerifyExists on CoverageAmountsBySalaryForm...", Logger.MessageType.INF);
			Control HBMCVSAL_CoverageAmountsBySalaryForm = new Control("CoverageAmountsBySalaryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMCVSAL_HBSALCVGSCH_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMCVSAL_CoverageAmountsBySalaryForm.Exists());

												
				CPCommon.CurrentComponent = "HBMCVSAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVSAL] Perfoming VerifyExist on CoverageAmountsBySalaryFormTable...", Logger.MessageType.INF);
			Control HBMCVSAL_CoverageAmountsBySalaryFormTable = new Control("CoverageAmountsBySalaryFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMCVSAL_HBSALCVGSCH_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMCVSAL_CoverageAmountsBySalaryFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMCVSAL";
							CPCommon.WaitControlDisplayed(HBMCVSAL_MainForm);
formBttn = HBMCVSAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

