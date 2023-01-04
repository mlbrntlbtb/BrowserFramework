 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPEPAD_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchasing Interfaces", "xpath","//div[@class='navItem'][.='Purchasing Interfaces']").Click();
new Control("Export eProcurement Addresses", "xpath","//div[@class='navItem'][.='Export eProcurement Addresses']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPEPAD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPAD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPEPAD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPEPAD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPEPAD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPAD] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPEPAD_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPEPAD_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPEPAD";
							CPCommon.WaitControlDisplayed(AOPEPAD_MainForm);
IWebElement formBttn = AOPEPAD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPEPAD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPEPAD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPEPAD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPAD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPEPAD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPAD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("SHIPIDNONCONTIGUOUSRANGES");


												
				CPCommon.CurrentComponent = "AOPEPAD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPAD] Perfoming Click on ShipIDNonContiguousRangesLink...", Logger.MessageType.INF);
			Control AOPEPAD_ShipIDNonContiguousRangesLink = new Control("ShipIDNonContiguousRangesLink", "ID", "lnk_1006037_AOPEPAD_PARAM");
			CPCommon.WaitControlDisplayed(AOPEPAD_ShipIDNonContiguousRangesLink);
AOPEPAD_ShipIDNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOPEPAD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPAD] Perfoming VerifyExist on ShipIDNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control AOPEPAD_ShipIDNonContiguousRangesFormTable = new Control("ShipIDNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSHIPID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPAD_ShipIDNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPEPAD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPAD] Perfoming Close on ShipIDNonContiguousRangesForm...", Logger.MessageType.INF);
			Control AOPEPAD_ShipIDNonContiguousRangesForm = new Control("ShipIDNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRSHIPID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOPEPAD_ShipIDNonContiguousRangesForm);
formBttn = AOPEPAD_ShipIDNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "AOPEPAD";
							CPCommon.WaitControlDisplayed(AOPEPAD_MainForm);
formBttn = AOPEPAD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

