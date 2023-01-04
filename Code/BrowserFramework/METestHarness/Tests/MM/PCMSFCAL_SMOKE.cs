 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMSFCAL_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Configure Shop Floor Calendar", "xpath","//div[@class='navItem'][.='Configure Shop Floor Calendar']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PCMSFCAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSFCAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMSFCAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMSFCAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMSFCAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSFCAL] Perfoming VerifyExists on StartingDate...", Logger.MessageType.INF);
			Control PCMSFCAL_StartingDate = new Control("StartingDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='START_DATE']");
			CPCommon.AssertEqual(true,PCMSFCAL_StartingDate.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PCMSFCAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMSFCAL] Perfoming VerifyExist on ShopCalendarFormTable...", Logger.MessageType.INF);
			Control PCMSFCAL_ShopCalendarFormTable = new Control("ShopCalendarFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMSFCAL_SHOPCALENDAR_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMSFCAL_ShopCalendarFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PCMSFCAL";
							CPCommon.WaitControlDisplayed(PCMSFCAL_MainForm);
IWebElement formBttn = PCMSFCAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

