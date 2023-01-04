 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMRTSET_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Routings Controls", "xpath","//div[@class='navItem'][.='Routings Controls']").Click();
new Control("Configure Routing Settings", "xpath","//div[@class='navItem'][.='Configure Routing Settings']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "RUMRTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRTSET] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUMRTSET_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUMRTSET_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUMRTSET";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMRTSET] Perfoming VerifyExists on AutoNumberEquipmentIDs...", Logger.MessageType.INF);
			Control RUMRTSET_AutoNumberEquipmentIDs = new Control("AutoNumberEquipmentIDs", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EQUIP_AUTO_NUM_FL']");
			CPCommon.AssertEqual(true,RUMRTSET_AutoNumberEquipmentIDs.Exists());

												
				CPCommon.CurrentComponent = "RUMRTSET";
							CPCommon.WaitControlDisplayed(RUMRTSET_MainForm);
IWebElement formBttn = RUMRTSET_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

