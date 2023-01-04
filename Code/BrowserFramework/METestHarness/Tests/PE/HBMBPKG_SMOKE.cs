 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMBPKG_SMOKE : TestScript
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
new Control("Benefit Entry and Creation", "xpath","//div[@class='navItem'][.='Benefit Entry and Creation']").Click();
new Control("Manage Benefit Packages", "xpath","//div[@class='navItem'][.='Manage Benefit Packages']").Click();


												
				CPCommon.CurrentComponent = "HBMBPKG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPKG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMBPKG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMBPKG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPKG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPKG] Perfoming VerifyExists on BenefitPackageCode...", Logger.MessageType.INF);
			Control HBMBPKG_BenefitPackageCode = new Control("BenefitPackageCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BEN_PKG_CD']");
			CPCommon.AssertEqual(true,HBMBPKG_BenefitPackageCode.Exists());

												
				CPCommon.CurrentComponent = "HBMBPKG";
							CPCommon.WaitControlDisplayed(HBMBPKG_MainForm);
IWebElement formBttn = HBMBPKG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMBPKG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMBPKG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMBPKG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPKG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMBPKG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPKG_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPKG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPKG] Perfoming VerifyExists on BenefitPackageDetailsForm...", Logger.MessageType.INF);
			Control HBMBPKG_BenefitPackageDetailsForm = new Control("BenefitPackageDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPKG_HBBENPKGLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMBPKG_BenefitPackageDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "HBMBPKG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMBPKG] Perfoming VerifyExist on BenefitPackageDetailsFormTable...", Logger.MessageType.INF);
			Control HBMBPKG_BenefitPackageDetailsFormTable = new Control("BenefitPackageDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMBPKG_HBBENPKGLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMBPKG_BenefitPackageDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMBPKG";
							CPCommon.WaitControlDisplayed(HBMBPKG_MainForm);
formBttn = HBMBPKG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

