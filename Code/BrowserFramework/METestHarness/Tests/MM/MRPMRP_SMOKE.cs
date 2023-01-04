 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRPMRP_SMOKE : TestScript
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
new Control("Material Requirements Planning", "xpath","//div[@class='deptItem'][.='Material Requirements Planning']").Click();
new Control("Material Requirements Planning", "xpath","//div[@class='navItem'][.='Material Requirements Planning']").Click();
new Control("Update Material Requirements Plan", "xpath","//div[@class='navItem'][.='Update Material Requirements Plan']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MRPMRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPMRP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MRPMRP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRPMRP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MRPMRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPMRP] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control MRPMRP_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MRPMRP_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MRPMRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPMRP] Perfoming VerifyExists on WarehouseStart...", Logger.MessageType.INF);
			Control MRPMRP_WarehouseStart = new Control("WarehouseStart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FROM_WAREHOUSE']");
			CPCommon.AssertEqual(true,MRPMRP_WarehouseStart.Exists());

												
				CPCommon.CurrentComponent = "MRPMRP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPMRP] Perfoming VerifyExists on MRPMethod_FullRegeneration...", Logger.MessageType.INF);
			Control MRPMRP_MRPMethod_FullRegeneration = new Control("MRPMethod_FullRegeneration", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MRP_METHOD_CD' and @value='F']");
			CPCommon.AssertEqual(true,MRPMRP_MRPMethod_FullRegeneration.Exists());

												
				CPCommon.CurrentComponent = "MRPMRP";
							CPCommon.WaitControlDisplayed(MRPMRP_MainForm);
IWebElement formBttn = MRPMRP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

