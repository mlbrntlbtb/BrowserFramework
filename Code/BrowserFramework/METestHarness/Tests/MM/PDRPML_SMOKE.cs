 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDRPML_SMOKE : TestScript
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
new Control("Product Definition Reports/Inquiries", "xpath","//div[@class='navItem'][.='Product Definition Reports/Inquiries']").Click();
new Control("Print Item Master Listing Report", "xpath","//div[@class='navItem'][.='Print Item Master Listing Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PDRPML";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDRPML] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDRPML_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDRPML_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDRPML";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDRPML] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PDRPML_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PDRPML_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PDRPML";
							CPCommon.WaitControlDisplayed(PDRPML_MainForm);
IWebElement formBttn = PDRPML_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDRPML_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDRPML_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDRPML";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDRPML] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDRPML_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDRPML_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDRPML";
							CPCommon.WaitControlDisplayed(PDRPML_MainForm);
formBttn = PDRPML_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

