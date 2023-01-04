 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEPRFID_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Order Entry Utilities", "xpath","//div[@class='navItem'][.='Sales Order Entry Utilities']").Click();
new Control("Create RFID Print File", "xpath","//div[@class='navItem'][.='Create RFID Print File']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "OEPRFID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPRFID] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control OEPRFID_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,OEPRFID_MainForm.Exists());

												
				CPCommon.CurrentComponent = "OEPRFID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPRFID] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control OEPRFID_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,OEPRFID_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "OEPRFID";
							CPCommon.WaitControlDisplayed(OEPRFID_MainForm);
IWebElement formBttn = OEPRFID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? OEPRFID_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
OEPRFID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "OEPRFID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPRFID] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEPRFID_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPRFID_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPRFID";
							CPCommon.WaitControlDisplayed(OEPRFID_MainForm);
formBttn = OEPRFID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEPRFID_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEPRFID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "OEPRFID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPRFID] Perfoming Click on RFIDNonContiguousRangesLink...", Logger.MessageType.INF);
			Control OEPRFID_RFIDNonContiguousRangesLink = new Control("RFIDNonContiguousRangesLink", "ID", "lnk_5339_OEPRFID_PARAM");
			CPCommon.WaitControlDisplayed(OEPRFID_RFIDNonContiguousRangesLink);
OEPRFID_RFIDNonContiguousRangesLink.Click(1.5);


											Driver.SessionLogger.WriteLine("RFIDNonContiguousRangesForm");


												
				CPCommon.CurrentComponent = "OEPRFID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPRFID] Perfoming VerifyExists on RFIDNonContiguousRangesForm...", Logger.MessageType.INF);
			Control OEPRFID_RFIDNonContiguousRangesForm = new Control("RFIDNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPRFID_NCR_RFID_CD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,OEPRFID_RFIDNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "OEPRFID";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEPRFID] Perfoming VerifyExist on RFIDNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control OEPRFID_RFIDNonContiguousRangesFormTable = new Control("RFIDNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__OEPRFID_NCR_RFID_CD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEPRFID_RFIDNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEPRFID";
							CPCommon.WaitControlDisplayed(OEPRFID_RFIDNonContiguousRangesForm);
formBttn = OEPRFID_RFIDNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "OEPRFID";
							CPCommon.WaitControlDisplayed(OEPRFID_MainForm);
formBttn = OEPRFID_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

