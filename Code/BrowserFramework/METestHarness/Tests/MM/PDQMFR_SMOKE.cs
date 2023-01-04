 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDQMFR_SMOKE : TestScript
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
new Control("Product Definition", "xpath","//div[@class='deptItem'][.='Product Definition']").Click();
new Control("Product Definition Reports/Inquiries", "xpath","//div[@class='navItem'][.='Product Definition Reports/Inquiries']").Click();
new Control("View Alternate Parts", "xpath","//div[@class='navItem'][.='View Alternate Parts']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PDQMFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQMFR] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDQMFR_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDQMFR_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDQMFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQMFR] Perfoming VerifyExists on Select_Manufacturer...", Logger.MessageType.INF);
			Control PDQMFR_Select_Manufacturer = new Control("Select_Manufacturer", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='SELECTCD' and @value='M']");
			CPCommon.AssertEqual(true,PDQMFR_Select_Manufacturer.Exists());

											Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "PDQMFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQMFR] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PDQMFR_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQMFR_ALTPART_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDQMFR_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDQMFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQMFR] Perfoming ClickButton on ChildForm...", Logger.MessageType.INF);
			Control PDQMFR_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQMFR_ALTPART_DTL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDQMFR_ChildForm);
IWebElement formBttn = PDQMFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDQMFR_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDQMFR_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDQMFR";
							CPCommon.AssertEqual(true,PDQMFR_ChildForm.Exists());

													
				CPCommon.CurrentComponent = "PDQMFR";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDQMFR] Perfoming VerifyExists on ChildForm_Part...", Logger.MessageType.INF);
			Control PDQMFR_ChildForm_Part = new Control("ChildForm_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__PDQMFR_ALTPART_DTL_']/ancestor::form[1]/descendant::*[@id='PART_ID']");
			CPCommon.AssertEqual(true,PDQMFR_ChildForm_Part.Exists());

											Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDQMFR";
							CPCommon.WaitControlDisplayed(PDQMFR_MainForm);
formBttn = PDQMFR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

