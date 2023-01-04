 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJQETC_SMOKE : TestScript
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
new Control("Budgeting and ETC", "xpath","//div[@class='deptItem'][.='Budgeting and ETC']").Click();
new Control("Estimate To Complete Reports/Inquiries", "xpath","//div[@class='navItem'][.='Estimate To Complete Reports/Inquiries']").Click();
new Control("View Estimate To Complete", "xpath","//div[@class='navItem'][.='View Estimate To Complete']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PJQETC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PJQETC_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJQETC_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJQETC_Project.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetailsLink...", Logger.MessageType.INF);
			Control PJQETC_ETCDetailsLink = new Control("ETCDetailsLink", "ID", "lnk_1005084_PJQETC_HDR");
			CPCommon.AssertEqual(true,PJQETC_ETCDetailsLink.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							PJQETC_Project.Click();
PJQETC_Project.SendKeys("0400", true);
CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));
PJQETC_Project.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);


													
				CPCommon.CurrentComponent = "CP7Main";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[CP7Main] Perfoming ClickToolbarButton on MainToolBar...", Logger.MessageType.INF);
			Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
			CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Execute')]")).FirstOrDefault();
if (tlbrBtn==null) throw new Exception("Unable to find button Execute.");
tlbrBtn.Click();


											Driver.SessionLogger.WriteLine("ETC Details");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetailsForm...", Logger.MessageType.INF);
			Control PJQETC_ETCDetailsForm = new Control("ETCDetailsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQETC_ETCDetailsForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetailsForm);
IWebElement formBttn = PJQETC_ETCDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJQETC_ETCDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJQETC_ETCDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Query not found ");


													
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExist on ETCDetailsFormTable...", Logger.MessageType.INF);
			Control PJQETC_ETCDetailsFormTable = new Control("ETCDetailsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetailsFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetailsForm);
formBttn = PJQETC_ETCDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQETC_ETCDetailsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQETC_ETCDetailsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_ProjectManager...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectManager = new Control("ETCDetails_ProjectManager", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CTW_']/ancestor::form[1]/descendant::*[@id='PROJ_MGR_NAME']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectManager.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.AssertEqual(true,PJQETC_ETCDetailsLink.Exists());

													
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_CostInfoLink...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_CostInfoLink = new Control("ETCDetails_CostInfoLink", "ID", "lnk_1005085_PJQETC_RPTETCAMT_CTW");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_CostInfoLink.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_ProjectCostDetailLink...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectCostDetailLink = new Control("ETCDetails_ProjectCostDetailLink", "ID", "lnk_1005087_PJQETC_RPTETCAMT_CTW");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectCostDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_OrgDetailLink...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_OrgDetailLink = new Control("ETCDetails_OrgDetailLink", "ID", "lnk_1005089_PJQETC_RPTETCAMT_CTW");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_OrgDetailLink.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_CostInfoLink);
PJQETC_ETCDetails_CostInfoLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Cost Info");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_CostInfoForm...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_CostInfoForm = new Control("ETCDetails_CostInfoForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_ETCUPDDT_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_CostInfoForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_CostInfo_DateUpdated_Incurred...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_CostInfo_DateUpdated_Incurred = new Control("ETCDetails_CostInfo_DateUpdated_Incurred", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_ETCUPDDT_HDR_']/ancestor::form[1]/descendant::*[@id='INC_AMT_UPD_DT']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_CostInfo_DateUpdated_Incurred.Exists());

											Driver.SessionLogger.WriteLine("Cost Info Detail");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_CostInfo_CostInfoDetailForm...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_CostInfo_CostInfoDetailForm = new Control("ETCDetails_CostInfo_CostInfoDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_CostInfo_CostInfoDetailForm);
formBttn = PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExist on ETCDetails_CostInfo_CostInfoDetailFormTable...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_CostInfo_CostInfoDetailFormTable = new Control("ETCDetails_CostInfo_CostInfoDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_CostInfo_CostInfoDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_CostInfo_CostInfoDetailForm);
formBttn = PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQETC_ETCDetails_CostInfo_CostInfoDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_CostInfo_CostInfoDetail_IncurredCost_Labor...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_CostInfo_CostInfoDetail_IncurredCost_Labor = new Control("ETCDetails_CostInfo_CostInfoDetail_IncurredCost_Labor", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CHLD_']/ancestor::form[1]/descendant::*[@id='INCLABOR']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_CostInfo_CostInfoDetail_IncurredCost_Labor.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_CostInfoForm);
formBttn = PJQETC_ETCDetails_CostInfoForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_ProjectCostDetailLink);
PJQETC_ETCDetails_ProjectCostDetailLink.Click(1.5);


												Driver.SessionLogger.WriteLine("ProjectCostDetail");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_ProjectCostDetailForm...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectCostDetailForm = new Control("ETCDetails_ProjectCostDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_ETCUPDDT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectCostDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_ProjectCostDetail_DateUpdated_Incurred...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectCostDetail_DateUpdated_Incurred = new Control("ETCDetails_ProjectCostDetail_DateUpdated_Incurred", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_ETCUPDDT_CHLD_']/ancestor::form[1]/descendant::*[@id='INC_AMT_UPD_DT']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectCostDetail_DateUpdated_Incurred.Exists());

											Driver.SessionLogger.WriteLine("ProjectCostDetail_ProjectCostDetail");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_ProjectCostDetail_ProjectCostDetailForm...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailForm = new Control("ETCDetails_ProjectCostDetail_ProjectCostDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CHLD_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExist on ETCDetails_ProjectCostDetail_ProjectCostDetailFormTable...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailFormTable = new Control("ETCDetails_ProjectCostDetail_ProjectCostDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailForm);
formBttn = PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_ProjectCostDetail_ProjectCostDetail_Description...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetail_Description = new Control("ETCDetails_ProjectCostDetail_ProjectCostDetail_Description", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_RPTETCAMT_CHLD_']/ancestor::form[1]/descendant::*[@id='DESCRIPTION']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_ProjectCostDetail_ProjectCostDetail_Description.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_ProjectCostDetailForm);
formBttn = PJQETC_ETCDetails_ProjectCostDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


													
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_OrgDetailLink);
PJQETC_ETCDetails_OrgDetailLink.Click(1.5);


												Driver.SessionLogger.WriteLine("Org Detail");


												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_OrgDetailForm...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_OrgDetailForm = new Control("ETCDetails_OrgDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_ETCUPDDT_ORGDETAIL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_OrgDetailForm.Exists());

												
				CPCommon.CurrentComponent = "PJQETC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJQETC] Perfoming VerifyExists on ETCDetails_OrgDetail_DateUpdated_Incurred...", Logger.MessageType.INF);
			Control PJQETC_ETCDetails_OrgDetail_DateUpdated_Incurred = new Control("ETCDetails_OrgDetail_DateUpdated_Incurred", "xpath", "//div[translate(@id,'0123456789','')='pr__PJQETC_ETCUPDDT_ORGDETAIL_']/ancestor::form[1]/descendant::*[@id='INC_AMT_UPD_DT']");
			CPCommon.AssertEqual(true,PJQETC_ETCDetails_OrgDetail_DateUpdated_Incurred.Exists());

											Driver.SessionLogger.WriteLine("Org Detail_Org Detail");


												
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_ETCDetails_OrgDetailForm);
formBttn = PJQETC_ETCDetails_OrgDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PJQETC_ETCDetails_OrgDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PJQETC_ETCDetails_OrgDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "PJQETC";
							CPCommon.WaitControlDisplayed(PJQETC_MainForm);
formBttn = PJQETC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

