 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMWARR_SMOKE : TestScript
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
new Control("Materials", "xpath","//div[@class='busItem'][.='Materials']").Click();
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Product Billing", "xpath","//div[@class='navItem'][.='Product Billing']").Click();
new Control("Manage Warranty Information", "xpath","//div[@class='navItem'][.='Manage Warranty Information']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMWARR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMWARR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMWARR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMWARR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMWARR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMWARR] Perfoming VerifyExists on WarrantyCode...", Logger.MessageType.INF);
			Control PDMWARR_WarrantyCode = new Control("WarrantyCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WARR_CD']");
			CPCommon.AssertEqual(true,PDMWARR_WarrantyCode.Exists());

												
				CPCommon.CurrentComponent = "PDMWARR";
							CPCommon.WaitControlDisplayed(PDMWARR_MainForm);
IWebElement formBttn = PDMWARR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMWARR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMWARR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMWARR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMWARR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMWARR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMWARR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DETAILSFORM");


												
				CPCommon.CurrentComponent = "PDMWARR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMWARR] Perfoming VerifyExist on WarrantyTermsFormTable...", Logger.MessageType.INF);
			Control PDMWARR_WarrantyTermsFormTable = new Control("WarrantyTermsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMWARR_WARRANTYTERMS_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMWARR_WarrantyTermsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMWARR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMWARR] Perfoming ClickButton on WarrantyTermsForm...", Logger.MessageType.INF);
			Control PDMWARR_WarrantyTermsForm = new Control("WarrantyTermsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMWARR_WARRANTYTERMS_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMWARR_WarrantyTermsForm);
formBttn = PDMWARR_WarrantyTermsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMWARR_WarrantyTermsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMWARR_WarrantyTermsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMWARR";
							CPCommon.AssertEqual(true,PDMWARR_WarrantyTermsForm.Exists());

													
				CPCommon.CurrentComponent = "PDMWARR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMWARR] Perfoming VerifyExists on WarrantyTerms_Line...", Logger.MessageType.INF);
			Control PDMWARR_WarrantyTerms_Line = new Control("WarrantyTerms_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMWARR_WARRANTYTERMS_DTL_']/ancestor::form[1]/descendant::*[@id='LN_NO']");
			CPCommon.AssertEqual(true,PDMWARR_WarrantyTerms_Line.Exists());

												
				CPCommon.CurrentComponent = "PDMWARR";
							CPCommon.WaitControlDisplayed(PDMWARR_MainForm);
formBttn = PDMWARR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

