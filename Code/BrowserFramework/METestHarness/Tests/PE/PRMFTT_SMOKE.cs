 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PRMFTT_SMOKE : TestScript
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
new Control("Payroll", "xpath","//div[@class='deptItem'][.='Payroll']").Click();
new Control("Federal Taxes", "xpath","//div[@class='navItem'][.='Federal Taxes']").Click();
new Control("Manage Federal Tax Tables", "xpath","//div[@class='navItem'][.='Manage Federal Tax Tables']").Click();


											Driver.SessionLogger.WriteLine("Checking the App");


												
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PRMFTT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PRMFTT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming VerifyExists on EffectiveDate...", Logger.MessageType.INF);
			Control PRMFTT_EffectiveDate = new Control("EffectiveDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EFFECT_DT']");
			CPCommon.AssertEqual(true,PRMFTT_EffectiveDate.Exists());

												
				CPCommon.CurrentComponent = "PRMFTT";
							CPCommon.WaitControlDisplayed(PRMFTT_MainForm);
IWebElement formBttn = PRMFTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PRMFTT_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PRMFTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PRMFTT_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMFTT_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming VerifyExists on FederalTaxTableLink...", Logger.MessageType.INF);
			Control PRMFTT_FederalTaxTableLink = new Control("FederalTaxTableLink", "ID", "lnk_3977_PRMFTT_FEDTAXTBL_HDR");
			CPCommon.AssertEqual(true,PRMFTT_FederalTaxTableLink.Exists());

												
				CPCommon.CurrentComponent = "PRMFTT";
							CPCommon.WaitControlDisplayed(PRMFTT_FederalTaxTableLink);
PRMFTT_FederalTaxTableLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming VerifyExists on FederalTaxTableForm...", Logger.MessageType.INF);
			Control PRMFTT_FederalTaxTableForm = new Control("FederalTaxTableForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFTT_FEDTAXTBL_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PRMFTT_FederalTaxTableForm.Exists());

												
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming VerifyExist on FederalTaxTableFormTable...", Logger.MessageType.INF);
			Control PRMFTT_FederalTaxTableFormTable = new Control("FederalTaxTableFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFTT_FEDTAXTBL_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PRMFTT_FederalTaxTableFormTable.Exists());

												
				CPCommon.CurrentComponent = "PRMFTT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PRMFTT] Perfoming Click on FederalTaxTable_Ok...", Logger.MessageType.INF);
			Control PRMFTT_FederalTaxTable_Ok = new Control("FederalTaxTable_Ok", "xpath", "//div[translate(@id,'0123456789','')='pr__PRMFTT_FEDTAXTBL_CTW_']/ancestor::form[1]/following-sibling::div[1]/descendant::*[@id='bOk']");
			CPCommon.WaitControlDisplayed(PRMFTT_FederalTaxTable_Ok);
if (PRMFTT_FederalTaxTable_Ok.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PRMFTT_FederalTaxTable_Ok.Click(5,5);
else PRMFTT_FederalTaxTable_Ok.Click(4.5);


											Driver.SessionLogger.WriteLine("Closing the App");


												
				CPCommon.CurrentComponent = "PRMFTT";
							CPCommon.WaitControlDisplayed(PRMFTT_MainForm);
formBttn = PRMFTT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

