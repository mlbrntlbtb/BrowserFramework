 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMRQTYP_SMOKE : TestScript
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
new Control("Procurement Planning", "xpath","//div[@class='deptItem'][.='Procurement Planning']").Click();
new Control("Procurement Planning Controls", "xpath","//div[@class='navItem'][.='Procurement Planning Controls']").Click();
new Control("Manage Purchase Requisition Types", "xpath","//div[@class='navItem'][.='Manage Purchase Requisition Types']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPMRQTYP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQTYP] Perfoming VerifyExists on RequisitionTypeID...", Logger.MessageType.INF);
			Control PPMRQTYP_RequisitionTypeID = new Control("RequisitionTypeID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RQ_TYPE_ID']");
			CPCommon.AssertEqual(true,PPMRQTYP_RequisitionTypeID.Exists());

												
				CPCommon.CurrentComponent = "PPMRQTYP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQTYP] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control PPMRQTYP_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMRQTYP_MainTab);
IWebElement mTab = PPMRQTYP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Charge Number Rules and Defaults").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMRQTYP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQTYP] Perfoming VerifyExists on ChargeNumberRulesAndDefaults_ChargeNumbersAllocationsAllowed...", Logger.MessageType.INF);
			Control PPMRQTYP_ChargeNumberRulesAndDefaults_ChargeNumbersAllocationsAllowed = new Control("ChargeNumberRulesAndDefaults_ChargeNumbersAllocationsAllowed", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_ALL_ALLOC_CD']");
			CPCommon.AssertEqual(true,PPMRQTYP_ChargeNumberRulesAndDefaults_ChargeNumbersAllocationsAllowed.Exists());

												
				CPCommon.CurrentComponent = "PPMRQTYP";
							CPCommon.WaitControlDisplayed(PPMRQTYP_MainTab);
mTab = PPMRQTYP_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Rules and Defaults").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMRQTYP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQTYP] Perfoming VerifyExists on OtherRulesAndDefaults_RequisitionItemRulesAndDefaults_ItemEntryRule...", Logger.MessageType.INF);
			Control PPMRQTYP_OtherRulesAndDefaults_RequisitionItemRulesAndDefaults_ItemEntryRule = new Control("OtherRulesAndDefaults_RequisitionItemRulesAndDefaults_ItemEntryRule", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_ITEM_ENTR_CD']");
			CPCommon.AssertEqual(true,PPMRQTYP_OtherRulesAndDefaults_RequisitionItemRulesAndDefaults_ItemEntryRule.Exists());

												
				CPCommon.CurrentComponent = "PPMRQTYP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQTYP] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PPMRQTYP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PPMRQTYP_MainForm);
IWebElement formBttn = PPMRQTYP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMRQTYP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMRQTYP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "PPMRQTYP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMRQTYP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPMRQTYP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMRQTYP_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMRQTYP";
							CPCommon.WaitControlDisplayed(PPMRQTYP_MainForm);
formBttn = PPMRQTYP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMRQTYP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMRQTYP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMRQTYP";
							CPCommon.WaitControlDisplayed(PPMRQTYP_MainForm);
formBttn = PPMRQTYP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

