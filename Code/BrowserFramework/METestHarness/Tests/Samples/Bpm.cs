using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Common;
using METestHarness.Sys;

namespace METestHarness.Tests
{
    public class Bpm : TestScript
    {
        public override bool TestExecute(out string ErrorMessage)
        {
            bool ret = true;
            ErrorMessage = string.Empty;
            /* Login */
            try
            {
                /* Login page */
                BPMCommon.Login("default");
                WaitForSpinner();

                /* Click Documents tab */
                Control documentsTab = new Control("documentsTab", "iframe_xpath", "servletBridgeIframe_//a[@title='Documents']");
                documentsTab.Click();
                WaitForSpinner();

                /* Click Folders side menu */
                Control foldersSideMenu = new Control("foldersSideMenu", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//a[@title='Expand Folders']");
                foldersSideMenu.Click();
                WaitForSpinner();

                /* Collapse Public Folders tree node */
                Control publicFolders = new Control("publicFolders", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//*[@id='accordionNavigationView_drawer1_treeView_treeNode1_name']");
                publicFolders.Click();
                WaitForSpinner();

                /* Click Business Performance Management tree node */
                ClickForwardIfNotFoundInTable("Business Performance Management");
                WaitForSpinner();

                Control bpm = new Control("bpm", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//*[@id='ListingURE_detailView_mainTableBody']/descendant::tr/td/div[text()='Business Performance Management']");
                bpm.DoubleClick();
                WaitForSpinner();

                /* Click Reporting Folder */
                ClickForwardIfNotFoundInTable("Reporting");
                Control reporting = new Control("reporting", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//*[@id='ListingURE_detailView_mainTableBody']/descendant::tr/td/div[text()='Reporting']");
                reporting.DoubleClick();
                WaitForSpinner();

                /* Click Job List */
                ClickForwardIfNotFoundInTable("Job List");
                Control jobList = new Control("jobList", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//*[@id='ListingURE_detailView_mainTableBody']/descendant::tr/td/div[text()='Job List']");
                jobList.DoubleClick();
                WaitForSpinner();

                /* Verify default fields existence */
                // How to legitly verify??

                /* Select Job No: prompt */

                /* Click refresh Values */
                //servletBridgeIframe
                //iframe4583 -82970-1518091222945
                //webiViewFrame
                //lovWidgetpromptLovZone_refresh
                Control refreshButton = new Control("refreshButton", "iframe_nested_xpath", "servletBridgeIframe~6~webiViewFrame_//*[@id='lovWidgetpromptLovZone_refresh']/descendant::tr");
                refreshButton.Click();
                WaitForSpinner();

                /* Double click target job no */
                Control targetJob = new Control("targetJob", "iframe_nested_xpath", "servletBridgeIframe~6~webiViewFrame_//*[@id='mlst_bodylovWidgetpromptLovZone_lov']/descendant::span[text()='250008']");
                targetJob.Click();
                WaitForSpinner();

                Control btnOk = new Control("btnOk", "iframe_nested_xpath", "servletBridgeIframe~6~webiViewFrame_//*[@id='OK_BTN_promptsDlg']/descendant::tr");
                btnOk.Click();
                WaitForSpinner();
                /* Verify displayed */
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                ret = false;
            }

            return ret;
        }

        private void ClickForwardIfNotFoundInTable(string target)
        {
            while (true)
            {
                Driver.Instance.SwitchTo().DefaultContent();
                Control lastRowTitle = new Control("lastRowTitle", "iframe_nested_xpath", "servletBridgeIframe~iframe4577-12_//*[@id='ListingURE_detailView_mainTableBody']/descendant::tr[last()]/td[2]");
                lastRowTitle.FindElement();
                if (string.Compare(target, lastRowTitle.mElement.Text) > 0)
                //if (string.Compare(target, lastRowTitle.GetAttributeValue("text")) > 0)
                {
                    // check current page against total page
                    Control currentPageInfo = new Control("currentPageInfo", "id", "ListingURE_pageNumberInput");
                    string[] pages = currentPageInfo.GetAttributeValue("title").Replace(" of ", "/").Split('/');
                    if (pages.First() == pages.Last())
                    {
                        break;
                    }
                    else
                    {
                        Control forward = new Control("forward", "xpath", "//*[@id='IconImg_ListingURE_goForwardButton']/..");
                        System.Threading.Thread.Sleep(1000);
                        forward.Click();
                        continue;
                    }
                }
                break;
            }
            Driver.Instance.SwitchTo().DefaultContent();
        }

        private void WaitForSpinner()
        {
            Driver.Instance.SwitchTo().DefaultContent();
            Control spinner = new Control("spinner", "iframe_nested_xpath", "servletBridgeIframe_//*[@class='spinner']");
            if (spinner.Exists(1))
            {
                while(spinner.mElement.Displayed)
                {
                    Driver.SessionLogger.WriteLine("Application loading...");
                    System.Threading.Thread.Sleep(1000);
                }
            }
            Driver.Instance.SwitchTo().DefaultContent();
        }
    }
}
