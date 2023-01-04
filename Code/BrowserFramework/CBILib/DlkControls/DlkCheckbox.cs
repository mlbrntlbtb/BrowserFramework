using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CBILib.DlkControls;
using CBILib.DlkUtility;
using System.Threading;

namespace CBILib.DlkControls
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

        public void Initialize()
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
        /// Verifies if Checkbox exists. Requires strExpectedValue - can either be True or False
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

        #endregion

        #region Private Methods

        private bool GetCheckedState()
        {
            string className = mElement.GetAttribute("class");
            bool isChecked = false;

            if (className.Contains("checked"))
            {
                isChecked = true;
            }
            else if (className == "dijitCheckBoxInput")
            {
                string chkDivClass = mElement.FindElements(By.XPath("./parent::div")).FirstOrDefault()?.GetAttribute("class");
                isChecked = chkDivClass.Contains("dijitCheckBoxChecked");
            }
            return isChecked;
        }
        #endregion

    }
}
