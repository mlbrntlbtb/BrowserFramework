 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class HPMEDS_SMOKE : TestScript
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
new Control("People", "xpath","//div[@class='busItem'][.='People']").Click();
new Control("Employee", "xpath","//div[@class='deptItem'][.='Employee']").Click();
new Control("Basic Employee Information", "xpath","//div[@class='navItem'][.='Basic Employee Information']").Click();
new Control("Manage Education, Skills & Training Data", "xpath","//div[@class='navItem'][.='Manage Education, Skills & Training Data']").Click();


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control HPMEDS_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,HPMEDS_MainForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on Employee...", Logger.MessageType.INF);
			Control HPMEDS_Employee = new Control("Employee", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='EMPL_ID']");
			CPCommon.AssertEqual(true,HPMEDS_Employee.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_MainForm);
IWebElement formBttn = HPMEDS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? HPMEDS_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
HPMEDS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control HPMEDS_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("DegreesForm");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on DegreesLink...", Logger.MessageType.INF);
			Control HPMEDS_DegreesLink = new Control("DegreesLink", "ID", "lnk_1005958_HPMEDS_HEDSKILLTRAIN_HDR");
			CPCommon.AssertEqual(true,HPMEDS_DegreesLink.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_DegreesLink);
HPMEDS_DegreesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on DegreesLinkForm...", Logger.MessageType.INF);
			Control HPMEDS_DegreesLinkForm = new Control("DegreesLinkForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLDEGREE_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_DegreesLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on DegreesLinkFormTable...", Logger.MessageType.INF);
			Control HPMEDS_DegreesLinkFormTable = new Control("DegreesLinkFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLDEGREE_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_DegreesLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_DegreesLinkForm);
formBttn = HPMEDS_DegreesLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEDS_DegreesLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEDS_DegreesLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on Degrees_Degree...", Logger.MessageType.INF);
			Control HPMEDS_Degrees_Degree = new Control("Degrees_Degree", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLDEGREE_DETL_']/ancestor::form[1]/descendant::*[@id='DEG_ID']");
			CPCommon.AssertEqual(true,HPMEDS_Degrees_Degree.Exists());

											Driver.SessionLogger.WriteLine("CoursesLink");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on CoursesLink...", Logger.MessageType.INF);
			Control HPMEDS_CoursesLink = new Control("CoursesLink", "ID", "lnk_1005959_HPMEDS_HEDSKILLTRAIN_HDR");
			CPCommon.AssertEqual(true,HPMEDS_CoursesLink.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_CoursesLink);
HPMEDS_CoursesLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on CoursesLinkForm...", Logger.MessageType.INF);
			Control HPMEDS_CoursesLinkForm = new Control("CoursesLinkForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLCOURSE_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_CoursesLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on CoursesLinkFormTable...", Logger.MessageType.INF);
			Control HPMEDS_CoursesLinkFormTable = new Control("CoursesLinkFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLCOURSE_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_CoursesLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_CoursesLinkForm);
formBttn = HPMEDS_CoursesLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEDS_CoursesLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEDS_CoursesLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on Courses_Course...", Logger.MessageType.INF);
			Control HPMEDS_Courses_Course = new Control("Courses_Course", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLCOURSE_DETL_']/ancestor::form[1]/descendant::*[@id='COURS_ID']");
			CPCommon.AssertEqual(true,HPMEDS_Courses_Course.Exists());

											Driver.SessionLogger.WriteLine("SkillsLink");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on SkillsLink...", Logger.MessageType.INF);
			Control HPMEDS_SkillsLink = new Control("SkillsLink", "ID", "lnk_1005960_HPMEDS_HEDSKILLTRAIN_HDR");
			CPCommon.AssertEqual(true,HPMEDS_SkillsLink.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_SkillsLink);
HPMEDS_SkillsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on SkillsLinkForm...", Logger.MessageType.INF);
			Control HPMEDS_SkillsLinkForm = new Control("SkillsLinkForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLSKILLS_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_SkillsLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on SkillsLinkFormTable...", Logger.MessageType.INF);
			Control HPMEDS_SkillsLinkFormTable = new Control("SkillsLinkFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLSKILLS_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_SkillsLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_SkillsLinkForm);
formBttn = HPMEDS_SkillsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEDS_SkillsLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEDS_SkillsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on Skills_Skill...", Logger.MessageType.INF);
			Control HPMEDS_Skills_Skill = new Control("Skills_Skill", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLSKILLS_DETL_']/ancestor::form[1]/descendant::*[@id='SKILL_ID']");
			CPCommon.AssertEqual(true,HPMEDS_Skills_Skill.Exists());

											Driver.SessionLogger.WriteLine("TrainingsLink");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on TrainingsLink...", Logger.MessageType.INF);
			Control HPMEDS_TrainingsLink = new Control("TrainingsLink", "ID", "lnk_1005961_HPMEDS_HEDSKILLTRAIN_HDR");
			CPCommon.AssertEqual(true,HPMEDS_TrainingsLink.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_TrainingsLink);
HPMEDS_TrainingsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on TrainingsLinkForm...", Logger.MessageType.INF);
			Control HPMEDS_TrainingsLinkForm = new Control("TrainingsLinkForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLTRAIN_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_TrainingsLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on TrainingsLinkFormTable...", Logger.MessageType.INF);
			Control HPMEDS_TrainingsLinkFormTable = new Control("TrainingsLinkFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLTRAIN_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_TrainingsLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_TrainingsLinkForm);
formBttn = HPMEDS_TrainingsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEDS_TrainingsLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEDS_TrainingsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on Trainings_Training...", Logger.MessageType.INF);
			Control HPMEDS_Trainings_Training = new Control("Trainings_Training", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLTRAIN_DETL_']/ancestor::form[1]/descendant::*[@id='TRAIN_ID']");
			CPCommon.AssertEqual(true,HPMEDS_Trainings_Training.Exists());

											Driver.SessionLogger.WriteLine("ProfessionalOrganizationsLink");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on ProfessionalOrganizationsLink...", Logger.MessageType.INF);
			Control HPMEDS_ProfessionalOrganizationsLink = new Control("ProfessionalOrganizationsLink", "ID", "lnk_1005962_HPMEDS_HEDSKILLTRAIN_HDR");
			CPCommon.AssertEqual(true,HPMEDS_ProfessionalOrganizationsLink.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_ProfessionalOrganizationsLink);
HPMEDS_ProfessionalOrganizationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on ProfessionalOrganizationsLinkForm...", Logger.MessageType.INF);
			Control HPMEDS_ProfessionalOrganizationsLinkForm = new Control("ProfessionalOrganizationsLinkForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLPROFORG_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_ProfessionalOrganizationsLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on ProfessionalOrganizationsLinkFormTable...", Logger.MessageType.INF);
			Control HPMEDS_ProfessionalOrganizationsLinkFormTable = new Control("ProfessionalOrganizationsLinkFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLPROFORG_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_ProfessionalOrganizationsLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_ProfessionalOrganizationsLinkForm);
formBttn = HPMEDS_ProfessionalOrganizationsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEDS_ProfessionalOrganizationsLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEDS_ProfessionalOrganizationsLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on ProfessionalOrganizations_ProfessionalOrganization...", Logger.MessageType.INF);
			Control HPMEDS_ProfessionalOrganizations_ProfessionalOrganization = new Control("ProfessionalOrganizations_ProfessionalOrganization", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLPROFORG_DETL_']/ancestor::form[1]/descendant::*[@id='PROF_ORG_ID']");
			CPCommon.AssertEqual(true,HPMEDS_ProfessionalOrganizations_ProfessionalOrganization.Exists());

											Driver.SessionLogger.WriteLine("CareerPlansLink");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on CareerPlansLink...", Logger.MessageType.INF);
			Control HPMEDS_CareerPlansLink = new Control("CareerPlansLink", "ID", "lnk_1005963_HPMEDS_HEDSKILLTRAIN_HDR");
			CPCommon.AssertEqual(true,HPMEDS_CareerPlansLink.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_CareerPlansLink);
HPMEDS_CareerPlansLink.Click(1.5);


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on CareerPlansLinkForm...", Logger.MessageType.INF);
			Control HPMEDS_CareerPlansLinkForm = new Control("CareerPlansLinkForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLCAREERPLAN_DETL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_CareerPlansLinkForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExist on CareerPlansLinkFormTable...", Logger.MessageType.INF);
			Control HPMEDS_CareerPlansLinkFormTable = new Control("CareerPlansLinkFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLCAREERPLAN_DETL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,HPMEDS_CareerPlansLinkFormTable.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_CareerPlansLinkForm);
formBttn = HPMEDS_CareerPlansLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? HPMEDS_CareerPlansLinkForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
HPMEDS_CareerPlansLinkForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on CareerPlans_DetailJobTitlePreferred...", Logger.MessageType.INF);
			Control HPMEDS_CareerPlans_DetailJobTitlePreferred = new Control("CareerPlans_DetailJobTitlePreferred", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEMPLCAREERPLAN_DETL_']/ancestor::form[1]/descendant::*[@id='DETL_JOB_CD']");
			CPCommon.AssertEqual(true,HPMEDS_CareerPlans_DetailJobTitlePreferred.Exists());

											Driver.SessionLogger.WriteLine("FutureGoalsForm");


												
				CPCommon.CurrentComponent = "HPMEDS";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[HPMEDS] Perfoming VerifyExists on FutureGoalsForm...", Logger.MessageType.INF);
			Control HPMEDS_FutureGoalsForm = new Control("FutureGoalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__HPMEDS_HEDSKILLTRAIN_FUTURGOAL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,HPMEDS_FutureGoalsForm.Exists());

												
				CPCommon.CurrentComponent = "HPMEDS";
							CPCommon.WaitControlDisplayed(HPMEDS_MainForm);
formBttn = HPMEDS_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

