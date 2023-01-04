using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkRadioButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }

        }

        [Keyword("Select", new String[] {"1|text|Value|TRUE"})]
        public void Select(String Value)
        {
            if (Convert.ToBoolean(Value))
            {
                Click();
                VerifyValue(true);
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
                    bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                    break;
            }
            return bCurrentVal;
        }

        [RetryKeyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String TrueOrFalse)
        {
            String isSelected = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyValue(Convert.ToBoolean(isSelected));
                }, new String[]{"retry"});
        }

        private void VerifyValue(Boolean IsSelected)
        {
            Boolean bCurrentValue = GetState();
            DlkAssert.AssertEqual("VerifyValue()", IsSelected, bCurrentValue);
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    /*
                    Boolean bActual = Exists();
                    if (bActual == Convert.ToBoolean(expectedValue))
                    {
                        DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bActual) + " : Expected = " + expectedValue);
                    }
                    else
                    {
                        throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bActual) + " : Expected = " + expectedValue));
                    }*/
                }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

     }
}

