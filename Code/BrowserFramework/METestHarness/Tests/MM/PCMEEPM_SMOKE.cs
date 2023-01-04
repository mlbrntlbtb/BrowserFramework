 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PCMEEPM_SMOKE : TestScript
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
new Control("Production Control Controls", "xpath","//div[@class='navItem'][.='Production Control Controls']").Click();
new Control("Manage Employee Project Manufacturing Status", "xpath","//div[@class='navItem'][.='Manage Employee Project Manufacturing Status']").Click();


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Close...", Logger.MessageType.INF);
			Control Query_Close = new Control("Close", "ID", "closeQ");
			CPCommon.WaitControlDisplayed(Query_Close);
if (Query_Close.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Close.Click(5,5);
else Query_Close.Click(4.5);


												
				CPCommon.CurrentComponent = "PCMEEPM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMEEPM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PCMEEPM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PCMEEPM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PCMEEPM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMEEPM] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control PCMEEPM_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,PCMEEPM_Employee.Exists());

												
				CPCommon.CurrentComponent = "PCMEEPM";
							CPCommon.WaitControlDisplayed(PCMEEPM_MainForm);
IWebElement formBttn = PCMEEPM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PCMEEPM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PCMEEPM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PCMEEPM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PCMEEPM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PCMEEPM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PCMEEPM_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PCMEEPM";
							CPCommon.WaitControlDisplayed(PCMEEPM_MainForm);
formBttn = PCMEEPM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

