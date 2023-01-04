using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;

namespace WorkBookLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
        [Keyword("FileUpload", new String[] {"1|text|File Name|C:\\test.txt",
                                                "2|text|Wait Time(secs)|30"})]
        public static void FileUpload(String Filename, String WaitTime)
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.FileUpload(sBrowserTitle, Filename, Convert.ToInt32(WaitTime));
        }

        [Keyword("MultipleFileUpload", new String[] {"1|text|File List|test1.txt~test2.txt~test3.ppt",
                                                "2|text|Wait Time(secs)|30"})]
        public static void MultipleFileUpload(String Filelist, String WaitTime)
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            String sBrowserTitle = DlkEnvironment.mPreviousTitle;
            DlkMSUIAutomationHelper.MultipleFileUpload(sBrowserTitle, Filelist, Convert.ToInt32(WaitTime));
        }
    }
}
