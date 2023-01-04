using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace TEMobileLib.DlkFunctions
{
    [Component("BrowseFilePopup")]
    public static class DlkBrowseFilePopup
    {

        [Keyword("VerifyFilename", new String[] { "1|text|Filename|Sample.txt" })]
        public static void VerifyFilename(String FileName)
        {
            DlkBaseTextBox txtFileName = new DlkBaseTextBox("Filename", "ID", "browseEdit");
            txtFileName.Initialize();
            //To handle trimming of the path as the value of the attribute returned includes 'C:\fakepath\' when the browser is Chrome
            string actValue = txtFileName.GetAttributeValue("value");
            string actualFileName = Path.GetFileName(actValue);
            DlkAssert.AssertEqual("VerifyFilename()", FileName, actualFileName);
        }

        [Keyword("ClickBrowse")]
        public static void ClickBrowse()
        {
            try
            {
                DlkBaseButton btnBrowseFile = new DlkBaseButton("BrowseFile", "ID", "browseEdit");
                btnBrowseFile.Initialize();
                DlkBaseControl frmBrowseFilePopupForm = new DlkBaseControl("BrowseFilePopupForm", "xpath", "//input[@id='browseEdit']/ancestor::form[1]");
                frmBrowseFilePopupForm.FindElement();
                DlkEnvironment.LastKnownBrowserTitle = DlkEnvironment.AutoDriver.Title;
                btnBrowseFile.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickBrowse() failed : " + e.Message, e);
            }
        }
    }
}
