 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class GLMORSET_SMOKE : TestScript
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
new Control("Organizations", "xpath","//div[@class='navItem'][.='Organizations']").Click();
new Control("Manage Organization Structures", "xpath","//div[@class='navItem'][.='Manage Organization Structures']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "GLMORSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control GLMORSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,GLMORSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "GLMORSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORSET] Perfoming VerifyExists on Organization...", Logger.MessageType.INF);
			Control GLMORSET_Organization = new Control("Organization", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,GLMORSET_Organization.Exists());

												
				CPCommon.CurrentComponent = "GLMORSET";
							CPCommon.WaitControlDisplayed(GLMORSET_MainForm);
IWebElement formBttn = GLMORSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? GLMORSET_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
GLMORSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "GLMORSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORSET] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control GLMORSET_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMORSET_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "GLMORSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORSET] Perfoming VerifyExists on childForm...", Logger.MessageType.INF);
			Control GLMORSET_childForm = new Control("childForm", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMORSET_ORGLVL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,GLMORSET_childForm.Exists());

												
				CPCommon.CurrentComponent = "GLMORSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[GLMORSET] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control GLMORSET_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__GLMORSET_ORGLVL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,GLMORSET_ChildForm_Table.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "GLMORSET";
							CPCommon.WaitControlDisplayed(GLMORSET_MainForm);
formBttn = GLMORSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

