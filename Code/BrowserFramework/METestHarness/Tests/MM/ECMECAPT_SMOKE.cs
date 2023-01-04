 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ECMECAPT_SMOKE : TestScript
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
new Control("Engineering Change Notices", "xpath","//div[@class='deptItem'][.='Engineering Change Notices']").Click();
new Control("Engineering Change Controls", "xpath","//div[@class='navItem'][.='Engineering Change Controls']").Click();
new Control("Manage Engineering Change Approval Titles", "xpath","//div[@class='navItem'][.='Manage Engineering Change Approval Titles']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "ECMECAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ECMECAPT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ECMECAPT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPT] Perfoming VerifyExists on ApprovalTitle...", Logger.MessageType.INF);
			Control ECMECAPT_ApprovalTitle = new Control("ApprovalTitle", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EC_APP_TITLE_DC']");
			CPCommon.AssertEqual(true,ECMECAPT_ApprovalTitle.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPT";
							CPCommon.WaitControlDisplayed(ECMECAPT_MainForm);
IWebElement formBttn = ECMECAPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ECMECAPT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ECMECAPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ECMECAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ECMECAPT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILDFORM");


												
				CPCommon.CurrentComponent = "ECMECAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPT] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control ECMECAPT_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPT_ECAPPTITLEUSER_USEAPP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ECMECAPT_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "ECMECAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPT] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control ECMECAPT_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPT_ECAPPTITLEUSER_USEAPP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ECMECAPT_ChildForm);
formBttn = ECMECAPT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? ECMECAPT_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
ECMECAPT_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "ECMECAPT";
							CPCommon.AssertEqual(true,ECMECAPT_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "ECMECAPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ECMECAPT] Perfoming VerifyExists on ApproverDetails_PrefSequence...", Logger.MessageType.INF);
			Control ECMECAPT_ApproverDetails_PrefSequence = new Control("ApproverDetails_PrefSequence", "xpath", "//div[translate(@id,'0123456789','')='pr__ECMECAPT_ECAPPTITLEUSER_USEAPP_']/ancestor::form[1]/descendant::*[@id='PREF_SEQ_NO']");
			CPCommon.AssertEqual(true,ECMECAPT_ApproverDetails_PrefSequence.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ECMECAPT";
							CPCommon.WaitControlDisplayed(ECMECAPT_MainForm);
formBttn = ECMECAPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

