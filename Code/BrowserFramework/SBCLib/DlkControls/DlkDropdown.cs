using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium.Support.UI;

namespace SBCLib.DlkControls
{
    [ControlType("Dropdown")]
    public class DlkDropdown : DlkBaseControl
    {
        #region Constructors
        public DlkDropdown(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkDropdown(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropdown(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion


        #region Declarations
        private Boolean IsDropdownOpen = false;
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
        /// <param name="ExpectedValue"></param>
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
        /// Selects an item inside the dropdown with the given value
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                ShowDropdownList();
                if (mElement.TagName.Equals("select"))
                {
                    SelectElement mSelect = new SelectElement(mElement);
                    mSelect.SelectByText(Value);
                }
                IsDropdownOpen = false;
                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the value of a dropdown control
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("VerifyValue", new String[] { "1|text|Value|TRUE" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                string ActValue = string.Empty;
                if (mElement.TagName.Equals("select"))
                {
                    SelectElement mSelect = new SelectElement(mElement);
                    ActValue = mSelect.SelectedOption.Text;
                }
                DlkAssert.AssertEqual("VerifyValue() : ", ExpectedValue, ActValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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
                Initialize();
                string ActValue = base.GetParent().GetAttribute("class").Contains("notClickable") ? "true" : base.IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the value of a dropdown control
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("AssignValueToVariable", new String[] { "1|text|Value|TRUE" })]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                string ActValue = string.Empty;
                if (mElement.TagName.Equals("select"))
                {
                    SelectElement mSelect = new SelectElement(mElement);
                    ActValue = mSelect.SelectedOption.Text;
                }
                DlkVariable.SetVariable(VariableName, ActValue);
                DlkLogger.LogInfo($"AssignValueToVariable() passed. Variable:[{VariableName}], Value:[{ActValue}]");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void ShowDropdownList()
        {
            if (!IsDropdownOpen)
            {
                if (mElement.TagName.Equals("select"))
                {
                    mElement.Click();
                }
            }
            IsDropdownOpen = true;
        }
        #endregion
    }
 }

