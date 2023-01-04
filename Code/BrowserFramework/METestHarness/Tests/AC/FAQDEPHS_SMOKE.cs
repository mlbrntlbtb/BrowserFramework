 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAQDEPHS_SMOKE : TestScript
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
new Control("Fixed Assets Processing", "xpath","//div[@class='navItem'][.='Fixed Assets Processing']").Click();
new Control("View Other Books Depreciation History", "xpath","//div[@class='navItem'][.='View Other Books Depreciation History']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAQDEPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQDEPHS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAQDEPHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAQDEPHS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAQDEPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQDEPHS] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAQDEPHS_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_NO']");
			CPCommon.AssertEqual(true,FAQDEPHS_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAQDEPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQDEPHS] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control FAQDEPHS_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQDEPHS_FADEPRHS_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAQDEPHS_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "FAQDEPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQDEPHS] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control FAQDEPHS_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQDEPHS_FADEPRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAQDEPHS_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAQDEPHS";
							CPCommon.AssertEqual(true,FAQDEPHS_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "FAQDEPHS";
							CPCommon.WaitControlDisplayed(FAQDEPHS_ChildForm);
IWebElement formBttn = FAQDEPHS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAQDEPHS_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAQDEPHS_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAQDEPHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQDEPHS] Perfoming VerifyExists on ChildForm_AssetNo...", Logger.MessageType.INF);
			Control FAQDEPHS_ChildForm_AssetNo = new Control("ChildForm_AssetNo", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQDEPHS_FADEPRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAQDEPHS_ChildForm_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAQDEPHS";
							CPCommon.WaitControlDisplayed(FAQDEPHS_MainForm);
formBttn = FAQDEPHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

