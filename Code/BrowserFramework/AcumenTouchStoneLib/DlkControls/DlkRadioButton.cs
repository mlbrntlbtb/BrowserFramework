using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;

namespace AcumenTouchStoneLib.DlkControls
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


        public new bool VerifyControlType()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            if (GetAttributeValue("type") == "radio")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String TrueOrFalse)
        {
            try
            {
                if (Convert.ToBoolean(TrueOrFalse)) // process only when Value is TRUE
                {
                    Initialize();
                    Boolean bIsChecked = Convert.ToBoolean(TrueOrFalse);
                    new DlkBaseControl("CheckBox", mElement).ScrollIntoViewUsingJavaScript();
                    if (GetState() != bIsChecked)
                    {
                        Click(4.3);
                        if (bIsChecked != GetState())
                        {
                            ClickUsingJavaScript();
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|ExpectedResult|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                bool expectedResult, actualResult;

                if (!Boolean.TryParse(TrueOrFalse, out expectedResult)) throw new Exception("Invalid ExpectedResult value : [" + TrueOrFalse + "]");

                Initialize();
                actualResult = Boolean.Parse(IsReadOnly());
                DlkAssert.AssertEqual("VerifyReadOnly()", expectedResult, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }

        }

        [Keyword("GetButtonState", new String[] { "1|text|Value|TRUE" })]
        public void GetButtonState(String VariableName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(VariableName)) throw new ArgumentException("VariableName must not be empty");
                // GetState already invokes Initialize
                var state = GetState().ToString();
                DlkLogger.LogInfo(String.Format("Storing {0} to variable '{1}'", state, VariableName));
                DlkVariable.SetVariable(VariableName, state);
                DlkLogger.LogInfo("GetButtonState() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetButtonState() failed : " + e.Message, e);
            }
        }

        public Boolean GetState()
        {
            Boolean bCurrentVal = false;
            Initialize();
            bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
            return bCurrentVal;
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String TrueOrFalse)
        {
            try
            {
                VerifyValue(Convert.ToBoolean(TrueOrFalse));
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

        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyToolTip(String ExpectedValue)
        {
            Initialize();
            String ActToolTip = mElement.GetAttribute("title");
            DlkAssert.AssertEqual("Verify tooltip for button: " + mControlName, ExpectedValue, ActToolTip);
            DlkLogger.LogInfo("VerifyToolTip() passed");
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
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

        public new String IsReadOnly()
        {
            String sValue = "";

            sValue = mElement.GetAttribute("readonly");
            if (sValue != null)
            {
                DlkLogger.LogInfo("readonly");
                if (sValue == "readonly")
                {
                    sValue = "TRUE";
                }
                return sValue;
            }

            sValue = mElement.GetAttribute("readOnly");
            if (sValue != null)
            {
                DlkLogger.LogInfo("readOnly");
                return sValue;
            }

            sValue = mElement.GetAttribute("isDisabled");
            if (sValue != null)
            {
                DlkLogger.LogInfo("isDisabled");
                return sValue;
            }

            sValue = mElement.GetAttribute("disabled");
            if (sValue != null)
            {
                DlkLogger.LogInfo("disabled");
                return sValue;
            }

            sValue = mElement.GetAttribute("class");
            if (sValue.Contains("disabled"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            sValue = mElement.GetAttribute("contenteditable");
            if (sValue != null && sValue.Contains("false"))
            {
                DlkLogger.LogInfo("disabled");
                return "true";
            }

            return "false";
        }
    }
}
