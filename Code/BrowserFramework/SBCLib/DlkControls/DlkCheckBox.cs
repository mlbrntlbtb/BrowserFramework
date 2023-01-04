using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace SBCLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        #region Constructors
        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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
        /// <summary>
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), base.IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Sets the value of a checkbox to checked or unchecked. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("SetValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetValue(String TrueOrFalse)
        {
            try
            {
                Initialize();
                if (!Boolean.TryParse(TrueOrFalse, out Boolean bValue)) throw new Exception($"Value: [{TrueOrFalse}] is invalid for Checkbox. True Or False values are only accepted.");                
                Boolean bCurrentValue = GetCheckedState();
                if ( bCurrentValue != bValue)
                {
                    this.ClickUsingJavaScript();
                }
                else
                {
                    DlkLogger.LogInfo("Checkbox already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("SetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetValue() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        public Boolean GetCheckedState()
        {
            Initialize();
            Boolean bCurrentVal = this.mElement.Selected;
            string bState = bCurrentVal ? "checked" : "unchecked";
            DlkLogger.LogInfo($"Checkbox is in [{ bState }] state");
            return bCurrentVal;
        }
        #endregion
    }
 }

