using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace TrafficLiveLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
        private static DlkBaseControl GetMessageBox()
        {
            DlkBaseControl ctlMessageBox = new DlkBaseControl("MessageBox", "XPATH_DISPLAY", "//div[contains(@class,'x-msgbox')]");
            return ctlMessageBox;
        }

        private static DlkBaseControl GetDialogButton(DlkBaseControl MessageBox, String ButtonCaption)
        {
            MessageBox.FindElement();
            IWebElement messageButton = MessageBox.mElement.FindElement(By.XPath("//span[@class='x-button-label'][contains(.,'" + ButtonCaption + "')]"));
            DlkBaseControl ctlButton = new DlkBaseControl("Button", messageButton);
            return ctlButton;
        }

        private static String GetMessageText(DlkBaseControl MessageBox)
        {
            MessageBox.FindElement();
            IWebElement messageTextElm = MessageBox.mElement.FindElement(By.XPath("//div[contains(@class,'x-msgbox-text')]//div[@class='x-innerhtml']"));
            DlkBaseControl ctlMessageText = new DlkBaseControl("MessageText", messageTextElm);
            return ctlMessageText.GetValue();
        }

        private static void ClickMessageButton(DlkBaseControl MessageBox, String ButtonCaption)
        {
            MessageBox.FindElement();
            IWebElement messageButton = MessageBox.mElement.FindElement(By.XPath("//span[@class='x-button-label'][contains(.,'" + ButtonCaption + "')]"));
            DlkBaseControl ctlButton = new DlkBaseControl("Button", messageButton);
            ctlButton.Click();
        }

        [Keyword("VerifyDialogExistsWithMessage", new String[] {"1|text|ExpectedMessage|Sample dialog message", 
                                                                "2|text|ExpectedValue (TRUE/FALSE)|TRUE"})]
        public static void VerifyDialogExistsWithMessage(String ExpectedMessage, String ExpectedValue)
        {
            DlkBaseControl ctlDialog = GetMessageBox();
            Boolean bExpectedValue = Convert.ToBoolean(ExpectedValue);
            Boolean bActual = ctlDialog.Exists(1);
            if(bExpectedValue)
            {
                String sActualText = GetMessageText(ctlDialog);
                DlkAssert.AssertEqual("VerifyDialogExistsWithMessage", ExpectedMessage, sActualText);
            }
            else
            {
                DlkAssert.AssertEqual("VerifyDialogExistsWithMessage", bExpectedValue, bActual);
            }
        }

        [Keyword("VerifyDialogButtonExists", new String[] {"1|text|ButtonCaption|OK", 
                                                                "2|text|ExpectedValue (TRUE/FALSE)|TRUE"})]
        public static void VerifyDialogButtonExists(String ButtonCaption, String ExpectedValue)
        {
            DlkBaseControl ctlDialog = GetMessageBox();
            DlkBaseControl ctlButton = GetDialogButton(ctlDialog, ButtonCaption);
            Boolean bExpectedValue = Convert.ToBoolean(ExpectedValue);
            Boolean bActual = ctlButton.Exists(1);
            DlkAssert.AssertEqual("VerifyDialogButtonExists", bExpectedValue, bActual);
        }

        [Keyword("ClickDialogButton", new String[] { "1|text|ButtonCaption|OK" })]
        public static void ClickDialogButton(String ButtonCaption)
        {
            DlkBaseControl ctlDialog = GetMessageBox();
            ClickMessageButton(ctlDialog, ButtonCaption);
            DlkLogger.LogInfo("ClickDialogButton() successfully executed.");
        }

        [Keyword("ClickDialogButtonIfExists", new String[] { "1|text|ButtonCaption|OK" })]
        public static void ClickDialogButtonIfExists(String ButtonCaption)
        {
            try
            {
                DlkBaseControl ctlDialog = GetMessageBox();
                ClickMessageButton(ctlDialog, ButtonCaption);
                DlkLogger.LogInfo("ClickDialogButtonIfExists() successfully executed.");
            }
            catch{
                DlkLogger.LogInfo("ClickDialogButtonIfExists(): Dialog box does not exist.");
            }
            
        }
    }
}
