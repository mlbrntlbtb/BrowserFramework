 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class AOMCDCCN_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Ceridian Interface", "xpath","//div[@class='navItem'][.='Ceridian Interface']").Click();
new Control("Manage Ceridian Configuration", "xpath","//div[@class='navItem'][.='Manage Ceridian Configuration']").Click();


												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control AOMCDCCN_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,AOMCDCCN_MainForm.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianCompanyID...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianCompanyID = new Control("CeridianCompanyID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CDN_CO_ID']");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianCompanyID.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
							CPCommon.WaitControlDisplayed(AOMCDCCN_MainForm);
IWebElement formBttn = AOMCDCCN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? AOMCDCCN_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
AOMCDCCN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control AOMCDCCN_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMCDCCN_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianIDFormatForm...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianIDFormatForm = new Control("CeridianIDFormatForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_XCDCIDFORMAT_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianIDFormatForm.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExist on CeridianIDFormatFormTable...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianIDFormatFormTable = new Control("CeridianIDFormatFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_XCDCIDFORMAT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianIDFormatFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
							CPCommon.WaitControlDisplayed(AOMCDCCN_CeridianIDFormatForm);
formBttn = AOMCDCCN_CeridianIDFormatForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMCDCCN_CeridianIDFormatForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMCDCCN_CeridianIDFormatForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianIDFormat_Sequence...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianIDFormat_Sequence = new Control("CeridianIDFormat_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_XCDCIDFORMAT_CHILD_']/ancestor::form[1]/descendant::*[@id='SEQUENCE_NO']");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianIDFormat_Sequence.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming Click on CeridianMappingLink...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMappingLink = new Control("CeridianMappingLink", "ID", "lnk_5155_AOMCDCCN_XCDCCONFIG");
			CPCommon.WaitControlDisplayed(AOMCDCCN_CeridianMappingLink);
AOMCDCCN_CeridianMappingLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianMappingForm...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMappingForm = new Control("CeridianMappingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_XCDCMAP_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianMappingForm.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExist on CeridianMappingFormTable...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMappingFormTable = new Control("CeridianMappingFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_XCDCMAP_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianMappingFormTable.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
							CPCommon.WaitControlDisplayed(AOMCDCCN_CeridianMappingForm);
formBttn = AOMCDCCN_CeridianMappingForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? AOMCDCCN_CeridianMappingForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
AOMCDCCN_CeridianMappingForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianMapping_AccountPayType...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMapping_AccountPayType = new Control("CeridianMapping_AccountPayType", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_XCDCMAP_DTL_']/ancestor::form[1]/descendant::*[@id='X_MAP_SRCE_S']");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianMapping_AccountPayType.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming Click on CeridianMapping_PopulateMappingTableLink...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMapping_PopulateMappingTableLink = new Control("CeridianMapping_PopulateMappingTableLink", "ID", "lnk_5156_AOMCDCCN_XCDCMAP_DTL");
			CPCommon.WaitControlDisplayed(AOMCDCCN_CeridianMapping_PopulateMappingTableLink);
AOMCDCCN_CeridianMapping_PopulateMappingTableLink.Click(1.5);


												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianMapping_PopulateMappingTable_Form...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMapping_PopulateMappingTable_Form = new Control("CeridianMapping_PopulateMappingTable_Form", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_ACCT_DLG_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianMapping_PopulateMappingTable_Form.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[AOMCDCCN] Perfoming VerifyExists on CeridianMapping_PopulateMappingTable_PayTypes_From...", Logger.MessageType.INF);
			Control AOMCDCCN_CeridianMapping_PopulateMappingTable_PayTypes_From = new Control("CeridianMapping_PopulateMappingTable_PayTypes_From", "xpath", "//div[translate(@id,'0123456789','')='pr__AOMCDCCN_ACCT_DLG_']/ancestor::form[1]/descendant::*[@id='DFPAYTYPEFROM']");
			CPCommon.AssertEqual(true,AOMCDCCN_CeridianMapping_PopulateMappingTable_PayTypes_From.Exists());

												
				CPCommon.CurrentComponent = "AOMCDCCN";
							CPCommon.WaitControlDisplayed(AOMCDCCN_CeridianMapping_PopulateMappingTable_Form);
formBttn = AOMCDCCN_CeridianMapping_PopulateMappingTable_Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "AOMCDCCN";
							CPCommon.WaitControlDisplayed(AOMCDCCN_MainForm);
formBttn = AOMCDCCN_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "Dialog";
								CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
CPCommon.ClickOkDialogIfExists("You have unsaved changes. Select Cancel to go back and save changes or select OK to discard changes and close this application.");


												
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

