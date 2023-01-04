using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using CostpointLib.DlkControls;

namespace CostpointLib.DlkFunctions
{
    [Component("FileUploadManager")]
    public static class DlkFileUploadManager
    {
        [Keyword("ClickBrowse")]
        public static void ClickBrowse()
        {
            try
            {
                DlkButton btnBrowseFile = new DlkButton("BrowseFile", "ID", "fileUpldFile");
                btnBrowseFile.Initialize();
                DlkForm frmFileUploadManagerForm = new DlkForm("FileUploadForm", "xpath", "//input[@id='fileUpldFile']/ancestor::form[1]");
                frmFileUploadManagerForm.Initialize();
                DlkEnvironment.LastKnownBrowserTitle = DlkEnvironment.AutoDriver.Title;
                frmFileUploadManagerForm.mElement.SendKeys(Keys.Space);
                btnBrowseFile.Click();
            }
            catch
            {

            }
        }
    }
}
