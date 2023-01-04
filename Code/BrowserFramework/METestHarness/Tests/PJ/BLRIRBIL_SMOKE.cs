 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLRIRBIL_SMOKE : TestScript
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
new Control("Print Indirect Rate Consolidated Retro Bill Worksheet", "xpath","//div[@class='navItem'][.='Print Indirect Rate Consolidated Retro Bill Worksheet']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLRIRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRIRBIL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLRIRBIL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLRIRBIL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLRIRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRIRBIL] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BLRIRBIL_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLRIRBIL_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLRIRBIL";
							CPCommon.WaitControlDisplayed(BLRIRBIL_MainForm);
IWebElement formBttn = BLRIRBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLRIRBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLRIRBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "BLRIRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRIRBIL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLRIRBIL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRIRBIL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLRIRBIL";
							CPCommon.WaitControlDisplayed(BLRIRBIL_MainForm);
formBttn = BLRIRBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLRIRBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLRIRBIL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												Driver.SessionLogger.WriteLine("Project Non Contigous Ranges ");


												
				CPCommon.CurrentComponent = "BLRIRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRIRBIL] Perfoming VerifyExists on Identification_ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLRIRBIL_Identification_ProjectNonContiguousRangesLink = new Control("Identification_ProjectNonContiguousRangesLink", "ID", "lnk_17406_BLRIRBIL_WFUNCPARMCATLG_HDR");
			CPCommon.AssertEqual(true,BLRIRBIL_Identification_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLRIRBIL";
							CPCommon.WaitControlDisplayed(BLRIRBIL_Identification_ProjectNonContiguousRangesLink);
BLRIRBIL_Identification_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLRIRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRIRBIL] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control BLRIRBIL_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRIRBIL_NCR_PROJ_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLRIRBIL_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLRIRBIL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLRIRBIL] Perfoming VerifyExists on ProjectNonContiguousRangesFormTable_Ok...", Logger.MessageType.INF);
			Control BLRIRBIL_ProjectNonContiguousRangesFormTable_Ok = new Control("ProjectNonContiguousRangesFormTable_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__BLRIRBIL_NCR_PROJ_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.AssertEqual(true,BLRIRBIL_ProjectNonContiguousRangesFormTable_Ok.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLRIRBIL";
							CPCommon.WaitControlDisplayed(BLRIRBIL_MainForm);
formBttn = BLRIRBIL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

