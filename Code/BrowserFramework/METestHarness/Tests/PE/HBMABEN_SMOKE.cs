 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HBMABEN_SMOKE : TestScript
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
new Control("Employee Benefit Information", "xpath","//div[@class='navItem'][.='Employee Benefit Information']").Click();
new Control("Assign Beneficiaries to Benefit Plans", "xpath","//div[@class='navItem'][.='Assign Beneficiaries to Benefit Plans']").Click();


											Driver.SessionLogger.WriteLine("Checking the App");


												
				CPCommon.CurrentComponent = "HBMABEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMABEN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HBMABEN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HBMABEN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HBMABEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMABEN] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HBMABEN_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HBMABEN_Employee.Exists());

												
				CPCommon.CurrentComponent = "HBMABEN";
							CPCommon.WaitControlDisplayed(HBMABEN_MainForm);
IWebElement formBttn = HBMABEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HBMABEN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HBMABEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HBMABEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMABEN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HBMABEN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMABEN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HBMABEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMABEN] Perfoming VerifyExist on BeneficiaryDetailsTable...", Logger.MessageType.INF);
			Control HBMABEN_BeneficiaryDetailsTable = new Control("BeneficiaryDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMABEN_HBEMPLLIFEBNFIC_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HBMABEN_BeneficiaryDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "HBMABEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMABEN] Perfoming ClickButton on BeneficiaryDetailsForm...", Logger.MessageType.INF);
			Control HBMABEN_BeneficiaryDetailsForm = new Control("BeneficiaryDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMABEN_HBEMPLLIFEBNFIC_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HBMABEN_BeneficiaryDetailsForm);
formBttn = HBMABEN_BeneficiaryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HBMABEN_BeneficiaryDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HBMABEN_BeneficiaryDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HBMABEN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HBMABEN] Perfoming VerifyExists on BeneficiaryDetails_BeneficiaryName...", Logger.MessageType.INF);
			Control HBMABEN_BeneficiaryDetails_BeneficiaryName = new Control("BeneficiaryDetails_BeneficiaryName", "xpath", "//div[translate(@id,'0123456789','')='pr__HBMABEN_HBEMPLLIFEBNFIC_']/ancestor::form[1]/descendant::*[@id='LAST_FIRST_NAME']");
			CPCommon.AssertEqual(true,HBMABEN_BeneficiaryDetails_BeneficiaryName.Exists());

											Driver.SessionLogger.WriteLine("Closing the App");


												
				CPCommon.CurrentComponent = "HBMABEN";
							CPCommon.WaitControlDisplayed(HBMABEN_MainForm);
formBttn = HBMABEN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

