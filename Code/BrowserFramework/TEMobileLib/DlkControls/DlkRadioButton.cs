using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using TEMobileLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        public DlkRadioButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                if (Convert.ToBoolean(Value)) // process only when Value is TRUE
                {
                    int retryCount = 0;
                    Initialize();
                    DlkTEMobileCommon.WaitForScreenToLoad();
                    Boolean bCurrentValue = GetState();
                    while (++retryCount <= 3 && bCurrentValue != true)
                    {
                        //ScrollIntoViewUsingJavaScript();
                        Click();
                        if (!DlkAlert.DoesAlertExist())  // If dialog is present, exit retry
                        {
                            bCurrentValue = GetState();
                            VerifyValue(true);
                        }
                        else
                        {
                            break;
                        }
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
            Boolean bCurrentVal = false;
            Initialize();
            switch (DlkEnvironment.mBrowser.ToLower())
            {
                case "ie":
                    bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("status"));
                    break;
                case "firefox":
                case "chrome":
                case "android":
                    bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                    break;
                case "safari":
                    DlkTEMobileCommon.WaitForElementToLoad(this);
                    bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                    break;
            }
            return bCurrentVal;
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String IsSelected)
        {
            try
            {
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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String IsTrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }
    }
}
