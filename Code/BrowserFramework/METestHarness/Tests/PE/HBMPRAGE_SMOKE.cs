 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMPRAGE_SMOKE : TestScript
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
new Control("Manage Premium Rates by Age", "xpath","//div[@class='navItem'][.='Manage Premium Rates by Age']").Click();


												
				CPCommon.CurrentComponent = "HBMPRAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPRAGE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMPRAGE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMPRAGE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPRAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPRAGE] Perfoming VerifyExists on RateTableCode...", Logger.MessageType.INF);
			Control HBMPRAGE_RateTableCode = new Control("RateTableCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='AGE_RT_TBL_CD']");
			CPCommon.AssertEqual(true,HBMPRAGE_RateTableCode.Exists());

												
				CPCommon.CurrentComponent = "HBMPRAGE";
							CPCommon.WaitControlDisplayed(HBMPRAGE_MainForm);
IWebElement formBttn = HBMPRAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMPRAGE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMPRAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMPRAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPRAGE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMPRAGE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPRAGE_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPRAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPRAGE] Perfoming Click on BenefitPremiumsByAgeDetailLink...", Logger.MessageType.INF);
			Control HBMPRAGE_BenefitPremiumsByAgeDetailLink = new Control("BenefitPremiumsByAgeDetailLink", "ID", "lnk_4092_HBMPRAGE_HBAGERT_HDR");
			CPCommon.WaitControlDisplayed(HBMPRAGE_BenefitPremiumsByAgeDetailLink);
HBMPRAGE_BenefitPremiumsByAgeDetailLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMPRAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPRAGE] Perfoming VerifyExists on BenefitPremiumsByAgeDetailForm...", Logger.MessageType.INF);
			Control HBMPRAGE_BenefitPremiumsByAgeDetailForm = new Control("BenefitPremiumsByAgeDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPRAGE_HBAGERTSCH_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMPRAGE_BenefitPremiumsByAgeDetailForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPRAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPRAGE] Perfoming VerifyExist on BenefitPremiumsByAgeDetailFormTable...", Logger.MessageType.INF);
			Control HBMPRAGE_BenefitPremiumsByAgeDetailFormTable = new Control("BenefitPremiumsByAgeDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPRAGE_HBAGERTSCH_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPRAGE_BenefitPremiumsByAgeDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPRAGE";
							CPCommon.WaitControlDisplayed(HBMPRAGE_MainForm);
formBttn = HBMPRAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

