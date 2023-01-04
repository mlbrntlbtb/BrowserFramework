using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        #region Declarations

        #endregion

        #region Constructors

        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            initialize();
        }

        public DlkCheckBox(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {
            initialize();
        }

        #endregion

        #region Keywords

        [Keyword("Set")]
        public void Set(string IsChecked)
        {
            try
            {
                bool bIsChecked = Convert.ToBoolean(IsChecked);
                bool bCurrentValue = isChecked;
                if (bCurrentValue != bIsChecked)
                {
                    this.ClickUsingJavaScript();
                }
                DlkLogger.LogInfo("Successfully executed Set(): " + mControlName);
            }
            catch (Exception ex)
            {
                throw new Exception("Set( ) failed : " + ex.Message, ex);
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

        [Keyword("VerifyText")]
        public void VerifyText(string Text)
        {
            try
            {
                DlkLabel parentLabelControl = new DlkLabel("Parent Label", mElement.FindElement(By.XPath("./..")));
                DlkAssert.AssertEqual("VerifyText() : ", Text.ToLower(), parentLabelControl.GetValue().ToLower());
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyValue")]
        public void VerifyValue(string TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = isChecked;
                DlkAssert.AssertEqual("VerifyValue() : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyValue() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyValue() execution failed. : " + ex.Message, ex);
            }
        }
        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion

        #region Properties

        private bool isChecked
        {
            get { return mElement.Selected; }
        }

        #endregion
    }
}
