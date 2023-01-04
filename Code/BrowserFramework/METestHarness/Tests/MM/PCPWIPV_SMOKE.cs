 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCPWIPV_SMOKE : TestScript
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
new Control("Manufacturing Orders", "xpath","//div[@class='navItem'][.='Manufacturing Orders']").Click();
new Control("Create MO WIP Variance Journal Entry", "xpath","//div[@class='navItem'][.='Create MO WIP Variance Journal Entry']").Click();


												
				CPCommon.CurrentComponent = "PCPWIPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPWIPV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCPWIPV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCPWIPV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCPWIPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPWIPV] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PCPWIPV_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PCPWIPV_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PCPWIPV";
							CPCommon.WaitControlDisplayed(PCPWIPV_MainForm);
IWebElement formBttn = PCPWIPV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCPWIPV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCPWIPV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCPWIPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPWIPV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCPWIPV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCPWIPV_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PREVIOUSWIPVARIANCEJOURNALENTRIES");


												
				CPCommon.CurrentComponent = "PCPWIPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPWIPV] Perfoming Click on PreviousWIPVarianceLink...", Logger.MessageType.INF);
			Control PCPWIPV_PreviousWIPVarianceLink = new Control("PreviousWIPVarianceLink", "ID", "lnk_3883_PCPWIPV_PARAM");
			CPCommon.WaitControlDisplayed(PCPWIPV_PreviousWIPVarianceLink);
PCPWIPV_PreviousWIPVarianceLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PCPWIPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPWIPV] Perfoming VerifyExist on PreviousWIPVarianceJournalEntriesTable...", Logger.MessageType.INF);
			Control PCPWIPV_PreviousWIPVarianceJournalEntriesTable = new Control("PreviousWIPVarianceJournalEntriesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCPWIPV_WIPVARJNLLOG_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCPWIPV_PreviousWIPVarianceJournalEntriesTable.Exists());

												
				CPCommon.CurrentComponent = "PCPWIPV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPWIPV] Perfoming Close on PreviousWIPVarianceJournalEntriesForm...", Logger.MessageType.INF);
			Control PCPWIPV_PreviousWIPVarianceJournalEntriesForm = new Control("PreviousWIPVarianceJournalEntriesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCPWIPV_WIPVARJNLLOG_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PCPWIPV_PreviousWIPVarianceJournalEntriesForm);
formBttn = PCPWIPV_PreviousWIPVarianceJournalEntriesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "PCPWIPV";
							CPCommon.WaitControlDisplayed(PCPWIPV_MainForm);
formBttn = PCPWIPV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

