using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AjeraTimeLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace AjeraTimeLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkAjeraTimeBaseControl
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
                    ClickUsingJavaScript();
                    bCurrentValue = GetCheckedState();
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
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
