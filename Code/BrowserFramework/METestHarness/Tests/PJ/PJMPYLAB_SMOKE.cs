 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPYLAB_SMOKE : TestScript
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
new Control("Project History", "xpath","//div[@class='navItem'][.='Project History']").Click();
new Control("Manage Project Labor History", "xpath","//div[@class='navItem'][.='Manage Project Labor History']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPYLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYLAB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPYLAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPYLAB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPYLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYLAB] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control PJMPYLAB_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPYLAB_Identification_Project.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "PJMPYLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYLAB] Perfoming VerifyExist on ChildForm_Table...", Logger.MessageType.INF);
			Control PJMPYLAB_ChildForm_Table = new Control("ChildForm_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYLAB_LABHS_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPYLAB_ChildForm_Table.Exists());

												
				CPCommon.CurrentComponent = "PJMPYLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYLAB] Perfoming ClickButton on DetailsForm...", Logger.MessageType.INF);
			Control PJMPYLAB_DetailsForm = new Control("DetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYLAB_LABHS_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPYLAB_DetailsForm);
IWebElement formBttn = PJMPYLAB_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPYLAB_DetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPYLAB_DetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPYLAB";
							CPCommon.AssertEqual(true,PJMPYLAB_DetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPYLAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPYLAB] Perfoming VerifyExists on Details_PLC...", Logger.MessageType.INF);
			Control PJMPYLAB_Details_PLC = new Control("Details_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPYLAB_LABHS_CHLD_']/ancestor::form[1]/descendant::*[@id='BILL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,PJMPYLAB_Details_PLC.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMPYLAB";
							CPCommon.WaitControlDisplayed(PJMPYLAB_MainForm);
formBttn = PJMPYLAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

