 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMEXPN_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchase Orders", "xpath","//div[@class='navItem'][.='Purchase Orders']").Click();
new Control("Manage Purchase Order Expediting Notes", "xpath","//div[@class='navItem'][.='Manage Purchase Order Expediting Notes']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "POMEXPN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMEXPN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMEXPN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMEXPN";
							CPCommon.WaitControlDisplayed(POMEXPN_MainForm);
IWebElement formBttn = POMEXPN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMEXPN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMEXPN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POMEXPN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMEXPN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMEXPN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMEXPN";
							CPCommon.WaitControlDisplayed(POMEXPN_MainForm);
formBttn = POMEXPN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMEXPN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMEXPN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMEXPN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMEXPN] Perfoming VerifyExists on Identification_PO...", Logger.MessageType.INF);
			Control POMEXPN_Identification_PO = new Control("Identification_PO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POMEXPN_Identification_PO.Exists());

												
				CPCommon.CurrentComponent = "POMEXPN";
							CPCommon.WaitControlDisplayed(POMEXPN_MainForm);
formBttn = POMEXPN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

