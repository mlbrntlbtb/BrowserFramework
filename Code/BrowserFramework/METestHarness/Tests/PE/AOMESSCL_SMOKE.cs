 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMESSCL_SMOKE : TestScript
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

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming Set on SearchApplications...", Logger.MessageType.INF);
			Control CP7Main_SearchApplications = new Control("SearchApplications", "ID", "appFltrFld");
			CP7Main_SearchApplications.Click();
CP7Main_SearchApplications.SendKeys("AOMESSCL", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CP7Main_SearchApplications.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


											CPCommon.SendKeys("Down");


											CPCommon.SendKeys("Enter");


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOMESSCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMESSCL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMESSCL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCL] Perfoming VerifyExists on TaxableEntity...", Logger.MessageType.INF);
			Control AOMESSCL_TaxableEntity = new Control("TaxableEntity", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TAXBLE_ENTITY']");
			CPCommon.AssertEqual(true,AOMESSCL_TaxableEntity.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCL";
							CPCommon.WaitControlDisplayed(AOMESSCL_MainForm);
IWebElement formBttn = AOMESSCL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMESSCL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMESSCL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMESSCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMESSCL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSCL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("COMPANY LINKS");


												
				CPCommon.CurrentComponent = "AOMESSCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCL] Perfoming VerifyExist on CompanyLinksFormTable...", Logger.MessageType.INF);
			Control AOMESSCL_CompanyLinksFormTable = new Control("CompanyLinksFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCL_ESS_COMP_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMESSCL_CompanyLinksFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMESSCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCL] Perfoming ClickButton on CompanyLinksForm...", Logger.MessageType.INF);
			Control AOMESSCL_CompanyLinksForm = new Control("CompanyLinksForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCL_ESS_COMP_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOMESSCL_CompanyLinksForm);
formBttn = AOMESSCL_CompanyLinksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMESSCL_CompanyLinksForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMESSCL_CompanyLinksForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMESSCL";
							CPCommon.AssertEqual(true,AOMESSCL_CompanyLinksForm.Exists());

													
				CPCommon.CurrentComponent = "AOMESSCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMESSCL] Perfoming VerifyExists on CompanyLinks_Order...", Logger.MessageType.INF);
			Control AOMESSCL_CompanyLinks_Order = new Control("CompanyLinks_Order", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMESSCL_ESS_COMP_CHILD_']/ancestor::form[1]/descendant::*[@id='LINK_ORDER']");
			CPCommon.AssertEqual(true,AOMESSCL_CompanyLinks_Order.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMESSCL";
							CPCommon.WaitControlDisplayed(AOMESSCL_MainForm);
formBttn = AOMESSCL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

