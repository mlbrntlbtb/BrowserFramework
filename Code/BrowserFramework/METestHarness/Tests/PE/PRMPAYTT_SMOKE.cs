 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMPAYTT_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Payroll Controls", "xpath","//div[@class='navItem'][.='Payroll Controls']").Click();
new Control("Manage Pay Type Taxability", "xpath","//div[@class='navItem'][.='Manage Pay Type Taxability']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMPAYTT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMPAYTT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExists on PayTypeCode...", Logger.MessageType.INF);
			Control PRMPAYTT_PayTypeCode = new Control("PayTypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PAY_TYPE']");
			CPCommon.AssertEqual(true,PRMPAYTT_PayTypeCode.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
							CPCommon.WaitControlDisplayed(PRMPAYTT_MainForm);
IWebElement formBttn = PRMPAYTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMPAYTT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMPAYTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PRMPAYTT_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPAYTT_MainTable.Exists());

											Driver.SessionLogger.WriteLine("State Form");


												
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExists on StateLink...", Logger.MessageType.INF);
			Control PRMPAYTT_StateLink = new Control("StateLink", "ID", "lnk_1001266_PRMPAYTT_PAYTYPE_HDR");
			CPCommon.AssertEqual(true,PRMPAYTT_StateLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
							CPCommon.WaitControlDisplayed(PRMPAYTT_StateLink);
PRMPAYTT_StateLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExists on StateForm...", Logger.MessageType.INF);
			Control PRMPAYTT_StateForm = new Control("StateForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPAYTT_STATE_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPAYTT_StateForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExist on StateTable...", Logger.MessageType.INF);
			Control PRMPAYTT_StateTable = new Control("StateTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPAYTT_STATE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPAYTT_StateTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
							CPCommon.WaitControlDisplayed(PRMPAYTT_StateForm);
formBttn = PRMPAYTT_StateForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Local Form");


												
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExists on LocalLink...", Logger.MessageType.INF);
			Control PRMPAYTT_LocalLink = new Control("LocalLink", "ID", "lnk_1002291_PRMPAYTT_PAYTYPE_HDR");
			CPCommon.AssertEqual(true,PRMPAYTT_LocalLink.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
							CPCommon.WaitControlDisplayed(PRMPAYTT_LocalLink);
PRMPAYTT_LocalLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExists on LocalForm...", Logger.MessageType.INF);
			Control PRMPAYTT_LocalForm = new Control("LocalForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPAYTT_LOCAL_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMPAYTT_LocalForm.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMPAYTT] Perfoming VerifyExist on LocalTable...", Logger.MessageType.INF);
			Control PRMPAYTT_LocalTable = new Control("LocalTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMPAYTT_LOCAL_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMPAYTT_LocalTable.Exists());

												
				CPCommon.CurrentComponent = "PRMPAYTT";
							CPCommon.WaitControlDisplayed(PRMPAYTT_LocalForm);
formBttn = PRMPAYTT_LocalForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMPAYTT";
							CPCommon.WaitControlDisplayed(PRMPAYTT_MainForm);
formBttn = PRMPAYTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

