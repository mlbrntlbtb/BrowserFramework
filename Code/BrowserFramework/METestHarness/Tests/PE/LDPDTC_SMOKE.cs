 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDPDTC_SMOKE : TestScript
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
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Timesheet Interface", "xpath","//div[@class='navItem'][.='Timesheet Interface']").Click();
new Control("Export Data To Deltek Time and Expense", "xpath","//div[@class='navItem'][.='Export Data To Deltek Time and Expense']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control LDPDTC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDPDTC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control LDPDTC_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,LDPDTC_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on MainFormSelectionRangesTab...", Logger.MessageType.INF);
			Control LDPDTC_MainFormSelectionRangesTab = new Control("MainFormSelectionRangesTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,LDPDTC_MainFormSelectionRangesTab.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
							CPCommon.WaitControlDisplayed(LDPDTC_MainFormSelectionRangesTab);
IWebElement mTab = LDPDTC_MainFormSelectionRangesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Export Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on SelectionRanges_ExportInformation_FileLocation...", Logger.MessageType.INF);
			Control LDPDTC_SelectionRanges_ExportInformation_FileLocation = new Control("SelectionRanges_ExportInformation_FileLocation", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='FILE_PATH']");
			CPCommon.AssertEqual(true,LDPDTC_SelectionRanges_ExportInformation_FileLocation.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
							CPCommon.WaitControlDisplayed(LDPDTC_MainFormSelectionRangesTab);
mTab = LDPDTC_MainFormSelectionRangesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Base/Link Tables").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on SelectionRanges_BaseLinkTables_Options_ProjectName...", Logger.MessageType.INF);
			Control LDPDTC_SelectionRanges_BaseLinkTables_Options_ProjectName = new Control("SelectionRanges_BaseLinkTables_Options_ProjectName", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_NAME']");
			CPCommon.AssertEqual(true,LDPDTC_SelectionRanges_BaseLinkTables_Options_ProjectName.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
							CPCommon.WaitControlDisplayed(LDPDTC_MainFormSelectionRangesTab);
mTab = LDPDTC_MainFormSelectionRangesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Resource Information").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on SelectionRanges_EMPLOYEEInformation_DeltekTimeAndExpenseEMPLOYEEMappings_UserDefinedCode1...", Logger.MessageType.INF);
			Control LDPDTC_SelectionRanges_EMPLOYEEInformation_DeltekTimeAndExpenseEMPLOYEEMappings_UserDefinedCode1 = new Control("SelectionRanges_EMPLOYEEInformation_DeltekTimeAndExpenseEMPLOYEEMappings_UserDefinedCode1", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='USER_DEF_1']");
			CPCommon.AssertEqual(true,LDPDTC_SelectionRanges_EMPLOYEEInformation_DeltekTimeAndExpenseEMPLOYEEMappings_UserDefinedCode1.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
							CPCommon.WaitControlDisplayed(LDPDTC_MainFormSelectionRangesTab);
mTab = LDPDTC_MainFormSelectionRangesTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Charge Trees").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExists on SelectionRanges_ChargeTrees_ChargeTrees_TreeCode...", Logger.MessageType.INF);
			Control LDPDTC_SelectionRanges_ChargeTrees_ChargeTrees_TreeCode = new Control("SelectionRanges_ChargeTrees_ChargeTrees_TreeCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='TREE_CD']");
			CPCommon.AssertEqual(true,LDPDTC_SelectionRanges_ChargeTrees_ChargeTrees_TreeCode.Exists());

												
				CPCommon.CurrentComponent = "LDPDTC";
							CPCommon.WaitControlDisplayed(LDPDTC_MainForm);
IWebElement formBttn = LDPDTC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDPDTC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDPDTC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDPDTC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDPDTC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control LDPDTC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDPDTC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "LDPDTC";
							CPCommon.WaitControlDisplayed(LDPDTC_MainForm);
formBttn = LDPDTC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

