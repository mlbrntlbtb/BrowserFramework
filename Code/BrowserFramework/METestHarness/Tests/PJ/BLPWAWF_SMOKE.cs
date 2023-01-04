 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BLPWAWF_SMOKE : TestScript
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
new Control("Billing", "xpath","//div[@class='deptItem'][.='Billing']").Click();
new Control("Standard Bills Processing", "xpath","//div[@class='navItem'][.='Standard Bills Processing']").Click();
new Control("Create iRAPT Billing Files", "xpath","//div[@class='navItem'][.='Create iRAPT Billing Files']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "BLPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWAWF] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BLPWAWF_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BLPWAWF_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BLPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWAWF] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control BLPWAWF_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,BLPWAWF_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "BLPWAWF";
							CPCommon.WaitControlDisplayed(BLPWAWF_MainForm);
IWebElement formBttn = BLPWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BLPWAWF_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BLPWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BLPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWAWF] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control BLPWAWF_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWAWF_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Project Non Contiguous Ranges");


												
				CPCommon.CurrentComponent = "BLPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWAWF] Perfoming VerifyExists on Identification_ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control BLPWAWF_Identification_ProjectNonContiguousRangesLink = new Control("Identification_ProjectNonContiguousRangesLink", "ID", "lnk_1007699_BLPWAWF_PROCESS");
			CPCommon.AssertEqual(true,BLPWAWF_Identification_ProjectNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "BLPWAWF";
							CPCommon.WaitControlDisplayed(BLPWAWF_Identification_ProjectNonContiguousRangesLink);
BLPWAWF_Identification_ProjectNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BLPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWAWF] Perfoming VerifyExist on ProjectNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control BLPWAWF_ProjectNonContiguousRanges_Table = new Control("ProjectNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BLPWAWF_ProjectNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "BLPWAWF";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BLPWAWF] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control BLPWAWF_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BLPWAWF_ProjectNonContiguousRangesForm);
formBttn = BLPWAWF_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "BLPWAWF";
							CPCommon.WaitControlDisplayed(BLPWAWF_MainForm);
formBttn = BLPWAWF_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

