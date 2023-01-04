 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDMPRJPD_SMOKE : TestScript
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
new Control("Items", "xpath","//div[@class='navItem'][.='Items']").Click();
new Control("Manage Part Project Data", "xpath","//div[@class='navItem'][.='Manage Part Project Data']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDMPRJPD_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRJPD_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PDMPRJPD_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PDMPRJPD_MainForm);
IWebElement formBttn = PDMPRJPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRJPD_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRJPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRJPD";
							CPCommon.AssertEqual(true,PDMPRJPD_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming VerifyExists on Part...", Logger.MessageType.INF);
			Control PDMPRJPD_Part = new Control("Part", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='ITEM_ID']");
			CPCommon.AssertEqual(true,PDMPRJPD_Part.Exists());

											Driver.SessionLogger.WriteLine("LINK");


												
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming Click on SubstituteProjectPartsLink...", Logger.MessageType.INF);
			Control PDMPRJPD_SubstituteProjectPartsLink = new Control("SubstituteProjectPartsLink", "ID", "lnk_1006551_PDMPRJPD_PARTPROJ");
			CPCommon.WaitControlDisplayed(PDMPRJPD_SubstituteProjectPartsLink);
PDMPRJPD_SubstituteProjectPartsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming VerifyExist on ProjectSubstitutePartsFormTable...", Logger.MessageType.INF);
			Control PDMPRJPD_ProjectSubstitutePartsFormTable = new Control("ProjectSubstitutePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJPD_PROJSUBSTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDMPRJPD_ProjectSubstitutePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming ClickButton on ProjectSubstitutePartsForm...", Logger.MessageType.INF);
			Control PDMPRJPD_ProjectSubstitutePartsForm = new Control("ProjectSubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJPD_PROJSUBSTPART_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PDMPRJPD_ProjectSubstitutePartsForm);
formBttn = PDMPRJPD_ProjectSubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDMPRJPD_ProjectSubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDMPRJPD_ProjectSubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "PDMPRJPD";
							CPCommon.AssertEqual(true,PDMPRJPD_ProjectSubstitutePartsForm.Exists());

													
				CPCommon.CurrentComponent = "PDMPRJPD";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDMPRJPD] Perfoming VerifyExists on ProjectSubstituteParts_Sequence...", Logger.MessageType.INF);
			Control PDMPRJPD_ProjectSubstituteParts_Sequence = new Control("ProjectSubstituteParts_Sequence", "xpath", "//div[translate(@id,'0123456789','')='pr__PDMPRJPD_PROJSUBSTPART_']/ancestor::form[1]/descendant::*[@id='USAGE_SEQ_NO']");
			CPCommon.AssertEqual(true,PDMPRJPD_ProjectSubstituteParts_Sequence.Exists());

												
				CPCommon.CurrentComponent = "PDMPRJPD";
							CPCommon.WaitControlDisplayed(PDMPRJPD_MainForm);
formBttn = PDMPRJPD_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

