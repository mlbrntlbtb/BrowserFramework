 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MEQPWU_SMOKE : TestScript
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
new Control("Materials Estimating", "xpath","//div[@class='deptItem'][.='Materials Estimating']").Click();
new Control("Materials Estimating Reports/Inquiries", "xpath","//div[@class='navItem'][.='Materials Estimating Reports/Inquiries']").Click();
new Control("View Proposal BOM Where-Used Information", "xpath","//div[@class='navItem'][.='View Proposal BOM Where-Used Information']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MEQPWU_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MEQPWU_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on MainForm_Component_CompPart...", Logger.MessageType.INF);
			Control MEQPWU_MainForm_Component_CompPart = new Control("MainForm_Component_CompPart", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='COMP_PART_ID']");
			CPCommon.AssertEqual(true,MEQPWU_MainForm_Component_CompPart.Exists());

											Driver.SessionLogger.WriteLine("SET DATA");


												
				CPCommon.CurrentComponent = "MEQPWU";
							MEQPWU_MainForm_Component_CompPart.Click();
MEQPWU_MainForm_Component_CompPart.SendKeys("(KVCC) W6", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
MEQPWU_MainForm_Component_CompPart.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control MEQPWU_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MEQPWU_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MEQPWU_ChildForm);
IWebElement formBttn = MEQPWU_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MEQPWU_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MEQPWU_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


											Driver.SessionLogger.WriteLine("TAB");


												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on ChildForm_BasicInfo_Line...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm_BasicInfo_Line = new Control("ChildForm_BasicInfo_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,MEQPWU_ChildForm_BasicInfo_Line.Exists());

												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming Select on ChildForm_ChildFormTab...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm_ChildFormTab = new Control("ChildForm_ChildFormTab", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(MEQPWU_ChildForm_ChildFormTab);
IWebElement mTab = MEQPWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Component Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on ChildForm_ComponentDetail_BreakpointQuantitiesAndCosts_Quantity_1...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm_ComponentDetail_BreakpointQuantitiesAndCosts_Quantity_1 = new Control("ChildForm_ComponentDetail_BreakpointQuantitiesAndCosts_Quantity_1", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='COM_BRK_1_QTY']");
			CPCommon.AssertEqual(true,MEQPWU_ChildForm_ComponentDetail_BreakpointQuantitiesAndCosts_Quantity_1.Exists());

												
				CPCommon.CurrentComponent = "MEQPWU";
							CPCommon.WaitControlDisplayed(MEQPWU_ChildForm_ChildFormTab);
mTab = MEQPWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Assembly Detail").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on ChildForm_AssemblyDetail_BreakpointQuantitiesAndCosts_Quantity_1...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm_AssemblyDetail_BreakpointQuantitiesAndCosts_Quantity_1 = new Control("ChildForm_AssemblyDetail_BreakpointQuantitiesAndCosts_Quantity_1", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='ASY_BRK1_QTY']");
			CPCommon.AssertEqual(true,MEQPWU_ChildForm_AssemblyDetail_BreakpointQuantitiesAndCosts_Quantity_1.Exists());

												
				CPCommon.CurrentComponent = "MEQPWU";
							CPCommon.WaitControlDisplayed(MEQPWU_ChildForm_ChildFormTab);
mTab = MEQPWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on ChildForm_OtherInfo_ProposalManager...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm_OtherInfo_ProposalManager = new Control("ChildForm_OtherInfo_ProposalManager", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,MEQPWU_ChildForm_OtherInfo_ProposalManager.Exists());

												
				CPCommon.CurrentComponent = "MEQPWU";
							CPCommon.WaitControlDisplayed(MEQPWU_ChildForm_ChildFormTab);
mTab = MEQPWU_ChildForm_ChildFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Notes").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "MEQPWU";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MEQPWU] Perfoming VerifyExists on ChildForm_Notes_AssemblyNotes_AssemblyNotes...", Logger.MessageType.INF);
			Control MEQPWU_ChildForm_Notes_AssemblyNotes_AssemblyNotes = new Control("ChildForm_Notes_AssemblyNotes_AssemblyNotes", "xpath", "//div[translate(@id,'0123456789','')='pr__MEQPWU_ZMEQPWU_CTW_']/ancestor::form[1]/descendant::*[@id='ASY_NOTES']");
			CPCommon.AssertEqual(true,MEQPWU_ChildForm_Notes_AssemblyNotes_AssemblyNotes.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MEQPWU";
							CPCommon.WaitControlDisplayed(MEQPWU_MainForm);
formBttn = MEQPWU_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

