using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {
        private String mStrToolbarButtonsCSS = "li>a";
        private ReadOnlyCollection<IWebElement> mButtons;


        public DlkToolbar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolbar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }


        //[Keyword("ToolTipHoverAndVerifyText", new String[] {"1|text|Button Caption|Save", "2|text|Expected Value|SampleValue"})]
        public void ToolTipHoverAndVerifyText(String ButtonCaption, String ExpectedValue)
        {
            try
            {
                String pStrButtonCaption = ButtonCaption;
                IWebElement toolTip = null;
                Initialize();


                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(mElement.FindElement(By.XPath("//a[contains(.,'" + pStrButtonCaption + "')]"))).Build();

                DlkEnvironment.CaptureUrl();
                String oldUrl = DlkEnvironment.LastUrl;

                while(true)
                {
                    try
                    {
                        mAction.Perform();
                        mElement.FindElement(By.XPath("//a[contains(.,'" + pStrButtonCaption + "')]")).Click();
                        DlkEnvironment.CaptureUrl();
                        String newUrl = DlkEnvironment.LastUrl;
                        if (oldUrl.Equals(newUrl)) break;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                    toolTip = DlkEnvironment.AutoDriver.FindElement(By.XPath("//table[@class='bubbletip']//td[@class='bt-content']/div[contains(@id,'tabTip')]"));
                
                break;
            }
                DlkAssert.AssertEqual("ToolTipHoverAndVerifyText()", ExpectedValue, toolTip.Text);
                DlkLogger.LogInfo("Successfully executed ToolTipHoverAndVerifyText().");
            }
            catch (InvalidOperationException invalid)
            {
                //InvalidOperationCan be due to alert dialog from WaitUrlUpdate call above
                if (invalid.Message.Contains("Modal dialog present"))
                {
                    if (DlkAlert.DoesAlertExist(3))
                    {
                        DlkAlert.Accept();
                        Click();
                    }
                }
                else
                {
                    throw new Exception(string.Format("Exception of type {0} caught in button ToolTipHoverAndVerifyText() method.", invalid.GetType()), invalid);
                }
            }
            catch (Exception e)
            {

                throw e;

            }

        }

        [Keyword("ClickToolbarButton", new String[] { "1|text|Button Caption|Save"})]
        public void ClickToolbarButton(String ButtonCaption)
        {
            bool bFound = false;
            String sActualToolButtons = "";

            Initialize();
            mButtons = mElement.FindElements(By.CssSelector(mStrToolbarButtonsCSS));
            foreach (IWebElement aButton in mButtons)
            {

                DlkLink lnkToolbarButton = new DlkLink("Toolbar Button", aButton);
                if (lnkToolbarButton.GetValue() == ButtonCaption)
                {
                    lnkToolbarButton.Click();
                    bFound = true;
                    break;
                }
                
            }

            //If New Window's invoked
            //DlkEnvironment.FocusNewWindowHandle();

            if (bFound)
            {
                DlkLogger.LogInfo("Successfully executed ClickToolbarButton(). Control : " + mControlName + " : " + ButtonCaption);
            }
            else
            {
                throw new Exception("ClickToolbarButton() failed. Control : " + mControlName + " : '" + ButtonCaption + 
                                        "' button not found in toolbar. : Actual Buttons = " + sActualToolButtons);
            }
        }

        [RetryKeyword("VerifyToolbarButtonExists", new String[] { "1|text|Button Caption|Save",
                                                             "2|text|Expected Value|TRUE"})]
        public void VerifyToolbarButtonExists(String ButtonCaption, String TrueOrFalse)
        {
            String pStrButtonCaption = ButtonCaption;
            String pstrExpectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    bool bFound = false;

                    Initialize();
                    mButtons = mElement.FindElements(By.CssSelector(mStrToolbarButtonsCSS));
                    foreach (IWebElement aButton in mButtons)
                    {

                        DlkLink lnkToolbarButton = new DlkLink("Toolbar Button", aButton);
                        if (lnkToolbarButton.GetValue() == pStrButtonCaption)
                        {
                            bFound = true;
                            break;
                        }

                    }
                    DlkAssert.AssertEqual("VerifyToolbarButtonExists()", Convert.ToBoolean(pstrExpectedValue), bFound);
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyButtonTextContains", new String[] { "1|text|Button Caption|Save",
                                                             "2|text|Expected Text|1"})]
        public void VerifyButtonTextContains(String ButtonCaption, String ExpectedText)
        {
            String pStrButtonCaption = ButtonCaption;
            String pstrExpectedValue = ExpectedText;            

            this.PerformAction(() =>
            {
                bool bFound = false;

                Initialize();
                mButtons = mElement.FindElements(By.CssSelector(mStrToolbarButtonsCSS));
                foreach (IWebElement aButton in mButtons)
                {

                    DlkLink lnkToolbarButton = new DlkLink("Toolbar Button", aButton);
                    if (lnkToolbarButton.GetValue().Contains(pStrButtonCaption))
                    {
                        if(lnkToolbarButton.GetValue().Contains(pstrExpectedValue))
                        {
                            bFound = true;
                            break;
                        }
                    }

                }
                DlkAssert.AssertEqual("VerifyButtonTextContains()", true, bFound);
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyToolbarContainsButtons", new String[] { "1|text|Expected (TRUE or FALSE)|TRUE" })]
        public void VerifyToolbarContainsButtons(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    bool bFound = false;

                    Initialize();
                    mButtons = mElement.FindElements(By.CssSelector(mStrToolbarButtonsCSS));

                    if (mButtons != null)
                    {
                        if (mButtons.Count > 0)
                            bFound = true;
                    }

                    DlkAssert.AssertEqual("VerifyToolbarContainsButtons()", Convert.ToBoolean(expectedValue), bFound);
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String sExpectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyExists(Convert.ToBoolean(sExpectedValue));
                }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }
    }
}

