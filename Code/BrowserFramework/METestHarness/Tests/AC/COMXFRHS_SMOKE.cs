 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class COMXFRHS_SMOKE : TestScript
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
new Control("Consolidations", "xpath","//div[@class='deptItem'][.='Consolidations']").Click();
new Control("Consolidations Processing", "xpath","//div[@class='navItem'][.='Consolidations Processing']").Click();
new Control("Manage Consolidation Transfer History", "xpath","//div[@class='navItem'][.='Manage Consolidation Transfer History']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "COMXFRHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMXFRHS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control COMXFRHS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,COMXFRHS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "COMXFRHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMXFRHS] Perfoming VerifyExists on ReceivingLocation...", Logger.MessageType.INF);
			Control COMXFRHS_ReceivingLocation = new Control("ReceivingLocation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RCV_LOC_CD']");
			CPCommon.AssertEqual(true,COMXFRHS_ReceivingLocation.Exists());

												
				CPCommon.CurrentComponent = "COMXFRHS";
							CPCommon.WaitControlDisplayed(COMXFRHS_MainForm);
IWebElement formBttn = COMXFRHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? COMXFRHS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
COMXFRHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "COMXFRHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMXFRHS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control COMXFRHS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMXFRHS_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMXFRHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMXFRHS] Perfoming VerifyExists on PreviousTransfersLink...", Logger.MessageType.INF);
			Control COMXFRHS_PreviousTransfersLink = new Control("PreviousTransfersLink", "ID", "lnk_3707_COMXFRHS_CONSXFRHS_HDR");
			CPCommon.AssertEqual(true,COMXFRHS_PreviousTransfersLink.Exists());

												
				CPCommon.CurrentComponent = "COMXFRHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMXFRHS] Perfoming VerifyExists on PreviousTransfersForm...", Logger.MessageType.INF);
			Control COMXFRHS_PreviousTransfersForm = new Control("PreviousTransfersForm", "xpath", "//div[translate(@id,'0123456789','')='pr__COMXFRHS_CONSXFRHS_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,COMXFRHS_PreviousTransfersForm.Exists());

												
				CPCommon.CurrentComponent = "COMXFRHS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[COMXFRHS] Perfoming VerifyExist on PreviousTransfersFormTable...", Logger.MessageType.INF);
			Control COMXFRHS_PreviousTransfersFormTable = new Control("PreviousTransfersFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__COMXFRHS_CONSXFRHS_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,COMXFRHS_PreviousTransfersFormTable.Exists());

												
				CPCommon.CurrentComponent = "COMXFRHS";
							CPCommon.WaitControlDisplayed(COMXFRHS_MainForm);
formBttn = COMXFRHS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

