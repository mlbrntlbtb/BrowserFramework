 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMMRELS_SMOKE : TestScript
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
new Control("Bills of Material", "xpath","//div[@class='navItem'][.='Bills of Material']").Click();
new Control("Release Multiple Manufacturing Bills of Material", "xpath","//div[@class='navItem'][.='Release Multiple Manufacturing Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMMMRELS_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMMRELS_MainTable.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control BMMMRELS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(BMMMRELS_MainForm);
IWebElement formBttn = BMMMRELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMMRELS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMMRELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.AssertEqual(true,BMMMRELS_MainForm.Exists());

													
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExists on AssemblyPart_AssemblyPart...", Logger.MessageType.INF);
			Control BMMMRELS_AssemblyPart_AssemblyPart = new Control("AssemblyPart_AssemblyPart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMMMRELS_AssemblyPart_AssemblyPart.Exists());

											Driver.SessionLogger.WriteLine("COMPONENTS ");


												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExists on ComponentsLink...", Logger.MessageType.INF);
			Control BMMMRELS_ComponentsLink = new Control("ComponentsLink", "ID", "lnk_1002193_BMMMRELS_HDR");
			CPCommon.AssertEqual(true,BMMMRELS_ComponentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.WaitControlDisplayed(BMMMRELS_ComponentsLink);
BMMMRELS_ComponentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExist on ComponentsTable...", Logger.MessageType.INF);
			Control BMMMRELS_ComponentsTable = new Control("ComponentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMRELS_COMPONENTS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMMRELS_ComponentsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming ClickButton on ComponentsForm...", Logger.MessageType.INF);
			Control BMMMRELS_ComponentsForm = new Control("ComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMRELS_COMPONENTS_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMMRELS_ComponentsForm);
formBttn = BMMMRELS_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMMRELS_ComponentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMMRELS_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.AssertEqual(true,BMMMRELS_ComponentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExists on Components_LineNo...", Logger.MessageType.INF);
			Control BMMMRELS_Components_LineNo = new Control("Components_LineNo", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMRELS_COMPONENTS_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,BMMMRELS_Components_LineNo.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming Select on ComponentsTab...", Logger.MessageType.INF);
			Control BMMMRELS_ComponentsTab = new Control("ComponentsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMRELS_COMPONENTS_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMMMRELS_ComponentsTab);
IWebElement mTab = BMMMRELS_ComponentsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExists on Components_ComponentInformation_Component_PartStatus...", Logger.MessageType.INF);
			Control BMMMRELS_Components_ComponentInformation_Component_PartStatus = new Control("Components_ComponentInformation_Component_PartStatus", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMMRELS_COMPONENTS_']/ancestor::form[1]/descendant::*[@id='S_STATUS_DESC']");
			CPCommon.AssertEqual(true,BMMMRELS_Components_ComponentInformation_Component_PartStatus.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.WaitControlDisplayed(BMMMRELS_ComponentsTab);
mTab = BMMMRELS_ComponentsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.WaitControlDisplayed(BMMMRELS_ComponentsForm);
formBttn = BMMMRELS_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("PART DOCUMENTS");


												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExists on PartDocumentsLink...", Logger.MessageType.INF);
			Control BMMMRELS_PartDocumentsLink = new Control("PartDocumentsLink", "ID", "lnk_1002194_BMMMRELS_HDR");
			CPCommon.AssertEqual(true,BMMMRELS_PartDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.WaitControlDisplayed(BMMMRELS_PartDocumentsLink);
BMMMRELS_PartDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExist on PartDocumentsTable...", Logger.MessageType.INF);
			Control BMMMRELS_PartDocumentsTable = new Control("PartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMMRELS_PartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming ClickButton on PartDocumentsForm...", Logger.MessageType.INF);
			Control BMMMRELS_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BMMMRELS_PartDocumentsForm);
formBttn = BMMMRELS_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMMRELS_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMMRELS_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.AssertEqual(true,BMMMRELS_PartDocumentsForm.Exists());

													
				CPCommon.CurrentComponent = "BMMMRELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMMRELS] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control BMMMRELS_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMMMRELS_PartDocuments_Type.Exists());

												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.WaitControlDisplayed(BMMMRELS_PartDocumentsForm);
formBttn = BMMMRELS_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APPLICATION");


												
				CPCommon.CurrentComponent = "BMMMRELS";
							CPCommon.WaitControlDisplayed(BMMMRELS_MainForm);
formBttn = BMMMRELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

