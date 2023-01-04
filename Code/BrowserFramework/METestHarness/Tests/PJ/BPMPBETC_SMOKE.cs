 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class BPMPBETC_SMOKE : TestScript
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
new Control("Advanced Project Budgeting", "xpath","//div[@class='deptItem'][.='Advanced Project Budgeting']").Click();
new Control("Project Budget and Estimate To Complete", "xpath","//div[@class='navItem'][.='Project Budget and Estimate To Complete']").Click();
new Control("Manage Project Budgets and ETC", "xpath","//div[@class='navItem'][.='Manage Project Budgets and ETC']").Click();


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control BPMPBETC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control BPMPBETC_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,BPMPBETC_Project.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on TotalBudget_Quantity_Direct...", Logger.MessageType.INF);
			Control BPMPBETC_TotalBudget_Quantity_Direct = new Control("TotalBudget_Quantity_Direct", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='QNTY_DIRECT']");
			CPCommon.AssertEqual(true,BPMPBETC_TotalBudget_Quantity_Direct.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_MainForm);
IWebElement formBttn = BPMPBETC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? BPMPBETC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
BPMPBETC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control BPMPBETC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("ETC");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on ETCLink...", Logger.MessageType.INF);
			Control BPMPBETC_ETCLink = new Control("ETCLink", "ID", "lnk_3377_BPMPBETC_PROJBUDGETINFO_HDRINF");
			CPCommon.AssertEqual(true,BPMPBETC_ETCLink.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_ETCLink);
BPMPBETC_ETCLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on ETCForm...", Logger.MessageType.INF);
			Control BPMPBETC_ETCForm = new Control("ETCForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_DUMMY_ETCHDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_ETCForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on ETCChildForm...", Logger.MessageType.INF);
			Control BPMPBETC_ETCChildForm = new Control("ETCChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJETCSUM_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_ETCChildForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on ETCChildTable...", Logger.MessageType.INF);
			Control BPMPBETC_ETCChildTable = new Control("ETCChildTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJETCSUM_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_ETCChildTable.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_ETCForm);
formBttn = BPMPBETC_ETCForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("INDIRECT");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on IndirectLink...", Logger.MessageType.INF);
			Control BPMPBETC_IndirectLink = new Control("IndirectLink", "ID", "lnk_1002590_BPMPBETC_PROJBUDGETINFO_HDRINF");
			CPCommon.AssertEqual(true,BPMPBETC_IndirectLink.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_IndirectLink);
BPMPBETC_IndirectLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on IndirectTable...", Logger.MessageType.INF);
			Control BPMPBETC_IndirectTable = new Control("IndirectTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETINDIR_INDIR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_IndirectTable.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming ClickButton on IndirectForm...", Logger.MessageType.INF);
			Control BPMPBETC_IndirectForm = new Control("IndirectForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETINDIR_INDIR_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(BPMPBETC_IndirectForm);
formBttn = BPMPBETC_IndirectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? BPMPBETC_IndirectForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
BPMPBETC_IndirectForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.AssertEqual(true,BPMPBETC_IndirectForm.Exists());

													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on Indirect_Organization_Organization...", Logger.MessageType.INF);
			Control BPMPBETC_Indirect_Organization_Organization = new Control("Indirect_Organization_Organization", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETINDIR_INDIR_']/ancestor::form[1]/descendant::*[@id='ORG_ID']");
			CPCommon.AssertEqual(true,BPMPBETC_Indirect_Organization_Organization.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_IndirectForm);
formBttn = BPMPBETC_IndirectForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("FEES");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on FeesLink...", Logger.MessageType.INF);
			Control BPMPBETC_FeesLink = new Control("FeesLink", "ID", "lnk_1002591_BPMPBETC_PROJBUDGETINFO_HDRINF");
			CPCommon.AssertEqual(true,BPMPBETC_FeesLink.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_FeesLink);
BPMPBETC_FeesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on FeesForm...", Logger.MessageType.INF);
			Control BPMPBETC_FeesForm = new Control("FeesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETFEE_CHILD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_FeesForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on FeesTable...", Logger.MessageType.INF);
			Control BPMPBETC_FeesTable = new Control("FeesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETFEE_CHILD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_FeesTable.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_FeesForm);
formBttn = BPMPBETC_FeesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("UNITS");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on UnitsLink...", Logger.MessageType.INF);
			Control BPMPBETC_UnitsLink = new Control("UnitsLink", "ID", "lnk_1002592_BPMPBETC_PROJBUDGETINFO_HDRINF");
			CPCommon.AssertEqual(true,BPMPBETC_UnitsLink.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_UnitsLink);
BPMPBETC_UnitsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on UnitsForm...", Logger.MessageType.INF);
			Control BPMPBETC_UnitsForm = new Control("UnitsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETUNITS_UNITS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_UnitsForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on UnitsTable...", Logger.MessageType.INF);
			Control BPMPBETC_UnitsTable = new Control("UnitsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETUNITS_UNITS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_UnitsTable.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_UnitsForm);
formBttn = BPMPBETC_UnitsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("LABOR");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on LaborLink...", Logger.MessageType.INF);
			Control BPMPBETC_LaborLink = new Control("LaborLink", "ID", "lnk_1002594_BPMPBETC_PROJBUDGETINFO_HDRINF");
			CPCommon.AssertEqual(true,BPMPBETC_LaborLink.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_LaborLink);
BPMPBETC_LaborLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on LaborForm...", Logger.MessageType.INF);
			Control BPMPBETC_LaborForm = new Control("LaborForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETLABOR_LABOR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_LaborForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on LaborTable...", Logger.MessageType.INF);
			Control BPMPBETC_LaborTable = new Control("LaborTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETLABOR_LABOR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_LaborTable.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_LaborForm);
formBttn = BPMPBETC_LaborForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("DIRECT");


												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on DirectLink...", Logger.MessageType.INF);
			Control BPMPBETC_DirectLink = new Control("DirectLink", "ID", "lnk_1002596_BPMPBETC_PROJBUDGETINFO_HDRINF");
			CPCommon.AssertEqual(true,BPMPBETC_DirectLink.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_DirectLink);
BPMPBETC_DirectLink.Click(1.5);


													
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExists on DirectForm...", Logger.MessageType.INF);
			Control BPMPBETC_DirectForm = new Control("DirectForm", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETDIRECT_DIR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,BPMPBETC_DirectForm.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[BPMPBETC] Perfoming VerifyExist on DirectTable...", Logger.MessageType.INF);
			Control BPMPBETC_DirectTable = new Control("DirectTable", "xpath", "//div[translate(@id,'0123456789','')='pr__BPMPBETC_PROJBUDGETDIRECT_DIR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,BPMPBETC_DirectTable.Exists());

												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_DirectForm);
formBttn = BPMPBETC_DirectForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "BPMPBETC";
							CPCommon.WaitControlDisplayed(BPMPBETC_MainForm);
formBttn = BPMPBETC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

