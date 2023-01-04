 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class FAMSOTBK_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Fixed Assets", "xpath","//div[@class='deptItem'][.='Fixed Assets']").Click();
new Control("Asset Master Records", "xpath","//div[@class='navItem'][.='Asset Master Records']").Click();
new Control("Manage Asset Other Books Information", "xpath","//div[@class='navItem'][.='Manage Asset Other Books Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control FAMSOTBK_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,FAMSOTBK_MainForm.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control FAMSOTBK_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,FAMSOTBK_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainForm);
IWebElement formBttn = FAMSOTBK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? FAMSOTBK_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
FAMSOTBK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on AssetNo...", Logger.MessageType.INF);
			Control FAMSOTBK_AssetNo = new Control("AssetNo", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ASSET_ID']");
			CPCommon.AssertEqual(true,FAMSOTBK_AssetNo.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on MainFormTab...", Logger.MessageType.INF);
			Control FAMSOTBK_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.AssertEqual(true,FAMSOTBK_MainFormTab.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
IWebElement mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Tax").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on Tax_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_Tax_Salvage_SalvagePercent = new Control("Tax_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B2_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_Tax_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Government").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on Government_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_Government_Salvage_SalvagePercent = new Control("Government_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B3_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_Government_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "State").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on State_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_State_Salvage_SalvagePercent = new Control("State_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B4_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_State_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "County").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on County_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_County_Salvage_SalvagePercent = new Control("County_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B5_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_County_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "City").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on City_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_City_Salvage_SalvagePercent = new Control("City_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B6_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_City_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Md State").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on MdState_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_MdState_Salvage_SalvagePercent = new Control("MdState_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B7_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_MdState_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Internal").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on Internal_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_Internal_Salvage_SalvagePercent = new Control("Internal_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B8_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_Internal_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "California").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on California_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_California_Salvage_SalvagePercent = new Control("California_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B9_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_California_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainFormTab);
mTab = FAMSOTBK_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "New York").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "FAMSOTBK";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[FAMSOTBK] Perfoming VerifyExists on NewYork_Salvage_SalvagePercent...", Logger.MessageType.INF);
			Control FAMSOTBK_NewYork_Salvage_SalvagePercent = new Control("NewYork_Salvage_SalvagePercent", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='B10_SLVGE_RT']");
			CPCommon.AssertEqual(true,FAMSOTBK_NewYork_Salvage_SalvagePercent.Exists());

												
				CPCommon.CurrentComponent = "FAMSOTBK";
							CPCommon.WaitControlDisplayed(FAMSOTBK_MainForm);
formBttn = FAMSOTBK_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

