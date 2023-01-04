using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using METestHarness.Common;
using METestHarness.Sys;
using OpenQA.Selenium;

namespace METestHarness.Tests
{
    public class DeleteUser : TestScript
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
                    Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Query...", Logger.MessageType.INF);
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
                    throw new Exception(TraceMessage("Error clicking SYMUSR Query button ",ex.Message));
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
                    QUERY_Find_CriteriaValue1.SendKeys(Keys.Shift + Keys.Tab);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error setting Criteria Value in SYMUSR Query ",ex.Message));
                }

                /* 4 - Click Find */
                try
                {
                    CPCommon.CurrentComponent = "Query";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[Query] Perfoming Click on Find...", Logger.MessageType.INF);
                    new Control("Find", "ID", "submitQ").Click();  
                    Thread.Sleep(800);
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Find button in SYMUSR Query ", ex.Message));
                }

                /* 5 - Click Delete button */
                try
                {
                    CPCommon.CurrentComponent = "SYMUSR";
                    CPCommon.WaitLoadingFinished();
                    Driver.SessionLogger.WriteLine("[SYMUSR] Perfoming Click on Delete button...", Logger.MessageType.INF);
                    Control SYMUSR_MainForm = new Control("Query", "xpath", "//div[@id='0']/form[1]");
                    CPCommon.WaitControlDisplayed(SYMUSR_MainForm);
                    IWebElement formBttn = SYMUSR_MainForm.mElement.FindElements(By.CssSelector("*[title*='Delete']")).Where(x => x.Displayed).FirstOrDefault();
                    if (formBttn != null)
                    {
                        formBttn.Click();
                    }
                    else
                        throw new Exception("Delete Button not found ");
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking SYMUSR Delete button ", ex.Message));
                }

                /* 6 - Click Save and continue */
                try
                {
                    CPCommon.CurrentComponent = "CP7Main";
                    Control CP7Main_MainToolBar = new Control("MainToolBar", "ID", "tlbr");
                    CPCommon.WaitControlDisplayed(CP7Main_MainToolBar);
                    IWebElement tlbrBtn = CP7Main_MainToolBar.mElement.FindElements(By.XPath(".//*[@class='tbBtnContainer']//div[contains(@title,'Save & Continue (F6)')]")).FirstOrDefault();
                    if (tlbrBtn == null)
                        throw new Exception("Unable to find button Save & Continue (F6).");
                    Thread.Sleep(800);
                    tlbrBtn.Click();
                }
                catch (Exception ex)
                {
                    throw new Exception(TraceMessage("Error clicking Save and Continue ", ex.Message));
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
