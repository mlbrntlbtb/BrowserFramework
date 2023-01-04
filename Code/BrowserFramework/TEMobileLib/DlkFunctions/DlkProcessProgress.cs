using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using TEMobileLib.System;
using TEMobileLib.DlkControls;

namespace TEMobileLib.DlkFunctions
{
    [Component("ProcessProgress")]
    public static class DlkProcessProgress
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        [Keyword("WaitForProcessFinished", new String[] { "1|text|Timeout(sec)|600" })]
        public static void WaitForProcessFinished(String sTimeout)
        {
            try
            {
                DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord("ProcessProgress", "OK");
                DlkButton btnOk = new DlkButton(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);

                for (int i = 0; i < Convert.ToInt32(sTimeout); i++)
                {
                    Thread.Sleep(1000);
                    if (btnOk.GetValue().ToLower() != "ok")
                    {
                        DlkLogger.LogInfo("OK button not yet visible. Waiting...");
                        continue;
                    }
                    DlkLogger.LogInfo("Successfully executed WaitForProcessFinished().");
                    return;
                }
                DlkLogger.LogWarning("WaitForProcessFinished() has timed out.");
            }
            catch (Exception e)
            {
                throw new Exception("WaitForProcessFinished() failed : " + e.Message, e);
            }
        }

        [Keyword("WaitForProcessDialogClosed", new String[] { "1|text|Timeout(sec)|600" })]
        public static void WaitForProcessDialogClosed(String sTimeout)
        {
            DlkObjectStoreFileControlRecord mControl = DlkDynamicObjectStoreHandler.GetControlRecord("ProcessProgress", "ProgressBar");
            DlkImage imgProgressBar = new DlkImage(mControl.mKey, mControl.mSearchMethod, mControl.mSearchParameters);
            
            int i;
            for (i = 0; i < Convert.ToInt32(sTimeout); i++)
            {
                if (imgProgressBar.Exists(1))
                {
                    DlkLogger.LogInfo("Process dialog still visible...");
                    Thread.Sleep(1000);
                    continue;
                }
                else
                {
                    break;
                }
            }
            if (i >= Convert.ToInt32(sTimeout))
            {
                DlkLogger.LogWarning("WaitForProcessDialogClosed() has timed out.");
            }
        }
    }
}
