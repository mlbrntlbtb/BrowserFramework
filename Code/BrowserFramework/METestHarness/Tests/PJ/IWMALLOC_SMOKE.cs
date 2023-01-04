 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class IWMALLOC_SMOKE : TestScript
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
new Control("Inter-Company Work Orders", "xpath","//div[@class='deptItem'][.='Inter-Company Work Orders']").Click();
new Control("Inter-Company Work Orders Processing", "xpath","//div[@class='navItem'][.='Inter-Company Work Orders Processing']").Click();
new Control("Manage IWO Allocations", "xpath","//div[@class='navItem'][.='Manage IWO Allocations']").Click();


											CPCommon.Wait(10);


												
				CPCommon.CurrentComponent = "Query";
								CPCommon.WaitControlDisplayed(new Control("QueryTitle", "ID", "qryHeaderLabel"));
CPCommon.AssertEqual("Manage IWO Allocations", new Control("QueryTitle", "ID", "qryHeaderLabel").GetValue().Trim());


												
				CPCommon.CurrentComponent = "Query";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
			Control Query_Find = new Control("Find", "ID", "submitQ");
			CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


												
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming ClickButtonIfExists on MainForm...", Logger.MessageType.INF);
			Control IWMALLOC_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(IWMALLOC_MainForm);
IWebElement formBttn = IWMALLOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWMALLOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWMALLOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control IWMALLOC_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMALLOC_MainFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_MainForm);
formBttn = IWMALLOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWMALLOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWMALLOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_MainForm);
formBttn = IWMALLOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Next']")).Count <= 0 ? IWMALLOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Next')]")).FirstOrDefault() :
IWMALLOC_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Next')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Next] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.AssertEqual(true,IWMALLOC_MainForm.Exists());

													
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExists on IWONumber...", Logger.MessageType.INF);
			Control IWMALLOC_IWONumber = new Control("IWONumber", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='IWO_NO']");
			CPCommon.AssertEqual(true,IWMALLOC_IWONumber.Exists());

											Driver.SessionLogger.WriteLine("ALLOCATION DETAIL");


												
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExists on AllocationDetailLink...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetailLink = new Control("AllocationDetailLink", "ID", "lnk_1002109_IWMALLOC_IWOALLOCHDR_HDR");
			CPCommon.AssertEqual(true,IWMALLOC_AllocationDetailLink.Exists());

												
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming ClickButtonIfExists on AllocationDetailForm...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetailForm = new Control("AllocationDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMALLOC_IWOALLOCTRN_CHLD_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetailForm);
formBttn = IWMALLOC_AllocationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWMALLOC_AllocationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWMALLOC_AllocationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExist on AllocationDetailFormTable...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetailFormTable = new Control("AllocationDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMALLOC_IWOALLOCTRN_CHLD_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMALLOC_AllocationDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetailForm);
formBttn = IWMALLOC_AllocationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWMALLOC_AllocationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWMALLOC_AllocationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.AssertEqual(true,IWMALLOC_AllocationDetailForm.Exists());

													
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExists on AllocationDetail_Line...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetail_Line = new Control("AllocationDetail_Line", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMALLOC_IWOALLOCTRN_CHLD_']/ancestor::form[1]/descendant::*[@id='IWO_LN_NO']");
			CPCommon.AssertEqual(true,IWMALLOC_AllocationDetail_Line.Exists());

											Driver.SessionLogger.WriteLine("LABOR DETAIL");


												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetailForm);
formBttn = IWMALLOC_AllocationDetailForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Count <= 0 ? IWMALLOC_AllocationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault() :
IWMALLOC_AllocationDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Query')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Query] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "Query";
							CPCommon.WaitControlDisplayed(Query_Find);
if (Query_Find.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
Query_Find.Click(5,5);
else Query_Find.Click(4.5);


													
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExists on AllocationDetail_LaborDetailLink...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetail_LaborDetailLink = new Control("AllocationDetail_LaborDetailLink", "ID", "lnk_1002110_IWMALLOC_IWOALLOCTRN_CHLD");
			CPCommon.AssertEqual(true,IWMALLOC_AllocationDetail_LaborDetailLink.Exists());

												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetail_LaborDetailLink);
IWMALLOC_AllocationDetail_LaborDetailLink.Click(1.5);


													
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming ClickButtonIfExists on AllocationDetail_LaborDetailForm...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetail_LaborDetailForm = new Control("AllocationDetail_LaborDetailForm", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMALLOC_IWOALLOCLAB_TBL_']/ancestor::form[1]");
			CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetail_LaborDetailForm);
formBttn = IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExist on AllocationDetail_LaborDetailFormTable...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetail_LaborDetailFormTable = new Control("AllocationDetail_LaborDetailFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMALLOC_IWOALLOCLAB_TBL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,IWMALLOC_AllocationDetail_LaborDetailFormTable.Exists());

												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetail_LaborDetailForm);
formBttn = IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.AssertEqual(true,IWMALLOC_AllocationDetail_LaborDetailForm.Exists());

													
				CPCommon.CurrentComponent = "IWMALLOC";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[IWMALLOC] Perfoming VerifyExists on AllocationDetail_LaborDetail_PLC...", Logger.MessageType.INF);
			Control IWMALLOC_AllocationDetail_LaborDetail_PLC = new Control("AllocationDetail_LaborDetail_PLC", "xpath", "//div[translate(@id,'0123456789','')='pr__IWMALLOC_IWOALLOCLAB_TBL_']/ancestor::form[1]/descendant::*[@id='BILL_LAB_CAT_CD']");
			CPCommon.AssertEqual(true,IWMALLOC_AllocationDetail_LaborDetail_PLC.Exists());

												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_AllocationDetail_LaborDetailForm);
formBttn = IWMALLOC_AllocationDetail_LaborDetailForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("CLOSE APP");


												
				CPCommon.CurrentComponent = "IWMALLOC";
							CPCommon.WaitControlDisplayed(IWMALLOC_MainForm);
formBttn = IWMALLOC_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

