 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class POMSCST_SMOKE : TestScript
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
new Control("Purchasing", "xpath","//div[@class='deptItem'][.='Purchasing']").Click();
new Control("Purchase Orders", "xpath","//div[@class='navItem'][.='Purchase Orders']").Click();
new Control("Update Subcontract Retainage PO Status", "xpath","//div[@class='navItem'][.='Update Subcontract Retainage PO Status']").Click();


											Driver.SessionLogger.WriteLine("Main Form");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming ClickButtonIfExists on MainForm...", Logger.MessageType.INF);
			Control POMSCST_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.WaitControlDisplayed(POMSCST_MainForm);
IWebElement formBttn = POMSCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSCST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSCST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on StatusUpdateID...", Logger.MessageType.INF);
			Control POMSCST_StatusUpdateID = new Control("StatusUpdateID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='RECPT_ID']");
			CPCommon.AssertEqual(true,POMSCST_StatusUpdateID.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_MainForm);
formBttn = POMSCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? POMSCST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
POMSCST_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Table] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control POMSCST_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSCST_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("Order Totals");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on OrderTotalsLink...", Logger.MessageType.INF);
			Control POMSCST_OrderTotalsLink = new Control("OrderTotalsLink", "ID", "lnk_1004058_POMSCST_RECPTHDR_HDR");
			CPCommon.AssertEqual(true,POMSCST_OrderTotalsLink.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_OrderTotalsLink);
POMSCST_OrderTotalsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on OrderTotalsForm...", Logger.MessageType.INF);
			Control POMSCST_OrderTotalsForm = new Control("OrderTotalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_ORDERTOTALS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSCST_OrderTotalsForm.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_OrderTotalsForm);
formBttn = POMSCST_OrderTotalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Hdr Std Txt");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on HdrStdTxtLink...", Logger.MessageType.INF);
			Control POMSCST_HdrStdTxtLink = new Control("HdrStdTxtLink", "ID", "lnk_1004480_POMSCST_RECPTHDR_HDR");
			CPCommon.AssertEqual(true,POMSCST_HdrStdTxtLink.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_HdrStdTxtLink);
POMSCST_HdrStdTxtLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on HdrStdTxtForm...", Logger.MessageType.INF);
			Control POMSCST_HdrStdTxtForm = new Control("HdrStdTxtForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POTEXT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSCST_HdrStdTxtForm.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExist on HdrStdTxtTable...", Logger.MessageType.INF);
			Control POMSCST_HdrStdTxtTable = new Control("HdrStdTxtTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POTEXT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSCST_HdrStdTxtTable.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_HdrStdTxtForm);
formBttn = POMSCST_HdrStdTxtForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Header Documents");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on HeaderDocumentsLink...", Logger.MessageType.INF);
			Control POMSCST_HeaderDocumentsLink = new Control("HeaderDocumentsLink", "ID", "lnk_1007722_POMSCST_RECPTHDR_HDR");
			CPCommon.AssertEqual(true,POMSCST_HeaderDocumentsLink.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_HeaderDocumentsLink);
POMSCST_HeaderDocumentsLink.Click(1.5);


													
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on HeaderDocumentsForm...", Logger.MessageType.INF);
			Control POMSCST_HeaderDocumentsForm = new Control("HeaderDocumentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSCST_HeaderDocumentsForm.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExist on HeaderDocumentsTable...", Logger.MessageType.INF);
			Control POMSCST_HeaderDocumentsTable = new Control("HeaderDocumentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSCST_HeaderDocumentsTable.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_HeaderDocumentsForm);
formBttn = POMSCST_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSCST_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSCST_HeaderDocumentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault();
if (formBttn!=null && formBttn.Displayed)
{ new Control("FormButton",formBttn).MouseOver(); formBttn.Click();
Driver.SessionLogger.WriteLine("Button [Form] found and clicked.", Logger.MessageType.INF);
}


													
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on HeaderDocuments_Document...", Logger.MessageType.INF);
			Control POMSCST_HeaderDocuments_Document = new Control("HeaderDocuments_Document", "xpath", "//div[translate(@id,'0123456789','')='pr__POM_PODOCUMENT_']/ancestor::form[1]/descendant::*[@id='DOCUMENT_ID']");
			CPCommon.AssertEqual(true,POMSCST_HeaderDocuments_Document.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_HeaderDocumentsForm);
formBttn = POMSCST_HeaderDocumentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Sub - Contract");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on SubContractForm...", Logger.MessageType.INF);
			Control POMSCST_SubContractForm = new Control("SubContractForm", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POLN_CTW_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,POMSCST_SubContractForm.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExist on SubContractTable...", Logger.MessageType.INF);
			Control POMSCST_SubContractTable = new Control("SubContractTable", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,POMSCST_SubContractTable.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_SubContractForm);
formBttn = POMSCST_SubContractForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? POMSCST_SubContractForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
POMSCST_SubContractForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on SubcontractLine...", Logger.MessageType.INF);
			Control POMSCST_SubcontractLine = new Control("SubcontractLine", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='PO_LN_NO']");
			CPCommon.AssertEqual(true,POMSCST_SubcontractLine.Exists());

												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming Select on Tab...", Logger.MessageType.INF);
			Control POMSCST_Tab = new Control("Tab", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='tbTbl']");
			CPCommon.WaitControlDisplayed(POMSCST_Tab);
IWebElement mTab = POMSCST_Tab.mElement.FindElements(OpenQA.Selenium.By.XPath(".//span[contains(@class, 'TabLbl')]")).Where(x => new Control("Tab", x).GetValue() == "Work Details").FirstOrDefault();
if (Driver.BrowserType.ToLower() != "ie") new Control("Tab", mTab).ScrollIntoViewUsingJavaScript();
else new Control("Tab", mTab).ScrollTab(mTab);
mTab.Click();


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on WorkDetails_RequestedWorkAmt...", Logger.MessageType.INF);
			Control POMSCST_WorkDetails_RequestedWorkAmt = new Control("WorkDetails_RequestedWorkAmt", "xpath", "//div[translate(@id,'0123456789','')='pr__POMSCST_POLN_CTW_']/ancestor::form[1]/descendant::*[@id='TRN_RQSTD_AMT']");
			CPCommon.AssertEqual(true,POMSCST_WorkDetails_RequestedWorkAmt.Exists());

											Driver.SessionLogger.WriteLine("Ln Std Txt");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on LnStdTxtLink...", Logger.MessageType.INF);
			Control POMSCST_LnStdTxtLink = new Control("LnStdTxtLink", "ID", "lnk_1003973_POMSCST_POLN_CTW");
			CPCommon.AssertEqual(true,POMSCST_LnStdTxtLink.Exists());

											Driver.SessionLogger.WriteLine("Line Documents");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on LineDocumentsLink...", Logger.MessageType.INF);
			Control POMSCST_LineDocumentsLink = new Control("LineDocumentsLink", "ID", "lnk_1007723_POMSCST_POLN_CTW");
			CPCommon.AssertEqual(true,POMSCST_LineDocumentsLink.Exists());

											Driver.SessionLogger.WriteLine("PO Line Accounts");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on POLineAccountsXLink...", Logger.MessageType.INF);
			Control POMSCST_POLineAccountsXLink = new Control("POLineAccountsXLink", "ID", "lnk_1003975_POMSCST_POLN_CTW");
			CPCommon.AssertEqual(true,POMSCST_POLineAccountsXLink.Exists());

											Driver.SessionLogger.WriteLine("Currency Line");


												
				CPCommon.CurrentComponent = "POMSCST";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[POMSCST] Perfoming VerifyExists on CurrencyLineLink...", Logger.MessageType.INF);
			Control POMSCST_CurrencyLineLink = new Control("CurrencyLineLink", "ID", "lnk_1004057_POMSCST_POLN_CTW");
			CPCommon.AssertEqual(true,POMSCST_CurrencyLineLink.Exists());

											Driver.SessionLogger.WriteLine("Closing Main Form");


												
				CPCommon.CurrentComponent = "POMSCST";
							CPCommon.WaitControlDisplayed(POMSCST_MainForm);
formBttn = POMSCST_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

