 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMMICEC_SMOKE : TestScript
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
new Control("Manage Microframe EOC Mappings", "xpath","//div[@class='navItem'][.='Manage Microframe EOC Mappings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "AOMMICEC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICEC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMMICEC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMMICEC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMMICEC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICEC] Perfoming VerifyExists on MappingID...", Logger.MessageType.INF);
			Control AOMMICEC_MappingID = new Control("MappingID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='X_MAP_CD']");
			CPCommon.AssertEqual(true,AOMMICEC_MappingID.Exists());

												
				CPCommon.CurrentComponent = "AOMMICEC";
							CPCommon.WaitControlDisplayed(AOMMICEC_MainForm);
IWebElement formBttn = AOMMICEC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMMICEC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMMICEC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMMICEC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICEC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMMICEC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMMICEC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("mapping Table Form");


												
				CPCommon.CurrentComponent = "AOMMICEC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICEC] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control AOMMICEC_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICEC_XMICEOCMAP_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMMICEC_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMMICEC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICEC] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control AOMMICEC_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICEC_XMICEOCMAP_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOMMICEC_ChildForm);
formBttn = AOMMICEC_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMMICEC_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMMICEC_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "AOMMICEC";
							CPCommon.AssertEqual(true,AOMMICEC_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "AOMMICEC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMMICEC] Perfoming VerifyExists on ChildForm_Account...", Logger.MessageType.INF);
			Control AOMMICEC_ChildForm_Account = new Control("ChildForm_Account", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMMICEC_XMICEOCMAP_']/ancestor::form[1]/descendant::*[@id='ACCT_ID']");
			CPCommon.AssertEqual(true,AOMMICEC_ChildForm_Account.Exists());

											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "AOMMICEC";
							CPCommon.WaitControlDisplayed(AOMMICEC_MainForm);
formBttn = AOMMICEC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

