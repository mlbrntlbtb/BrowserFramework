 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRPTOOL3_SMOKE : TestScript
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
new Control("MRP Utilities", "xpath","//div[@class='navItem'][.='MRP Utilities']").Click();
new Control("Assign Planning Warehouses to Inventory Projects", "xpath","//div[@class='navItem'][.='Assign Planning Warehouses to Inventory Projects']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MRPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPTOOL3] Perfoming VerifyExists on AssignPlanningWarehousesForm...", Logger.MessageType.INF);
			Control MRPTOOL3_AssignPlanningWarehousesForm = new Control("AssignPlanningWarehousesForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRPTOOL3_AssignPlanningWarehousesForm.Exists());

												
				CPCommon.CurrentComponent = "MRPTOOL3";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPTOOL3] Perfoming VerifyExists on AssignPlanningWarehouses_SelectionRange_Start...", Logger.MessageType.INF);
			Control MRPTOOL3_AssignPlanningWarehouses_SelectionRange_Start = new Control("AssignPlanningWarehouses_SelectionRange_Start", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID_FR']");
			CPCommon.AssertEqual(true,MRPTOOL3_AssignPlanningWarehouses_SelectionRange_Start.Exists());

												
				CPCommon.CurrentComponent = "MRPTOOL3";
							CPCommon.WaitControlDisplayed(MRPTOOL3_AssignPlanningWarehousesForm);
IWebElement formBttn = MRPTOOL3_AssignPlanningWarehousesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

