 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRPFPO_SMOKE : TestScript
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
new Control("Material Requirements Planning", "xpath","//div[@class='navItem'][.='Material Requirements Planning']").Click();
new Control("Firm Material Requirements Planning Planned Orders", "xpath","//div[@class='navItem'][.='Firm Material Requirements Planning Planned Orders']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "MRPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPFPO] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MRPFPO_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRPFPO_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MRPFPO";
							CPCommon.WaitControlDisplayed(MRPFPO_MainForm);
IWebElement formBttn = MRPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MRPFPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MRPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MRPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPFPO] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MRPFPO_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MRPFPO_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "MRPFPO";
							CPCommon.WaitControlDisplayed(MRPFPO_MainForm);
formBttn = MRPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? MRPFPO_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
MRPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "MRPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPFPO] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MRPFPO_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MRPFPO_MainForm_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "MRPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPFPO] Perfoming VerifyExists on MainForm_SelectionRanges_Planner...", Logger.MessageType.INF);
			Control MRPFPO_MainForm_SelectionRanges_Planner = new Control("MainForm_SelectionRanges_Planner", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PLANNER_RANGE']");
			CPCommon.AssertEqual(true,MRPFPO_MainForm_SelectionRanges_Planner.Exists());

												
				CPCommon.CurrentComponent = "MRPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPFPO] Perfoming VerifyExists on MainForm_Options_IncludePlannedOrderType_MRP...", Logger.MessageType.INF);
			Control MRPFPO_MainForm_Options_IncludePlannedOrderType_MRP = new Control("MainForm_Options_IncludePlannedOrderType_MRP", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='MRP_FL']");
			CPCommon.AssertEqual(true,MRPFPO_MainForm_Options_IncludePlannedOrderType_MRP.Exists());

												
				CPCommon.CurrentComponent = "MRPFPO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRPFPO] Perfoming VerifyExists on MainForm_Generate_PurchaseRequisitions...", Logger.MessageType.INF);
			Control MRPFPO_MainForm_Generate_PurchaseRequisitions = new Control("MainForm_Generate_PurchaseRequisitions", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='GEN_RQ_FL']");
			CPCommon.AssertEqual(true,MRPFPO_MainForm_Generate_PurchaseRequisitions.Exists());

											Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "MRPFPO";
							CPCommon.WaitControlDisplayed(MRPFPO_MainForm);
formBttn = MRPFPO_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

