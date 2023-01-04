using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldEaseLib.DlkControls
{
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        private bool IsInit = false;
        private IWebElement radioControl;

        #region Constructors
        public DlkRadioButton(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement) { }

        public DlkRadioButton(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue) { }

        public DlkRadioButton(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues) { }

        public DlkRadioButton(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public DlkRadioButton(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();

                if (mElement!=null)
                {
                    radioControl = mElement.FindElement(By.XPath(".//input[@type='radio']"));
                }

                this.ScrollIntoViewUsingJavaScript();
                IsInit = true;
            }
        }

        [Keyword("VerifyExists", new string[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(string TrueOrFalse)
        {
            try
            {
                Initialize();
                bool actual = radioControl != null;

                DlkAssert.AssertEqual("VerifyExists()", bool.Parse(TrueOrFalse), actual);
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                if (Convert.ToBoolean(Value)) // process only when Value is TRUE
                {
                    Initialize();
                    Boolean bCurrentValue = GetState();

                    if (bool.Parse(Value) != bCurrentValue)
                    {
                        radioControl.Click();
                    }
                }
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        private Boolean GetState()
        {
            var control = new DlkBaseControl("radioControl", radioControl);
            Boolean bCurrentVal = false;
            switch (DlkEnvironment.mBrowser.ToLower())
            {
                case "ie":
                    bCurrentVal = Convert.ToBoolean(control.GetAttributeValue("status"));
                    break;
                case "firefox":
                case "chrome":
                    bCurrentVal = Convert.ToBoolean(control.GetAttributeValue("checked"));
                    break;
                case "edge":
                    bCurrentVal = Convert.ToBoolean(control.GetAttributeValue("selected"));
                    break;
                default:
                    throw new ArgumentException($"GetState failed: {DlkEnvironment.mBrowser} browser not supported.");
            }
            return bCurrentVal;
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

        private void VerifyValue(Boolean IsSelected)
        {
            Boolean bCurrentValue = GetState();
            DlkAssert.AssertEqual("VerifyValue() : " + IsSelected.ToString() + " : " + mControlName, IsSelected, bCurrentValue);
        }
    }
}
