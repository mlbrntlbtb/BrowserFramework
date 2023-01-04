 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class MRRBMAM_SMOKE : TestScript
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
new Control("Print Bills of Material MRP Action Message Report", "xpath","//div[@class='navItem'][.='Print Bills of Material MRP Action Message Report']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "MRRBMAM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRBMAM] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control MRRBMAM_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,MRRBMAM_MainForm.Exists());

												
				CPCommon.CurrentComponent = "MRRBMAM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRBMAM] Perfoming VerifyExists on MainForm_ParameterID...", Logger.MessageType.INF);
			Control MRRBMAM_MainForm_ParameterID = new Control("MainForm_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,MRRBMAM_MainForm_ParameterID.Exists());

											Driver.SessionLogger.WriteLine("ASSEMBLY PART/REV NON CONTIGUOUS RANGES LINK");


												
				CPCommon.CurrentComponent = "MRRBMAM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRBMAM] Perfoming VerifyExists on MainForm_SelectionRanges_AssemblyPartRevNonContiguousRangesLink...", Logger.MessageType.INF);
			Control MRRBMAM_MainForm_SelectionRanges_AssemblyPartRevNonContiguousRangesLink = new Control("MainForm_SelectionRanges_AssemblyPartRevNonContiguousRangesLink", "ID", "lnk_15105_MRRBMAM_PARAM");
			CPCommon.AssertEqual(true,MRRBMAM_MainForm_SelectionRanges_AssemblyPartRevNonContiguousRangesLink.Exists());

												
				CPCommon.CurrentComponent = "MRRBMAM";
							CPCommon.WaitControlDisplayed(MRRBMAM_MainForm_SelectionRanges_AssemblyPartRevNonContiguousRangesLink);
MRRBMAM_MainForm_SelectionRanges_AssemblyPartRevNonContiguousRangesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "MRRBMAM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRBMAM] Perfoming VerifyExist on AssemblyPartRevContiguousRangesFormTable...", Logger.MessageType.INF);
			Control MRRBMAM_AssemblyPartRevContiguousRangesFormTable = new Control("AssemblyPartRevContiguousRangesFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MRRBMAM_NCR_ASYPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MRRBMAM_AssemblyPartRevContiguousRangesFormTable.Exists());

												
				CPCommon.CurrentComponent = "MRRBMAM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRBMAM] Perfoming Close on AssemblyPartRevContiguousRangesForm...", Logger.MessageType.INF);
			Control MRRBMAM_AssemblyPartRevContiguousRangesForm = new Control("AssemblyPartRevContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MRRBMAM_NCR_ASYPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(MRRBMAM_AssemblyPartRevContiguousRangesForm);
IWebElement formBttn = MRRBMAM_AssemblyPartRevContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("MAIN FORM TABLE");


												
				CPCommon.CurrentComponent = "MRRBMAM";
							CPCommon.WaitControlDisplayed(MRRBMAM_MainForm);
formBttn = MRRBMAM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? MRRBMAM_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
MRRBMAM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "MRRBMAM";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[MRRBMAM] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control MRRBMAM_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,MRRBMAM_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "MRRBMAM";
							CPCommon.WaitControlDisplayed(MRRBMAM_MainForm);
formBttn = MRRBMAM_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

