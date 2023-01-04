 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMADP25_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("ADP Interface", "xpath","//div[@class='navItem'][.='ADP Interface']").Click();
new Control("Manage ADP 2.5 Mapping Values", "xpath","//div[@class='navItem'][.='Manage ADP 2.5 Mapping Values']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOMADP25";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMADP25] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMADP25_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMADP25_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMADP25";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMADP25] Perfoming VerifyExists on ADPMapCode...", Logger.MessageType.INF);
			Control AOMADP25_ADPMapCode = new Control("ADPMapCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ADP_MAP_CD']");
			CPCommon.AssertEqual(true,AOMADP25_ADPMapCode.Exists());

												
				CPCommon.CurrentComponent = "AOMADP25";
							CPCommon.WaitControlDisplayed(AOMADP25_MainForm);
IWebElement formBttn = AOMADP25_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMADP25_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMADP25_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMADP25";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMADP25] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMADP25_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMADP25_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "AOMADP25";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMADP25] Perfoming VerifyExists on TableWindowForm...", Logger.MessageType.INF);
			Control AOMADP25_TableWindowForm = new Control("TableWindowForm", "xpath", "//div[starts-with(@id,'pr__AOMADP25_XADPMAP25_CTW_')]/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMADP25_TableWindowForm.Exists());

												
				CPCommon.CurrentComponent = "AOMADP25";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMADP25] Perfoming VerifyExist on TableWindowTable...", Logger.MessageType.INF);
			Control AOMADP25_TableWindowTable = new Control("TableWindowTable", "xpath", "//div[starts-with(@id,'pr__AOMADP25_XADPMAP25_CTW_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMADP25_TableWindowTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "AOMADP25";
							CPCommon.WaitControlDisplayed(AOMADP25_MainForm);
formBttn = AOMADP25_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

