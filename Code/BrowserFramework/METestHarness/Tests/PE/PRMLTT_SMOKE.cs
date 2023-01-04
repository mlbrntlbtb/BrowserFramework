 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMLTT_SMOKE : TestScript
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
new Control("Local Taxes", "xpath","//div[@class='navItem'][.='Local Taxes']").Click();
new Control("Manage Local Tax Tables", "xpath","//div[@class='navItem'][.='Manage Local Tax Tables']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PRMLTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMLTT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMLTT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTT] Perfoming VerifyExists on Locality...", Logger.MessageType.INF);
			Control PRMLTT_Locality = new Control("Locality", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LOCAL_CD']");
			CPCommon.AssertEqual(true,PRMLTT_Locality.Exists());

												
				CPCommon.CurrentComponent = "PRMLTT";
							CPCommon.WaitControlDisplayed(PRMLTT_MainForm);
IWebElement formBttn = PRMLTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMLTT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMLTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMLTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMLTT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMLTT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Local Tax Table Form");


												
				CPCommon.CurrentComponent = "PRMLTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTT] Perfoming VerifyExists on LocalTaxTableLink...", Logger.MessageType.INF);
			Control PRMLTT_LocalTaxTableLink = new Control("LocalTaxTableLink", "ID", "lnk_3961_PRMLTT_LOCALTAXTBL_HDR");
			CPCommon.AssertEqual(true,PRMLTT_LocalTaxTableLink.Exists());

												
				CPCommon.CurrentComponent = "PRMLTT";
							CPCommon.WaitControlDisplayed(PRMLTT_LocalTaxTableLink);
PRMLTT_LocalTaxTableLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMLTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTT] Perfoming VerifyExists on LocalTaxTableForm...", Logger.MessageType.INF);
			Control PRMLTT_LocalTaxTableForm = new Control("LocalTaxTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTT_LOCALTAXTBL_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMLTT_LocalTaxTableForm.Exists());

												
				CPCommon.CurrentComponent = "PRMLTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMLTT] Perfoming VerifyExist on LocalTaxTableFormTable...", Logger.MessageType.INF);
			Control PRMLTT_LocalTaxTableFormTable = new Control("LocalTaxTableFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMLTT_LOCALTAXTBL_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMLTT_LocalTaxTableFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMLTT";
							CPCommon.WaitControlDisplayed(PRMLTT_LocalTaxTableForm);
formBttn = PRMLTT_LocalTaxTableForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PRMLTT";
							CPCommon.WaitControlDisplayed(PRMLTT_MainForm);
formBttn = PRMLTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

