 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMLVTAB_SMOKE : TestScript
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
new Control("Leave", "xpath","//div[@class='deptItem'][.='Leave']").Click();
new Control("Leave Controls", "xpath","//div[@class='navItem'][.='Leave Controls']").Click();
new Control("Manage Leave Codes", "xpath","//div[@class='navItem'][.='Manage Leave Codes']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDMLVTAB_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMLVTAB_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on LeaveCode...", Logger.MessageType.INF);
			Control LDMLVTAB_LeaveCode = new Control("LeaveCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='LV_CD']");
			CPCommon.AssertEqual(true,LDMLVTAB_LeaveCode.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on LeaveTableDetailsLink...", Logger.MessageType.INF);
			Control LDMLVTAB_LeaveTableDetailsLink = new Control("LeaveTableDetailsLink", "ID", "lnk_1001487_LDMLVTAB_LVTABLE_HDR");
			CPCommon.AssertEqual(true,LDMLVTAB_LeaveTableDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on LeaveTableDetails...", Logger.MessageType.INF);
			Control LDMLVTAB_LeaveTableDetails = new Control("LeaveTableDetails", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_LVTABLESCH_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMLVTAB_LeaveTableDetails.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExist on LeaveTableDetailsTable...", Logger.MessageType.INF);
			Control LDMLVTAB_LeaveTableDetailsTable = new Control("LeaveTableDetailsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_LVTABLESCH_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVTAB_LeaveTableDetailsTable.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
							CPCommon.WaitControlDisplayed(LDMLVTAB_LeaveTableDetails);
IWebElement formBttn = LDMLVTAB_LeaveTableDetails.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? LDMLVTAB_LeaveTableDetails.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
LDMLVTAB_LeaveTableDetails.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on LeaveTableDetails_MonthsOfService...", Logger.MessageType.INF);
			Control LDMLVTAB_LeaveTableDetails_MonthsOfService = new Control("LeaveTableDetails_MonthsOfService", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_LVTABLESCH_DTL_']/ancestor::form[1]/descendant::*[@id='MO_SERV_NO']");
			CPCommon.AssertEqual(true,LDMLVTAB_LeaveTableDetails_MonthsOfService.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
							CPCommon.WaitControlDisplayed(LDMLVTAB_LeaveTableDetails);
formBttn = LDMLVTAB_LeaveTableDetails.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on EligibleLaborLocationsLink...", Logger.MessageType.INF);
			Control LDMLVTAB_EligibleLaborLocationsLink = new Control("EligibleLaborLocationsLink", "ID", "lnk_5618_LDMLVTAB_LVTABLE_HDR");
			CPCommon.AssertEqual(true,LDMLVTAB_EligibleLaborLocationsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
							CPCommon.WaitControlDisplayed(LDMLVTAB_EligibleLaborLocationsLink);
LDMLVTAB_EligibleLaborLocationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on EligibleLaborLocations1Form...", Logger.MessageType.INF);
			Control LDMLVTAB_EligibleLaborLocations1Form = new Control("EligibleLaborLocations1Form", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_LABLOC_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMLVTAB_EligibleLaborLocations1Form.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExists on EligibleLaborLocations2Form...", Logger.MessageType.INF);
			Control LDMLVTAB_EligibleLaborLocations2Form = new Control("EligibleLaborLocations2Form", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_ELIGLABLOC_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,LDMLVTAB_EligibleLaborLocations2Form.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExist on EligibleLaborLocations1Table...", Logger.MessageType.INF);
			Control LDMLVTAB_EligibleLaborLocations1Table = new Control("EligibleLaborLocations1Table", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_LABLOC_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVTAB_EligibleLaborLocations1Table.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExist on EligibleLaborLocations2Table...", Logger.MessageType.INF);
			Control LDMLVTAB_EligibleLaborLocations2Table = new Control("EligibleLaborLocations2Table", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMLVTAB_ELIGLABLOC_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVTAB_EligibleLaborLocations2Table.Exists());

												
				CPCommon.CurrentComponent = "LDMLVTAB";
							CPCommon.WaitControlDisplayed(LDMLVTAB_EligibleLaborLocations2Form);
formBttn = LDMLVTAB_EligibleLaborLocations2Form.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "LDMLVTAB";
							CPCommon.WaitControlDisplayed(LDMLVTAB_MainForm);
formBttn = LDMLVTAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMLVTAB_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMLVTAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMLVTAB";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMLVTAB] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDMLVTAB_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMLVTAB_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDMLVTAB";
							CPCommon.WaitControlDisplayed(LDMLVTAB_MainForm);
formBttn = LDMLVTAB_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

