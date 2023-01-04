using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Checkbox")]
    public class DlkCheckbox : DlkBaseControl
    {
        #region Constructors
        public DlkCheckbox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckbox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckbox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords

        /// <summary>>
        /// Sets the value of a checkbox to checked or unchecked. Requires TrueOrFalse - can either be True or False
        /// </summary>

        [Keyword("Set")]
        public void Set(String IsChecked)
        {
            try
            {
                Initialize();
                if (!Boolean.TryParse(IsChecked, out Boolean bValue)) 
                    throw new Exception($"Value: [{IsChecked}] is invalid for Checkbox. True Or False values are only accepted.");
                Boolean bCurrentValue = GetCheckedState();
                if (bCurrentValue != bValue)
                {
                    Click(4.5);
                }
                else
                {
                    DlkLogger.LogInfo("Checkbox already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("Set() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }
     
        /// <summary>
        ///  Verifies if Checkbox exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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
        ///  Verifies if checkbox is checked or not
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyState", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyState(String IsChecked)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyCheckboxState() : " + mControlName, GetCheckedState(), Convert.ToBoolean(IsChecked));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private methods

        public Boolean GetCheckedState()
        {
            try
            {
                Initialize();
                bool state = false;
                // checkboxes mapped under elemens with class = MuiCheckbox-root
                if (mElement.GetAttribute("class").Contains("MuiCheckbox-root"))
                {
                    if (mElement.GetAttribute("class").Contains("MuiCheckbox-checked") || mElement.GetAttribute("class").Contains("Mui-checked"))
                        state = true;
                }
                else if(mElement.GetAttribute("type") != null && mElement.GetAttribute("type") == "checkbox")
                {
                    state = mElement.GetAttribute("checked") != null;
                }
                else // for login page
                {
                    IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    string checkboxId = mElement.FindElement(By.XPath("./input")).GetAttribute("id");
                    object isChecked = javascript.ExecuteScript("return $('#" + checkboxId + "').is(':checked');", mElement);
                    state = Convert.ToBoolean(isChecked);
                }
                return state;
            }
            catch (NoSuchElementException)
            {
                // As of April2020: Checkbox in login page and in accounts page are the only supported checkbox types
                throw new Exception("VerifyExists() failed : Checkbox not supported");
            }
        }

        #endregion
    }
}
