 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RCRTRVLR_SMOKE : TestScript
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
new Control("Receiving", "xpath","//div[@class='deptItem'][.='Receiving']").Click();
new Control("Receiving", "xpath","//div[@class='navItem'][.='Receiving']").Click();
new Control("Print Receipt Traveler", "xpath","//div[@class='navItem'][.='Print Receipt Traveler']").Click();


												
				CPCommon.CurrentComponent = "RCRTRVLR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCRTRVLR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RCRTRVLR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RCRTRVLR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RCRTRVLR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCRTRVLR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control RCRTRVLR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,RCRTRVLR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "RCRTRVLR";
							CPCommon.WaitControlDisplayed(RCRTRVLR_MainForm);
IWebElement formBttn = RCRTRVLR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RCRTRVLR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RCRTRVLR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RCRTRVLR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RCRTRVLR] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control RCRTRVLR_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RCRTRVLR_MainForm_Table.Exists());

												
				CPCommon.CurrentComponent = "RCRTRVLR";
							CPCommon.WaitControlDisplayed(RCRTRVLR_MainForm);
formBttn = RCRTRVLR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

