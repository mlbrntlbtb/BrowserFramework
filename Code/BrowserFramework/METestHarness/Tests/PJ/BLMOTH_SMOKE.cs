 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMOTH_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing Controls", "xpath","//div[@class='navItem'][.='Billing Controls']").Click();
new Control("Manage Other Charges", "xpath","//div[@class='navItem'][.='Manage Other Charges']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMOTH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOTH] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMOTH_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMOTH_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMOTH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOTH] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BLMOTH_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BLMOTH_MainForm);
IWebElement formBttn = BLMOTH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMOTH_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMOTH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMOTH";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMOTH] Perfoming VerifyExists on OtherChargeCode...", Logger.MessageType.INF);
			Control BLMOTH_OtherChargeCode = new Control("OtherChargeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OTH_CHG_CD']");
			CPCommon.AssertEqual(true,BLMOTH_OtherChargeCode.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMOTH";
							CPCommon.WaitControlDisplayed(BLMOTH_MainForm);
formBttn = BLMOTH_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

