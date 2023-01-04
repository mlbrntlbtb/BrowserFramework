 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class INMRQST_SMOKE : TestScript
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
new Control("Inventory", "xpath","//div[@class='deptItem'][.='Inventory']").Click();
new Control("Reservations", "xpath","//div[@class='navItem'][.='Reservations']").Click();
new Control("Manage Inventory Requests", "xpath","//div[@class='navItem'][.='Manage Inventory Requests']").Click();


											Driver.SessionLogger.WriteLine("Query");


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming ClickButton on MainForm...", Logger.MessageType.INF);
			Control INMRQST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(INMRQST_MainForm);
IWebElement formBttn = INMRQST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? INMRQST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
INMRQST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).FirstOrDefault();
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


											Driver.SessionLogger.WriteLine("MainForm");


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control INMRQST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMRQST_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.AssertEqual(true,INMRQST_MainForm.Exists());

													
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_MainForm);
formBttn = INMRQST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMRQST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMRQST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on Identification_Warehouse...", Logger.MessageType.INF);
			Control INMRQST_Identification_Warehouse = new Control("Identification_Warehouse", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='WHSE_ID_FR']");
			CPCommon.AssertEqual(true,INMRQST_Identification_Warehouse.Exists());

											Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control INMRQST_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_RESLN_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMRQST_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control INMRQST_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_RESLN_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMRQST_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_ChildForm);
formBttn = INMRQST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMRQST_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMRQST_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on ChildForm_Line...", Logger.MessageType.INF);
			Control INMRQST_ChildForm_Line = new Control("ChildForm_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_RESLN_DTL_']/ancestor::form[1]/descendant::*[@id='RES_LN_NO']");
			CPCommon.AssertEqual(true,INMRQST_ChildForm_Line.Exists());

											Driver.SessionLogger.WriteLine("ChildFormTab");


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming Select on ChildForm_Tab...", Logger.MessageType.INF);
			Control INMRQST_ChildForm_Tab = new Control("ChildForm_Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_RESLN_DTL_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(INMRQST_ChildForm_Tab);
IWebElement mTab = INMRQST_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Line Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on ChildForm_LineDetails_Reservation_FromInvAbbrev...", Logger.MessageType.INF);
			Control INMRQST_ChildForm_LineDetails_Reservation_FromInvAbbrev = new Control("ChildForm_LineDetails_Reservation_FromInvAbbrev", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_RESLN_DTL_']/ancestor::form[1]/descendant::*[@id='INVT_ABBRV_CD_FR']");
			CPCommon.AssertEqual(true,INMRQST_ChildForm_LineDetails_Reservation_FromInvAbbrev.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_ChildForm_Tab);
mTab = INMRQST_ChildForm_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Other Info").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


													
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on ChildForm_OtherInfo_OtherInfo_UnitCost...", Logger.MessageType.INF);
			Control INMRQST_ChildForm_OtherInfo_OtherInfo_UnitCost = new Control("ChildForm_OtherInfo_OtherInfo_UnitCost", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_RESLN_DTL_']/ancestor::form[1]/descendant::*[@id='UNIT_CST_AMT']");
			CPCommon.AssertEqual(true,INMRQST_ChildForm_OtherInfo_OtherInfo_UnitCost.Exists());

											Driver.SessionLogger.WriteLine("On Hand Locations");


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on ChildForm_OnHandLocationsLink...", Logger.MessageType.INF);
			Control INMRQST_ChildForm_OnHandLocationsLink = new Control("ChildForm_OnHandLocationsLink", "ID", "lnk_1007968_INM_RESLN_DTL");
			CPCommon.AssertEqual(true,INMRQST_ChildForm_OnHandLocationsLink.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_ChildForm_OnHandLocationsLink);
INMRQST_ChildForm_OnHandLocationsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExist on OnHandLocationsFormTable...", Logger.MessageType.INF);
			Control INMRQST_OnHandLocationsFormTable = new Control("OnHandLocationsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_INVTWHSELOC_ONHAND_QTY_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMRQST_OnHandLocationsFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on OnHandLocationsForm...", Logger.MessageType.INF);
			Control INMRQST_OnHandLocationsForm = new Control("OnHandLocationsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__INM_INVTWHSELOC_ONHAND_QTY_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMRQST_OnHandLocationsForm.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_OnHandLocationsForm);
formBttn = INMRQST_OnHandLocationsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Substitute Parts");


												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on ChildForm_SubstitutePartsLink...", Logger.MessageType.INF);
			Control INMRQST_ChildForm_SubstitutePartsLink = new Control("ChildForm_SubstitutePartsLink", "ID", "lnk_16581_INM_RESLN_DTL");
			CPCommon.AssertEqual(true,INMRQST_ChildForm_SubstitutePartsLink.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_ChildForm_SubstitutePartsLink);
INMRQST_ChildForm_SubstitutePartsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExist on SubstitutePartsFormTable...", Logger.MessageType.INF);
			Control INMRQST_SubstitutePartsFormTable = new Control("SubstitutePartsFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSUBSTPART_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,INMRQST_SubstitutePartsFormTable.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on SubstitutePartsForm...", Logger.MessageType.INF);
			Control INMRQST_SubstitutePartsForm = new Control("SubstitutePartsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSUBSTPART_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,INMRQST_SubstitutePartsForm.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_SubstitutePartsForm);
formBttn = INMRQST_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? INMRQST_SubstitutePartsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
INMRQST_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "INMRQST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[INMRQST] Perfoming VerifyExists on SubstituteParts_SubstitutePart...", Logger.MessageType.INF);
			Control INMRQST_SubstituteParts_SubstitutePart = new Control("SubstituteParts_SubstitutePart", "xpath", "//div[translate(@id,'0123456789','')='pr__MMMSUBSTPART_']/ancestor::form[1]/descendant::*[@id='SUBST_PART_ID']");
			CPCommon.AssertEqual(true,INMRQST_SubstituteParts_SubstitutePart.Exists());

												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_SubstitutePartsForm);
formBttn = INMRQST_SubstitutePartsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "INMRQST";
							CPCommon.WaitControlDisplayed(INMRQST_MainForm);
formBttn = INMRQST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

