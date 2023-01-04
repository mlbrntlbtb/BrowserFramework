 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMTMSEQ_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Revenue", "xpath","//div[@class='navItem'][.='Revenue']").Click();
new Control("Manage Rate Sequence Orders", "xpath","//div[@class='navItem'][.='Manage Rate Sequence Orders']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJMTMSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTMSEQ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMTMSEQ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMTMSEQ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMTMSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTMSEQ] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMTMSEQ_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMTMSEQ_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMTMSEQ";
							CPCommon.WaitControlDisplayed(PJMTMSEQ_MainForm);
IWebElement formBttn = PJMTMSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMTMSEQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMTMSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMTMSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTMSEQ] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMTMSEQ_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMTMSEQ_MainTable.Exists());

											Driver.SessionLogger.WriteLine("Rate Sequence Details");


												
				CPCommon.CurrentComponent = "PJMTMSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTMSEQ] Perfoming VerifyExist on RateSequenceDetailsTable...", Logger.MessageType.INF);
			Control PJMTMSEQ_RateSequenceDetailsTable = new Control("RateSequenceDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMTMSEQ_TMRTORDER_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMTMSEQ_RateSequenceDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "PJMTMSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTMSEQ] Perfoming ClickButton on RateSequenceDetailsForm...", Logger.MessageType.INF);
			Control PJMTMSEQ_RateSequenceDetailsForm = new Control("RateSequenceDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMTMSEQ_TMRTORDER_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMTMSEQ_RateSequenceDetailsForm);
formBttn = PJMTMSEQ_RateSequenceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMTMSEQ_RateSequenceDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMTMSEQ_RateSequenceDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMTMSEQ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMTMSEQ] Perfoming VerifyExists on RateSequenceDetails_Sequence...", Logger.MessageType.INF);
			Control PJMTMSEQ_RateSequenceDetails_Sequence = new Control("RateSequenceDetails_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMTMSEQ_TMRTORDER_CTW_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,PJMTMSEQ_RateSequenceDetails_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PJMTMSEQ";
							CPCommon.WaitControlDisplayed(PJMTMSEQ_MainForm);
formBttn = PJMTMSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? PJMTMSEQ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
PJMTMSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJMTMSEQ";
							CPCommon.WaitControlDisplayed(PJMTMSEQ_MainForm);
formBttn = PJMTMSEQ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

