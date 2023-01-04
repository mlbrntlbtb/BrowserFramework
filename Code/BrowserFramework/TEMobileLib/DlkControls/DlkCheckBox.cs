using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
    

        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public new bool VerifyControlType()
        {
            FindElement();
            if (GetAttributeValue("type") == "checkbox")
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
                FindElement();
        }

        [Keyword("Set", new String[] { "1|text|Value|TRUE" })]
        public void Set(String IsChecked)
        {
            try
            {
                int retryCount = 0;
                Initialize();
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);
                Boolean bCurrentValue = GetCheckedState();
                while (++retryCount <= 3 && bCurrentValue != bIsChecked)
                {
                    ScrollIntoViewUsingJavaScript();
                    Click(4.3);
                    if (!DlkAlert.DoesAlertExist())  // If dialog is present, exit retry
                    {
                        bCurrentValue = GetCheckedState();
                        if (bCurrentValue == bIsChecked)
                        {
                            break;
                        }
                        else
                        {
                            DlkLogger.LogInfo("Set() failed. Retrying...");
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }
        public Boolean GetCheckedState()
        {
            Initialize();
            Boolean bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
            return bCurrentVal;
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String IsChecked)
        {
            try
            {
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);
                Boolean bCurrentValue = GetCheckedState();
                DlkAssert.AssertEqual("VerifyValue()", bIsChecked, bCurrentValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(ExpectedValue), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
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
