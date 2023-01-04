 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPOOL_SMOKE : TestScript
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
new Control("Cost and Revenue Processing", "xpath","//div[@class='deptItem'][.='Cost and Revenue Processing']").Click();
new Control("Cost Pools", "xpath","//div[@class='navItem'][.='Cost Pools']").Click();
new Control("Manage Cost Pools", "xpath","//div[@class='navItem'][.='Manage Cost Pools']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJMPOOL_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJMPOOL_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on AllocationGroup...", Logger.MessageType.INF);
			Control PJMPOOL_AllocationGroup = new Control("AllocationGroup", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ALLOC_GRP_NO']");
			CPCommon.AssertEqual(true,PJMPOOL_AllocationGroup.Exists());

											Driver.SessionLogger.WriteLine("TAB");


												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on PoolType...", Logger.MessageType.INF);
			Control PJMPOOL_PoolType = new Control("PoolType", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='POOL_TYPE_DESC']");
			CPCommon.AssertEqual(true,PJMPOOL_PoolType.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on PoolCostLink...", Logger.MessageType.INF);
			Control PJMPOOL_PoolCostLink = new Control("PoolCostLink", "ID", "lnk_1002408_PJMPOOL_POOLALLOC_HDR");
			CPCommon.AssertEqual(true,PJMPOOL_PoolCostLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.WaitControlDisplayed(PJMPOOL_PoolCostLink);
PJMPOOL_PoolCostLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExist on PoolCostLinkTable...", Logger.MessageType.INF);
			Control PJMPOOL_PoolCostLinkTable = new Control("PoolCostLinkTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLCOSTACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOOL_PoolCostLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExist on PoolCost_AvailableAccountLinkTable...", Logger.MessageType.INF);
			Control PJMPOOL_PoolCost_AvailableAccountLinkTable = new Control("PoolCost_AvailableAccountLinkTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_ORGACCT_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOOL_PoolCost_AvailableAccountLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming Close on PoolCostForm...", Logger.MessageType.INF);
			Control PJMPOOL_PoolCostForm = new Control("PoolCostForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLCOSTACCT_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPOOL_PoolCostForm);
IWebElement formBttn = PJMPOOL_PoolCostForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on PoolBaseLink...", Logger.MessageType.INF);
			Control PJMPOOL_PoolBaseLink = new Control("PoolBaseLink", "ID", "lnk_1002409_PJMPOOL_POOLALLOC_HDR");
			CPCommon.AssertEqual(true,PJMPOOL_PoolBaseLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.WaitControlDisplayed(PJMPOOL_PoolBaseLink);
PJMPOOL_PoolBaseLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExist on PoolBaseLinkTable...", Logger.MessageType.INF);
			Control PJMPOOL_PoolBaseLinkTable = new Control("PoolBaseLinkTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLBASEACCT_POOLBASE_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOOL_PoolBaseLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExist on PoolBase_AvailableAccountLinkTable...", Logger.MessageType.INF);
			Control PJMPOOL_PoolBase_AvailableAccountLinkTable = new Control("PoolBase_AvailableAccountLinkTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLBASEACCT_ORGACCT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOOL_PoolBase_AvailableAccountLinkTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming Close on PoolBaseForm...", Logger.MessageType.INF);
			Control PJMPOOL_PoolBaseForm = new Control("PoolBaseForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLBASEACCT_POOLBASE_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPOOL_PoolBaseForm);
formBttn = PJMPOOL_PoolBaseForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on ServiceCenterLink...", Logger.MessageType.INF);
			Control PJMPOOL_ServiceCenterLink = new Control("ServiceCenterLink", "ID", "lnk_1002411_PJMPOOL_POOLALLOC_HDR");
			CPCommon.AssertEqual(true,PJMPOOL_ServiceCenterLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.WaitControlDisplayed(PJMPOOL_ServiceCenterLink);
PJMPOOL_ServiceCenterLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on ServiceCenterForm...", Logger.MessageType.INF);
			Control PJMPOOL_ServiceCenterForm = new Control("ServiceCenterForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_SVCTRINFO_SERVICECENTR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMPOOL_ServiceCenterForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on ServiceCenter_StandardCost_StandardUnit...", Logger.MessageType.INF);
			Control PJMPOOL_ServiceCenter_StandardCost_StandardUnit = new Control("ServiceCenter_StandardCost_StandardUnit", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_SVCTRINFO_SERVICECENTR_']/ancestor::form[1]/descendant::*[@id='UNIT_CST_AMT']");
			CPCommon.AssertEqual(true,PJMPOOL_ServiceCenter_StandardCost_StandardUnit.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.WaitControlDisplayed(PJMPOOL_ServiceCenterForm);
formBttn = PJMPOOL_ServiceCenterForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExists on PoolRatesLink...", Logger.MessageType.INF);
			Control PJMPOOL_PoolRatesLink = new Control("PoolRatesLink", "ID", "lnk_1002412_PJMPOOL_POOLALLOC_HDR");
			CPCommon.AssertEqual(true,PJMPOOL_PoolRatesLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.WaitControlDisplayed(PJMPOOL_PoolRatesLink);
PJMPOOL_PoolRatesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming VerifyExist on PoolRatesTable...", Logger.MessageType.INF);
			Control PJMPOOL_PoolRatesTable = new Control("PoolRatesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLRTTABLE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPOOL_PoolRatesTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPOOL";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPOOL] Perfoming ClickButton on PoolRatesForm...", Logger.MessageType.INF);
			Control PJMPOOL_PoolRatesForm = new Control("PoolRatesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJMPOOL_POOLRTTABLE_CHILD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPOOL_PoolRatesForm);
formBttn = PJMPOOL_PoolRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPOOL_PoolRatesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPOOL_PoolRatesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.AssertEqual(true,PJMPOOL_PoolRatesForm.Exists());

												Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMPOOL";
							CPCommon.WaitControlDisplayed(PJMPOOL_MainForm);
formBttn = PJMPOOL_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

