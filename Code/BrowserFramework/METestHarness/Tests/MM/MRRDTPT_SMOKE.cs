 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRRDTPT_SMOKE : TestScript
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
new Control("Material Requirements Planning", "xpath","//div[@class='deptItem'][.='Material Requirements Planning']").Click();
new Control("MRP Reports/Inquiries", "xpath","//div[@class='navItem'][.='MRP Reports/Inquiries']").Click();
new Control("Print Detailed Part Availability Report", "xpath","//div[@class='navItem'][.='Print Detailed Part Availability Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MRRDTPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRDTPT] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MRRDTPT_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRRDTPT_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MRRDTPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRDTPT] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control MRRDTPT_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MRRDTPT_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MRRDTPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRDTPT] Perfoming VerifyExists on MPSPlan...", Logger.MessageType.INF);
			Control MRRDTPT_MPSPlan = new Control("MPSPlan", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MPS_PLAN_ONE']");
			CPCommon.AssertEqual(true,MRRDTPT_MPSPlan.Exists());

												
				CPCommon.CurrentComponent = "MRRDTPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRDTPT] Perfoming VerifyExists on InventoryProject_SelectionOption...", Logger.MessageType.INF);
			Control MRRDTPT_InventoryProject_SelectionOption = new Control("InventoryProject_SelectionOption", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_PROJ_OPTION_CD']");
			CPCommon.AssertEqual(true,MRRDTPT_InventoryProject_SelectionOption.Exists());

												
				CPCommon.CurrentComponent = "MRRDTPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRDTPT] Perfoming VerifyExists on SortBy_FirstSort...", Logger.MessageType.INF);
			Control MRRDTPT_SortBy_FirstSort = new Control("SortBy_FirstSort", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='S_SORT_BY_CD']");
			CPCommon.AssertEqual(true,MRRDTPT_SortBy_FirstSort.Exists());

												
				CPCommon.CurrentComponent = "MRRDTPT";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRDTPT] Perfoming VerifyExists on IncludeMakeBuy_Make...", Logger.MessageType.INF);
			Control MRRDTPT_IncludeMakeBuy_Make = new Control("IncludeMakeBuy_Make", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MAKE_FL']");
			CPCommon.AssertEqual(true,MRRDTPT_IncludeMakeBuy_Make.Exists());

												
				CPCommon.CurrentComponent = "MRRDTPT";
							CPCommon.WaitControlDisplayed(MRRDTPT_MainForm);
IWebElement formBttn = MRRDTPT_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

