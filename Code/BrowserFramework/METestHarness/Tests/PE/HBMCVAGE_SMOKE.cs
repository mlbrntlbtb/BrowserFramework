 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMCVAGE_SMOKE : TestScript
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
new Control("Manage Coverage Amounts by Age", "xpath","//div[@class='navItem'][.='Manage Coverage Amounts by Age']").Click();


												
				CPCommon.CurrentComponent = "HBMCVAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVAGE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMCVAGE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMCVAGE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMCVAGE";
							CPCommon.WaitControlDisplayed(HBMCVAGE_MainForm);
IWebElement formBttn = HBMCVAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMCVAGE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMCVAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMCVAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVAGE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMCVAGE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMCVAGE_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMCVAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVAGE] Perfoming Click on CoverageAmountsByAgeLink...", Logger.MessageType.INF);
			Control HBMCVAGE_CoverageAmountsByAgeLink = new Control("CoverageAmountsByAgeLink", "ID", "lnk_4067_HBMCVAGE_HBAGECVG_HDR");
			CPCommon.WaitControlDisplayed(HBMCVAGE_CoverageAmountsByAgeLink);
HBMCVAGE_CoverageAmountsByAgeLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMCVAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVAGE] Perfoming VerifyExists on CoverageAmountsByAgeForm...", Logger.MessageType.INF);
			Control HBMCVAGE_CoverageAmountsByAgeForm = new Control("CoverageAmountsByAgeForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMCVAGE_HBAGECVGSCH_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMCVAGE_CoverageAmountsByAgeForm.Exists());

												
				CPCommon.CurrentComponent = "HBMCVAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMCVAGE] Perfoming VerifyExist on CoverageAmountsByAgeFormTable...", Logger.MessageType.INF);
			Control HBMCVAGE_CoverageAmountsByAgeFormTable = new Control("CoverageAmountsByAgeFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMCVAGE_HBAGECVGSCH_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMCVAGE_CoverageAmountsByAgeFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMCVAGE";
							CPCommon.WaitControlDisplayed(HBMCVAGE_MainForm);
formBttn = HBMCVAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

