 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMRE_SMOKE : TestScript
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
new Control("Reorganizations", "xpath","//div[@class='navItem'][.='Reorganizations']").Click();
new Control("Manage Reorganization Elements", "xpath","//div[@class='navItem'][.='Manage Reorganization Elements']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "GLMRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMRE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMRE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRE] Perfoming VerifyExists on Identification_Reorganization...", Logger.MessageType.INF);
			Control GLMRE_Identification_Reorganization = new Control("Identification_Reorganization", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DFSREORGID']");
			CPCommon.AssertEqual(true,GLMRE_Identification_Reorganization.Exists());

												
				CPCommon.CurrentComponent = "GLMRE";
							CPCommon.WaitControlDisplayed(GLMRE_MainForm);
IWebElement formBttn = GLMRE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMRE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMRE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMRE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD");


												
				CPCommon.CurrentComponent = "GLMRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRE] Perfoming VerifyExists on Identification_LinkReorganizationsToOrgsLink...", Logger.MessageType.INF);
			Control GLMRE_Identification_LinkReorganizationsToOrgsLink = new Control("Identification_LinkReorganizationsToOrgsLink", "ID", "lnk_1000695_GLMRE_REORGSTRUC_MAINTREORG");
			CPCommon.AssertEqual(true,GLMRE_Identification_LinkReorganizationsToOrgsLink.Exists());

												
				CPCommon.CurrentComponent = "GLMRE";
							CPCommon.WaitControlDisplayed(GLMRE_Identification_LinkReorganizationsToOrgsLink);
GLMRE_Identification_LinkReorganizationsToOrgsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "GLMRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRE] Perfoming VerifyExists on LinkReorganizationsToOrgsForm...", Logger.MessageType.INF);
			Control GLMRE_LinkReorganizationsToOrgsForm = new Control("LinkReorganizationsToOrgsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRE_REORGORGLINK_LINK_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMRE_LinkReorganizationsToOrgsForm.Exists());

												
				CPCommon.CurrentComponent = "GLMRE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMRE] Perfoming VerifyExist on LinkReorganizationsToOrgsFormTable...", Logger.MessageType.INF);
			Control GLMRE_LinkReorganizationsToOrgsFormTable = new Control("LinkReorganizationsToOrgsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMRE_REORGORGLINK_LINK_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMRE_LinkReorganizationsToOrgsFormTable.Exists());

												
				CPCommon.CurrentComponent = "GLMRE";
							CPCommon.WaitControlDisplayed(GLMRE_LinkReorganizationsToOrgsForm);
formBttn = GLMRE_LinkReorganizationsToOrgsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "GLMRE";
							CPCommon.WaitControlDisplayed(GLMRE_MainForm);
formBttn = GLMRE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

