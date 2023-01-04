 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJRLABSM_SMOKE : TestScript
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
new Control("Project Inquiry and Reporting", "xpath","//div[@class='deptItem'][.='Project Inquiry and Reporting']").Click();
new Control("Project Reports/Inquiries", "xpath","//div[@class='navItem'][.='Project Reports/Inquiries']").Click();
new Control("Print Project Labor Summary Report", "xpath","//div[@class='navItem'][.='Print Project Labor Summary Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJRLABSM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJRLABSM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PJRLABSM_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PJRLABSM_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control PJRLABSM_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(PJRLABSM_MainFormTab);
IWebElement mTab = PJRLABSM_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Options").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExists on Options_SelectionRanges_PrimaryGrouping_GroupBy...", Logger.MessageType.INF);
			Control PJRLABSM_Options_SelectionRanges_PrimaryGrouping_GroupBy = new Control("Options_SelectionRanges_PrimaryGrouping_GroupBy", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='GRP_BY_OPTION']");
			CPCommon.AssertEqual(true,PJRLABSM_Options_SelectionRanges_PrimaryGrouping_GroupBy.Exists());

												
				CPCommon.CurrentComponent = "PJRLABSM";
							CPCommon.WaitControlDisplayed(PJRLABSM_MainFormTab);
mTab = PJRLABSM_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Next").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExists on Next_SelectionRanges_Option_Organizations...", Logger.MessageType.INF);
			Control PJRLABSM_Next_SelectionRanges_Option_Organizations = new Control("Next_SelectionRanges_Option_Organizations", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ORG_RANGE_CD']");
			CPCommon.AssertEqual(true,PJRLABSM_Next_SelectionRanges_Option_Organizations.Exists());

												
				CPCommon.CurrentComponent = "PJRLABSM";
							CPCommon.WaitControlDisplayed(PJRLABSM_MainForm);
IWebElement formBttn = PJRLABSM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJRLABSM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJRLABSM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PJRLABSM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJRLABSM_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Project Non Contiguus Ranges");


												
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExists on ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control PJRLABSM_ProjectNonContiguousRangesLink = new Control("ProjectNonContiguousRangesLink", "ID", "lnk_2684_PJRLABSM_PARAM");
			CPCommon.AssertEqual(true,PJRLABSM_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "PJRLABSM";
							CPCommon.WaitControlDisplayed(PJRLABSM_ProjectNonContiguousRangesLink);
PJRLABSM_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming VerifyExist on ProjectNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control PJRLABSM_ProjectNonContiguousRangesFormTable = new Control("ProjectNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJRLABSM_ProjectNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJRLABSM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJRLABSM] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control PJRLABSM_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJRLABSM_ProjectNonContiguousRangesForm);
formBttn = PJRLABSM_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "PJRLABSM";
							CPCommon.WaitControlDisplayed(PJRLABSM_MainForm);
formBttn = PJRLABSM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

