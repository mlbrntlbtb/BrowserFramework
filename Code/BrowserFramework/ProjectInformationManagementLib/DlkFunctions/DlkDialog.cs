using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace ProjectInformationManagementLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {

        [Keyword("ClickOkDialogWithMessage", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickOkDialogWithMessage(String Message)
        {
            DlkAlert.ClickOkDialogWithMessage(Message);
        }

        [Keyword("ClickCancelDialogWithMessage", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickCancelDialogWithMessage(String Message)
        {
            DlkAlert.ClickCancelDialogWithMessage(Message);
        }

        [Keyword("ClickOkDialogIfExists", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickOkDialogIfExists(String Message)
        {
            DlkAlert.ClickOkDialogIfExists(Message);
        }

        [Keyword("FileDownload", new String[] {"1|text|File Name|C:\\test.txt", 
                                                "2|text|Wait Time(secs)|30"})]
        public static void FileDownload(String Filename, String WaitTime)
        {
            //DlkWinApi.FileDownload(Filename, Convert.ToInt32(WaitTime) );
            String sBrowserTitle = DlkEnvironment.AutoDriver.Title;
            DlkMSUIAutomationHelper.FileDownload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));

        }

        [Keyword("FileUpload", new String[] {"1|text|File Name|C:\\test.txt", 
                                                "2|text|Wait Time(secs)|30"})]
        public static void FileUpload(String Filename, String WaitTime)
        {
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.FileUpload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }

        [Keyword("MultipleFileUpload", new String[] {"1|text|File List|test1.txt~test2.txt~test3.ppt", 
                                                "2|text|Wait Time(secs)|30"})]
        public static void MultipleFileUpload(String Filelist, String WaitTime)
        {
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.MultipleFileUpload(sBrowserTitle, Filelist, Convert.ToInt32(WaitTime));
        }
    }
}
