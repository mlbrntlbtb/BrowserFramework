 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJM533FM_SMOKE : TestScript
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
new Control("NASA 533s", "xpath","//div[@class='deptItem'][.='NASA 533s']").Click();
new Control("NASA 533 Processing", "xpath","//div[@class='navItem'][.='NASA 533 Processing']").Click();
new Control("Manage NASA 533 Formats", "xpath","//div[@class='navItem'][.='Manage NASA 533 Formats']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJM533FM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJM533FM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming VerifyExists on Format...", Logger.MessageType.INF);
			Control PJM533FM_Format = new Control("Format", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='NASA_FRMT_CD']");
			CPCommon.AssertEqual(true,PJM533FM_Format.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming VerifyExists on LineDetailsLink...", Logger.MessageType.INF);
			Control PJM533FM_LineDetailsLink = new Control("LineDetailsLink", "ID", "lnk_1001151_PJM533FM_NASAFRMT_HDR");
			CPCommon.AssertEqual(true,PJM533FM_LineDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJM533FM";
							CPCommon.WaitControlDisplayed(PJM533FM_LineDetailsLink);
PJM533FM_LineDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming VerifyExist on LineDetailsFormTable...", Logger.MessageType.INF);
			Control PJM533FM_LineDetailsFormTable = new Control("LineDetailsFormTable", "xpath", "//div[starts-with(@id,'pr__PJM533FM_NASAFRMT_CHLD_')]/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJM533FM_LineDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming ClickButton on LineDetailsForm...", Logger.MessageType.INF);
			Control PJM533FM_LineDetailsForm = new Control("LineDetailsForm", "xpath", "//div[starts-with(@id,'pr__PJM533FM_NASAFRMT_CHLD_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJM533FM_LineDetailsForm);
IWebElement formBttn = PJM533FM_LineDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJM533FM_LineDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJM533FM_LineDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJM533FM";
							CPCommon.AssertEqual(true,PJM533FM_LineDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming VerifyExists on LineDetails_LineType...", Logger.MessageType.INF);
			Control PJM533FM_LineDetails_LineType = new Control("LineDetails_LineType", "xpath", "//div[starts-with(@id,'pr__PJM533FM_NASAFRMT_CHLD_')]/ancestor::form[1]/descendant::*[@id='S_NASA_FM_LN_TYPE']");
			CPCommon.AssertEqual(true,PJM533FM_LineDetails_LineType.Exists());

											Driver.SessionLogger.WriteLine("MAINFOrMTABLE");


												
				CPCommon.CurrentComponent = "PJM533FM";
							CPCommon.WaitControlDisplayed(PJM533FM_MainForm);
formBttn = PJM533FM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJM533FM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJM533FM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJM533FM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJM533FM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJM533FM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJM533FM_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJM533FM";
							CPCommon.WaitControlDisplayed(PJM533FM_MainForm);
formBttn = PJM533FM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

