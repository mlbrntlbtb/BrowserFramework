using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Toggle")]
    public class DlkToggle : DlkBaseControl
    {
        #region Constructors
        public DlkToggle(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToggle(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToggle(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords

        /// <summary>>
        /// Sets the value of a Toggle to checked or unchecked. Requires TrueOrFalse - can either be True or False
        /// </summary>

        [Keyword("Set")]
        public void Set(String IsOn)
        {
            try
            {
                Initialize();
                if (!Boolean.TryParse(IsOn, out Boolean bValue))
                    throw new Exception($"Value: [{IsOn}] is invalid for Toggle. True Or False values are only accepted.");
                Boolean bCurrentValue = GetCheckedState();
                if (bCurrentValue != bValue)
                {
                    Click(4.5);
                }
                else
                {
                    DlkLogger.LogInfo("Toggle already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("Set() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if Toggle exists. Requires TrueOrFalse - can either be True or False
        ///  base VerifyExists not working for this control. Opted to use the approach below instead
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                if (!bool.TryParse(TrueOrFalse, out bool bValue))
                    throw new Exception($"Value: [{TrueOrFalse}] is invalid for Toggle. True Or False values are only accepted.");

                bool toggleExists;

                //find toggle type to verify if the toggle exists
                if (mElement.GetAttribute("type") == null)
                {
                    //some types of toggles do not have the type in their attributes
                    //look for the toggle type under the toggle control
                    var toggleType = mElement.FindElements(By.XPath(".//input[@type]")).FirstOrDefault();

                    if (toggleType == null)
                        toggleExists = false;

                    toggleExists = toggleType.GetAttribute("type").Length != 0; //exists
                }
                else
                {
                    toggleExists = mElement.GetAttribute("type").Length != 0; //exists
                }

                DlkAssert.AssertEqual("VerifyExists() : " + mControlName, toggleExists, bValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if Toggle is checked or not
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyState", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyState(String IsChecked)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyState() : " + mControlName, GetCheckedState(), Convert.ToBoolean(IsChecked));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyState() failed : " + e.Message, e);
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
                if (mElement.GetAttribute("checked") != null || mElement.GetAttribute("class").Contains("Mui-checked"))
                    state = true;
                return state;
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Keyword failed : Toggle not supported");
            }
        }

        #endregion
    }
}
