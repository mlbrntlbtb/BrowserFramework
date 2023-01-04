using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using FieldEaseLib.DlkSystem;

namespace FieldEaseLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTextBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }
        #endregion

        #region KEYWORDS
        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Value)
        {
            try
            {
                Initialize();
                mElement.Click();
                if (DlkEnvironment.mBrowser.ToLower() != "ie")
                {
                    mElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    mElement.SendKeys(Keys.Control + "a");
                    mElement.SendKeys(Keys.Delete);
                }

                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter(String Value)
        {
            try
            {
                Initialize();
                mElement.SendKeys(Keys.Control + "a");
                mElement.SendKeys(Keys.Delete);
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);
                    mElement.SendKeys(Keys.Return);
                }
                DlkLogger.LogInfo("Successfully executed SetAndEnter()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                string ActualValue = !String.IsNullOrEmpty(mElement.Text) ? mElement.Text : new DlkBaseControl("TextBox", mElement).GetValue().Trim();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPlaceholder", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyPlaceholder(String ExpectedValue)
        {
            try
            {
                Initialize();
                string placeHolderValue = mElement.GetAttribute("placeholder") != null ? mElement.GetAttribute("placeholder").Trim() : "";
                DlkAssert.AssertEqual("VerifyPlaceholder()", ExpectedValue, placeHolderValue);
                DlkLogger.LogInfo("VerifyPlaceholder() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPlaceholder() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyIfBlank", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyIfBlank(String ExpectedValue)
        {
            try
            {
                Initialize();

                bool expectedValue;
                if (!Boolean.TryParse(ExpectedValue, out expectedValue))
                    throw new Exception("[" + ExpectedValue + "] is not a valid input for parameter ExpectedValue.");

                bool fieldIsBlank = true;
                string ActualValue = !String.IsNullOrEmpty(mElement.Text) ? mElement.Text : new DlkBaseControl("TextBox", mElement).GetValue().Trim();
                if (!String.IsNullOrEmpty(ActualValue))
                {
                    fieldIsBlank = false;
                }

                DlkAssert.AssertEqual("VerifyIfBlank()", expectedValue, fieldIsBlank);
                DlkLogger.LogInfo("VerifyIfBlank() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfBlank() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
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
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetValue(String VariableName)
        {
            try
            {
                Initialize();
                string ActualValue = !String.IsNullOrEmpty(mElement.Text) ? mElement.Text : new DlkBaseControl("TextBox", mElement).GetValue().Trim();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetPlaceholderValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetPlaceholderValue(String VariableName)
        {
            try
            {
                Initialize();
                string placeHolderValue = mElement.GetAttribute("placeholder") != null ? mElement.GetAttribute("placeholder").Trim() : "";
                DlkVariable.SetVariable(VariableName, placeHolderValue);
                DlkLogger.LogInfo("[" + placeHolderValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetPlaceholderValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetPlaceholderValue() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
