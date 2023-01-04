 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMCSADJ_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Adjustments", "xpath","//div[@class='navItem'][.='Adjustments']").Click();
new Control("Enter Cost Adjustments", "xpath","//div[@class='navItem'][.='Enter Cost Adjustments']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control INMCSADJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,INMCSADJ_MainForm.Exists());

												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming VerifyExists on CostAdjustmentID...", Logger.MessageType.INF);
			Control INMCSADJ_CostAdjustmentID = new Control("CostAdjustmentID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVT_TRN_ID']");
			CPCommon.AssertEqual(true,INMCSADJ_CostAdjustmentID.Exists());

												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming VerifyExists on Details_PartToAdjust_InvAbbrev...", Logger.MessageType.INF);
			Control INMCSADJ_Details_PartToAdjust_InvAbbrev = new Control("Details_PartToAdjust_InvAbbrev", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='INVT_ABBRV_CD_FR']");
			CPCommon.AssertEqual(true,INMCSADJ_Details_PartToAdjust_InvAbbrev.Exists());

												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming Select on MainFormTab...", Logger.MessageType.INF);
			Control INMCSADJ_MainFormTab = new Control("MainFormTab", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INMCSADJ_MainFormTab);
IWebElement mTab = INMCSADJ_MainFormTab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Cost Elements").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming VerifyExists on CostElements_CostMethod...", Logger.MessageType.INF);
			Control INMCSADJ_CostElements_CostMethod = new Control("CostElements_CostMethod", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='CST_MTHD_DESC']");
			CPCommon.AssertEqual(true,INMCSADJ_CostElements_CostMethod.Exists());

											Driver.SessionLogger.WriteLine("accounting period");


												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming Click on AccountingPeriodLink...", Logger.MessageType.INF);
			Control INMCSADJ_AccountingPeriodLink = new Control("AccountingPeriodLink", "ID", "lnk_1007986_INMCSADJ_INVTTRN");
			CPCommon.WaitControlDisplayed(INMCSADJ_AccountingPeriodLink);
INMCSADJ_AccountingPeriodLink.Click(1.5);


												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming VerifyExists on AccountingPeriodForm...", Logger.MessageType.INF);
			Control INMCSADJ_AccountingPeriodForm = new Control("AccountingPeriodForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMCSADJ_AccountingPeriodForm.Exists());

												
				CPCommon.CurrentComponent = "INMCSADJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMCSADJ] Perfoming VerifyExists on FiscalYear...", Logger.MessageType.INF);
			Control INMCSADJ_FiscalYear = new Control("FiscalYear", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMACCPD_HDR_']/ancestor::form[1]/descendant::*[@id='DFS_FYCD_ST']");
			CPCommon.AssertEqual(true,INMCSADJ_FiscalYear.Exists());

												
				CPCommon.CurrentComponent = "INMCSADJ";
							CPCommon.WaitControlDisplayed(INMCSADJ_AccountingPeriodForm);
IWebElement formBttn = INMCSADJ_AccountingPeriodForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMCSADJ";
							CPCommon.WaitControlDisplayed(INMCSADJ_MainForm);
formBttn = INMCSADJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

