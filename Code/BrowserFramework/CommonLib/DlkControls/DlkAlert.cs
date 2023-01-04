using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;

namespace CommonLib.DlkControls
{
    public static class DlkAlert
    {
        private static int iWait = 3;
        public static Boolean DoesAlertExist(int iTimeInSecs)
        {
            Boolean bExist = false;
            IAlert mAlert;
            for (int i = 1; i <= iTimeInSecs; i++)
            {
                try
                {
                    mAlert = DlkEnvironment.AutoDriver.SwitchTo().Alert();
                    if (mAlert.Text.Length > 0)
                    {
                        bExist = true;
                        break;
                    }
                }
                catch
                {
                    // do nothing...
                }
                Thread.Sleep(1000);
            }
            return bExist;
        }

        public static Boolean DoesAlertExist()
        {
            Boolean bExist = false;
            IAlert mAlert;

            try
            {
                mAlert = DlkEnvironment.AutoDriver.SwitchTo().Alert();
                if (mAlert.Text.Length > 0)
                {
                    bExist = true;
                }
            }
            catch
            {
                // do nothing...
            }
            return bExist;
        }


        public static void VerifyAlertText(String ExpectedAlertText)
        {
            String ActAlertText = "";
            if (DoesAlertExist(iWait))
            {
                ActAlertText = System.Text.RegularExpressions.Regex.Replace(DlkEnvironment.AutoDriver.SwitchTo().Alert().Text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "), @"\s+", " ");
            }
            DlkAssert.AssertEqual("Alert text verification.", ExpectedAlertText, ActAlertText);
        }

        public static void ClickOkDialogWithMessage(String Message)
        {
            VerifyAlertText(Message);
            DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
        }

        public static void ClickCancelDialogWithMessage(String Message)
        {
            VerifyAlertText(Message);
            DlkEnvironment.AutoDriver.SwitchTo().Alert().Dismiss();
        }

        public static void ClickOkDialogWithMessagePart(String PartOfMessage)
        {
            String ActAlertText = "";
            if (DoesAlertExist(iWait))
            {
                ActAlertText = System.Text.RegularExpressions.Regex.Replace(DlkEnvironment.AutoDriver.SwitchTo().Alert().Text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " "), @"\s+", " ");

                if (ActAlertText.Contains(PartOfMessage))
                {
                    DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                    DlkLogger.LogInfo("Successfully executed Accept() on alert.");
                }
                else
                {
                    throw new Exception("Actual alert text [" + ActAlertText + "] does not contain [" + PartOfMessage + "].");
                }
            }
            else
            {
                throw new Exception("No alert exists.");
            }
        }

        public static void ClickOkDialogIfExists(String ExpectedAlertText)
        {
            if (DoesAlertExist(iWait))
            {
                VerifyAlertText(ExpectedAlertText);
                DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                DlkLogger.LogInfo("Successfully executed Accept() on alert.");
            }
            else
            {
                DlkLogger.LogInfo("No alert exists.");
            }
        }

        public static String GetText(int iTimeInSecs)
        {
            String ActAlertText = "";
            if (DoesAlertExist(iTimeInSecs))
            {
                ActAlertText = DlkEnvironment.AutoDriver.SwitchTo().Alert().Text;
            }
            return ActAlertText;
        }
        public static void Accept()
        {
            if (DoesAlertExist(iWait))
            {
                DlkEnvironment.AutoDriver.SwitchTo().Alert().Accept();
                DlkLogger.LogInfo("Successfully executed Accept() on alert.");
            }
            else
            {
                DlkLogger.LogInfo("No alert found to execute Accept() on.");
            }

        }
        public static void Dismiss()
        {
            if (DoesAlertExist(iWait))
            {
                DlkEnvironment.AutoDriver.SwitchTo().Alert().Dismiss();
                DlkLogger.LogInfo("Successfully executed Dismiss() on alert.");
            }
            else
            {
                DlkLogger.LogInfo("No alert found to execute Dismiss() on.");
            }
        }
    }
}