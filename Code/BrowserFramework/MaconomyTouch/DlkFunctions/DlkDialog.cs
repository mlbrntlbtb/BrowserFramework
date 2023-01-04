using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyTouchLib.DlkFunctions
{
    [Component("Dialog")]
    public static class DlkDialog
    {
        private static DlkBaseControl GetMessageBox(int iDefaultSearchMaxValue = 40)
        {
            DlkBaseControl ctlMessageBox = new DlkBaseControl("MessageBox", "XPATH_DISPLAY", "//div[contains(@class,'x-msgbox ')]");
            ctlMessageBox.iFindElementDefaultSearchMax = iDefaultSearchMaxValue;
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
            IWebElement messageTextElm = MessageBox.mElement.FindElement(By.XPath(".//div[contains(@class,'x-msgbox-text')]//div[@class='x-innerhtml']"));
            DlkBaseControl ctlMessageText = new DlkBaseControl("MessageText", messageTextElm);
            return ctlMessageText.GetValue();
        }

        private static void ClickMessageButton(DlkBaseControl MessageBox, String ButtonCaption)
        {
            IWebElement messageButton;
            if (DlkEnvironment.mIsMobile)
            {
                MessageBox.FindElement();
                messageButton = MessageBox.mElement.FindElement(By.XPath("//span[@class='x-button-label'][contains(.,'" + ButtonCaption + "')]"));
            }
            else
            {
                messageButton = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[normalize-space(text())='" + ButtonCaption +"']"));
            }
            DlkBaseControl ctlButton = new DlkBaseControl("Button", messageButton);
            ctlButton.Click();
        }

        private static void SetDialogBoxText(DlkBaseControl MessageBox, String text)
        {
            MessageBox.FindElement();
            IWebElement textbox = MessageBox.mElement.FindElement(By.XPath("//textarea"));
            textbox.SendKeys(text);
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

        [Keyword("VerifyDialogExistsAndStoreToVar", new String[] {"1|text|ButtonCaption|OK", 
                                                                "2|text|ExpectedValue (TRUE/FALSE)|TRUE"})]
        public static void VerifyDialogExistsAndStoreToVar(String VariableName)
        {
            DlkBaseControl ctlDialog = GetMessageBox();
            Boolean bActual = ctlDialog.Exists(1);
            DlkVariable.SetVariable(VariableName, bActual.ToString().ToLower());
        }

        [Keyword("VerifyIfDialogButtonIsReadOnly", new String[] { "1|text|ButtonCaption|OK" })]
        public static void VerifyIfDialogButtonIsReadOnly(String ButtonCaption, String ExpectedValue)
        {
            //guard clause to check if ExpectedValue is either true or false.
            bool expected = false;//won't be used..
            bool.TryParse(ExpectedValue, out expected);
            DlkBaseControl ctlDialog = GetMessageBox();
            DlkBaseControl ctlButton = GetDialogButton(ctlDialog, ButtonCaption);
            String bExpectedValue = ExpectedValue;
            String bActual = ctlButton.IsReadOnly();
          
            //assert if readonly value is equivalent to expectedvalue
            DlkAssert.AssertEqual("VerifyIfDialogButtonIsReadOnly", bExpectedValue.ToLower(), bActual.ToLower());
            DlkLogger.LogInfo("VerifyIfDialogButtonIsReadOnly() successfully executed.");
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
                int iDefaultSearchMaxValue = 3; //set max timeout for searching dialog boxes
                DlkBaseControl ctlDialog = GetMessageBox(iDefaultSearchMaxValue);
                ClickMessageButton(ctlDialog, ButtonCaption);
                DlkLogger.LogInfo("ClickDialogButtonIfExists() successfully executed.");
            }
            catch{
                DlkLogger.LogInfo("ClickDialogButtonIfExists(): Dialog box does not exist.");
            }
            
        }

        [Keyword("RepeatedlyTypeAndClickOnDialogBox", new String[] { "1|text|ButtonCaption|OK" })]
        public static void RepeatedlyTypeAndClickOnDialogBox(String TextToEnter, String ButtonToClick, String NumberOfClicks, String ClickInterval)
        {
            try
            {
                int clicks = 0;
                int delayBetweenDialogBoxes = 0;
                int.TryParse(NumberOfClicks, out clicks);
                int.TryParse(ClickInterval, out delayBetweenDialogBoxes);

               
                for (int i = 0; i < clicks; i++)
                {
                    DlkBaseControl ctlDialog = GetMessageBox();
                    if (ctlDialog == null) break;
                    SetDialogBoxText(ctlDialog, TextToEnter);
                    ClickMessageButton(ctlDialog, ButtonToClick);
                    DlkLogger.LogInfo("Clicked " + ButtonToClick);
                    Thread.Sleep(delayBetweenDialogBoxes * 1000);
                }
            }
            catch
            {
                DlkLogger.LogInfo("RepeatedlyTypeAndClickOnDialogBox(): Dialog box does not exist.");
            }

        }
    }
}
