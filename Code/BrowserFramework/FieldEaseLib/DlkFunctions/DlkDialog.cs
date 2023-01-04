using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkControls;

namespace FieldEaseLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
        [Keyword("ValidateMessage", new string[] { "1|text|message1~message2~message3", })]
        public static void ValidateMessage(string Message)
        {
            try
            {
                List<string> expectedMessages = new List<string>(Message.Split('~'));
                List<string> actualMesages = new List<string>();
                var messages = GetValidationMessages();
                bool notFound = false;

                if (messages.Count() == expectedMessages.Count())
                {
                    for (int i = 0; i < expectedMessages.Count(); i++)
                    {
                        string message = expectedMessages[i];
                        if (messages.Contains(message))
                        {
                            actualMesages.Add(message);
                        }
                        else
                        {
                            notFound = true;
                            break;
                        }
                    }
                }
                else
                    notFound = true;

                if (notFound)
                    DlkAssert.AssertEqual("ValidateMessage() :", expectedMessages.ToArray(), messages.ToArray());
                else
                    DlkAssert.AssertEqual("ValidateMessage() :", expectedMessages.ToArray(), actualMesages.ToArray());

                DlkLogger.LogInfo("ValidateMessage() : Passed");
            }
            catch (Exception ex)
            {
                throw new Exception("ValidateMessage() failed: " + ex.Message);
            }
        }

        [Keyword("ValidateMessagePart", new string[] { "1|text|message1~message2~message3", })]
        public static void ValidateMessagePart(string Message)
        {
            try
            {
                List<string> expectedMessages = new List<string>(Message.Split('~'));
                List<string> actualMesages = new List<string>();
                var messages = GetValidationMessages();
                bool notFound = false;

                for (int i = 0; i < expectedMessages.Count(); i++)
                {
                    string message = expectedMessages[i];
                    if (messages.Contains(message))
                    {
                        actualMesages.Add(message);
                    }
                    else
                    {
                        notFound = true;
                        break;
                    }
                }

                if (notFound)
                    DlkAssert.AssertEqual("ValidateMessagePart() :", expectedMessages.ToArray(), messages.ToArray());
                else
                    DlkAssert.AssertEqual("ValidateMessagePart() :", expectedMessages.ToArray(), actualMesages.ToArray());

                DlkLogger.LogInfo("ValidateMessagePart() : Passed");
            }
            catch (Exception ex)
            {
                throw new Exception("ValidateMessagePart() failed: " + ex.Message);
            }
        }

        [Keyword("ClickOkDialogWithMessage", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickOkDialogWithMessage(String Message)
        {
            DlkAlert.ClickOkDialogWithMessage(Message);
        }

        [Keyword("ClickOkDialogIfExists", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickOkDialogIfExists(String Message)
        {
            DlkAlert.ClickOkDialogIfExists(Message);
        }

        [Keyword("ClickCancelDialogWithMessage", new String[] { "1|text|Expected Message|Sample dialog message" })]
        public static void ClickCancelDialogWithMessage(String Message)
        {
            DlkAlert.ClickCancelDialogWithMessage(Message);
        }

        private static List<string> GetValidationMessages()
        {
            var dialog = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='ctl00_cphMain_rnServer_popup']"));
            string getStyle() => dialog.GetAttribute("style");
            int retry = 0;
            while (getStyle().Contains("display: none;") && retry != 30)
            {
                System.Threading.Thread.Sleep(100);
                retry++;
            }

            var dialogLi = dialog.FindElements(By.XPath("//div[@class='Default-NotificationContainer']//li"));
            List<string> messages = new List<string>();

            foreach (var item in dialogLi)
            {
                messages.Add(item.Text);
            }
            return messages;
        }
    }
}
