 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMPLANT_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Inventory Controls", "xpath","//div[@class='navItem'][.='Inventory Controls']").Click();
new Control("Manage Plants", "xpath","//div[@class='navItem'][.='Manage Plants']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "INMPLANT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPLANT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INMPLANT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMPLANT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMPLANT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPLANT] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control INMPLANT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(INMPLANT_MainForm);
IWebElement formBttn = INMPLANT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMPLANT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMPLANT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "INMPLANT";
							CPCommon.AssertEqual(true,INMPLANT_MainForm.Exists());

													
				CPCommon.CurrentComponent = "INMPLANT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMPLANT] Perfoming VerifyExists on PlantID...", Logger.MessageType.INF);
			Control INMPLANT_PlantID = new Control("PlantID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PLANT_ID']");
			CPCommon.AssertEqual(true,INMPLANT_PlantID.Exists());

												
				CPCommon.CurrentComponent = "INMPLANT";
							CPCommon.WaitControlDisplayed(INMPLANT_MainForm);
formBttn = INMPLANT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

