 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class RUMSUBOP_SMOKE : TestScript
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
new Control("Routings", "xpath","//div[@class='deptItem'][.='Routings']").Click();
new Control("Operations", "xpath","//div[@class='navItem'][.='Operations']").Click();
new Control("Manage Subcontractor Operations", "xpath","//div[@class='navItem'][.='Manage Subcontractor Operations']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control RUMSUBOP_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,RUMSUBOP_MainForm.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on Name...", Logger.MessageType.INF);
			Control RUMSUBOP_Name = new Control("Name", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='OP_DESC']");
			CPCommon.AssertEqual(true,RUMSUBOP_Name.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_MainForm);
IWebElement formBttn = RUMSUBOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? RUMSUBOP_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
RUMSUBOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control RUMSUBOP_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMSUBOP_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Vendors");


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming Click on VendorsLink...", Logger.MessageType.INF);
			Control RUMSUBOP_VendorsLink = new Control("VendorsLink", "ID", "lnk_1001339_RUMSUBOP_OPERATION");
			CPCommon.WaitControlDisplayed(RUMSUBOP_VendorsLink);
RUMSUBOP_VendorsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExist on VendorFormTable...", Logger.MessageType.INF);
			Control RUMSUBOP_VendorFormTable = new Control("VendorFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_VEND_CHD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMSUBOP_VendorFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on VendorForm...", Logger.MessageType.INF);
			Control RUMSUBOP_VendorForm = new Control("VendorForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_VEND_CHD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUMSUBOP_VendorForm.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_VendorForm);
formBttn = RUMSUBOP_VendorForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMSUBOP_VendorForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMSUBOP_VendorForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on Vendor_Location...", Logger.MessageType.INF);
			Control RUMSUBOP_Vendor_Location = new Control("Vendor_Location", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_VEND_CHD_']/ancestor::form[1]/descendant::*[@id='VEND_NAME_EXT']");
			CPCommon.AssertEqual(true,RUMSUBOP_Vendor_Location.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_VendorForm);
formBttn = RUMSUBOP_VendorForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("NOTES");


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming Click on NotesLink...", Logger.MessageType.INF);
			Control RUMSUBOP_NotesLink = new Control("NotesLink", "ID", "lnk_1001342_RUMSUBOP_OPERATION");
			CPCommon.WaitControlDisplayed(RUMSUBOP_NotesLink);
RUMSUBOP_NotesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on Notes_RequisitionPONoteOptions_DoNotLoadRequisitionLineNotes...", Logger.MessageType.INF);
			Control RUMSUBOP_Notes_RequisitionPONoteOptions_DoNotLoadRequisitionLineNotes = new Control("Notes_RequisitionPONoteOptions_DoNotLoadRequisitionLineNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_OPERATIONNOTES_SUB_']/ancestor::form[1]/descendant::*[@id='S_NOTES_LOAD_CD' and @value='N']");
			CPCommon.AssertEqual(true,RUMSUBOP_Notes_RequisitionPONoteOptions_DoNotLoadRequisitionLineNotes.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on NotesForm...", Logger.MessageType.INF);
			Control RUMSUBOP_NotesForm = new Control("NotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_OPERATIONNOTES_SUB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUMSUBOP_NotesForm.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_NotesForm);
formBttn = RUMSUBOP_NotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("TEXT");


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming Click on TextLink...", Logger.MessageType.INF);
			Control RUMSUBOP_TextLink = new Control("TextLink", "ID", "lnk_1001343_RUMSUBOP_OPERATION");
			CPCommon.WaitControlDisplayed(RUMSUBOP_TextLink);
RUMSUBOP_TextLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on StandardTextForm...", Logger.MessageType.INF);
			Control RUMSUBOP_StandardTextForm = new Control("StandardTextForm", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_OPERATIONTEXT_SUB_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,RUMSUBOP_StandardTextForm.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExist on StandardTextFormTable...", Logger.MessageType.INF);
			Control RUMSUBOP_StandardTextFormTable = new Control("StandardTextFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_OPERATIONTEXT_SUB_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,RUMSUBOP_StandardTextFormTable.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_StandardTextForm);
formBttn = RUMSUBOP_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? RUMSUBOP_StandardTextForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
RUMSUBOP_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on StandardText_Sequence...", Logger.MessageType.INF);
			Control RUMSUBOP_StandardText_Sequence = new Control("StandardText_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_OPERATIONTEXT_SUB_']/ancestor::form[1]/descendant::*[@id='SEQ_NO']");
			CPCommon.AssertEqual(true,RUMSUBOP_StandardText_Sequence.Exists());

												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_StandardTextForm);
formBttn = RUMSUBOP_StandardTextForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("cost");


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming Click on CostLink...", Logger.MessageType.INF);
			Control RUMSUBOP_CostLink = new Control("CostLink", "ID", "lnk_1001344_RUMSUBOP_OPERATION");
			CPCommon.WaitControlDisplayed(RUMSUBOP_CostLink);
RUMSUBOP_CostLink.Click(1.5);


												
				CPCommon.CurrentComponent = "RUMSUBOP";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[RUMSUBOP] Perfoming VerifyExists on CostDetails_StandardUnitCost...", Logger.MessageType.INF);
			Control RUMSUBOP_CostDetails_StandardUnitCost = new Control("CostDetails_StandardUnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__RUMSUBOP_COST_HDR_']/ancestor::form[1]/descendant::*[@id='ITEM_STD_CST']");
			CPCommon.AssertEqual(true,RUMSUBOP_CostDetails_StandardUnitCost.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "RUMSUBOP";
							CPCommon.WaitControlDisplayed(RUMSUBOP_MainForm);
formBttn = RUMSUBOP_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

