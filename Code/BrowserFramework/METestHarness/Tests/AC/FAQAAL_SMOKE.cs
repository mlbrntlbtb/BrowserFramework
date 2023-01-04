 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAQAAL_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("View Asset Change History", "xpath","//div[@class='navItem'][.='View Asset Change History']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAQAAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQAAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAQAAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAQAAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAQAAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQAAL] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAQAAL_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAQAAL_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAQAAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQAAL] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control FAQAAL_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQAAL_ASSETAUDITLOG_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAQAAL_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "FAQAAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQAAL] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control FAQAAL_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQAAL_ASSETAUDITLOG_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAQAAL_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAQAAL";
							CPCommon.AssertEqual(true,FAQAAL_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "FAQAAL";
							CPCommon.WaitControlDisplayed(FAQAAL_ChildForm);
IWebElement formBttn = FAQAAL_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAQAAL_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAQAAL_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAQAAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQAAL] Perfoming VerifyExists on ChildForm_AssetNo...", Logger.MessageType.INF);
			Control FAQAAL_ChildForm_AssetNo = new Control("ChildForm_AssetNo", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQAAL_ASSETAUDITLOG_DTL_']/ancestor::form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAQAAL_ChildForm_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAQAAL";
							CPCommon.WaitControlDisplayed(FAQAAL_MainForm);
formBttn = FAQAAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

