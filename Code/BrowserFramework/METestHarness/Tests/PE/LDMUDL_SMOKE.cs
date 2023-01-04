 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMUDL_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Employee Controls", "xpath","//div[@class='navItem'][.='Employee Controls']").Click();
new Control("Manage Employee User-Defined Labels", "xpath","//div[@class='navItem'][.='Manage Employee User-Defined Labels']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUDL] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMUDL_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMUDL_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUDL] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control LDMUDL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(LDMUDL_MainForm);
IWebElement formBttn = LDMUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMUDL_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "LDMUDL";
							CPCommon.AssertEqual(true,LDMUDL_MainForm.Exists());

													
				CPCommon.CurrentComponent = "LDMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUDL] Perfoming VerifyExists on SequenceNumber...", Logger.MessageType.INF);
			Control LDMUDL_SequenceNumber = new Control("SequenceNumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,LDMUDL_SequenceNumber.Exists());

												
				CPCommon.CurrentComponent = "LDMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUDL] Perfoming VerifyExists on ValidatedTextLink...", Logger.MessageType.INF);
			Control LDMUDL_ValidatedTextLink = new Control("ValidatedTextLink", "ID", "lnk_1002827_CPMUDLAB_UDEFLBL_USERDEFLABELS");
			CPCommon.AssertEqual(true,LDMUDL_ValidatedTextLink.Exists());

												
				CPCommon.CurrentComponent = "LDMUDL";
							CPCommon.WaitControlDisplayed(LDMUDL_ValidatedTextLink);
LDMUDL_ValidatedTextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMUDL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUDL] Perfoming VerifyExist on LabelInfo_ValidatedTextFormTable...", Logger.MessageType.INF);
			Control LDMUDL_LabelInfo_ValidatedTextFormTable = new Control("LabelInfo_ValidatedTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPMUDLAB_UDEFVALIDVALUES_VALID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMUDL_LabelInfo_ValidatedTextFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "LDMUDL";
							CPCommon.WaitControlDisplayed(LDMUDL_MainForm);
formBttn = LDMUDL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

