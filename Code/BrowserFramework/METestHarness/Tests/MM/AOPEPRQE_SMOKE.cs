 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPEPRQE_SMOKE : TestScript
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
new Control("Export eProcurement Requisitions", "xpath","//div[@class='navItem'][.='Export eProcurement Requisitions']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPEPRQE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPRQE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPEPRQE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPEPRQE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPEPRQE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPRQE] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPEPRQE_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPEPRQE_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPEPRQE";
							CPCommon.WaitControlDisplayed(AOPEPRQE_MainForm);
IWebElement formBttn = AOPEPRQE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPEPRQE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPEPRQE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPEPRQE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPRQE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPEPRQE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPRQE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("PROJECTNONCONTIGUOUSRANGES");


												
				CPCommon.CurrentComponent = "AOPEPRQE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPRQE] Perfoming Click on RequisitionNonContiguousRangesLink...", Logger.MessageType.INF);
			Control AOPEPRQE_RequisitionNonContiguousRangesLink = new Control("RequisitionNonContiguousRangesLink", "ID", "lnk_3592_AOPEPRQE_PARAM");
			CPCommon.WaitControlDisplayed(AOPEPRQE_RequisitionNonContiguousRangesLink);
AOPEPRQE_RequisitionNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOPEPRQE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPRQE] Perfoming VerifyExist on RequisitionNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control AOPEPRQE_RequisitionNonContiguousRangesFormTable = new Control("RequisitionNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRRQID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPEPRQE_RequisitionNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOPEPRQE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPEPRQE] Perfoming Close on RequisitionNonContiguousRangesForm...", Logger.MessageType.INF);
			Control AOPEPRQE_RequisitionNonContiguousRangesForm = new Control("RequisitionNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRRQID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOPEPRQE_RequisitionNonContiguousRangesForm);
formBttn = AOPEPRQE_RequisitionNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "AOPEPRQE";
							CPCommon.WaitControlDisplayed(AOPEPRQE_MainForm);
formBttn = AOPEPRQE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

