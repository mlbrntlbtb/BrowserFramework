 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCPMRR_SMOKE : TestScript
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
new Control("Materials Planning", "xpath","//div[@class='navItem'][.='Materials Planning']").Click();
new Control("Compute Material Requirements", "xpath","//div[@class='navItem'][.='Compute Material Requirements']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCPMRR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCPMRR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PCPMRR_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PCPMRR_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PCPMRR";
							CPCommon.WaitControlDisplayed(PCPMRR_MainForm);
IWebElement formBttn = PCPMRR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCPMRR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCPMRR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCPMRR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCPMRR_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("EndPartsForm");


												
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExists on EndPartsLink...", Logger.MessageType.INF);
			Control PCPMRR_EndPartsLink = new Control("EndPartsLink", "ID", "lnk_1004746_PCPMRR_PARAM");
			CPCommon.AssertEqual(true,PCPMRR_EndPartsLink.Exists());

												
				CPCommon.CurrentComponent = "PCPMRR";
							CPCommon.WaitControlDisplayed(PCPMRR_EndPartsLink);
PCPMRR_EndPartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExists on EndPartsForm...", Logger.MessageType.INF);
			Control PCPMRR_EndPartsForm = new Control("EndPartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PCPMRR_PARMSPCPMRR_ENDPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PCPMRR_EndPartsForm.Exists());

												
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExists on EndParts_Part...", Logger.MessageType.INF);
			Control PCPMRR_EndParts_Part = new Control("EndParts_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__PCPMRR_PARMSPCPMRR_ENDPART_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,PCPMRR_EndParts_Part.Exists());

												
				CPCommon.CurrentComponent = "PCPMRR";
							CPCommon.WaitControlDisplayed(PCPMRR_EndPartsForm);
formBttn = PCPMRR_EndPartsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCPMRR_EndPartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCPMRR_EndPartsForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCPMRR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCPMRR] Perfoming VerifyExist on EndPartsFormTable...", Logger.MessageType.INF);
			Control PCPMRR_EndPartsFormTable = new Control("EndPartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PCPMRR_PARMSPCPMRR_ENDPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCPMRR_EndPartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PCPMRR";
							CPCommon.WaitControlDisplayed(PCPMRR_EndPartsForm);
formBttn = PCPMRR_EndPartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PCPMRR";
							CPCommon.WaitControlDisplayed(PCPMRR_MainForm);
formBttn = PCPMRR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

