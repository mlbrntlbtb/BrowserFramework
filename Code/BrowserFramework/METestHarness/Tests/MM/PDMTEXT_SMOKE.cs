 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMTEXT_SMOKE : TestScript
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
new Control("Manage Standard Text", "xpath","//div[@class='navItem'][.='Manage Standard Text']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDMTEXT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDMTEXT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExists on StandardTextCode...", Logger.MessageType.INF);
			Control PDMTEXT_StandardTextCode = new Control("StandardTextCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TEXT_CD']");
			CPCommon.AssertEqual(true,PDMTEXT_StandardTextCode.Exists());

												
				CPCommon.CurrentComponent = "PDMTEXT";
							CPCommon.WaitControlDisplayed(PDMTEXT_MainForm);
IWebElement formBttn = PDMTEXT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDMTEXT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDMTEXT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMTEXT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMTEXT_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Where-Used");


												
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExists on WhereUsedLink...", Logger.MessageType.INF);
			Control PDMTEXT_WhereUsedLink = new Control("WhereUsedLink", "ID", "lnk_1002174_PDMTEXT_STD_TEXT_HDR");
			CPCommon.AssertEqual(true,PDMTEXT_WhereUsedLink.Exists());

												
				CPCommon.CurrentComponent = "PDMTEXT";
							CPCommon.WaitControlDisplayed(PDMTEXT_WhereUsedLink);
PDMTEXT_WhereUsedLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExist on WhereUsedCodeFormTable...", Logger.MessageType.INF);
			Control PDMTEXT_WhereUsedCodeFormTable = new Control("WhereUsedCodeFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMTEXT_TEXTWHEREUSED_SELWHER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMTEXT_WhereUsedCodeFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExists on WhereUsedCodeForm...", Logger.MessageType.INF);
			Control PDMTEXT_WhereUsedCodeForm = new Control("WhereUsedCodeForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMTEXT_TEXTWHEREUSED_SELWHER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMTEXT_WhereUsedCodeForm.Exists());

												
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExist on WhereUsedDescriptionFormTable...", Logger.MessageType.INF);
			Control PDMTEXT_WhereUsedDescriptionFormTable = new Control("WhereUsedDescriptionFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMTEXT_SWHEREUSED_WHEREUSED_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMTEXT_WhereUsedDescriptionFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMTEXT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMTEXT] Perfoming VerifyExists on WhereUsedDescriptionForm...", Logger.MessageType.INF);
			Control PDMTEXT_WhereUsedDescriptionForm = new Control("WhereUsedDescriptionForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMTEXT_SWHEREUSED_WHEREUSED_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDMTEXT_WhereUsedDescriptionForm.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDMTEXT";
							CPCommon.WaitControlDisplayed(PDMTEXT_MainForm);
formBttn = PDMTEXT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

