 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMRWU_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='deptItem'][.='Bills of Material']").Click();
new Control("Bills of Material Reports/Inquiries", "xpath","//div[@class='navItem'][.='Bills of Material Reports/Inquiries']").Click();
new Control("Print Bills of Material Where-Used Report", "xpath","//div[@class='navItem'][.='Print Bills of Material Where-Used Report']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "BMRWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRWU] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMRWU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMRWU_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMRWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRWU] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control BMRWU_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BMRWU_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BMRWU";
							CPCommon.WaitControlDisplayed(BMRWU_MainForm);
IWebElement formBttn = BMRWU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMRWU_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMRWU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMRWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMRWU] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BMRWU_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMRWU_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close the app");


												
				CPCommon.CurrentComponent = "BMRWU";
							CPCommon.WaitControlDisplayed(BMRWU_MainForm);
formBttn = BMRWU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

