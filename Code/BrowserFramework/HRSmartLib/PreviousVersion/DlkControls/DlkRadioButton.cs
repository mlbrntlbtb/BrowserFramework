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
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        #region Declarations

        #endregion

        #region Constructors

        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        public DlkRadioButton(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("Select")]
        public void Select(string Value)
        {
            try
            {
                if (Convert.ToBoolean(Value))
                {
                    initialize();
                    ClickUsingJavaScript();
                    VerifyValue();
                }

                DlkLogger.LogInfo("Select ( ) passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Select ( ) execution failed. : " + ex.Message, ex);
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
                initialize();
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
                initialize();
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

        private bool isChecked
        {
            get { return mElement.Selected; }
        }

        private void initialize()
        {
            FindElement();
        }

        private void VerifyValue()
        {

        }

        #endregion
    }
}
