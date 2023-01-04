 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMRDOC_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Documents", "xpath","//div[@class='navItem'][.='Documents']").Click();
new Control("Release Documents", "xpath","//div[@class='navItem'][.='Release Documents']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMMRDOC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMMRDOC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on DocumentID...", Logger.MessageType.INF);
			Control BMMRDOC_DocumentID = new Control("DocumentID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,BMMRDOC_DocumentID.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BMMRDOC_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMMRDOC_MainTab);
IWebElement mTab = BMMRDOC_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Document Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on DocumentDetails_Status...", Logger.MessageType.INF);
			Control BMMRDOC_DocumentDetails_Status = new Control("DocumentDetails_Status", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_STATUS_TYPE']");
			CPCommon.AssertEqual(true,BMMRDOC_DocumentDetails_Status.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_MainTab);
mTab = BMMRDOC_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Locations").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on Locations_Location...", Logger.MessageType.INF);
			Control BMMRDOC_Locations_Location = new Control("Locations_Location", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DOC_LOCATION']");
			CPCommon.AssertEqual(true,BMMRDOC_Locations_Location.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_MainTab);
mTab = BMMRDOC_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer/Project Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on CustomerPROJECTInformation_CustomerInformation_Customer...", Logger.MessageType.INF);
			Control BMMRDOC_CustomerPROJECTInformation_CustomerInformation_Customer = new Control("CustomerPROJECTInformation_CustomerInformation_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BMMRDOC_CustomerPROJECTInformation_CustomerInformation_Customer.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_MainForm);
IWebElement formBttn = BMMRDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMRDOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMRDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMMRDOC_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMRDOC_MainTable.Exists());

											Driver.SessionLogger.WriteLine("PARTS");


												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on PartsLink...", Logger.MessageType.INF);
			Control BMMRDOC_PartsLink = new Control("PartsLink", "ID", "lnk_1006789_MMMMDOC_DOCUMENT_DOCUMENT");
			CPCommon.AssertEqual(true,BMMRDOC_PartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_PartsLink);
BMMRDOC_PartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExist on PartsTable...", Logger.MessageType.INF);
			Control BMMRDOC_PartsTable = new Control("PartsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_PARTDOCUMENT_PARTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMRDOC_PartsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming ClickButton on PartsForm...", Logger.MessageType.INF);
			Control BMMRDOC_PartsForm = new Control("PartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_PARTDOCUMENT_PARTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMRDOC_PartsForm);
formBttn = BMMRDOC_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMRDOC_PartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMRDOC_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.AssertEqual(true,BMMRDOC_PartsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on Parts_Part...", Logger.MessageType.INF);
			Control BMMRDOC_Parts_Part = new Control("Parts_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_PARTDOCUMENT_PARTS_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMMRDOC_Parts_Part.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_PartsForm);
formBttn = BMMRDOC_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TEXT");


												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on TextLink...", Logger.MessageType.INF);
			Control BMMRDOC_TextLink = new Control("TextLink", "ID", "lnk_1001726_MMMMDOC_DOCUMENT_DOCUMENT");
			CPCommon.AssertEqual(true,BMMRDOC_TextLink.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_TextLink);
BMMRDOC_TextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExist on TextTable...", Logger.MessageType.INF);
			Control BMMRDOC_TextTable = new Control("TextTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_DOCUMENTTEXT_TEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMRDOC_TextTable.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMRDOC] Perfoming VerifyExists on TextForm...", Logger.MessageType.INF);
			Control BMMRDOC_TextForm = new Control("TextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_DOCUMENTTEXT_TEXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMRDOC_TextForm.Exists());

												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_TextForm);
formBttn = BMMRDOC_TextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BMMRDOC";
							CPCommon.WaitControlDisplayed(BMMRDOC_MainForm);
formBttn = BMMRDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

