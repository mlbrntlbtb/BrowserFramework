 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMDOCTP_SMOKE : TestScript
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
new Control("Manage Document Types", "xpath","//div[@class='navItem'][.='Manage Document Types']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BMMDOCTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMDOCTP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMMDOCTP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMMDOCTP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMMDOCTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMDOCTP] Perfoming VerifyExists on TypeCode...", Logger.MessageType.INF);
			Control BMMDOCTP_TypeCode = new Control("TypeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMMDOCTP_TypeCode.Exists());

												
				CPCommon.CurrentComponent = "BMMDOCTP";
							CPCommon.WaitControlDisplayed(BMMDOCTP_MainForm);
IWebElement formBttn = BMMDOCTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMDOCTP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMDOCTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMDOCTP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMDOCTP] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMMDOCTP_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMDOCTP_MainTable.Exists());

												
				CPCommon.CurrentComponent = "BMMDOCTP";
							CPCommon.WaitControlDisplayed(BMMDOCTP_MainForm);
formBttn = BMMDOCTP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

