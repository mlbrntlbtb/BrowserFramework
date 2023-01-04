using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
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

        [Keyword("Set", new String[] {"1|text|Value|TRUE"})]
        public void Set(String IsChecked)
        {
            Initialize();
            Boolean bIsChecked = Convert.ToBoolean(IsChecked);
            Boolean bCurrentValue = GetCheckedState();
            if (bCurrentValue != bIsChecked)
            {
                Click(4.3);
            }
            VerifyValue(IsChecked);
            DlkLogger.LogInfo("Successfully executed Set(): " + mControlName);
        }

        public Boolean GetCheckedState()
        {
            Initialize();
            Boolean bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
            return bCurrentVal;
        }

        #region Verify methods
        [RetryKeyword("VerifyValue", new String[] {"1|text|Expected Value|TRUE"})]
        public void VerifyValue(String TrueOrFalse)
        {
            String isChecked = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Boolean bIsChecked = Convert.ToBoolean(isChecked);
                    Boolean bCurrentValue = GetCheckedState();
                    DlkAssert.AssertEqual("VerifyValue", bIsChecked, bCurrentValue);
                }, new String[] {"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Boolean bExists = Exists(10);

                    //DlkAssert.AssertEqual("VerifyExists", Convert.ToBoolean(expectedValue), bExists);
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));

                }, new String[] {"retry"});

        }

        [RetryKeyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public void GetVerifyExists(String TrueOrFalse, String VariableName)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {                    

                    Boolean bExists = base.Exists();
                    DlkVariable.SetVariable(VariableName, Convert.ToString(bExists == Convert.ToBoolean(expectedValue)));

                }, new String[] {"retry"});
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

        [RetryKeyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    String ActValue = IsReadOnly();
                    DlkAssert.AssertEqual("VerifyReadOnly", Convert.ToBoolean(expectedValue), Convert.ToBoolean(ActValue));
                }, new String[] {"retry"});
        }
        #endregion
    }
}

