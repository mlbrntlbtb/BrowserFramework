 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class OEMSOUDI_SMOKE : TestScript
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
new Control("Sales Order Entry", "xpath","//div[@class='deptItem'][.='Sales Order Entry']").Click();
new Control("Sales Orders", "xpath","//div[@class='navItem'][.='Sales Orders']").Click();
new Control("Manage Sales Order User-Defined Information", "xpath","//div[@class='navItem'][.='Manage Sales Order User-Defined Information']").Click();


											Driver.SessionLogger.WriteLine("Query");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "OEMSOUDI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSOUDI] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control OEMSOUDI_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,OEMSOUDI_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "OEMSOUDI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSOUDI] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control OEMSOUDI_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(OEMSOUDI_MainForm);
IWebElement formBttn = OEMSOUDI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? OEMSOUDI_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
OEMSOUDI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "OEMSOUDI";
							CPCommon.AssertEqual(true,OEMSOUDI_MainForm.Exists());

													
				CPCommon.CurrentComponent = "OEMSOUDI";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[OEMSOUDI] Perfoming VerifyExists on SalesOrder...", Logger.MessageType.INF);
			Control OEMSOUDI_SalesOrder = new Control("SalesOrder", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SO_ID']");
			CPCommon.AssertEqual(true,OEMSOUDI_SalesOrder.Exists());

											Driver.SessionLogger.WriteLine("MainForm Close");


												
				CPCommon.CurrentComponent = "OEMSOUDI";
							CPCommon.WaitControlDisplayed(OEMSOUDI_MainForm);
formBttn = OEMSOUDI_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

