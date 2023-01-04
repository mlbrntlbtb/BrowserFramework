using System;
using AjeraLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace AjeraLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkAjeraBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit = false;
        #endregion

        #region CONSTRUCTORS
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
        }

        #endregion

        #region KEYWORDS

        [Keyword("Set", new String[] { "TRUE|FALSE" })]
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
                    bCurrentValue = GetCheckedState();
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "TRUE|FALSE" })]
        public void VerifyValue(String IsChecked)
        {
            try
            {
                Initialize();
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

        [Keyword("VerifyExists", new String[] { "TRUE|FALSE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST
        [Keyword("SetByDescription", new String[] { "TRUE|FALSE" })]
        public void SetByDescription(String Description, String IsChecked)
        {
            try
            {
                int retryCount = 0;
                InitializeAllElements();
                GetElementByText(Description);
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);

                if (mElement != null)
                {
                    Boolean bCurrentValue = GetCheckedState();
                    while (++retryCount <= 3 && bCurrentValue != bIsChecked)
                    {
                        ScrollIntoViewUsingJavaScript();
                        mElement.Click();
                        bCurrentValue = GetCheckedState();
                    }
                    DlkLogger.LogInfo("Successfully executed SetByDescription()");
                }
                else
                    throw new Exception("SetByDescription() failed : Checkbox - " + Description + "not found.");
            }
            catch (Exception e)
            {
                throw new Exception("SetByDescription() failed : " + e.Message, e);
            }
        }


        [Keyword("SetByRow", new String[] { "TRUE|FALSE" })]
        public void SetByRow(String Row, String IsChecked)
        {
            try
            {
                int retryCount = 0;
                InitializeSelectedElement(Row);
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);

                if (mElement != null)
                {
                    Boolean bCurrentValue = GetCheckedState();
                    while (++retryCount <= 3 && bCurrentValue != bIsChecked)
                    {
                        ScrollIntoViewUsingJavaScript();
                        mElement.Click();
                        bCurrentValue = GetCheckedState();
                    }
                    DlkLogger.LogInfo("Successfully executed SetByRow()");
                }
                else
                    throw new Exception("SetByRow() failed : Checkbox [" + Row + "] not found.");
            }
            catch (Exception e)
            {
                throw new Exception("SetByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyByRow", new String[] { "TRUE|FALSE" })]
        public void VerifyByRow(String Row, String IsTrueOrFalse)
        {
            try
            {
                InitializeSelectedElement(Row);
                Boolean bIsTrueOrFalse = Convert.ToBoolean(IsTrueOrFalse);
                if (mElement != null)
                {
                    DlkAssert.AssertEqual("VerifyByRow() : Row " + Row, Convert.ToBoolean(IsTrueOrFalse), mElement.Selected);
                    
                }
                else
                    throw new Exception("VerifyByRow() failed : Checkbox [" + Row + "] not found.");
                
            }
            catch (Exception e)
            {
                throw new Exception("VerifyByRow() failed : " + e.Message, e);
            }
        }
        #endregion

        #region METHODS
        public Boolean GetCheckedState()
        {
            bool bCurrentVal = Convert.ToBoolean(mElement.GetAttribute("checked"));
            return bCurrentVal;
        }

        private void GetElementByText(string text)
        {
            foreach (var element in mElementList)
            {
                //for checkbox with label on next node
                var checkboxLabel = element.FindElement(By.XPath("./following-sibling::label[1]"));

                if (checkboxLabel.Text.ToLower().Equals(text.ToLower()))
                {
                    mElement = element;
                    break;
                }
            }

        }
        #endregion
    }
}
