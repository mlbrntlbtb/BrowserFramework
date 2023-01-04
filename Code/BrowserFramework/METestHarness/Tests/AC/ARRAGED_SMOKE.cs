 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class ARRAGED_SMOKE : TestScript
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
new Control("Accounts Receivable Reports/Inquiries", "xpath","//div[@class='navItem'][.='Accounts Receivable Reports/Inquiries']").Click();
new Control("Print Accounts Receivable Aging Report", "xpath","//div[@class='navItem'][.='Print Accounts Receivable Aging Report']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExists on Identification_ParameterID...", Logger.MessageType.INF);
			Control ARRAGED_Identification_ParameterID = new Control("Identification_ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,ARRAGED_Identification_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control ARRAGED_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_MainForm);
IWebElement formBttn = ARRAGED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? ARRAGED_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
ARRAGED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on MainForm_Table...", Logger.MessageType.INF);
			Control ARRAGED_MainForm_Table = new Control("MainForm_Table", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_MainForm_Table.Exists());

											Driver.SessionLogger.WriteLine("Customer Non Contiguous Ranges");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_CustomerNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_CustomerNonContiguousRangesLink = new Control("Identification_CustomerNonContiguousRangesLink", "ID", "lnk_1006661_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_CustomerNonContiguousRangesLink);
ARRAGED_Identification_CustomerNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on CustomerNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_CustomerNonContiguousRanges_Table = new Control("CustomerNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_CustomerNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on CustomerNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_CustomerNonContiguousRangesForm = new Control("CustomerNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_CustomerNonContiguousRangesForm);
formBttn = ARRAGED_CustomerNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Project Non Contigous Ranges");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_ProjectNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_ProjectNonContiguousRangesLink = new Control("Identification_ProjectNonContiguousRangesLink", "ID", "lnk_1006662_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_ProjectNonContiguousRangesLink);
ARRAGED_Identification_ProjectNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on ProjectNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_ProjectNonContiguousRanges_Table = new Control("ProjectNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_ProjectNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on ProjectNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_ProjectNonContiguousRangesForm = new Control("ProjectNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRPROJID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_ProjectNonContiguousRangesForm);
formBttn = ARRAGED_ProjectNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Account Non Contigouos");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_AccountNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_AccountNonContiguousRangesLink = new Control("Identification_AccountNonContiguousRangesLink", "ID", "lnk_1006665_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_AccountNonContiguousRangesLink);
ARRAGED_Identification_AccountNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on AccountNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_AccountNonContiguousRanges_Table = new Control("AccountNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRACCTID_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_AccountNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on AccountNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_AccountNonContiguousRangesForm = new Control("AccountNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRACCTID_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_AccountNonContiguousRangesForm);
formBttn = ARRAGED_AccountNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Organization Non Contigouos");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_OrganizationNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_OrganizationNonContiguousRangesLink = new Control("Identification_OrganizationNonContiguousRangesLink", "ID", "lnk_1006666_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_OrganizationNonContiguousRangesLink);
ARRAGED_Identification_OrganizationNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on OrganizationNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_OrganizationNonContiguousRanges_Table = new Control("OrganizationNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_OrganizationNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on OrganizationNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_OrganizationNonContiguousRangesForm = new Control("OrganizationNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRORGID_NCR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_OrganizationNonContiguousRangesForm);
formBttn = ARRAGED_OrganizationNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Customer Name Non Contigouos");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_CustomerNameNonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_CustomerNameNonContiguousRangesLink = new Control("Identification_CustomerNameNonContiguousRangesLink", "ID", "lnk_1006667_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_CustomerNameNonContiguousRangesLink);
ARRAGED_Identification_CustomerNameNonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on CustomerNameNonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_CustomerNameNonContiguousRanges_Table = new Control("CustomerNameNonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTNAME_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_CustomerNameNonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on CustomerNameNonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_CustomerNameNonContiguousRangesForm = new Control("CustomerNameNonContiguousRangesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRCUSTNAME_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_CustomerNameNonContiguousRangesForm);
formBttn = ARRAGED_CustomerNameNonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("User Def1 Non Contigous");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_UserDef1NonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_UserDef1NonContiguousRangesLink = new Control("Identification_UserDef1NonContiguousRangesLink", "ID", "lnk_1006674_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_UserDef1NonContiguousRangesLink);
ARRAGED_Identification_UserDef1NonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on UserDef1NonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_UserDef1NonContiguousRanges_Table = new Control("UserDef1NonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRUSERDEFFLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_UserDef1NonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on UserDef1NonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_UserDef1NonContiguousRangesForm = new Control("UserDef1NonContiguousRangesForm", "xpath", "//div[starts-with(@id,'pr__CPP_NCRUSERDEF1FLD_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_UserDef1NonContiguousRangesForm);
formBttn = ARRAGED_UserDef1NonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("User Def2 Non Contigous");


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Click on Identification_UserDef2NonContiguousRangesLink...", Logger.MessageType.INF);
			Control ARRAGED_Identification_UserDef2NonContiguousRangesLink = new Control("Identification_UserDef2NonContiguousRangesLink", "ID", "lnk_1006675_ARRAGED_PARAM");
			CPCommon.WaitControlDisplayed(ARRAGED_Identification_UserDef2NonContiguousRangesLink);
ARRAGED_Identification_UserDef2NonContiguousRangesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming VerifyExist on UserDef2NonContiguousRanges_Table...", Logger.MessageType.INF);
			Control ARRAGED_UserDef2NonContiguousRanges_Table = new Control("UserDef2NonContiguousRanges_Table", "xpath", "//div[translate(@id,'0123456789','')='pr__CPP_NCRUSERDEFFLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,ARRAGED_UserDef2NonContiguousRanges_Table.Exists());

												
				CPCommon.CurrentComponent = "ARRAGED";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[ARRAGED] Perfoming Close on UserDef2NonContiguousRangesForm...", Logger.MessageType.INF);
			Control ARRAGED_UserDef2NonContiguousRangesForm = new Control("UserDef2NonContiguousRangesForm", "xpath", "//div[starts-with(@id,'pr__CPP_NCRUSERDEF2FLD_')]/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(ARRAGED_UserDef2NonContiguousRangesForm);
formBttn = ARRAGED_UserDef2NonContiguousRangesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


											Driver.SessionLogger.WriteLine("Closing App");


												
				CPCommon.CurrentComponent = "ARRAGED";
							CPCommon.WaitControlDisplayed(ARRAGED_MainForm);
formBttn = ARRAGED_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

