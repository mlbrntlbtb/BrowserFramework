 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEPPBOM_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Proposal Bills of Material", "xpath","//div[@class='navItem'][.='Proposal Bills of Material']").Click();
new Control("Copy Proposal Bills of Material", "xpath","//div[@class='navItem'][.='Copy Proposal Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "MEPPBOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPPBOM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEPPBOM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEPPBOM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEPPBOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPPBOM] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MEPPBOM_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MEPPBOM_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MEPPBOM";
							CPCommon.WaitControlDisplayed(MEPPBOM_MainForm);
IWebElement formBttn = MEPPBOM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MEPPBOM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MEPPBOM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MEPPBOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPPBOM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MEPPBOM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEPPBOM_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Serial Number Lookup");


												
				CPCommon.CurrentComponent = "MEPPBOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPPBOM] Perfoming VerifyExists on MainForm_SerialNumberLookupLink...", Logger.MessageType.INF);
			Control MEPPBOM_MainForm_SerialNumberLookupLink = new Control("MainForm_SerialNumberLookupLink", "ID", "lnk_2337_MEPPBOM_PARAM");
			CPCommon.AssertEqual(true,MEPPBOM_MainForm_SerialNumberLookupLink.Exists());

												
				CPCommon.CurrentComponent = "MEPPBOM";
							CPCommon.WaitControlDisplayed(MEPPBOM_MainForm_SerialNumberLookupLink);
MEPPBOM_MainForm_SerialNumberLookupLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MEPPBOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPPBOM] Perfoming VerifyExist on SerialNumberLookup_table1...", Logger.MessageType.INF);
			Control MEPPBOM_SerialNumberLookup_table1 = new Control("SerialNumberLookup_table1", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIAPEG_ENDPARTCONFIG_LKP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEPPBOM_SerialNumberLookup_table1.Exists());

												
				CPCommon.CurrentComponent = "MEPPBOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEPPBOM] Perfoming Close on SerialNumberLookupForm...", Logger.MessageType.INF);
			Control MEPPBOM_SerialNumberLookupForm = new Control("SerialNumberLookupForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMIAPEG_ENDPARTCONFIG_LKP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEPPBOM_SerialNumberLookupForm);
formBttn = MEPPBOM_SerialNumberLookupForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "MEPPBOM";
							CPCommon.WaitControlDisplayed(MEPPBOM_MainForm);
formBttn = MEPPBOM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

