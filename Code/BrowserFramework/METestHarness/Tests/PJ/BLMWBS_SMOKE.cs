 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMWBS_SMOKE : TestScript
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
new Control("Configure iRAPT Billing Settings", "xpath","//div[@class='navItem'][.='Configure iRAPT Billing Settings']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMWBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWBS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMWBS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMWBS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMWBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWBS] Perfoming VerifyExists on SenderInformation_CAGECode...", Logger.MessageType.INF);
			Control BLMWBS_SenderInformation_CAGECode = new Control("SenderInformation_CAGECode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WAWF_SEND_CAGE_CD']");
			CPCommon.AssertEqual(true,BLMWBS_SenderInformation_CAGECode.Exists());

												
				CPCommon.CurrentComponent = "BLMWBS";
							CPCommon.WaitControlDisplayed(BLMWBS_MainForm);
IWebElement formBttn = BLMWBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMWBS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMWBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMWBS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWBS] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BLMWBS_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMWBS_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMWBS";
							CPCommon.WaitControlDisplayed(BLMWBS_MainForm);
formBttn = BLMWBS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

