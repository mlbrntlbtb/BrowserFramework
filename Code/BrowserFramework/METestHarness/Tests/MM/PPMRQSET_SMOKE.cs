 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQSET_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Procurement Planning Controls", "xpath","//div[@class='navItem'][.='Procurement Planning Controls']").Click();
new Control("Configure Purchase Requisition Settings", "xpath","//div[@class='navItem'][.='Configure Purchase Requisition Settings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPMRQSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMRQSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMRQSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMRQSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQSET] Perfoming VerifyExists on RequisitionNumberingMethod_LastSystemReqNumber...", Logger.MessageType.INF);
			Control PPMRQSET_RequisitionNumberingMethod_LastSystemReqNumber = new Control("RequisitionNumberingMethod_LastSystemReqNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LAST_RQ_ID']");
			CPCommon.AssertEqual(true,PPMRQSET_RequisitionNumberingMethod_LastSystemReqNumber.Exists());

												
				CPCommon.CurrentComponent = "PPMRQSET";
							CPCommon.WaitControlDisplayed(PPMRQSET_MainForm);
IWebElement formBttn = PPMRQSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

