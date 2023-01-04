 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMCHNG_SMOKE : TestScript
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
new Control("Create Purchase Order Change Orders", "xpath","//div[@class='navItem'][.='Create Purchase Order Change Orders']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "POMCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMCHNG] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control POMCHNG_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,POMCHNG_MainForm.Exists());

												
				CPCommon.CurrentComponent = "POMCHNG";
							CPCommon.WaitControlDisplayed(POMCHNG_MainForm);
IWebElement formBttn = POMCHNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMCHNG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMCHNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "POMCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMCHNG] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMCHNG_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMCHNG_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "POMCHNG";
							CPCommon.WaitControlDisplayed(POMCHNG_MainForm);
formBttn = POMCHNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMCHNG_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMCHNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMCHNG";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMCHNG] Perfoming VerifyExists on PO...", Logger.MessageType.INF);
			Control POMCHNG_PO = new Control("PO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PO_ID']");
			CPCommon.AssertEqual(true,POMCHNG_PO.Exists());

												
				CPCommon.CurrentComponent = "POMCHNG";
							CPCommon.WaitControlDisplayed(POMCHNG_MainForm);
formBttn = POMCHNG_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

