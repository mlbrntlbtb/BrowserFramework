 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMPASAL_SMOKE : TestScript
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
new Control("Manage Premiums by Salary", "xpath","//div[@class='navItem'][.='Manage Premiums by Salary']").Click();


												
				CPCommon.CurrentComponent = "HBMPASAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPASAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMPASAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMPASAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPASAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPASAL] Perfoming VerifyExists on RateTableCode...", Logger.MessageType.INF);
			Control HBMPASAL_RateTableCode = new Control("RateTableCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SAL_RT_CD']");
			CPCommon.AssertEqual(true,HBMPASAL_RateTableCode.Exists());

												
				CPCommon.CurrentComponent = "HBMPASAL";
							CPCommon.WaitControlDisplayed(HBMPASAL_MainForm);
IWebElement formBttn = HBMPASAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMPASAL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMPASAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMPASAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPASAL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMPASAL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPASAL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPASAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPASAL] Perfoming Click on PremiumAmountsBySalaryLink...", Logger.MessageType.INF);
			Control HBMPASAL_PremiumAmountsBySalaryLink = new Control("PremiumAmountsBySalaryLink", "ID", "lnk_4082_HBMPASAL_HBSALRT_HDR");
			CPCommon.WaitControlDisplayed(HBMPASAL_PremiumAmountsBySalaryLink);
HBMPASAL_PremiumAmountsBySalaryLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMPASAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPASAL] Perfoming VerifyExists on PremiumAmountsBySalaryForm...", Logger.MessageType.INF);
			Control HBMPASAL_PremiumAmountsBySalaryForm = new Control("PremiumAmountsBySalaryForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPASAL_HBSALRTSCH_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMPASAL_PremiumAmountsBySalaryForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPASAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPASAL] Perfoming VerifyExist on PremiumAmountsBySalaryFormTable...", Logger.MessageType.INF);
			Control HBMPASAL_PremiumAmountsBySalaryFormTable = new Control("PremiumAmountsBySalaryFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPASAL_HBSALRTSCH_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPASAL_PremiumAmountsBySalaryFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPASAL";
							CPCommon.WaitControlDisplayed(HBMPASAL_MainForm);
formBttn = HBMPASAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

