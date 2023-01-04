using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        #region Constructors
        public DlkRadioButton(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                if (!Boolean.TryParse(Value, out Boolean bValue))
                    throw new Exception($"Value: [{Value}] is invalid for RadioButton. True Or False values are only accepted.");
                if (bValue)
                {
                    Initialize();
                    Boolean bCurrentValue = GetState();
                    if (bCurrentValue != bValue)
                    {
                        Click(4.5);
                    }
                    else
                    {
                        DlkLogger.LogInfo("RadioButton is already selected. No action performed...");
                    }
                }
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String IsSelected)
        {
            try
            {
                Initialize();
                VerifyValue(Convert.ToBoolean(IsSelected));
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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
                DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods

        private Boolean GetState()
        {
            try
            {
                return mElement.FindElements(By.XPath(".//*[contains(@class, 'Mui-checked')]")).Any();
            }
            catch
            {
                throw new Exception("Cannot retrieve radio button state.");
            }
        }

        private void VerifyValue(Boolean IsSelected)
        {
            Boolean bCurrentValue = GetState();
            DlkAssert.AssertEqual("VerifyValue() : " + IsSelected.ToString() + " : " + mControlName, IsSelected, bCurrentValue);
        }

        #endregion
    }
}
