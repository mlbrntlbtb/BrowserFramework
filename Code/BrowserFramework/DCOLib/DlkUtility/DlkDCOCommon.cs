using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium;

namespace DCOLib.DlkUtility
{
    public static class DlkDCOCommon
    {
        public static bool IsSystemError()
        {
            // system error check logic
            bool bSystemError = false;
            try
            {
                if (!DlkAlert.DoesAlertExist(1))
                {
                    // probably possible that there are other modal headers that use this class,
                    var modals = DlkEnvironment.AutoDriver.FindElements(By.Id("msgPopDivTBar"));
                    // so check each of them here.
                    foreach (var modal in modals)
                    {
                        bSystemError = modal.Text.Contains("System Error");
                        if (bSystemError) break;
                    }
                }
            }
            catch
            {
                bSystemError = false; // always ignore errors encountered here
            }
            return bSystemError;
        }
    }
}
