 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMUSAGE_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Units Usage Processing", "xpath","//div[@class='navItem'][.='Units Usage Processing']").Click();
new Control("Manage Unit Usage", "xpath","//div[@class='navItem'][.='Manage Unit Usage']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMUSAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMUSAGE] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMUSAGE_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMUSAGE_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMUSAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMUSAGE] Perfoming VerifyExists on UnitUsage_Identification_DocumentNumber...", Logger.MessageType.INF);
			Control BLMUSAGE_UnitUsage_Identification_DocumentNumber = new Control("UnitUsage_Identification_DocumentNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='USAGE_DOC_ID']");
			CPCommon.AssertEqual(true,BLMUSAGE_UnitUsage_Identification_DocumentNumber.Exists());

												
				CPCommon.CurrentComponent = "BLMUSAGE";
							CPCommon.WaitControlDisplayed(BLMUSAGE_MainForm);
IWebElement formBttn = BLMUSAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMUSAGE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMUSAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMUSAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMUSAGE] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BLMUSAGE_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMUSAGE_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CHILD FORM");


												
				CPCommon.CurrentComponent = "BLMUSAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMUSAGE] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control BLMUSAGE_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMUSAGE_UNITSUSAGELN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMUSAGE_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "BLMUSAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMUSAGE] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control BLMUSAGE_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMUSAGE_UNITSUSAGELN_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLMUSAGE_ChildForm);
formBttn = BLMUSAGE_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BLMUSAGE_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BLMUSAGE_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BLMUSAGE";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMUSAGE] Perfoming VerifyExists on UnitUsageDetails_Line...", Logger.MessageType.INF);
			Control BLMUSAGE_UnitUsageDetails_Line = new Control("UnitUsageDetails_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMUSAGE_UNITSUSAGELN_CHLD_']/ancestor::form[1]/descendant::*[@id='LINE_NO']");
			CPCommon.AssertEqual(true,BLMUSAGE_UnitUsageDetails_Line.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "BLMUSAGE";
							CPCommon.WaitControlDisplayed(BLMUSAGE_MainForm);
formBttn = BLMUSAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Count <= 0 ? BLMUSAGE_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Delete')]")).FirstOrDefault() :
BLMUSAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Delete not found ");


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Save.");
tlbrBtn.Click();


												
				CPCommon.CurrentComponent = "BLMUSAGE";
							CPCommon.WaitControlDisplayed(BLMUSAGE_MainForm);
formBttn = BLMUSAGE_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

