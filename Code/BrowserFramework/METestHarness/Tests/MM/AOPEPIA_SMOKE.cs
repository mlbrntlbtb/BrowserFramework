 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPEPIA_SMOKE : TestScript
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
new Control("Export eProcurement Inventory Abbreviations", "xpath","//div[@class='navItem'][.='Export eProcurement Inventory Abbreviations']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPEPIA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPIA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPEPIA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPEPIA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPEPIA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPIA] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPEPIA_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPEPIA_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPEPIA";
							CPCommon.WaitControlDisplayed(AOPEPIA_MainForm);
IWebElement formBttn = AOPEPIA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPEPIA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPEPIA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPEPIA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPIA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPEPIA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPIA_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("SHIPIDNONCONTIGUOUSRANGES");


												
				CPCommon.CurrentComponent = "AOPEPIA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPIA] Perfoming Click on NonContiguousInvtAbbrLink...", Logger.MessageType.INF);
			Control AOPEPIA_NonContiguousInvtAbbrLink = new Control("NonContiguousInvtAbbrLink", "ID", "lnk_3573_AOPEPIA_WFUNCPARMCATLG_PARAM");
			CPCommon.WaitControlDisplayed(AOPEPIA_NonContiguousInvtAbbrLink);
AOPEPIA_NonContiguousInvtAbbrLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOPEPIA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPIA] Perfoming VerifyExists on NonContiguousInvtAbbrForm...", Logger.MessageType.INF);
			Control AOPEPIA_NonContiguousInvtAbbrForm = new Control("NonContiguousInvtAbbrForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVTABBRVCD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOPEPIA_NonContiguousInvtAbbrForm.Exists());

												
				CPCommon.CurrentComponent = "AOPEPIA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPIA] Perfoming VerifyExist on NonContiguousInvtAbbrFormTable...", Logger.MessageType.INF);
			Control AOPEPIA_NonContiguousInvtAbbrFormTable = new Control("NonContiguousInvtAbbrFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRINVTABBRVCD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPIA_NonContiguousInvtAbbrFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPEPIA";
							CPCommon.WaitControlDisplayed(AOPEPIA_NonContiguousInvtAbbrForm);
formBttn = AOPEPIA_NonContiguousInvtAbbrForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "AOPEPIA";
							CPCommon.WaitControlDisplayed(AOPEPIA_MainForm);
formBttn = AOPEPIA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

