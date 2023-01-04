using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Threading;

namespace StormTouchCRMLib.DlkControls
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

                base.Click();

            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyBlank", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyBlank(String TrueOrFalse)
        {
            try
            {
                // perform checks before performing keyword logic
                bool expected = false;
                if (! Boolean.TryParse(TrueOrFalse, out expected))
                {
                    throw new ArgumentException("Parameter must be either true or false.");
                }
                Initialize();
                bool isBlank = false;

                DlkLogger.LogInfo("Looking at the button text...");
                isBlank = mElement.Text.Equals(String.Empty);

                DlkAssert.AssertEqual("Verify if text is blank/empty for button: " + mControlName, expected, isBlank);
                DlkLogger.LogInfo("VerifyBlank() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyBlank() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPressed", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyPressed(String TrueOrFalse)
        {
            try
            {
                Initialize();
                Boolean isPressed = false;

                DlkLogger.LogInfo("Looking at the button");
                // looks at mElement if it contains the class "x-button-pressed"
                if (mElement.GetAttribute("class").ToLower().Contains("pressed"))
                {
                    isPressed = true;
                    DlkLogger.LogInfo("Element is currently pressed.");
                }
                else
                {
                    DlkLogger.LogInfo("Looking at the button's container");
                    mElement = mElement.FindElement(By.XPath("./.."));
                    if (mElement.GetAttribute("class").ToLower().Contains("pressed"))
                    {
                        isPressed = true;
                        DlkLogger.LogInfo("Element is currently pressed.");
                    }
                }
                DlkAssert.AssertEqual("Verify if pressed for button: " + mControlName, TrueOrFalse.ToLower(), isPressed.ToString().ToLower());
                DlkLogger.LogInfo("VerifyPressed() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                // ensure that mapping is correct , I think we should almost always map the element with the x-innerhtml class for buttons..
                DlkButton mButton = new DlkButton("Button", mElement);
                String ActText = mElement.FindElements(By.TagName("input")).Count == 0 ? mButton.GetValue() : 
                    new DlkBaseControl("Input", mElement.FindElement(By.TagName("input"))).GetValue();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextBeforeBreakingSpace", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTextBeforeBreakingSpace(String ExpectedValue)
        {
            try
            {
                Initialize();
                // ensure that mapping is correct , I think we should almost always map the element with the x-innerhtml class for buttons..
                DlkButton mButton = new DlkButton("Button", mElement);
                String ActText = mElement.FindElements(By.TagName("input")).Count == 0 ? mButton.GetValue() :
                    new DlkBaseControl("Input", mElement.FindElement(By.TagName("input"))).GetValue();

                ActText = DlkString.GetStringBeforeBreakingSpace(ActText);
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyTextBeforeBreakingSpace() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextBeforeBreakingSpace() failed : " + e.Message, e);
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

        [Keyword("VerifyPageChanged", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPageChanged(String TrueOrFalse)
        {
            try
            {
                bool expected = false;
                if (!bool.TryParse(TrueOrFalse, out expected))
                {
                    throw new ArgumentException("Parameter must be either true or false");
                }
                Initialize();
                var beforeEvent = DlkEnvironment.AutoDriver.PageSource;
               
                new DlkBaseControl("Device button", mElement).Tap();
                // wait until entire document is ready after click
                for (int i = 0; !((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("return document.readyState").Equals("complete") && i < 10; i++)
                {
                    Thread.Sleep(1000);
                }
                var afterEvent = DlkEnvironment.AutoDriver.PageSource;
                var isPageUpdated = beforeEvent != afterEvent; // if not the same then it was updated

                DlkAssert.AssertEqual("Verify if page html was updated", expected, isPageUpdated);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPageChanged() failed. " + e.Message);
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

    }
}
