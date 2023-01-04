using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Linq;

namespace TrafficLiveLib.DlkControls
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

        #region KEYWORDS

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

        [Keyword("AssignExistStatusToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignExistStatusToVariable(String VariableName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(VariableName)) throw new ArgumentException("VariableName must not be empty.");
                Initialize();
                var isBtnDisplayed = new DlkBaseControl("Button", mElement).Exists().ToString().ToLower();
                DlkVariable.SetVariable(VariableName, isBtnDisplayed);
                DlkLogger.LogInfo("AssignExistStatusToVariable() passed");
            }
            catch (Exception e)
            {
                throw new Exception("AssignExistStatusToVariable() failed : " + e.Message, e);
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
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", TrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySelected", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySelected(String IsSelected)
        {
            try
            {
                Initialize();
                String className = mElement.FindElement(By.XPath(".//ancestor::div[1]")).GetAttribute("class");
                bool buttonSelected;

                if(className.Contains("x-field-radio"))
                {
                    var colorElem = mElement.FindElements(By.XPath(".//div[@class='x-field-mask']")).Where(item => item.Displayed).FirstOrDefault();
                    string color = ((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteScript("return window.getComputedStyle(arguments[0], ':after').getPropertyValue('color')", colorElem).ToString();
                    buttonSelected = !color.Equals("rgb(221, 221, 221)");
                }
                else
                {
                    buttonSelected = className.Contains("button-pressed");
                }
                    
                DlkAssert.AssertEqual("VerifySelected()", Convert.ToBoolean(IsSelected), buttonSelected);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelected() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
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


        #endregion

    }
}
