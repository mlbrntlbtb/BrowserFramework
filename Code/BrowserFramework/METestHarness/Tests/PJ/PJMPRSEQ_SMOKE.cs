 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPRSEQ_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost Pool Controls", "xpath","//div[@class='navItem'][.='Cost Pool Controls']").Click();
new Control("Manage Pool Processing Sequence Numbers", "xpath","//div[@class='navItem'][.='Manage Pool Processing Sequence Numbers']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage Pool Processing Sequence Numbers", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "PJMPRSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPRSEQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPRSEQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPRSEQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPRSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPRSEQ] Perfoming VerifyExists on AllocationGroupNumber...", Logger.MessageType.INF);
			Control PJMPRSEQ_AllocationGroupNumber = new Control("AllocationGroupNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALLOC_GRP_NO']");
			CPCommon.AssertEqual(true,PJMPRSEQ_AllocationGroupNumber.Exists());

												
				CPCommon.CurrentComponent = "PJMPRSEQ";
							CPCommon.WaitControlDisplayed(PJMPRSEQ_MainForm);
IWebElement formBttn = PJMPRSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMPRSEQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMPRSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMPRSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPRSEQ] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMPRSEQ_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPRSEQ_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMPRSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPRSEQ] Perfoming VerifyExist on SequenceInfoFormTable...", Logger.MessageType.INF);
			Control PJMPRSEQ_SequenceInfoFormTable = new Control("SequenceInfoFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPRSEQ_POOLALLOC_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPRSEQ_SequenceInfoFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPRSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPRSEQ] Perfoming ClickButton on SequenceInfoForm...", Logger.MessageType.INF);
			Control PJMPRSEQ_SequenceInfoForm = new Control("SequenceInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPRSEQ_POOLALLOC_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPRSEQ_SequenceInfoForm);
formBttn = PJMPRSEQ_SequenceInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPRSEQ_SequenceInfoForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPRSEQ_SequenceInfoForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPRSEQ";
							CPCommon.AssertEqual(true,PJMPRSEQ_SequenceInfoForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPRSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPRSEQ] Perfoming VerifyExists on SequenceInfo_SequenceNumber...", Logger.MessageType.INF);
			Control PJMPRSEQ_SequenceInfo_SequenceNumber = new Control("SequenceInfo_SequenceNumber", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPRSEQ_POOLALLOC_CHLD_']/ancestor::form[1]/descendant::*[@id='PROC_SEQ_NO']");
			CPCommon.AssertEqual(true,PJMPRSEQ_SequenceInfo_SequenceNumber.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMPRSEQ";
							CPCommon.WaitControlDisplayed(PJMPRSEQ_MainForm);
formBttn = PJMPRSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

