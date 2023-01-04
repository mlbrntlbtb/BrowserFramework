 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPTOOL3_SMOKE : TestScript
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
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee Utilities", "xpath","//div[@class='navItem'][.='Employee Utilities']").Click();
new Control("Change Employee Address Line Order", "xpath","//div[@class='navItem'][.='Change Employee Address Line Order']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "LDPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPTOOL3] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPTOOL3_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPTOOL3_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPTOOL3] Perfoming VerifyExists on LineAddress_MoveLine1AddressToLine...", Logger.MessageType.INF);
			Control LDPTOOL3_LineAddress_MoveLine1AddressToLine = new Control("LineAddress_MoveLine1AddressToLine", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MOV_LINE_ADDR_1']");
			CPCommon.AssertEqual(true,LDPTOOL3_LineAddress_MoveLine1AddressToLine.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "LDPTOOL3";
							CPCommon.WaitControlDisplayed(LDPTOOL3_MainForm);
IWebElement formBttn = LDPTOOL3_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

