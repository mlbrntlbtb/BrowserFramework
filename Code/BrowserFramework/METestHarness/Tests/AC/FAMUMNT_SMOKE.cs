 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMUMNT_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Maintenance Records", "xpath","//div[@class='navItem'][.='Maintenance Records']").Click();
new Control("Manage Asset Maintenance Transactions", "xpath","//div[@class='navItem'][.='Manage Asset Maintenance Transactions']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "FAMUMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUMNT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMUMNT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMUMNT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMUMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUMNT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control FAMUMNT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(FAMUMNT_MainForm);
IWebElement formBttn = FAMUMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMUMNT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMUMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "FAMUMNT";
							CPCommon.AssertEqual(true,FAMUMNT_MainForm.Exists());

													
				CPCommon.CurrentComponent = "FAMUMNT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMUMNT] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMUMNT_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMUMNT_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMUMNT";
							CPCommon.WaitControlDisplayed(FAMUMNT_MainForm);
formBttn = FAMUMNT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

