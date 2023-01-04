 
using System;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using System.Threading;
using System.Linq;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class PDPCONV_SMOKE : TestScript
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
new Control("Provisional Parts", "xpath","//div[@class='navItem'][.='Provisional Parts']").Click();
new Control("Convert Provisional Parts to Standard Parts", "xpath","//div[@class='navItem'][.='Convert Provisional Parts to Standard Parts']").Click();


											Driver.SessionLogger.WriteLine("MAINFORM");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on MainForm...", Logger.MessageType.INF);
			Control PDPCONV_MainForm = new Control("MainForm", "xpath", "//div[@id='0']/form[1]");
			CPCommon.AssertEqual(true,PDPCONV_MainForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on ParameterID...", Logger.MessageType.INF);
			Control PDPCONV_ParameterID = new Control("ParameterID", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='PARM_ID']");
			CPCommon.AssertEqual(true,PDPCONV_ParameterID.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_MainForm);
IWebElement formBttn = PDPCONV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).Count <= 0 ? PDPCONV_MainForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Table')]")).FirstOrDefault() :
PDPCONV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Table']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Table not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on MainFormTable...", Logger.MessageType.INF);
			Control PDPCONV_MainFormTable = new Control("MainFormTable", "xpath", "//div[@id='0']/form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_MainFormTable.Exists());

											Driver.SessionLogger.WriteLine("ChildForm");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on ChildForm...", Logger.MessageType.INF);
			Control PDPCONV_ChildForm = new Control("ChildForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROVPART_HDR_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDPCONV_ChildForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on ChildFormTable...", Logger.MessageType.INF);
			Control PDPCONV_ChildFormTable = new Control("ChildFormTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROVPART_HDR_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_ChildFormTable.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming Click on ChildForm_AutoloadTable...", Logger.MessageType.INF);
			Control PDPCONV_ChildForm_AutoloadTable = new Control("ChildForm_AutoloadTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROVPART_HDR_']/ancestor::form[1]/descendant::*[@id='PB1___T']");
			CPCommon.WaitControlDisplayed(PDPCONV_ChildForm_AutoloadTable);
if (PDPCONV_ChildForm_AutoloadTable.mElement.GetAttribute("class") == "popupBtn" && Driver.BrowserType == "ie")
PDPCONV_ChildForm_AutoloadTable.Click(5,5);
else PDPCONV_ChildForm_AutoloadTable.Click(4.5);


												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_ChildForm);
formBttn = PDPCONV_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDPCONV_ChildForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDPCONV_ChildForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on ChildForm_ProvisionalPartDetails_Part...", Logger.MessageType.INF);
			Control PDPCONV_ChildForm_ProvisionalPartDetails_Part = new Control("ChildForm_ProvisionalPartDetails_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROVPART_HDR_']/ancestor::form[1]/descendant::*[@id='PROV_PART_ID']");
			CPCommon.AssertEqual(true,PDPCONV_ChildForm_ProvisionalPartDetails_Part.Exists());

											Driver.SessionLogger.WriteLine("Quotes");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming Click on QuotesLink...", Logger.MessageType.INF);
			Control PDPCONV_QuotesLink = new Control("QuotesLink", "ID", "lnk_1004523_PDPCONV_PROVPART_HDR");
			CPCommon.WaitControlDisplayed(PDPCONV_QuotesLink);
PDPCONV_QuotesLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on QuotesForm...", Logger.MessageType.INF);
			Control PDPCONV_QuotesForm = new Control("QuotesForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_QTHDR_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDPCONV_QuotesForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on QuotesTable...", Logger.MessageType.INF);
			Control PDPCONV_QuotesTable = new Control("QuotesTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_QTHDR_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_QuotesTable.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_QuotesForm);
formBttn = PDPCONV_QuotesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDPCONV_QuotesForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDPCONV_QuotesForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on Quotes_Quote...", Logger.MessageType.INF);
			Control PDPCONV_Quotes_Quote = new Control("Quotes_Quote", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_QTHDR_DTL_']/ancestor::form[1]/descendant::*[@id='QUOTE_ID']");
			CPCommon.AssertEqual(true,PDPCONV_Quotes_Quote.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_QuotesForm);
formBttn = PDPCONV_QuotesForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Ebom Components");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming Click on EBOMComponentsLink...", Logger.MessageType.INF);
			Control PDPCONV_EBOMComponentsLink = new Control("EBOMComponentsLink", "ID", "lnk_1004524_PDPCONV_PROVPART_HDR");
			CPCommon.WaitControlDisplayed(PDPCONV_EBOMComponentsLink);
PDPCONV_EBOMComponentsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on EBOMComponentsForm...", Logger.MessageType.INF);
			Control PDPCONV_EBOMComponentsForm = new Control("EBOMComponentsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_ENGBOM_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDPCONV_EBOMComponentsForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on EBOMComponentsTable...", Logger.MessageType.INF);
			Control PDPCONV_EBOMComponentsTable = new Control("EBOMComponentsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_EBOMComponentsTable.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_EBOMComponentsForm);
formBttn = PDPCONV_EBOMComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDPCONV_EBOMComponentsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDPCONV_EBOMComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on EBOMComponents_CompLine...", Logger.MessageType.INF);
			Control PDPCONV_EBOMComponents_CompLine = new Control("EBOMComponents_CompLine", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_ENGBOM_DTL_']/ancestor::form[1]/descendant::*[@id='COMP_LN_NO']");
			CPCommon.AssertEqual(true,PDPCONV_EBOMComponents_CompLine.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_EBOMComponentsForm);
formBttn = PDPCONV_EBOMComponentsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Ebom assemb.");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming Click on EBOMAssembliesWhereUsedLink...", Logger.MessageType.INF);
			Control PDPCONV_EBOMAssembliesWhereUsedLink = new Control("EBOMAssembliesWhereUsedLink", "ID", "lnk_1004525_PDPCONV_PROVPART_HDR");
			CPCommon.WaitControlDisplayed(PDPCONV_EBOMAssembliesWhereUsedLink);
PDPCONV_EBOMAssembliesWhereUsedLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on EBOMAssembliesWhereUsedForm...", Logger.MessageType.INF);
			Control PDPCONV_EBOMAssembliesWhereUsedForm = new Control("EBOMAssembliesWhereUsedForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_ENGBOM_DTLS_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDPCONV_EBOMAssembliesWhereUsedForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on EBOMAssembliesWhereUsedTable...", Logger.MessageType.INF);
			Control PDPCONV_EBOMAssembliesWhereUsedTable = new Control("EBOMAssembliesWhereUsedTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_ENGBOM_DTLS_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_EBOMAssembliesWhereUsedTable.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_EBOMAssembliesWhereUsedForm);
formBttn = PDPCONV_EBOMAssembliesWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDPCONV_EBOMAssembliesWhereUsedForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDPCONV_EBOMAssembliesWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on EBOMAssembliesWhereUsed_Assembly_Part...", Logger.MessageType.INF);
			Control PDPCONV_EBOMAssembliesWhereUsed_Assembly_Part = new Control("EBOMAssembliesWhereUsed_Assembly_Part", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_ENGBOM_DTLS_']/ancestor::form[1]/descendant::*[@id='ASY_PART_ID']");
			CPCommon.AssertEqual(true,PDPCONV_EBOMAssembliesWhereUsed_Assembly_Part.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_EBOMAssembliesWhereUsedForm);
formBttn = PDPCONV_EBOMAssembliesWhereUsedForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Proposals");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming Click on ProposalsLink...", Logger.MessageType.INF);
			Control PDPCONV_ProposalsLink = new Control("ProposalsLink", "ID", "lnk_1004526_PDPCONV_PROVPART_HDR");
			CPCommon.WaitControlDisplayed(PDPCONV_ProposalsLink);
PDPCONV_ProposalsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on ProposalsForm...", Logger.MessageType.INF);
			Control PDPCONV_ProposalsForm = new Control("ProposalsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROPHDR_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDPCONV_ProposalsForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on ProposalsTable...", Logger.MessageType.INF);
			Control PDPCONV_ProposalsTable = new Control("ProposalsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROPHDR_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_ProposalsTable.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_ProposalsForm);
formBttn = PDPCONV_ProposalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDPCONV_ProposalsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDPCONV_ProposalsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on Proposals_Proposal...", Logger.MessageType.INF);
			Control PDPCONV_Proposals_Proposal = new Control("Proposals_Proposal", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_PROPHDR_DTL_']/ancestor::form[1]/descendant::*[@id='PROP_ID']");
			CPCommon.AssertEqual(true,PDPCONV_Proposals_Proposal.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_ProposalsForm);
formBttn = PDPCONV_ProposalsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Proposals");


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming Click on RFQsLink...", Logger.MessageType.INF);
			Control PDPCONV_RFQsLink = new Control("RFQsLink", "ID", "lnk_1004527_PDPCONV_PROVPART_HDR");
			CPCommon.WaitControlDisplayed(PDPCONV_RFQsLink);
PDPCONV_RFQsLink.Click(1.5);


												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on RFQsForm...", Logger.MessageType.INF);
			Control PDPCONV_RFQsForm = new Control("RFQsForm", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_RFQHDR_DTL_']/ancestor::form[1]");
			CPCommon.AssertEqual(true,PDPCONV_RFQsForm.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExist on RFQsTable...", Logger.MessageType.INF);
			Control PDPCONV_RFQsTable = new Control("RFQsTable", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_RFQHDR_DTL_']/ancestor::form[1]/descendant::*[@id='dTbl']");
			CPCommon.AssertEqual(true,PDPCONV_RFQsTable.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_RFQsForm);
formBttn = PDPCONV_RFQsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).Count <= 0 ? PDPCONV_RFQsForm.mElement.FindElements(By.XPath(".//*[contains(text(),'Form')]")).FirstOrDefault() :
PDPCONV_RFQsForm.mElement.FindElements(By.CssSelector("*[title*='Form']")).FirstOrDefault();
if (formBttn!=null) { new Control("FormButton",formBttn).MouseOver(); formBttn.Click(); }
else throw new Exception(" Form not found ");


													
				CPCommon.CurrentComponent = "PDPCONV";
				
			CPCommon.WaitLoadingFinished(CPCommon.IsCurrentComponentModal(false));

			Driver.SessionLogger.WriteLine("[PDPCONV] Perfoming VerifyExists on RFQs_RFQ...", Logger.MessageType.INF);
			Control PDPCONV_RFQs_RFQ = new Control("RFQs_RFQ", "xpath", "//div[translate(@id,'0123456789','')='pr__PDPCONV_RFQHDR_DTL_']/ancestor::form[1]/descendant::*[@id='RFQ_ID']");
			CPCommon.AssertEqual(true,PDPCONV_RFQs_RFQ.Exists());

												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_RFQsForm);
formBttn = PDPCONV_RFQsForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
	if(formBttn!=null) formBttn.Click();
	else throw new Exception("Close Button not found ");


												Driver.SessionLogger.WriteLine("Close the application");


												
				CPCommon.CurrentComponent = "PDPCONV";
							CPCommon.WaitControlDisplayed(PDPCONV_MainForm);
formBttn = PDPCONV_MainForm.mElement.FindElements(By.CssSelector("*[title*='Close']")).Where(x=>x.Displayed).FirstOrDefault();
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

