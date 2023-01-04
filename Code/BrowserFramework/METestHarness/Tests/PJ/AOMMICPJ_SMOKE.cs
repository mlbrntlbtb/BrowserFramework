 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMMICPJ_SMOKE : TestScript
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
new Control("Cost and Revenue Processing Interfaces", "xpath","//div[@class='navItem'][.='Cost and Revenue Processing Interfaces']").Click();
new Control("Manage Microframe WBS Mappings", "xpath","//div[@class='navItem'][.='Manage Microframe WBS Mappings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "AOMMICPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICPJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMMICPJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMMICPJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMMICPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICPJ] Perfoming VerifyExists on RangeOption...", Logger.MessageType.INF);
			Control AOMMICPJ_RangeOption = new Control("RangeOption", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RANGE_OPTION']");
			CPCommon.AssertEqual(true,AOMMICPJ_RangeOption.Exists());

											Driver.SessionLogger.WriteLine("Child Form");


												
				CPCommon.CurrentComponent = "AOMMICPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICPJ] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control AOMMICPJ_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICPJ_XMICWBSMAP_MICRO_WBS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMMICPJ_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMMICPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICPJ] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control AOMMICPJ_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICPJ_XMICWBSMAP_MICRO_WBS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOMMICPJ_ChildForm);
IWebElement formBttn = AOMMICPJ_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMMICPJ_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMMICPJ_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMMICPJ";
							CPCommon.AssertEqual(true,AOMMICPJ_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "AOMMICPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICPJ] Perfoming VerifyExists on ChildForm_Project...", Logger.MessageType.INF);
			Control AOMMICPJ_ChildForm_Project = new Control("ChildForm_Project", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICPJ_XMICWBSMAP_MICRO_WBS_']/ancestor::form[1]/descendant::*[@id='X_CP_PROJ_ID_PK']");
			CPCommon.AssertEqual(true,AOMMICPJ_ChildForm_Project.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "AOMMICPJ";
							CPCommon.WaitControlDisplayed(AOMMICPJ_MainForm);
formBttn = AOMMICPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

