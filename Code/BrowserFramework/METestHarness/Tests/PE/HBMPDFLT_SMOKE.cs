 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMPDFLT_SMOKE : TestScript
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
new Control("Manage Benefit Package Defaults", "xpath","//div[@class='navItem'][.='Manage Benefit Package Defaults']").Click();


												
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMPDFLT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMPDFLT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming VerifyExists on TaxableEntity...", Logger.MessageType.INF);
			Control HBMPDFLT_TaxableEntity = new Control("TaxableEntity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY_ID']");
			CPCommon.AssertEqual(true,HBMPDFLT_TaxableEntity.Exists());

												
				CPCommon.CurrentComponent = "HBMPDFLT";
							CPCommon.WaitControlDisplayed(HBMPDFLT_MainForm);
IWebElement formBttn = HBMPDFLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMPDFLT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMPDFLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMPDFLT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPDFLT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming Click on BenefitPackageDefaultDetailsLink...", Logger.MessageType.INF);
			Control HBMPDFLT_BenefitPackageDefaultDetailsLink = new Control("BenefitPackageDefaultDetailsLink", "ID", "lnk_4064_HBMPDFLT_HBBENPKGDFLT_HDR");
			CPCommon.WaitControlDisplayed(HBMPDFLT_BenefitPackageDefaultDetailsLink);
HBMPDFLT_BenefitPackageDefaultDetailsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming VerifyExists on BenefitPackageDefaultDetailsForm...", Logger.MessageType.INF);
			Control HBMPDFLT_BenefitPackageDefaultDetailsForm = new Control("BenefitPackageDefaultDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPDFLT_HBBENPKGDFLT_DETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMPDFLT_BenefitPackageDefaultDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming VerifyExist on BenefitPackageDefaultDetailsFormTable...", Logger.MessageType.INF);
			Control HBMPDFLT_BenefitPackageDefaultDetailsFormTable = new Control("BenefitPackageDefaultDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPDFLT_HBBENPKGDFLT_DETAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMPDFLT_BenefitPackageDefaultDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMPDFLT";
							CPCommon.WaitControlDisplayed(HBMPDFLT_BenefitPackageDefaultDetailsForm);
formBttn = HBMPDFLT_BenefitPackageDefaultDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMPDFLT_BenefitPackageDefaultDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMPDFLT_BenefitPackageDefaultDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMPDFLT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMPDFLT] Perfoming VerifyExists on BenefitPackageDefaultDetails_DefaultBenefitPackage...", Logger.MessageType.INF);
			Control HBMPDFLT_BenefitPackageDefaultDetails_DefaultBenefitPackage = new Control("BenefitPackageDefaultDetails_DefaultBenefitPackage", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMPDFLT_HBBENPKGDFLT_DETAIL_']/ancestor::form[1]/descendant::*[@id='BEN_PKG_CD']");
			CPCommon.AssertEqual(true,HBMPDFLT_BenefitPackageDefaultDetails_DefaultBenefitPackage.Exists());

												
				CPCommon.CurrentComponent = "HBMPDFLT";
							CPCommon.WaitControlDisplayed(HBMPDFLT_MainForm);
formBttn = HBMPDFLT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

