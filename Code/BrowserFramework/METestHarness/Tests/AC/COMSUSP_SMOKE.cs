 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COMSUSP_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Consolidations", "xpath","//div[@class='deptItem'][.='Consolidations']").Click();
new Control("Consolidations Processing", "xpath","//div[@class='navItem'][.='Consolidations Processing']").Click();
new Control("Manage Consolidation Suspense Entries", "xpath","//div[@class='navItem'][.='Manage Consolidation Suspense Entries']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "COMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSUSP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control COMSUSP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,COMSUSP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "COMSUSP";
							CPCommon.WaitControlDisplayed(COMSUSP_MainForm);
IWebElement formBttn = COMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? COMSUSP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
COMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "COMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSUSP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control COMSUSP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMSUSP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMSUSP";
							CPCommon.WaitControlDisplayed(COMSUSP_MainForm);
formBttn = COMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? COMSUSP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
COMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "COMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSUSP] Perfoming VerifyExists on ConsolidationNumberDescription...", Logger.MessageType.INF);
			Control COMSUSP_ConsolidationNumberDescription = new Control("ConsolidationNumberDescription", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CONS_DESC']");
			CPCommon.AssertEqual(true,COMSUSP_ConsolidationNumberDescription.Exists());

												
				CPCommon.CurrentComponent = "COMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSUSP] Perfoming VerifyExists on ConsolidationEntriesLink...", Logger.MessageType.INF);
			Control COMSUSP_ConsolidationEntriesLink = new Control("ConsolidationEntriesLink", "ID", "lnk_3741_COMSUSP_CONSHDRSUSP_HDR");
			CPCommon.AssertEqual(true,COMSUSP_ConsolidationEntriesLink.Exists());

												
				CPCommon.CurrentComponent = "COMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSUSP] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control COMSUSP_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__COMSUSP_CONSHDRSUSP_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COMSUSP_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "COMSUSP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMSUSP] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control COMSUSP_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__COMSUSP_CONSHDRSUSP_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMSUSP_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMSUSP";
							CPCommon.WaitControlDisplayed(COMSUSP_MainForm);
formBttn = COMSUSP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

