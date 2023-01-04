 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HAMCDATA_SMOKE : TestScript
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
new Control("Affirmative Action", "xpath","//div[@class='deptItem'][.='Affirmative Action']").Click();
new Control("Affirmative Action Plan Information", "xpath","//div[@class='navItem'][.='Affirmative Action Plan Information']").Click();
new Control("Manage Affirmative Action Census Data", "xpath","//div[@class='navItem'][.='Manage Affirmative Action Census Data']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HAMCDATA_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HAMCDATA_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExists on CensusCode...", Logger.MessageType.INF);
			Control HAMCDATA_CensusCode = new Control("CensusCode", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CENSUS_CO_ID']");
			CPCommon.AssertEqual(true,HAMCDATA_CensusCode.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
							CPCommon.WaitControlDisplayed(HAMCDATA_MainForm);
IWebElement formBttn = HAMCDATA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HAMCDATA_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HAMCDATA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HAMCDATA_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAMCDATA_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExists on CensusDataDetailsLink...", Logger.MessageType.INF);
			Control HAMCDATA_CensusDataDetailsLink = new Control("CensusDataDetailsLink", "ID", "lnk_1000854_HAMCDATA_HAFFCENSUSHDR_HDR");
			CPCommon.AssertEqual(true,HAMCDATA_CensusDataDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
							CPCommon.WaitControlDisplayed(HAMCDATA_CensusDataDetailsLink);
HAMCDATA_CensusDataDetailsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExist on CensusDataDetailsFormTable...", Logger.MessageType.INF);
			Control HAMCDATA_CensusDataDetailsFormTable = new Control("CensusDataDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMCDATA_HAFFCENSUSL_DET_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HAMCDATA_CensusDataDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming ClickButton on CensusDataDetailsForm...", Logger.MessageType.INF);
			Control HAMCDATA_CensusDataDetailsForm = new Control("CensusDataDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMCDATA_HAFFCENSUSL_DET_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(HAMCDATA_CensusDataDetailsForm);
formBttn = HAMCDATA_CensusDataDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HAMCDATA_CensusDataDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HAMCDATA_CensusDataDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "HAMCDATA";
							CPCommon.AssertEqual(true,HAMCDATA_CensusDataDetailsForm.Exists());

													
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming Select on CensusDataDetailsTab...", Logger.MessageType.INF);
			Control HAMCDATA_CensusDataDetailsTab = new Control("CensusDataDetailsTab", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMCDATA_HAFFCENSUSL_DET_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(HAMCDATA_CensusDataDetailsTab);
IWebElement mTab = HAMCDATA_CensusDataDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Census Job Title").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExists on CensusDataDetails_CensusJobTitle_CensusDetails_CensusJobTitle...", Logger.MessageType.INF);
			Control HAMCDATA_CensusDataDetails_CensusJobTitle_CensusDetails_CensusJobTitle = new Control("CensusDataDetails_CensusJobTitle_CensusDetails_CensusJobTitle", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMCDATA_HAFFCENSUSL_DET_']/ancestor::form[1]/descendant::*[@id='CENSUS_JOB_CD']");
			CPCommon.AssertEqual(true,HAMCDATA_CensusDataDetails_CensusJobTitle_CensusDetails_CensusJobTitle.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
							CPCommon.WaitControlDisplayed(HAMCDATA_CensusDataDetailsTab);
mTab = HAMCDATA_CensusDataDetailsTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Census Job Percentage").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "HAMCDATA";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HAMCDATA] Perfoming VerifyExists on CensusDataDetails_CensusJobPercentage_Percentage_Female...", Logger.MessageType.INF);
			Control HAMCDATA_CensusDataDetails_CensusJobPercentage_Percentage_Female = new Control("CensusDataDetails_CensusJobPercentage_Percentage_Female", "xpath", "//div[translate(@id,'0123456789','')='pr__HAMCDATA_HAFFCENSUSL_DET_']/ancestor::form[1]/descendant::*[@id='FEMALE_PCT']");
			CPCommon.AssertEqual(true,HAMCDATA_CensusDataDetails_CensusJobPercentage_Percentage_Female.Exists());

												
				CPCommon.CurrentComponent = "HAMCDATA";
							CPCommon.WaitControlDisplayed(HAMCDATA_CensusDataDetailsForm);
formBttn = HAMCDATA_CensusDataDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close App");


												
				CPCommon.CurrentComponent = "HAMCDATA";
							CPCommon.WaitControlDisplayed(HAMCDATA_MainForm);
formBttn = HAMCDATA_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

