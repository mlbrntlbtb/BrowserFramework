 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOPCPMO_SMOKE : TestScript
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
new Control("Production Control Interfaces", "xpath","//div[@class='navItem'][.='Production Control Interfaces']").Click();
new Control("Export Manufacturing Orders", "xpath","//div[@class='navItem'][.='Export Manufacturing Orders']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "AOPCPMO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCPMO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOPCPMO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOPCPMO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOPCPMO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCPMO] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control AOPCPMO_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,AOPCPMO_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "AOPCPMO";
							CPCommon.WaitControlDisplayed(AOPCPMO_MainForm);
IWebElement formBttn = AOPCPMO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOPCPMO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOPCPMO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOPCPMO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCPMO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOPCPMO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPCPMO_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("MANUFACTURINGORDERNONCONTIGUOUSRANGES");


												
				CPCommon.CurrentComponent = "AOPCPMO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCPMO] Perfoming Click on ManufacturingOrderNonContiguousRangesLink...", Logger.MessageType.INF);
			Control AOPCPMO_ManufacturingOrderNonContiguousRangesLink = new Control("ManufacturingOrderNonContiguousRangesLink", "ID", "lnk_1004737_AOPCPMO_MOROUTING_DOWNLOADMO");
			CPCommon.WaitControlDisplayed(AOPCPMO_ManufacturingOrderNonContiguousRangesLink);
AOPCPMO_ManufacturingOrderNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOPCPMO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCPMO] Perfoming VerifyExist on ManufacturingOrderNonContiguousRangesTable...", Logger.MessageType.INF);
			Control AOPCPMO_ManufacturingOrderNonContiguousRangesTable = new Control("ManufacturingOrderNonContiguousRangesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRMOID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOPCPMO_ManufacturingOrderNonContiguousRangesTable.Exists());

												
				CPCommon.CurrentComponent = "AOPCPMO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOPCPMO] Perfoming Close on ManufacturingOrderNonContiguousRangesForm...", Logger.MessageType.INF);
			Control AOPCPMO_ManufacturingOrderNonContiguousRangesForm = new Control("ManufacturingOrderNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRMOID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(AOPCPMO_ManufacturingOrderNonContiguousRangesForm);
formBttn = AOPCPMO_ManufacturingOrderNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "AOPCPMO";
							CPCommon.WaitControlDisplayed(AOPCPMO_MainForm);
formBttn = AOPCPMO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

