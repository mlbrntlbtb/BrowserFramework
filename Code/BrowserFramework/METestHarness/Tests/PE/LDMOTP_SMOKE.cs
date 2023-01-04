 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMOTP_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Overtime Controls", "xpath","//div[@class='navItem'][.='Overtime Controls']").Click();
new Control("Manage Overtime Premium Recast Charge Codes", "xpath","//div[@class='navItem'][.='Manage Overtime Premium Recast Charge Codes']").Click();


												
				CPCommon.CurrentComponent = "LDMOTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMOTP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMOTP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMOTP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMOTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMOTP] Perfoming VerifyExists on ProjectAccountGroup...", Logger.MessageType.INF);
			Control LDMOTP_ProjectAccountGroup = new Control("ProjectAccountGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ACCT_GRP_CD']");
			CPCommon.AssertEqual(true,LDMOTP_ProjectAccountGroup.Exists());

												
				CPCommon.CurrentComponent = "LDMOTP";
							CPCommon.WaitControlDisplayed(LDMOTP_MainForm);
IWebElement formBttn = LDMOTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMOTP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMOTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMOTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMOTP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMOTP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMOTP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMOTP";
							CPCommon.WaitControlDisplayed(LDMOTP_MainForm);
formBttn = LDMOTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

