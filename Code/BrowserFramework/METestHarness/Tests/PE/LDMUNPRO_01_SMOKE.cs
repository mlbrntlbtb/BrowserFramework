 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class LDMUNPRO_01_SMOKE : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
			bool ret = true;
			ErrorMessage = string.Empty;
			try
			{
				CPCommon.Login("default", out ErrorMessage);
							Driver.SessionLogger.WriteLine("START");


												
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming SelectMenu on NavMenu...", Logger.MessageType.INF);
			Control CP7Main_NavMenu = new Control("NavMenu", "ID", "navCont");
			if(!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed) new Control("Browse", "css", "span[id = 'goToLbl']").Click();
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Labor", "xpath","//div[@class='deptItem'][.='Labor']").Click();
new Control("Union Information Controls", "xpath","//div[@class='navItem'][.='Union Information Controls']").Click();
new Control("Manage Union Profile Setup", "xpath","//div[@class='navItem'][.='Manage Union Profile Setup']").Click();


												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExists on UnionProfilesForm...", Logger.MessageType.INF);
			Control LDMUNPRO_UnionProfilesForm = new Control("UnionProfilesForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,LDMUNPRO_UnionProfilesForm.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExists on UnionProfiles_Union...", Logger.MessageType.INF);
			Control LDMUNPRO_UnionProfiles_Union = new Control("UnionProfiles_Union", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='UNION_ID']");
			CPCommon.AssertEqual(true,LDMUNPRO_UnionProfiles_Union.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
							CPCommon.WaitControlDisplayed(LDMUNPRO_UnionProfilesForm);
IWebElement formBttn = LDMUNPRO_UnionProfilesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? LDMUNPRO_UnionProfilesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
LDMUNPRO_UnionProfilesForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExist on UnionProfilesFormTable...", Logger.MessageType.INF);
			Control LDMUNPRO_UnionProfilesFormTable = new Control("UnionProfilesFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMUNPRO_UnionProfilesFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExists on ProjectInformationLink...", Logger.MessageType.INF);
			Control LDMUNPRO_ProjectInformationLink = new Control("ProjectInformationLink", "ID", "lnk_4560_LDMUNPRO_UNIONPROFLE_HDR");
			CPCommon.AssertEqual(true,LDMUNPRO_ProjectInformationLink.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
							CPCommon.WaitControlDisplayed(LDMUNPRO_ProjectInformationLink);
LDMUNPRO_ProjectInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExist on ProjectInformationFormTable...", Logger.MessageType.INF);
			Control LDMUNPRO_ProjectInformationFormTable = new Control("ProjectInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMUNPRO_UNIONPROFLEPROJ_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMUNPRO_ProjectInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming Close on ProjectInformationForm...", Logger.MessageType.INF);
			Control LDMUNPRO_ProjectInformationForm = new Control("ProjectInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMUNPRO_UNIONPROFLEPROJ_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(LDMUNPRO_ProjectInformationForm);
formBttn = LDMUNPRO_ProjectInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExists on FringeInformationLink...", Logger.MessageType.INF);
			Control LDMUNPRO_FringeInformationLink = new Control("FringeInformationLink", "ID", "lnk_4562_LDMUNPRO_UNIONPROFLE_HDR");
			CPCommon.AssertEqual(true,LDMUNPRO_FringeInformationLink.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
							CPCommon.WaitControlDisplayed(LDMUNPRO_FringeInformationLink);
LDMUNPRO_FringeInformationLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExist on FringeInformationFormTable...", Logger.MessageType.INF);
			Control LDMUNPRO_FringeInformationFormTable = new Control("FringeInformationFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMUNPRO_UNIONPROFLEFRN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMUNPRO_FringeInformationFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming Close on FringeInformationForm...", Logger.MessageType.INF);
			Control LDMUNPRO_FringeInformationForm = new Control("FringeInformationForm", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMUNPRO_UNIONPROFLEFRN_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(LDMUNPRO_FringeInformationForm);
formBttn = LDMUNPRO_FringeInformationForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExists on DeductionsLink...", Logger.MessageType.INF);
			Control LDMUNPRO_DeductionsLink = new Control("DeductionsLink", "ID", "lnk_4564_LDMUNPRO_UNIONPROFLE_HDR");
			CPCommon.AssertEqual(true,LDMUNPRO_DeductionsLink.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
							CPCommon.WaitControlDisplayed(LDMUNPRO_DeductionsLink);
LDMUNPRO_DeductionsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "LDMUNPRO";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[LDMUNPRO] Perfoming VerifyExist on DeductionsFormTable...", Logger.MessageType.INF);
			Control LDMUNPRO_DeductionsFormTable = new Control("DeductionsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__LDMUNPRO_UNIONPROFLEDED_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,LDMUNPRO_DeductionsFormTable.Exists());

												
				CPCommon.CurrentComponent = "LDMUNPRO";
							CPCommon.WaitControlDisplayed(LDMUNPRO_UnionProfilesForm);
formBttn = LDMUNPRO_UnionProfilesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

