 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAQTAL_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Fixed Assets Utilities", "xpath","//div[@class='navItem'][.='Fixed Assets Utilities']").Click();
new Control("View Template Change History", "xpath","//div[@class='navItem'][.='View Template Change History']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "FAQTAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQTAL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAQTAL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAQTAL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAQTAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQTAL] Perfoming VerifyExists on Template...", Logger.MessageType.INF);
			Control FAQTAL_Template = new Control("Template", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FA_TMPLT_ID']");
			CPCommon.AssertEqual(true,FAQTAL_Template.Exists());

												
				CPCommon.CurrentComponent = "FAQTAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQTAL] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control FAQTAL_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQTAL_FATMPLTAUDITLOG_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,FAQTAL_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "FAQTAL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAQTAL] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control FAQTAL_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__FAQTAL_FATMPLTAUDITLOG_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAQTAL_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAQTAL";
							CPCommon.AssertEqual(true,FAQTAL_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "FAQTAL";
							CPCommon.WaitControlDisplayed(FAQTAL_ChildForm);
IWebElement formBttn = FAQTAL_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAQTAL_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAQTAL_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAQTAL";
							CPCommon.AssertEqual(true,FAQTAL_Template.Exists());

													
				CPCommon.CurrentComponent = "FAQTAL";
							CPCommon.WaitControlDisplayed(FAQTAL_MainForm);
formBttn = FAQTAL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

