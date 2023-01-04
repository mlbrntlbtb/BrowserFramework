using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace BPMLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
     
        [Keyword("ClickAcceptDialogWithMessage", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickAcceptDialogWithMessage(String Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                DlkAlert.ClickOkDialogWithMessage(Message);
            }
            else
            {
                DlkAlert.Accept();
            }
        }

        [Keyword("ClickRejectDialogWithMessage", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickRejectDialogWithMessage(String Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                DlkAlert.ClickCancelDialogWithMessage(Message);
            }
            else
            {
                DlkAlert.Dismiss();
            }
        }

        [Keyword("ClickAcceptDialogIfExists", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickAcceptDialogIfExists(String Message)
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
            String sBrowserTitle = DlkEnvironment.AutoDriver.Title;
            DlkMSUIAutomationHelper.FileUpload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }
    }
}
