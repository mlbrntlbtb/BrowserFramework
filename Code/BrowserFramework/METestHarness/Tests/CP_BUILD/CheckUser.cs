using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class CheckUser : TestScript
    {
        public override bool TestExecute(out string ErrorMessage, string TestEnvironment)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {
                /* Log-in */
                CPCommon.Login(TestEnvironment, out ErrorMessage);

                /* 1 - Navigate to Manage Users */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    CPCommon.WaitLoadingFinished();
                    if (!Driver.Instance.FindElement(By.CssSelector("div[class='navCont']")).Displayed)
                        new Control("Browse", "css", "span[id = 'goToLbl']").Click();
                    new Control("Admin", "xpath", "//div[@class='busItem'][.='Admin']").Click();
                    new Control("Security", "xpath", "//div[@class='deptItem'][.='Security']").Click();
                    new Control("System Security", "xpath", "//div[@class='navItem'][.='System Security']").Click();
                    new Control("Manage Users", "xpath", "//div[@class='navItem'][.='Manage Users']").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error navigating to Manage Users app ", ex.Message));
                }

                /* 2 - Click Query button */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Click on Query...", Logger.MessageType.INF);
                    Control SYMUSR_MainForm = new Control("Query", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(SYMUSR_MainForm);
                    IWebElement formBttn = SYMUSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Query']")).Where(x => x.Displayed).FirstOrDefault();
                    if (formBttn != null)
                        formBttn.Click();
                    else
                        throw new Exception("Query Button not found ");
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking SYMUSR Query button [ln 55]. ", ex.Message));
                }

                /* 3 - Set Criteria Value */
                try
                {
                    CPCommon.CurrentComponent = "Query";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[Query] Perfoming Set on Criteria Value...", Logger.MessageType.INF);
                    Control QUERY_Find_CriteriaValue1 = new Control("Find_CriteriaValue1", "ID", "basicField0");
                    QUERY_Find_CriteriaValue1.Click();
                    QUERY_Find_CriteriaValue1.SendKeys("CPBUILDACCEPTANCE", true);
                    CPCommon.WaitLoadingFinished();
                    QUERY_Find_CriteriaValue1.SendKeys(OpenQA.Selenium.Keys.Shift + OpenQA.Selenium.Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Criteria Value in SYMUSR Query ", ex.Message));
                }

                /* 4 - Click Find Count */
                try
                {
                    CPCommon.CurrentComponent = "Query";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[Query] Perfoming click on Find Count...", Logger.MessageType.INF);
                    new Control("Find_Count", "ID", "basicCountBttn").Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Find Count in SYMUSR Query ", ex.Message));
                }

                /* 5 - Verify Find Count Results */
                try
                {
                    CPCommon.CurrentComponent = "Query";
                    CPCommon.WaitLoadingFinished();
                    Control QUERY_Find_Count_Results = new Control("Find_Count_Results", "ID", "basicCountBox");
                    QUERY_Find_Count_Results.FindElement();
                    CPCommon.AssertEqual(CPCommon.ReplaceCarriageReturn("0 records will be returned", ""), CPCommon.ReplaceCarriageReturn(QUERY_Find_Count_Results.mElement.Text.Trim(), ""));
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error verifying text in Find Count Results in SYMUSR Query ", ex.Message));
                }

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
