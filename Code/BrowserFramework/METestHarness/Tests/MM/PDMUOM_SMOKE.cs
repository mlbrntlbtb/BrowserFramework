 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMUOM_SMOKE : TestScript
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
new Control("Manage Units of Measure", "xpath","//div[@class='navItem'][.='Manage Units of Measure']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMUOM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMUOM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMUOM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMUOM_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMUOM";
							CPCommon.WaitControlDisplayed(PDMUOM_MainForm);
IWebElement formBttn = PDMUOM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMUOM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMUOM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExists on Description...", Logger.MessageType.INF);
			Control PDMUOM_Description = new Control("Description", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UM_DESC']");
			CPCommon.AssertEqual(true,PDMUOM_Description.Exists());

												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming Click on ConversionLink...", Logger.MessageType.INF);
			Control PDMUOM_ConversionLink = new Control("ConversionLink", "ID", "lnk_3498_PDMUOM_UM");
			CPCommon.WaitControlDisplayed(PDMUOM_ConversionLink);
PDMUOM_ConversionLink.Click(1.5);


											Driver.SessionLogger.WriteLine("ConversionForm");


												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExists on ConversionForm...", Logger.MessageType.INF);
			Control PDMUOM_ConversionForm = new Control("ConversionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMUOM_UMCONV_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMUOM_ConversionForm.Exists());

												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExist on ConversionFormTable...", Logger.MessageType.INF);
			Control PDMUOM_ConversionFormTable = new Control("ConversionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMUOM_UMCONV_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMUOM_ConversionFormTable.Exists());

											Driver.SessionLogger.WriteLine("ToUnitOfMeasureForm");


												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExists on ToUnitOfMeasureForm...", Logger.MessageType.INF);
			Control PDMUOM_ToUnitOfMeasureForm = new Control("ToUnitOfMeasureForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMUOM_UM_AVAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMUOM_ToUnitOfMeasureForm.Exists());

												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming VerifyExist on ToUnitOfMeasureFormTable...", Logger.MessageType.INF);
			Control PDMUOM_ToUnitOfMeasureFormTable = new Control("ToUnitOfMeasureFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMUOM_UM_AVAIL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMUOM_ToUnitOfMeasureFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMUOM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMUOM] Perfoming Click on ToUnitOfMeasure_Ok...", Logger.MessageType.INF);
			Control PDMUOM_ToUnitOfMeasure_Ok = new Control("ToUnitOfMeasure_Ok", "xpath", "//div[@class='app']/div[@id='0']/following::span[@class='layerSpan' and contains(@style,'block')]/descendant::input[@id='bOk']");
			CPCommon.WaitControlDisplayed(PDMUOM_ToUnitOfMeasure_Ok);
if (PDMUOM_ToUnitOfMeasure_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PDMUOM_ToUnitOfMeasure_Ok.Click(5,5);
else PDMUOM_ToUnitOfMeasure_Ok.Click(4.5);


											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDMUOM";
							CPCommon.WaitControlDisplayed(PDMUOM_MainForm);
formBttn = PDMUOM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

