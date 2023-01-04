 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BMMERELS_SMOKE : TestScript
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
new Control("Release Multiple Engineering Bills of Material", "xpath","//div[@class='navItem'][.='Release Multiple Engineering Bills of Material']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BMMERELS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BMMERELS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control BMMERELS_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMERELS_MainTable.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
							CPCommon.WaitControlDisplayed(BMMERELS_MainForm);
IWebElement formBttn = BMMERELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMERELS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMERELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on AssemblyPart...", Logger.MessageType.INF);
			Control BMMERELS_AssemblyPart = new Control("AssemblyPart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,BMMERELS_AssemblyPart.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on BillOfMaterialDetails_MakeBuy...", Logger.MessageType.INF);
			Control BMMERELS_BillOfMaterialDetails_MakeBuy = new Control("BillOfMaterialDetails_MakeBuy", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_MAKE_BUY_CD']");
			CPCommon.AssertEqual(true,BMMERELS_BillOfMaterialDetails_MakeBuy.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming Select on MainTab...", Logger.MessageType.INF);
			Control BMMERELS_MainTab = new Control("MainTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMMERELS_MainTab);
IWebElement mTab = BMMERELS_MainTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Assembly Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on AssemblyNotes_Notes...", Logger.MessageType.INF);
			Control BMMERELS_AssemblyNotes_Notes = new Control("AssemblyNotes_Notes", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EBOM_ASY_NOTES']");
			CPCommon.AssertEqual(true,BMMERELS_AssemblyNotes_Notes.Exists());

											Driver.SessionLogger.WriteLine("Components");


												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming Click on ComponentsLink...", Logger.MessageType.INF);
			Control BMMERELS_ComponentsLink = new Control("ComponentsLink", "ID", "lnk_1002223_BMMERELS_PART_HDR");
			CPCommon.WaitControlDisplayed(BMMERELS_ComponentsLink);
BMMERELS_ComponentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on ComponentsForm...", Logger.MessageType.INF);
			Control BMMERELS_ComponentsForm = new Control("ComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMERELS_ENGBOM_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMERELS_ComponentsForm.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExist on ComponentsTable...", Logger.MessageType.INF);
			Control BMMERELS_ComponentsTable = new Control("ComponentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMERELS_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMERELS_ComponentsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
							CPCommon.WaitControlDisplayed(BMMERELS_ComponentsForm);
formBttn = BMMERELS_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMERELS_ComponentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMERELS_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on Components_LineNo...", Logger.MessageType.INF);
			Control BMMERELS_Components_LineNo = new Control("Components_LineNo", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMERELS_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,BMMERELS_Components_LineNo.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on Components_ComponentInformatiom_Component_UM...", Logger.MessageType.INF);
			Control BMMERELS_Components_ComponentInformatiom_Component_UM = new Control("Components_ComponentInformatiom_Component_UM", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMERELS_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='UM']");
			CPCommon.AssertEqual(true,BMMERELS_Components_ComponentInformatiom_Component_UM.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming Select on ComponentsTab...", Logger.MessageType.INF);
			Control BMMERELS_ComponentsTab = new Control("ComponentsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__BMMERELS_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(BMMERELS_ComponentsTab);
mTab = BMMERELS_ComponentsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Line Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "BMMERELS";
							CPCommon.WaitControlDisplayed(BMMERELS_ComponentsForm);
formBttn = BMMERELS_ComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Part Documents");


												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming Click on PartDocumentsLink...", Logger.MessageType.INF);
			Control BMMERELS_PartDocumentsLink = new Control("PartDocumentsLink", "ID", "lnk_1002224_BMMERELS_PART_HDR");
			CPCommon.WaitControlDisplayed(BMMERELS_PartDocumentsLink);
BMMERELS_PartDocumentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on PartDocumentsForm...", Logger.MessageType.INF);
			Control BMMERELS_PartDocumentsForm = new Control("PartDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BMMERELS_PartDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExist on PartDocumentsTable...", Logger.MessageType.INF);
			Control BMMERELS_PartDocumentsTable = new Control("PartDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BMMERELS_PartDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "BMMERELS";
							CPCommon.WaitControlDisplayed(BMMERELS_PartDocumentsForm);
formBttn = BMMERELS_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BMMERELS_PartDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BMMERELS_PartDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "BMMERELS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BMMERELS] Perfoming VerifyExists on PartDocuments_Type...", Logger.MessageType.INF);
			Control BMMERELS_PartDocuments_Type = new Control("PartDocuments_Type", "xpath", "//div[translate(@id,'0123456789','')='pr__DVGMMDOC_PARTDOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOC_TYPE_CD']");
			CPCommon.AssertEqual(true,BMMERELS_PartDocuments_Type.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "BMMERELS";
							CPCommon.WaitControlDisplayed(BMMERELS_MainForm);
formBttn = BMMERELS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

