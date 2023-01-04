 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PJMPLCPJ_SMOKE : TestScript
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
new Control("Project Setup", "xpath","//div[@class='deptItem'][.='Project Setup']").Click();
new Control("Project Labor", "xpath","//div[@class='navItem'][.='Project Labor']").Click();
new Control("Link Project Labor Categories to Projects", "xpath","//div[@class='navItem'][.='Link Project Labor Categories to Projects']").Click();


											Driver.SessionLogger.WriteLine("QUERY");


												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control PJMPLCPJ_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(PJMPLCPJ_MainForm);
IWebElement formBttn = PJMPLCPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? PJMPLCPJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
PJMPLCPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


											Driver.SessionLogger.WriteLine("MAIN FORM");


												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExist on MainTable...", Logger.MessageType.INF);
			Control PJMPLCPJ_MainTable = new Control("MainTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCPJ_MainTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
							CPCommon.WaitControlDisplayed(PJMPLCPJ_MainForm);
formBttn = PJMPLCPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PJMPLCPJ_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PJMPLCPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PJMPLCPJ";
							CPCommon.AssertEqual(true,PJMPLCPJ_MainForm.Exists());

													
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExists on Project...", Logger.MessageType.INF);
			Control PJMPLCPJ_Project = new Control("Project", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PROJ_ID']");
			CPCommon.AssertEqual(true,PJMPLCPJ_Project.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExist on ProjectLaborCategoriesTable...", Logger.MessageType.INF);
			Control PJMPLCPJ_ProjectLaborCategoriesTable = new Control("ProjectLaborCategoriesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_BILLLABCAT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCPJ_ProjectLaborCategoriesTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExist on AssignPLCTable...", Logger.MessageType.INF);
			Control PJMPLCPJ_AssignPLCTable = new Control("AssignPLCTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCAT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCPJ_AssignPLCTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExists on AssignPLC_LinkGLCToProjectPLCLink...", Logger.MessageType.INF);
			Control PJMPLCPJ_AssignPLC_LinkGLCToProjectPLCLink = new Control("AssignPLC_LinkGLCToProjectPLCLink", "ID", "lnk_1007349_PJM_PROJLABCAT_CTW");
			CPCommon.AssertEqual(true,PJMPLCPJ_AssignPLC_LinkGLCToProjectPLCLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
							CPCommon.WaitControlDisplayed(PJMPLCPJ_AssignPLC_LinkGLCToProjectPLCLink);
PJMPLCPJ_AssignPLC_LinkGLCToProjectPLCLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExist on GeneralLaborCategoriesTable...", Logger.MessageType.INF);
			Control PJMPLCPJ_GeneralLaborCategoriesTable = new Control("GeneralLaborCategoriesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_GENLLABCAT_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCPJ_GeneralLaborCategoriesTable.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExists on LinkedGLCtoProjectPLCForm...", Logger.MessageType.INF);
			Control PJMPLCPJ_LinkedGLCtoProjectPLCForm = new Control("LinkedGLCtoProjectPLCForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCATMAP_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PJMPLCPJ_LinkedGLCtoProjectPLCForm.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExists on LinkedGLCsLink...", Logger.MessageType.INF);
			Control PJMPLCPJ_LinkedGLCsLink = new Control("LinkedGLCsLink", "ID", "lnk_1007353_PJM_PROJLABCAT_HDR");
			CPCommon.AssertEqual(true,PJMPLCPJ_LinkedGLCsLink.Exists());

												
				CPCommon.CurrentComponent = "PJMPLCPJ";
							CPCommon.WaitControlDisplayed(PJMPLCPJ_LinkedGLCsLink);
PJMPLCPJ_LinkedGLCsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming VerifyExist on LinkedGLCTable...", Logger.MessageType.INF);
			Control PJMPLCPJ_LinkedGLCTable = new Control("LinkedGLCTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCATMAPPLC_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PJMPLCPJ_LinkedGLCTable.Exists());

											Driver.SessionLogger.WriteLine("CLOSE");


												
				CPCommon.CurrentComponent = "PJMPLCPJ";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PJMPLCPJ] Perfoming Close on LinkedGLCForm...", Logger.MessageType.INF);
			Control PJMPLCPJ_LinkedGLCForm = new Control("LinkedGLCForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PJM_PROJLABCATMAPPLC_CTW_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(PJMPLCPJ_LinkedGLCForm);
formBttn = PJMPLCPJ_LinkedGLCForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												
				CPCommon.CurrentComponent = "PJMPLCPJ";
							CPCommon.WaitControlDisplayed(PJMPLCPJ_MainForm);
formBttn = PJMPLCPJ_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

