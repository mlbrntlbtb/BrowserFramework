using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                if (DlkEnvironment.mIsMobile)
                {
                    ScrollIntoViewUsingJavaScript();
                    base.Tap();
                }
                else
                {
                    base.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("ClickAndCheckHtmlChanges", new String[] { "1|text|Expected Value|SampleValue" })]
        public void ClickAndCheckHtmlChanges(String ExpectedValue)
        {
            try
            {
                //guard clause
                if (string.IsNullOrWhiteSpace(ExpectedValue)) throw new ArgumentException("Argument must be valid");
                Initialize();
                var htmlBeforeclick = DlkEnvironment.AutoDriver.PageSource;
                DlkLogger.LogInfo("Clicking the control...");
                mElement.Click();
                var htmlAfterClick = DlkEnvironment.AutoDriver.PageSource;
                DlkAssert.AssertEqual("Comparing Html before and after click... ", ExpectedValue, htmlBeforeclick.Equals(htmlAfterClick).ToString());
                DlkLogger.LogInfo("CheckIfHtmlChanged() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Repeatedly click the mapped button given the number of times
        /// </summary>
        [Keyword("ClickButtonRepeatedly", new String[] { "1|text|Expected Value|SampleValue" })]
        public void ClickButtonRepeatedly(String NumberOfClicks, String ClickInterval)
        {
            try
            {
                int clicks = int.Parse(NumberOfClicks);
                int delay = int.Parse(ClickInterval) * 1000;
                for (int i = 0; i < clicks; i++)
                {
                    // re-initialize to avoid stale element
                    Initialize();
                    // perform tap
                    new DlkBaseControl("btn", mElement).Tap();
                    DlkLogger.LogInfo("Clicked the button");
                    Thread.Sleep(delay);
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickButtonRepeatedly() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = new DlkBaseControl("TargetButton", mElement).GetValue().ToLower();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue.ToLower(), ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = new DlkBaseControl("TargetButton", mElement).GetValue();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextContains(String TextToVerify = "")
        {
            try
            {
                Initialize();
                String ActText = new DlkBaseControl("TargetButton", mElement).GetValue().ToLower();
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActText.Contains(TextToVerify.ToLower()));
                DlkLogger.LogInfo("VerifyTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", ExpectedValue.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetReadOnlyState", new String[] { "1|Variable Name|Sample" })]
        public void GetReadOnlyState(String VariableName)
        {
            try
            {
                String btnState = IsReadOnly();
                DlkVariable.SetVariable(VariableName, btnState);
                DlkLogger.LogInfo("Successfully executed GetReadOnlyState().Variable:[" + VariableName + "], Value:[" + btnState + "].");
            }
            catch (Exception e)
            {
                throw new Exception("GetReadOnlyState() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySelected", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySelected(String IsSelected)
        {
            try
            {
                Initialize();
                String className = mElement.FindElement(By.XPath(".//ancestor::div[1]")).GetAttribute("class");
                bool buttonSelected = className.Contains("button-pressed");

                if (!buttonSelected)
                {
                    buttonSelected = mElement.GetAttribute("class").Contains("jobCheckSelectAll");
                }

                DlkAssert.AssertEqual("VerifySelected()", Convert.ToBoolean(IsSelected), buttonSelected);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelected() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyBadgeCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyBadgeCount(String ExpectedCount)
        {
            try
            {

                Initialize();
                int actual = 0; 
                if (mElement != null && mElement.Displayed)
                {
                    try
                    {
                        var badge = mElement.FindElement(By.XPath("./following-sibling::*[@class='deltekMenuTabBadge-red']"));
                        if(mElement.Displayed && badge != null)
                        {
                            actual = Int32.Parse(badge.GetAttribute("text"));
                        }  
                    }
                    catch
                    {
                        DlkLogger.LogInfo("No badge count.");
                        actual = 0;
                    }                                
                }
                else
                {
                    throw new Exception("Control not found.");
                }
                int expected = int.Parse(ExpectedCount);

                DlkAssert.AssertEqual("VerifyBadgeCount()", expected, actual);
                DlkLogger.LogInfo("VerifyBadgeCount() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyBadgeCount() failed : " + e.Message, e);
            }
        }

    }
}
