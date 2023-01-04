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

namespace ngCRMLib.DlkControls
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
            FindElement();
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String TrueOrFalse)
        {
            try
            {
                if (Convert.ToBoolean(TrueOrFalse)) // process only when Value is TRUE
                {
                    int retryCount = 0;
                    Initialize();
                    Boolean bCurrentValue = GetState();
                    while (++retryCount <= 3 && bCurrentValue != true)
                    {
                        //ScrollIntoViewUsingJavaScript();
                        Click();
                        bCurrentValue = GetState();
                    }
                    VerifyValue(true);
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
    }
}
