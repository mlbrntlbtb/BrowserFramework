 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMAWD_SMOKE : TestScript
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
new Control("Personnel", "xpath","//div[@class='deptItem'][.='Personnel']").Click();
new Control("Personnel Controls", "xpath","//div[@class='navItem'][.='Personnel Controls']").Click();
new Control("Manage Award Codes", "xpath","//div[@class='navItem'][.='Manage Award Codes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "HPMAWD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAWD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMAWD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMAWD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMAWD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAWD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMAWD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMAWD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMAWD";
							CPCommon.WaitControlDisplayed(HPMAWD_MainForm);
IWebElement formBttn = HPMAWD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMAWD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMAWD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMAWD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMAWD] Perfoming VerifyExists on AwardCode...", Logger.MessageType.INF);
			Control HPMAWD_AwardCode = new Control("AwardCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='AWARD_CD']");
			CPCommon.AssertEqual(true,HPMAWD_AwardCode.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "HPMAWD";
							CPCommon.WaitControlDisplayed(HPMAWD_MainForm);
formBttn = HPMAWD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

