 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLRMBIL_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Standard Bills Processing", "xpath","//div[@class='navItem'][.='Standard Bills Processing']").Click();
new Control("Print Standard Bills", "xpath","//div[@class='navItem'][.='Print Standard Bills']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLRMBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRMBIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLRMBIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLRMBIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLRMBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRMBIL] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLRMBIL_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLRMBIL_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLRMBIL";
							CPCommon.WaitControlDisplayed(BLRMBIL_MainForm);
IWebElement formBttn = BLRMBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLRMBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLRMBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLRMBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRMBIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLRMBIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRMBIL_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECT NON CON RANGES");


												
				CPCommon.CurrentComponent = "BLRMBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRMBIL] Perfoming VerifyExists on ProjectNonContiguousLink...", Logger.MessageType.INF);
			Control BLRMBIL_ProjectNonContiguousLink = new Control("ProjectNonContiguousLink", "ID", "lnk_1004284_BLRMBIL_WFUNCPARMCATLG_HDR");
			CPCommon.AssertEqual(true,BLRMBIL_ProjectNonContiguousLink.Exists());

												
				CPCommon.CurrentComponent = "BLRMBIL";
							CPCommon.WaitControlDisplayed(BLRMBIL_ProjectNonContiguousLink);
BLRMBIL_ProjectNonContiguousLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLRMBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRMBIL] Perfoming VerifyExists on ProjectNonContiguousForm...", Logger.MessageType.INF);
			Control BLRMBIL_ProjectNonContiguousForm = new Control("ProjectNonContiguousForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRMBIL_NONCONTIGUOUS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLRMBIL_ProjectNonContiguousForm.Exists());

												
				CPCommon.CurrentComponent = "BLRMBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRMBIL] Perfoming VerifyExist on ProjectNonContiguousLinkTable...", Logger.MessageType.INF);
			Control BLRMBIL_ProjectNonContiguousLinkTable = new Control("ProjectNonContiguousLinkTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRMBIL_NONCONTIGUOUS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRMBIL_ProjectNonContiguousLinkTable.Exists());

												
				CPCommon.CurrentComponent = "BLRMBIL";
							CPCommon.WaitControlDisplayed(BLRMBIL_ProjectNonContiguousForm);
formBttn = BLRMBIL_ProjectNonContiguousForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLRMBIL";
							CPCommon.WaitControlDisplayed(BLRMBIL_MainForm);
formBttn = BLRMBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

