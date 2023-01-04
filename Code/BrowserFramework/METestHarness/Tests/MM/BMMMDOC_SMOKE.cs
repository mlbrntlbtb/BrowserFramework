 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMMDOC_SMOKE : TestScript
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
new Control("Manage Documents", "xpath","//div[@class='navItem'][.='Manage Documents']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMMMDOC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMMMDOC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_MainForm);
IWebElement formBttn = BMMMDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BMMMDOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BMMMDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BMMMDOC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMMDOC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_MainForm);
formBttn = BMMMDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMMDOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMMDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control BMMMDOC_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMMMDOC_MainFormTab);
IWebElement mTab = BMMMDOC_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Document Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on DocumentID...", Logger.MessageType.INF);
			Control BMMMDOC_DocumentID = new Control("DocumentID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,BMMMDOC_DocumentID.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_MainFormTab);
mTab = BMMMDOC_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Locations").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on Locations_Location...", Logger.MessageType.INF);
			Control BMMMDOC_Locations_Location = new Control("Locations_Location", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='DOC_LOCATION']");
			CPCommon.AssertEqual(true,BMMMDOC_Locations_Location.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_MainFormTab);
mTab = BMMMDOC_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Customer/Project Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on CustomerProjectInformation_CustomerInformation_Customer...", Logger.MessageType.INF);
			Control BMMMDOC_CustomerProjectInformation_CustomerInformation_Customer = new Control("CustomerProjectInformation_CustomerInformation_Customer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CUST_ID']");
			CPCommon.AssertEqual(true,BMMMDOC_CustomerProjectInformation_CustomerInformation_Customer.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_MainFormTab);
mTab = BMMMDOC_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Document Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												Driver.SessionLogger.WriteLine("Parts");


												
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on PartsLink...", Logger.MessageType.INF);
			Control BMMMDOC_PartsLink = new Control("PartsLink", "ID", "lnk_1006789_MMMMDOC_DOCUMENT_DOCUMENT");
			CPCommon.AssertEqual(true,BMMMDOC_PartsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_PartsLink);
BMMMDOC_PartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExist on PartsFormTable...", Logger.MessageType.INF);
			Control BMMMDOC_PartsFormTable = new Control("PartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_PARTDOCUMENT_PARTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMMDOC_PartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on PartsForm...", Logger.MessageType.INF);
			Control BMMMDOC_PartsForm = new Control("PartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_PARTDOCUMENT_PARTS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMMDOC_PartsForm.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_PartsForm);
formBttn = BMMMDOC_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMMDOC_PartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMMDOC_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on Parts_Part...", Logger.MessageType.INF);
			Control BMMMDOC_Parts_Part = new Control("Parts_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_PARTDOCUMENT_PARTS_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMMMDOC_Parts_Part.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_PartsForm);
formBttn = BMMMDOC_PartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Text");


												
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on TextLink...", Logger.MessageType.INF);
			Control BMMMDOC_TextLink = new Control("TextLink", "ID", "lnk_1001726_MMMMDOC_DOCUMENT_DOCUMENT");
			CPCommon.AssertEqual(true,BMMMDOC_TextLink.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_TextLink);
BMMMDOC_TextLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExist on TextFormTable...", Logger.MessageType.INF);
			Control BMMMDOC_TextFormTable = new Control("TextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_DOCUMENTTEXT_TEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMMDOC_TextFormTable.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMDOC] Perfoming VerifyExists on TextForm...", Logger.MessageType.INF);
			Control BMMMDOC_TextForm = new Control("TextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMMDOC_DOCUMENTTEXT_TEXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMMDOC_TextForm.Exists());

												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_TextForm);
formBttn = BMMMDOC_TextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close Main Form");


												
				CPCommon.CurrentComponent = "BMMMDOC";
							CPCommon.WaitControlDisplayed(BMMMDOC_MainForm);
formBttn = BMMMDOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

