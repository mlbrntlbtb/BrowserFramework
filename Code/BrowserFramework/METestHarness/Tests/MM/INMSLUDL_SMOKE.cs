 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMSLUDL_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Controls", "xpath","//div[@class='navItem'][.='Inventory Controls']").Click();
new Control("Manage Serial/Lot User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Serial/Lot User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "INMSLUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSLUDL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMSLUDL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMSLUDL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMSLUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMSLUDL] Perfoming VerifyExists on UserDefinedFieldLabels_Label1...", Logger.MessageType.INF);
			Control INMSLUDL_UserDefinedFieldLabels_Label1 = new Control("UserDefinedFieldLabels_Label1", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UDEF_LBL_1']");
			CPCommon.AssertEqual(true,INMSLUDL_UserDefinedFieldLabels_Label1.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMSLUDL";
							CPCommon.WaitControlDisplayed(INMSLUDL_MainForm);
IWebElement formBttn = INMSLUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

