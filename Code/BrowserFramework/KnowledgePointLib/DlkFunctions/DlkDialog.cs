using System;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;

namespace KnowledgePointLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
        #region Keywords
        [Keyword("ClickButtonOnDialogWithMessage", new String[] {"1|text|DialogText|Sample Text",
                                                     "2|text|ButtonText|Sample Text"})]
        public static void ClickButtonOnDialogWithMessage(String Message, String ButtonText)
        {
            try
            {
                DlkAssert.AssertEqual("Dialog text verification.", Message, GetDialogMessage());
                GetDialogButton(ButtonText).Click();
                DlkLogger.LogInfo("Successfully clicked '"+ ButtonText +"' on dialog.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickButtonOnDialogWithMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickButtonIfDialogExists", new String[] { "1|text|ButtonText|Sample text" })]
        public static void ClickButtonIfDialogExists(String ButtonText)
        {
            try
            {
                var dialog = GetDialog();

                if (dialog == null)
                {
                    DlkLogger.LogInfo("No dialog exists. Skipping...");
                }
                else
                {
                    GetDialogButton(ButtonText).Click();
                    DlkLogger.LogInfo("Successfully clicked '" + ButtonText + "' on alert.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickButtonIfDialogExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDialogExists", new String[] { "1|text|ButtonText|Sample text" })]
        public static void VerifyDialogExists(String TrueOrFalse)
        {
            try
            {
                var dialog = GetDialog();

                DlkAssert.AssertEqual("VerifyDialogExists()", Convert.ToBoolean(TrueOrFalse), dialog != null);
                DlkLogger.LogInfo("VerifyDialogExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDialogExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private static IWebElement GetDialogButton(string buttonText)
        {
            IWebElement button = null;
            try
            {
                var dialog = GetDialog();
                if (dialog == null) throw new Exception("Unable to find dialog.");
                button = dialog.FindElements(By.XPath(".//button")).First(btn => btn.Enabled && btn.Text == buttonText);
            }
            catch
            {
                throw new Exception("Cannot retrieve dialog buttons.");
            }
            return button;
        }

        private static String GetDialogMessage()
        {
            string dialogMsg = null;
            try
            {
                var dialogTxt = GetDialog().FindElement(By.XPath(".//*[contains(@id, 'DialogDescription')]"));
                dialogMsg = dialogTxt.Text;
            }
            catch
            {
                throw new Exception("Cannot retrieve dialog message.");
            }
            return dialogMsg;
        }

        private static IWebElement GetDialog()
        {
            IWebElement dialog;
            try
            {
                dialog = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[@role='dialog' and contains(@class, 'Dialog')]"));
            }
            catch(Exception)
            {
                dialog = null;
            }
            return dialog;
        }
        #endregion
    }
}
