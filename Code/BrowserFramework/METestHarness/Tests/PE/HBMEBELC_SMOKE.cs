 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMEBELC_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee Benefit Information", "xpath","//div[@class='navItem'][.='Employee Benefit Information']").Click();
new Control("Manage Employee Benefit Elections", "xpath","//div[@class='navItem'][.='Manage Employee Benefit Elections']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "HBMEBELC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEBELC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMEBELC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMEBELC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEBELC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEBELC] Perfoming VerifyExists on BenefitPackage...", Logger.MessageType.INF);
			Control HBMEBELC_BenefitPackage = new Control("BenefitPackage", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='BEN_PKG_CD']");
			CPCommon.AssertEqual(true,HBMEBELC_BenefitPackage.Exists());

											Driver.SessionLogger.WriteLine("Table");


												
				CPCommon.CurrentComponent = "HBMEBELC";
							CPCommon.WaitControlDisplayed(HBMEBELC_MainForm);
IWebElement formBttn = HBMEBELC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMEBELC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMEBELC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMEBELC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEBELC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMEBELC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEBELC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Benefit Elections Detail");


												
				CPCommon.CurrentComponent = "HBMEBELC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEBELC] Perfoming VerifyExists on BenefitElectionsDetailForm...", Logger.MessageType.INF);
			Control HBMEBELC_BenefitElectionsDetailForm = new Control("BenefitElectionsDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEBELC_HBEMPLPKGELEC_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HBMEBELC_BenefitElectionsDetailForm.Exists());

												
				CPCommon.CurrentComponent = "HBMEBELC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEBELC] Perfoming VerifyExist on BenefitElectionsDetailTable...", Logger.MessageType.INF);
			Control HBMEBELC_BenefitElectionsDetailTable = new Control("BenefitElectionsDetailTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEBELC_HBEMPLPKGELEC_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMEBELC_BenefitElectionsDetailTable.Exists());

												
				CPCommon.CurrentComponent = "HBMEBELC";
							CPCommon.WaitControlDisplayed(HBMEBELC_BenefitElectionsDetailForm);
formBttn = HBMEBELC_BenefitElectionsDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMEBELC_BenefitElectionsDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMEBELC_BenefitElectionsDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HBMEBELC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMEBELC] Perfoming VerifyExists on BenefitElectionsDetails_BenefitPlan...", Logger.MessageType.INF);
			Control HBMEBELC_BenefitElectionsDetails_BenefitPlan = new Control("BenefitElectionsDetails_BenefitPlan", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMEBELC_HBEMPLPKGELEC_CTW_']/ancestor::form[1]/descendant::*[@id='BEN_PLAN_CD']");
			CPCommon.AssertEqual(true,HBMEBELC_BenefitElectionsDetails_BenefitPlan.Exists());

											Driver.SessionLogger.WriteLine("Close Application");


												
				CPCommon.CurrentComponent = "HBMEBELC";
							CPCommon.WaitControlDisplayed(HBMEBELC_MainForm);
formBttn = HBMEBELC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

