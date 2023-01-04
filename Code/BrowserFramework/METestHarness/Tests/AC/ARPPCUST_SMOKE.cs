 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARPPCUST_SMOKE : TestScript
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
new Control("Accounting", "xpath","//div[@class='busItem'][.='Accounting']").Click();
new Control("Accounts Receivable", "xpath","//div[@class='deptItem'][.='Accounts Receivable']").Click();
new Control("Administrative Utilities", "xpath","//div[@class='navItem'][.='Administrative Utilities']").Click();
new Control("Purge Customers", "xpath","//div[@class='navItem'][.='Purge Customers']").Click();


											Driver.SessionLogger.WriteLine("MAIN TABLE");


												
				CPCommon.CurrentComponent = "ARPPCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPCUST] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control ARPPCUST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,ARPPCUST_MainForm.Exists());

												
				CPCommon.CurrentComponent = "ARPPCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPCUST] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control ARPPCUST_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,ARPPCUST_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "ARPPCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPCUST] Perfoming VerifyExists on CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARPPCUST_CustomerNonContiguousRangesLink = new Control("CustomerNonContiguousRangesLink", "ID", "lnk_3616_ARPPCUST_WFUNCPARAMCATLG_PARAM");
			CPCommon.AssertEqual(true,ARPPCUST_CustomerNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "ARPPCUST";
							CPCommon.WaitControlDisplayed(ARPPCUST_CustomerNonContiguousRangesLink);
ARPPCUST_CustomerNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "ARPPCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPCUST] Perfoming VerifyExist on CustomerNonContiguousRangesFormTable...", Logger.MessageType.INF);
			Control ARPPCUST_CustomerNonContiguousRangesFormTable = new Control("CustomerNonContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARPPCUST_CustomerNonContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "ARPPCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPCUST] Perfoming VerifyExists on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARPPCUST_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,ARPPCUST_CustomerNonContiguousRangesForm.Exists());

												
				CPCommon.CurrentComponent = "ARPPCUST";
							CPCommon.WaitControlDisplayed(ARPPCUST_MainForm);
IWebElement formBttn = ARPPCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ARPPCUST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ARPPCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "ARPPCUST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARPPCUST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control ARPPCUST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARPPCUST_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "ARPPCUST";
							CPCommon.WaitControlDisplayed(ARPPCUST_MainForm);
formBttn = ARPPCUST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

