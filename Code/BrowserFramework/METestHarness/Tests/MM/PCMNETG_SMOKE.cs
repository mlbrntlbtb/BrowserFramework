 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMNETG_SMOKE : TestScript
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
new Control("Production Control", "xpath","//div[@class='deptItem'][.='Production Control']").Click();
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Manage Netting Groups", "xpath","//div[@class='navItem'][.='Manage Netting Groups']").Click();


												
				CPCommon.CurrentComponent = "PCMNETG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMNETG] Perfoming VerifyExist on UnassignedInventoryProjectsTable...", Logger.MessageType.INF);
			Control PCMNETG_UnassignedInventoryProjectsTable = new Control("UnassignedInventoryProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMNETG_INVTPROJ_SELECTED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMNETG_UnassignedInventoryProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMNETG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMNETG] Perfoming VerifyExist on SelectedInventoryProjectsTable...", Logger.MessageType.INF);
			Control PCMNETG_SelectedInventoryProjectsTable = new Control("SelectedInventoryProjectsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCMNETG_INVT_PROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMNETG_SelectedInventoryProjectsTable.Exists());

												
				CPCommon.CurrentComponent = "PCMNETG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMNETG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCMNETG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMNETG_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PCMNETG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMNETG] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PCMNETG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PCMNETG_MainForm);
IWebElement formBttn = PCMNETG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PCMNETG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PCMNETG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PCMNETG";
							CPCommon.AssertEqual(true,PCMNETG_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PCMNETG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMNETG] Perfoming VerifyExists on NettingGroup...", Logger.MessageType.INF);
			Control PCMNETG_NettingGroup = new Control("NettingGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NET_GRP_ID']");
			CPCommon.AssertEqual(true,PCMNETG_NettingGroup.Exists());

												
				CPCommon.CurrentComponent = "PCMNETG";
							CPCommon.WaitControlDisplayed(PCMNETG_MainForm);
formBttn = PCMNETG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

