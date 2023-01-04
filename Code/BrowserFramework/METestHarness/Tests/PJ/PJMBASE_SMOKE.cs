 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMBASE_SMOKE : TestScript
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
new Control("Cost Pools", "xpath","//div[@class='navItem'][.='Cost Pools']").Click();
new Control("Manage Base Creation Setups", "xpath","//div[@class='navItem'][.='Manage Base Creation Setups']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMBASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBASE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMBASE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMBASE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMBASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBASE] Perfoming VerifyExists on AllocationGroup...", Logger.MessageType.INF);
			Control PJMBASE_AllocationGroup = new Control("AllocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALLOC_GRP_NO']");
			CPCommon.AssertEqual(true,PJMBASE_AllocationGroup.Exists());

												
				CPCommon.CurrentComponent = "PJMBASE";
							CPCommon.WaitControlDisplayed(PJMBASE_MainForm);
IWebElement formBttn = PJMBASE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJMBASE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJMBASE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJMBASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBASE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJMBASE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBASE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMBASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBASE] Perfoming VerifyExist on BaseCreationDetailsFormTable...", Logger.MessageType.INF);
			Control PJMBASE_BaseCreationDetailsFormTable = new Control("BaseCreationDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASE_POOLBASE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMBASE_BaseCreationDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJMBASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBASE] Perfoming ClickButton on BaseCreationDetailsForm...", Logger.MessageType.INF);
			Control PJMBASE_BaseCreationDetailsForm = new Control("BaseCreationDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASE_POOLBASE_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMBASE_BaseCreationDetailsForm);
formBttn = PJMBASE_BaseCreationDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMBASE_BaseCreationDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMBASE_BaseCreationDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMBASE";
							CPCommon.AssertEqual(true,PJMBASE_BaseCreationDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMBASE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMBASE] Perfoming VerifyExists on BaseCreationDetails_Base_Acct...", Logger.MessageType.INF);
			Control PJMBASE_BaseCreationDetails_Base_Acct = new Control("BaseCreationDetails_Base_Acct", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMBASE_POOLBASE_CHILD_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,PJMBASE_BaseCreationDetails_Base_Acct.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "PJMBASE";
							CPCommon.WaitControlDisplayed(PJMBASE_MainForm);
formBttn = PJMBASE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

