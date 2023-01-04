 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PPMPOHDR_SMOKE : TestScript
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
new Control("Purchase Requisitions", "xpath","//div[@class='navItem'][.='Purchase Requisitions']").Click();
new Control("Manage Purchase Order Header Information", "xpath","//div[@class='navItem'][.='Manage Purchase Order Header Information']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PPMPOHDR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PPMPOHDR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExists on PlannedPO...", Logger.MessageType.INF);
			Control PPMPOHDR_PlannedPO = new Control("PlannedPO", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SETUP_PO_ID']");
			CPCommon.AssertEqual(true,PPMPOHDR_PlannedPO.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control PPMPOHDR_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PPMPOHDR_MainTab);
IWebElement mTab = PPMPOHDR_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExists on Details_OrderDate...", Logger.MessageType.INF);
			Control PPMPOHDR_Details_OrderDate = new Control("Details_OrderDate", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ORD_DT']");
			CPCommon.AssertEqual(true,PPMPOHDR_Details_OrderDate.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
							CPCommon.WaitControlDisplayed(PPMPOHDR_MainTab);
mTab = PPMPOHDR_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Address").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExists on Address_VendorAddress_AddressCode...", Logger.MessageType.INF);
			Control PPMPOHDR_Address_VendorAddress_AddressCode = new Control("Address_VendorAddress_AddressCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ADDR_DC']");
			CPCommon.AssertEqual(true,PPMPOHDR_Address_VendorAddress_AddressCode.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
							CPCommon.WaitControlDisplayed(PPMPOHDR_MainForm);
IWebElement formBttn = PPMPOHDR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PPMPOHDR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PPMPOHDR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PPMPOHDR_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMPOHDR_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
							CPCommon.WaitControlDisplayed(PPMPOHDR_MainForm);
formBttn = PPMPOHDR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PPMPOHDR_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PPMPOHDR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming Click on HeaderStandardTextLink...", Logger.MessageType.INF);
			Control PPMPOHDR_HeaderStandardTextLink = new Control("HeaderStandardTextLink", "ID", "lnk_1003223_PPMPOHDR_POSETUPHDR_HDR");
			CPCommon.WaitControlDisplayed(PPMPOHDR_HeaderStandardTextLink);
PPMPOHDR_HeaderStandardTextLink.Click(1.5);


											Driver.SessionLogger.WriteLine("HeaderStandardTextForm");


												
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExists on HeaderStandardTextForm...", Logger.MessageType.INF);
			Control PPMPOHDR_HeaderStandardTextForm = new Control("HeaderStandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMPOHDR_POSETUPTEXT_HEADER_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PPMPOHDR_HeaderStandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PPMPOHDR] Perfoming VerifyExist on HeaderStandardTextTable...", Logger.MessageType.INF);
			Control PPMPOHDR_HeaderStandardTextTable = new Control("HeaderStandardTextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PPMPOHDR_POSETUPTEXT_HEADER_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PPMPOHDR_HeaderStandardTextTable.Exists());

												
				CPCommon.CurrentComponent = "PPMPOHDR";
							CPCommon.WaitControlDisplayed(PPMPOHDR_HeaderStandardTextForm);
formBttn = PPMPOHDR_HeaderStandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PPMPOHDR";
							CPCommon.WaitControlDisplayed(PPMPOHDR_MainForm);
formBttn = PPMPOHDR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

