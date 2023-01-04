 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMLABCL_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Routings Controls", "xpath","//div[@class='navItem'][.='Routings Controls']").Click();
new Control("Manage Labor Classifications", "xpath","//div[@class='navItem'][.='Manage Labor Classifications']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "RUMLABCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABCL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RUMLABCL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMLABCL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMLABCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABCL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control RUMLABCL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(RUMLABCL_MainForm);
IWebElement formBttn = RUMLABCL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMLABCL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMLABCL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "RUMLABCL";
							CPCommon.AssertEqual(true,RUMLABCL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "RUMLABCL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMLABCL] Perfoming VerifyExists on LaborClassificationCode...", Logger.MessageType.INF);
			Control RUMLABCL_LaborClassificationCode = new Control("LaborClassificationCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RU_LAB_CLASS_CD']");
			CPCommon.AssertEqual(true,RUMLABCL_LaborClassificationCode.Exists());

												
				CPCommon.CurrentComponent = "RUMLABCL";
							CPCommon.WaitControlDisplayed(RUMLABCL_MainForm);
formBttn = RUMLABCL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

