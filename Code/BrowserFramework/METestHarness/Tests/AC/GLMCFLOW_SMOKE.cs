 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMCFLOW_SMOKE : TestScript
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
new Control("General Ledger", "xpath","//div[@class='deptItem'][.='General Ledger']").Click();
new Control("General Ledger Reports/Inquiries", "xpath","//div[@class='navItem'][.='General Ledger Reports/Inquiries']").Click();
new Control("Manage Preliminary Cash Flow Statements", "xpath","//div[@class='navItem'][.='Manage Preliminary Cash Flow Statements']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "GLMCFLOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCFLOW] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMCFLOW_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCFLOW_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMCFLOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCFLOW] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control GLMCFLOW_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(GLMCFLOW_MainForm);
IWebElement formBttn = GLMCFLOW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMCFLOW_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMCFLOW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMCFLOW";
							CPCommon.AssertEqual(true,GLMCFLOW_MainForm.Exists());

													
				CPCommon.CurrentComponent = "GLMCFLOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCFLOW] Perfoming VerifyExists on Identification_FSCode...", Logger.MessageType.INF);
			Control GLMCFLOW_Identification_FSCode = new Control("Identification_FSCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FS_CD']");
			CPCommon.AssertEqual(true,GLMCFLOW_Identification_FSCode.Exists());

												
				CPCommon.CurrentComponent = "GLMCFLOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCFLOW] Perfoming VerifyExist on CashFlowDetailsFormTable...", Logger.MessageType.INF);
			Control GLMCFLOW_CashFlowDetailsFormTable = new Control("CashFlowDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMCFLOW_FSLNADJ_CH_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMCFLOW_CashFlowDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMCFLOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCFLOW] Perfoming ClickButton on CashFlowDetailsForm...", Logger.MessageType.INF);
			Control GLMCFLOW_CashFlowDetailsForm = new Control("CashFlowDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMCFLOW_FSLNADJ_CH_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(GLMCFLOW_CashFlowDetailsForm);
formBttn = GLMCFLOW_CashFlowDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? GLMCFLOW_CashFlowDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
GLMCFLOW_CashFlowDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "GLMCFLOW";
							CPCommon.AssertEqual(true,GLMCFLOW_CashFlowDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "GLMCFLOW";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMCFLOW] Perfoming VerifyExists on CashFlowDetails_ActivityType...", Logger.MessageType.INF);
			Control GLMCFLOW_CashFlowDetails_ActivityType = new Control("CashFlowDetails_ActivityType", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMCFLOW_FSLNADJ_CH_']/ancestor::form[1]/descendant::*[@id='FS_S_CF_ACTVTY_CD']");
			CPCommon.AssertEqual(true,GLMCFLOW_CashFlowDetails_ActivityType.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMCFLOW";
							CPCommon.WaitControlDisplayed(GLMCFLOW_CashFlowDetailsForm);
formBttn = GLMCFLOW_CashFlowDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? GLMCFLOW_CashFlowDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
GLMCFLOW_CashFlowDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
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


												
				CPCommon.CurrentComponent = "GLMCFLOW";
							CPCommon.WaitControlDisplayed(GLMCFLOW_MainForm);
formBttn = GLMCFLOW_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

