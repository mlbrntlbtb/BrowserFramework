 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLMWAWF_SMOKE : TestScript
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
new Control("Projects", "xpath","//div[@class='busItem'][.='Projects']").Click();
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Billing Master", "xpath","//div[@class='navItem'][.='Billing Master']").Click();
new Control("Manage Project iRAPT Information", "xpath","//div[@class='navItem'][.='Manage Project iRAPT Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLMWAWF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLMWAWF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on Identification_Project...", Logger.MessageType.INF);
			Control BLMWAWF_Identification_Project = new Control("Identification_Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BLMWAWF_Identification_Project.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_MainForm);
IWebElement formBttn = BLMWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLMWAWF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLMWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BLMWAWF_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMWAWF_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("UID");


												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on Identification_UIDInformationLink...", Logger.MessageType.INF);
			Control BLMWAWF_Identification_UIDInformationLink = new Control("Identification_UIDInformationLink", "ID", "lnk_1007673_BLMWAWF_PROJWAWFINFO_HDR");
			CPCommon.AssertEqual(true,BLMWAWF_Identification_UIDInformationLink.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_Identification_UIDInformationLink);
BLMWAWF_Identification_UIDInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on UIDInformationForm...", Logger.MessageType.INF);
			Control BLMWAWF_UIDInformationForm = new Control("UIDInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWFUID_UIDCHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMWAWF_UIDInformationForm.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExist on UIDInformation_Table...", Logger.MessageType.INF);
			Control BLMWAWF_UIDInformation_Table = new Control("UIDInformation_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWFUID_UIDCHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMWAWF_UIDInformation_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_UIDInformationForm);
formBttn = BLMWAWF_UIDInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("RFID");


												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on Identification_RFIDInformationLink...", Logger.MessageType.INF);
			Control BLMWAWF_Identification_RFIDInformationLink = new Control("Identification_RFIDInformationLink", "ID", "lnk_1007668_BLMWAWF_PROJWAWFINFO_HDR");
			CPCommon.AssertEqual(true,BLMWAWF_Identification_RFIDInformationLink.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_Identification_RFIDInformationLink);
BLMWAWF_Identification_RFIDInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on RFIDInformationForm...", Logger.MessageType.INF);
			Control BLMWAWF_RFIDInformationForm = new Control("RFIDInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWFRFID_RFIDCHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMWAWF_RFIDInformationForm.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExist on RFIDInformation_Table...", Logger.MessageType.INF);
			Control BLMWAWF_RFIDInformation_Table = new Control("RFIDInformation_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWFRFID_RFIDCHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMWAWF_RFIDInformation_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_RFIDInformationForm);
formBttn = BLMWAWF_RFIDInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLIN");


												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on Identification_CLINMappingLink...", Logger.MessageType.INF);
			Control BLMWAWF_Identification_CLINMappingLink = new Control("Identification_CLINMappingLink", "ID", "lnk_3610_BLMWAWF_PROJWAWFINFO_HDR");
			CPCommon.AssertEqual(true,BLMWAWF_Identification_CLINMappingLink.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_Identification_CLINMappingLink);
BLMWAWF_Identification_CLINMappingLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on CLINMappingForm...", Logger.MessageType.INF);
			Control BLMWAWF_CLINMappingForm = new Control("CLINMappingForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWCLIN_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMWAWF_CLINMappingForm.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExist on CLINMapping_Table...", Logger.MessageType.INF);
			Control BLMWAWF_CLINMapping_Table = new Control("CLINMapping_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWCLIN_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMWAWF_CLINMapping_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_CLINMappingForm);
formBttn = BLMWAWF_CLINMappingForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("ACRN");


												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on Identification_ACRNFMSWorksheetLink...", Logger.MessageType.INF);
			Control BLMWAWF_Identification_ACRNFMSWorksheetLink = new Control("Identification_ACRNFMSWorksheetLink", "ID", "lnk_1007665_BLMWAWF_PROJWAWFINFO_HDR");
			CPCommon.AssertEqual(true,BLMWAWF_Identification_ACRNFMSWorksheetLink.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_Identification_ACRNFMSWorksheetLink);
BLMWAWF_Identification_ACRNFMSWorksheetLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExists on ACRNFMSWorksheetForm...", Logger.MessageType.INF);
			Control BLMWAWF_ACRNFMSWorksheetForm = new Control("ACRNFMSWorksheetForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWFACRN_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BLMWAWF_ACRNFMSWorksheetForm.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLMWAWF] Perfoming VerifyExist on ACRNFMSWorksheet_Table...", Logger.MessageType.INF);
			Control BLMWAWF_ACRNFMSWorksheet_Table = new Control("ACRNFMSWorksheet_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__BLMWAWF_PROJWAWFACRN_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLMWAWF_ACRNFMSWorksheet_Table.Exists());

												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_ACRNFMSWorksheetForm);
formBttn = BLMWAWF_ACRNFMSWorksheetForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BLMWAWF";
							CPCommon.WaitControlDisplayed(BLMWAWF_MainForm);
formBttn = BLMWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

