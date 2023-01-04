using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private const string GridCheckboxClass = "gridCheckbox";
        private string mClass = "";

        #endregion

        #region CONSTRUCTORS

        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        //public DlkCheckBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            string sClass = GetAttributeValue("class");
            if (sClass.Contains(GridCheckboxClass))
            {
                mClass = GridCheckboxClass;
            }
        }
        public Boolean GetCheckedState()
        {
            Initialize();
            Boolean bCurrentVal;

            switch (mClass)
            {
                case GridCheckboxClass:
                    string sClass = GetAttributeValue("class");
                    if (sClass.Contains("checked"))
                    {
                        bCurrentVal = true;
                    }
                    else
                    {
                        bCurrentVal = false;
                    }
                    break;

                default:
                    bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                    break;
            }

            return bCurrentVal;
        }

        #endregion

        #region KEYWORDS

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                base.Click();
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|TRUE" })]
        public void Set(String TrueOrFalse)
        {
            try
            {
                int retryCount = 0;
                Initialize();
                Boolean bIsChecked = Convert.ToBoolean(TrueOrFalse);
                Boolean bCurrentValue = GetCheckedState();
                while (++retryCount <= 3 && bCurrentValue != bIsChecked)
                {
                    //  ScrollIntoViewUsingJavaScript();
                    Click(4.3);
                    bCurrentValue = GetCheckedState();
                }
                VerifyValue(TrueOrFalse);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String TrueOrFalse)
        {
            try
            {
                Boolean bIsChecked = Convert.ToBoolean(TrueOrFalse);
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

        #endregion
    }
}
