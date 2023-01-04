 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMPOPT_SMOKE : TestScript
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
new Control("Manage Product Options", "xpath","//div[@class='navItem'][.='Manage Product Options']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMPOPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPOPT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMPOPT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMPOPT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMPOPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPOPT] Perfoming VerifyExists on ProductOptionID...", Logger.MessageType.INF);
			Control PDMPOPT_ProductOptionID = new Control("ProductOptionID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROD_OPTION_ID']");
			CPCommon.AssertEqual(true,PDMPOPT_ProductOptionID.Exists());

												
				CPCommon.CurrentComponent = "PDMPOPT";
							CPCommon.WaitControlDisplayed(PDMPOPT_MainForm);
IWebElement formBttn = PDMPOPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMPOPT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMPOPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMPOPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPOPT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMPOPT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPOPT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DETAILSFORM");


												
				CPCommon.CurrentComponent = "PDMPOPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPOPT] Perfoming VerifyExist on DetailsFormTable...", Logger.MessageType.INF);
			Control PDMPOPT_DetailsFormTable = new Control("DetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPOPT_PRODOPTIONITEM_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPOPT_DetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPOPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPOPT] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control PDMPOPT_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPOPT_PRODOPTIONITEM_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPOPT_DetailsForm);
formBttn = PDMPOPT_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPOPT_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPOPT_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPOPT";
							CPCommon.AssertEqual(true,PDMPOPT_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPOPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPOPT] Perfoming VerifyExists on Details_Item...", Logger.MessageType.INF);
			Control PDMPOPT_Details_Item = new Control("Details_Item", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPOPT_PRODOPTIONITEM_DTL_']/ancestor::form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDMPOPT_Details_Item.Exists());

												
				CPCommon.CurrentComponent = "PDMPOPT";
							CPCommon.WaitControlDisplayed(PDMPOPT_MainForm);
formBttn = PDMPOPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

