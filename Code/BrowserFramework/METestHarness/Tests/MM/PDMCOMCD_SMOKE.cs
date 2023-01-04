 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMCOMCD_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Product Definition Controls", "xpath","//div[@class='navItem'][.='Product Definition Controls']").Click();
new Control("Manage Commodity Codes", "xpath","//div[@class='navItem'][.='Manage Commodity Codes']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMCOMCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMCOMCD] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMCOMCD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMCOMCD_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMCOMCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMCOMCD] Perfoming VerifyExists on CommodityCode...", Logger.MessageType.INF);
			Control PDMCOMCD_CommodityCode = new Control("CommodityCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMM_CD']");
			CPCommon.AssertEqual(true,PDMCOMCD_CommodityCode.Exists());

												
				CPCommon.CurrentComponent = "PDMCOMCD";
							CPCommon.WaitControlDisplayed(PDMCOMCD_MainForm);
IWebElement formBttn = PDMCOMCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMCOMCD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMCOMCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMCOMCD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMCOMCD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMCOMCD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMCOMCD_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDMCOMCD";
							CPCommon.WaitControlDisplayed(PDMCOMCD_MainForm);
formBttn = PDMCOMCD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

